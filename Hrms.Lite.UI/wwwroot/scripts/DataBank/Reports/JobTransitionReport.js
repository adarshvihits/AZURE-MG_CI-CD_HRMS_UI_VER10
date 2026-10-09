function fillData()
{
    debugger;
    var TranType = $('#JobTransitionsType_Code').val();
    var fromdate = $('#From').val();
    var todate = $('#To').val();

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;


    var dataTable = $("#JobTransitionReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/JobTransitionReport/GetJobTransitionReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromdate + '&F_To=' + todate + "&TransitionType=" + TranType + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {






                    $('#JobTransitionReportDetails_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].effectiveDate = _GetFormattedDate(d.data[i].effectiveDate);
                        d.data[i].orderDate = _GetFormattedDate(d.data[i].orderDate);
                        d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                        //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","Width":"10%" },
                { "data": "employeeName", "Width": "30%" },
                { "data": "effectiveDate", "Width": "10%" },
                { "data": "orderDate", "Width": "10%" },
                { "data": "entryDate", "Width": "10%" },
                { "data": "transitionType", "Width": "10%" },
                { "data": "reason", "Width": "20%" },
              



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
            dataTable.search('').draw();
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

        $.fn.DataTable.ext.pager.numbers_length = 5;


}

function getList() {
    debugger

    var TranType = $('#JobTransitionsType_Code').val();
    var fromdate = $('#From').val();
    var todate = $('#To').val();


    if (fromdate !== '' && todate !== '' && TranType !== '') {
        showSpinner();
       
        $.ajax({

            type: 'POST',
            url: '/DataBank/JobTransitionReport/GetJobTransitionReport',
            dataType: 'html',
            data: { fromdate: fromdate, todate: todate, TranType: TranType},

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#jobTransitionreportList').html("");
                    $('#jobTransitionreportList').html(result);
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
        return true;
    }
    else {

        return false;
    }
   
}


function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}


function OnGridSettingsClick() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();


    $.ajax({
        type: 'GET',
        url: "/DataBank/JobTransitionReport/GetGridSetting?fromDate=" + fromDate + "&toDate=" + toDate,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
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

function OnApplyClick() {


    debugger;
   
 
    var TranType = $('#JobTransitionsType_Code').val();
    var fromdate = $('#From').val();
    var todate = $('#To').val();
    var table = $('#PopUptable').DataTable();
    /*var data = table.$('input,select,textarea').serializeArray();*/
    console.log($('#Emp-popup').serialize());
    $.ajax({

        type: 'POST',
        url: '/DataBank/JobTransitionReport/FilterColumn?fromdate=' + fromdate + "&todate=" + todate + "&TranType=" + TranType,

        dataType: 'html',
        data: $('#Emp-popup').serialize(),
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.bd-example-modal-lg').modal('hide');
                $('#jobTransitionreportList').html(result);

            }
        },
            error: function(xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

        }
    });

}


function ExportToExcel() {

    debugger;

    var fromdate = $('#From').val();
    var todate = $('#To').val();
    var JobTranType = $('#JobTransitionsType_Code').val();
    var Type = $('#JobTransitionsType_Code option:selected ').text();

    var pendingTableLength = $('#JobTransitionReportDetails_Count').val();
   /* var pendingTableLength = document.getElementById("JobTransitionReportTable").rows.length - 1;*/
        if (pendingTableLength > 0) {
            var url = "/DataBank/JobTransitionReport/ExportToExcel?fromdate=" + fromdate + "&todate=" + todate + "&JobTranType=" + JobTranType + "&Type=" + Type + "&TotalRows=" + pendingTableLength;
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