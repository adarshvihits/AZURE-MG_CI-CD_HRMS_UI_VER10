var salVisible;


function ValidateDate() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
      /*  $("#To").val('');*/



        return false;
    }
    else
        return true;
}
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    if (fromDate != '' && toDate != '') {
    

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#JoiningReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/JoiningReport/GetList",
            "type": "POST",

            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
              

                $('#JoiningReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    if (d.data[i].absoluteUri != "" && d.data[i].absoluteUri != null)
                        d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                    else
                        d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div > <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].dob = _GetFormattedDate(d.data[i].dob);
                }
                console.log(d.data)
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeid", "width": "10%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "11%" },
            { "data": "functionalState", "width": "10%" },
            { "data": "category", "width": "12%" },
            { "data": "doj", "width": "9%" },
            { "data": "dob", "width": "9%" },
            { "data": "grossPay", "width": "9%" },


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
        dataTable.search('').draw();
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

        if (salVisible == 0) {
            let column7 = dataTable.column(7);
            column7.visible(false);
        }
}
}




function OnGroupSummaryClick() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();

    if (fromDate == '' && toDate == '')
    {
        debugger;
        swal.fire("Please select From and To date");
        var element = document.getElementById("myDiv");
        element.style.display = "none";      
    }
    else
    {
        debugger;
        $.ajax({
            type: 'GET',
            url: "/DataBank/JoiningReport/GetGroupSummary?fromDate=" + fromDate + "&toDate=" + toDate,
            dataType: 'html',

            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    $("#myDiv").css("display", "block");
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
   
}


function groupbyChange(sender) {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var ddid = $(sender).val();
    var gname = $('#GroupByFill_Code option:selected').text();

    $.ajax({

        type: 'GET',
        url: '/Databank/JoiningReport/GroupByBasedSummary',
        dataType: 'html',
        data: { id: ddid, groupbyname: gname, fromDate: fromDate, toDate: toDate },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#listsummary').html("");
                $('#listsummary').html(result);
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



function ExportToExcel()
{

    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    
    var tableLength = $('#JoiningReportList_Count').val();
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        url = "/DataBank/JoiningReport/ExportToExcelDataset?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}








function GroupSummaryExportToExcel() {


    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var id = $('#GroupByFill_Code').val();
    var type = $('#GroupByFill_Code option:selected').text();
    var tableLength = document.getElementById("GroupsummaryTable").rows.length - 1;
    if (tableLength > 0) {
        url = "/DataBank/JoiningReport/GroupSummaryExportToExcel?id=" + id +"&fromDate=" + fromDate + "&toDate=" + toDate+"&type="+type;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }



}


//function ApplyFilter(mode) {
//    fillData();
//    CloseSlider('basicFilter');
//}






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


