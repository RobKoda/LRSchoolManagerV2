// ReSharper disable UnusedMemberInSuper.Global - Setter must be implemented for EF core type
namespace LRSchoolV2.Infrastructure.Core;

public interface IGuidEntity
{
    public Guid Id { get; set; }
}