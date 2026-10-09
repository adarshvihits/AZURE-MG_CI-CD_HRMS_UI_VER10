function GetList() {
    debugger
    Month = $("#Month_Code").val()

    $.ajax({

        type: 'GET',
        url: '/DataBank/StaffConfirmationReport/GetList',
        dataType: 'html',
        data: { Month: Month },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#List').html("");
                $('#List').html(result);
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


function GetDetails(EmployeeGI) {
    Month = $("#Month_Code").val()
    debugger;

    $.ajax({
        type: 'GET',
        url: "/DataBank/StaffConfirmationReport/GetDetails",
        data: { EmployeeGI: EmployeeGI, Month: Month },
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.empchecklistdetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

                m.modal('show');

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}



function ExportToExcel() {
    debugger;

    var filter = $('#BasicFilter').serialize();

    Month = $("#Month_Code").val()

    var PARAM = 'data=' + filter + '&Month=' + Month;
    var TableLength = $('#StaffConfirmationReportList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/StaffConfirmationReport/ExportToExcel?' + PARAM + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function ItemDetailsExportToExcel(EmployeeGI) {
    debugger

    Month = $("#Month_Code").val()
    var TableLength = $('#StaffConfirmationReportDetails_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/StaffConfirmationReport/ItemDetailsExportToExcel?EmployeeGI=" + EmployeeGI + "&Month=" + Month;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function combination1searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination1tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }

    //for (i = 0; i < tr.length; i++) {
    //    debugger;
    //    td = tr[i].getElementsByTagName("td")[0];
    //    if (td) {
    //        txtValue = td.textContent || td.innerText;
    //        if (txtValue.toUpperCase().indexOf(filter) > -1) {
    //            tr[i].style.display = "";
    //        } else {
    //            tr[i].style.display = "none";
    //            noOfRows--;
    //        }
    //    }
    //}
}