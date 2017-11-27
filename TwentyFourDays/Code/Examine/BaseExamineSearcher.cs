using Examine;
using Examine.Providers;
using Examine.SearchCriteria;
using System;

namespace TwentyFourDays.Code.Examine
{
    public abstract class BaseExamineSearcher
    {
        internal BaseSearchProvider Searcher { get; }
        internal BaseIndexProvider Indexer { get; }

        public BaseExamineSearcher(string name)
        {
            Searcher = ExamineManager.Instance.SearchProviderCollection[name + "Searcher"];
            Indexer = ExamineManager.Instance.IndexProviderCollection[name + "Indexer"];
        }
    }
}