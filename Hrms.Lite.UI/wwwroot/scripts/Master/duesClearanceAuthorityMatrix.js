function onClick(LevelCode) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/Master/DuesClearanceAuthorityMatrix/GetClearanceVsAuthorityPopup?LevelCode=' + LevelCode,
        dataType: 'html',
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.addShiftPopup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
                $(".form-overlay").remove();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function onSaveClick() {
    debugger;

    swal.fire({
        text: "Do you want to Save ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/DuesClearanceAuthorityMatrix/Save',
                dataType: 'json',
                data: $('#DuesClearanceAuthoritySave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DuesClearanceAuthorityMatrix/Index";
                            window.location.href = url;

                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
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
        else {
            return false;
        }
    })

}






function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/Master/DuesClearanceAuthorityMatrix/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + '&LevelCode=' + $('#LevelCode').val() ,
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
                url: '/Master/DuesClearanceAuthorityMatrix/ApplyFilter',
                dataType: 'html',

                data: $('#BasicFilter').serialize() + '&LevelCode=' + $('#LevelCode').val(),
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
            url: '/Master/DuesClearanceAuthorityMatrix/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + '&LevelCode=' + $('#LevelCode').val() ,
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













var searchClick = 0;

function searchRows() {
    debugger;

    
    var input = document.getElementById("dataTableSearch");
    var filter = input.value.toUpperCase();

    var table = document.getElementById("AuthorityVsDUETable");
    var tr = table.getElementsByTagName("tr");
    var tbody = table.getElementsByTagName("tbody");

    $('#noDataImg').remove();
    var noOfRows = tr.length;

    for (var i = 1; i < tr.length; i++) {
       
        let empId = tr[i].getElementsByTagName("td")[1];
        let empName = tr[i].getElementsByTagName("td")[2];

        if (!empId || !empName) continue;

      
        let rowText = (empId.innerText + " " + empName.innerText).toUpperCase();

        if (rowText.indexOf(filter) > -1) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
            noOfRows--;
        }
    }

  
    if (noOfRows == 1) {
        $(table).find(tbody).append(
            `<tr id='noDataImg'>
                <td colspan='5'>
                    <div style='text-align:center; display:flex; 
                        align-items:center; justify-content:center; height:45vh;'>
                        <img src='/images/nodata.png'/>
                    </div>
                </td>
            </tr>`
        );
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






