using System.Web.Optimization;
using System.Web;
namespace InvoicingWebCore.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/script")
                .Include("~/Scripts/index.ts"));
        }
    }
}
