function fillData() {
    debugger;
    var isVisibleColumns = false;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();

    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();

    var Type = $("input[type='radio']:checked").val();
    if (Type != '0') {
        $(".payslipbutton").addClass('hide');
        isVisibleColumns = false;
        $(".sendpaysliptable").removeClass('salary-summary-report2');
        $(".sendpaysliptable").addClass('salary-summary-report');
    }   
    else {
        $(".payslipbutton").removeClass('hide');
        isVisibleColumns = true;
        $(".sendpaysliptable").addClass('salary-summary-report2');
        $(".sendpaysliptable").removeClass('salary-summary-report');
    }


    var dataTable = $("#PaySlip_Gnrtn_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SendPayslip/SendPaySlipList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&PayrollGroup='
                        + PayrollGroup + '&Type=' + Type + '&dummy=dummy';



                    d.filter = temp;
                    d.model1 = $('#Send_PaySlip').serialize();

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                console.log(d);
                debugger
                $('#SendPaySlipList_Count').val(d.recordsTotal);
                
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (d.data[i].yn != false) {
                            if (Type == 0)
                                d.data[i].yn = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input data-val="true" data-val-required="The HrmsUser field is required." id="SendPaySlipList_' + i + '__YN" name="SendPaySlipList[' + i + '].YN" type="checkbox" value="true" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].yn + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div><input class="form-control form-control-sm" id="SendPaySlipList_' + i + '__EmployeeGI" name="SendPaySlipList[' + i + '].EmployeeGI" type="hidden" value="' + d.data[i].employeeGI + '">'
                            else
                                d.data[i].yn = '';
                        }
                        else {
                            if (Type == 0)
                            d.data[i].yn = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input data-val="true" data-val-required="The HrmsUser field is required." id="SendPaySlipList_' + i + '__YN" name="SendPaySlipList[' + i + '].YN" type="checkbox" value="true" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div ><input class="form-control form-control-sm" id="SendPaySlipList_' + i + '__EmployeeGI" name="SendPaySlipList[' + i + '].EmployeeGI" type="hidden" value="' + d.data[i].employeeGI + '">'
                            else
                                d.data[i].yn = '';
                        }

                    }
               
               
                return d.data;
            }

        },
        
        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "category" },
            { "data": "payrollGroup" },
            { "data": "lop" },
            { "data": "grossAllowance" },
            { "data": "grossDeduction" },
            { "data": "advance" },
            { "data": "netPay" },
            { "data": "paymentMode" },
            { "data": "email" },
            { "data": "yn",  visible: isVisibleColumns },



        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '40vh',
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
    $('#PaySlip_Gnrtn_Table tbody tr :nth-child(14)').hide();
}

function GetList() {
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();

    var Type = $("input[type='radio']:checked").val();
    if (Month != '' && PayrollGroup != '' && Type != '') {
        if (Type != '0') {
            $(".payslipbutton").addClass('hide');
            
            $(".sendpaysliptable").removeClass('sendpaysliptable_list');
            $(".sendpaysliptable").addClass('notsendpaysliptable_list');
        }
        else {
            $(".payslipbutton").removeClass('hide');
            
            $(".sendpaysliptable").addClass('notsendpaysliptable_list');
            $(".sendpaysliptable").removeClass('sendpaysliptable_list');
        }
     
        $.ajax({
         
            type: 'POST',
            url: '/PayRoll/SendPayslip/GetList?Month=' + Month + '&PayrollGroup=' + PayrollGroup + '&Type=' + Type,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                $('#LIST_ID').html('');
                $('#LIST_ID').html(result);
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
     
    }
    
}


function ApplyFilter(mode) {
    debugger   


    if (mode == 'apply') {
        //howSpinner();
        if (filterApplicable >= 1) {
            //fillData();
            GetList();
            $('#filterForm').removeClass('filter-apply');
            CloseSlider('basicFilter');
        }
        else {
            //fillData();
            GetList();
            $('#filterForm').addClass('filter-apply');
            CloseSlider('basicFilter');
        }

    }
    else {
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        //fillData();
        GetList();
    }

}


function onCheckboxChange(sender, index) {
    debugger;
    checked = $(sender).is(':checked');
    if (checked) {
        var row = $(sender).closest('tr').html();
        var empGI = $('#SendPaySlipList_' + index + '__EmployeeGI').val();

        $('#overtimeApprovalTableTwo tbody').append('<tr id="PaySlipSave_' + index + '">' + row + '</tr>');

        $('#SendPaySlipList_' + index + '__YN').attr('checked', true);
        $('#SendPaySlipList_' + index + '__EmployeeGI').val(empGI);
        //debugger
        //var inp = $('#Details_0__OTHours');
        //$('#testOT').append(inp)
        //$('#testOT #Details_0__OTHours').attr('id','DetailsSave_0__OTHours');
        //$('#testOT #DetailsSave_0__OTHours').val(5);
    }
    else {

        $('#PaySlipSave_' + index).remove();    

    }

    $('#overtimeApprovalTableTwo tbody').find("tr").each(function (currindex) {
        /*if (currindex > 0) {*/
            $(this).find("td").eq(2).html(currindex);
            debugger
            var tdd = $(this).find("td").eq(2);
        tdd.prevObject[13].getElementsByTagName('input')[1].id = 'SendPaySlipList_' + currindex + '__EmployeeGI';
        tdd.prevObject[13].getElementsByTagName('input')[1].name = 'SendPaySlipList[' + currindex + '].EmployeeGI';
        tdd.prevObject[13].getElementsByTagName('input')[0].id = 'SendPaySlipList_' + currindex + '__YN';
        tdd.prevObject[13].getElementsByTagName('input')[0].name = 'SendPaySlipList[' + currindex + '].YN';


        /*}*/
    });
}

function SendPaySlip() {
    //showSpinner();
    debugger;
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();
    var Type = $("input[type='radio']:checked").val();
    var table = $('#PaySlip_Gnrtn_Table').DataTable();
    var data = table.$('input,select,textarea').serializeArray();
    swal.fire({
        text: "Do you want to Send PaySlip ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SendPayslip/Send_PaySlip?Month=' + Month + '&PayrollGrp=' + PayrollGroup + '&Type=' + Type,
                dataType: 'json',
                data: data,       
                success: function (result) {
                    debugger;
                    swal.fire('Sending payslip will take a few minutes. Feel free to exit the form while the payslip is being sent.').then(function () {
                        GetList();

                    });
                  
                  
                    
                   
                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSpinner();
                }
            });
        }
        else {
            return false;
        }
    })




}


function onAllClick(chkid, tableid) {
    var oTable = $('#' + tableid).dataTable();

    var allPages = oTable.fnGetNodes();


    if ($('#' + chkid).is(':checked')) {
        //    $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
        $('input[type="checkbox"]', allPages).prop('checked', true);
    }
    else {
    //    $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
        $('input[type="checkbox"]', allPages).prop('checked', false);
    }
}


function ExportToExcel() {
    debugger;
    var Month = $("#Month_Code").val();
    var PayrollGroup = $("#PayGroup_Code").val();
    var MName = $("#Month_Code option:selected").text();
    var PayRollName = $("#PayGroup_Code option:selected").text();
    var Type = $("input[type='radio']:checked").val();
    var Total = $("#SendPaySlipList_Count").val();
 

        var TableLength = $('#SendPaySlipList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/SendPayslip/ExportToExcel?Month=" + Month + "&PayrollGroup=" + PayrollGroup + "&Type=" + Type + "&PayRollName="
                + PayRollName + "&MName=" + MName + "&Total=" + Total;

            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}