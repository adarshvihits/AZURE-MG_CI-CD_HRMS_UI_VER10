var pagenumber = 0;
function validatedata(index) {
    if (parseFloat($('#HRAbsentRegularizationList_' + index + '__LeaveBalance').val()) == 0) {
        $('#HRAbsentRegularizationList_' + index + '__Applicable').prop('checked', false);

        swal.fire('No Leave Balance');
        return false;
    }
    
    if (parseFloat($('#HRAbsentRegularizationList_' + index + '__LeaveBalance').val()) < parseFloat($('#HRAbsentRegularizationList_' + index + '__AbscentCount').val())) 
    {
        $('#HRAbsentRegularizationList_' + index + '__Applicable').prop('checked', false);
            swal.fire('No Sufficient Balance.Please Regularize Datewisely');
            return false;
    }
    return true;
}
function onCheckboxChange(sender, index) {
    if (validatedata(index)) {
        debugger;
        checked = $(sender).is(':checked');

        if (checked) {
            var row = $(sender).closest('tr').html();
            var empcode = $('#HRAbsentRegularizationList_' + index + '__EmpCode').val();
            var abs = $('#HRAbsentRegularizationList_' + index + '__AbscentCount').val();

            $('#hrAbscentHiidenTable tbody').append('<tr id="otSave_' + pagenumber + '_' + index + '">' + row + '</tr>');

            $('#HRAbsentRegularizationList_' + index + '__EmpCode').val(empcode);
            $('#HRAbsentRegularizationList_' + index + '__AbscentCount').val(abs);
            $('#HRAbsentRegularizationList_' + index + '__Applicable').attr('checked', true);

            var checkedCount = $('#hrabscentRegTable').find('input[type=checkbox]:checked').length;
            var totalRows = $('#hrabscentRegTable tbody tr').length;

            if (totalRows == checkedCount) {
                $('#SelectAll').attr('checked', true)
            }

        }
        else {

            $('#otSave_' + pagenumber + '_' + index).remove();
            $('.selectAllChkBx').html('')
            $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

        }

        $('#hrAbscentHiidenTable tbody').find("tr").each(function (currindex) {
            debugger;
            // if (currindex > 0) {
            $(this).find("td").eq(2).html(currindex);
            debugger
            var tdd = $(this).find("td").eq(2);
            tdd.prevObject[0].getElementsByTagName('input')[0].id = 'HRAbsentRegularizationList_' + currindex + '__EmpCode';
            tdd.prevObject[0].getElementsByTagName('input')[0].name = 'HRAbsentRegularizationList[' + currindex + '].EmpCode';

            tdd.prevObject[5].getElementsByTagName('input')[0].id = 'HRAbsentRegularizationList_' + currindex + '__AbscentCount';
            tdd.prevObject[5].getElementsByTagName('input')[0].name = 'HRAbsentRegularizationList[' + currindex + '].AbscentCount';
            tdd.prevObject[7].getElementsByTagName('input')[0].id = 'HRAbsentRegularizationList_' + currindex + '__Applicable';
            tdd.prevObject[7].getElementsByTagName('input')[0].name = 'HRAbsentRegularizationList[' + currindex + '].Applicable';


            // }
        });
    }
}
function fillData() {
    var filter1 = $('#BasicFilter').serialize();
    debugger
    var temp = filter1;
    var search = $('#SearchId').val();
    var PGroup = $("#PGroup_Code").val();
    var Month = $('#Month_Code').val();
    var Leave = $('#Leave_Code').val();
    
    var dataTable = $("#hrabscentRegTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/HRAbsentRegularization/GetList",
                "type": "POST",
                "datatype": "json",

                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();              

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&Pgroup=' + PGroup + '&Month=' + Month + '&Leave=' + Leave + '&SearchId=' + search + '&dummy=dummy';
                        d.filter = temp;
                        d._model = $('#HR_Atten_Approval_Form').serialize();

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#HRAbsentRegularizationList_Count').val(d.recordsTotal);
                    //console.log(d.data);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].empId = d.data[i].empId + '<input type="hidden" id="HRAbsentRegularizationList_' + i + '__EmpCode' +
                            '" name="HRAbsentRegularizationList_[' + i + '].EmpCode" value="' + d.data[i].empCode + '">'
                       

                        d.data[i].empName = '<a data-bs-toggle="modal" data-target=".hrAbscentPopup"  onclick="onEmpClick(' + d.data[i].empCode + ',' + "'" + d.data[i].empGI + "'" + ',' + d.data[i].leaveBalance  + ')">' + d.data[i].empName + '</a>';
                        d.data[i].abscentCount = d.data[i].abscentCount + '<input type="hidden" id="HRAbsentRegularizationList_' + i + '__AbscentCount' +
                            '" name="HRAbsentRegularizationList_[' + i + '].AbscentCount" value="' + d.data[i].abscentCount + '">'
                        d.data[i].leaveBalance = d.data[i].leaveBalance + '<input type="hidden" id="HRAbsentRegularizationList_' + i + '__LeaveBalance' +
                            '" name="HRAbsentRegularizationList_[' + i + '].LeaveBalance" value="' + d.data[i].leaveBalance + '">'
                        if (d.data[i].applicable != false) {
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="HRAbsentRegularizationList_' + i + '__Applicable" name="HRAttendanceApprovalList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                        }
                        else
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="HRAbsentRegularizationList_' + i + '__Applicable" name="HRAttendanceApprovalList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'



                    }
                  /*  const myTimeout = setTimeout(ChkBxChecking, 100);*/
                    return d.data;
                }

            },

            "columns": [

                { "data": "empId", "width": "17%" },
                { "data": "empName", "width": "17%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "12%" },               
                { "data": "location", "width": "12%" },
                { "data": "abscentCount", "width": "10%" },
                { "data": "leaveBalance", "width": "13%" },            
                { "data": "applicable", "width": "6%" }

            ],


            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "ordering": false,
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

    $('#hrabscentRegTable').on('page.dt', function () {
            debugger
            var info = dataTable.page.info();
            pagenumber = info.page + 1;

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
        /*    dataTable.search('').draw();*/
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

function onEmpClick(EmpCode, EmpGI, balance) {
    var PGroup = $("#PGroup_Code").val();
    var Month = $('#Month_Code').val();
    var Leave = $('#Leave_Code').val();
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/HRAbsentRegularization/EmployeeWiseABDetails',
        dataType: 'html',
        data: { EmpCode: EmpCode, EmpGI: EmpGI, PGroup: PGroup, Month: Month, Leave: Leave, Balance: balance },
        success: function (Data) {
            debugger
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.hrAbscentPopup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);               
                m.modal('show');
            }
            
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function ApplyFilter(mode) {
    debugger


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
function fillMonth(sender) {
    debugger;
    $.getJSON("/TimeOffice/MonthlyAttendanceProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function regularize() {

    debugger;
    swal.fire({
        text: "Do you want to Regularize?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/HRAbsentRegularization/Regularize',
                dataType: 'json',
                data: $('#HR_Abscent_Form').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire("Updated Successfully").then(function () {                           
                            fillData();
                        });

                    }
                    else
                        swal.fire(result.message);
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
function empRegularize() {

    debugger;
    swal.fire({
        text: "Do you want to Regularize?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/HRAbsentRegularization/EmployeeWiseRegularize',
                dataType: 'json',
                data: $('#HR_AbscentEmp_Form').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire("Updated Successfully").then(function () {
                            close_modal_popup('hrAbscentPopup');
                            fillData();
                        });

                    }
                    else
                        swal.fire(result.message);
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


function ExportToExcel() {
    debugger;

    var filter = $('#BasicFilter').serialize();
    var PCode = $("#PGroup_Code").val();
    var MCode = $('#Month_Code').val();
    var LCode = $('#Leave_Code').val();
  
    var PName = $("#PGroup_Code option:selected").text();
    var MName = $("#Month_Code option:selected").text();
    var LName = $("#Leave_Code option:selected").text();

    var TableLength = $('#HRAbsentRegularizationList_Count').val();
    var PARAM = 'data=' + filter + '&PCode=' + PCode + '&MCode=' + MCode + '&MCode=' + MCode + '&LCode=' + LCode +
        '&PName=' + PName +'&MName=' + MName + '&LName=' + LName ;
    if (TableLength > 0) {
        url = '/TimeOffice/HRAbsentRegularization/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}