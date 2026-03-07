using FluentAssertions;
using Lead.Application.DTOs;
using Lead.Application.DTOs.Request;
using Lead.Tests.Fixtures;
using Shared.Models;
using Shared.Models.Helpers;
using Shared.Models.Utilities.Filters;
using System.Net;
using System.Net.Http.Json;

namespace Lead.Tests.Integration.Api;

public class LeadControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public LeadControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var createDto = new CreateLeadDto
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith@test.com",
            Phone = "555-1234",
            Company = "Test Corp",
            EstimatedValue = 50000,
            Probability = 75
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/leads", createDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        result.Should().NotBeNull();
        result!.status.Should().Be("success");
        result.message.Should().Be("Lead created successfully");
    }

    [Fact]
    public async Task Search_WithFilters_ShouldReturnFilteredResults()
    {
        // Arrange
        var searchRequest = new LeadSearchRequest<object>
        {
            PageNumber = 1,
            PageSize = 10,
            Filters = new List<Filter<object>>
            {
                new Filter<object>
                {
                    Id = "FirstName",
                    Value = "John",
                    Operation = Operation.Contains,
                    LogicalOperation = "OR"
                }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/leads/search", searchRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        result.Should().NotBeNull();
        result!.status.Should().Be("success");
    }

    [Fact]
    public async Task GetAll_ShouldReturnPaginatedResults()
    {
        // Act
        var response = await _client.GetAsync("/api/leads?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<ResponseModel>();
        result.Should().NotBeNull();
        result!.status.Should().Be("success");
    }

    [Fact]
    public async Task Create_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var createDto = new CreateLeadDto
        {
            FirstName = "",
            LastName = "",
            Email = "invalid-email",
            Phone = ""
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/leads", createDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
