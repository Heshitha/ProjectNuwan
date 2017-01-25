using System.Web;
using System.Web.Optimization;

namespace NetworkMarketing
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/ionicons.min.css"));

            bundles.Add(new StyleBundle("~/AdminLTE/css").Include(
                      "~/Content/AdminLTE/css/AdminLTE.min.css",
                      "~/Content/AdminLTE/plugins/pace/pace.min.css",
                      "~/Content/AdminLTE/plugins/iCheck/square/blue.css",
                      "~/Content/AdminLTE/plugins/datepicker/datepicker3.css",
                      "~/Content/AdminLTE/css/skins/_all-skins.min.css",
                      "~/Content/AdminLTE/css/skins/skin-blue.min.css"));

            bundles.Add(new StyleBundle("~/Content/SiteCSS").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/PrintCSS").Include(
                      "~/Content/PrintStyle.css"));

            bundles.Add(new ScriptBundle("~/bundles/AdminLTE").Include(
                      "~/Content/AdminLTE/plugins/fastclick/fastclick.min.js",
                      "~/Content/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/Content/AdminLTE/plugins/pace/pace.min.js",
                      "~/Content/AdminLTE/plugins/iCheck/icheck.min.js",
                      "~/Content/AdminLTE/plugins/datepicker/bootstrap-datepicker.js",
                      "~/Content/AdminLTE/js/app.min.js",
                      "~/Scripts/UtilityMethods.js"));
        }
    }
}
