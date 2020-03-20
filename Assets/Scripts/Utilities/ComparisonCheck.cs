using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoulsLike
{
    public static class ComparisonCheck
    {
        public static bool IsTrue<T, U>(T value1, ComparisonOperator comparisonOperator, U value2)
            where T : U
            where U : IComparable
        {
            bool retValue = false;
            switch (comparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    retValue = value1.CompareTo(value2) == 0;
                    break;
                case ComparisonOperator.LessOrEqualTo:
                    retValue = value1.CompareTo(value2) <= 0;
                    break;
                case ComparisonOperator.LessThan:
                    retValue = value1.CompareTo(value2) < 0;
                    break;
                case ComparisonOperator.MoreOrEqualTo:
                    retValue = value1.CompareTo(value2) >= 0;
                    break;
                case ComparisonOperator.MoreThan:
                    retValue = value1.CompareTo(value2) > 0;
                    break;
                case ComparisonOperator.NotEqualTo:
                    retValue = value1.CompareTo(value2) != 0;
                    break;
            }
            return retValue;
        }
    }
}
