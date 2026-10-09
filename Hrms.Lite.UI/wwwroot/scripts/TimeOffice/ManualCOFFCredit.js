


function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    //group = $('#LeaveGroup_Code').val();

   var group = 0;
    var dataTable = $("#manualCOFFCreditTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/ManualCOFFCredit/GetManualCOFFCreditList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    debugger;

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeaveGroup=' + group +"&Form=Entry" + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#ManualCOFFCreditList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                    d.data[i].category = '<div class="d-flex justify-content-center"><a data-bs-toggle="tooltip" title="Edit" id="physicallocationedit" type="button" class="btn btn-icon btn-edit icon-only slider-btn d-flex justify-content-center "style="float:none;" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></div>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "17%" },
            { "data": "department.name", "width": "17%" },
            { "data": "location.name", "width": "12%" },
            { "data": "balance", "width": "7%" },
            { "data": "category", "width": "8%" },


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

function onEditClick(EmployeeGI) {
    preventENterFlag = 0;
     $.ajax({

        type: 'GET',
         url: '/TimeOffice/ManualCOFFCredit/GetEditDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editManualCOFFCredit').html("");
                $('#editManualCOFFCredit').html(result);
            }
            hideSpinner();


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });


}

function EmployeeWiseCreditSave(form) {
       
    debugger;
    var i = 0;
    if (!validateById('#AttendanceDayStatus_Code', 'attendancedaystatus')) {
        i++;
    }
    if (!validateById('#EntitledDate', 'entitleddate')) {
        i++;
    }

    if (i == 0) {
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
                    url: '/TimeOffice/ManualCOFFCredit/Edit',
                    dataType: 'json',
                    data: $('#manualCOFFCreditSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                fillData();
                                CloseSlider('manualCOFFCreditEdit_');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
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
    else {
        return false;
    }
}

function ExportToExcel() {
    debugger;

        debugger;
        var ManualCOFFCreditListLength = $('#ManualCOFFCreditList_Count').val();
        if (ManualCOFFCreditListLength > 0) {
            url = "/TimeOffice/ManualCOFFCredit/ExportToExcel?totRecords=" + ManualCOFFCreditListLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
}
function validateDrop() {
    var i = 0;
   
    if (!validateById('#LeaveGroup_Code', 'group')) {
        i++;
    }
   
    if (i == 0)
        return true
    else
        return false;
}
function ApplyFilter(mode) {
    debugger
    if (validateDrop()) {
        var tabIndex = $('#Tabindex').val();
        var search = $('#SearchId').val();

        $("#SearchId").val($('#FilterList_Code').val())
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
}
function datepicker() {
    $('.kt_datepicker_8, .kt_datepicker_8').datepicker({

    });

}
