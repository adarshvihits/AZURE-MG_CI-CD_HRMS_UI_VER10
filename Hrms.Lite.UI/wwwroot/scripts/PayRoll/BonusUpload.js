var MonthCode;
var Month;
var IncCategoryCode;
var IncCategory;

function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var IncCategory = $('#IncentiveCategory_Code').val();
    var Month = $('#Month_Code').val();

    var dataTable = $("#BonusUploadTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/BonusUpload/GetList",
            "type": "POST",
            "datatype": "json",

            "data": function (d) {
                var searchValue = $('#dataTableSearch').val();

                // Add custom parameters to the data object

                d.Month = Month;
                d.IncCategory = IncCategory;
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
                debugger;
                $('#BonusUploadList_Count').val(d.recordsTotal);
                $("#totAmt").html(d.totalAmount);
                if ($('#BonusUploadList_Count').val() == '0')
                    $('#totAmt').html('');
          



                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    if (d.data[i].amount != null)
                        d.data[i].amount = '<span class="d-flex justify-content-end" id = "amtLabel_' + i + '">' + d.data[i].amount + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                    else
                        d.data[i].amount = '<span  class="d-flex justify-content-end" id = "amtLabel_' + i + '">' + '' + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 10 onkeypress = "return onlyAmount(event)"></span>'

              

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "year" },
            { "data": "effectiveMonth" },
            { "data": "quarter" },
            { "data": "product" },
            { "data": "subProduct" },
            { "data": "bonusType" },
            { "data": "amount" },

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


    const myTimeout = setTimeout(tooltipHover, 500);



}
function validateDropdowns() {

    var i = 0;
    if (!validateById('#Month_Code', 'Month')) {
        i++;
    }
    if (!validateById('#IncentiveCategory_Code', 'IncType')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;


}
function ExportToExcel() {
    var TableLength = $('#BonusUploadList_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/BonusUpload/ExportToExcel?Month=" + $('#Month_Code').val() + "&IncCategory=" + $('#IncentiveCategory_Code').val() + "&MName=" + $('#Month_Code option:selected').text() + "&IncCategoryName=" + $('#IncentiveCategory_Code option:selected').text() + "&totalRecord=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
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
        CloseSlider('basicFilter');
    }
}
function validateuploadDropdowns() {
    var i = 0;

    debugger;
    if (!validateById('#Month_Code', 'Month')) {
        i++;
    }
    if (!validateById('#IncentiveCategory_Code', 'IncType')) {
        i++;
    }

    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}
function onImportExcelClick() {

    debugger;

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            var BonusUploadInfo = new FormData($('#bonusupload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/BonusUpload/ImportFromExcel',
                contentType: false,
                processData: false,
                data: BonusUploadInfo,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {

                            fillData()
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
                    hideSpinner();
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

