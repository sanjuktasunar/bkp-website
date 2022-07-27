using System.Web;
using System.Web.Optimization;

namespace web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("~/web/back-end/css").Include(
            //      "~/Assets/Backend/vendor/fontawesome-free/css/all.min.css",
            //      "~/Assets/Backend/css/sb-admin-2.css",
            //      "~/assets/Backend/vendor/datatables/dataTables.bootstrap4.min.css",
            //      "~/assets/Backend/css/custom/custom.css",
            //      "~/Assets/Backend/css/custom/plugin/sweetalert.min.css",
            //      "~/assets/Backend/css/custom/plugin/nepali.datepicker.v3.6.min.css",
            //      "~/assets/Backend/css/custom/plugin/select2/select2.min.css",
            //      "~/Assets/Backend/css/custom/plugin/form-validation.css",
            //      "~/Assets/Backend/js/custom/Dropdown.js"
            //      ));

            //bundles.Add(new ScriptBundle("~/web/back-end/layout/js").Include(
            //        "~/Assets/Backend/vendor/jquery/jquery.min.js",
            //        "~/Assets/Backend/vendor/bootstrap/js/bootstrap.bundle.min.js",
            //        "~/Assets/Backend/vendor/jquery-easing/jquery.easing.min.js",
            //        "~/Assets/Backend/js/custom/plugin/sweetalert.min.js",
            //        "~/assets/Backend/js/sb-admin-2.min.js",
            //        "~/Assets/Backend/js/custom/LoadMessage.js"
            //        ));

            //bundles.Add(new ScriptBundle("~/web/back-end/js").Include(
            //        "~/Assets/Backend/vendor/jquery/jquery.min.js",
            //        "~/Assets/Backend/vendor/bootstrap/js/bootstrap.bundle.min.js",
            //        "~/Assets/Backend/vendor/jquery-easing/jquery.easing.min.js",
            //        "~/Assets/Backend/js/custom/plugin/sweetalert.min.js",
            //        "~/assets/Backend/js/sb-admin-2.min.js",
            //        "~/Assets/Backend/js/custom/LoadMessage.js",
            //        "~/assets/Backend/vendor/datatables/jquery.dataTables.min.js",
            //        "~/assets/Backend/vendor/datatables/dataTables.bootstrap4.min.js",
            //        "~/Assets/Backend/js/custom/LoadDatatable.js",
            //        "~/Assets/Backend/js/custom/plugin/nepali.datepicker.v3.6.min.js",
            //        "~/Assets/Backend/js/custom/Dropdown.js",
            //        "~/Assets/Backend/js/custom/custom.js"
            //        ));

            bundles.Add(new ScriptBundle("~/web/backend/jquery-ui").Include(
                       //"~/Assets/Backend/vendor/jquery/jquery.min.js",
                       //"~/Assets/Backend/js/sweetalert.min.js",
                       //"~/Assets/Backend/custom/custom.js",
                       //"~/Assets/Backend/custom/load.dropdown.js",
                       //"~/Assets/Backend/js/nepali.datepicker.v3.6.min.js",
                       //"~/Assets/Backend/js/select2.min.js",
                       //"~/Assets/Backend/js/show.message.js"

                       "~/Assets/Backend/vendor/jquery/jquery.min.js",
                       "~/Assets/Backend/js/sweetalert.min.js",
                       "~/Assets/Backend/custom/Custom.js",
                       "~/Assets/Backend/js_code/ajax_load.js",
                       "~/Assets/Backend/custom/nepali-datepicker/nepali.datepicker.v3.6.min.js",
                       "~/Assets/Backend/custom/nepali-datepicker/load-picker.js",
                       "~/Assets/Backend/js/select2.min.js",
                       "~/Assets/Backend/custom/ShowMessage.js"

                       ));

            bundles.Add(new ScriptBundle("~/web/backend/datatables").Include(
                        "~/Assets/Backend/vendor/datatables/jquery.dataTables.min.js",
                        "~/Assets/Backend/vendor/datatables/dataTables.bootstrap4.min.js",
                        "~/Assets/Backend/js/datatable.js"
                        ));

            //bundles.Add(new ScriptBundle("~/web/custom/js").Include(
            //       //"~/Assets/Backend/js/custom/plugin/sweetalert.min.js",
            //       //"~/Assets/Backend/js/custom/LoadMessage.js",
            //       //"~/Assets/Backend/js/custom/plugin/nepali.datepicker.v3.6.min.js",
            //       "~/Assets/Backend/js/custom/Dropdown.js",
            //       "~/Assets/Backend/js/custom/NumberTyping.js",
            //       "~/Assets/Backend/js/custom/custom.js"
            //       ));
        }
    }
}
