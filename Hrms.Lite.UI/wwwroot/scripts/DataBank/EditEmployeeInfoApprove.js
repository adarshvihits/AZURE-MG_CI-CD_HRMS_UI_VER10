function fillData() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var search = $('#SearchId').val();
    $("#SearchId").val($('#FilterCombo_Code').val())

    var temp = filter;

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#EditEmpInfoApprove").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/EditEmployeeInfoApprove/GetEditEmployeeInfoApprove",
            "type": "POST",

            "datatype": "json",

            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +  '&searchID=' + search + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#EditInfoList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].expectedDOJ = _GetFormattedDate(d.data[i].expectedDOJ);
                    d.data[i].editDate = _GetFormattedDate(d.data[i].editDate);

                    d.data[i].fullName = '<td data-label="Candidate Name" ><a onclick="onApproveClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].logGI + "'" + ',' + "'" + i + "'" + ',' + "'" + d.data[i].makerChecker + "'" + ')">' + d.data[i].fullName + '</a></td>'

                }

                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "14%" },
            { "data": "expectedDOJ", "width": "7%" },
            { "data": "editDate", "width": "6%" },
            
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

function onFilterComboChange(SearchId) {
    debugger;
    /* showSpinner();*/
    //var url = "/DataBank/EditEmployeeInfo/GetList";
    //$("." + prevTab).removeClass("active");
    //$(".all").addClass("active");
    var search = SearchId.value;
    $('#FilterCombo_Code').val(search);
    $('.filterDropDown').val(search);

    ApplyFilter('apply');
    //$('#editEmployeeList').load(url, { TabIndex: currentTab, SearchId: search }, function () { });  
}


function onApproveClick(EmployeeGI, LogGI, index, makerChecker) {
    debugger;
    if (makerChecker == 'true')
        swal.fire("MAKER AND CHECKER SHOULD BE DIFFERENT");
    else {
        var url = "/DataBank/EditEmployeeInfoApprove/Approve?EmployeeGI=" + EmployeeGI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}


function confirmApprove() {
    debugger
    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/DataBank/EditEmployeeInfoApprove/Approve',
                dataType: 'json',
                data: $('#Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Approved Successfully").then(function () {
                            var url = '/DataBank/EditEmployeeInfoApprove/Index?';
                            window.location.href = url;
                        });
                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and Checker should be different");
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


function confirmReject() {
    debugger
    swal.fire({
        text: "Do you want to Reject?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/DataBank/EditEmployeeInfoApprove/Reject',
                dataType: 'json',
                data: $('#Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            var url = '/DataBank/EditEmployeeInfoApprove/Index?';
                            window.location.href = url;
                        });
                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and Checker should be different");
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

function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
   
    var PARAM = 'data=' + filter ;
    var TableLength = $('#EditInfoList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/EditEmployeeInfoApprove/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}