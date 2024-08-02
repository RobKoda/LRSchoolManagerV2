using AutoFixture;
using FluentAssertions;
using FluentValidation;
using LRSchoolV2.Application.Common.SchoolYears.DeleteSchoolYear;
using LRSchoolV2.Application.Common.SchoolYears.Persistence;
using LRSchoolV2.Application.Tests.Core;
using LRSchoolV2.Domain.Common;
using Moq;

namespace LRSchoolV2.Application.Tests.Common.SchoolYears.DeleteSchoolYear;

public class DeleteSchoolYearRequestValidationTests
{
    private readonly DeleteSchoolYearRequestValidation _validation;
    private readonly IFixture _fixture;
    private readonly Mock<ISchoolYearsRepository> _mockRepository;

    public DeleteSchoolYearRequestValidationTests()
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        
        _fixture = new Fixture();
        
        _mockRepository = new Mock<ISchoolYearsRepository>();
        _mockRepository.Setup(inRepository => inRepository.AnySchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _mockRepository.Setup(inRepository => inRepository.CanSchoolYearBeDeletedAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _mockRepository.Setup(inRepository => inRepository.GetPreviousSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_fixture.Create<SchoolYear>());
        _validation = new DeleteSchoolYearRequestValidation(_mockRepository.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldSucceed()
    {
        // Arrange
        var request = _fixture.Create<DeleteSchoolYearRequest>();

        // Act
        var result = await _validation.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenSchoolYearNotFound()
    {
        // Arrange
        var request = _fixture.Create<DeleteSchoolYearRequest>();
        _mockRepository.Setup(inRepository => inRepository.AnySchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        var result = await _validation.ValidateAsync(request);

        // Assert
        result.ShouldError($"{nameof(DeleteSchoolYearRequest.Id)}",
            DeleteSchoolYearRequestValidationErrors.SchoolYearNotFound);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenSchoolYearCannotBeDeleted()
    {
        // Arrange
        var request = _fixture.Create<DeleteSchoolYearRequest>();
        _mockRepository.Setup(inRepository => inRepository.CanSchoolYearBeDeletedAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.ShouldError($"{nameof(DeleteSchoolYearRequest.Id)}",
            DeleteSchoolYearRequestValidationErrors.SchoolYearCannotBeDeleted);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenThereArePreviousAndNextSchoolYear()
    {
        // Arrange
        var request = _fixture.Create<DeleteSchoolYearRequest>();
        _mockRepository.Setup(inRepository => inRepository.GetNextSchoolYearAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_fixture.Create<SchoolYear>());
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.ShouldError($"{nameof(DeleteSchoolYearRequest.Id)}",
            DeleteSchoolYearRequestValidationErrors.SchoolYearBetweenTwoSchoolYears);
    }
}