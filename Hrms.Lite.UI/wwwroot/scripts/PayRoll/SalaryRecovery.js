var AllowanceMonthName;
var AllowanceTypeName;
var Month;
var Allowance;
var UpdatedAmount;

var isselected;


function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {


        var TableLength = $('#SalaryRecoveryList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/SalaryRecovery/ExportToExcel?Month=" + $('#Month').val() + "&Allowance=" + $('#Allowance_Code').val() + "&MonthName=" + $('#Month option:selected').text() + "&AllowanceType=" + $('#Allowance_Code option:selected').text() + "&totRecords=" + TableLength;

            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

}




function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

  


    var MonthCode = $('#Month').val();
    var AllowanceCode = $('#Allowance_Code').val();

    var dataTable = $("#salaryRecoveryTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryRecovery/GetSalaryRecoveryList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&AllowanceCode=' + AllowanceCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                debugger;

                $('#SalaryRecoveryList_Count').val(d.recordsTotal);
               
                if (d.data != "") {
                 
                    $('#totAmt').html(d.data[0].totalAmount);
                }
                if ($('#SalaryRecoveryList_Count').val() == '0')
                    $('#totAmt').html('');
                debugger;

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeCode = '<span id="edit_' + i +  '"style="display:flex;justify-content:center;"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                 

                    if (d.data[i].amount != null)
                        d.data[i].amount = '<span class="d-flex justify-content-end" id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8"   oninput = "process(this)"></span>'

                    else
                        d.data[i].amount = '<span class="d-flex justify-content-end" id="hideAmount_' + i + '">' + '' + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8"   oninput = "process(this)"></span>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "location" },
            { "data": "division" },
            { "data": "department" },
            { "data": "wageGrade" },
            { "data": "amount" },
            { "data": "employeeCode", "width": "8%" },
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


    const myTimeout = setTimeout(tooltipHover, 500);


   
}



function onEditClick(i) {
    debugger

    $('#newAmount_' + i).val($('#hideAmount_' + i).text());
    $('#hideAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#hideRemark_' + i).addClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}

function onCancelClick(i) {
    debugger
    $('#hideAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);

}


function onUpdateClick(index, EmployeeGI) {
    debugger;
    var UpdatedAmount = $('#newAmount_' + index).val()

    var Month = $('#Month').val();
    var Allowance = $('#Allowance_Code').val();
    if (UpdatedAmount == "" || UpdatedAmount == null) {
        swal.fire("Amount should not be null.");
        return false;
    }
    else if (UpdatedAmount>0) {
        swal.fire("Only negative values are allowed.");
        return false;
    }
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
                url: '/PayRoll/SalaryRecovery/Save',
                dataType: 'json',
                data: { Amount: UpdatedAmount, Month: Month, AllowanceCode: Allowance, EmployeeGI: EmployeeGI },
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




function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#Month', 'MonthAllowance')) {
        i++;
    }
    if (!validateById('#Allowance_Code', 'TypeAllowance')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;

}



function ApplyFilter(mode) {
    debugger

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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}
function validateAllowanceDropdowns() {
    var i = 0;
    debugger;
    
    if (!validateById('#Month', 'MonthAllowance')) {
        i++;
    }
    if (!validateById('#Allowance_Code', 'TypeAllowance')) {
        i++;
    }
    if (i == 0) {
       
        var m = $('.bd-example-modal-lg');
        m.modal('show');

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
            var RecoveryInfo = new FormData($('#SalaryRecoveryUpload')[0]);
          
            debugger
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryRecovery/ImportFromExcel?Allowance=' + $('#Allowance_Code option:selected').text(),
                contentType: false,
                processData: false,
                data: RecoveryInfo,
                success: function (result) {
                    if (result.success == true) {
                      
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

