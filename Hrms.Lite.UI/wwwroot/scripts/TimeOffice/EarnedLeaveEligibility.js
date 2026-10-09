
function getLeave(sender) {
    debugger;
    selectedLgroup = $(sender).val();

    $.getJSON("/TimeOffice/EarnedLeaveEligibility/GetApplicableLeaves", { LeaveGroup: selectedLgroup }, function (data) {
        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Select Leave--</option');
        for (var i = 0; i < data.length; i++) {
            $('#Leave_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    fillData();
}
function validateEarnedLeaveEligibility() {
    debugger;
    var i = 0;
    if (!validateById('#Month_Code', 'month_Id_LvCrdt'))
        i++;
    if (!validateById('#Leave_Code', 'leaveId_LvCrdt'))
        i++;

    if (i == 0)
        return true;
    else
        return false;
}
function EarnedLeaveEligibilitySave() {
    debugger;
    if (validateEarnedLeaveEligibility()) {
        var Leave = $('#Leave_Code').val();
        var Month = $('#Month_Code').val();
        debugger
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
                    url: '/TimeOffice/EarnedLeaveEligibility/EarnedLeaveEligibilitySave',
                    dataType: 'json',
                    data: { Leave: Leave, Month: Month },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                fillData();

                            });

                        }
                        else {
                            swal.fire(result.message);
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

}
function ApplyFilter(mode) {
    debugger;

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
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}
//function ExportToExcel() {
//    debugger;


//    var Leave = $('#Leave_Code').val();
//    var Month = $('#Month_Code').val();


//    var TableLength = $('#EarnedLeaveEligibilityList_Count').val();
//    if (TableLength > 0) {
//        url = "/TimeOffice/EarnedLeaveEligibility/ExportToExcel?totRecords=" + TableLength + "&Month=" + Month + "&Leave=" + Leave;

//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }

//}



function ExportToExcel() {
    debugger;

    var filter = $('#BasicFilter').serialize();

    var Leave = $('#Leave_Code').val();
    var Month = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text();
    var LeaveName = $('#Leave_Code option:selected').text();
    var searchValue = $('#dataTableSearch').val();

    var PARAM = 'data=' + filter;
    var TableLength = $('#EarnedLeaveEligibilityList_Count').val();

    if (TableLength > 0) {
        var url = "/TimeOffice/EarnedLeaveEligibility/ExportToExcel?totRecords=" + TableLength +
            "&searchValue=" + encodeURIComponent(searchValue) +
            "&Month=" + encodeURIComponent(Month) +
            "&Leave=" + encodeURIComponent(Leave) +
            "&LeaveName=" + encodeURIComponent(LeaveName) +
            PARAM +
            "&MonthName=" + encodeURIComponent(MonthName); // <-- Add this line

        window.location.href = url;
    } else {
        swal.fire("No data found to export");
    }
}

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var Leave = $('#Leave_Code').val();
    var Month = $('#Month_Code').val();

    var dataTable = $("#Periodic_Leave_Credit_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/EarnedLeaveEligibility/GetEarnedLeaveEligibilityList",
            "type": "POST",

            "datatype": "json",

            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                         '&Leave=' + Leave + '&Month=' + Month + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                console.log(d.data);
                $('#EarnedLeaveEligibilityList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                }

                return d.data;
               
            }
      
        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },
            { "data": "leaveGroup.name" },
            { "data": "doj" },
            { "data": "elOpening" }



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
