using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class PropertySorting
    {
        public string PropertyName { get; }
        public bool IsAscending { get; }

        public PropertySorting(string propertyName, bool isAscending)
        {
            PropertyName = propertyName;
            IsAscending = isAscending;
        }
    }

    public class Filter
    {
        public int? Skip { get; }
        public int? Take { get; }
        public List<PropertySorting> Sortings { get; } = new List<PropertySorting>();

        public Filter(int? skip = null, int? take = null, IEnumerable<PropertySorting> sortings = null)
        {
            Skip = skip;
            Take = take;

            if (sortings != null)
                Sortings.AddRange(sortings);
        }
    }
}