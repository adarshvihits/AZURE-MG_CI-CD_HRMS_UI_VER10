

function fillData() {

    debugger;
    var LeavPrd = $('#LeavePeriod_Code').val();
    var LeavGrp = $('#LeaveGroup_Code').val();
    var Leav = $('#Leave_Code').val();

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;





    var dataTable = $("#LeaveDeleteTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveOpeningDelete/GetLeaveOpeningDeleteList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavPrd +
                        '&LeaveGroup=' + LeavGrp + '&Leave=' + Leav + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#LeaveOpeningList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].leave = '<a data-bs-toggle="tooltip" title="" id="deleteopening" type="button" class="btn btn-icon  icon-only mobile-full-width  btn-delete" data-bs-original-title="Delete" onclick="onDeleteClick('+"'"+d.data[i].employeeGI+"'"+')"><i class="icon-xl far fa-trash-alt"></i></a>'
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "17%" },
            { "data": "department.name", "width": "17%" },
            { "data": "location.name", "width": "13%" },
            { "data": "doj", "width": "9%" },
            { "data": "opening", "width": "9%" },
            { "data": "leave", "width": "6%" },


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





function onDeleteClick(employeeGI) {
    debugger;
    $.ajaxSetup({
        async: false
    });

    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
           
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LeaveOpeningDelete/Delete?employeeGI=' + employeeGI,
                dataType: 'json',
                data: $('#openingDelete').serialize(),
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire(result.message).then(() => {
                            fillData();

                        });
                    }
                    else {
                        debugger;
                        swal.fire(result.message);
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
    });
}

function OnDeleteAllClick() {
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
                url: '/TimeOffice/LeaveOpeningDelete/DeleteAll',
                dataType: 'json',
                data: $('#openingDelete').serialize(),
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire(result.message).then(() => {
                            fillData();

                        });
                    }
                    else {
                        debugger;
                        swal.fire(result.message);
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
    });
}



function GetList() {
    debugger;
    var LeavPrd = $('#LeavePeriod_Code').val();
    var LeavGrp = $('#LeaveGroup_Code').val();
    var Leav = $('#Leave_Code').val();

   /* showspinner();*/
    $.ajax({
       
        type: 'GET',
        url: '/TimeOffice/LeaveOpeningDelete/GetList',
        dataType: 'html',
        data: { LeavePeriod: LeavPrd, LeaveGroup: LeavGrp, Leave: Leav },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else
            {
                $('#editLeaveOpening').html("");
                $('#editLeaveOpening').html(result);
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








function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/TimeOffice/LeaveOpeningDelete/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#editLeaveOpening').html("");
            //            $('#editLeaveOpening').html(result);
            //        }
            //        hideSpinner();
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }

            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/TimeOffice/LeaveOpeningDelete/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#editLeaveOpening').html("");
            //            $('#editLeaveOpening').html(result);
            //        }
            //        hideSpinner();
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }

            //});
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //$.ajax({

        //    type: 'POST',
        //    url: '/TimeOffice/LeaveOpeningDelete/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#editLeaveOpening').html("");
        //            $('#editLeaveOpening').html(result);
        //        }
        //        hideSpinner();
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //    }
        //});

    }
}



function ExportToExcel() {
    debugger;

    var LeavPrdName = $('#LeavePeriod_Code option:selected').text();
    var LeavGrpName = $('#LeaveGroup_Code option:selected').text();
    var LeavName = $('#Leave_Code option:selected').text();
    var leaveOpeningTableLength = $('#LeaveOpeningList_Count').val();
    if (leaveOpeningTableLength > 0) {
        url = "/TimeOffice/LeaveOpeningDelete/ExportToExcel?LeavPrdName=" + LeavPrdName + "&LeavGrpName=" + LeavGrpName + "&LeavName=" + LeavName + "&TotalRec=" + leaveOpeningTableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}