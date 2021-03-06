﻿using System.Web;
using System.Web.Optimization;

namespace WineOutlet
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
                      "~/Content/site.css",
                        "~/Content/animate.css",
                        "~/Content/css.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/jquery-ui.min.css",
                        "~/Content/jquery.simpleLens.css",
                        "~/Content/master.css",
                        "~/Content/meanmenu.min.css",
                        "~/Content/nivo-slider.css",
                        "~/Content/owl.carousel.css",
                        "~/Content/responsive.css",
                        "~/Content/style.css",
                        "~/Content/animate.css"));
        }
    }
}
