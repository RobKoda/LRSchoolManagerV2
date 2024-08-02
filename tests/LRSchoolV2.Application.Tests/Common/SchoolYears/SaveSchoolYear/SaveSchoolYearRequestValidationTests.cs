using FluentAssertions;
using FluentValidation;
using LanguageExt;
using LRSchoolV2.Application.Common.SchoolYears.SaveSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Application.Tests.Core;
using LRSchoolV2.Domain.Common;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.SaveSchoolYear;

public class SaveSchoolYearRequestValidationTests
{
    private readonly SaveSchoolYearRequestValidation _validation;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;
    
    public SaveSchoolYearRequestValidationTests()
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _mockRepository.Setup(inRepository => inRepository.GetPreviousSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GetValidPreviousYear());
        _mockRepository.Setup(inRepository => inRepository.GetNextSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(GetValidNextYear());
        _validation = new SaveSchoolYearRequestValidation(_mockRepository.Object);
    }
    
    private static SaveSchoolYearRequest GetValidSaveSchoolYearRequest() =>
        new(GetValidSchoolYear());
    
    private static SchoolYear GetValidSchoolYear() =>
        new(Guid.NewGuid(),
            new DateTime(2000, 1, 1),
            new DateTime(2000, 12, 31),
            10);
    
    private static SchoolYear GetValidPreviousYear() => 
        new(Guid.NewGuid(), 
            new DateTime(1999, 1, 1), 
            new DateTime(1999, 12, 31), 
            5);
    
    private static SchoolYear GetValidNextYear() => 
        new(Guid.NewGuid(), 
            new DateTime(2001, 1, 1), 
            new DateTime(2001, 12, 31), 
            15);
    
    [Fact]
    public async Task ValidateAsync_ShouldSucceed()
    {
        // Arrange
        var request = GetValidSaveSchoolYearRequest();

        // Act
        var result = await _validation.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenStartDateNotRightAfterPreviousEndDate()
    {
        // Arrange
        var schoolYear = GetValidSchoolYear();
        schoolYear = schoolYear with
        {
            StartDate = schoolYear.StartDate.AddDays(-1)
        };
        var request = new SaveSchoolYearRequest(schoolYear);
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.ShouldError($"{nameof(SaveSchoolYearRequest.SchoolYear)}.{nameof(SaveSchoolYearRequest.SchoolYear.StartDate)}",
            SaveSchoolYearRequestValidationErrors.SchoolYearStartDateNotRightAfterPreviousEndDate);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldSucceed_GivenNoPreviousSchoolYear()
    {
        // Arrange
        _mockRepository.Setup(inRepository => inRepository.GetPreviousSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Option<SchoolYear>.None);
        var request = GetValidSaveSchoolYearRequest();
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenEndDateNotRightBeforeNextStartDate()
    {
        // Arrange
        var schoolYear = GetValidSchoolYear();
        schoolYear = schoolYear with
        {
            EndDate = schoolYear.EndDate.AddDays(1)
        };
        var request = new SaveSchoolYearRequest(schoolYear);
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.ShouldError($"{nameof(SaveSchoolYearRequest.SchoolYear)}.{nameof(SaveSchoolYearRequest.SchoolYear.EndDate)}",
            SaveSchoolYearRequestValidationErrors.SchoolYearEndDateNotRightBeforeNextStartDate);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldSucceed_GivenNoNextSchoolYear()
    {
        // Arrange
        _mockRepository.Setup(inRepository => inRepository.GetNextSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Option<SchoolYear>.None);
        var request = GetValidSaveSchoolYearRequest();
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenMembershipIsNegativeOrZero() =>
        await _validation.ValidateDecimalIsGreater(
            GetValidSaveSchoolYearRequest,
            (inRequest, _, inNewValue) => new SaveSchoolYearRequest(inRequest.SchoolYear with
            {
                MembershipFee = inNewValue
            }),
            $"{nameof(SaveSchoolYearRequest.SchoolYear)}.{nameof(SaveSchoolYearRequest.SchoolYear.MembershipFee)}",
            SaveSchoolYearRequestValidationErrors.SchoolYearMembershipFeeNotPositive);
}