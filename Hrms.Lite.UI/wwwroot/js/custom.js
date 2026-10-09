

//$(document).ready(function () {
//	var selectDivHeight = $(".tab-only .tab-content.active").height();
//	$('.ulList').height(selectDivHeight + 40);

//	$('.tab-outer .ulList li.clickOnTab').click(function () {
//		var myTabList = $(this).closest('.tab-outer');
//        myTabList.find('.ulList li.clickOnTab').removeClass('active');
//		$(this).addClass('active');


//		var getLiClass = $(this).attr('class').split(" ");
//		var splitFirstClass = getLiClass[0];
//		myTabList.find('.tab-content.active').slideUp(300, function () {
//			$(this).removeClass('active');
//			$('#' + getLiClass).slideDown(300, function () {
//				$(this).addClass('active');
//				var selectDivHeight = $(".tab-only .tab-content.active").height();
//				$('.ulList').height(selectDivHeight + 40);
//				// alert(selectDivHeight);
//			});

//		});

//	});


//	var avatar4 = new KTImageInput('kt_image_4');

//	avatar4.on('cancel', function (imageInput) {
//		//swal.fire({
//		//	title: 'Image Successfully Canceled',
//		//	type: 'success',
//		//	buttonsStyling: false,
//		//	confirmButtonText: 'OK',
//		//	confirmButtonClass: 'btn btn-primary font-weight-bold'
//		//});
//	});

//	avatar4.on('change', function (imageInput) {
//		//swal.fire({
//		//	title: 'Image Successfully Changed',
//		//	type: 'success',
//		//	buttonsStyling: false,
//		//	confirmButtonText: 'OK',
//		//	confirmButtonClass: 'btn btn-primary font-weight-bold'
//		//});
//	});

//	avatar4.on('remove', function (imageInput) {
//		//swal.fire({
//		//	title: 'Image Successfully Removed',
//		//	type: 'error',
//		//	buttonsStyling: false,
//		//	confirmButtonText: 'OK',
//		//	confirmButtonClass: 'btn btn-primary font-weight-bold'
//		//});
//	});



//	$(".clickOnTab").click(function () {
//		$('html, body').animate({
//			scrollTop: $(".TabMoving").offset().top
//		}, 2000);
//	});


//	//$('.slider-btn').click(function() {
//	//	$('body').append('<div class="form-overlay"></div>');
//	//	$("div").removeClass('slider-forms-open');    
//	//   // get class name this way
//	//   var elementClassName = $(this).attr('Id');
//	//   // alert(elementClassName)
//	//   //find the divs with this class name
//	//   $('div#'+elementClassName).toggleClass('slider-forms-open');                                         
//	//  });
//	  $(".slider-form-close").click(function(){
//          $(".slider-forms").removeClass('slider-forms-open');
//          $(".slider-forms").removeClass('new-slider-expand');
//	   $(".form-overlay").remove();
//		  });

//		  var multipleCancelButton = new Choices('.choices-multiple-remove-button', {
//			removeItemButton: true,
//			// maxItemCount: 5,
//			// searchResultLimit: 5,
//			// renderChoiceLimit: 5
		
//		});
//		var multipleCancelButton = new Choices('.choices-multiple-remove-button-two', {
//			removeItemButton: true,
//			// maxItemCount: 5,
//			// searchResultLimit: 5,
//			// renderChoiceLimit: 5
		
//		});
    
//});



//$(function() {
//  $('.filter-combobox select').change(function() {
//      var val = $(this).val();

//      if (val) {
//          $('.selectFieldValuShow div:not(#' + val + ')').hide();
//          $('#' + val).show();
//      } else {
//          $('.selectFieldValuShow div').hide();
//      }
//  });
//    $('.multiselectDropdownAll').multiselect('selectAll', false);
//    $('.multiselectDropdownAll').multiselect('updateButtonText');
//});

//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()

//  })
//$('[data-toggle="tooltip"]').tooltip({
//    trigger: 'hover',
//    container: '.wrapper'
//  });
//$(".tooltip-link").on("mouseover", function () {

//	$(".tooltip-hover").fadeIn(300, function () {

//	});

//})
//$(".tooltip-link").on("mouseout", function () {

//	$(".tooltip-hover").fadeOut(300, function () {

//	});

