function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var dataTable = $("#AllListtable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/EmployeeVsBankDetailsApproval/GetList",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                var searchValue = $('#dataTableSearch').val();
                // Add custom parameters to the data object
                d.searchValue = searchValue;
                // Parse filter1 into key-value pairs and add them to d
                filter1.split('&').forEach(function (pair) {
                    var parts = pair.split('=');
                    var key = decodeURIComponent(parts[0]);
                    var val = decodeURIComponent(parts[1] || '');
                    d[key] = val;
                });
                return d; // Will be sent as form data (x-www-form-urlencoded)
            },

            "dataSrc": function (d) {
                $('#EmployeeVsBankDetailsList_Count').val(d.recordsTotal);
                if (d.data.length != 0) {
                    debugger;
                }
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].actions = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onViewClick(' + "'" + d.data[i].employeeGI + "'" + ')"  data-bs-toggle="tooltip" title = "View" type = "button" class="view_btn" ></a></span>'
                }

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
            /*          { "data": "payMode" },*/
            { "data": "bankName" },
            { "data": "accountNo" },
            { "data": "ifsc" },
            { "data": "bankBranch" },
            { "data": "actions" },
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

function onViewClick(EmpGI) {
    debugger;

    $.ajax({

        type: 'GET',
        url: '/Databank/EmployeeVsBankDetailsApproval/ViewDetails',
        dataType: 'html',
        data: { EmployeeGI: EmpGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EditSlider').html("");
                $('#EditSlider').html(result);
            }
        },

        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}

function onApproveOrRejectClick(EmpGI, mode) {
    debugger
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
                url: '/Databank/EmployeeVsBankDetailsApproval/ApproveOrReject',
                dataType: 'json',
                data: { EmployeeGI: EmpGI, mode: mode },
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('EditBankdiv');
                            fillData();
                        });
                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
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
    var TableLength = $('#EmployeeVsBankDetailsList_Count').val();
    if (TableLength > 0) {
        url = "/Databank/EmployeeVsBankDetailsApproval/ExportToExcel?TotalRow=" + TableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}



function showDecryptDocument(RequestGI, DocType) {
    debugger
    var url = "/Databank/EmployeeVsBankDetailsApproval/ShowDoc?RequestGI=" + RequestGI + "&DocType=" + DocType;
    window.open(url, "_blank");
}