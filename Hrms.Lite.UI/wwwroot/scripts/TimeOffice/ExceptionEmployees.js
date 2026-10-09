var prevTab = "tab1";
var currentTab = 0;
var currentTabName ="Normal";

function tabClick(Tab, BasedOn) {
    debugger;

    currentTab = BasedOn;

    currentTabName = Tab;
    //if (BasedOn == '1') {
    //    $('#exceptn').addClass('hide');
    //    $('#normall').removeClass('hide');
    //    $('#confirmbtn').html('Confirm');
    //}
    //else {
    //    $('#normall').addClass('hide');
    //    $('#exceptn').removeClass('hide');
    //    $('#confirmbtn').html('Confirm');
    //}
    //if (BasedOn == 1) {
    //    debugger
    //    $('.Month_1').removeClass('hide');
    //    $('.Month_0').addClass('hide');
    //}
    //else {

    //    debugger
    //    $('.Month_1').addClass('hide');
    //    $('.Month_0').removeClass('hide');
    //}
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");


    // alert(currentTab);
    
        fillData();

    prevTab = Tab;
}


function ApplyFilter(mode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}


function fillData() {

    debugger;

    if (currentTab == 0) {

        var MonthCode = $('#Month_Code').val();
        var Filter = $('#BasicFilter').serialize();
        var temp = Filter;

        var dataTable = $("#ExceptionEmployeesListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/ExceptionEmployees/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        console.log(d);
                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&MonthCode=' + MonthCode + '&BasedOn=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ExceptionEmployeesList_Count').val(d.recordsTotal);
                    debugger;
                    for (var j = 0, ien = d.data.length; j < ien; j++) {


                        d.data[j].remarks = '<input type = "text" id = "rmk_' + j + '" class="form-control form-control-sm" placeholder = "Remarks" value="' + d.data[j].remarks + '">';



                        d.data[j].action = '<a  class=" btn btn-icon btn-edit icon-only slider-btn " onclick="onAddClick(' + "'" + d.data[j].employeeGI + "'" + "," + "'ADD'" + "," + MonthCode + "," + j + ')"  >  <i class="fa fa-plus" aria-hidden="true"></i></a > ';



                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "11%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "department", "width": "15%" },
                { "data": "designation", "width": "16%" },
                { "data": "location", "width": "14%" },
                //  { "data": "grade", "width": "12%" },
                { "data": "remarks", "width": "12%" },
                { "data": "action", "width": "12%" },

            ],



            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

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


    }

    else {

        var MonthCode = $('#Month_Code').val();
        var Filter = $('#BasicFilter').serialize();
        var temp = Filter;
      



        var dataTable = $("#ExceptionEmployeesListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/ExceptionEmployees/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        console.log(d);
                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&MonthCode=' + MonthCode + '&BasedOn=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ExceptionEmployeesList_Count').val(d.recordsTotal);
                    debugger;
                    for (var j = 0, ien = d.data.length; j < ien; j++) {

                        if (d.data[j].remarks == '')
                        {
                            d.data[j].remarks = '<input type = "text" id = "rmk_' + j + '" class="form-control form-control-sm" placeholder = "Remarks" value="' + d.data[j].remarks + '">';

                        }
                        else
                        {
                            d.data[j].remarks = d.data[j].remarks;
                        }

                        if (d.data[j].applicability == 0) {
                            d.data[j].action = '<a  class=" btn btn-icon btn-edit icon-only slider-btn " onclick="onAddClick(' + "'" + d.data[j].employeeGI + "'" + "," + "'ADD'" + "," + MonthCode + "," + j + ')"  >  <i class="fa fa-plus" aria-hidden="true"></i></a > ';


                        }
                        else {
                            d.data[j].action = '<a  class=" btn btn-icon  icon-only mobile-full-width btn-delete " onclick="onAddClick(' + "'" + d.data[j].employeeGI + "'" + "," + "'REMOVE'" + "," + MonthCode + "," + j + ')"  ><i class="icon-xl far fa-trash-alt"></i></a > ';
                        }

                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "11%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "department", "width": "15%" },
                { "data": "designation", "width": "16%" },
                { "data": "location", "width": "14%" },
                //  { "data": "grade", "width": "12%" },
                { "data": "remarks", "width": "12%" },
                { "data": "action", "width": "12%" },

            ],






            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

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

    }

}


//function fillData() {

//    debugger;

    
//    var MonthCode = $('#Month_Code').val();
//    var Filter = $('#BasicFilter').serialize();
//    var temp = Filter;
//    var BasedOn = $('input[name="BasedOn"]:checked').val();



//        var dataTable = $("#ExceptionEmployeesListTable").DataTable({

//            "pagingType": 'full_numbers',
//            "destroy": 'true',
//            "ajax": {
//                "url": "/TimeOffice/ExceptionEmployees/GetList",
//                "type": "POST",


//                "datatype": "json",


//                'data':

//                    function (d) {
//                        temp = '';

//                        var start = d.start;
//                        console.log(d);
//                        var searchValue = $('#dataTableSearch').val();
//                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
//                            '&MonthCode=' + MonthCode + '&BasedOn=' + BasedOn + '&dummy=dummy';
//                        d.filter = temp;

//                        return JSON.stringify(d);
//                    },


//                "dataSrc": function (d) {

//                    $('#ExceptionEmployeesList_Count').val(d.recordsTotal);
//                    debugger;
//                    for (var j = 0, ien = d.data.length; j < ien; j++) {

//                        if (d.data[j].remarks == '')
//                        {
//                            d.data[j].remarks = '<input type = "text" id = "rmk_' + j + '" class="form-control form-control-sm" placeholder = "Remarks" value="' + d.data[j].remarks + '">';

//                        }
//                        else
//                        {
//                            d.data[j].remarks = d.data[j].remarks;
//                        }

//                        if (d.data[j].applicability == 0) {
//                            d.data[j].action = '<a  class=" btn btn-icon btn-edit icon-only slider-btn " onclick="onAddClick(' + "'" + d.data[j].employeeGI + "'" + "," + "'ADD'" + "," + MonthCode + "," + j + ')"  >  <i class="fa fa-plus" aria-hidden="true"></i></a > ';

                           
//                        }
//                        else {
//                            d.data[j].action = '<a  class=" btn btn-default mobile-full-width slider-btn " onclick="onAddClick(' + "'" + d.data[j].employeeGI + "'" + "," + "'REMOVE'" + "," + MonthCode + "," + j + ')"  >Remove</a > ';
//                        }

//                    }
//                    return d.data;
//                }

//            },

//            "columns": [

//                { "data": "employeeID", "width": "11%" },
//                { "data": "employeeName", "width": "20%" },
//                { "data": "department", "width": "15%" },
//                { "data": "designation", "width": "16%" },
//                { "data": "location", "width": "14%" },
//              /*  { "data": "grade", "width": "12%" },*/
//                { "data": "remarks", "width": "12%" },
//                { "data": "action", "width": "12%" },

//            ],






//            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",

//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {

//                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                sLengthMenu: "_MENU_",
//                paginate: {


//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'




//                }
//            }



//        });



//        $('.ihits-table table').removeClass("dataTable no-footer");


//        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.next').attr("title", "Next");
//        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.previous').attr("title", "Previous");
//        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.first').attr("title", "First");
//        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.last').attr("title", "Last");


//        $("#dataTableSearch").on("keyup search input paste cut", function () {
//            dataTable.search(this.value).draw();

//        });

//        $('.search-slider-inner').click(function () {
//            //dataTable.search('').draw();
//            $('#dataTableSearch').val('');
//            $('#dataTableSearch').focus();
//        });

//        dataTable.on('draw', function () {
//            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.next').attr("title", "Next");
//            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.previous').attr("title", "Previous");
//            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.first').attr("title", "First");
//            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.last').attr("title", "Last");

//        });

//        $.fn.DataTable.ext.pager.numbers_length = 5;
 
//}

function onAddClick(EmployeeGI, Type, MonthCode,j) {
    debugger

    var SetType = '';
    var remarks = $('#rmk_' + j + '').val();
    if (Type == 'ADD') {
        if (remarks == "" || remarks == null) {
            swal.fire("Please enter Remarks ");
            fillData();
            return false;
        }
    }

    var BasedOn = $('input[name="BasedOn"]:checked').val();

    if (BasedOn == 0) {
        SetType = 'Added';
    }
    else {
        SetType = 'Remove'

    }

    swal.fire({
        text: "Do you want to " + Type + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/ExceptionEmployees/AddEmployee?EmployeeGI=' + EmployeeGI,
                dataType: 'json',
                data: { Type: Type, MonthCode: MonthCode, remarks: remarks},
                success: function (result) {
                    if (result.success == true) {
                        //swal.fire(result.message).then(function () {

                        //    ScheduleList();


                        //});
                        swal.fire(SetType + " Successfully");
                        fillData();
                        debugger;


                    }

                    else {
                        swal.fire(result.message).then(function () {
                            /* ScheduleList();*/
                        });
                    }

                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);

                }
            });
        }
        else {
            return false;
        }
    })
}

