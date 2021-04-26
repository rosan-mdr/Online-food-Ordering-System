using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.AprioriAlgorithm.Model
{
    public class Rule
    {
        string combination, remaining;
        double confidence;

        public Rule(string combination,string remaining,double confidence) {
            this.combination = combination;
            this.remaining = remaining;
            this.confidence = confidence;
        }

        public string X { get { return combination; }  }
        public string Y { get { return remaining; }  }
        public double Confidence { get { return confidence; } }

        //public int CompareTo(Rule other)
        //{
        //    return X.CompareTo(other.X);
        //}

        //public override bool Equals(object obj)
        //{
        //    var other = obj as Rule;
        //    if (other == null)
        //    {
        //        return false;
        //    }

        //    return other.X == this.X && other.Y == this.Y ||
        //        other.X == this.Y && other.Y == this.X;
        //}
    }
}