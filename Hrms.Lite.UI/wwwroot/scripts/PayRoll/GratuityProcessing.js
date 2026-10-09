

function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#GratuityTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/GratuityProcessing/GetgratuityEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue  + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {


                debugger;
                console.log(d);
                $('#EmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].doc = _GetFormattedDate(d.data[i].doc);
                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].employeeName = '<div class="d-flex align-items-center" ><div class="name-post" ><div><a href="/PayRoll/GratuityProcessing/GetBreakingServiceDetails?EmployeeGI=' + d.data[i].employeeGI  + '&EmpCode=' + d.data[i].employeeCode+ '">' + d.data[i].employeeName + '</a></div>'


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
            { "data": "doj" },
            { "data": "doc" },
            { "data": "lwd" },
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




}

function StartDateValidate(index) {
    debugger;
    var lwd = $('#EmployeeData_LWD').val();
    var enddate = $('#BreakingService_' + index + '__EndDate').val();
    var lwd1 = validateDate(lwd);
    var doj = $('#EmployeeData_DOJ').val();
   
    var startdate = $('#BreakingService_' + index + '__StartDate').val();
 
    var doj1 = validateDate(doj);


    if (Date.parse(startdate) < Date.parse(doj1)) {
        swal.fire("Start date must be after the DOJ");
     /*   $('#BreakingService_' + index + '__StartDate').val('');*/
        $('#BreakingService_' + index + '__StartDate').datepicker('setDate', null);
        return false;
    }

    else if (Date.parse(startdate) > lwd1) {
        swal.fire("Start date cannot be after the last working day");
        $('#BreakingService_' + index + '__StartDate').datepicker('setDate', null);
        return false;
    }
    else if (Date.parse(enddate) == Date.parse(startdate)) {
        swal.fire("Start Date and End Date should not be same");
        $('#BreakingService_' + index + '__StartDate').datepicker('setDate', null);
        return false;
    }

    else {
        return true;
    }

}

function EndDateValidate(index) {
    debugger;

    var lwd = $('#EmployeeData_LWD').val();
    var enddate = $('#BreakingService_' + index + '__EndDate').val();
    var doj = $('#EmployeeData_DOJ').val();
    var startdate = $('#BreakingService_' + index + '__StartDate').val();
    var lwd1 = validateDate(lwd);
    var doj1 = validateDate(doj);
    if (Date.parse(enddate) > Date.parse(lwd1)) {
        swal.fire("End date cannot be after the last working day");
        $('#BreakingService_' + index + '__EndDate').datepicker('setDate', null);
        return false;
    }
    else if (Date.parse(enddate) < Date.parse(doj1)) {
        swal.fire("End date must be after the  DOJ");
        $('#BreakingService_' + index + '__EndDate').datepicker('setDate', null);
        return false;
    }
    else if (Date.parse(enddate) == Date.parse(startdate)) {
        swal.fire("Start Date and End Date should not be same");
        $('#BreakingService_' + index + '__EndDate').datepicker('setDate', null);
        return false;
    }
    else {
        return true;
    }
}