function onAddBatchUpdate(MonthCode, BasedOn, TypeValue)
{
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;
    debugger;
    $.ajax({

        type: 'POST',
        url: '/TimeOffice/ExceptionEmployees/BatchUpdate?MonthCode=' + MonthCode + '&BasedOn=' + BasedOn + '&TypeValue=' + TypeValue,
        dataType: 'json',
        data: temp,
        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {

                //    ScheduleList();


                //});
                swal.fire("Updated Successfully");
                fillData();
                debugger;


            }

            else {
                swal.fire(result.message).then(function () {
                    /* ScheduleList();*/
                });
            }

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}


function onBatchUpdateClick() {
    debugger

    var MonthCode = $('#Month_Code').val();
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;
    var title = '';
    var BasedOn = $('input[name="BasedOn"]:checked').val();
    if (BasedOn == 0) {
        title = "Do you want to Update?"
        swal.fire({
            title: title,
            //text: "Reason to unlock",
            inputLabel: 'Remark',
            input: 'text',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok',

            preConfirm: (value) => {
                if (!value) {
                    Swal.showValidationMessage(
                        'Remark is mandatory'
                    )
                }
            }

        }).then((result) => {
            if (result.value) {
                console.log("Result: " + result.value);
                this.onAddBatchUpdate(MonthCode, BasedOn, result.value);
            }

        });
    }


    else {

        var TypeValue = 'delete';
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
                    url: '/TimeOffice/ExceptionEmployees/BatchUpdate?MonthCode=' + MonthCode + '&BasedOn=' + BasedOn + '&TypeValue=' + TypeValue,
                    dataType: 'json',
                    data: temp,
                    success: function (result) {
                        if (result.success == true) {
                            //swal.fire(result.message).then(function () {

                            //    ScheduleList();


                            //});
                            swal.fire("Updated Successfully");
                            fillData();
                            debugger;


                        }

                        else {
                            swal.fire(result.message).then(function () {
                                /* ScheduleList();*/
                            });
                        }

                    },


                    error: function (xhr, textStatus, errorThrown) {
                        debugger
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);

                    }
                });
            }
            else {
                return false;
            }
        })
    }
    
}


