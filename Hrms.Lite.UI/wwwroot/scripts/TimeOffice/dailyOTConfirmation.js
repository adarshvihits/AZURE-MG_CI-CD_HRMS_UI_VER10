function GetApprovedList() {      

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




    var date = $('#Date').val()

    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#overtimeApprovedTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/DailyOTConfirmation/GetApprovedList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Date=' + date + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {


                debugger;

                $('#Details_Count').val(d.recordsTotal);
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "shift" },
            { "data": "punchIn" },
            { "data": "punchOutTime" },
            { "data": "totalPunchTime" },
            { "data": "actualOTHours" },
            { "data": "actualOTMin" },
            { "data": "otHours" },
            { "data": "otMin" },
            



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

    //}
    //else {
    //    return false;
    //}






}



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    $('#overtimeApprovalTableTwo tbody tr').remove();


    var date = $('#Date').val()

    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#overtimeApprovalTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/DailyOTConfirmation/GetOTList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    modeld = '';



                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    console.log($('#dailyOtSave').serialize());



                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Date=' + date + '&dummy=dummy';
                    d.filter = temp;
                    d.model1 = $('#dailyOtSave').serialize();
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {


                debugger;

                $('#Details_Count').val(d.recordsTotal);                
                
                var chkBx = false;
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                   
                    d.data[i].otHours = '<input class="form-control form-control-sm" id="Details_' + i + '__OTHours" name="Details[' + i + '].OTHours" type="text" value="' + d.data[i].otHours + '" onchange="onOtHoursChange(this,' + i + ')" maxlength="2"><input class="form-control form-control-sm" id="Details_' + i + '__EmployeeGI" name="Details[' + i + '].EmployeeGI" type="hidden" value="' + d.data[i].employeeGI + '">'
                        d.data[i].otMin = '<input class="form-control form-control-sm" id="Details_' + i + '__OTMin" name="Details[' + i + '].OTMin" type="text" value="' + d.data[i].otMin + '" onchange="onOtMinsChange(this,' + i + ')" maxlength="2">'
                    
                    if (d.data[i].yn != false) {
                        d.data[i].yn = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input data-val="true" data-val-required="The HrmsUser field is required." id="Details_' + i + '__YN" name="Details[' + i + '].YN" type="checkbox" value="true" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].yn + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                    }
                    else {
                        d.data[i].yn = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input data-val="true" data-val-required="The HrmsUser field is required." id="Details_' + i + '__YN" name="Details[' + i + '].YN" type="checkbox" value="true" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                    }
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "13%"},
            { "data": "designation", "width": "11%" },
            { "data": "department", "width": "11%"},
            { "data": "location", "width": "9%" },
            { "data": "shift", "width": "6%"},
            { "data": "punchIn", "width": "7%" },
            { "data": "punchOutTime", "width": "6%" },
            { "data": "totalPunchTime", "width": "9%"},
            { "data": "otHours", "width": "7%" },
            { "data": "otMin", "width": "7%" },
            { "data": "yn", "width": "4%"    },



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
    
    //}
    //else {
    //    return false;
    //}


}





function Save()
{
    debugger
    if (($('#overtimeApprovalTableTwo tr').length - 1) > 0) {
        swal.fire({
            text: "Do you want to Confirm?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TimeOffice/DailyOTConfirmation/Save',
                    dataType: 'json',
                    data: $('#dailyOtSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Confirmed Successfully").then(function () {
                                fillData();
                            });
                        }
                        else
                            swal.fire(result.message);

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
        });
    }
    else {
        swal.fire("No Data selected")
    }
   
   
}


function ExportToExcel() {

    debugger;
    
        var pendingTableLength = $('#Details_Count').val();
        if (pendingTableLength > 0) {
            url = "/TimeOffice/DailyOTConfirmation/ExportToExcel?Dte=" + $('#Date').val();
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }    

   
}

function ExportToExcelApprovedList() {

    debugger;

    var pendingTableLength = $('#Details_Count').val();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/DailyOTConfirmation/ExportToExcelApprovedList?Dte=" + $('#Date').val();
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }


}

function onOtHoursChange(sender,index) {
    $('#Details_' + index + '__OTHours').val($(sender).val());   
}
function onOtMinsChange(sender, index) {
    
    $('#Details_' + index + '__OTMin').val($(sender).val());
}

function onCheckboxChange(sender, index) {
    debugger;
    checked = $(sender).is(':checked');
    if (checked) {
        var row = $(sender).closest('tr').html();
        var otHrs = $('#Details_' + index + '__OTHours').val();
        var otMins = $('#Details_' + index + '__OTMin').val();        
        var empGI = $('#Details_' + index + '__EmployeeGI').val();

        $('#overtimeApprovalTableTwo tbody').append('<tr id="OtSave_' + index + '">' + row + '</tr>');

        $('#Details_' + index + '__OTHours').val(otHrs);
        $('#Details_' + index + '__OTMin').val(otMins);
        $('#Details_' + index + '__YN').attr('checked', true);
        $('#Details_' + index + '__EmployeeGI').val(empGI);
        //debugger
        //var inp = $('#Details_0__OTHours');
        //$('#testOT').append(inp)
        //$('#testOT #Details_0__OTHours').attr('id','DetailsSave_0__OTHours');
        //$('#testOT #DetailsSave_0__OTHours').val(5);
    }
    else {

        $('#OtSave_' + index).remove();        
               
    }

    $('#overtimeApprovalTableTwo tbody').find("tr").each(function (currindex) {
        debugger;
       // if (currindex > 0) {
            $(this).find("td").eq(2).html(currindex);
            debugger
            var tdd = $(this).find("td").eq(2);
            tdd.prevObject[9].getElementsByTagName('input')[0].id = 'Details_' + currindex + '__OTHours';
            tdd.prevObject[9].getElementsByTagName('input')[0].name = 'Details[' + currindex + '].OTHours';
            tdd.prevObject[9].getElementsByTagName('input')[1].id = 'Details_' + currindex + '__EmployeeGI';
            tdd.prevObject[9].getElementsByTagName('input')[1].name = 'Details[' + currindex + '].EmployeeGI';
            tdd.prevObject[10].getElementsByTagName('input')[0].id = 'Details_' + currindex + '__OTMin';
            tdd.prevObject[10].getElementsByTagName('input')[0].name = 'Details[' + currindex + '].OTMin';
            tdd.prevObject[11].getElementsByTagName('input')[0].id = 'Details_' + currindex + '__YN';
            tdd.prevObject[11].getElementsByTagName('input')[0].name = 'Details[' + currindex + '].YN';


       // }
    });
}


