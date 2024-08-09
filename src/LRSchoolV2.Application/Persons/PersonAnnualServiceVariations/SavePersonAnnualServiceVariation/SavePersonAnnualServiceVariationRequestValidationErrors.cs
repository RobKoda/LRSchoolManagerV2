namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.SavePersonAnnualServiceVariation;

public static class SavePersonAnnualServiceVariationRequestValidationErrors
{
    public const string PersonAnnualServiceVariationNotUnique = "Cette inscription existe déjà";
    public const string PersonNotRegisteredForYear = "La personne n'est pas adhérente à cette année";
    public const string PaymentCountNotPositive = "Le nombre de paiement doit être positif";
    public const string ServiceHasNoPrice = "Le service annuel n'a pas de prix configuré pour l'année scolaire choisie";
}