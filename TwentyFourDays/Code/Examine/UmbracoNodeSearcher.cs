using System;
using System.Linq;
using System.Web;
using Umbraco.Web.PublishedContentModels;
using Umbraco.Web;
using Examine.SearchCriteria;
using Umbraco.Core.Models;
using System.Collections.Generic;
using Examine;
using Examine.LuceneEngine;

namespace TwentyFourDays.Code.Examine
{
    public class UmbracoNodeSearcher : BaseExamineSearcher
    {
        private Lazy<UmbracoHelper> _lazyUmbracoHelper;
        private UmbracoHelper UmbracoHelper => _lazyUmbracoHelper.Value;

        public UmbracoNodeSearcher(UmbracoHelper umbracoHelper = null) : base("UmbracoNode")
        {
            _lazyUmbracoHelper = new Lazy<UmbracoHelper>(() => 
                umbracoHelper ?? new UmbracoHelper(UmbracoContext.Current));
        }

        public IEnumerable<T> GetAll<T>(string doctypeAlias, int? maxResults = null, bool inCurrentSite = true) 
            where T : class, IPublishedContent
        {
            IBooleanOperation operation = Searcher.CreateSearchCriteria().NodeTypeAlias(doctypeAlias);
           
            if(inCurrentSite)
            {
                int? homepageId = UmbracoContext.Current?.PublishedContentRequest?.PublishedContent?.AncestorOrSelf<Home>()?.Id;
                if (homepageId != null)
                {
                    operation.And().Field("@homepageId", homepageId.ToString());
                }
            }

            ISearchCriteria criteria = operation.Compile();
            ISearchResults searchResults = maxResults.HasValue ? Searcher.Search(criteria, maxResults.Value) : Searcher.Search(criteria);
            return searchResults.Select(sr => UmbracoHelper.TypedContent(sr.Id) as T);            
        }

        public T GetOne<T>(string doctypeAlias) where T : class, IPublishedContent
        {
            string cacheKey = $"{nameof(UmbracoNodeSearcher)}_{nameof(GetOne)}_{doctypeAlias}";
            return HttpContext.Current?.GetItem(cacheKey, () => GetAll<T>(doctypeAlias, maxResults: 1)?.FirstOrDefault());
        }   
    }
}