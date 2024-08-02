using AutoFixture;
using AutoFixture.Kernel;
using LRSchoolV2.Infrastructure.Common.SchoolYears;

// ReSharper disable UnusedType.Global - Implicit use
namespace LRSchoolV2.Infrastructure.Tests.Common.SchoolYears;

public class SchoolYearDataModelBuilder(IFixture inFixture) : ISpecimenBuilder
{
    public object Create(object inRequest, ISpecimenContext inContext)
    {
        if (inRequest is not Type type)
            return new NoSpecimen();
        
        if (type != typeof(SchoolYearDataModel))
            return new NoSpecimen();
        
        var date = inFixture.Create<DateTime>();
        var result = inFixture.Build<SchoolYearDataModel>()
            .With(inSchoolYear => inSchoolYear.StartDate, date)
            .With(inSchoolYear => inSchoolYear.EndDate, date.AddYears(1))
            .Create();
        
        return result;
    }
}