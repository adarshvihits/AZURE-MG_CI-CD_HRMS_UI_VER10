//function GetList() {
//    showSpinner();
//    var month = $('#Month').val();
////    var url = "/TimeOffice/MusterRollReport/GetReportList?Month=" + month;
////    $('#MusterRollList').load(url, function () { hideSpinner(); });

//    $.ajax({

//        type: 'GET',
//        url: '/TimeOffice/MusterRollReport/GetReportList',
//        dataType: 'html',
//        data: { Month: month },

//        success: function (result) {
//            debugger
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }
//            else {
//                $('#MusterRollList').html("");
//                $('#MusterRollList').html(result);
//            }
//            hideSpinner();


//        },
//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });

//}


//function ApplyFilter(mode) {
//    debugger
//    var month = $("#Month").val();

//    var tabIndex = $('#Tabindex').val();
//    var search = $('#SearchId').val();
//    //$("#Tabindex").val(currentTab);
//    $("#SearchId").val($('#FilterList_Code').val())

//    if (mode == 'apply') {
//        showSpinner();
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/TimeOffice/MusterRollReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&Month=" + month ,
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        $('#MusterRollList').html("");
//                        $('#MusterRollList').html(result);

//                        //$('#rewardsListPartial').addClass('hide');
//                    }
//                    hideSpinner();
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            $.ajax({

//                type: 'POST',
//                url: '/TimeOffice/MusterRollReport/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&Month=" + month,
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        //$('#MusterRollList').html("");
//                        //$('#MusterRollList').html(result);

//                        //$('#MusterRollList').removeClass('hide');
//                        $('#MusterRollList').html("");
//                        $('#MusterRollList').html(result);

//                        //$('#rewardsListPartial').addClass('hide');
//                    }
//                    hideSpinner();
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    hideSpinner();
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//        showSpinner();
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('#filterForm').removeClass('filter-apply');
//        $.ajax({

//            type: 'POST',
//            url: '/TimeOffice/MusterRollReport/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize() + "&Month=" + month,
//            success: function (result) {
//                debugger

//                //CloseSlider('assetAllocationFilter');
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else{
//                $('#MusterRollList').html("");
//                $('#MusterRollList').html(result);
//            }
//                //$('#rewardsListPartial').addClass('hide');
//                hideSpinner();
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                hideSpinner();
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}

function ApplyFilter(mode) {

    $('#Report').data('kendoGrid').dataSource.read().then(function () {

        $('#Report').data('kendoGrid').dataSource.page(1);
    });
    $('#Report').data('kendoGrid').refresh();




    if (mode == 'apply') {

        if (filterApplicable >= 1) {

            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {


            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }


    }

    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');

    }


}


function ExportToExcel() {
    debugger;
    var month = $('#Item1_Month').val();
    var MonthName = $('#Item1_Month option:selected').text();
    /*var pendingTableLength = $('#Rows_Count').val();*/
    var pendingTableLength = $('#Report').data('kendoGrid').dataSource.total();
    if (pendingTableLength > 0) {
        url = "/TimeOffice/MusterRollReport/ExportToExcel?month=" + month + "&MonthName=" + MonthName + "&TotalRows=" + pendingTableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}