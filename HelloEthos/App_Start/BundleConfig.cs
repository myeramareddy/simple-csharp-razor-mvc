using System.Web;
using System.Web.Optimization;

namespace HelloEthos
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrapcss").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/bootstrap-theme.min.css"));

            bundles.Add(new StyleBundle("~/Content/homepagecss").Include(
                        "~/Content/Homepage.css", 
                        "~/Content/Cards.css"));

            bundles.Add(new StyleBundle("~/Content/actioncss").Include(
                        "~/Content/Cards.css"));

            bundles.Add(new StyleBundle("~/Content/myfonts").Include(
                        "~/Content/fonts/AnticSlab-Regular.ttf"));
        }
    }
}