
function GetEncashHeading() {
    debugger;
    var EntryType = $('#EntryType').val();
   
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveEncash/GetEncashHeading',
        dataType: 'html',
        data: { Type: EntryType },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#leaveEncashList').html("");
                $('#leaveEncashList').html(result);
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
    var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    var Month = $('#Month_Code').val();

    var EntryType = $('#EntryType').val();
    if (EntryType == 1) {
        var dataTable = $("#encash_createTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveEncash/GetLeaveEncashList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod + '&LeaveGroup=' +
                            LeaveGroup + '&Leave=' + Leave + '&Month=' + Month + "&Type=1" +"&Form=Entry"+ '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    $('#LeaveEncashmentList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }

                    return d.data;
                    console.log(d.data);
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "designation.name" },
                { "data": "department.name" },
                { "data": "location.name" },
                { "data": "balance" },
                { "data": "maxLimit" },
                { "data": "value" },
             

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

    }

    else {
        var dataTable = $("#encash_editTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveEncash/GetLeaveEncashList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod + '&LeaveGroup=' +
                            LeaveGroup + '&Leave=' + Leave + '&Month=' + Month + "&Type=2"+"&Form=Entry" + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    $('#LeaveEncashmentList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].category = '<div style="display:flex; justify-content:center;"><a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Edit" id = "physicallocationedit" type = "button"class="btn btn-icon btn-edit icon-only slider-btn" onclick = "onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')" > <i class="icon-xl fas fa-pencil-alt"></i></a ></div>'

                    }
                    console.log(d.data);
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "designation.name" },
                { "data": "department.name" },
                { "data": "location.name" },
                { "data": "balance" },
                { "data": "maxLimit" },
                { "data": "value" },
                { "data": "category" },


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
    }

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

    $.fn.DataTable.ext.pager.numbers_length = 5;

    setTimeout(tooltipHover, 500);






}



function fillLeaveCombo(sender) {
    debugger;

    $.getJSON("/TimeOffice/LeaveEncash/fillLeaveCombo", { LGroup: $(sender).val() }, function (data) {

        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Leave--</option');

        for (var i = 0; i < data.length; i++) {

            $('#Leave_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });

}




function validateDropdowns()
{
  
    var i = 0;
    if (!validateById('#LeavePeriod_Code', 'levPeriod')) {
        i++;
    }
    if (!validateById('#LeaveGroup_Code', 'leavGroup')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'leav')) {
        i++;
    }
    if (!validateById('#Month_Code', 'MonthID')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;

}




function BatchUpdate() {
    debugger;

        //swal.fire({
        //    text: "Verify the entered details are correct. Once saved it cannot be recalled'. Do you want to save?",
        //    icon: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'Ok'
        //}).then((result) => {
        swal.fire({
            text: "Do you want to Save?",
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
                    url: '/TimeOffice/LeaveEncash/BatchUpdate',
                    dataType: 'json',
                    data: $('#encash_Index_form').serialize() + $('#BasicFilter').serialize(),
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                debugger
                                fillData();




                            });

                        }
                        else
                            swal.fire(result.message);



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



function onEditClick(EmployeeGI) {
    debugger;
    preventENterFlag = 0;

    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveEncash/GetEditDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LeavePeriod: $('#LeavePeriod_Code').val(), LeaveGroup: $('#LeaveGroup_Code').val(), Leave: $('#Leave_Code').val(), Month: $('#Month_Code').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#leaveEncashEdit').html("");
                $('#leaveEncashEdit').html(result);
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



function EmployeeWiseLeaveEncashModify()
{
    debugger;
    if (validateById("#LeaveEncashmentEditDetils_EnCashment", 'edit_encash_amount')) {

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
                    url: '/TimeOffice/LeaveEncash/Edit',
                    dataType: 'json',
                    data: $('#LeaveEncashEdit').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                fillData();
                                //var url = "/TimeOffice/LeaveEncash/GetList";
                                //$('#leaveEncashList').load(url, { LeavePeriod: $('#LeavePeriod_Code').val(), LeaveGroup: $('#LeaveGroup_Code').val(), Leave: $('#Leave_Code').val(), Month: $('#Month_Code').val() }, function () { });
                                CloseSlider('EditLeaveEncashment');
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

        var TableLength = $('#LeaveEncashmentList_Count').val();
        if (TableLength > 0) {
            url = "/TimeOffice/LeaveEncash/ExportToExcel?LeavePeriod=" + $('#LeavePeriod_Code').val() + "&LeaveGroup=" + $('#LeaveGroup_Code').val() + "&Leave=" + $('#Leave_Code').val() + "&Month=" + $('#Month_Code').val() + "&LPName=" + $('#LeavePeriod_Code option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&Mname=" + $('#Month_Code option:selected').text() + "&totRecords=" + TableLength;
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
   
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
       /* showSpinner();*/
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
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        fillData();
       
        $('#filterForm').removeClass('filter-apply');
    }
}

function onLeavePeriodChange(sender, spanId) {
    var payroll = 0;
    var code = $(sender).val();
    if (code == 0) {
        $("#" + spanId + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + spanId + "").html("This field is required");
    }
    else {
        $("#" + spanId + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + spanId + "").html("");
        $.getJSON("/Custom/CustomLeaveEncash/GetMonth", { PayrollCode:payroll, LeavePeriod: code }, function (data) {
            console.log(data);
            $('#Month_Code option').remove();
            $('#Month_Code').append('<option value>Select Month</option');
            debugger;
            for (var i = 0; i < data.length; i++) {
                $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
    }
}