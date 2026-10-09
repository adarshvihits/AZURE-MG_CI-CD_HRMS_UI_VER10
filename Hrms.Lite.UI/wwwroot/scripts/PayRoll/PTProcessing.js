
var monthNames = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var Month = $('#Month_Code').val();
    var PTgroup = $('#PTGroup_Code').val();


    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#PTprocessingTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PTProcessing/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&PTgroup=' + PTgroup + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#Details_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].PTgroup = '<a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title="Edit" id="readeredit" type="button" class="btn btn-icon btn-edit icon-only slider-btn" onclick="OnEditClick(' + "'" + d.data[i].employeeGI + "'" +')"><i class="icon-xl fas fa-pencil-alt"></i></a>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "4%" },
            { "data": "employeeName", "width": "9%" },
            { "data": "actualPTApplicabileSalary", "width": "7%" },
            { "data": "processedMonth", "width": "6%" },
            { "data": "processedSalary", "width": "6%" },
            { "data": "notProcessedMonth", "width": "7%" },
            { "data": "notProcessedSalary", "width": "7%" },
            { "data": "ptSalary", "width": "5%" },
            { "data": "basic", "width": "4%" },
            { "data": "otherAllowance", "width": "6%" },
            { "data": "ptAmount", "width": "5%" },
            { "data": "ptAlreadyPaid", "width": "5%" },
            { "data": "currentMonthPT", "width": "4%" },
            { "data": "remainingAmount", "width": "5%" },
            { "data": "dividingPeriod", "width": "5%" },
            { "data": "PTgroup", "width": "4%" },
          /*  { "data": "PTgroup" },*/

           

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




    const myTimeout = setTimeout(tooltipHover, 1000);


}



function ApplyFilter(mode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    
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


function ExportToExcel() {
    debugger;
    debugger;
    var Month = $('#Month_Code').val();
    var PTgroup = $('#PTGroup_Code').val();
    var MonthName = $('#Month_Code option:selected').text();
    var ptGroupName = $('#PTGroup_Code option:selected').text();
    var tableLength = $('#Details_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/PTProcessing/ExportToExcel?Month=" + Month + "&PTgroup=" + PTgroup + "&MonthName=" + MonthName + "&ptGroupName=" + ptGroupName + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function GetData(sender) {
    debugger;
  /* showSpinner();*/
    $.ajaxSetup({
        async: false
    });
    var Code = $(sender).val();
    var month = $('#Month_Code').val();
    
    $.getJSON("/PayRoll/PTProcessing/GetDetails", { month: month, Ptgroup: Code }, function (data) {
        console.log(data);
        $('#hideDisplayData').removeClass('hide');
        $('.fromdate').html(GetFormattedDate(data.from));
        $('.todate').html(GetFormattedDate(data.to));
        $('.salary').html(data.salaryMonths);
        $('.duration').html(data.duration);

    });
    fillData();

}

function GetFormattedDate(date) {
    debugger


    var formattedDate = new Date(date);
    var cDate = formattedDate.getDate();
    var cMonth = formattedDate.getMonth();
    var cYear = formattedDate.getFullYear();
    if (cDate < 10)
        cDate = '0' + cDate;
    formattedDate = (cDate + "-" + monthNames[cMonth] + "-" + cYear);
    return formattedDate;

}

function OnEditClick(EmployeeGI)
{
    debugger;
    showSpinner();
    var month = $('#Month_Code').val();
    var PtGroup = $('#PTGroup_Code').val();
    //var url = "/PayRoll/PTProcessing/GetBatchUpdateDetails?Month=" + month + "&PTGroup=" + PtGroup;
    //$('#BatchUpdateSlider').load(url, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PTProcessing/GetBatchUpdateDetails',
        dataType: 'html',
        data: { Month: month, PTGroup: PtGroup, Mode:'EDIT', EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editSlider').html("");
                $('#editSlider').html(result);


                //$('#PTProcessingList').html("");
            //    $('#PTProcessingList').html(result);
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

function OnBatchUpdate()
{
    debugger;
    var i = 0;
    var month = $('#Month_Code').val();
    var PtGroup = $('#PTGroup_Code').val();
    //var url = "/PayRoll/PTProcessing/GetBatchUpdateDetails?Month=" + month + "&PTGroup=" + PtGroup;
    //$('#BatchUpdateSlider').load(url, function () { hideSpinner(); });
    if (!validateById('#Month_Code','mCode'))
         i++; 
    if (!validateById('#PTGroup_Code','ptGrp'))
        i++;

    if (i == 0) {

        //showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/PTProcessing/GetBatchUpdateDetails',
            dataType: 'html',
            data: { Month: month, PTGroup: PtGroup, Mode: 'UPDATE', EmployeeGI:null },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#BatchUpdateSlider').html("");
                    $('#BatchUpdateSlider').html(result);
                }

                //hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                //hideSpinner();

            }
        });
    }
}


function GetList(sender) {
    debugger;
    showSpinner();
    var Code = $(sender).val();
    var PtGroup= $('#PTGroup_Code').val();
    //var url = "/PayRoll/PTProcessing/GetPTProcessingList?Month=" + Code + "&PTGroup=" + PtGroup;
    //$('#PTProcessingList').load(url, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PTProcessing/GetPTProcessingList',
        dataType: 'html',
        data: { Month: Code, PTGroup: PtGroup },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#PTProcessingList').html("");
                $('#PTProcessingList').html(result);
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


function Save() {
    debugger;
    var month = $('#Month_Code').val();
    var PtGroup = $('#PTGroup_Code').val();
    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/PayRoll/PTProcessing/Save?month=' + month + '&PtGroup=' + PtGroup,
                dataType: 'json',
                data: $('#BatchUpdate_PTprocessing').serialize(),
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire("Updated Successfully").then(function () {
                     
                            fillData();
                            CloseSlider('batchupdateDiv');
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }
        else {
            return false;
        }
    })
}