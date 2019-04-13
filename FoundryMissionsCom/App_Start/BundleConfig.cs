using System.Web;
using System.Web.Optimization;

namespace FoundryMissionsCom
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



            //my bundles

            //main css 
            bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/bootstrap.css",
          "~/Content/site.css"));

            //view data css
            bundles.Add(new StyleBundle("~/Content/viewdata").Include(
          "~/Content/bootstrap.css",
          "~/Content/viewdata.css",
          "~/Content/verticaltabs.css"));

            //view data js
            bundles.Add(new ScriptBundle("~/bundles/viewdata").Include(          
          "~/Scripts/ViewData/objective.js",
          "~/Scripts/ViewData/project.js",
          "~/Scripts/ViewData/map.js",
          "~/Scripts/ViewData/costume.js",
          "~/Scripts/ViewData/component.js",
          "~/Scripts/ViewData/dialog.js",
          "~/Scripts/viewdata.js"));

            bundles.Add(new ScriptBundle("~/bundles/editmission").Include(
                      "~/Scripts/mission.js"));

            bundles.Add(new ScriptBundle("~/bundles/submitmission").Include(
                      "~/Scripts/submit.js"));

            bundles.Add(new StyleBundle("~/Content/lightbox").Include(
                      "~/Content/blueimp-gallery.min.css",
                      "~/Content/bootstrap-image-gallery.min.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/lightbox").Include(
                      "~/Scripts/jquery.blueimp-gallery.min.js",
                      "~/Scripts/bootstrap-image-gallery.min.js"));

            bundles.Add(new StyleBundle("~/Content/missiondetails").Include(
                "~/Content/jquery.jscrollpane.css",
                "~/Content/scrollpane-styles.css"));
            bundles.Add(new ScriptBundle("~/bundles/missiondetails").Include(
                "~/Scripts/jquery.jscrollpane.js",
                "~/Scripts/mission-details.js"));
        }
    }
}