function AddRow(tableId,index) {
    debugger;




    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;

    //if (validateList()) {

    //}
    if (count >= 0) {

        if (EndDateValidate(count) || StartDateValidate(count)) {

            var a = $('#BreakingService_' + (count - 1) + '__StartDate').val();
            var b = $('#BreakingService_' + (count - 1) + '__EndDate').val();

            if (($('#BreakingService_' + (count - 1) + '__StartDate').val() == '') || ($('#BreakingService_' + (count - 1) + '__Remarks').val() == '') || ($('#BreakingService_' + (count - 1) + '__EndDate').val() == '')) {
                swal.fire("please fill the current row");
                /*     $('#BreakingService_' + i + '__StartDate').focus();*/
                return false;
            }

            if (Date.parse(a) > Date.parse(b)) {
                swal.fire("End Date should be greater than Start Date");
            }
            //if (Date.parse(a) = Date.parse(b)) {
            //    swal.fire("Start Date and End Date should not be same");
            //}

            else {

                debugger
              


                var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row

                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(/\[0\]./g, '[' + count + '].');
                        $(this).attr('name', name);
                    }

                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(/_0__/g, '_' + count + '__');
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }

                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onchange', onchange);
                    }

                    var onblur = $(this).attr('onblur');
                    if (onblur !== undefined) {
                        onblur = onblur.replace(/\,0\)/g, ',' + count + ')');
                        $(this).attr('onblur', onblur);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(/\[0\]./g, '[' + count + '].');
                    $(this).attr('name', name);

                    var id = $(this).attr('id');
                    id = id.replace(/_0__/g, '_' + count + '__');
                    $(this).attr('id', id);


                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });

                //--------------------------date picker

                $.each(template.find('input.datepicker'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(/\[0\]./g, '[' + count + '].');
                    $(this).attr('name', name);

                    var id = $(this).attr('id');
                    id = id.replace(/_0__/g, '_' + count + '__');
                    $(this).attr('id', id);

                    //---------------------------------------
                    // For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });




                $.each(template.find('button[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                    $(this).attr('onclick', onclick);
                });


                $.each(template.find('.RemarksClass'), function () {

                    $(this).attr("id", "remarks_" + count);
                });

                debugger;
                template.find('td[id=grid_slno]').text(count + 1);
                $.each(template.find('.addObj'), function () {
                    $(this).attr("id", "Add_" + count);
                });
                $.each(template.find('.delObj'), function () {
                    $(this).attr("id", "delete_" + count);
                });
                $.each(template.find('.editObj'), function () {
                    $(this).attr("id", "edit_" + count);
                });
                $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
                //------------to clear when new row---------



                $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
                $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
                $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');
                $('#' + tableId + ' tr[id="row_OI_' + count + '"] input.datepicker').datepicker();
                debugger;

                $('#Add_' + (count - 1)).addClass('hide');
                $('#delete_' + (count - 1)).addClass('hide');
                $('#edit_' + (count - 1)).addClass('hide');
                $('#Add_' + (count)).removeClass('hide');
                $('#delete_' + (count)).removeClass('hide');
                $('#edit_' + (count)).removeClass('hide');

                $('.CommonObj').attr('disabled', true);
                debugger;
                if ((count > index)) {
                    $('#BreakingService_' + (count) + '__StartDate').attr('disabled', false);
                    $('#BreakingService_' + (count) + '__EndDate').attr('disabled', false);
                    $('#BreakingService_' + (count) + '__Remarks').attr('disabled', false);
                }


                datePicker();
            }
        }

    }
        //---------------------------------

        //make numbers only inputs.
        //$('.numbers-only').keypress(function (e) {
        //    if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
        //});
   // }
    //else {
    //    swal.fire('Limit Exceeded.Maximum level of point criteria is 5');
    //}
}

function DeleteRw(index) {
    debugger;


    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;
            var count = 0;
            count = $('tr[id^="row_OI_"]').length;
            if (count > 1) {
                $("#row_OI_" + index).remove();
                for (var j = index + 1; j < count; j++) {
                    var template = $('#row_OI_' + j).clone(); // clone the row
                    var start = $("#BreakingService_" + j + "__StartDate").val();
                    var end = $("#BreakingService_" + j + "__EndDate").val();
                    var remark = $("#BreakingService_" + j + "__Remarks").val();

                    $.each(template.find('input, textarea'), function () {
                        var name = $(this).attr('name');
                        if (typeof name != 'undefined') {
                            name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('name', name);
                        }
                        var id = $(this).attr('id');
                        if (typeof id != 'undefined') {
                            id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('id', id);
                        }
                        //---------------------------------For row Increment When onkeyup
                        var onkeyup = $(this).attr('onkeyup');
                        if (onkeyup !== undefined) {
                            onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onkeyup', onkeyup);
                        }
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('select'), function () {
                        var name = $(this).attr('name');
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                        var id = $(this).attr('id');
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('button[type=button]'), function () {
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });



                    $.each(template.find('.addObj'), function () {
                        $(this).attr("id", "Add_" + (j - 1));
                    });
                    $.each(template.find('.delObj'), function () {
                        $(this).attr("id", "delete_" + j - 1);
                    });
                    $.each(template.find('.editObj'), function () {
                        $(this).attr("id", "edit_" + j - 1);
                    });
                 

                    template.find('td[id=grid_slno]').text(j);
                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));



                  


                    //$.each(template.find('.hasDatepicker'), function () {
                    //    $(this).removeClass('hasDatepicker');
                    //});
                    //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                    //    dateFormat: "dd-M-yy", changeMonth: true,
                    //    changeYear: true
                    //});

                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));
                    $("#BreakingService_" + (j - 1) + "__StartDate").val(start);
                    $("#BreakingService_" + (j - 1) + "__EndDate").val(end);
                    $("#BreakingService_" + (j - 1) + "__Remarks").val(remark);
                   
                  
                  
                }
               
            }
           
          
            $('#Add_' + (index-1)).removeClass('hide');
            $('#delete_' + (index - 1)).removeClass('hide');
            $('#edit_' + (index - 1)).removeClass('hide');
            $('#BreakingService_' + (index - 1) + '__StartDate').attr('disabled', false);
            $('#BreakingService_' + (index - 1) + '__EndDate').attr('disabled', false);
            $('#BreakingService_' + (index - 1) + '__Remarks').attr('disabled', false);
            if (index == 1) {
                $('#delete_' + (index-1)).addClass('hide');

            }

        }
        else {
            $("#BreakingService_0__Name").val("");
            $("#BreakingService_0__Point").val("");
            $("#BreakingService_0__Remarks").val("");
        }
        debugger;
     
     
    })
}


