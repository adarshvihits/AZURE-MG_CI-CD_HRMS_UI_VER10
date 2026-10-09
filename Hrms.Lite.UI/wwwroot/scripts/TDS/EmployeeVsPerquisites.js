
var isselected;
function onAssessmentYearChange(sender, id) {
    debugger;



    $.getJSON("/TDS/EmployeeVsPerquisites/GetMonthDropDown", { AssessmentYearCode: $(sender).val() }, function (data) {
        $('#PayrollMonth_Code option').remove();


        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        validateById('#PayrollMonth_Code', 'month')
    });

}
function validateDropdowns() {
    debugger
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'Assessmnt_year'))
        i++;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#Perquisite_Code", 'Perquisit'))
        i++;
  
    if (i == 0) {
        return true;
    }
    else {
        return false;
    }
}

function validateExcelDropdowns() {
    debugger
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'Assessmnt_year'))
        i++;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#Perquisite_Code", 'Perquisit'))
        i++;

    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }
    
}
function fillData() {
    debugger
    var i = 0;
  
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var AssessmentYear = $('#AssessmentYear_Code').val();
    var Month = $('#PayrollMonth_Code').val();
    var PerquisiteNature = $('#Perquisite_Code').val();

    if (!validateById("#AssessmentYear_Code", 'Assessmnt_year'))
        i++;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#Perquisite_Code", 'Perquisit'))
        i++;

    if (i == 0) {

        var dataTable = $("#Perquisitestable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TDS/EmployeeVsPerquisites/GetEmpVsPerquisitesList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&Month=' + Month + '&PerquisiteNature=' + PerquisiteNature + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#EmployeeVsPerquisitesList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].employee.employeeCode = '<span  onmouseenter="tooltipHover()" id = "hideAction_' + i + '" ><a data-bs-toggle="tooltip" title="Edit" type="button"class="btn btn-icon btn-edit icon-only" style="float: none;"  onclick="onEditClick(' + i + ',' + d.data[i].perqAmount + ' , ' + d.data[i].tdsAmount + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span ><div class="small-btn-group save_Cancel_' + i + ' hide"><a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="onUpdateClick(' + i + ', ' + "'" + d.data[i].employee.employeeGuid + "'" + ')"></i></a><a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-cancel icon-only"><i class="icon-xl fas fa-times cancel-btn" onclick="onCancelClick(' + i + ')"></i></a></div>'
                        d.data[i].perqAmount = ' <td data-label="perqAmount" id="row_' + i + '"><span id="hidePerqAmount_' + i + '">' + d.data[i].perqAmount + '</span><span id="perqAmount"></span><span id="amountrow_' + i + '" class="hide"><input type"text"  id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="" maxlength="8" onkeypress="return onlyNumberKey(event)" onpaste="return false"><span id="amount"></span></span></td>'
                        d.data[i].tdsAmount = ' <td data-label="taxAmount" id="row_' + i + '"><span id="hidetdsAmount_' + i + '">' + d.data[i].tdsAmount + '</span><span id="tdsAmount"></span><span id="tdsamountrow_' + i + '" class="hide"><input type"text" id="newtdsAmount_' + i + '" class="form-control form-control-sm" placeholder="" maxlength="8" onkeypress="return onlyNumberKey(event)" onpaste="return false"><span id="tds"></span></span></td>'
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employee.employeeId", "width": "10%" },
                { "data": "employee.employeeName", "width": "20%" },
                { "data": "designationName", "width": "15%" },
                { "data": "departmentName", "width": "15%" },
                { "data": "locationName", "width": "10%" },
                { "data": "perqAmount", "width": "12%" },
                { "data": "tdsAmount", "width": "12%" },
                { "data": "employee.employeeCode", "width": "8%" },

            ],






            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

            "order": [0, "asc"],
            "processing": "true",
            "language": {

                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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


        setTimeout(tooltipHover, 500);
    }
    else
        return false;
}

function onEditClick(index,perqAmt,tax) {
    if (isselected == null)
        isselected = 1;
    $('#hideAction_' + index).addClass('hide');
    $('#hidePerqAmount_' + index).addClass('hide');
    $('#amountrow_' + index).removeClass('hide');
    $('#newAmount_' + index).css('text-align', 'right');
    $('#newtdsAmount_' + index).css('text-align', 'right');
    $('#hidetdsAmount_' + index).addClass('hide');
    $('#tdsamountrow_' + index).removeClass('hide');
    $('.save_Cancel_' + index).removeClass('hide');
    $('#newAmount_' + index).val(perqAmt);
    $('#newtdsAmount_' + index).val(tax);
   
}
function onCancelClick(index) {
    $('#hidePerqAmount_' + index).removeClass('hide');
    $('#hidetdsAmount_' + index).removeClass('hide');
    $('#hideAction_' + index).removeClass('hide');
    $('.save_Cancel_' + index).addClass('hide');
    $('#amountrow_' + index).addClass('hide');
    $('#tdsamountrow_' + index).addClass('hide');
    isselected = null;
}
function onUpdateClick(index, EmployeeGI) {

    var AssessmentYear = $('#AssessmentYear_Code').val();
    var Month = $('#PayrollMonth_Code').val();
    var PerquisiteNature = $('#Perquisite_Code').val();

    UpdatedAmt = $('#newAmount_' + index).val();
    UpdatedtdsAmt = $('#newtdsAmount_' + index).val();
    if (UpdatedAmt == "" || UpdatedAmt == null) {
        swal.fire("Perquisite Amount should not be null");
        return false;
    }
    else if (UpdatedtdsAmt == "" || UpdatedtdsAmt == null) {
        swal.fire("TDS Amount should not be null");
        return false;
    }

    
    else {
        $.ajax({
            type: 'POST',
            url: '/TDS/EmployeeVsPerquisites/SaveEmployeeVsPerquisites',
            dataType: 'json',
            data: { PerqAmount: UpdatedAmt, tdsAmount: UpdatedtdsAmt, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear, Month: Month, PerquisiteNature: PerquisiteNature },
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {

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
    isselected = null;

}
function ExportToExcel() {
  
    var AssessmentYearName = $('#AssessmentYear_Code option:selected').text();
    var Month = $('#PayrollMonth_Code').val();
    var Perquisite = $('#Perquisite_Code').val();
    var MonthName = $('#PayrollMonth_Code option:selected').text();
    var PerquisiteName = $('#Perquisite_Code option:selected').text();

    if (validateDropdowns()) {
       
        var PerquisitetableLength = $('#EmployeeVsPerquisitesList_Count').val();
        if (PerquisitetableLength > 0) {
            url = "/TDS/EmployeeVsPerquisites/ExportToExcel?AssessmentYear=" + $('#AssessmentYear_Code').val() + '&AssessmentYearName=' + AssessmentYearName + '&Month=' + Month + '&MName=' + MonthName + '&PerquisiteName=' + PerquisiteName + '&Perquisite=' + Perquisite + '&Totalrow=' + PerquisitetableLength+"&data=" + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

}

function onImportExcelClick() {
    debugger



    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#perqUpload')[0]);
            $("#Excel_FileData").val('');
            debugger
            $.ajax({

                type: 'POST',
                url: '/TDS/EmployeeVsPerquisites/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {                           
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