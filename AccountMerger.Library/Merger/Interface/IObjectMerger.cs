using AccountMerger.Library.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMerger.Library.Merger.Interface
{
    public interface IObjectMerger<TSource, TDestination>
        where TSource : IAccount
        where TDestination : IPerson, new()
    {
        public IEnumerable<TDestination> Merge(IEnumerable<TSource> source);
    }
}
