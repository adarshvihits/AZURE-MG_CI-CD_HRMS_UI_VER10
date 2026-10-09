function AddNewOrEditClick(mode, MessageCode) {
    debugger;
    $.ajax
        ({
            type: 'GET',
            url: '/DataBank/InstantMessaging/CreateOrEdit',
            dataType: 'html',
            data: { Mode: mode, MessageCode: MessageCode},
            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    var m = $('.addnew_popup');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    hideSpinner();
                    m.modal('show');
                    if (mode == 'CREATE')
                        $('#addnew_popup').html('Add New');
                    else
                        $('#addnew_popup').html('Edit');
                }
            },

            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
}

function OnSaveClick(mode) {
    debugger;
    swal.fire
        ({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger;
                $.ajax
                    ({
                        type: 'POST',
                        url: '/DataBank/InstantMessaging/Save',
                        dataType: 'json',
                        data: $('#Message-popup').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    $('.addnew_popup').modal('hide');
                                    var url = "/DataBank/InstantMessaging/Index";
                                    window.location.href = url;
                                });
                            }


                            else {
                                swal.fire(result.message).then(function () {

                                });
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
            else {
                return false;
            }
        })
}

function OnEmployeesSaveClick() {
    debugger;
    swal.fire
        ({
            text: "Do you want to Save ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger;
                $.ajax
                    ({
                        type: 'POST',
                        url: '/DataBank/InstantMessaging/EmployeeSave',
                        dataType: 'json',
                        data: $('#empList').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    var url = "/DataBank/InstantMessaging/Index";
                                    window.location.href = url;
                                });
                            }


                            else {
                                swal.fire(result.message).then(function () {

                                });
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
            else {
                return false;
            }
        })
}

function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#EmpListTable').find('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('#EmpListTable').find('input[type=checkbox]').prop('checked', false);
    }
}

function ApplyFilter(mode) {

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/InstantMessaging/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&MessageCode=" + $('#MessageCode').val() ,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#EmpListDiv').html("");
                        $('#EmpListDiv').html(result);
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
                url: '/DataBank/InstantMessaging/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&MessageCode=" + $('#MessageCode').val() ,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#EmpListDiv').html("");
                        $('#EmpListDiv').html(result);
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
            url: '/DataBank/InstantMessaging/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&MessageCode=" + $('#MessageCode').val() ,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#EmpListDiv').html("");
                    $('#EmpListDiv').html(result);
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

function onSendClick(MessageCode) {
    debugger;
    swal.fire
        ({
            text: "Do you want to send?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger;
                $.ajax
                    ({
                        type: 'POST',
                        url: '/DataBank/InstantMessaging/Send',
                        data: { MessageCode: MessageCode },
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    var url = "/DataBank/InstantMessaging/Index";
                                    window.location.href = url;
                                });
                            }


                            else {
                                swal.fire(result.message).then(function () {

                                });
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
            else {
                return false;
            }
        })
}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("EmpListTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
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