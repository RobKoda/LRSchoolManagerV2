using LRSchoolV2.Domain.Consultants;

namespace LRSchoolV2.Application.Consultants.GetConsultants;

public record GetConsultantsResponse(IEnumerable<Consultant> Consultants);
