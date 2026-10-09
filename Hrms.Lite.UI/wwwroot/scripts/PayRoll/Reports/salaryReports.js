var currMonth = '';



function onAllClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function checkBoxClick(sender, TableId, checkId) {
    debugger;
    if ($(sender).is(':checked')) {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length - 1) {

            $('#' + checkId).prop('checked', true)
        }

        //else if ($('#' + TableId).find('input[type="checkbox"]:checked').length-1 == $('#' + TableId).find('input[type="checkbox"]').length-1)
        //{
        //    $('#' + checkId).prop('checked', true)
        //}
        else
            $('#' + checkId).prop('checked', false)
    }
    else {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length) {

            $('#' + checkId).prop('checked', true)
        }
        else
            $('#' + checkId).prop('checked', false)
    }
}
function onMonthChange() {
    $.ajax({

        type: 'POST',
        url: '/PayRoll/SalaryReports/GetSelectedMonthDates',
        dataType: 'json',
        data: { MonthCode: $('#MonthA_Code').val()},
        success: function (data) {
            debugger;
            //if (currMonth != '')
            //    $('#MonthB_Code option[value=' + currMonth + ']').show();
            //currMonth = data.monthCode;
            //$('#MonthB_Code option[value=' + data.monthCode + ']').hide();
            //$('#MonthB_Code').val(data.processed_PreviousMonthCode);
            $('#MonthB_Code option').remove();
            $('#MonthB_Code').append('<option value="">--Select--</option');

            for (var i = 0; i < data.length; i++) {
                $('#MonthB_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
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


function onAllowDedRadioChange(sender) {
    debugger;
    var cid = $(sender).val();
    //if (cid == 0 || cid == "") {
    //    $(sender).addClass('is-invalid');
    //    $("#" + id + "").addClass('error-message');
    //    $("#" + id + "").html("This field is required");
    //}
    //else {
    //    $(sender).removeClass('is-invalid');
    //    $("#" + id + "").removeClass('error-message');
    //    $("#" + id + "").html("");
    //}
    $.getJSON("/PayRoll/SalaryReports/GetAllowance_Deduction", { id: cid }, function (data) {

        $('#Type_Code option').remove();
        //if(cid==0)
        //    $('#Type_Code').append('<option value="">--Select Allowance--</option');
        //else
        //    $('#Type_Code').append('<option value="">--Select Deduction--</option');
        for (var i = 0; i < data.length; i++) {
            $('#Type_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function getSalaryReportList() {
    $.ajax({

        type: 'POST',
        url: '/PayRoll/SalaryReports/GetSalaryReport',
        dataType: 'html',
        data: $('#getSalaryReportData').serialize(),
        success: function (result) {
                    $('#salaryReportList').html("");
                    $('#salaryReportList').html(result);
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

function fillData() {

    debugger;
    var salaryReportdata = $('#getSalaryReportData').serialize();
    var gridSettingsdata = $('#gridSettingsData').serialize();
    var filter1 = $('#BasicFilter').serialize();
    var monthA = $('#MonthA_Code').val();
    var modeld = salaryReportdata;
    var temp = filter1;
    if ($('#MonthA_Code').val() != '') {
        $('#mthA').html($('#MonthA_Code option:selected').text())
        $('#Ded_mthA').html($('#MonthA_Code option:selected').text())
    }
    if ($('#MonthB_Code').val() != '') {
        $('#mthB').html($('#MonthB_Code option:selected').text())
        $('#Ded_mthB').html($('#MonthB_Code option:selected').text())
    }
    /* if (Allowance != '') {*/
    if ($('input[name="Allowance_Deduction"]:checked').val() == 0) {  //alllowance radio button
        $('#salaryReportDeductionList').addClass('hide');
        $('#salaryReportList').removeClass('hide');
        var dataTable2 = $("#SalaryReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/SalaryReports/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        modeld = '';
                        gridSettings = ''


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&monA=' + monthA + '&dummy=dummy';
                        modeld = salaryReportdata;
                        //gridSettings = gridSettingsdata;
                        d.model = modeld;
                        d.filter = temp;
                        d.gridSettings = gridSettings;
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#MainList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].employeeName = '<td data-label="Employee Name"><a type="button"class="btn  btn-sm float-left" onclick="onEmpClick(' + "'" + d.data[i].employeeCode + "'" + ',' + "'" + d.data[i].employeeName + "'" + ',' + "'" + d.data[i].employeeID + "'" + ')">' + d.data[i].employeeName + '</a></td>'



                    }
                    return d.data;
                }

            },
           
            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "12%" },
                { "data": "location", "width": "11%" },
                { "data": "monthA_Amount", "width": "7%" },
                { "data": "monthB_Amount", "width": "7%" },
                { "data": "difference", "width": "7%" },
                { "data": "monthA_LOP", "width": "7%" },
                { "data": "monthB_LOP", "width": "7%" },
               
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
            dataTable2.search(this.value).draw();
            $('#noDataFound').css("display", 'none');

        });

        $('.search-slider-inner').click(function () {
            //dataTable.search('').draw();
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
        });

        dataTable2.on('draw', function () {
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
    else { //deduction
        $('#salaryReportDeductionList').removeClass('hide');
        $('#salaryReportList').addClass('hide');

        var param = $('#Type_Code option:selected').text();
       
            colmns = [{ "data": "employeeID", "width": "5%", },
            { "data": "employeeName", "width": "10%" },
            { "data": "designation", "width": "8%" },
            { "data": "department", "width": "8%" },
            { "data": "location", "width": "8%" },
            { "data": "monthA_Amount", "width": "13%" },
            { "data": "monthB_Amount", "width": "11%" },
            { "data": "difference", "width": "11%" },
            { "data": "monthA_ActualGross", "width": "10%" },
            { "data": "monthA_EarnedGross", "width": "5%" },
            { "data": "monthB_ActualGross", "width": "5%" },
            { "data": "monthB_EarnedGross", "width": "5%" }]
       
        var dataTable = $("#SalaryReportDeductionTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/SalaryReports/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        modeld = '';
                        gridSettings = ''


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&monA=' + monthA + '&dummy=dummy';
                        modeld = salaryReportdata;
                        gridSettings = gridSettingsdata;
                        d.model = modeld;
                        d.filter = temp;
                        d.gridSettings = gridSettings;
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#MainList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].employeeName = '<td data-label="Employee Name"><a type="button"class="btn  btn-sm float-left" onclick="onEmpClick(' + "'" + d.data[i].employeeCode + "'" + ',' + "'" + d.data[i].employeeName + "'" + ',' + "'" + d.data[i].employeeID + "'" + ')">' + d.data[i].employeeName + '</a></td>'



                    }
                    return d.data;
                }

            },

            "columns": colmns ,


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
        if (param == 'PF' || (param == 'ESI'))
        {
        
            debugger
            let column8 = dataTable.column(8);
            column8.visible(true);
            let column9 = dataTable.column(9);
            column9.visible(true);
            let column10 = dataTable.column(10);
            column10.visible(true);
            let column11 = dataTable.column(11);
            column11.visible(true);
        }
        
        else {
            let column8 = dataTable.column(8);
            column8.visible(false);
            let column9 = dataTable.column(9);
            column9.visible(false);
            let column10 = dataTable.column(10);
            column10.visible(false);
            let column11 = dataTable.column(11);
            column11.visible(false);
        }
    }

    //}
    //else {
    //    return false;
    //}

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
        $('#filterForm').removeClass('filter-apply');
        fillData();
        CloseSlider('basicFilter');
    }
}
function GetExportToExcelData()
{
    var i = 0;
    var basedon = $('input[name="Allowance_Deduction"]:checked').val();
    if (!validateById('#MonthA_Code', 'monthA'))
        i++;
    if (!validateById('#MonthB_Code', 'monthB'))
        i++;
    if (i == 0) {
      
        TableLength = $("#MainList_Count").val();
        if (TableLength != 1) {

            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryReports/GetExportToExcelData',
                dataType: 'html',
                data: $('#getSalaryReportData').serialize(),
                success: function (result) {
                    ExportToExcel(basedon);
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
            swal.fire(NoDataFoundMsg);
        }
    }
}




function ExportToExcel(basedon) {
    debugger;
    if (basedon== 1) {
      
      
            url = "/PayRoll/SalaryReports/ExportToExcel?AllwOrDedCode=" +
                $('input[name="Allowance_Deduction"]:checked').val() + "&MonthA=" + $('#MonthA_Code option:selected').text() + "&MonthB=" + $('#MonthB_Code option:selected').text()
                + "&AllwOrDedName=" + $('#Type_Code option:selected').text() + "&Recordcount=" + $("#MainList_Count").val();
            window.location.href = url;
      
    }
    else {
       
            url = "/PayRoll/SalaryReports/ExportToExcel?AllwOrDedCode=" + $('input[name="Allowance_Deduction"]:checked').val() +
                "&MonthA=" + $('#MonthA_Code option:selected').text() + "&MonthB=" + $('#MonthB_Code option:selected').text() 
                + "&AllwOrDedName=" + $('#Type_Code option:selected').text() + "&Recordcount=" + $("#MainList_Count").val();
            window.location.href = url;
    }
}


function onEmpClick(empCode,empName,empId) {
    $.ajax({
        type: 'GET',
        url: "/PayRoll/SalaryReports/GetSalaryIntegrityReport_Details",
        dataType: 'html',
        data: { MonthA: $('#MonthA_Code').val(), MonthB: $('#MonthB_Code').val(), EmployeeCode: empCode, EmpName: empName, empId: empId },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.bd-example-modal-lg2');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                empNameTxt = empName + '(' + empId + ')';
                $('.empName').html(empNameTxt);
                $('.mnthA').html($('#MonthA_Code option:selected').text());
                $('.mnthB').html($('#MonthB_Code option:selected').text());
                m.modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
        
    });
}


function ExportToExcelEmpPopUp(EmpName,empId) {
        debugger;
    var TableLength = $('#Details_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/SalaryReports/ExcelEmpPopUp?EmpName=" + EmpName + "&empId=" + empId;
    window.location.href = url;
        }
    else {
        swal.fire("No Data Found To Export");
        }

    }
