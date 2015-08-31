using System.Web;
using System.Web.Optimization;

namespace BulkSMSWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Template/jquery-1.11.1.min.js"/*,
                        "~/Scripts/Custom/jquery.nicescroll.js",
                        "~/Scripts/Custom/jquery.scrollTo.min.js",
                        "~/Scripts/Custom/scripts.js"*/));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                       "~/Scripts/jquery.dataTables.min.js",
                       "~/Scripts/Template/jquery.knob.js",
                       "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/MasterPage/css").Include(
                "~/Content/jquery.dataTables.min.css",
                "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Template/font-awesome.min.css",
                      "~/Content/Template/fontsgoogleapis.css",
                      "~/Content/Template/theme.css",
                      "~/Content/Template/theme.css"
                      
                      //"~/Content/site.css",
                      //"~/Content/bootstrap-theme.css",
                      //"~/Content/Custom/style-responsive.css",
                      //"~/Content/Custom/style.css",
                      /*"~/Content/jquery.dataTables.min.css"*/));

      
        }
    }
}
