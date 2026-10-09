function GetList() {
    $.ajax({

        type: 'POST',
        url: '/DataBank/HRDuesClearance/ApplyFilter',
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();

            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
                $('#ListView').removeClass('hide');
            }
            hideSpinner();
            CloseSlider('basicFilter');

        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
}
function ApplyFilter(mode) {
    debugger
    
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            showSpinner();
            $('#filterForm').removeClass('filter-apply');
            GetList();
        }
        else {
            showSpinner();
            GetList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList();
    }
}

function OnEmployeeClick(ResignationGI,EmpID,EmpName) {

    $.ajax({
        type: 'POST',
        url: '/DataBank/HRDuesClearance/DuesDetails',
        dataType: 'html',
        data: { ResignationGI: ResignationGI},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();

            }
            else {
                var m = $('.duesclearance');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                $('.Name').html(EmpName + '(' + EmpID + ')');
                m.modal('show');
            }
            hideSpinner();
            CloseSlider('basicFilter');

        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });
}


function Save() {


    if ($('#parameterTable').find('input[type=checkbox]').is(':checked')) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/HRDuesClearance/Save',
                    dataType: 'json',
                    data: $('#parameterDetails').serialize(),
                    success: function (result) {
                        debugger
                        if (result.success) {
                            swal.fire('Saved Successfully');
                            var m = $('.duesclearance');
                            m.find('.modal-body').html('');
                            m.modal('hide');
                            GetList();
                        }
                        else
                            swal.fire(result.message);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })
        }
    else {
        swal.fire('Select atleast One')
            return false;
        }

}

function ExportToExcel() {
    debugger;
    var TableLength = $("#List_Count").val();
    if (TableLength != "") {
        url = "/DataBank/HRDuesClearance/ExportToExcel?data=" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}