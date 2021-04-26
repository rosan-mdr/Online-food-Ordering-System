using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.AprioriAlgorithm.Model
{
    public class Item
    {
        public string Name { get; set; }
        public double  Support { get; set; }

        public int CompareTo(Item other)
        {
            return Name.CompareTo(other.Name);
        }

    }
}