//})
//$(".btn ").click(function () {
//    $(".tooltip-hover").fadeOut(300, function () {
//    });

//});


//$(".click-button").click(function () {

//	$(".fields-box").slideToggle("slow")

//});

//$('.search-click').click(function(){
//  $('.employee-pProfile-search-result').show(300);
//});
//$('.search-close').click(function(){
//	$('.employee-pProfile-search-result').hide(300);
//});

//$(".search-slider-inner").click(function(){
//$(".form-control").toggleClass('field-width-big')
//});




//$(".search-slider-new").click(function () {
//    $(".form-controls").toggleClass('field-width-big')
//});



//$("#sliderMenuInclude").load("menu.html"); 


//$('#mySelect').selectpicker();

//// CHECK BOX VIEW HIDE

//  $("#coupon_question").on("click",function() {
//    $(".showCheckBox").toggle(this.checked);
//  });

//// CHECK BOX VIEW HIDE
//// SELECT FIELD STYLE

//$('.multiselectDropdownField ').multiselect({
//    includeSelectAllOption: true,
//  /*  enableFiltering: true,*/
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,
//    includeSelectAllOption: false,
   

 
//});





////$('.ResignResponsible').multiselect({
////    includeSelectAllOption: true,
////    /*  enableFiltering: true,*/
////    enableCaseInsensitiveFiltering: true,
////    includeFilterClearBtn: false,
////    includeSelectAllOption: false,
////    numberDisplayed: -1,


////});
//$('.multiselectDropdownFields').multiselect({
//    includeSelectAllOption: true,
//    nonSelectedText: 'All',
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,


   
//});

//$('.multiselectDropdownAll').multiselect({
//    includeSelectAllOption: true,
//    nonSelectedText: 'All',
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,



//});


//// SELECT FIELD STYLE
//$('.color-theme-picker div.color-item').click(function () {
//    //var elementClassName = $(this).attr('class');

//    var getColorThemeClassName = $(this).attr('class').split(" ");
//    var splitColorThemeClassName = getColorThemeClassName[1];
//    //alert(splitColorThemeClassName)
//    $('body').attr('id', splitColorThemeClassName);
//});
//$('.color-theme-icon-outer').click(function () {
//    $('.color-theme-picker').toggleClass('picker-slide');
//});



//// SELECT COLOR CODE START
//$('html').click(function () {
//    $('.slider-color-box').hide();
//});
//$('.slider-color-list').click(function () {
//    $('.slider-color-box').hide();
//});
//$('.slider-color-selecter').click(function (e) {
//    e.stopPropagation();
//});
//$('.slider-color-pick').click(function (e) {
//    $('.slider-color-box').toggle();
//});
//$('.slider-color-list').click(function () {
//    debugger
//    var one = $(this).attr('data-ticket');
//    //alert(one);
//    var classname = $(this).attr('class');
//    var getColorCodeName = $(this).attr('class').substring(18, classname.lenth)
    
//    $('.slider-color-pick span').html(getColorCodeName);
//    $('.slider-color-pick i').removeClass();
//    $('.slider-color-pick i').addClass('color-code ' + getColorCodeName);
//});

//$('html').click(function () {
//    $('.slider-color-box-two').hide();
//});
//$('.slider-color-list-two').click(function () {
//    $('.slider-color-box-two').hide();
//});
//$('.slider-color-selecter-two').click(function (e) {
//    e.stopPropagation();
//});
//$('.slider-color-pick-two').click(function (e) {
//    $('.slider-color-box-two').toggle();
//});
//$('.slider-color-list-two').click(function () {
//    var one = $(this).attr('data-ticket');
//    //alert(one);
//    var classname = $(this).attr('class');
//    var getColorCodeName = $(this).attr('class').substring(21, classname.lenth)
//    $('.slider-color-pick-two span').html(getColorCodeName);
//    $('.slider-color-pick-two i').removeClass();
//    $('.slider-color-pick-two i').addClass('color-code ' + getColorCodeName);
//});

//// SELECT COLOR CODE END






//    $('.sliderSearchInput').click(function () {
//        $('.search-profile-details').show(300)
//    });
//    $('.searchProfileSliderClose').click(function () {
//        $('.search-profile-details').hide(300)
//    });

