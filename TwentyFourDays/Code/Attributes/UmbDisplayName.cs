using System.ComponentModel;
using Umbraco.Core.Models;
using Umbraco.Web;
using TwentyFourDays.Code.Examine;

namespace TwentyFourDays.Code.Attributes.Validation
{
    public class UmbDisplayName : DisplayNameAttribute
    {
        private string DoctypeAlias { get; }
        private string PropertyAlias { get; }        
        private UmbracoNodeSearcher Searcher { get; } = new UmbracoNodeSearcher();        

        public UmbDisplayName(string doctypeAlias, string propertyAlias)
        {                        
            DoctypeAlias = doctypeAlias;
            PropertyAlias = propertyAlias;
        }

        // When the current Umbraco page should be used, specify only the property alias
        public UmbDisplayName(string propertyAlias) : this(null, propertyAlias) { }

        // Obtain display name from Umbraco 
        public override string DisplayName  
            // DisplayName is not allowed to be null or empty so fallback to " " in that case
            => Content?.GetPropertyValue<string>(PropertyAlias)?.WhenNullOrEmpty(" ");

        // Get IPublishedContent based on Document Type alias
        // When alias is NULL -> use current Umbraco page
        protected IPublishedContent Content => DoctypeAlias != null
            ? Searcher.GetOne<IPublishedContent>(DoctypeAlias)
            : UmbracoContext.Current?.PublishedContentRequest?.PublishedContent;
    }
}