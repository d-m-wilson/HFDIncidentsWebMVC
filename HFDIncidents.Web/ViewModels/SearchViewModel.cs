using HFDIncidents.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HFDIncidents.Web.ViewModels
{
    public class SearchViewModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<SelectListItem> PageSizes { get; private set; }

        public PagedList.IPagedList<ArchivedIncident> Incidents { get; set; }

        public IEnumerable<SelectListItem> IncidentTypes { get; set; }

        public IEnumerable<long> types { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
        public DateTime ToDate { get; set; }

        public SearchViewModel()
        {
            IncidentTypes = new List<SelectListItem>();
            types = new List<long>();

            PageSizes = DefaultValues.PageSizes.Select((ps) => new SelectListItem { Text = ps.ToString(), Value = ps.ToString(), Selected = ps == PageSize });
        }

    }
}
