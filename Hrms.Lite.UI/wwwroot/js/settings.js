
var dataTable;
$(document).ready(function () {


 


    //$('a[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
    //    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    //});
   /* setTimeout(function () {*/

    $.fn.DataTable.ext.pager.numbers_length = 5;

        dataTable = $('.common-table').DataTable({
          
            "paging": true,
            
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "pagingType": 'full_numbers',
        //"pagingType": 'full_numbers_no_ellipses',
        "scrollY": '60vh',
        "sScrollX": "100%",
            "sScrollXInner": "100%",
            
            "ordering": false,
            "autoWidth": false,
            
        
        
       
           
        "scrollCollapse": false,
      
          // "dom": '<"top" if>rt<"bottom" lp>',
            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            

            language: {

                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 60vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 60vh;"><img src="/images/nodata.png" /></div >',
                
                sLengthMenu: "_MENU_",
                paginate: {
                    //next: '&#8594;', // or '→'
                    //previous: '&#8592;' // or '←' 

                    next: '<img src="/images/icons/Icon awesome-arrow-right-1.png">',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'

                    //next: '<span class="glyphicon glyphicon-menu-right"></span>',
                    //previous: '<span class="glyphicon glyphicon-menu-left"></span>'

                }
            }
          


    });


      //  $(".intro, .demo, .end").css('background', 'green');
    


       $('.ihits-table table').removeClass("dataTable no-footer");


        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

       $(".ihits-table table tr th").removeAttr('style');


    $("#dataTableSearch").on("keyup search paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        if (!$('.sraech-slider #dataTableSearch').hasClass('field-width-big'))
            dataTable.search('').draw();
    });
    debugger;
    var info = dataTable.page.info();

    dataTable.on('draw', function () {

       

            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.next').attr("title", "Next");
            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.previous').attr("title", "Previous");
            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.first').attr("title", "First");
            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.last').attr("title", "Last");

        });

/*}, 100);*/
   

    dataTable.page(currentPage).draw(false);

    });



