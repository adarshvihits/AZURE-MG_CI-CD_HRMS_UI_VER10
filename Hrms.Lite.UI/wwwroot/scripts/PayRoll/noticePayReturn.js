




function fillData(status) {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var TabIndex = $('input[name="TabIndex"]:checked').val();


    /* if (Allowance != '') {*/
    var dataTable = $("#NoticePayReturntable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": true,
        "ajax": {
            "url": "/PayRoll/NoticePayReturn/GetNoticePayReturnList",
            "type": "POST",

            "datatype": "json",
            'data':
                function (d) {
                    var searchValue = $('#dataTableSearch').val();

                    // 🔥 IMPORTANT — PASS TAB INDEX HERE
                    d.filter = filter1 +
                        '&draw=' + d.draw +
                        '&start=' + d.start +
                        '&length=' + d.length +
                        '&searchValue=' + searchValue +
                        '&TabIndex=' + TabIndex +  // 🔥 CHANGED
                        '&dummy=dummy';

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#NoticePayReturnList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].des = '<div class="d-flex justify-content-center"><a data-bs-toggle="tooltip" title = "Edit" id = "physicallocationedit" class="btn btn-icon btn-edit icon-only slider-btn" onclick = "onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')" > <i class="icon-xl fas fa-pencil-alt"></i></a >'



                }
                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeId", "width": "8%" },
            { "data": "name", "width": "17%" },
            { "data": "designationName", "width": "24%" },
            { "data": "departmentName", "width": "24%" },
            { "data": "locationName", "width": "21%" },
            { "data": "des", "width": "6%" },
        ],




        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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

    /*setTimeout(datepicker, 500);*/

}












function NoticePayReturnSave(mode) {
    debugger;
    var TabIndex = $('input[name="TabIndex"]:checked').val();


    if (validateNPS()) {
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/NoticePayReturn/Save',
                    dataType: 'json',
                    data: $('#NoticePayReturnsave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddnewOrEditDiv');
                                //OnTabClick(prevTab, currentTab);
                                //tabClick(document.getElementById('tabsDropdownDepartment'))
                                fillData();

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
    else {
        return false;
    }
}




function onEditClick(employeeGI) {
    var TabIndex = $('input[name="TabIndex"]:checked').val();

    if (TabIndex == "1")
        $.ajax({

            type: 'GET',
            url: "/PayRoll/NoticePayReturn/Edit",
            dataType: 'html',
            data: { EmployeeGI: employeeGI },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
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
    else
        $.ajax({

            type: 'GET',
            url: "/PayRoll/NoticePayReturn/Create",
            dataType: 'html',
            data: { EmployeeGI: employeeGI },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
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





function confirmDelete(GI) {
    var TabIndex = 0;
    debugger
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
                url: '/PayRoll/NoticePayReturn/Delete',
                dataType: 'json',
                data: $('#NoticePayReturnsave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('AddnewOrEditDiv');
                            //OnTabClick(prevTab, currentTab);
                            //tabClick(document.getElementById('tabsDropdownDepartment'));
                            fillData();

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //var url = "/PayRoll/NoticePayReturn/GetNoticePayReturnList?TabIndex=" + TabIndex;
                            //$('#NoticePayReturnList').load(url, function () { });

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










function ExportToExcel() {
    debugger;
    var TabIndex = $('input[name="TabIndex"]:checked').val();

    var tlength = 0;
    if (TabIndex == 0) {
        var currentTabName = "Nor Registered";
        tlength = $('#NoticePayReturnList_Count').val();
    }
    else {
        var currentTabName = "Registered";
        tlength = $('#NoticePayReturnList_Count').val();
    }

    if (tlength != 0) {
        url = "/PayRoll/NoticePayReturn/ExportToExcel?TabIndex=" + TabIndex + "&currentTabName=" + currentTabName +
            '&data=' + $('#BasicFilter').serialize() + "&TotalRows=" + tlength;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}









function validateNPS() {
    var i = 0;
    if (!validateById('#PayedDate', 'PayedDate')) {
        i++;
    }
    if (!validateById('#Amount', 'Amount')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}
