using MediatR;

namespace LRSchoolV2.Application.Common.Documents.GetDocumentPerReferenceId;

public record GetDocumentPerReferenceIdQuery(Guid ReferenceId) : IRequest<GetDocumentPerReferenceIdResponse>;