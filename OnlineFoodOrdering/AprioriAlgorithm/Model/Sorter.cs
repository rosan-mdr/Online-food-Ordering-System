using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.AprioriAlgorithm.Model
{
    public class Sorter
    {
        public string Sort(string token)
            {
                
                char[] tokenArray = token.ToCharArray();
                Array.Sort(tokenArray);
                return new string(tokenArray);
            }
        
    }
}