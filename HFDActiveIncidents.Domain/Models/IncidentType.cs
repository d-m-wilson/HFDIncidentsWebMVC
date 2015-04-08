using System;
using System.Collections.Generic;

namespace HFDActiveIncidents.Domain.Models
{
    public partial class IncidentType
    {
        public IncidentType()
        {
            this.ActiveIncidents = new List<ActiveIncident>();
            this.ArchivedIncidents = new List<ArchivedIncident>();
        }

        public long Id { get; set; }
        public long AgencyId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ActiveIncident> ActiveIncidents { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual ICollection<ArchivedIncident> ArchivedIncidents { get; set; }
    }
}
