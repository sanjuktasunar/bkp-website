using System.Web;
using System.Web.Optimization;

namespace web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/web/backend/jquery-ui").Include(
                       "~/Assets/Backend/vendor/jquery/jquery.min.js",
                        "~/Assets/Backend/js/nepali.datepicker.v3.6.min.js",
                       "~/Assets/Backend/js/sweetalert.min.js",
                       "~/Assets/Backend/js/select2.min.js",
                       "~/Assets/Backend/custom/showMessage.js",
                       "~/Assets/Backend/custom/load_ajax_dropdown.js",
                      "~/Assets/Backend/custom/custom.js",
                       "~/Assets/Backend/custom/layout_js_functions.js"
                       ));

            bundles.Add(new ScriptBundle("~/web/backend/datatables").Include(
                        "~/Assets/Backend/vendor/datatables/jquery.dataTables.min.js",
                        "~/Assets/Backend/vendor/datatables/dataTables.bootstrap4.min.js",
                        "~/Assets/Backend/js/datatable.js"
                        ));
        }
    }
}
