function onChange() {
    
   
    $.ajax({

        type: 'GET',
        url: '/Master/VersionUpdate/GetVersionUpdateList',
        dataType: 'html',
        data: { VersionCode: $("#Version_Code").val()},
        success: function (result) {
            
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#VersionUpdateList').html("");
                $('#VersionUpdateList').html(result);
            }

            hideSpinner();
        },


        error: function (xhr, textStatus, errorThrown) {
            
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}
function validateVersionUpdateDropdowns() {
    var i = 0;
    ;
    if (!validateById('#Version_Code', 'Version')) {
        i++;
    }
    
    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}
function CheckExtensionAndSize(e) {
    ;


    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['xlsx'];
    if (size > 1024 * 1024)  //1,048,576 Bytes=1 MB
    {
        swal.fire('File size should be less than 1 MB');
        $(e).val('');
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only Excel file is allowded");
        $(e).val('');
        return false;

    }
    return true;

}


function GetNewVersionPopUp() {
    ;
    $.ajax({
        type: 'GET',
        url: "/Master/VersionUpdate/GetNewVersionPopUp",

        dataType: 'html',

        success: function (Data) {
            ;
            var m = $('.Exit_Questionnaire');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}
function SaveNewVersion() {
    ;

    
    //console.log($('#NewVersionsave').serialize());
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
                url: '/Master/VersionUpdate/SaveNewVersion',
                dataType: 'json',
                data: $('#NewVersionsave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.Exit_Questionnaire').modal('hide');

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

function AddNewVersionRow(tableId, i) {
    ;


    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;



    var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });


    template.find('td[id=grid_slno]').text(count + 1);
    $.each(template.find('.addObj'), function () {
        $(this).attr("id", "objAdd_" + count);
    });
    $.each(template.find('.delObj'), function () {
        $(this).attr("id", "objDel_" + count);
    });
    $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
    //------------to clear when new row---------



    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



    //---------------------------------

    //make numbers only inputs.
    $('.numbers-only').keypress(function (e) {
        if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
    });
    
    $('#objAdd_' + (count - 1)).addClass('hide');
    $('#objAdd_' + count).removeClass('hide');
    $('#objDel_' + count).removeClass('hide');
}




function DeleteNewVersionRow(index) {
    ;



    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {



            // if (confirm("Do you want to delete..?"))


            //{
            var count = 0;
            count = $('tr[id^="row_OI_"]').length;
            if (count > 1) {
                $("#row_OI_" + index).remove();
                for (var j = index + 1; j < count; j++) {
                    var template = $('#row_OI_' + j).clone(); // clone the row
                    var code = $("#Results_" + j + "__Name").val();

                    $.each(template.find('input, textarea'), function () {
                        var name = $(this).attr('name');
                        if (typeof name != 'undefined') {
                            name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('name', name);
                        }
                        var id = $(this).attr('id');
                        if (typeof id != 'undefined') {
                            id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('id', id);
                        }
                        //---------------------------------For row Increment When onkeyup
                        var onkeyup = $(this).attr('onkeyup');
                        if (onkeyup !== undefined) {
                            onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onkeyup', onkeyup);
                        }
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('select'), function () {
                        var name = $(this).attr('name');
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                        var id = $(this).attr('id');
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('button[type=button]'), function () {
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });


                    $.each(template.find('a'), function () {
                        
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });
                    $.each(template.find('.addObj'), function () {

                        $(this).attr("id", "objAdd_" + (j - 1));
                    });

                    template.find('td[id=grid_slno]').text(j);
                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));


                    $("#Results_" + (j - 1) + "__Name").val(code);


                }
            }
            else {
                $("#Results_0__Name").val("");


            }
        }
        if ((count - 1) == index) {
            ;
            $('#objAdd_' + index).addClass('hide');
            $('#objAdd_' + (index - 1)).removeClass('hide');
        }
    });
}
function AddNewOrEditClick(Type,GI, index) {
    ;
    currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#_VersionUpdateList" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/VersionUpdate/Edit',
                dataType: 'html',
                data: { VersionUpdateGI: GI },
                success: function (result) {
                    
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#CreateOrEditSlider').html("");
                        $('#CreateOrEditSlider').html(result);
                    }


                },


                error: function (xhr, textStatus, errorThrown) {
                    
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);


                }
            });

        }

    }


    else {
        $.ajax({

            type: 'GET',
            url: '/Master/VersionUpdate/Create',
            dataType: 'html',
            data: { VersionUpdateGI: GI},
            success: function (result) {
                
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#CreateOrEditSlider').html("");
                    $('#CreateOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

    }

}
function VersionUpdateSaveClick(mode) {
    ;
    if (validateVersionUpdateDetails()) {

        //console.log($('#StateSave').serialize());
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/VersionUpdate/Save',
                    dataType: 'json',
                    data: $('#VersionUpdateSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddVersionUpdateDiv');

                                var VersionCode=$("#Version_Code").val()
                                var url = "/Master/VersionUpdate/GetVersionUpdateList?VersionCode=" + VersionCode;
                                $('#VersionUpdateList').load(url, function () { });

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
    else {
        return false;
    }
}

function validateVersionUpdateDetails() {
    var i = 0;

    if (!validateById('#VersionUpdateVersion_Code', 'VersionUpdateVersion')) {
        document.getElementById("VersionUpdateVersion_Code").focus();
        i++;
    }
    if (!validateById('#Module_Code', 'VersionUpdateModule')) {
        document.getElementById("Module_Code").focus();
        i++;
    }
    if (!validateById('#Type_Code', 'VersionUpdateType')) {
        document.getElementById("Type_Code").focus();
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function onDeleteClick(Type, GI,index) {

    ;

    if ($("#_VersionUpdateList" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'POST',
            url: '/Master/VersionUpdate/Delete',
            dataType: 'html',
            data: { VersionUpdateGI: GI},
            success: function (result) {
                
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteSlider').html("");
                    $('#deleteSlider').html(result);
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
    
}
function confirmDelete(GI) {

    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: '/Master/VersionUpdate/DeleteVersionUpdate',
                dataType: 'json',
                data: $('#deleteVersionUpdate').serialize(),
               
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');

                            var VersionCode = $("#Version_Code").val()
                            var url = "/Master/VersionUpdate/GetVersionUpdateList?VersionCode=" + VersionCode;
                            $('#VersionUpdateList').load(url, function () { });

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);

                }
            });
        }
        else {
            return false;
        }
    })
}
function onExportToExcelClick() {


    var TableLength = $('#VersionUpdateList_Count').val();
    if (TableLength > 0) {
        url = "/Master/VersionUpdate/ExportToExcel?VersionCode=" + $('#Version_Code').val() + "&totalRows=" + TableLength + "&VersionName=" + $('#Version_Code option:selected').text() ;

            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }


}

function onImportExcelClick() {
    debugger;

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var salaryhold = new FormData($('#VersionUpdate')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/Master/VersionUpdate/ImportFromExcel',
                contentType: false,
                processData: false,
                data: salaryhold,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {

                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        if ($.fn.dataTable.isDataTable('#VersionUpdateTable')) {
                            $('#VersionUpdateTable').DataTable().destroy();
                        }
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

