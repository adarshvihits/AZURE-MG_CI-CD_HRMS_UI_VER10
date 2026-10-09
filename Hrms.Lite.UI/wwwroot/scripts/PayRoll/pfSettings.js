////function onDropdownChangeold() {
////    debugger;
////    month = $('#PayrollMonth_Code').val();
////    deduction = $('#PFDeduction_Code').val();

////    if (month !== '' && deduction !== '') {
////        showSpinner();
////        var url = "/PayRoll/PFSettings/GetEmployeeList";
////        $('#EmployeeList').load(url, { deduction: deduction, monthcode: month }, function () { hideSpinner(); });
////    }
////    else {
////        showSpinner();
////        var url = "/PayRoll/PFSettings/GetEmployeeList";
////        $('#EmployeeList').load(url, { deduction: 0, monthcode: 0 }, function () { hideSpinner(); });
////    }
////    //validate(sender, 'loanTypeSpan');
////    //ApplyFilter('reset');
////}
function CheckGeneralSettingsDeviation() {
    debugger;
    var EPF = parseFloat($('#PFSettingsDetails_EPFPercentage').val());
    var EPS = parseFloat($('#PFSettingsDetails_EPSPercentage').val());
    var ERPF = parseFloat($('#PFSettingsDetails_ERPFPercentage').val());
    var MinLimit = parseFloat($('#PFSettingsDetails_MinLimit').val());
    var EmpMaxLimit = parseFloat($('#PFSettingsDetails_EmpMaxLimit').val());
    var EmployerMaxLimit = parseFloat($('#PFSettingsDetails_EmployerMaxLimit').val());
    var Gen_EPF = parseFloat($('#PFSettingsDetails_Gen_EPF').val());
    var Gen_EPS = parseFloat($('#PFSettingsDetails_Gen_EPS').val());
    var Gen_ERPF = parseFloat($('#PFSettingsDetails_Gen_ERPF').val());
    var Gen_MinLimit = parseFloat($('#PFSettingsDetails_Gen_MinLimit').val());
    var Gen_EmpMaxLimit = parseFloat($('#PFSettingsDetails_Gen_EmpMaxLimit').val());
    var Gen_EmployerMaxLimit = parseFloat($('#PFSettingsDetails_Gen_EmployerMaxLimit').val());
    if (EPF != Gen_EPF)
        return false;
    if (EPS != Gen_EPS)
        return false;
    if (ERPF != Gen_ERPF)
        return false;
    if (MinLimit != Gen_MinLimit)
        return false;
    if (EmpMaxLimit != Gen_EmpMaxLimit)
        return false;
    if (EmployerMaxLimit != Gen_EmployerMaxLimit)
        return false;
    return true;
}
function onCheckboxClick(index) {
    debugger;
    if ($('#PFApplicableAllowance_' + index + '__YN').is(':checked')==true) {
        $('#PFApplicableAllowance_' + index + '__Applicable').val('100.00');
    }
    else {
        $('#PFApplicableAllowance_' + index + '__Applicable').val('');
    }
}

