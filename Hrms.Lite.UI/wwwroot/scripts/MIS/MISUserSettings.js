var currentTab = 'ACTIVE';
var prevTab = 'ActiveTab';
function fillData() {

    debugger;
    if (currentTab == 'ACTIVE') {
        var Tab = 1;
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;


        /*if (MonthCode != '' && AllowanceCode != '') {*/
        var dataTable = $("#MISUserTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/MIS/MISUserSettings/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&Tabindex=' + Tab +'&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    console.log(d)


                    debugger;

                    //$('#VariableAllowanceList_Count').val(d.recordsTotal);
                    //$('#totEmpCnt').html(d.recordsTotal);
                    //if (d.data != "") {
                    //    $('#empWithAmt').html(d.data[0].employeeswithAmount);
                    //    $('#totAmt').html(d.data[0].totalAmount);
                    //}
                    //if ($('#VariableAllowanceList_Count').val() == '0')
                    //    $('.textDisplay').addClass('footerDiv');
                    //else
                    //    $('.textDisplay').removeClass('footerDiv');

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        //d.data[i].deduction = '<a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="EmployeeWiseTDSConfirmationSave(' + "'" + d.data[i].employeeGI + "'" + ')"></i></a> <a type="button" data-bs-toggle="tooltip" title = "Cancel"  class="btn btn-icon btn-delete icon-only btn-bg-info" onclick="onCancelClick(' + "'" + +"'" + ')"> <i class="icon-xl fas fa-times"></i></a>

                        //d.data[i].amount = '<span id = "amtLabel_' + i + '">' + d.data[i].amount + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyNumberKey(event)"></span>'
                        d.data[i].status = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Delete" class="btn btn-icon  icon-only mobile-full-width  btn-delete" onclick = "onDeleteClick(' + i + ',' + "'" + d.data[i].employeeCode + "'" + ')">   <i class="icon-xl far fa-trash-alt"></i> </a></span>'
                        //d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><input class="form-control form-control-sm"id="correctedamt" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)">'

                        // d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'

                           
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "fullName" },
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "category" },
                //{ "data": "gender" },
                { "data": "status" },



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
    else {
        debugger;
        var Tab = 0;
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;



        /*if (MonthCode != '' && AllowanceCode != '') {*/
        var dataTable = $("#MISUserTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/MIS/MISUserSettings/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&Tabindex=' + Tab + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    console.log(d)


                    debugger;

                    //$('#VariableAllowanceList_Count').val(d.recordsTotal);
                    //$('#totEmpCnt').html(d.recordsTotal);
                    //if (d.data != "") {
                    //    $('#empWithAmt').html(d.data[0].employeeswithAmount);
                    //    $('#totAmt').html(d.data[0].totalAmount);
                    //}
                    //if ($('#VariableAllowanceList_Count').val() == '0')
                    //    $('.textDisplay').addClass('footerDiv');
                    //else
                    //    $('.textDisplay').removeClass('footerDiv');

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        //d.data[i].deduction = '<a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="EmployeeWiseTDSConfirmationSave(' + "'" + d.data[i].employeeGI + "'" + ')"></i></a> <a type="button" data-bs-toggle="tooltip" title = "Cancel"  class="btn btn-icon btn-delete icon-only btn-bg-info" onclick="onCancelClick(' + "'" + +"'" + ')"> <i class="icon-xl fas fa-times"></i></a>

                        //d.data[i].amount = '<span id = "amtLabel_' + i + '">' + d.data[i].amount + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyNumberKey(event)"></span>'
                        d.data[i].status = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Save" class=" btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeCode + "'" + ')"> <i class="icon-xl fas fa fa-save new-siz-mater"></i> </a></span>'
                        //d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><input class="form-control form-control-sm"id="correctedamt" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)">'

                        // d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'

                           
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "fullName" },
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "category" },
                //{ "data": "gender" },
                { "data": "status" },



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

    //}
    //else {
    //    return false;
    //}
 
}
function onEditClick(i) {
    debugger;
   
    // //if (isselected == null) {
    // //    isselected = 1;
    // $('#hideAmount_' + index).hide();
    // //$('#row_' + index).append($('<input class="form-control form-control-sm"id="correctedamt" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)">'));

    // $('#correctedamt').val(origAmt);
    // $('#updatecancel_' + index).css("display", "block");
    // $('#edit_' + index).css("display", "none");

    // $('#correctedamt_' + i).css("display", "block");
    //moveCursorToEnd('correctedamt');
    // /*  }*/


    //$('#newAmount_' + i).val($('#VariableAllowanceList_' + i + '__Amount').val());

    //$('#hideAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    //'$('#amtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');


}
function onUpdateClick(index, EmployeeCode) {
    debugger;
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
                url: '/MIS/MISUserSettings/Save',
                dataType: 'json',
                data: { EmployeeCode: EmployeeCode},
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


function OnTabClick(Tab, TabIndex) {
    debugger;
    $.ajaxSetup({ async: false });
   
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsMISUserSettings').val(currentTab);

    fillData();

    prevTab = Tab;
}

function onDeleteClick(index,EmployeeCode) {

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
         $.ajax({
            type: 'POST',
            url: '/MIS/MISUserSettings/Delete',
            dataType: 'json',
            data: { EmployeeCode: EmployeeCode },
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
