using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits;
using LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;

// ReSharper disable UnusedMember.Global - Implicit use
// ReSharper disable PropertyCanBeMadeInitOnly.Global - Implicit use

namespace LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments;

[Table("CheckDepositPayment")]
public class CheckDepositPaymentDataModel : IGuidEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(CheckDeposit))]
    public Guid CheckDepositId { get; set; }
    public CheckDepositDataModel? CheckDeposit { get; set; }
    
    [ForeignKey(nameof(CustomerPayment))]
    public Guid CustomerPaymentId { get; set; }
    public CustomerPaymentDataModel? CustomerPayment { get; set; }
}