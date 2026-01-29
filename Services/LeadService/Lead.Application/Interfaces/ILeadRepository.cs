using System;
using System.Collections.Generic;
using System.Text;
using Lead.Domain.Entities;


namespace Lead.Application.Interfaces
{
    public interface ILeadRepository
    {
        Task AddAsync(Leads lead);
        Task<List<Leads>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
