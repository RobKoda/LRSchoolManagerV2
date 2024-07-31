using System.Net;
using FluentAssertions;
using LRSchoolV2.Acceptance.Drivers;
using TechTalk.SpecFlow;

namespace LRSchoolV2.Acceptance.Steps;

[Binding]
public class HealthcheckSteps(HealthcheckDriver inDriver)
{
    [When("I GET the Healthcheck")]
    public async Task WhenIGetTheHealthcheck() =>
        await inDriver.GetHealthcheckAsync();

    [Then("I should receive an OK result")]
    public void ThenIShouldReceiveAnOkResult() =>
        inDriver.GetHealthcheckStatusCode().Should().Be(HttpStatusCode.OK);
}