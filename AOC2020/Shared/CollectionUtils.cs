using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public class CollectionUtils
    {
        public static IEnumerable<string> CollectRecords(string[] lines)
        {
            return lines.Aggregate(
                new List<string>(new[] { "" }),
                (l, s) => {
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        return l.Concat(new[] { "" }).ToList<string>();
                    }
                    else
                    {
                        var l2 = l.GetRange(0, l.Count() - 1);
                        var s2 = string.IsNullOrWhiteSpace(l[^1]) ? s : l[^1] + " " + s;
                        return l2.Concat(new[] { s2 }).ToList<string>();
                    }
                }
            );
        }
    }
}