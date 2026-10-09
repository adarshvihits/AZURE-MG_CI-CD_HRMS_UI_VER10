var TypeCode = 0;

function onLeaveGrpChange() {
    debugger;
    $.getJSON("/TimeOffice/AttendanceSetting/GetApplicableLeaves", { id: $('#LeaveGroup').val() }, function (data) {

        $('#ApplicableLeave option').remove();
        //$('#ApplicableLeave').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#ApplicableLeave').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

        $('.multiselectDropdownField').multiselect('rebuild');
    });
}
function onPunchingApplChange(val) {
    debugger


    if (val.value == 1) {
        $(".pun_MethodDiv").removeClass('hide');
    }
    else {
        $(".pun_MethodDiv").addClass('hide');
       /* AttendanceSettings_PunchingMethodIDs.length = 0;*/

    }

}
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var classification = $('#Classification').val();
    var groupby = $('#GroupBy').val();
    var Type = $('input[name="Type"]:checked').val();

    if (Type == 'NotAssigned') TypeCode = 0;
    else TypeCode = 1;


    var dataTable = $("#AttendanceSettingsTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/AttendanceSetting/GetAttendanceList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&groupbyCode=" + groupby + "&ClassificationCode=" + classification + "&TypeCode=" + TypeCode+ '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#AttendanceSettingList_Count').val(d.recordsTotal);
                console.log(d);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger;
                    d.data[i].department = '<a data-bs-toggle="tooltip" title="Edit"id = "empattendancesettings" type ="button" onclick="onEditClick(' + "'" + d.data[i].employeeGuid + "'" + ')"  class="btn btn-icon btn-edit icon-only slider-btn"><i class="icon-xl fas fa-pencil-alt"></i></a>'
                       
                    if (d.data[i].coffapplicabilty != true) {
                        d.data[i].coffapplicabilty = '<a type = "button" class="btn btn-icon btn-close" > <i class="icon-xl fas fa-times"></i></a>'
                                  
                    }
                    else {
                        d.data[i].coffapplicabilty = '<a type = "button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></a>'
                                   
                    }

                    if (d.data[i].lesshour != true) {
                        d.data[i].lesshour = '<a type = "button"  class="btn btn-icon btn-close"><i class="icon-xl fas fa-times"></i></a >'

                    }
                    else {
                        d.data[i].lesshour = '<a type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></a>'

                    }
                    if (d.data[i].singlepunchapplicabilty != true) {
                        d.data[i].singlepunchapplicabilty = '<a type = "button"  class="btn btn-icon btn-close"><i class="icon-xl fas fa-times"></i></a >'

                    }
                    else {
                        d.data[i].singlepunchapplicabilty = '<a type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></a>'

                    }
                    if (d.data[i].nightoffapplicabilty != true) {
                        d.data[i].nightoffapplicabilty = '<a type = "button"  class="btn btn-icon btn-close"><i class="icon-xl fas fa-times"></i></a >'

                    }
                    else {
                        d.data[i].nightoffapplicabilty = '<a type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></a>'

                    }
                    if (d.data[i].odar != true) {
                        d.data[i].odar = '<a type = "button"  class="btn btn-icon btn-close"><i class="icon-xl fas fa-times"></i></a >'

                    }
                    else {
                        d.data[i].odar = '<a type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></a>'

                    }

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "empId"},
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "punchingid" },
            { "data": "punchingmethod" },
            { "data": "shifttype" },
            { "data": "holidaygroup" },
            { "data": "offday"},
            { "data": "leavegroup" },
            { "data": "coffapplicabilty" },
            { "data": "lesshour" },
            { "data": "singlepunchapplicabilty" },
            { "data": "nightoffapplicabilty" },
            { "data": "odar" },
            { "data": "department" },

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