function fillData() {
    debugger;
   
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




  
    var month = $('#PayrollMonth_Code').val();
    var deduction = $('#PFDeduction_Code').val();
    var pfgroup = $('#PFGroup_Code').val();

   /* if (month != '' && deduction != '') {*/
        var dataTable = $("#EmpList").DataTable({
            //"paging": true,
            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/PFSettings/GetSettingsList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';



                        var searchId = $('#Filter_Code').val();
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&Month=' + month + '&Deduction=' + deduction + '&PFGroup=' + pfgroup + '&dummy=dummy';
                        /*   d.LeavePeriodCode = LeavePeriod;*/
                        d.filter = temp;

                        // alert(d.filter);

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {


                    $('#PFSettingsList_Count').val(d.recordsTotal);
                    
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].location = '<a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" id="PF_Settings_slider" title="Edit"  class="btn btn-icon btn-edit icon-only" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"><i class="icon-xl fas fa-pencil-alt"></i></a>'

                    }
                    return d.data;
                }

            },



            "columns": [

                { "data": "employeeID", "width": "8%" },
                { "data": "employeeName", "width": "12%" },
                { "data": "pfGroup.name", "width": "7%" },
                { "data": "pfNo", "width": "7%" },
                { "data": "uanNo", "width": "7%" },
                { "data": "epfPercentage", "width": "5%" },
                { "data": "epsPercentage", "width": "5%" },
                { "data": "erpfPercentage", "width": "5%" },
                { "data": "vpfPercentage", "width": "5%" },
                { "data": "vpfAmount", "width": "6%" },
                { "data": "minLimit", "width": "7%" },
                { "data": "empMaxLimit", "width": "10%" },
                { "data": "employerMaxLimit", "width": "10%" },
                { "data": "location", "width": "6%" },



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



function onDropdownChange() {
    debugger;
    showSpinner();
    month = $('#PayrollMonth_Code').val();
    deduction = $('#PFDeduction_Code').val();
    if (month == '' && deduction == '') {
        month = 0;
        deduction = 0;
    } else if (deduction == '') {
        Month = 0;
    }
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PFSettings/GetEmployeeList',
        dataType: 'html',
        data: { deduction: deduction, monthcode: month },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
          
            else {
                   
                    $('#EmployeeList').html("");
                    $('#EmployeeList').html(result);
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

    //validate(sender, 'loanTypeSpan');
    //ApplyFilter('reset');
}
function onEditClick(EmployeeGI) {
    debugger;
    showSpinner();
    deduction = $('#PFDeduction_Code').val();
    monthCode = $('#PayrollMonth_Code').val();
    pfgroup = $('#PFGroup_Code').val();
   
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PFSettings/GetPFEmployeeWiseSettingsDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, deduction: deduction, monthcode: monthCode, pfgroup: pfgroup },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay testt"></div>');
                $('#editEmployeePf').html("");
                $('#editEmployeePf').html(result);
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

function PFDetailsSave() {


   
    var i = 0;
    //if (!validateById('#PFSettingsDetails_PFNo', 'pfno')) {
    //    i++;
    //}
    if (!validateById('#PFSettingsDetails_UANNo', 'uanno')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_EPFPercentage', 'epfpercentange')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_ERPFPercentage', 'erpfpercentage')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_EPSPercentage', 'epspercentage')) {
        i++;
    }
    //if (!validateById('#PFSettingsDetails_VPFPercentage', 'vpfpercentage')) {
    //    i++;
    //}
    //if (!validateById('#PFSettingsDetails_VPFAmount', 'vpfamount')) {
    //    i++;
    //}
    if (!validateById('#PFSettingsDetails_PFGroup_Code', 'pfgroup')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_MinLimit', 'minlimit')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_EmpMaxLimit', 'empmaxlimit')) {
        i++;
    }
    if (!validateById('#PFSettingsDetails_EmployerMaxLimit', 'employermaxlimit')) {
        i++;
    }

    if (!CheckGeneralSettingsDeviation()) {
        msg = 'There is a deviation from the PF general settings. Do you want to save?'
    }
    else {
        msg = 'Do you want to Save?'
    }

    if (i == 0) {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
    $.ajax({
        type: 'POST',
        url: '/PayRoll/PFSettings/PfDetailsSave',
        dataType: 'json',
        data: $('#PfDetailsSave').serialize(),
        success: function (result) {
            debugger
            if (result.success) {

                swal.fire("Saved Successfully");
                CloseSlider('closeEdit');

                //var url = "/PayRoll/PFSettings/ApplyFilter";
                //$('#loanClosureList').load(url, function () { });
                ApplyFilter('apply')
            }
            else {
                swal.fire(result.message);
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
        });

    }
    else
        return false;
}

function GeneralSettingsSave() {
    debugger;
    var deduction = $('#PFDeduction_Code').val();
    var monthCode = $('#PayrollMonth_Code').val();
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
                url: '/PayRoll/PFSettings/GeneralSettingsSave',
                dataType: 'json',
                data: $('#GeneralSettingsSave').serialize(),
                success: function (result) {
                    debugger
                    if (result.success) {                        
                        $('.bd-example-modal-lg').modal('hide')
                        swal.fire("Saved Successfully");

                       

                    }
                    else {
                        swal.fire(result.message);
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
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

function applicableAllowanceSave() {
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
                url: '/PayRoll/PFSettings/ApplicableAllowanceSave',
                dataType: 'json',
                data: $('#ApplicableAllowanceSave').serialize(),
                success: function (result) {
                    debugger
                    if (result.success) {                       
                        $('.PF_settings_applicable').modal('hide')
                        swal.fire("Saved Successfully");
                    }
                    else {
                        swal.fire(result.message);
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
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
        /*CloseSlider('basicFilter');*/
    }
}

function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {
        deductionname = $('#PFDeduction_Code option:selected').text();
        monthname = $('#PayrollMonth_Code option:selected').text();
        var EmpListTableLength = $('#PFSettingsList_Count').val();
        if (EmpListTableLength > 0) {
            url = "/PayRoll/PFSettings/ExportToExcel?deductionname=" + $('#PFDeduction_Code option:selected').text() + "&monthname=" + $('#PayrollMonth_Code option:selected').text() + "&Totalrows=" + EmpListTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}







function validateDropdowns() {

    var i = 0;
    if (!validateById('#PFDeduction_Code', 'levPeriod')) {
        i++;
    }
    if (!validateById('#PayrollMonth_Code', 'leavGroup')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;

}


function POPUP() {
    debugger;
    deduction = $('#PFDeduction_Code').val();
    if (deduction == "") {
        swal.fire("Please Select PF Deduction");
    }
    else {
        debugger;
        $.ajax({
            type: 'GET',
            url: '/PayRoll/PFSettings/GetPFGeneralSettings',
            dataType: 'html',
            data: { deduction: deduction },

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
                swal.fire(xhr.responseText);

            }
        });
    }
}

function POPUP1() {
    debugger;
    deduction = $('#PFDeduction_Code').val();
    pfgroup = $('#PFGroup_Code').val();
    if (deduction == "") {
        swal.fire("Please Select PF Deduction");
    }
    else {
        debugger;
        $.ajax({
            type: 'GET',
            url: '/PayRoll/PFSettings/GetApplicableAllowance',
            dataType: 'html',
            data: { deduction: deduction, pfgroup: pfgroup },

            success: function (Data) {
                console.log(Data);
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    var m = $('.PF_settings_applicable');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    }
}
