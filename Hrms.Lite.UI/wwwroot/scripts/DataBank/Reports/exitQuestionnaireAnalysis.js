var searchClick = 0;







function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("exitQstnAnalysisTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}
function onSearchClick() {
    if (searchClick == 0) {
        searchClick = 1;
        $('#dataTableSearch').addClass('field-width-big');
        CursorFocusOnSearch();
    }
    else {
        searchClick = 0;
        $('#dataTableSearch').removeClass('field-width-big');
    }
}
function GetDetails(index,qstnCode,ansCode) {
    $.ajax({
        type: 'GET',
        url: "/DataBank/ExitQuestionnaireAnalysis/GetDetails",
        dataType: 'html',
        data: { QuestionCode: qstnCode, AnswerCode: ansCode },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#qstnNo').html('Question '+index);
                //$("#myDiv").css("display", "block");
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);            
        }
    });
}

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/ExitQuestionnaireAnalysis/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + '&QuestionCode=' + $('#QuestionCode').val() + '&AnswerCode=' + $('#AnswerCode').val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#empList').html("");
                        $('#empList').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }

            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/DataBank/ExitQuestionnaireAnalysis/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + '&QuestionCode=' + $('#QuestionCode').val() + '&AnswerCode=' + $('#AnswerCode').val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#empList').html("");
                        $('#empList').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }

            });
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/ExitQuestionnaireAnalysis/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + '&QuestionCode=' + $('#QuestionCode').val() + '&AnswerCode=' + $('#AnswerCode').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#empList').html("");
                    $('#empList').html(result);
                }
                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function ExportToExcel() {
    debugger   
    var TableLength = $('#ExitQuestionnaireAnalysisList_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    if (TableLength > 0) {
        url = "/DataBank/ExitQuestionnaireAnalysis/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelPopUp(Question,Answer) {
    debugger   
    var TableLength = $('#ExitQuestionnaireAnalysisList_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    if (TableLength > 0) {
        url = "/DataBank/ExitQuestionnaireAnalysis/ExportToExcelPopUp?QuestionCode=" + $('#QuestionCode').val() + "&AnswerCode=" + $('#AnswerCode').val() + "&Question=" + Question + "&Answer=" + Answer + "&totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}