//    $('.slider-btn').click(function () {
//        $('body').append('<div class="form-overlay"></div>');
//        $("div").removeClass('slider-forms-open');
//        // get class name this way
//        var elementClassName = $(this).attr('Id');
//        // alert(elementClassName)
//        //find the divs with this class name
//        $('div#' + elementClassName).toggleClass('slider-forms-open');
//    });
//$(".slider-form-close").click(function () {
//    $(".slider-forms").removeClass('slider-forms-open');
//        $(".slider-forms").removeClass('new-slider-expand');
//        $(".form-overlay").remove();
//    });

//    var KTCalendarBasic = function () {

//        return {
//            //main function to initiate the module
//            init: function () {
//                var todayDate = moment().startOf('day');
//                var YM = todayDate.format('YYYY-MM');
//                var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
//                var TODAY = todayDate.format('YYYY-MM-DD');
//                var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

//                var calendarEl = document.getElementById('kt_calendar');
//                var calendar = new FullCalendar.Calendar(calendarEl, {
//                    plugins: ['bootstrap', 'interaction', 'dayGrid', 'timeGrid', 'list'],
//                    themeSystem: 'bootstrap',

//                    isRTL: KTUtil.isRTL(),

//                    header: {
//                        left: 'prev,next today',
//                        center: 'title',
//                        right: 'dayGridMonth,timeGridWeek,timeGridDay'
//                    },

//                    height: 500,
//                    contentHeight: 480,
//                    aspectRatio: 3,  // see: https://fullcalendar.io/docs/aspectRatio

//                    nowIndicator: true,
//                    now: TODAY + 'T09:25:00', // just for demo

//                    views: {
//                        dayGridMonth: { buttonText: 'month' },
//                        timeGridWeek: { buttonText: 'week' },
//                        timeGridDay: { buttonText: 'day' }
//                    },

//                    defaultView: 'dayGridMonth',
//                    defaultDate: TODAY,

//                    editable: true,
//                    eventLimit: true, // allow "more" link when too many events
//                    navLinks: true,
//                    events: [

//                        {
//                            title: 'Company',
//                            start: YM + '-02',
//                            description: 'Lorem ipsum dolor sit tempor incid',
//                            end: YM + '-03',
//                            className: "fc-event-light fc-event-solid-primary"
//                        },
//                        {
//                            title: 'Life',
//                            start: YM + '-05',
//                            end: YM + '-06',
//                            className: "fc-event-solid-info fc-event-lightfc-event-info",
//                            description: 'Lorem ipsum dolor sit amet, conse ctetur'
//                        }

//                    ],

//                    eventRender: function (info) {
//                        var element = $(info.el);

//                        if (info.event.extendedProps && info.event.extendedProps.description) {
//                            if (element.hasClass('fc-day-grid-event')) {
//                                element.data('content', info.event.extendedProps.description);
//                                element.data('placement', 'top');
//                                KTApp.initPopover(element);
//                            } else if (element.hasClass('fc-time-grid-event')) {
//                                element.find('.fc-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
//                            } else if (element.find('.fc-list-item-title').lenght !== 0) {
//                                element.find('.fc-list-item-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
//                            }
//                        }
//                    }
//                });

//                calendar.render();
//            }
//        };
//    }();

//    jQuery(document).ready(function () {
//        KTCalendarBasic.init();
//    });


//    google.load("visualization", "1", { packages: ["corechart"] });
//    google.setOnLoadCallback(drawCharts);
//    function drawCharts() {

