using LanguageExt;
using LRSchoolV2.Domain.Common;

namespace LRSchoolV2.Application.Common.Documents.GetDocumentPerReferenceId;

public record GetDocumentPerReferenceIdResponse(Option<Document> Document);