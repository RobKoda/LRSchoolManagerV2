﻿using LRSchoolV2.Domain.Persons;

namespace LRSchoolV2.Application.Persons.PersonAnnualServiceVariations.GetPersonAnnualServiceVariationsPerPerson;

public record GetPersonAnnualServiceVariationsPerPersonResponse(IEnumerable<PersonAnnualServiceVariation> PersonAnnualServiceVariations);