//        // BEGIN BAR CHART
//        /*
//        // create zero data so the bars will 'grow'
//        var barZeroData = google.visualization.arrayToDataTable([
//          ['Day', 'Page Views', 'Unique Views'],
//          ['Sun',  0,      0],
//          ['Mon',  0,      0],
//          ['Tue',  0,      0],
//          ['Wed',  0,      0],
//          ['Thu',  0,      0],
//          ['Fri',  0,      0],
//          ['Sat',  0,      0]
//        ]);
//          */
//        // actual bar chart data
//        var barData = google.visualization.arrayToDataTable([
//            ['Day', '', ''],
//            ['Depart', 1050, 600],
//            ['Depart', 1370, 910],
//            ['Tue', 660, 400],
//            ['Wed', 1030, 540],
//            ['Thu', 1000, 480],
//            ['Fri', 1170, 960],
//            ['Sat', 660, 320],
//            ['Sats', 660, 320],
//            ['Sats', 660, 320]
//        ]);
//        // set bar chart options
//        var barOptions = {
//            focusTarget: 'category',
//            backgroundColor: 'transparent',
//            colors: ['cornflowerblue', 'tomato'],
//            fontName: 'Open Sans',
//            chartArea: {
//                left: 50,
//                top: 10,
//                width: '100%',
//                height: '70%'
//            },
//            bar: {
//                groupWidth: '80%'
//            },
//            hAxis: {
//                textStyle: {
//                    fontSize: 11
//                }
//            },
//            vAxis: {
//                minValue: 0,
//                maxValue: 1500,
//                baselineColor: '#DDD',
//                gridlines: {
//                    color: '#DDD',
//                    count: 4
//                },
//                textStyle: {
//                    fontSize: 11
//                }
//            },
//            legend: {
//                position: 'bottom',
//                textStyle: {
//                    fontSize: 12
//                }
//            },
//            animation: {
//                duration: 1200,
//                easing: 'out',
//                startup: true
//            }
//        };
//        // draw bar chart twice so it animates
//        var barChart = new google.visualization.ColumnChart(document.getElementById('bar-chart'));
//        //barChart.draw(barZeroData, barOptions);
//        barChart.draw(barData, barOptions);

//        // BEGIN LINE GRAPH

//        function randomNumber(base, step) {
//            return Math.floor((Math.random() * step) + base);
//        }
//        function createData(year, start1, start2, step, offset) {
//            var ar = [];
//            for (var i = 0; i < 12; i++) {
//                ar.push([new Date(year, i), randomNumber(start1, step) + offset, randomNumber(start2, step) + offset]);
//            }
//            return ar;
//        }
//        var randomLineData = [
//            ['Year', 'Page Views', 'Unique Views']
//        ];
//        for (var x = 0; x < 7; x++) {
//            var newYear = createData(2007 + x, 10000, 5000, 4000, 800 * Math.pow(x, 2));
//            for (var n = 0; n < 12; n++) {
//                randomLineData.push(newYear.shift());
//            }
//        }
//        var lineData = google.visualization.arrayToDataTable(randomLineData);

//        /*
//      var animLineData = [
//        ['Year', 'Page Views', 'Unique Views']
//      ];
//      for (var x = 0; x < 7; x++) {
//        var zeroYear = createData(2007+x, 0, 0, 0, 0);
//        for (var n = 0; n < 12; n++) {
//          animLineData.push(zeroYear.shift());
//        }
//      }
//      var zeroLineData = google.visualization.arrayToDataTable(animLineData);
//        */

//        var lineOptions = {
//            backgroundColor: 'transparent',
//            colors: ['cornflowerblue', 'tomato'],
//            fontName: 'Open Sans',
//            focusTarget: 'category',
//            chartArea: {
//                left: 50,
//                top: 10,
//                width: '100%',
//                height: '70%'
//            },
//            hAxis: {
//                //showTextEvery: 12,
//                textStyle: {
//                    fontSize: 11
//                },
//                baselineColor: 'transparent',
//                gridlines: {
//                    color: 'transparent'
//                }
//            },
//            vAxis: {
//                minValue: 0,
//                maxValue: 50000,
//                baselineColor: '#DDD',
//                gridlines: {
//                    color: '#DDD',
//                    count: 4
//                },
//                textStyle: {
//                    fontSize: 11
//                }
//            },
//            legend: {
//                position: 'bottom',
//                textStyle: {
//                    fontSize: 12
//                }
//            },
//            animation: {
//                duration: 1200,
//                easing: 'out',
//                startup: true
//            }
//        };

//        var lineChart = new google.visualization.LineChart(document.getElementById('line-chart'));
//        //lineChart.draw(zeroLineData, lineOptions);
//        lineChart.draw(lineData, lineOptions);

//        // BEGIN PIE CHART