function ExportToExcel() {
    debugger
    var searchValue = $('#dataTableSearch').val();
    var MonthCode = $('#Month_Code').val();
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;
    var BasedOn = currentTab
    if (BasedOn == 0) {
        Type = 'Non Exceptional';
    }
    else {
        Type = 'Exceptional';
    }
    
    var TableLength = $('#ExceptionEmployeesList_Count').val();
    if (TableLength > 0)
    {
        url = "/TimeOffice/ExceptionEmployees/ExportToExcel?MonthCode=" + MonthCode + '&data=' + temp + '&TableLength=' + TableLength + '&BasedOn=' + BasedOn + '&Type=' + Type + '&searchValue=' + searchValue;
            window.location.href = url;
    }
        else {
            swal.fire(NoDataFoundMsg);
  
            }
}


function validateMonth() {
    var i = 0;
    debugger;
    if (!validateById('#Month_Code', 'type')) {
        i++;
    }

    if (i == 0) {
        /*$('#Excel_FileData').trigger('click')*/
        var m = $('.bd-example-modal-lg');
        m.modal('show');

    }

}


function onImportExcelClick() {

    var MonthCode = $('#Month_Code').val();
    if (MonthCode == '') {
       
        swal.fire("Month is required ");
            return false;
      
    }

    var BasedOn = $('input[name="BasedOn"]:checked').val();

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#ExceptionEmployeeUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: "/TimeOffice/ExceptionEmployees/ImportFromExcel?BasedOn=" + BasedOn,
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                            //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                            //});
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


function getData() {

    debugger;

    var MonthCode = $('#Month1_Code').val();
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;


    var dataTable = $("#TransactionHistoryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TimeOffice/ExceptionEmployees/Getdatalist",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        console.log(d);
                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&MonthCode=' + MonthCode  + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {
                    debugger;
                    $('#TransactionHistoryList_Count').val(d.recordsTotal);
  
              
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "11%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "16%" },
                { "data": "location", "width": "14%" },
                { "data": "remarks", "width": "12%" },
              
            ],



            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

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

}

function HistoryExportToExcel() {
    debugger
    var MonthCode = $('#Month1_Code').val();
    var Filter = $('#BasicFilter').serialize();
    //var temp = Filter;
    

    var PARAM = '&data=' + Filter + '&MonthCode=' + MonthCode;
    var TableLength = $('#TransactionHistoryList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/ExceptionEmployees/HistoryExportToExcel?' + PARAM + '&TableLength=' + TableLength ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");

    }
}


