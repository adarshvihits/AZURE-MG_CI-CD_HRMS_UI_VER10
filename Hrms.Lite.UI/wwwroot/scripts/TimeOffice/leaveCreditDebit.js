var LeavePeriod;
var Month;
var Leave;
var DebitCredit;


function  validateDrop()
      {
        var i = 0;
    if (!validateById('#LeavePeriodDropdown', 'leaveperiod')) {
            i++;
        }
        if (!validateById('#Month_Code', 'month')) {
            i++;
        }
        if (!validateById('#Leave_Code', 'leave')) {
            i++;
    }
    if (!validateById('#LeaveGroup_Code', 'group')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'leave')) {
        i++;
    }
        if (!validateById('#DebitCredit_Code', 'debitcredit')) {
            i++;
        }
        if (i == 0) 
            return true
            else
            return false;
}
function fillData() {
    debugger;
   
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;


    /*  LeavePeriod = $('#LeavePeriod_Code').val();*/
    var LeavePeriod = $('#LeavePeriodDropdown').val();
        Month = $('#Month_Code').val();
    group = $('#LeaveGroup_Code').val();
        Leave = $('#Leave_Code').val();
        DebitCredit = $("#DebitCredit_Code").val();



        var dataTable = $("#leaveCreditdebitTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveCreditDebit/GetLeaveCreditDebitList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';


                        debugger;

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod +
                            '&Month=' + Month + '&Leave=' + Leave + '&DebitCredit=' + DebitCredit + '&LeaveGroup=' + group +"&Form=Entry"+ '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {






                    $('#LeaveCreditDebitList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                        d.data[i].category = '<a data-bs-toggle="tooltip" title="Edit" id="physicallocationedit" type="button" class="btn btn-icon btn-edit icon-only slider-btn" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"><i class="icon-xl fas fa-pencil-alt"></i></a>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation.name", "width": "17%" },
                { "data": "department.name", "width": "17%" },
                { "data": "location.name", "width": "12%" },
                { "data": "balance", "width": "7%" },
                { "data": "appliedLeave", "width": "7%" },
                { "data": "category", "width": "8%" },


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


function fillLeaveMonth() {
    debugger;
    var selectedValue = $('#LeavePeriodDropdown').val();


    $.getJSON("/TimeOffice/LeaveCreditDebit/GetApplicableLeavesPeriod", { LeavePeriod: selectedValue }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month--</option');
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}




function sliderChange() {
    preventENterFlag = 0;
    debugger;
    if(validateDrop()) {
        /* LeavePeriod = $('#LeavePeriod_Code').val();*/
        var LeavePeriod = $('#LeavePeriodDropdown').val();
        Month = $('#Month_Code').val();
        Leave = $('#Leave_Code').val();
        DebitCredit = $("#DebitCredit_Code").val();
        debugger;
        group = $('#LeaveGroup_Code').val();
        if (DebitCredit == 1) {
           
            //var url = "/TimeOffice/LeaveCreditDebit/GetBatchUpdateDetails";
            //$('#bupdate1').load(url, { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, Type: DebitCredit }, function () { });


            $.ajax({

                type: 'GET',
                url: '/TimeOffice/LeaveCreditDebit/GetBatchUpdateDetails',
                dataType: 'html',
                data: { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, Type: DebitCredit, LeaveGroup: group},

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#bupdate1').html("");
                        $('#bupdate1').html(result);
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
            CloseSlider('batchUpdateSlider2');
        }
        else {
            //$('body').append('<div class="form-overlay"></div>');
            //var url = "/TimeOffice/LeaveCreditDebit/GetBatchUpdateDetails";
            //$('#bupdate1').load(url, { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, Type: DebitCredit }, function () { });

            $.ajax({

                type: 'GET',
                url: '/TimeOffice/LeaveCreditDebit/GetBatchUpdateDetails',
                dataType: 'html',
                data: { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, Type: DebitCredit, LeaveGroup: group},

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#bupdate1').html("");
                        $('#bupdate1').html(result);
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
            CloseSlider('batchUpdateSlider');
        }
    }
    else {
        return false;
    }
}

function getList() {
    debugger;
    /* LeavePeriod = $('#LeavePeriod_Code').val();*/
    var LeavePeriod = $('#LeavePeriodDropdown').val();
    Month = $('#Month_Code').val();
    Leave = $('#Leave_Code').val();
    DebitCredit = $('#DebitCredit_Code').val();
    group = $('#LeaveGroup_Code').val();
    //if (validateById('#LeavePeriod_Code', 'leaveperiod') && validateById('#Month_Code', 'month') && validateById('#Leave_Code', 'debitcredit') && validateById('#DebitCredit_Code', 'leave')) {
    if (LeavePeriod !== '' && Month !== '' && Leave !== '' && DebitCredit !== '') {
        showSpinner();
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        //var url = "/TimeOffice/LeaveCreditDebit/GetList";
        //debugger
        //$('#LeaveCreditDebitList').load(url, { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, DebitCredit: DebitCredit }, function () { hideSpinner(); });

        $.ajax({

            type: 'GET',
            url: '/TimeOffice/LeaveCreditDebit/GetList',
            dataType: 'html',
            data: { LeavePeriod: LeavePeriod, Leave: Leave, Month: Month, DebitCredit: DebitCredit, LeaveGroup: group },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#LeaveCreditDebitList').html("");
                    $('#LeaveCreditDebitList').html(result);
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
    /*}*/
    
}

function onEditClick(EmployeeGI) {
    preventENterFlag = 0;
    //$('body').append('<div class="form-overlay"></div>');
    //var url = "/TimeOffice/LeaveCreditDebit/GetEditDetails";
    //$('#editLeaveCreditDebit').load(url, { EmployeeGI: EmployeeGI, LeavePeriod: $('#LeavePeriod_Code').val(), Leave: Leave = $('#Leave_Code').val(), Month: Month = $('#Month_Code').val(), Type: $('#DebitCredit_Code').val() }, function () { });
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveCreditDebit/GetEditDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LeavePeriod: $('#LeavePeriodDropdown').val(), Leave: Leave = $('#Leave_Code').val(), Month: Month = $('#Month_Code').val(), Type: $('#DebitCredit_Code').val()},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editLeaveCreditDebit').html("");
                $('#editLeaveCreditDebit').html(result);
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

function EmployeeWiseLeaveCreditDebitSave(form) {
    var i = 0;
    if (form == "Credit") {
        span = "credit";
        if (!validateById(".creditval", span))
            i++;
    }
    else {
        span = "debit";
        if (!validateById(".debitval", span))
            i++;
    }
    
    if (i==0) {
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/LeaveCreditDebit/Edit',
                    dataType: 'json',
                    data: $('#editSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                fillData();
                                CloseSlider('leavCreditDebitEdit');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);

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

function DebitSubmit() {
    debugger;

    var i = 0;
    if (!validateById('#LeaveCreditDebitBatchUpDate_Value', 'debitbatch')) {
        i++;
    }
    if (i == 0) {
        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/LeaveCreditDebit/BatchSave',
                    dataType: 'json',
                    data: $('#debitS').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                            
                                fillData();
                                CloseSlider('batchUpdateSlider2');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);

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

function CreditSubmit() {
    debugger;
    var i = 0;
    if (!validateById('#LeaveCreditDebitBatchUpDate_Value', 'creditbatch')) {
        i++;
    }

    if (i == 0) {
        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/LeaveCreditDebit/BatchSave',
                    dataType: 'json',
                    data: $('#creditS').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {                             


                                fillData();
                                CloseSlider('batchUpdateSlider');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);

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

function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {
        debugger;
        var leaveCreditdebitTableLength = $('#LeaveCreditDebitList_Count').val();
        if (leaveCreditdebitTableLength > 0) {
            url = "/TimeOffice/LeaveCreditdebit/ExportToExcel?LeavePeriod=" + $('#LeavePeriodDropdown').val() + "&Leave=" + $('#Leave_Code').val() + "&Month=" + $('#Month_Code').val() + "&DebitCredit=" + $('#DebitCredit_Code').val() + "&LeaveGroup=" + $('#LeaveGroup_Code').val()+ "&LPName=" + $('#LeavePeriod_Code option:selected').text() + "&MName=" + $('#Month_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&DCName=" + $('#DebitCredit_Code option:selected').text() + "&totRecords=" + leaveCreditdebitTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}
function validateCreditDebitDropdowns() {
    var i = 0;
    debugger;
    if (!validateById('#LeavePeriodDropdown', 'leaveperiod')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'leave')) {
        i++;
    }
    if (!validateById('#Month_Code', 'month')) {
        i++;
    }
    if (!validateById('#DebitCredit_Code', 'debitcredit')) {
        i++;
    }
    debugger
    if (i == 0) {
        /*$('#Excel_FileData').trigger('click')*/
        var m = $('.bd-example-modal-lg');
        m.modal('show');

    }

}
function onImportExcelClick() {

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#CreditDebitUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LeaveCreditDebit/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                            //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                            //});
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
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
function validateDropdowns() {
    if (validateById('#LeavePeriodDropdown', 'leaveperiod') && validateById('#Leave_Code', 'leave') && validateById('#Month_Code', 'month') && validateById('#DebitCredit_Code', 'debitcredit')) {
        return true;
    }
    else {
        return false;
    }
}

function ApplyFilter(mode) {
    debugger
    if (validateDrop())
    { 
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
}