//        // pie chart options
//        var pieOptions = {
//            backgroundColor: 'transparent',
//            pieHole: 0.4,
//            colors: ["cornflowerblue",
//                "olivedrab",
//                "orange",
//                "tomato",
//                "crimson",
//                "purple",
//                "turquoise",
//                "forestgreen",
//                "navy",
//                "gray"],
//            pieSliceText: 'value',
//            tooltip: {
//                text: 'percentage'
//            },
//            fontName: 'Open Sans',
//            chartArea: {
//                width: '100%',
//                height: '94%'
//            },
//            legend: {
//                textStyle: {
//                    fontSize: 13
//                }
//            }
//        };

//    }



$(document).ready(function () {
    var selectDivHeight = $(".tab-only .tab-content.active").height();
    $('.ulList').height(selectDivHeight + 40);

    $('.tab-outer .ulList li.clickOnTab').click(function () {
        var myTabList = $(this).closest('.tab-outer');
        myTabList.find('.ulList li.clickOnTab').removeClass('active');
        $(this).addClass('active');


        var getLiClass = $(this).attr('class').split(" ");
        var splitFirstClass = getLiClass[0];
        myTabList.find('.tab-content.active').slideUp(300, function () {
            $(this).removeClass('active');
            $('#' + getLiClass).slideDown(300, function () {
                $(this).addClass('active');
                var selectDivHeight = $(".tab-only .tab-content.active").height();
                $('.ulList').height(selectDivHeight + 40);
                // alert(selectDivHeight);
            });

        });

    });


    var avatar4 = new KTImageInput('kt_image_4');

    avatar4.on('cancel', function (imageInput) {
        //swal.fire({
        //	title: 'Image Successfully Canceled',
        //	type: 'success',
        //	buttonsStyling: false,
        //	confirmButtonText: 'OK',
        //	confirmButtonClass: 'btn btn-primary font-weight-bold'
        //});
    });

    avatar4.on('change', function (imageInput) {
        //swal.fire({
        //	title: 'Image Successfully Changed',
        //	type: 'success',
        //	buttonsStyling: false,
        //	confirmButtonText: 'OK',
        //	confirmButtonClass: 'btn btn-primary font-weight-bold'
        //});
    });

    avatar4.on('remove', function (imageInput) {
        //swal.fire({
        //	title: 'Image Successfully Removed',
        //	type: 'error',
        //	buttonsStyling: false,
        //	confirmButtonText: 'OK',
        //	confirmButtonClass: 'btn btn-primary font-weight-bold'
        //});
    });



    $(".clickOnTab").click(function () {
        $('html, body').animate({
            scrollTop: $(".TabMoving").offset().top
        }, 2000);
    });


    //$('.slider-btn').click(function() {
    //	$('body').append('<div class="form-overlay"></div>');
    //	$("div").removeClass('slider-forms-open');    
    //   // get class name this way
    //   var elementClassName = $(this).attr('Id');
    //   // alert(elementClassName)
    //   //find the divs with this class name
    //   $('div#'+elementClassName).toggleClass('slider-forms-open');                                         
    //  });
    $(".slider-form-close").click(function () {
        $(".slider-forms").removeClass('slider-forms-open');
        $(".slider-forms").removeClass('new-slider-expand');
        $(".form-overlay").remove();
    });

    var multipleCancelButton = new Choices('.choices-multiple-remove-button', {
        removeItemButton: true,
        // maxItemCount: 5,
        // searchResultLimit: 5,
        // renderChoiceLimit: 5

    });
    var multipleCancelButton = new Choices('.choices-multiple-remove-button-two', {
        removeItemButton: true,
        // maxItemCount: 5,
        // searchResultLimit: 5,
        // renderChoiceLimit: 5

    });

});



$(function () {
    $('.filter-combobox select').change(function () {
        var val = $(this).val();

        if (val) {
            $('.selectFieldValuShow div:not(#' + val + ')').hide();
            $('#' + val).show();
        } else {
            $('.selectFieldValuShow div').hide();
        }
    });
    $('.multiselectDropdownAll').multiselect('selectAll', false);
    $('.multiselectDropdownAll').multiselect('updateButtonText');
});

//$(function () {
//    $('[data-bs-toggle="tooltip"]').tooltip()

//  })
//$('[data-bs-toggle="tooltip"]').tooltip({
//    trigger: 'hover',
//    container: '.wrapper'
//  });
//$(".tooltip-link").on("mouseover", function () {

//	$(".tooltip-hover").fadeIn(300, function () {

//	});

