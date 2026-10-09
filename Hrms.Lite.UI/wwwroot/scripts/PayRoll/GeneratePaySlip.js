function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();
    
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();

    var Type = $("input[name='Type']:checked").val();
    if (Type != 0)
        $(".payslipbutton").addClass('hide');
    else
        $(".payslipbutton").removeClass('hide');


    var dataTable = $("#PaySlip_Gnrtn_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/GeneratePaySlip/GeneratePaySlipList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&PayrollGroup='
                        + PayrollGroup + '&Type=' + Type+'&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#PaySlipGenerationList_Count').val(d.recordsTotal);
                setInterval(GetPaySlipGeneratedStatus, 2000);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "employeeName", "width": "13%" },
            { "data": "designation", "width": "11%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "7%" },
            { "data": "category", "width": "8%" },
            { "data": "payrollGroup", "width": "7%" },
            { "data": "lop", "width": "4%" },
            { "data": "grossAllowance", "width": "6%" },

            { "data": "grossDeduction", "width": "7%" },
            { "data": "advance", "width": "6%" },
            { "data": "netPay", "width": "7%" },
            { "data": "paymentMode", "width": "7%" },



        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '60vh',
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
        /* dataTable.search('').draw();*/
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
function GeneratePaySlip() {

    
    debugger;
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();
    var MName = $("#Month_Code option:selected").text();
    var payslipGenerationStatus = 0;
    if ($('#PaySlipGenerationList_Count').val() > 0) {
        showSpinner();
        swal.fire({
            text: "Do you want to Generate PaySlip ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/GeneratePaySlip/GeneratePaySlip',
                    dataType: 'json',
                    data: $('#Generate_PaySlip').serialize() + "&MonthName=" + MName,
                    /*data: { Month: Month, PayrollGroup: PayrollGroup},*/
                    success: function (result) {
                        swal.fire("Payslip Generated Successfully");
                        payslipGenerationStatus = 1;
                        fillData();
                        hideSpinner();

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                        hideSpinner();
                        payslipGenerationStatus = 1;
                    }
                });
                if (payslipGenerationStatus == 0) {
                    swal.fire('Payslip generation will take a few minutes. Feel free to exit the form while the payslip is being generated.');
                    hideSpinner();
                }
            }
            else {
                hideSpinner();
                return false;
            }
        })
    }
    else {
        swal.fire('No data found.');
    }


}
function ExportToExcel() {
    debugger;
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();var Month = $("#Month_Code").val();
    var MName = $("#Month_Codee option:selected").text();
    var PayRollName = $("#PayGroup_Code option:selected").text();
    var Type = $("input[type='radio']:checked").val();
    var Total = $("#PaySlipGenerationList_Count").val();
    if (validateDropdowns()) {

        var TableLength = $('#PaySlipGenerationList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/GeneratePaySlip/ExportToExcel?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&Type=" + Type + "&PayRollName="
                + PayRollName + "&MName=" + MName + "&Total=" + Total;

            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}


function GetPaySlipGeneratedStatus() {
    var mCode = $('#Month_Code').val();
    var pCode = $('#PayGroup_Code').val();
    if (mCode != '' && pCode != '') {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/GeneratePaySlip/PaySlipGeneratedStatus',
            dataType: 'json',
            data: { SalaryPeriod: mCode, PayRollGroup: pCode },
            /*data: { Month: Month, PayrollGroup: PayrollGroup},*/
            success: function (data) {
                //if (data.generatedEmployeeCount != data.totalEmployeeCount && data.generatedEmployeeCount > 0 ) {
                //    //$('#GenBtn').attr('disabled', true);
                    $('#GenerationStatus').removeClass('hide');
                //}
                //else {
                //    //$('#GenBtn').attr('disabled', false);
                //    $('#GenerationStatus').addClass('hide');
                //}

                $('#genCount').html(data.generatedEmployeeCount);
                $('#totCount').html(data.totalEmployeeCount);

            },
            error: function (xhr, textStatus, errorThrown) {
                //document.body.id = "kt_body";
                //$('body').html(xhr.responseText);                       
            }
        });
    }
   
}

function validateDropdowns() {
    debugger;
    return true;
}