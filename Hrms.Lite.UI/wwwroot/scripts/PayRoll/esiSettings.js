
function onCheckboxClick(index) {
    debugger;
    if ($('#ESIApplicableAllowance_' + index + '__YN').is(':checked') == true) {
        $('#ESIApplicableAllowance_' + index + '__Applicable').val('100.00');
    }
    else {
        $('#ESIApplicableAllowance_' + index + '__Applicable').val('');
    }
}
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var esiDeduction = $('#ESIDeduction_Code').val();
    var Month = $('#PayrollMonth_Code').val();



    //if (esiDeduction == '' && Month == '') {
    //    month = 0;
    //    deduction = 0;
    //}
    //else if (esiDeduction == '') {
    //    Month = 0;
    //}
   /* if (esiDeduction != '' && Month != '') {*/
        var dataTable = $("#EsiSettingsTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/ESISEttings/GetESIList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&EsiDeduction=' + esiDeduction + '&Month=' + Month + '&dummy=dummy';
                        d.filter = temp;
                        //d.esiDeduction = EsiDeduction;
                        //d.Month = Month;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {






                    $('#ESISettingsList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                      /*  d.data[i].doj = d.data[i].doj.split('T')[0];*/
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].employeeCode = ' <a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title="Edit"id = "ESI_Settings_slider" type = "button"class="btn btn-icon btn-edit icon-only slide r-btn" ><i class="icon-xl fas fa-pencil-alt"onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"></i></a >'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","width":"10%" },
                { "data": "employeeName", "width": "18%" },
                { "data": "doj", "width": "8%" },
                { "data": "location", "width": "10%" },
                { "data": "esiGroup.name", "width": "12%" },
                { "data": "esiNo", "width": "9%" },
                { "data": "eesi", "width": "6%" },
                { "data": "mesi", "width": "7%"},
                { "data": "minLimit", "width": "7%" },
                { "data": "empMaxLimit", "width": "7%" },
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




    //}
    //else {
    //    return false;
    //}

}