//})
//$(".tooltip-link").on("mouseout", function () {

//	$(".tooltip-hover").fadeOut(300, function () {

//	});

//})
$(".btn ").click(function () {
    $(".tooltip-hover").fadeOut(300, function () {
    });

});


$(".click-button").click(function () {

    $(".fields-box").slideToggle("slow")

});

$('.search-click').click(function () {
    $('.employee-pProfile-search-result').show(300);
});
$('.search-close').click(function () {
    $('.employee-pProfile-search-result').hide(300);
});

$(".search-slider-inner").click(function () {
    $(".form-control").toggleClass('field-width-big')
});




$(".search-slider-new").click(function () {
    $(".form-controls").toggleClass('field-width-big')
});



$("#sliderMenuInclude").load("menu.html");


/*$('#mySelect').selectpicker();*/

// CHECK BOX VIEW HIDE

$("#coupon_question").on("click", function () {
    $(".showCheckBox").toggle(this.checked);
});




$('.multiselectDropdownField ').multiselect({
    includeSelectAllOption: true,
    /*  enableFiltering: true,*/
    enableCaseInsensitiveFiltering: true,
    includeFilterClearBtn: false,
    includeSelectAllOption: false,



});

$('.multiselectDropdownFieldSelectAll').multiselect({
    includeSelectAllOption: true,
    nonSelectedText: 'All',
    enableCaseInsensitiveFiltering: true,
    includeFilterClearBtn: false,



});
$('.multiselectDropdownFieldSelectAll').multiselect('selectAll', false);

// CHECK BOX VIEW HIDE
// SELECT FIELD STYLE

//$('.multiselectDropdownField').multiselect({
//    includeSelectAllOption: true,
//  /*  enableFiltering: true,*/
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,
//    includeSelectAllOption: false,



//});





//$('.ResignResponsible').multiselect({
//    includeSelectAllOption: true,
//    /*  enableFiltering: true,*/
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,
//    includeSelectAllOption: false,
//    numberDisplayed: -1,


//});
//$('.multiselectDropdownFields').multiselect({
//    includeSelectAllOption: true,
//    nonSelectedText: 'All',
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,



//});

//$('.multiselectDropdownAll').multiselect({
//    includeSelectAllOption: true,
//    nonSelectedText: 'All',
//    enableCaseInsensitiveFiltering: true,
//    includeFilterClearBtn: false,



//});


// SELECT FIELD STYLE
$('.color-theme-picker div.color-item').click(function () {
    //var elementClassName = $(this).attr('class');

    var getColorThemeClassName = $(this).attr('class').split(" ");
    var splitColorThemeClassName = getColorThemeClassName[1];
    //alert(splitColorThemeClassName)
    $('body').attr('id', splitColorThemeClassName);
});
$('.color-theme-icon-outer').click(function () {
    $('.color-theme-picker').toggleClass('picker-slide');
});



// SELECT COLOR CODE START
$('html').click(function () {
    $('.slider-color-box').hide();
});
$('.slider-color-list').click(function () {
    $('.slider-color-box').hide();
});
$('.slider-color-selecter').click(function (e) {
    e.stopPropagation();
});
$('.slider-color-pick').click(function (e) {
    $('.slider-color-box').toggle();
});
$('.slider-color-list').click(function () {
    debugger
    var one = $(this).attr('data-ticket');
    //alert(one);
    var classname = $(this).attr('class');
    var getColorCodeName = $(this).attr('class').substring(18, classname.lenth)

    $('.slider-color-pick span').html(getColorCodeName);
    $('.slider-color-pick i').removeClass();
    $('.slider-color-pick i').addClass('color-code ' + getColorCodeName);
});

$('html').click(function () {
    $('.slider-color-box-two').hide();
});
$('.slider-color-list-two').click(function () {
    $('.slider-color-box-two').hide();
});
$('.slider-color-selecter-two').click(function (e) {
    e.stopPropagation();
});
$('.slider-color-pick-two').click(function (e) {
    $('.slider-color-box-two').toggle();
});
$('.slider-color-list-two').click(function () {
    var one = $(this).attr('data-ticket');
    //alert(one);
    var classname = $(this).attr('class');
    var getColorCodeName = $(this).attr('class').substring(21, classname.lenth)
    $('.slider-color-pick-two span').html(getColorCodeName);
    $('.slider-color-pick-two i').removeClass();
    $('.slider-color-pick-two i').addClass('color-code ' + getColorCodeName);
});

