let Traininghead;

function GetList() {
    debugger;
    var month = $('#Month').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/TrainingModule/MonthlyReport/GetList',
        dataType: 'html',
        data: "&Month=" + month,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#MonthlyListID').html("");
                $('#MonthlyListID').html(result);
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

function OnParticipantSelectedClick(ScheduleCode,Trainingname) {
    debugger;
    var month = $('#Month').val();
    Traininghead = Trainingname;
    var MonthName = $('#Month option:selected').text();
    var ScheduleCode = ScheduleCode;
    $.ajax({
        type: 'GET',
        url: "/TrainingModule/MonthlyReport/GetParticipantsSelectedPopUp",
        data: { ScheduleCode: ScheduleCode, Month: month },
        
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.participantselecteddetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#participantSelectedLabel').html('Participant Selected Details -' + Trainingname + ' (' + MonthName + ')');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function OnParticipantCompletedClick(ScheduleCode, Trainingname) {
    debugger;
    var month = $('#Month').val();
    Traininghead = Trainingname;
    var MonthName = $('#Month option:selected').text();
    var ScheduleCode = ScheduleCode;

    $.ajax({
        type: 'GET',
        url: "/TrainingModule/MonthlyReport/GetParticipantsCompletedPopUp",
        data: { ScheduleCode: ScheduleCode, Month: month},
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.participantcompleteddetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#participantCompletedLabel').html('Participant Completed Details -' + Trainingname + ' (' + MonthName+')');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function participantselectedExportToExcel() {
    debugger;
    var month = $('#Month').val();
    var ScheduleCode = $('#ScheduleCode').val();
    var filter = $('#BasicFilter').serialize();
    var TableLength = $('#ParticipantDetails_Count').val();

    // Logging for debugging
    console.log("Month: ", month);
    console.log("ScheduleCode: ", ScheduleCode);
    console.log("Filter: ", filter);
    console.log("TableLength: ", TableLength);

    if (TableLength > 0) {
        var params = 'data=' + encodeURIComponent(filter) +
            '&ScheduleCode=' + encodeURIComponent(ScheduleCode) +
            '&Month=' + encodeURIComponent(month) +
            '&TotalRows=' + encodeURIComponent(TableLength);

        var url = '/TrainingModule/MonthlyReport/PaticipantselectedExportToExcel?' + params + '&Traininghead=' + Traininghead;
        console.log("Redirect URL: ", url);
        window.location.href = url;
    } else {
        swal.fire("No data found to export");
    }
}

function participantcompletedExportToExcel() {
    debugger;
    var month = $('#Month').val();
    var ScheduleCode = $('#ScheduleCode').val();
    var filter = $('#BasicFilter').serialize();
    var TableLength = $('#ParticipantDetails_Count').val();
    $('#participantCompletedLabel').val();
    // Logging for debugging
    console.log("Month: ", month);
    console.log("ScheduleCode: ", ScheduleCode);
    console.log("Filter: ", filter);
    console.log("TableLength: ", TableLength);

    if (TableLength > 0) {
        var params = 'data=' + encodeURIComponent(filter) +
            '&ScheduleCode=' + encodeURIComponent(ScheduleCode) +
            '&Month=' + encodeURIComponent(month) +
            '&TotalRows=' + encodeURIComponent(TableLength);

        var url = '/TrainingModule/MonthlyReport/participantcompletedExportToExcel?' + params + '&Traininghead=' + Traininghead;
        console.log("Redirect URL: ", url);
        window.location.href = url;
    } else {
        swal.fire("No data found to export");
    }
}

function searchRows() {
    debugger
    var input, filter, table, tr, td0,td1, i;

    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("ParticipantsSelected");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var dataRowsCount = 0;
    var visibleRowsCount = 0;
    var spannedRows = 0;
    var show = true;

    for (i = 1; i < tr.length; i++) { // Start from 1 to skip the header row
        debugger;
        td0 = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        if (td0||td1) {
            dataRowsCount++;
            if (spannedRows > 0) {
                if (show)
                    tr[i].style.display = "";
                else {
                    tr[i].style.display = "none";
                    visibleRowsCount--;
                }
                spannedRows--;
            } else {
                tr[i].style.display = "none";
                if ((td0 && td0.innerHTML.toUpperCase().indexOf(filter) > -1) ||
                    (td1 && td1.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                    tr[i].style.display = "";
                    visibleRowsCount++;
                }
                let rs = td1 ? td1.getAttribute("rowspan") : null;
                if (rs && rs > 1) {
                    show = td1.innerHTML.toUpperCase().indexOf(filter) > -1 || td0.innerHTML.toUpperCase().indexOf(filter) > -1;
                    spannedRows = rs - 1;
                }
            }
        }
    }

    if (visibleRowsCount === 0) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='5'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }

}

function searchRows1() {
    debugger
    var input, filter, table, tr, td0,td1, i;

    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList1");
    filter = input.value.toUpperCase();
    table = document.getElementById("ParticipantsCompleted");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var dataRowsCount = 0;
    var visibleRowsCount = 0;
    var spannedRows = 0;
    var show = true;

    for (i = 1; i < tr.length; i++) { // Start from 1 to skip the header row
        debugger;
        td0 = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        if (td0||td1) {
            dataRowsCount++;
            if (spannedRows > 0) {
                if (show)
                    tr[i].style.display = "";
                else {
                    tr[i].style.display = "none";
                    visibleRowsCount--;
                }
                spannedRows--;
            } else {
                tr[i].style.display = "none";
                if ((td0 && td0.innerHTML.toUpperCase().indexOf(filter) > -1) ||
                    (td1 && td1.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                    tr[i].style.display = "";
                    visibleRowsCount++;
                }
                let rs = td1 ? td1.getAttribute("rowspan") : null;
                if (rs && rs > 1) {
                    show = td1.innerHTML.toUpperCase().indexOf(filter) > -1 || td0.innerHTML.toUpperCase().indexOf(filter) > -1;
                    spannedRows = rs - 1;
                }
            }
        }
    }

    if (visibleRowsCount === 0) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='5'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }

}






function searchRows2() {
    debugger
    var input, filter, table, tr, td0, td1, i;

    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList3");
    filter = input.value.toUpperCase();
    table = document.getElementById("EsiSettingsTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var dataRowsCount = 0;
    var visibleRowsCount = 0;
    var spannedRows = 0;
    var show = true;

    for (i = 1; i < tr.length; i++) { // Start from 1 to skip the header row
        debugger;
        td0 = tr[i].getElementsByTagName("td")[0];
        td1 = tr[i].getElementsByTagName("td")[1];
        if (td0 || td1) {
            dataRowsCount++;
            if (spannedRows > 0) {
                if (show)
                    tr[i].style.display = "";
                else {
                    tr[i].style.display = "none";
                    visibleRowsCount--;
                }
                spannedRows--;
            } else {
                tr[i].style.display = "none";
                if ((td0 && td0.innerHTML.toUpperCase().indexOf(filter) > -1) ||
                    (td1 && td1.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                    tr[i].style.display = "";
                    visibleRowsCount++;
                }
                let rs = td1 ? td1.getAttribute("rowspan") : null;
                if (rs && rs > 1) {
                    show = td1.innerHTML.toUpperCase().indexOf(filter) > -1 || td0.innerHTML.toUpperCase().indexOf(filter) > -1;
                    spannedRows = rs - 1;
                }
            }
        }
    }

    if (visibleRowsCount === 0) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='5'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }

}
//function participantselectedExportToExcel() {
//    debugger;
//    var month = $('#Month').val();
//    var ScheduleCode = $('#ScheduleCode').val();
//    var filter = $('#BasicFilter').serialize();
//    var PARAM = 'data=' + filter + '&ScheduleCode=' + ScheduleCode + '&Month=' + month;
//    var TableLength = $('#ParticipantDetails_Count').val();
//    if (TableLength > 0) {
//        url = '/TrainingModule/MonthlyReport/PaticipantselectedExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//}

function ExportToExcel() {
    debugger;
    var Month = $('#Month').val();
    var MonthName = $('#Month option:selected').text();
    var filter = $("#BasicFilter").serialize();
    
    var MonthlyReportListLength = $('#MonthlyReportList_Count').val();
    var PARAM = 'data=' + filter;

    if (MonthlyReportListLength > 0) {
        url = '/TrainingModule/MonthlyReport/ExportToExcel?' + PARAM + "&Month=" + Month + "&monthname=" + MonthName;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

