using Umbraco.Core;
using Examine;
using Umbraco.Web.PublishedContentModels;
using UmbracoExamine;
using Umbraco.Web;

namespace TwentyFourDays.Code.Examine
{
    public class ExamineEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            InitUmbracoNodeSearcherEvents();            
        }

        private void InitUmbracoNodeSearcherEvents()
        {
            UmbracoNodeSearcher searcher = new UmbracoNodeSearcher();
            if (searcher != null)
            {
                UmbracoContentIndexer indexer = (UmbracoContentIndexer)searcher.Indexer;
                if (indexer != null)
                {
                    UmbracoHelper uh = new UmbracoHelper(UmbracoContext.Current);

                    indexer.GatheringNodeData += (sender, e) => UmbracoNodeIndexer_GatheringNodeData(sender, e, uh);                    
                }
            }
        }

        private void UmbracoNodeIndexer_GatheringNodeData(object sender, IndexingNodeDataEventArgs e, UmbracoHelper uh)
        {
            // Store ID of closest Home page.
            e.Fields["@homepageId"] = uh.TypedContent(e.NodeId)?.AncestorOrSelf<Home>()?.Id.ToString();
        }
    }
}