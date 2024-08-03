namespace LRSchoolV2.Application.AnnualServices.AnnualServiceVariationYearlyPrices.DeleteAnnualServiceVariationYearlyPrice;

public static class DeleteAnnualServiceVariationYearlyPriceRequestValidationErrors
{
    public const string AnnualServiceVariationYearlyPriceNotFound = "Le prix annuel n'a pas été trouvé";
    public const string AnnualServiceVariationYearlyPriceCannotBeDeleted = "Le prix annuel ne peut pas être supprimé car il est lié à d'autres données";
}