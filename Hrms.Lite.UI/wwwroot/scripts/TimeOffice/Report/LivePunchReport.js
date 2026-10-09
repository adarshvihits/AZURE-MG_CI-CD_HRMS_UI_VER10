function GetList() {
    debugger
    $('#grpType').val(1)
    showSpinner();
        var date = $('#Date').val();
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/LivePunchReport/GetList',
            dataType: 'html',
            data: { Date: date },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#PunchingList').html("");
                    $('#PunchingList').html(result);
                    onGrpChange();  
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

function getParam() {

    var date = $('#Date').val();

    var formArray = $("#BasicFilter").serializeArray();

    var tabIndex = 0;
    var searchValue = $('#dataTableSearch').val();


    var temp = {};
    for (var i = 0; i < formArray.length; i++) {
        temp[formArray[i]['name']] = formArray[i]['value']; // Map the current key-value pairs into the filter variable object.
    }

    temp["TabIndex"] = tabIndex;//$("#F_To").val();
    temp["searchValue"] = searchValue;

    return { date:date,filter1: temp };
}
function onGrpChange() {
    debugger
    showSpinner();
    if ($('#grpType').val() == 3) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "LOCATION" });
    }
    else if ($('#grpType').val() == 2) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "SECTION" });
    }
    else if ($('#grpType').val() == 1) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "DEPARTMENT" });
    }
    else if ($('#grpType').val() == 4) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "DIVISION" });
    }
    else if ($('#grpType').val() == 5) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "SHIFT" });
    }
    else if ($('#grpType').val() == 6) {
        $('#grid').data('kendoGrid').dataSource.group({ field: "PunchStatus" });
    }
    else
        $("#grid").data("kendoGrid").dataSource.data([]);
    //refresh will re-render items in grid from the current datasource
    hideSpinner();
    $('#grid').data('kendoGrid').refresh();
}
function ApplyFilter(mode) {
    debugger
    var date = $('#Date').val();
    var search = $('#Filter_Code').val();
    //var search = $('#SearchId').val();

    $("#SearchId").val($('#Filter_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
        
            CloseSlider('basicFilter');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LivePunchReport/ApplyFilter?Date=' + date,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#PunchingList').html("");
                        $('#PunchingList').html(result);
                        $('#PunchingList').removeClass('hide');
                    }
                    hideSpinner();

                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
         
            CloseSlider('basicFilter');
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/LivePunchReport/ApplyFilter?Date=' + date,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#PunchingList').html("");
                        $('#PunchingList').html(result);
                        $('#PunchingList').removeClass('hide');
                    }
                    hideSpinner();

                },


                error: function (xhr, textStatus, errorThrown) {
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
            url: '/TimeOffice/LivePunchReport/ApplyFilter?Date=' + date,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#PunchingList').html("");
                    $('#PunchingList').html(result);
                    $('#PunchingList').removeClass('hide');
                }
                hideSpinner();


            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}


//function ExportToExcel() {
//    debugger

//    var filter = $('#BasicFilter').serialize();
//    var Date = $('#Date').val();



//    var PARAM = 'data=' + filter + '&Date=' + Date;
//    var rowCount = grid.dataSource.view().length;
//    /*var rowCount = grid.dataSource.total();*/

//    /*var TableLength = $('#LivePunchReportList_Count').val();*/
//    if (rowCount > 0) {
//        url = '/TimeOffice/Report/LivePunchReport/ExportToExcel?' + PARAM + "&TotalRows=" + rowCount;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//}

function ExportToExcel() {
    debugger

    var filter = $('#BasicFilter').serialize();
    var Date = $('#Date').val();

    var PARAM = 'data=' + filter + '&Date=' + Date;

    // Get the Kendo Grid instance by ID
    var grid = $("#grid").data("kendoGrid");

    // Check if the grid is initialized and the dataSource is available
    if (grid && grid.dataSource) {
        // Get the number of rows currently visible in the grid
        var rowCount = grid.dataSource.view().length;

        if (rowCount > 0) {
            var url = '/TimeOffice/LivePunchReport/ExportToExcel?' + PARAM + "&TotalRows=" + rowCount;
            window.location.href = url;
        } else {
            swal.fire("No data found to export");
        }
    } else {
        console.error("Kendo Grid is not initialized or dataSource is undefined");
        swal.fire("Grid is not initialized or data source is not available");
    }
}



        //var grid = $("#grid").data("kendoGrid");
        //grid.saveAsExcel()
    

    //var grid = $("#grid").data("kendoGrid");
    //var pageSize = grid.dataSource._data.length;
    //var dataSourceTotal = grid.dataSource.total();
    //grid.dataSource.pageSize(dataSourceTotal);
    //grid.saveAsExcel();

    //setTimeout(function () {
    //    grid.dataSource.pageSize(pageSize);
    //}, 300);


//$(document).ready(function () {
//    $("#dataTableSearch").on("keyup search paste cut", function () {
//        console.log('h');
//        $('#grid').data('kendoGrid').dataSource.read();
//        $('#grid').data('kendoGrid').refresh();
//    });
//});