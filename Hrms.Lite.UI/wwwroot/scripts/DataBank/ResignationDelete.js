

function onDeleteClick(EmployeeGI, ResignationGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/DataBank/ResignationDelete/GetDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, ResignationGI: ResignationGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#DeleteSlider').html("");
                $('#DeleteSlider').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger          
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
          

        }
    });
}

function Delete(ResignationGI, Reason){
    debugger;
    $.ajaxSetup({
        async: false
    });
   /* var Reason = $('#ResignationDeleteDetails_Reason').val();*/
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResignationDelete/Delete',
                dataType: 'json',
                data: { ResignationGI: ResignationGI, Remarks: Reason },
                success: function (result) {
                    debugger;
                    if (result.success) {               
                        swal.fire(result.message).then(() => {
                            CloseSlider('delete');
                            ApplyFilter('apply');
                          
                        });
                    }
                    else {
                        debugger;
                        swal.fire(result.message);
                    }

                },


                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }
    });
        
    }
 





function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResignationDelete/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#ResignationDeleteList').html("");
                        $('#ResignationDeleteList').html(result);
                        $('#ResignationDeleteList').removeClass('hide');
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
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResignationDelete/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#ResignationDeleteList').html("");
                        $('#ResignationDeleteList').html(result);
                        $('#ResignationDeleteList').removeClass('hide');
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
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/ResignationDelete/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#ResignationDeleteList').html("");
                    $('#ResignationDeleteList').html(result);
                    $('#ResignationDeleteList').removeClass('hide');
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
    debugger;
   

    var TableLength = $('#ResignationList_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/ResignationDelete/ExportToExcel";
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}