function getList(sender) {
    debugger;  
    var classification = $('#Classification').val();
    var grooupby = $('#GroupBy').val();

    var Type = $('input[name="Type"]:checked').val();

    if (Type == 'Not Assigned') TypeCode = 0;
    else TypeCode = 1;
   
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceSetting/GetList',
        dataType: 'html',
        data: { groupbyCode: grooupby, ClassificationCode: classification, TypeCode: TypeCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AttendanceSettingslist').html("");
                $('#AttendanceSettingslist').html(result);
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

function onEditClick(GI) {
    debugger;


    $.ajax({

        type: 'GET',
        url: '/TimeOffice/AttendanceSetting/Edit',
        dataType: 'html',
        data: { EmpGI: GI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                 $('body').append('<div class="form-overlay"></div>');
                $('#EditSliderPartial').html("");
                $('#EditSliderPartial').html(result);
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

function onGroupByChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == 5 || cid == 6 || cid == 8 || cid == 9 ) {
        $('#Classification option').remove();
        $('#Classification').append('<option value="0">All</option');
        $('#Classification').append('<option value="1">Yes</option');
        $('#Classification').append('<option value="2">No</option');
    }
    else {
    $.getJSON("/TimeOffice/AttendanceSetting/GetDropDown", { id: cid }, function (data) {
        $('#Classification option').remove();
        $('#Classification').append('<option value="0">All</option');

        for (var i = 0; i < data.length; i++) {
            $('#Classification').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    }
    fillData();
}

function onShiftTypeChange(sender) {
    debugger;
    var value = $(sender).val();
    if (value == 1) {

        $('#singleselect').show();
        $('#multipleselect').hide();
        $("#sselect").prop('selectedIndex', 0);
    }
    else if (value == 2) {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettingShtSelection").val('');
        $("#sselect").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
        $('#AttendanceSettingShtSelection').multiselect('refresh');
    }
    else {
        $('#singleselect').hide();
        $('#multipleselect').show();
        $("#AttendanceSettingShtSelection").val('');
        $("#sselect").val('');
        $('#sselect').removeClass('is-invalid');
        $('#shiftdetails').removeClass('error-message');
        $('#shiftdetails').html("");
        $('#AttendanceSettingShtSelection').multiselect('refresh');
    }
   
}
function validateAttendSettings()
{
    debugger
    var i = 0;
    if (!validateById('#AttendanceSettingDetails_PUNCHINGID', 'attsett_PUNCHINGID'))
        i++;
    if (!validateById('#PunchingApplicability', 'punchingAppl')) {
        i++;
    }
    if ($("#PunchingApplicability").val() == 1) {
        if (!validateMultiselect('AttendanceSettingPunchMethods', 'attsett_punchMethods', 'punchdiv'))
            i++;
    }

    if (!validateById('#ShiftType', 'attsett_ShiftType')) 
        i++;

    if (validateById('#ShiftType', 'attsett_ShiftType'))
    {
        debugger
        if ($("#ShiftType").val() !=1) {
            if (!validateMultiselect('AttendanceSettingShtSelection', 'attsett_shiftmulti', 'shiftmultidiv'))
                i++;
        }
            else {
                if (!validateById('#ShiftSelect', 'attsett_Shiftsingle'))
                    i++;
        }
    }

   
    //if (!validateById('#Circle', 'attsett_Circle'))
    //    i++;

    if (!validateById('#HolidayGroup', 'attsett_HolidayGroup'))
        i++;
    //if (!validateById('#Offday', 'attsett_LeaveGroup'))
    //    i++;

    //if (!validateMultiselect('ApplicableLeave', 'attsett_appleaves', 'appldiv'))
    //    i++;
    if (i == 0)
        return true;
    else {
        return false;
    }
}
function EditSave() {
    debugger

    if (validateAttendSettings()) {
        var GroupBy = $('#GroupBy').val();
        var Classification = $('#Classification').val();
        debugger
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
                    url: '/TimeOffice/AttendanceSetting/AttendanceSettingSave',
                    dataType: 'json',
                    data: $('#AttendaceSettSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('closeEdit');
                                fillData();

                            });

                        }
                        else {
                            swal.fire(result.message);
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
}


function ApplyFilter(mode) {
    debugger
    var GroupBy = $('#GroupBy').val();
    var Classification = $('#Classification').val();
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
   
    }
}

function ExportToExcel() {
    debugger;
    var GroupBy = $('#GroupBy').val();
    var Classification = $('#Classification').val();
    var groupByName = $('#GroupBy option:selected').text();
    var classificationnName = $('#Classification option:selected').text();

    var Type = $('input[name="Type"]:checked').val();

    if (Type == 'NotAssigned') TypeCode = 0;
    else TypeCode = 1;

    //if (!($('#AttendanceSettingsTable').length))
    //    swal.fire("No data found to export");
    var TableLength = $('#AttendanceSettingList_Count').val()
    if (TableLength > 0) {
        url = '/TimeOffice/AttendanceSetting/ExportToExcel?GroupByName=' + groupByName + '&ClassificationName=' + classificationnName + '&GroupByCode=' + GroupBy + '&Classification=' + Classification + '&TypeCode='+ TypeCode +'&TotalRows=' + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}