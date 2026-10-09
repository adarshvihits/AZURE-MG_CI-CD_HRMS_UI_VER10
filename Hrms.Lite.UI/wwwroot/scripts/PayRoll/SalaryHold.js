
function getPopUp() {
    var m = $('.bd-example-modal-lg');
    m.modal('show');
}
function fillData() {
    debugger;
 
    var filter1 = $('#BasicFilter').serialize();

    var month = $('#Month').val();
    var payroll = $('#PayrollGroup').val();

    
    var dataTable = $("#TableSalaryHold").DataTable({
        
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryHold/GetSalaryHoldList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var searchId = $('#Filter_Code').val();
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                        '&SearchId=' + searchValue + '&Month=' + month + '&PayrollGroupCode=' + payroll+'&dummy=dummy';
                   
                    d.filter = temp;

                

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {


                $('#SalaryHoldList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

 
                        d.data[i].month = '<a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Edit" id = "empattendancesettings" type = "button"class="btn btn-icon btn-edit icon-only slider-btn" onclick = "onEditClick(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ',' + month + ')" > <i class="icon-xl fas fa-pencil-alt"></i></a >'



                }


                return d.data;
            }

        },



        "columns": [

            { "data": "employeeHeader.employeeId" },
            { "data": "employeeHeader.employeeName"},
            { "data": "designation"},
            { "data": "department"},
            { "data": "location" },
            { "data": "grossSalry"},
            { "data": "grossDeduction" },
            { "data": "netPay" },
            { "data": "holdAmount"},
            { "data": "remarks"},
            { "data": "month" },
            



        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "autoWidth": false,


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
    debugger;


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

    $.fn.DataTable.ext.pager.numbers_length = 5;

    const myTimeout = setTimeout(tooltipHover, 500);
}
function onEditClick(EmpGI, month) {
    debugger;
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryHold/GetSalaryHoldDetails',
        dataType: 'html',
        data: {
            EmpGI: EmpGI,
            month: month
           
        },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#SalaryHoldSlider').html("");
                $('#SalaryHoldSlider').html(result);
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
function validatefields() {
    debugger;
    var i = 0;
    if (!validatezero('#HoldAmount', 'holdamnt')) {
        i++;
    }
    if (!validateById('#Remarks', 'remarks')) {
        i++;
    }

    if (i == 0)
        return true;
    else {
        return false;
    }
}
function CheckHoldAmount(sender) {
    debugger;
    var netpay = $('#NetPay').val();
    var holdAmt = $(sender).val();
   
    if (parseFloat(netpay) < parseFloat(holdAmt))
    {
        var amt = document.getElementById('HoldAmount');
        amt.value = '';
        swal.fire("Hold amount should be less than or equal to net pay");
      
    }
    
}
function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/SalaryHold/fillMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month option').remove();
        $('#Month').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    fillData();
}
function Save() {
    debugger;
    var month = $('#Month').val();
    if (validatefields()) {
        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger



                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/SalaryHold/Save?month=' + month,
                    dataType: 'json',
                    data: $('#Save_Salary_Hold').serialize(),
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                debugger
                                fillData();
                                CloseSlider('salaryHld');



                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
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
    else {
        return false;
    }
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


function validatezero(elementId, spanId) {
    debugger
    debugger;
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else if ($(elementId).val() == 0) {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("Zero is not allowded");
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
   
}

function ExportToExcel() {
    debugger;
    var tableLength = $('#SalaryHoldList_Count').val();
    var month = $('#Month').val();
    var Monthname = $('#Month option:selected').text();

   var payroll = $('#PayrollGroup').val();
    if (tableLength > 0)
    {
        url = '/PayRoll/SalaryHold/ExportToExcel?month=' + month + "&Monthname=" + Monthname +  '&PGroup=' + payroll + '&PGname=' + $('#PayrollGroup option:selected').text()+
            "&totRecords=" + tableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function onImportExcelClick() {
    debugger;

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var salaryhold = new FormData($('#ExcelSalaryHoldUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryHold/ImportFromExcel',
                contentType: false,
                processData: false,
                data: salaryhold,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        debugger;
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
                        }
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



