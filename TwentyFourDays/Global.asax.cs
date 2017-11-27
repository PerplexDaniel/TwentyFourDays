using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using Umbraco.Web;

namespace TwentyFourDays
{
    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarting(object sender, EventArgs e)
        {
            base.OnApplicationStarting(sender, e);
            RegisterBundles(BundleTable.Bundles);
        }        

        private static void RegisterBundles(BundleCollection bundles)
        {
            // css
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                         "~/Styles/bootstrap.css",
                         "~/Styles/style.css"));

            // js/head
            bundles.Add(new ScriptBundle("~/bundles/js/head").Include(
                         "~/Scripts/modernizr-{version}.js"));
            

            // js/body
            bundles.Add(new ScriptBundle("~/bundles/js/body").Include(
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery.validate.js",
                         "~/Scripts/jquery.validate.unobtrusive.js",
                         "~/Scripts/validators.js",
                         "~/Scripts/bootstrap.js"));
        }
    }
}