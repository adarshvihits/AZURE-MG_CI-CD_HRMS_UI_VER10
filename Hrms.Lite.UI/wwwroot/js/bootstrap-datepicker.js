/******/ (() => { // webpackBootstrap
    /*!************************************************************************!*\
      !*** ../demo1/src/js/pages/crud/forms/widgets/bootstrap-datepicker.js ***!
      \************************************************************************/
    // Class definition

    var KTBootstrapDatepicker = function () {

        var arrows;
        if (KTUtil.isRTL()) {
            arrows = {
                leftArrow: '<i class="la la-angle-right"></i>',
                rightArrow: '<i class="la la-angle-left"></i>'
            }
        } else {
            arrows = {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            }
        }

        // Private functions
        var demos = function () {
            // minimum setup
            $('#kt_datepicker_1, #kt_datepicker_1_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });

            // minimum setup for modal demo
            $('#kt_datepicker_1_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });

            // input group layout 
            $('.kt_datepicker_2, .kt_datepicker_2_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows,


                format: "dd-M-yyyy",
                orientation: "auto",
                startDate: date,
                clearBtn: true,
                autoclose: true
            });

            // input group layout for modal demo
            $('.kt_datepicker_2_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });

            // enable clear button 
            $('#kt_datepicker_3, #kt_datepicker_3_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                templates: arrows
            });

            // enable clear button for modal demo
            $('#kt_datepicker_3_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayBtn: "linked",
                clearBtn: true,
                todayHighlight: true,
                templates: arrows
            });

            // orientation 
            $('#kt_datepicker_4_1').datepicker({
                rtl: KTUtil.isRTL(),
                orientation: "top left",
                todayHighlight: true,
                templates: arrows
            });
            $('#kt_datepicker_4_1_1').datepicker({
                rtl: KTUtil.isRTL(),
                orientation: "top left",
                todayHighlight: true,
                templates: arrows
            });

            $('#kt_datepicker_4_2').datepicker({
                rtl: KTUtil.isRTL(),
                orientation: "top right",
                todayHighlight: true,
                templates: arrows
            });

            $('#kt_datepicker_4_3').datepicker({
                rtl: KTUtil.isRTL(),
                orientation: "bottom left",
                todayHighlight: true,
                templates: arrows
            });

            $('#kt_datepicker_4_4').datepicker({
                rtl: KTUtil.isRTL(),
                orientation: "bottom right",
                todayHighlight: true,
                templates: arrows
            });

            // range picker
            $('#kt_datepicker_5').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                templates: arrows
            });

            // inline picker
            $('#kt_datepicker_6').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                templates: arrows
            });

            // input group layout 
            $('.kt_datepicker_8, .kt_datepicker_8_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows,
                endDate: '+30d',
                autoclose: true,



                format: "dd-M-yyyy",
                orientation: "auto",
                startDate: date,
                clearBtn: true
            });

            // input group layout for modal demo
            $('.kt_datepicker_8_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });


            // input group layout 
            $('.kt_datepicker_10, .kt_datepicker_10_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows,
                endDate: '+30d',
                autoclose: true,



                format: "dd-M-yyyy",
                orientation: "auto",
                startDate: date,
                clearBtn: true
            });

            // input group layout for modal demo
            $('.kt_datepicker_10_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });

            // inline picker
            $('.monthYear').datepicker({
                format: "M-yyyy",
                startView: "months",
                minViewMode: "months",
                endDate: '+0d'
            });


            var date = new Date();
            date.setDate(date.getDate());
            // input group layout 
            $('.kt_datepicker_9, .kt_datepicker_9_validate').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows,
                format: "dd-M-yyyy",
                orientation: "auto",
                startDate: date,
                clearBtn: true,
                autoclose: true

            });

            // input group layout for modal demo
            $('.kt_datepicker_9_modal').datepicker({
                rtl: KTUtil.isRTL(),
                todayHighlight: true,
                orientation: "bottom left",
                templates: arrows
            });



          

        }

        return {
            // public functions
            init: function () {
                demos();
            }
        };
    }();

    jQuery(document).ready(function () {
        KTBootstrapDatepicker.init();
    });
    /******/
})()
    ;
//# sourceMappingURL=bootstrap-datepicker.js.map













