function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    //var PTGroupMonth = $('#PTGroupMonth').val();
    //var PTGroupCode = $('#PTGroupCode').val();
    //if (PTGroupMonth != '' && PTGroupCode != '') {


        /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#employeeRejoinTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeRejoin/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    //if (d.data.length != 0) {
                    //    $('.alltab').html(d.data[0].active);
                    //    $('.assgnd').html(d.data[0].ASSIGNED);
                    //    $('.notassgnd').html(d.data[0].NOTASSIGNED);
                    //}
                    //else {
                    //    $('.alltab').html(0);
                    //}
                    //console.log(d);
                    //$('#GetPTReportList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        //d.data[i].employeeid = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + '</a>'+ d.data[i].employeeid+'</div>'
                        d.data[i].employeeHeader.employeeName = '<a href = "/DataBank/EmployeeRejoin/Create?EnrollGI=' + d.data[i].employeeHeader.employeeGuid + '">' + d.data[i].employeeHeader.employeeName + ' </a>'

                        d.data[i].employeeHeader.doj = _GetFormattedDate(d.data[i].employeeHeader.doj);
                        d.data[i].employeeHeader.lwd = _GetFormattedDate(d.data[i].employeeHeader.lwd);


                    }

                    return d.data;
                }

            },

            "columns": [

                //{ "data": "employeeID" },
                { "data": "employeeHeader.employeeId", "width": "10%" },
                { "data": "employeeHeader.employeeName","width":"20%" },
                { "data": "department.name", "width": "20%"  },
                { "data": "designation.name", "width": "15%" },
                { "data": "location.name", "width": "15%"  },
                { "data": "employeeHeader.doj", "width": "10%"  },
                { "data": "employeeHeader.lwd", "width": "10%" },
                


            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "order": [0, "asc"],
            "processing": "true",
            "language": {

                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                sLengthMenu: "_MENU_",
                paginate: {


                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'




                }
            }



        });



        $('.ihits-table table').removeClass("dataTable no-footer");


        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");


        $("#dataTableSearch").on("keyup search input paste cut", function () {
            dataTable.search(this.value).draw();

            $('#noDataFound').css("display", 'none');
        });

        $('.search-slider-inner').click(function () {

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            //dataTable.search('').draw();
        });

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

        $.fn.DataTable.ext.pager.numbers_length = 5;

   // }
}


function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
           
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
       
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}