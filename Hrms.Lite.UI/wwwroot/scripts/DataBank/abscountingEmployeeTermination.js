function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var dataTable = $("#AbscountingTerminationTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/AbscountingEmployeeTermination/ApplyFilter",
            "type": "POST",

            "datatype": "json",

            'data':

                function (d) {
                    temp = '';

                    var searchId = $('#Filter_Code').val();
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                console.log(d);

                $('#AbscondingReporttList_Count').val(d.recordsTotal)

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeName = '<a href="/DataBank/AbscountingEmployeeTermination/Create?EmployeeGI=' + d.data[i].employeeGI + '">' + d.data[i].employeeName + '</a>'
                    d.data[i].lastReportingDate = _GetFormattedDate(d.data[i].lastReportingDate);
                    d.data[i].lastPunchDate = _GetFormattedDate(d.data[i].lastPunchDate);
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "12%" },
            { "data": "doj", "width": "10%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "lastPunchDate", "width": "8%" },
            { "data": "lastProcessedMonth", "width": "10%" },
            { "data": "lastReportingDate", "width": "10%" },
            { "data": "ageing", "width": "10%" },



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
    const myTimeout = setTimeout(tooltipHover, 500);
}

function onEmployeeClick(GI) {
    debugger;
    url = "/DataBank/AbscountingEmployeeTermination/Create?EmployeeGI=" + GI;
    window.location.href = url;
}

function getSalaryList() {
    debugger;
    var i = 0;
    if (!validateById('#Reason_Code', 'reason')) {
        i++;
    }
    if (i == 0) {
        $.ajax({

            type: 'GET',
            url: '/DataBank/AbscountingEmployeeTermination/GetSalaryList',
            data: { EmployeeGI: $('#EmployeeGI').val(), LWD: $('#Details_LWD').val() },
            dataType: 'html',
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {

                    $('#SalaryList').html("");
                    $('#SalaryList').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
    }
       

    
}

function SaveAbscounting() {
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
            //let NetTotal = 0;
            //var tableLength = $('#AbscountingTerminationTable tr').length
            //for (let i = 0; i < tableLength-1; i++) {
            //    debugger;
            //    NetTotal = parseInt(NetTotal) + parseInt($('#netPay_'+i).text());
            //}
            //if (NetTotal == 0) {
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/AbscountingEmployeeTermination/Create',
                    dataType: 'json',
                    data: $('#AbscountingTerminationSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/AbscountingEmployeeTermination/Index";
                                window.location.href = url;
                            });

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
            //}
            //else {
            //    swal.fire("Termination Total Net Pay of employee never be greater than zero");
            //}
        }
    });
}

function ApplyFilter(mode) {
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData()
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            //showSpinner();
            fillData()
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}

function ExportToExcel() {
    debugger;
    //if (validateDropdowns()) {

    var TableLength = $('#AbscondingReporttList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/AbscountingEmployeeTermination/ExportToExcel?Policy=" + $('#InsurancePolicy_Code').val() + "&totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
    //}
}