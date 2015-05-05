// HFD Incidents
// Copyright © 2015 David M. Wilson
// https://twitter.com/dmwilson_dev
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using HFDIncidents.Domain;
using HFDIncidents.Domain.Models;
using HFDIncidents.Web.ViewModels;
using PagedList.EntityFramework;

namespace HFDIncidents.Web.Controllers
{
    public class HomeController : Controller
    {
        private IIncidentDataSource db;

        public HomeController(IIncidentDataSource db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Home/Search
        public async Task<ActionResult> Search(int? itemsPerPage, int? page, string fromDate, string toDate, List<long> types)
        {
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }

            if (!itemsPerPage.HasValue || itemsPerPage.Value < 1)
            {
                itemsPerPage = DefaultValues.DefaultPageSize;
            }
            else if (itemsPerPage.Value > DefaultValues.MaxPageSize)
            {
                itemsPerPage = DefaultValues.MaxPageSize;
            }

            DateTime from;
            DateTime to;

            if (String.IsNullOrWhiteSpace(fromDate) || !DateTime.TryParse(fromDate, out from))
            {
                from = DateTime.Today.AddDays(-30);
            }
            else
            {
                from = from.Date;
            }

            if (String.IsNullOrWhiteSpace(toDate) || !DateTime.TryParse(toDate, out to))
            {
                to = DateTime.Today.AddMinutes(1439);
            }
            else
            {
                to = to.Date.AddMinutes(1439);
            }

            var incidentTypes = await db.IncidentTypes
                .OrderBy(it => it.Name)
                .ToListAsync();

            IEnumerable<SelectListItem> incidentTypesListItems;

            if (types == null)
            {
                incidentTypesListItems = incidentTypes.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString(), Selected = it.Id > 1 });
                types = incidentTypes.Where(it => it.Id > 1).Select(it => it.Id).ToList();
            }
            else
            {
                incidentTypesListItems = incidentTypes.Select(it => new SelectListItem { Text = it.Name, Value = it.Id.ToString(), Selected = types.Contains(it.Id) });
            }

            var incidentsQuery = db.ArchivedIncidents
                .Where(ai => ai.CallTimeOpened >= from && ai.CallTimeOpened <= to && types.Contains(ai.IncidentTypeId.Value));

            var pagedList = await incidentsQuery.OrderBy(i => i.CallTimeOpened).ToPagedListAsync(page.Value, itemsPerPage.Value);

            var vm = new SearchViewModel
            {
                IncidentTypes = incidentTypesListItems,
                FromDate = from,
                ToDate = to,
                types = types,
                Page = page.Value,
                PageSize = itemsPerPage.Value,
                Incidents = pagedList,
            };

            return View(vm);
        }

        // GET: Home/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArchivedIncident archivedIncident = await db.ArchivedIncidents
                .Where(ai => ai.Id == id)
                .FirstOrDefaultAsync();

            if (archivedIncident == null)
            {
                return HttpNotFound();
            }

            return View(archivedIncident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db is IDisposable)
                {
                    ((IDisposable)db).Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}