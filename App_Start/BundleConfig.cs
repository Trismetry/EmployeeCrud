using System.Web;
using System.Web.Optimization;

namespace EmployeeCrud
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/modernizr-*",
                        "~/Scripts/jquery.easyui.min.js"
                        ));

    
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     // "~/Content/bootstrap.css",
                     // "~/Content/site.css",
                      "~/Content/EasyUI/themes/black/easyui.css"));


        }
    }
}
