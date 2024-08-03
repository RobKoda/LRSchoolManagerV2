using LRSchoolV2.Domain.Common;
using MediatR;

namespace LRSchoolV2.Application.Common.Documents.SaveDocument;

public record SaveDocumentCommand(Document Document) : IRequest;