// SELECT COLOR CODE END






$('.sliderSearchInput').click(function () {
    $('.search-profile-details').show(300)
});
$('.searchProfileSliderClose').click(function () {
    $('.search-profile-details').hide(300)
});

//    $('.slider-btn').click(function () {
//        $('body').append('<div class="form-overlay"></div>');
//        $("div").removeClass('slider-forms-open');
//        // get class name this way
//        var elementClassName = $(this).attr('Id');
//        // alert(elementClassName)
//        //find the divs with this class name
//        $('div#' + elementClassName).toggleClass('slider-forms-open');
//    });


//$(".slider-form-close").click(function () {
//    $(".slider-forms").removeClass('slider-forms-open');
//        $(".slider-forms").removeClass('new-slider-expand');
//        $(".form-overlay").remove();
//    });

$('.slider-btn').click(function () {
    $('body').append('<div class="form-overlay"></div>');
    $("div").removeClass('slider-forms-open');
    // get class name this way
    var elementClassName = $(this).attr('Id');
    // alert(elementClassName)
    //find the divs with this class name
    $('div#' + elementClassName).toggleClass('slider-forms-open');
});
$(".slider-form-close").click(function () {
    $(".slider-forms").removeClass('slider-forms-open');
    $(".slider-forms").removeClass('new-slider-expand');
    $(".form-overlay").remove();
});
var KTCalendarBasic = function () {

    return {
        //main function to initiate the module
        init: function () {
            var todayDate = moment().startOf('day');
            var YM = todayDate.format('YYYY-MM');
            var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
            var TODAY = todayDate.format('YYYY-MM-DD');
            var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

            var calendarEl = document.getElementById('kt_calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                plugins: ['bootstrap', 'interaction', 'dayGrid', 'timeGrid', 'list'],
                themeSystem: 'bootstrap',

                isRTL: KTUtil.isRTL(),

                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },

                height: 500,
                contentHeight: 480,
                aspectRatio: 3,  // see: https://fullcalendar.io/docs/aspectRatio

                nowIndicator: true,
                now: TODAY + 'T09:25:00', // just for demo

                views: {
                    dayGridMonth: { buttonText: 'month' },
                    timeGridWeek: { buttonText: 'week' },
                    timeGridDay: { buttonText: 'day' }
                },

                defaultView: 'dayGridMonth',
                defaultDate: TODAY,

                editable: true,
                eventLimit: true, // allow "more" link when too many events
                navLinks: true,
                events: [

                    {
                        title: 'Company',
                        start: YM + '-02',
                        description: 'Lorem ipsum dolor sit tempor incid',
                        end: YM + '-03',
                        className: "fc-event-light fc-event-solid-primary"
                    },
                    {
                        title: 'Life',
                        start: YM + '-05',
                        end: YM + '-06',
                        className: "fc-event-solid-info fc-event-lightfc-event-info",
                        description: 'Lorem ipsum dolor sit amet, conse ctetur'
                    }

                ],

                eventRender: function (info) {
                    var element = $(info.el);

                    if (info.event.extendedProps && info.event.extendedProps.description) {
                        if (element.hasClass('fc-day-grid-event')) {
                            element.data('content', info.event.extendedProps.description);
                            element.data('placement', 'top');
                            KTApp.initPopover(element);
                        } else if (element.hasClass('fc-time-grid-event')) {
                            element.find('.fc-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
                        } else if (element.find('.fc-list-item-title').lenght !== 0) {
                            element.find('.fc-list-item-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
                        }
                    }
                }
            });

            calendar.render();
        }
    };
}();

jQuery(document).ready(function () {
    KTCalendarBasic.init();
});


