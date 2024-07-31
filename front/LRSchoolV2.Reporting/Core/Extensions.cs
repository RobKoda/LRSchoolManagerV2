using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace LRSchoolV2.Reporting.Core;

public static class Extensions
{
    public static void ReplaceIfFound(this Text inTextElement, string inPattern, string inValue)
    {
        if (!inTextElement.Text.Contains(inPattern)) return;

        var lines = inValue.Split('\n');
        inTextElement.Text = inTextElement.Text.Replace(inPattern, lines[0]);

        for (var i = 1; i < lines.Length; i++)
        {
            inTextElement.Parent!.AppendChild(new Break());
            inTextElement.Parent!.AppendChild(new Text(lines[i]));
        }
    }

    public static void WriteCell(this IEnumerable<TableCell> inCells, int inIndex, string inText, JustificationValues inHorizontalAlignment, bool inBold = false, int? inFontSize = null)
    {
        var cell = inCells.ElementAt(inIndex);
        cell.RemoveAllChildren();

        var runProperties = new RunProperties
        {
            Bold = new Bold { Val = OnOffValue.FromBoolean(inBold) }
        };
        if (inFontSize.HasValue)
        {
            runProperties.FontSize = new FontSize
            {
                Val = inFontSize.ToString()
            };
        }
        var run = new Run(runProperties, new Text(inText));
        
        var paragraphProperties = new ParagraphProperties
        {
            Justification = new Justification { Val = inHorizontalAlignment }
        };
        var paragraph = new Paragraph(paragraphProperties, run);

        var cellProperties = new TableCellProperties
        {
            TableCellVerticalAlignment = new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center }
        };
        cell.Append(cellProperties, paragraph);
    }
}