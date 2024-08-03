using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.Common;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.Common.Documents;

[Table(nameof(Document))]
public class DocumentDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public Guid ReferenceId { get; set; }

    [MaxLength(1024)]
    public string FileName { get; set; } = string.Empty;
    
    [MaxLength(128)]
    public string ContentType { get; set; } = string.Empty;

    public byte[] FileContent { get; set; } = [];
}