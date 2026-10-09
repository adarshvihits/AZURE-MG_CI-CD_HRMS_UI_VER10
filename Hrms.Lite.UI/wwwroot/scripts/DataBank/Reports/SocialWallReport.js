
function GetList() {
    debugger;
    var monthCode = $('#MonthCode').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({

        type: 'POST',
        url: '/DataBank/SocialWallReport/GetList',
        dataType: 'html',
        data: "&MonthCode=" + monthCode,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#SocialWallList').html("");
                $('#SocialWallList').html(result);
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

function OnLikesClick(SocialWallCode,i) {
    debugger;
    var monthCode = $('#MonthCode').val();
    var SocialWallCode = SocialWallCode;
    var Description = $('#SocialWallReportList_' + i + '__Name ').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/SocialWallReport/GetEmployeeLikesPopUp",
        data: { Name: Description,SocialWallCode: SocialWallCode, MonthCode: monthCode },

        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger;
                var m = $('.LikesEmployeedetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function OnCommentsClick(SocialWallCode,i) {
    debugger;
    var monthCode = $('#MonthCode').val();
    var SocialWallCode = SocialWallCode;
    var Description = $('#SocialWallReportList_' + i + '__Name ').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/SocialWallReport/GetEmployeeCommentPopUp",
        data: { Name: Description,SocialWallCode: SocialWallCode, MonthCode: monthCode },
        dataType: 'html',
        
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.CommentEmployeedetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}



function search() {
    debugger
    var input, filter, table, tr, td0, td1, i;

    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("SocialwallTable");
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

function ExportToExcel() {
    debugger;
    var MonthCode = $('#MonthCode').val();
  
    var SocialWallReportListLength = $('#SocialWallReportList_Count').val();
    
    if (SocialWallReportListLength > 0) {
        url = '/DataBank/SocialWallReport/ExportToExcel?'+ "&MonthCode=" + MonthCode;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}


