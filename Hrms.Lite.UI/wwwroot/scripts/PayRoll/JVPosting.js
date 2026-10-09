
function getList() {

    var monthCode = $('#PayrollMonth_Code').val();

    var BasedOn = $('input[name="radioSelection"]:checked').val();

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    if (!validateById("#PayrollGroup_Code", 'Payrollgroup')) return false;
    if (!validateById("#PayrollMonth_Code", 'Month')) return false;
   

    $.ajax({
        url: "/PayRoll/JVPosting/CheckJVLinking",
        type: "GET",
        data: { MonthCode: monthCode },

        success: function (data) {

  
            if (data.success == true) {
                var dataTable = $("#jvTable").DataTable({

                    "pagingType": 'full_numbers',
                    "destroy": 'true',
                    "ajax": {
                        "url": '/PayRoll/JVPosting/GetList',
                        "type": "POST",
                        "datatype": "json",
                        'data':

                            function (d) {
                                temp = '';

                                var start = d.start;

                                var searchValue = $('#dataTableSearch').val();

                                temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + $("#PayrollMonth_Code").val() + '&PayrollGroup=' + $("#PayrollGroup_Code").val() + '&Type=' + BasedOn + '&dummy=dummy';
                                d.filter = temp;

                                return JSON.stringify(d);
                            },




                        "dataSrc": function (d) {

                            $('#JVPostingList_Count').val(d.recordsTotal);


                            for (var i = 0, ien = d.data.length; i < ien; i++) {
                                d.data[i].vocherDate = _GetFormattedDate(d.data[i].vocherDate);
                            }
                            debugger
                            return d.data;
                        }

                    },

                    "columns": [

                        { "data": "location" },
                        { "data": "glAccountCode" },
                        { "data": "serialNo" },
                        { "data": "payElementDesc" },
                        { "data": "vocherDate" },
                        { "data": "processPeriod" },
                        { "data": "refNo" },
                        { "data": "narration" },
                        { "data": "drOrCr" },
                        { "data": "amount" },
                        { "data": "deptCostCenter" }

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
            else {
                $('#CheckList').html(data);
                // show modal
                $('.bd-example-modal-lg').modal('show');
            }
        }
        });
    }

function OnMonthChange()
{
    $('#jvTable tbody').empty();
}

function GetMonth() {
    OnMonthChange();
    $.getJSON("/PayRoll/JVPosting/GetMonth", { PayrollGrpCode: $('#PayrollGroup_Code').val(), Type: $('input[name="radioSelection"]:checked').val()}, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value="">--Select Month--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function JVPostingSave() {
    debugger;
    var Month = $('#PayrollMonth_Code').val();
    var Payrollgroup = $('#PayrollGroup_Code').val();
    var BasedOn = $('input[name="radioSelection"]:checked').val();
    var TableLength = $('#JVPostingList_Count').val();
  
    if (!validateById("#PayrollGroup_Code", 'Payrollgroup')) return false;
    if (!validateById("#PayrollMonth_Code", 'Month')) return false;
       
  
   
    if (TableLength > 0) {
        showSpinner();
        $.ajax({
            type: 'POST',
            url: '/PayRoll/JVPosting/Save',
            data: { Month: Month, Payrollgroup: Payrollgroup, Type:BasedOn },
            success: function (result) {
                debugger;
                hideSpinner();
                Swal.fire({
                    text: result.message,
                   /* icon: "success"*/
                }).then(function () {
                    window.location.href = "/PayRoll/JVPosting/Index";
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger;
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
    else {
        swal.fire("No data to Save");
    }

}
