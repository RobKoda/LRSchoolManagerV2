﻿using LanguageExt;
using LRSchoolV2.Application.Core;
using LRSchoolV2.Domain.Common;

// ReSharper disable UnusedType.Global - Auto scan
namespace LRSchoolV2.Application.Common.Documents.Persistence;

public interface IDocumentsRepository : IRepository
{
    Task<Option<Document>> GetDocumentPerReferenceIdAsync(Guid inReferenceId);
    Task SaveDocumentAsync(Document inDocument);
    Task DeleteDocumentPerReferenceIdAsync(Guid inReferenceId);
}