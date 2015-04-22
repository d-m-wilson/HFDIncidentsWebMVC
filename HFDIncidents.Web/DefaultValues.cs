using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HFDIncidents.Web
{
    public static class DefaultValues
    {
        public const int MinPageSize = 10;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;

        public const double Latitude = 29.7605;
        public const double Longitude = -95.3666;

        public static readonly IReadOnlyCollection<int> PageSizes;

        static DefaultValues()
        {
            PageSizes = new List<int>(new int[] { 10, 20, 30, 50, 75, 100 }).AsReadOnly();
        }

        public static SelectList ItemsPerPageList
        {
            get
            {
                return new SelectList(PageSizes, DefaultPageSize);
            }
        }
    }
}