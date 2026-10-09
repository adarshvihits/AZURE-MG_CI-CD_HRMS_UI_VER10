
function fillData() {
    debugger;
    if (ValidateDate()) {
        var fromDate = $('#SettlementFrom').val();
        var toDate = $('#SettlementTo').val();


        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;


        var dataTable = $("#fandfDeleteTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/FullAndFinalSettelmentDelete/GetDeleteList",
                "type": "POST",

                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {




                    $('#EmployeeList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].settlementDate = _GetFormattedDate(d.data[i].settlementDate);


                        d.data[i].employeeName = '<a class="" onclick="onEmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].employeeName + '</a>'

                    }
                    console.log(d.data)
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "15%" },

                { "data": "department", "width": "15%" },
                { "data": "designation", "width": "15%" },
                { "data": "designation", "width": "15%" },
                { "data": "location", "width": "10%" },


                { "data": "doj", "width": "10%" },
                { "data": "settlementDate", "width": "10%" },

               



            ],






            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "order": [0, "asc"],
            "processing": "true",
            "language": {

                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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


}





function onEmpNameClick(EmployeeGI)
{
    debugger;
    url = "/PayRoll/FullAndFinalSettelmentDelete/GetDetails?EmployeeGI=" + EmployeeGI+"&Mode=DELETE";
    window.location.href = url;
}

function ValidateDate() {
    debugger;
    var fromDate = $('#SettlementFrom').val();
    var toDate = $('#SettlementTo').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        $("#fandfDeleteTable_tbody").empty();
        return false;
    }
    else
        return true;
}

function validateRemarks() {
    debugger;
    var i = 0;
    if (!validateById('#Details_VarificationComments', 'remarks')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}


function confirmDelete() {
    debugger
    if (validateRemarks()) {
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
                    url: '/PayRoll/FullAndFinalSettelmentDelete/Delete',
                    dataType: 'json',
                    data: $('#deletesettlement').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                url = "/PayRoll/FullAndFinalSettelmentDelete/Index";
                                window.location.href = url;
                            });

                        }
                        else {
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
    else {
        return false;
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
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}
function ExportToExcel() {
    debugger;
    var fromdate = $('#SettlementFrom').val();
    var todate = $('#SettlementTo').val();

    var tableLength = $('#EmployeeList_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/FullAndFinalSettelmentDelete/ExportToExcel?fromDate=" + fromdate + "&toDate=" + todate + "&totRecords=" + tableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}



var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function inWords(num) {
    debugger;
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'And ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'Only ' : '';
    return str;
}