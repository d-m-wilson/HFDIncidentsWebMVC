using HFDActiveIncidents.Domain.Models;
using System.Linq;

namespace HFDActiveIncidents.Domain
{
    public interface IIncidentDataSource
    {
        IQueryable<ArchivedIncident> ArchivedIncidents { get; }
        IQueryable<IncidentType> IncidentTypes { get; }
    }
}
