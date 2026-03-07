using FluentAssertions;
using Lead.Domain.Entities;
using Lead.Domain.Enums;
using Shared.Models.Utilities.Filters;

namespace Lead.Tests.Unit.Filters;

public class FilterServiceTests
{
    private readonly List<Leads> _testData;

    public FilterServiceTests()
    {
        _testData = new List<Leads>
        {
            new Leads
            {
                LeadId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                Status = LeadStatus.Qualified,
                EstimatedValue = 50000,
                Probability = 75
            },
            new Leads
            {
                LeadId = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Status = LeadStatus.New,
                EstimatedValue = 25000,
                Probability = 50
            },
            new Leads
            {
                LeadId = 3,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                Status = LeadStatus.Qualified,
                EstimatedValue = 75000,
                Probability = 90
            }
        };
    }

    [Fact]
    public void ApplyFilters_WithContainsOperation_ShouldFilterCorrectly()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>
        {
            new Filter<object>
            {
                Id = "FirstName",
                Value = "John",
                Operation = Operation.Contains
            }
        };

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(1); // Only "John" Smith (FirstName contains "John")
        result.Should().Contain(l => l.FirstName == "John");
        result[0].LastName.Should().Be("Smith");
    }

    [Fact]
    public void ApplyFilters_WithContainsOperation_OnLastName_ShouldFilterCorrectly()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>
        {
            new Filter<object>
            {
                Id = "LastName",
                Value = "John",
                Operation = Operation.Contains
            }
        };

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(1); // Only Bob "Johnson" (LastName contains "John")
        result.Should().Contain(l => l.LastName == "Johnson");
    }

    [Fact]
    public void ApplyFilters_WithEqualsOperation_ShouldFilterCorrectly()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>
        {
            new Filter<object>
            {
                Id = "Status",
                Value = "Qualified",
                Operation = Operation.Eq
            }
        };

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllSatisfy(l => l.Status.Should().Be(LeadStatus.Qualified));
    }

    [Fact]
    public void ApplyFilters_WithGreaterThanOperation_ShouldFilterCorrectly()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>
        {
            new Filter<object>
            {
                Id = "EstimatedValue",
                Value = "50000",
                Operation = Operation.Gt
            }
        };

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].EstimatedValue.Should().Be(75000);
    }

    [Fact]
    public void ApplyFilters_WithMultipleFilters_ShouldApplyAll()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>
        {
            new Filter<object>
            {
                Id = "Status",
                Value = "Qualified",
                Operation = Operation.Eq
            },
            new Filter<object>
            {
                Id = "Probability",
                Value = "80",
                Operation = Operation.Gte
            }
        };

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].FirstName.Should().Be("Bob");
        result[0].Probability.Should().Be(90);
    }

    [Fact]
    public void ApplyFilters_WithNullFilters_ShouldReturnAllResults()
    {
        // Arrange
        var query = _testData.AsQueryable();

        // Act
        var result = FilterService.ApplyFilters(query, null).ToList();

        // Assert
        result.Should().HaveCount(3);
    }

    [Fact]
    public void ApplyFilters_WithEmptyFilters_ShouldReturnAllResults()
    {
        // Arrange
        var query = _testData.AsQueryable();
        var filters = new List<Filter<object>>();

        // Act
        var result = FilterService.ApplyFilters(query, filters).ToList();

        // Assert
        result.Should().HaveCount(3);
    }
}