google.load("visualization", "1", { packages: ["corechart"] });
google.setOnLoadCallback(drawCharts);
function drawCharts() {

    // BEGIN BAR CHART
    /*
    // create zero data so the bars will 'grow'
    var barZeroData = google.visualization.arrayToDataTable([
      ['Day', 'Page Views', 'Unique Views'],
      ['Sun',  0,      0],
      ['Mon',  0,      0],
      ['Tue',  0,      0],
      ['Wed',  0,      0],
      ['Thu',  0,      0],
      ['Fri',  0,      0],
      ['Sat',  0,      0]
    ]);
      */
    // actual bar chart data
    var barData = google.visualization.arrayToDataTable([
        ['Day', '', ''],
        ['Depart', 1050, 600],
        ['Depart', 1370, 910],
        ['Tue', 660, 400],
        ['Wed', 1030, 540],
        ['Thu', 1000, 480],
        ['Fri', 1170, 960],
        ['Sat', 660, 320],
        ['Sats', 660, 320],
        ['Sats', 660, 320]
    ]);
    // set bar chart options
    var barOptions = {
        focusTarget: 'category',
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        chartArea: {
            left: 50,
            top: 10,
            width: '100%',
            height: '70%'
        },
        bar: {
            groupWidth: '80%'
        },
        hAxis: {
            textStyle: {
                fontSize: 11
            }
        },
        vAxis: {
            minValue: 0,
            maxValue: 1500,
            baselineColor: '#DDD',
            gridlines: {
                color: '#DDD',
                count: 4
            },
            textStyle: {
                fontSize: 11
            }
        },
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 12
            }
        },
        animation: {
            duration: 1200,
            easing: 'out',
            startup: true
        }
    };
    // draw bar chart twice so it animates
    var barChart = new google.visualization.ColumnChart(document.getElementById('bar-chart'));
    //barChart.draw(barZeroData, barOptions);
    barChart.draw(barData, barOptions);

    // BEGIN LINE GRAPH

    function randomNumber(base, step) {
        return Math.floor((Math.random() * step) + base);
    }
    function createData(year, start1, start2, step, offset) {
        var ar = [];
        for (var i = 0; i < 12; i++) {
            ar.push([new Date(year, i), randomNumber(start1, step) + offset, randomNumber(start2, step) + offset]);
        }
        return ar;
    }
    var randomLineData = [
        ['Year', 'Page Views', 'Unique Views']
    ];
    for (var x = 0; x < 7; x++) {
        var newYear = createData(2007 + x, 10000, 5000, 4000, 800 * Math.pow(x, 2));
        for (var n = 0; n < 12; n++) {
            randomLineData.push(newYear.shift());
        }
    }
    var lineData = google.visualization.arrayToDataTable(randomLineData);

    /*
  var animLineData = [
    ['Year', 'Page Views', 'Unique Views']
  ];
  for (var x = 0; x < 7; x++) {
    var zeroYear = createData(2007+x, 0, 0, 0, 0);
    for (var n = 0; n < 12; n++) {
      animLineData.push(zeroYear.shift());
    }
  }
  var zeroLineData = google.visualization.arrayToDataTable(animLineData);
    */

    var lineOptions = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        focusTarget: 'category',
        chartArea: {
            left: 50,
            top: 10,
            width: '100%',
            height: '70%'
        },
        hAxis: {
            //showTextEvery: 12,
            textStyle: {
                fontSize: 11
            },
            baselineColor: 'transparent',
            gridlines: {
                color: 'transparent'
            }
        },
        vAxis: {
            minValue: 0,
            maxValue: 50000,
            baselineColor: '#DDD',
            gridlines: {
                color: '#DDD',
                count: 4
            },
            textStyle: {
                fontSize: 11
            }
        },
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 12
            }
        },
        animation: {
            duration: 1200,
            easing: 'out',
            startup: true
        }
    };

    var lineChart = new google.visualization.LineChart(document.getElementById('line-chart'));
    //lineChart.draw(zeroLineData, lineOptions);
    lineChart.draw(lineData, lineOptions);

    // BEGIN PIE CHART


    // pie chart options
    var pieOptions = {
        backgroundColor: 'transparent',
        pieHole: 0.4,
        colors: ["cornflowerblue",
            "olivedrab",
            "orange",
            "tomato",
            "crimson",
            "purple",
            "turquoise",
            "forestgreen",
            "navy",
            "gray"],
        pieSliceText: 'value',
        tooltip: {
            text: 'percentage'
        },
        fontName: 'Open Sans',
        chartArea: {
            width: '100%',
            height: '94%'
        },
        legend: {
            textStyle: {
                fontSize: 13
            }
        }
    };

}

