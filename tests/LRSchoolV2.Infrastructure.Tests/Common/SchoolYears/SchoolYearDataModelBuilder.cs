using AutoFixture;
using AutoFixture.Kernel;
using LRSchoolV2.Infrastructure.Common.SchoolYears;

namespace LRSchoolV2.Infrastructure.Tests.Common.SchoolYears;

public class SchoolYearDataModelBuilder(IFixture inFixture) : ISpecimenBuilder
{
    public object Create(object inRequest, ISpecimenContext inContext)
    {
        if (inRequest is not Type type)
            return new NoSpecimen();
        
        if (type != typeof(SchoolYearDataModel))
            return new NoSpecimen();
        
        var result = inFixture.Build<SchoolYearDataModel>()
            .With(inSchoolYear => inSchoolYear.StartDate, DateTime.Today)
            .With(inSchoolYear => inSchoolYear.EndDate, DateTime.Today.AddYears(1))
            .Create();
        
        return result;
    }
}