function SaveBreakingService(EmployeeGI) {
    debugger;
/*    if (validateRewards()) {*/
    debugger
    //count =$('tr[id^="row_OI_"]').length;
    //for (let i = 1; i <= count; i++) {
    //    $('#BreakingService_' + i + '__StartDate').removeAttr('disabled');
    //    $('#BreakingService_' + i + '__EndDate').removeAttr('disabled');
    //    $('#BreakingService_' + i + '__Remarks').removeAttr('disabled');
    //}
    $('.CommonObj').attr('disabled', false);
    console.log($('#SaveService').serialize());
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
                    url: '/PayRoll/GratuityProcessing/SaveBreakingService',
                    dataType: 'json',
                    data: $('#SaveService').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                onNextClick(EmployeeGI);
                            });
                        }
                        else
                            debugger;
                            swal.fire("Saved Successfully");
                      
                            onNextClick(EmployeeGI);
                       

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

    //}
    //else {
    //    return false;
    //}
}





function SavePaymentDetails() {
    debugger;
        if (validatePayment()) {
    debugger
    //console.log($('#rewardsSave').serialize());
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
                url: '/PayRoll/GratuityProcessing/SavePaymentDetails',
                dataType: 'json',
                data: $('#SaveService').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            debugger;
                            var url = '/PayRoll/GratuityProcessing/Index';
                            window.location.href = url;
                        });
                        debugger;


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
    else {
        return false;
    }
}





function validatePayment() {
    var i = 0;
    if (!validateById('#GratuityPayment_CheckNumber', 'checknumber')) {
        i++;
    }
    if (!validateById('#GratuityPayment_CheckDate', 'Chequedate')) {
        i++;
    }
    if (!validateById('#GratuityPayment_HandoverTo', 'handoverTo')) {
        i++;
    }
    if (!validateById('#GratuityPayment_HandoverDate', 'handoverDate')) {
        i++;
    }

 
    if (i == 0)
        return true;
    else
        return false;
}



function ApproveGratuityDetails(EmployeeGI) {
    debugger;
    /*    if (validateRewards()) {*/
    
    //console.log($('#rewardsSave').serialize());
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
                url: '/PayRoll/GratuityProcessing/ApproveGratuityDetails',
                dataType: 'json',
                data: $('#SaveService').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            debugger;
                            GetPaymentDetails(EmployeeGI);

                        });
                        debugger;


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

    //}
    //else {
    //    return false;
    //}
}

function onNextClick(EmployeeGI) {
    $.ajaxSetup({
        async: false
    });
    debugger
    //$('#breakingServiceList').addClass(hide)
    //$('#detailsApprove').removeClass(hide)
    //$('#detailsApprove').removeClass(hide)
    //var url = "/PayRoll/GratuityProcessing/GetGratuityApprovalDetails?EmployeeGI=" + EmployeeGI
    //window.location.url;
    //$('#breakingServiceList').addClass(hide)
    //$('#detailsApprove').removeClass(hide)





    $.ajax({
        type: 'GET',
        url: '/PayRoll/GratuityProcessing/GetGratuityDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
               /* $('body').append('<div class="form-overlay"></div>');*/
                $('#serviceList').addClass('hide');
              /*  $('#detailsApprove').removeClass('hide');*/
                $('#detailsApprove').html("");
                $('#detailsApprove').html(result);
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


function GetPaymentDetails(EmployeeGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/PayRoll/GratuityProcessing/GetPaymentDetails?EmployeeGI=' + EmployeeGI,
        dataType: 'html',
        data: {  },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                /* $('body').append('<div class="form-overlay"></div>');*/
                $('#serviceList').addClass('hide');
                $('#detailsApprove').addClass('hide');
               /* $('#paymentDetails').removeClass('hide');*/
                $('#paymentDetails').html("");
                $('#paymentDetails').html(result);
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
    }
}
function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
  


    var PARAM = 'data=' + filterdata;
    var TableLength = $('#EmployeeList_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/GratuityProcessing/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        //url = '/Custom/CautionDeposit/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function onStepCircleClick(StepperName) {
    debugger;
    if (StepperName == "Breaking_Service") {
        $('#detailsApprove').addClass('hide');
        $('#serviceList').removeClass('hide');
        $('#paymentDetails').addClass('hide');
    }
    else if (StepperName == "Approval") {
        $('#paymentDetails').addClass('hide');
        $('#detailsApprove').removeClass('hide');
        $('#serviceList').addClass('hide');
    }

}