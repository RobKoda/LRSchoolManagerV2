// ReSharper disable NotAccessedPositionalProperty.Global - Implicit use
namespace LRSchoolV2.Domain.Common;

public record Document(
    Guid Id, 
    Guid ReferenceId,
    string FileName,
    string ContentType,
    byte[] FileContent);