using AutoFixture;

namespace LRSchoolV2.Tests.Core;

public class DateAndTimeOnlyCustomization : ICustomization
{
    public void Customize(IFixture inFixture)
    {
        inFixture.Customize<DateOnly>(inComposer => inComposer.FromFactory<DateTime>(DateOnly.FromDateTime));
        inFixture.Customize<TimeOnly>(inComposer => inComposer.FromFactory<DateTime>(TimeOnly.FromDateTime));
    }
}