
function getLeave(sender) {
    debugger;
    selectedLgroup = $(sender).val();

    $.getJSON("/TimeOffice/PeriodicLeaveCredit/GetApplicableLeaves", { LeaveGroup: selectedLgroup }, function (data) {       
       
        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Select Leave--</option');
        for (var i = 0; i < data.length; i++) {
            $('#Leave_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        } 

    });
}
function fillLeaveMonth() {
    debugger;
    var selectedValue = $('#LeavePeriodDropdown').val();


    $.getJSON("/TimeOffice/PeriodicLeaveCredit/GetApplicableLeavesPeriod", { LeavePeriod: selectedValue }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select Month--</option');
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}
function validatePeriodicLeaveCredit() {
    debugger;
    var i = 0;
    if (!validateById('#Month_Code', 'month_Id_LvCrdt'))
        i++;
    if (!validateById('#LeavePeriodDropdown', 'leavePeriod_LvCrdt'))
        i++;
    if (!validateById('#LeaveGroup_Code', 'grpLeave_LvCrdt'))
        i++;
    if (!validateById('#Leave_Code', 'leaveId_LvCrdt'))
        i++;

    if (i == 0)
        return true;
    else 
        return false;
    }
function PeriodicLeaveCreditSave() {
    debugger;
    if (validatePeriodicLeaveCredit()) {
        /* var LeavePeriod = $('#LeavePeriod_Code').val();*/
        var LeavePeriod = $('#LeavePeriodDropdown').val();
        var LeaveGroup = $('#LeaveGroup_Code').val();
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
                    url: '/TimeOffice/PeriodicLeaveCredit/PeriodicLeaveCreditSave',
                    dataType: 'json',
                    data: { LeavePeriod: LeavePeriod, LeaveGroup: LeaveGroup, Leave: Leave, Month: Month},
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
function ExportToExcel() {
    debugger;


    //var LeavePeriod = $('#LeavePeriod_Code option:selected').text();
    //var LeaveGroup = $('#LeaveGroup_Code option:selected').text();
    //var Leave = $('#Leave_Code option:selected').text();
    //var Month = $('#Month_Code option:selected').text();
    //var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeavePeriod = $('#LeavePeriodDropdown').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    var Month = $('#Month_Code').val();
    var LeavePeriodName = $('#LeavePeriodDropdown option:selected').text();
    var LeaveGroupName = $('#LeaveGroup_Code option:selected').text();
    var LeaveName = $('#Leave_Code option:selected').text();
    var MonthName = $('#Month_Code option:selected').text();

    var TableLength = $('#PeriodicLeaveCreditList_Count').val();
    if (TableLength > 0) {
        url = "/TimeOffice/PeriodicLeaveCredit/ExportToExcel?totRecords=" + TableLength + "&LeavePeriod=" + LeavePeriod + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&Month=" + Month
            + "&LeavePeriodName=" + LeavePeriodName + "&LeaveGroupName=" + LeaveGroupName + "&LeaveName=" + LeaveName + "&MonthName=" + MonthName;

        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    //var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeavePeriod = $('#LeavePeriodDropdown').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    var Month = $('#Month_Code').val();

    var dataTable = $("#Periodic_Leave_Credit_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/PeriodicLeaveCredit/GetPeriodicLeaveCreditList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                        '&LeavePeriod=' + LeavePeriod + '&LeaveGroup=' +
                        LeaveGroup + '&Leave=' + Leave + '&Month=' + Month + "&Form=Entry" +  '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



                $('#PeriodicLeaveCreditList_Count').val(d.recordsTotal);
               

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },
            { "data": "leaveCredit" },
           


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

    //setTimeout(tooltipHover, 500);






}
