
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var Month = $('#PayrollMonth_Code').val();
    var PayRollGroup = $('#PayrollGroup_Code').val();
   
    var dataTable = $("#SalaryReportDraft").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryReportDraft/GetSalaryReportDraftList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&PayrollGroup=' + PayRollGroup + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {

                $('#SalaryDraft_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].employeeHeader.employeeName = '<td data-label="Employee Name"><a data-bs-toggle="modal"data-target=".bd-example-modal-lg" type="button"class="btn  btn-sm float-left" onclick="ShowNetPaySplitup(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ', ' + Month + ', ' + "'" + PayRollGroup + "'" + ')">' + d.data[i].employeeHeader.employeeName + '</a><input type="hidden" id="empID_' + i + '" value="' + d.data[i].employeeHeader.employeeID + '"></td><input type="hidden" id="empNAME_' + i + '" value="' + d.data[i].employeeHeader.employeeName + '"></td>'
                 
                }

                return d.data;
             
            }


         

        },

        "columns": [


            { "data": "employeeHeader.employeeId", "width":"8%"  },
            { "data": "employeeHeader.employeeName", "width": "16%"   },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "9%"  },
            { "data": "location", "width": "9%"  },
            { "data": "lopDays", "width": "5%" },
            { "data": "paidDays", "width": "5%" },
            { "data": "actualGross", "width": "6%" },
            { "data": "lopAmount", "width": "5%" },
            { "data": "earnedGross", "width": "8%"  },
            { "data": "totalDeduction", "width": "5%"  },
            { "data": "holdAmount", "width": "7%"  },
            { "data": "netPay", "width": "7%" },


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
        //dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
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

    $.fn.DataTable.ext.pager.numbers_length = 7;







}
function ExportToExcel() {
    debugger;
    var Month = $('#PayrollMonth_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var MonthName = $('#PayrollMonth_Code option:selected').text();
    var PayrollGroupName = $('#PayrollGroup_Code option:selected').text();

    var TableLength = $('#SalaryDraft_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReportDraft/ExportToExcel?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&MonthName=" + MonthName + "&PayrollGroupName=" + PayrollGroupName + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function ApplyFilter(mode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
   
}
function ShowNetPaySplitup(EmployeeGI, SalaryPeriod, PayRollGroup) {

    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryReportDraft/SalaryReportSplitupDetails',
        dataType: 'html',
        data: { SalaryPeriod: SalaryPeriod, EmployeeGI: EmployeeGI, PayRollGroup: PayRollGroup },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
           
            }

            hideSpinner();
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });



}