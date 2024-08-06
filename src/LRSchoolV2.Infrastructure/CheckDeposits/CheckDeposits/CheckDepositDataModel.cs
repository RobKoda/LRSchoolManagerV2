using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Domain.CheckDeposits;
using LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable CollectionNeverUpdated.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits;

[Table(nameof(CheckDeposit))]
public class CheckDepositDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime Date { get; set; }

    [MaxLength(64)]
    public string Number { get; set; } = string.Empty;

    [InverseProperty(nameof(CheckDepositPaymentDataModel.CheckDeposit))]
    public ICollection<CheckDepositPaymentDataModel> CheckDepositPayments { get; set; } = new List<CheckDepositPaymentDataModel>();
    
    [NotMapped]
    public bool HasDocument { get; set; }
}