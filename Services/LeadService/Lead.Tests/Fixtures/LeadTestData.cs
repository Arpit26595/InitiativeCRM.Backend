using Bogus;
using Lead.Domain.Entities;
using Lead.Domain.Enums;

namespace Lead.Tests.Fixtures;

public static class LeadTestData
{
    public static Faker<Leads> LeadFaker => new Faker<Leads>()
        .RuleFor(l => l.FirstName, f => f.Name.FirstName())
        .RuleFor(l => l.LastName, f => f.Name.LastName())
        .RuleFor(l => l.Email, f => f.Internet.Email())
        .RuleFor(l => l.Phone, f => f.Phone.PhoneNumber())
        .RuleFor(l => l.Company, f => f.Company.CompanyName())
        .RuleFor(l => l.Address, f => f.Address.StreetAddress())
        .RuleFor(l => l.City, f => f.Address.City())
        .RuleFor(l => l.State, f => f.Address.StateAbbr())
        .RuleFor(l => l.ZipCode, f => f.Address.ZipCode())
        .RuleFor(l => l.Description, f => f.Lorem.Sentence())
        .RuleFor(l => l.Details, f => f.Lorem.Paragraph())
        .RuleFor(l => l.EstimatedValue, f => f.Random.Decimal(10000, 500000))
        .RuleFor(l => l.Probability, f => f.Random.Int(0, 100))
        .RuleFor(l => l.Status, f => f.PickRandom<LeadStatus>())
        .RuleFor(l => l.LeadSource, f => f.PickRandom<LeadSource>())
        .RuleFor(l => l.LeadType, f => f.PickRandom<LeadType>())
        .RuleFor(l => l.DateOpened, f => f.Date.Past())
        .RuleFor(l => l.ExpectedCloseDate, f => f.Date.Future())
        .RuleFor(l => l.AssignedTo, f => f.Random.Int(1, 10));

    public static List<Leads> GenerateLeads(int count = 10)
    {
        return LeadFaker.Generate(count);
    }

    public static Leads GenerateSingleLead()
    {
        return LeadFaker.Generate();
    }
}
