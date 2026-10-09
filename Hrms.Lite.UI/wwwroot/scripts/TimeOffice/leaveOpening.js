var LeavePeriod;
var LeaveGroup;
var Leave;
var LPName;
var LGName;
var LName;


function GetOpeningHeading() {
    debugger;
    var EntryType = $('#EntryType').val();
    if (EntryType == 2) {
        $(".batchleave").addClass('hide');
        $("#importbtn").addClass('hide');
    }   
    else {
        $(".batchleave").removeClass('hide');
        $("#importbtn").removeClass('hide');
    }
        
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveOpening/GetOpeningHeading',
        dataType: 'html',
        data: { Type: EntryType },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#leaveOpeningList').html("");
                $('#leaveOpeningList').html(result);
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
    var EntryType = $('#EntryType').val();

    if (EntryType == 1) {
        var dataTable = $("#leaveOpCreateTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveOpening/GetLeaveOpeningList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod +
                            '&LeaveGroup=' + LeaveGroup + '&Leave=' + Leave + "&EntryTYpe=" + EntryType+ "&Form=Entry" + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#LeaveOpeningList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation.name", "width": "17%" },
                { "data": "department.name", "width": "17%" },
                { "data": "location.name", "width": "13%" },
                { "data": "doj", "width": "9%" },
                { "data": "opening", "width": "9%" }
              


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
    if (EntryType == 2 || EntryType=="")
    {
        var dataTable = $("#leaveOpEditTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/LeaveOpening/GetLeaveOpeningList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod +
                            '&LeaveGroup=' + LeaveGroup + '&Leave=' + Leave + "&EntryTYpe=" + EntryType + "&Form=Entry" + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#LeaveOpeningList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].leave = '<a data-bs-toggle="tooltip" title="Edit" id="physicallocationedit" type="button" class="btn btn-icon btn-edit icon-only slider-btn" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"><i class="icon-xl fas fa-pencil-alt"></i></a>'
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation.name", "width": "17%" },
                { "data": "department.name", "width": "17%" },
                { "data": "location.name", "width": "13%" },
                { "data": "doj", "width": "9%" },
                { "data": "opening", "width": "9%" },
                { "data": "leave", "width": "6%" },


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




function validateLeaveOpeningDropdowns() {
    var i = 0;
    if (!validateById('#LeavePeriod_Code', 'MlevPeriod') && !validateById('.Lperiod', 'levPeriod'))
        i++;
    if (!validateById('#LeaveGroup_Code', 'MlevGroup') && !validateById('.Lgroup', 'levGroup'))
        i++;
    if (!validateById('#Leave_Code', 'Mlev') && !validateById('.Ltype', 'lev'))
        i++;

    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
        return true;
    }    
    else {
        return false;
    }
}


function validateDropdowns() {
    var i = 0;
    if (!validateById('#LeavePeriod_Code', 'MlevPeriod') && !validateById('.Lperiod', 'levPeriod'))
        i++;
    if (!validateById('#LeaveGroup_Code', 'MlevGroup') && !validateById('.Lgroup', 'levGroup'))
        i++;
    if (!validateById('#Leave_Code', 'Mlev') && !validateById('.Ltype', 'lev')) 
        i++;

    if (i == 0)
        return true;
    else {
        return false;
    }
}



function onEditClick(EmployeeGI) {

    var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();


    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveOpening/GetEditDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LeavePeriod: LeavePeriod, LeaveGroup: LeaveGroup, Leave: Leave },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                 $('body').append('<div class="form-overlay"></div>');
                $('#editLeaveOpening').html("");
                $('#editLeaveOpening').html(result);
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
    if (validateDropdowns()) {
        debugger
  

        var LeavePeriod = $('#LeavePeriod_Code').val();
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var Leave = $('#Leave_Code').val();
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/LeaveOpening/GetBatchUpdateDetails',
            dataType: 'html',
            data: { LeavePeriod: LeavePeriod, LeaveGroup: LeaveGroup, Leave: Leave },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                     $('body').append('<div class="form-overlay"></div>');
                    $('#batchUpdatePartial').html("");
                    $('#batchUpdatePartial').html(result);
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
}


function LeaveOpeningSave() {
    var i = 0;
    if (!validateById('#LeaveOpeningBatchUpDate_Opening', 'btchOpening')) {
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
                    url: '/TimeOffice/LeaveOpening/LeaveOpeningSave',
                    dataType: 'json',
                    data: $('#BatchUpdate1').serialize() +$('#BasicFilter').serialize(),
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
    else
        return false;

}


function EmployeeWiseLeaveOpeningSave() {
    var i = 0;
    if (!validateById('#LeaveOpeningEdit_OpeningEdit', 'opening')) {
        i++;
    }


    if (i == 0) {
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
                    url: '/TimeOffice/LeaveOpening/Edit',
                    dataType: 'json',
                    data: $('#EmployeeWiseLeaveOpeningSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {                 

                                fillData();
                                CloseSlider('leaveOpeningEdit');
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
            var MPPersonalInfo = new FormData($('#LeaveOpeningUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LeaveOpening/ImportFromExcel',
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
function ExportToExcel() {
    debugger;    
    if (validateDropdowns()) {

        var LeavePeriod = $('#LeavePeriod_Code').val();
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var Leave = $('#Leave_Code').val();
        var EntryType = $('#EntryType').val();
        var tableLength = $('#LeaveOpeningList_Count').val();

        if (tableLength > 0) {
            url = '/TimeOffice/LeaveOpening/ExportToExcel?LeavePeriod=' + LeavePeriod + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&EntryTYpe=" + EntryType + "&LPName=" +
                $('#LeavePeriod_Code option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() +
                "&LName=" + $('#Leave_Code option:selected').text() + "&totRecords=" + tableLength;
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
            $('#filterForm').removeClass('filter-apply');}
        else {
          
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');}
        
    }
    else {
     
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
     
    }
}

