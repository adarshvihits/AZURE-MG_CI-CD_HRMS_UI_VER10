function validateDropdowns() {
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'MlevGroup') && !validateById('.leavegroup', 'levGroup'))
        i++;
    if (!validateById('#Leave_Code', 'Mlev') && !validateById('.leavetype', 'lev'))
        i++;

    if (!validateById('#LeavePeriodFrom_Code', 'MlevPeriodFrom') && !validateById('.from', 'levPeriodFrom'))
        i++;

    if (!validateById('#LeavePeriodTo_Code', 'MlevPeriodTo') && !validateById('.to', 'levPeriodTo'))
        i++;
    if (!validateById('#Month_Code', 'Mmonth') && !validateById('.month', 'month'))
        i++;       
    if ($('#LeavePeriodFrom_Code').val() == $('#LeavePeriodTo_Code').val())
        i++;
    if (i == 0)
        return true;
    else {
        return false;
    }
}

function fillToDropdown(sender) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    $.getJSON("/TimeOffice/LeaveCarryForward/fillLeavePeriodToDropdown", { LeavePeriodFrom: $(sender).val() }, function (data) {

        $("#LeavePeriodTo_Code").val(data[0].code);
        $("#LeavePeriodTo_Name").val(data[0].name);
        $('#LeavePeriodTo_ShortName option').remove();
       
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#LeavePeriodTo_ShortName').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    fillData();
}
function fillLeaveCombo(sender) {
    debugger;
   
    $.getJSON("/TimeOffice/LeaveCarryForward/fillLeaveCombo", { LGroup: $(sender).val() }, function (data) {

        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Leave--</option');

        for (var i = 0; i < data.length; i++) {

            $('#Leave_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });

}





function GetCarryForwardHeading() {
    debugger;
    var EntryType = $('#EntryType').val();
    if (EntryType == 2)
        $(".batchleave").addClass('hide');
    else
        $(".batchleave").removeClass('hide');
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveCarryForward/GetCarryForwardHeading',
        dataType: 'html',
        data: { Type: EntryType },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#CFLIst').html("");
                $('#CFLIst').html(result);
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
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var LeaveGroup = $('.leavegroup').val();
    var Leave = $('.leavetype').val();
    var LeavePeriodFrom = $('.from').val();
    var LeavePeriodTo = $('.to').val();
    var Month = $('.month').val();

    var EntryType = $('#EntryType').val();
    if (EntryType == 1)
    { 
        var dataTable = $("#leaveCFCreateTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveCarryForward/GetLeaveCFList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&LeavePeriodFrom=" +
                        LeavePeriodFrom + "&LeavePeriodTo=" + LeavePeriodTo + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&Month=" + Month + "&EntryType=" + EntryType +  "&Form=Entry" + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#LeaveCarryForwardList_Count').val(d.recordsTotal);            

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "15%" },
            { "data": "department.name", "width": "13%" },
            { "data": "location.name", "width": "10%" },
            { "data": "balance", "width": "8%" },
            { "data": "cfMaxLimit", "width": "10%" },
            { "data": "cf", "width": "8%" },         


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
    if (EntryType == 2 || EntryType == "") {
        var dataTable = $("#leaveCFEditTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveCarryForward/GetLeaveCFList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&LeavePeriodFrom=" +
                        LeavePeriodFrom + "&LeavePeriodTo=" + LeavePeriodTo + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&Month=" + Month + "&EntryType=" + EntryType +  "&Form=Entry" + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#LeaveCarryForwardList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeCode = '<a data-bs-toggle="tooltip" title="Edit"  id = "physicallocationedit" type = "button" class="btn btn-icon btn-edit icon-only" onclick = "onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')" ><i class="icon-xl fas fa-pencil-alt"></i></a >'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "15%" },
            { "data": "department.name", "width": "13%" },
            { "data": "location.name", "width": "10%" },
            { "data": "balance", "width": "8%" },
            { "data": "cfMaxLimit", "width": "10%" },
            { "data": "cf", "width": "8%" },
            { "data": "employeeCode", "width": "6%" },


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




}




function getList()
{

    var LeaveGroup = $('.leavegroup').val();
    var Leave = $('.leavetype').val();
    var LeavePeriodFrom = $('.from').val();
    var LeavePeriodTo = $('.to').val();
    var Month = $('.month').val();



    if (LeaveGroup !== '' && Leave !== '' && LeavePeriodFrom !== '' && LeavePeriodTo !== '' && Month!=='')
    {



        $.ajax({

            type: 'GET',
            url: '/TimeOffice/LeaveCarryForward/GetList',
            dataType: 'html',
            data: { LeaveGroup: LeaveGroup, Leave: Leave, LeavePeriodFrom: LeavePeriodFrom, LeavePeriodTo: LeavePeriodTo, Month: Month  },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#leaveCarryForwardList').html("");
                    $('#leaveCarryForwardList').html(result);
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
    else {

        return false;
    }
}

function onEditClick(EmployeeGI) {
    preventENterFlag = 0;
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveCarryForward/GetEditDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LeaveGroup: $('#LeaveGroup_Code').val(), Leave: $('#Leave_Code').val(), LeavePeriodFrom: $('#LeavePeriodFrom_Code').val(), LeavePeriodTo: $('#LeavePeriodTo_Code').val(), Month: $('#Month_Code').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                 $('body').append('<div class="form-overlay"></div>');
                $('#editCarryForward').html("");
                $('#editCarryForward').html(result);
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

function onBatchUpdateClick() {
    preventENterFlag = 0;
    if ($('#LeaveCarryForwardList_Count').val() == 0) {
        debugger;
        swal.fire("No Data in List");
    }
    else {
        if (validateDropdowns()) {

            $.ajax({

                type: 'GET',
                url: '/TimeOffice/LeaveCarryForward/GetBatchUpdateDetails',
                dataType: 'html',
                data: { LeaveGroup: $('#LeaveGroup_Code').val(), Leave: $('#Leave_Code').val(), LeavePeriodFrom: $('#LeavePeriodFrom_Code').val(), LeavePeriodTo: $('#LeavePeriodTo_Code').val(), Month: $('#Month_Code').val() },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#batchUpdateCarryForward').html("");
                        $('#batchUpdateCarryForward').html(result);
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
        else {
            return false;
        }
    }

}


function BatchUpdate() {

    var i = 0;
    if (!validateById('#LeaveCFDetails_CF', 'cf'))
        i++;

    if (i == 0) {
    
    swal.fire({
        text: "Verify the entered details are correct. Once saved it cannot be recalled'. Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/TimeOffice/LeaveCarryForward/BatchUpdate',
                dataType: 'json',
                data: $('#BatchUpdateSlider').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Updated Successfully").then(function () {
                            fillData();
                            CloseSlider('batchupSlider');
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
   

}

function EmployeeWiseLeaveCarryForwardSave() {
    if (validateById('.LeaveCarryfwd', 'carryfwd')) {
        swal.fire({
            text: "Verify the entered details are correct. Once saved it cannot be recalled'. Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/LeaveCarryForward/Edit',
                    dataType: 'json',
                    data: $('#editCarryForwardSlider').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                debugger
                                fillData();
                                CloseSlider('editLeaveCarryForwardSlider');
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
   

}

function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {
        debugger;
        var leaveCarryForwardTableLength = $('#LeaveCarryForwardList_Count').val();
        if (leaveCarryForwardTableLength > 0) {
            var EntryType = $('#EntryType').val();
            url = "/TimeOffice/LeaveCarryForward/ExportToExcel?LeaveGroup=" + $('#LeaveGroup_Code').val() + "&LeavePeriodFrom=" + $('#LeavePeriodFrom_Code').val() + "&LeavePeriodTo=" +
                $('#LeavePeriodTo_Code').val() + "&Leave=" + $('#Leave_Code').val() + "&EntryTYpe=" + EntryType +
                "&Month=" + $('#Month_Code').val() + "&LPFrom=" + $('#LeavePeriodFrom_Code option:selected').text() + "&LPTo=" + $('#LeavePeriodTo_ShortName option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&MName=" + $('#Month_Code option:selected').text() + "&TotalRow=" + leaveCarryForwardTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}


function ApplyFilter(mode) {
    debugger
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

function LeaveCarryForwardSave() {
    debugger

    var table = $('#leaveCarryForwardTable').DataTable();
    var data = table.$('input,select,textarea').serializeArray();

    var LeaveGroup = $('.leavegroup').val();
    var Leave = $('.leavetype').val();
    var LeavePeriodFrom = $('.from').val();
    var LeavePeriodTo = $('.to').val();
    var Month = $('.month').val();
    var params = "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&LeavePeriodFrom=" + LeavePeriodFrom + "&LeavePeriodTo=" +LeavePeriodTo+ "&Month=" + Month
    if (validateDropdowns()) {

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
                    url: '/TimeOffice/LeaveCarryForward/LeaveCarryForwardSave',
                    dataType: 'json',
                    data: $('#BasicFilter').serialize() + params,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                window.location.href = "/TimeOffice/LeaveCarryForward/Index";


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

}