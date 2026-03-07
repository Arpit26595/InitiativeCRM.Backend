using AutoFixture;
using FluentAssertions;
using Lead.Application.DTOs;
using Lead.Application.DTOs.Request;
using Lead.Application.DTOs.Response;
using Lead.Application.Interfaces;
using Lead.Application.Services;
using Lead.Domain.Entities;
using Lead.Domain.Enums;
using Moq;
using Shared.Models.Utilities.Filters;
using Shared.Models.Utilities.Paging;

namespace Lead.Tests.Unit.Services;

public class LeadServiceTests
{
    private readonly Mock<ILeadRepository> _mockRepository;
    private readonly LeadService _service;
    private readonly Fixture _fixture;

    public LeadServiceTests()
    {
        _mockRepository = new Mock<ILeadRepository>();
        _service = new LeadService(_mockRepository.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task CreateAsync_ShouldCallRepository_WithCorrectData()
    {
        CancellationToken cancellationToken=new CancellationToken();
        // Arrange
        var dto = new CreateLeadDto
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith@example.com",
            Phone = "555-1234",
            EstimatedValue = 50000,
            Probability = 75
        };

        // Act
        await _service.CreateAsync(dto,cancellationToken);

        // Assert
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Leads>(), default), Times.Once);
        //_mockRepository.Verify(r => r.CreateAsync(default), Times.Once);
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnPaginatedResults()
    {
        // Arrange
        var request = new LeadSearchRequest<object>
        {
            PageNumber = 1,
            PageSize = 10,
            Filters = new List<Filter<object>>
            {
                new Filter<object>
                {
                    Id = "FirstName",
                    Value = "John",
                    Operation = Operation.Contains
                }
            }
        };

        var expectedResponse = new PagingResponseDto<LeadResponse>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalCount = 5,
            TotalPages = 1,
            Items = new[]
            {
                new LeadResponse
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com"
                }
            }
        };

        _mockRepository
            .Setup(r => r.SearchAsync(request, default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchAsync(request, default);

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(1);
        result.TotalCount.Should().Be(5);
        result.Items.Should().HaveCount(1);
        result.Items[0].FirstName.Should().Be("John");
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnFilterAndPaginatedResults()
    {
        // Arrange
        var request = new LeadSearchRequest<object>
        {
            PageNumber = 1,
            PageSize = 10,
            Filters = new List<Filter<object>>
            {
                new Filter<object>
                {
                    Id = "FirstName",
                    Value = "John",
                    Operation = Operation.Contains
                }
            }
        };

        var expectedResponse = new PagingResponseDto<LeadResponse>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalCount = 5,
            TotalPages = 1,
            Items = new[]
            {
                new LeadResponse
                {
                    LeadId = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com"
                }
            }
        };

        _mockRepository
            .Setup(r => r.SearchAsync(request, default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchAsync(request, default);

        // Assert
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(1);
        result.TotalCount.Should().Be(5);
        result.Items.Should().HaveCount(1);
        result.Items[0].FirstName.Should().Be("John");
    }

    [Fact]
    public async Task SearchAsync_WithNoFilters_ShouldReturnAllResults()
    {
        // Arrange
        var request = new LeadSearchRequest<object>
        {
            PageNumber = 1,
            PageSize = 10
        };

        var expectedResponse = new PagingResponseDto<LeadResponse>
        {
            CurrentPage = 1,
            PageSize = 10,
            TotalCount = 25,
            TotalPages = 3,
            Items = _fixture.CreateMany<LeadResponse>(10).ToArray()
        };

        _mockRepository
            .Setup(r => r.SearchAsync(request, default))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchAsync(request, default);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(10);
        result.TotalCount.Should().Be(25);
    }
}
