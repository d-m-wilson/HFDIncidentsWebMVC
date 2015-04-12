using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HFDActiveIncidents.Domain.Models
{
    public partial class IncidentType
    {
        public long Id { get; set; }
        public long AgencyId { get; set; }

        [Display(Name="Incident Type")]
        public string Name { get; set; }

        public virtual ICollection<ActiveIncident> ActiveIncidents { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual ICollection<ArchivedIncident> ArchivedIncidents { get; set; }
    }
}