function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#ESIDeduction_Code', 'EsiDed')) {
        i++;
    }
    if (!validateById('#PayrollMonth_Code', 'PayrollM')) {
        i++;
    }    

    if (i == 0)
        return true;
    else
        return false;

}
function onDropdownsChange() {
    var esiDeduction = $('#ESIDeduction_Code').val();
    var Month = $('#PayrollMonth_Code').val();
    showSpinner();
    validateDropdowns();
    if (esiDeduction == '' && Month == '') {
        month = 0;
        deduction = 0;
    }
    else if (esiDeduction == '') {
        Month = 0;
    }
    $.ajax({

        type: 'GET',
        url: '/PayRoll/ESISEttings/GetList',
        dataType: 'html',
        data: { ESIDeductionCode: esiDeduction, MonthCode: Month },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#EsiSettingsList').html("");
                $('#EsiSettingsList').html(result);
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
    //if (esiDeduction != "" && Month != "") {
    //    showSpinner();
    //    $('#EsiSettingsList').load(url, { ESIDeductionCode: esiDeduction, MonthCode: Month }, function () { hideSpinner(); });
    //}
    //else if (esiDeduction != "") {
    //    $('#EsiSettingsList').load(url, { ESIDeductionCode: esiDeduction, MonthCode: 0 }, function () { hideSpinner(); });
    //}
    //else {
    //    $('#EsiSettingsList').load(url, { ESIDeductionCode: 0, MonthCode: 0 }, function () { hideSpinner(); });
    //}

}

function onEditClick(EmployeeGI) {
    showSpinner();
    var esiDeduction = $('#ESIDeduction_Code').val();
    var Month = $('#PayrollMonth_Code').val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/ESISEttings/Edit',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, ESIDeductionCode: esiDeduction, MonthCode: Month },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EsiSettingsSlider').html("");
                $('#EsiSettingsSlider').html(result);
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
function CheckGeneralSettingsDeviation() {
    debugger;
    var EESI = $('#ESISettingsDetails_EESI').val();
    var MESI = $('#ESISettingsDetails_MESI').val();
    var MinLimit = $('#ESISettingsDetails_MinLimit').val();
    var EmpMaxLimit = $('#ESISettingsDetails_EmpMaxLimit').val();
    var Gen_EESI = $('#ESISettingsDetails_Gen_EESI').val();
    var Gen_MESI = $('#ESISettingsDetails_Gen_MESI').val();
    var Gen_MinLimit = $('#ESISettingsDetails_Gen_MinLimit').val();
    var Gen_EmpMaxLimit = $('#ESISettingsDetails_Gen_EmpMaxLimit').val();
    if (EESI != Gen_EESI)
        return false;
    if (MESI != Gen_MESI)
        return false;
    if (MinLimit != Gen_MinLimit)
        return false;
    if (EmpMaxLimit != Gen_EmpMaxLimit)
        return false;
    return true;
}

function EmpWiseSave() {
    var i = 0;
    var msg = '';
    if (!validateById('#ESISettingsDetails_ESIGroup_Code', 'esiGrp')) {
        i++;
    }
    if (!validateById('#ESISettingsDetails_ESINo', 'esiNo')) {
        i++;
    }
    if (!validateById('#ESISettingsDetails_EESI', 'esiEESI')) {
        i++;
    }
    if (!validateById('#ESISettingsDetails_MESI', 'esiMESI')) {
        i++;
    }
    if (!validateById('#ESISettingsDetails_MinLimit', 'esiMinLimit')) {
        i++;
    }
    if (!validateById('#ESISettingsDetails_EmpMaxLimit', 'esiMaxLimit')) {
        i++;
    }
    if (!CheckGeneralSettingsDeviation()) {
        msg = 'There is a deviation from the ESI general settings. Do you want to save?'
    }
    else {
        msg = 'Do you want to Save?'
    }
    if (i == 0)
    {
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
                url: '/PayRoll/ESISettings/ESISettingsEmpWiseSave',
                dataType: 'json',
                data: $('#esiSettingsEditSlider').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('esiSlider');
                            //var url = "/DataBank/IndustryBenchmarkSalary/Index";
                            //onDropdownsChange();
                            fillData();
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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
    else
        return false;
}


function OnApplicableAllowanceOrGeneralSettingsClick() {
    debugger;
    var esiDeduction = $('#ESIDeduction_Code').val();
    if (esiDeduction == "") {
        swal.fire("Please Select ESI Deduction");
    }   
}

function ESIGeneralSettingsSave() {
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
                url: '/PayRoll/ESISettings/ESIGeneralSettingsSave',
                dataType: 'json',
                data: $('#esiGeneralSettings').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            //CloseSlider('esiSlider');
                            //var url = "/DataBank/IndustryBenchmarkSalary/Index";
                            //onDropdownsChange();
                            $('.bd-example-modal-lg').modal('hide')
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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

function ESISettingsAllowanceSave() {
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
                url: '/PayRoll/ESISettings/ESISettingsAllowanceSave',
                dataType: 'json',
                data: $('#ApplAllowance').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            //CloseSlider('esiSlider');
                            //var url = "/DataBank/IndustryBenchmarkSalary/Index";
                            //onDropdownsChange();
                            $('.bd-example-modal-lg2').modal('hide')

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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
    if (!validateById('#ESIDeduction_Code', 'EsiDed') && !validateById('#PayrollMonth_Code', 'PayrollM')) {
        CloseSlider('basicFilter');
        return false;
    }
    else {

        if (mode == 'apply') {
          /*  showSpinner();*/
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
           /* showSpinner();*/
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            fillData();
            CloseSlider('basicFilter');
        }
    }
}


function ExportToExcel() {
    debugger;

    if (!validateById('#ESIDeduction_Code', 'EsiDed') && !validateById('#PayrollMonth_Code', 'PayrollM')) {        
        return false;
    }
    else {
        var TableLength = $('#ESISettingsList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/ESISettings/ExportToExcel?EsiDeductionName=" + $('#ESIDeduction_Code option:selected').text() + "&PayrollMonthName=" + $('#PayrollMonth_Code option:selected').text() + "&totRecords=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

}


function OnESIGeneralSettingsClick() {
    debugger;    
    var esiDeduction = $('#ESIDeduction_Code').val();
    if (esiDeduction == "") {
        swal.fire("Please Select ESI Deduction");
    }
    else {
        $.ajax({
            type: 'GET',
            url: "/PayRoll/ESISettings/GetESIGeneralsettingsList?ESIDeductionCode=" + $('#ESIDeduction_Code').val(),
            dataType: 'html',

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
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
}

function OnApplicableAllowanceClick() {
    debugger;
    var esiDeduction = $('#ESIDeduction_Code').val();
    if (esiDeduction == "") {
        swal.fire("Please Select ESI Deduction");
    }
    else {
        $.ajax({
            type: 'GET',
            url: "/PayRoll/ESISettings/GetApplicableAllowanceList?ESIDeductionCode=" + $('#ESIDeduction_Code').val(),
            dataType: 'html',

            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    var m = $('.bd-example-modal-lg2');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
                }
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

