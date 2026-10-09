
function POPUP(GI) {
    var yearCode = $("#Year_Code").val();
    var ShiftTypeCode = $("#ShiftType_Code").val();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/EmployeeWiseOffdaySettings/GetEmployeeCombinationPopUp',
        dataType: 'html',
        data: { GI: GI, YearCode: yearCode, ShiftType: ShiftTypeCode},

        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var yearCode = $('#Year_Code').val();
    var ShiftTypeCode = $("#ShiftType_Code").val();
    var temp = filter1;


    /* if (Allowance != '') {*/
    var dataTable = $("#EmployeeOffDayTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/EmployeeWiseOffdaySettings/GetEmployeeWiseList",
            "type": "POST",

            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&year=' + yearCode + '&ShiftType=' + ShiftTypeCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#List_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    if (d.data[i].effectiveFrom!="")
                        d.data[i].effectiveFrom = _GetFormattedDate(d.data[i].effectiveFrom);
                    //d.data[i].order = '<a data-bs-toggle="tooltip" title="Edit" id="AppraisalView" type = "button" onclick = "onEditClick('+"'"+ d.data[i].employee.employeeGI +"'"+')" class="btn btn-icon btn-edit icon-only slider-btn" ><i class="icon-xl fas fa-pencil-alt"></i></a>'
                    d.data[i].des = '<div class="d-flex justify-content-center"><a data-bs-toggle="tooltip" title = "Edit" id = "physicallocationedit" class="btn btn-icon btn-edit icon-only slider-btn" onclick = "onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')" > <i class="icon-xl fas fa-pencil-alt"></i></a >'
                    d.data[i].fullName = '<a data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="POPUP(' + "'" + d.data[i].employeeGI + "'" + ')">'+d.data[i].fullName+'</a ></div>'
                    
                                
                }

                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "fullName", "width": "17%" },
            { "data": "designation", "width": "13%" },
            { "data": "department", "width": "13%" },
            { "data": "location", "width": "10%" },
            { "data": "previousCombination", "width": "12%" },
            { "data": "newCombination", "width": "12%" },
            { "data": "effectiveFrom", "width": "10%" },
            { "data": "des", "width": "5%" },
            ],

       


        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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

    setTimeout(datepicker, 500);

}


function datepicker() {
    $('.kt_datepicker_8, .kt_datepicker_8').datepicker({
      
    });

}



function batchupdateClick() {
    debugger;
    var yearCode = $('#Year_Code').val();
    var ShiftTypeCode = $("#ShiftType_Code").val();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/EmployeeWiseOffdaySettings/BatchUpdate',
        dataType: 'html',
        data: { YearCode: yearCode, ShiftType: ShiftTypeCode},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#batchupdateSlider').html("");
                $('#batchupdateSlider').html(result);
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

function employeeEditSave() {
    var i = 0;
    if (!validateById('#NewCombination_Code', 'newcombination')) {
        i++;
    }
    if (!validateById('#EffectiveFrom', 'effectivefrom')) {
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
            //    // Save it
            $.ajax({

                type: 'POST',
                url: "/TimeOffice/EmployeeWiseOffdaySettings/EmployeeWiseEdit",
                dataType: 'json',
                data: $('#EmployeeWiseEdit').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Modified Successfully").then(function () {
                            var url="/TimeOffice/EmployeeWiseOffdaySettings/Index";
                            window.location.href = url;

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
function batchUpdateSave() {
    var i = 0;
    //if (!validateById('#PreviousCombination_Code', 'prevcombination')) {
    //    i++;
    //}
    if (!validateById('#NewCombination_Code', 'newcombination')) {
        i++;
    }
    if (!validateById('#EffectiveFrom', 'effectivefrom')) {
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
            //    // Save it
            $.ajax({

                type: 'POST',
                url: "/TimeOffice/EmployeeWiseOffdaySettings/BatchUpdateSave",
                dataType: 'json',
                data: $('#offdayBatchUpdateSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Updated Successfully").then(function () {
                            var url = "/TimeOffice/EmployeeWiseOffdaySettings/Index";
                            window.location.href = url;

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
function onYearDropdownChange(sender) {
    showSpinner();
    //var url = "/TimeOffice/EmployeeWiseOffdaySettings/GetList";
    //$('#OffdaySettingsListPartial').load(url, { YearCode: $(sender).val() }, function () { hideSpinner(); });
   var YearCode = $(sender).val();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/EmployeeWiseOffdaySettings/GetList',
        dataType: 'html',
        data: { YearCode: YearCode},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#OffdaySettingsListPartial').html("");
                $('#OffdaySettingsListPartial').html(result);
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

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())

    if (mode == 'apply') {
        var yearCode = $('#Year_Code').val();
    
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
       
    }
}


function onEditClick(GI) {
    var yearCode = $('#Year_Code').val();

    $.ajax({

        type: 'GET',
        url: "/TimeOffice/EmployeeWiseOffdaySettings/Edit",
        dataType: 'html',
        data: { EmpGI: GI, YearCode: yearCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editSlider').html("");
                $('#editSlider').html(result);
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
function popUpExportToExcel() {
    debugger;
    var EmpGI = $('#EmployeeGI').val();
    var yearCode = $('#Year_Code').val();
    var year = $('#Year_Code option:selected').text();
    var EmpName = $('#EmployeeName').val();;
    var newCombionation = $('#NewCombination_Name').val();;
    if (!($('#EmployeepopupTable').length))
        swal.fire("No data found to export");
    var TableLength = document.getElementById("EmployeepopupTable").rows.length - 1;
    if (TableLength > 0) {
        url = '/TimeOffice/EmployeeWiseOffdaySettings/popUpExportToExcel?EmployeeGI=' + EmpGI + '&YearCode=' + yearCode + '&Year=' + year + '&EmployeeName=' + EmpName + '&NewCombionation=' + newCombionation;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcel() {
    debugger;
    var yearCode = $('#Year_Code').val();
    var ShiftTypeCode = $("#ShiftType_Code").val();

    var year = $('#Year_Code option:selected').text();
    var ShiftType = $("#ShiftType_Code option:selected").text();
    //if (!($('#EmployeeOffDayTable').length))
    //    swal.fire("No data found to export");
    //var TableLength = document.getElementById("EmployeeOffDayTable").rows.length - 1;

    var TableLength = $('#List_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/EmployeeWiseOffdaySettings/ExportToExcel?YearCode=' + yearCode + '&Year=' + year + '&ShiftTypeCode=' + ShiftTypeCode + '&ShiftType=' + ShiftType + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
