﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using LRSchoolV2.Domain.ConsultantQuotes;
using LRSchoolV2.Reporting.Core;
using Spire.Doc;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using TableCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;
using TableRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;

namespace LRSchoolV2.Reporting.Consultants.ConsultantQuotes;

public class ConsultantQuoteReport(ConsultantQuote inConsultantQuote, IEnumerable<ConsultantQuoteItem> inConsultantQuoteItems, byte[] inConsultantQuoteDocument)
{
    private string ExportFileNameNoExtension => $"{inConsultantQuote.Number} - {inConsultantQuote.Consultant.GetFullName()} - {inConsultantQuote.Date:yyyy-MM-dd}.";
    private string ExportFileNameDocx => $"{ExportFileNameNoExtension}docx";
    public string ExportFileName => $"{ExportFileNameNoExtension}pdf";
    public const string ContentType = "application/pdf";

    public async Task<MemoryStream> GetReportMemoryStreamAsync()
    {
        var exportDocument = await CreateExportFileAsync();
        var exportMainPart = exportDocument.MainDocumentPart!;

        ReplaceTexts(exportMainPart);
        FillTable(exportMainPart);

        SaveAndClose(exportMainPart, exportDocument);
        return ConvertToPdf();
    }

    private MemoryStream ConvertToPdf()
    {
        var document = new Spire.Doc.Document();
        document.LoadFromFile(ExportFileNameDocx);

        document.JPEGQuality = 100;
        var memoryStream = new MemoryStream();
        document.SaveToStream(memoryStream, FileFormat.PDF);
        document.Dispose();
        File.Delete(ExportFileNameDocx);
        return memoryStream;
    }

    // ReSharper disable once SuggestBaseTypeForParameter - Better performances 
    private static void SaveAndClose(MainDocumentPart inExportMainPart, WordprocessingDocument inExportDocument)
    {
        inExportMainPart.Document.Save();
        inExportDocument.Dispose();
    }

    private async Task<WordprocessingDocument> CreateExportFileAsync()
    {
        if (File.Exists(ExportFileNameDocx))
        {
            File.Delete(ExportFileNameDocx);
        }

        var stream = new MemoryStream(inConsultantQuoteDocument);
        var fileStream = File.Create(ExportFileNameDocx);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
        await fileStream.DisposeAsync();
        
        var exportDocument = WordprocessingDocument.Open(ExportFileNameDocx, true);
        return exportDocument;
    }

    private void ReplaceTexts(MainDocumentPart? inExportMainPart)
    {
        var textElements = inExportMainPart!.Document
            .Descendants<Text>()
            .ToList();

        foreach (var textElement in textElements)
        {
            textElement.ReplaceIfFound("[QuoteNumber]", inConsultantQuote.Number);
            textElement.ReplaceIfFound("[QuoteDate]", inConsultantQuote.Date.ToString("dd/MM/yyyy"));
        }
    }

    private void FillTable(MainDocumentPart? inExportMainPart)
    {
        var table = inExportMainPart!.Document
            .Descendants<Table>()
            .Single(inTable => inTable.InnerText.Contains("Quantité"));

        var rows = table.Elements<TableRow>().ToList();
        for (var i = 0 ; i < inConsultantQuoteItems.Count() ; i++)
        {
            var item = inConsultantQuoteItems.OrderBy(inItem => inItem.Order).ElementAt(i);
            var row = rows.ElementAt(i + 1);
            var cells = row.Elements<TableCell>().ToList();
            
            cells.WriteCell(0, item.Quantity.ToString(), JustificationValues.Left);
            cells.WriteCell(1, item.Denomination, JustificationValues.Left);
            cells.WriteCell(2, $"{item.UnitPrice:0.00}€", JustificationValues.Right);
            cells.WriteCell(3, $"{item.GetTotal():0.00}€", JustificationValues.Right);
        }

        var lastRow = rows.Last();
        var lastRowCells = lastRow.Elements<TableCell>().ToList();
        lastRowCells.WriteCell(lastRowCells.Count - 1, $"{inConsultantQuote.GetTotalToPayFromItems(inConsultantQuoteItems):0.00}€", JustificationValues.Right, true, 24);
    }
}