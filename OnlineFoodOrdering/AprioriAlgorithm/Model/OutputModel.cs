using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.AprioriAlgorithm.Model
{
    public class Output
    {
        #region Public Properties

        public IList<Rule> StrongRules { get; set; }

        //public IList<string> MaximalItemSets { get; set; }

        //public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }

        public ItemsDictionary FrequentItems { get; set; }

        #endregion
    }

    public class ItemsDictionary : KeyedCollection<string, Item>
    {
        protected override string GetKeyForItem(Item item)
        {
            return item.Name;
        }

        internal void ConcatItems(IList<Item> frequentItems)
        {
            foreach (var item in frequentItems)
            {
                this.Add(item);
            }
        }
    }
}