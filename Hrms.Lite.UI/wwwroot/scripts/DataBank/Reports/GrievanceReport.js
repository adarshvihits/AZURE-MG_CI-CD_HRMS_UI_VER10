
function getList() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var Category = $('#Category_Code').val();



    if (fromDate !== '' && toDate !== '' && Category!=='') {
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/DataBank/GrievanceReport/GetList',
            dataType: 'html',
            data: { FromDate: fromDate, ToDate: toDate, Category: Category },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#listIDgrievance').html("");
                    $('#listIDgrievance').html(result);
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
        return true;
    }
    else {

        return false;
    }
}

function ApplyFilter(mode) {
    debugger;
    var monthCode = $('#PayrollMonth_Code').val();


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/GrievanceReport/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#listIDgrievance').html("");
                        $('#listIDgrievance').html(result);

                        $('#listIDgrievance').removeClass('hide');
                    }

                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/DataBank/GrievanceReport/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#listIDgrievance').html("");
                        $('#listIDgrievance').html(result);

                        $('#listIDgrievance').removeClass('hide');
                    }

                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/GrievanceReport/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger


                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#listIDgrievance').html("");
                    $('#listIDgrievance').html(result);

                    $('#listIDgrievance').removeClass('hide');
                    //$('#rewardsListPartial').addClass('hide');
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });


    }
}

function ExportToExcel() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var Category = $('#Category_Code').val();
    var CategoryName = $('#Category_Code option:selected').text();

    var tableLength = $('#GrievanceList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/GrievanceReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&Category=" + Category + "&CategoryName=" + CategoryName+"&totRecords=" + tableLength;
        window.location.href = url;
    }
    else
    {
        swal.fire("No data found to export");
    }
}





function onDateClick() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var Category = $('#Category_Code').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
      /*  $("#To").val('');*/
        var url = "/DataBank/GrievanceReport/GetList?FromDate=" + fromDate + "&ToDate=" + toDate + "&Category=" + Category;
        $('#listIDgrievance').load(url, function () { });
        

        return false;
    }
    else
        return true;
}