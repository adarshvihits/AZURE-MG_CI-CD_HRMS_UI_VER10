
function ApplyFilter(mode) {
    debugger
    var GroupBy = $('#GroupBy').val();
    var Classification = $('#Classification').val();
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            GetEmpList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetEmpList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetEmpList();

    }

}

function fillData() {

    debugger;

    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;

    let radioVal;
    radioVal = $('input[name="Type"]:checked').val();

   


    var dataTable = $("#UHIDLIST").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/UHID/UHIDList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +'&Type=' + radioVal
                             + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                 

                    $('#UHIDList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].comments = '<a onmouseenter="tooltipHover()" onclick="onEdit_BatchClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'Edit'" + ')" data-bs-toggle="tooltip"  title = "Edit" class="btn btn-icon btn-edit icon-only" > <i class="icon-xl fas fa-pencil-alt"></i></a > ';
                        d.data[i].employeeName = '<div><a onclick="POPUP(\'' + d.data[i].employeeGI + '\')" data-bs-toggle="modal" data-target="#UHID_Popup">' + d.data[i].employeeName + '</a></div>';

                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "18%" },
                { "data": "designation.name", "width": "10%" },
                { "data": "department.name", "width": "10%" },
                { "data": "location.name", "width": "8%" },
                { "data": "doj", "width": "10%" },
                { "data": "uhidNumber", "width": "6%" },
             



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
function POPUP(EmployeeGI) {

    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/UHID/GetEmployeeDetails?EmployeeeGI= "+ EmployeeGI ,
      
        dataType: 'html',
      
        success: function (Data) {
            var m = $('#UHID_Popup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}
function UHIDSave() {
    debugger;
   
        //console.log($('#DesignationSave').serialize());
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
                    url: '/DataBank/UHID/Save',
                    dataType: 'json',
                    data: $('#UHIDSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                close_modal_popup('bd-example-modal-lg');
                                window.location.href = '/DataBank/UHID';

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                               
                            });
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
            else {
                return false;
            }
        })

  
}


function onExportToExcelClick() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    let radioVal;
    radioVal = $('input[name="Type"]:checked').val();

    var PARAM = 'data=' + filter ;
    var TableLength = $('#UHIDList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/UHID/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength + '&Type=' + radioVal;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function GetEmpList() {
    debugger;
    var filter = $('#BasicFilter').serialize();
   

    let radioVal;
    //if (document.getElementById('r1').checked) {
    //    radioVal = document.getElementById('r1').value;
    //} else {
    //    radioVal = document.getElementById('r2').value;
    //}
    radioVal = $('input[name="Type"]:checked').val();
    var dataTable = $("#UHIDLIST").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/UHID/UHIDListDetails",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + radioVal + '&dummy=dummy';
                        + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
               
            "dataSrc": function (d) {



                $('#UHIDList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].comments = '<a onmouseenter="tooltipHover()" onclick="onEdit_BatchClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'Edit'" + ')" data-bs-toggle="tooltip"  title = "Edit" class="btn btn-icon btn-edit icon-only" > <i class="icon-xl fas fa-pencil-alt"></i></a > ';
                    d.data[i].employeeName = '<div><a onclick="POPUP(\'' + d.data[i].employeeGI + '\')" data-bs-toggle="modal" data-target="#UHID_Popup">' + d.data[i].employeeName + '</a></div>';

                }
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "18%" },
            { "data": "designation.name", "width": "10%" },
            { "data": "department.name", "width": "10%" },
            { "data": "location.name", "width": "8%" },
            { "data": "doj", "width": "10%" },
            { "data": "uhidNumber", "width": "6%" },




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

}