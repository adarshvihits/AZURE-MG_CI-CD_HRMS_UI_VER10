var dropdownValues;


function OnTypeChange(sender, id) {
    debugger
    var type = $('#LeaveGroup_Code').val();
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'entrytype')) {
        i++;
    }

    if (i == 0) {
        $.getJSON("/DataBank/BulkLeaveApplicablity/GetLeaveGroupApplicableLeavesDropdown", { TypeCode: type }, function (data) {
            $('#LeaveType_Code option').remove();

            for (var i = 0; i < data.length; i++) {
                $('#LeaveType_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
    }
}


function GetList() {
    debugger;
    $.ajaxSetup({ async: false });
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var LeaveGroupCode = $('#LeaveGroup_Code').val();
    var LeaveTypeCode = $('#LeaveType_Code').val();
    var Selection = $('input[name="radioSelection"]:checked').val();

    if (LeaveTypeCode != '' && LeaveGroupCode != '') {
        showSpinner();
        $.ajax({
            type: 'GET',
            async: true,
            url: '/DataBank/BulkLeaveApplicablity/GetApplicableList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&LeaveTypeCode=" + LeaveTypeCode + "&LeaveGroupCode=" + LeaveGroupCode + "&radioSelection=" + Selection,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ApplicableListView').html("");
                    $('#ApplicableListView').html(result);

                    hideSpinner();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
}


function ApplyFilter(mode) {

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            GetList()
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            GetList()
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList()
        //    CloseSlider('basicFilter');
    }
}




function searchRows() {
    debugger;
    var input, filter, table, tr, tdID, tdName, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("dataTableSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("ApplicabilityListTable");
    tr = table.getElementsByTagName("tr");
    var tbody = table.getElementsByTagName("tbody")[0];
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 1; i < tr.length; i++) {
        tdID = tr[i].getElementsByTagName("td")[0];
        tdName = tr[i].getElementsByTagName("td")[1];

        if (spannedRows > 0) {
            tr[i].style.display = show ? "" : "none";
            spannedRows--;
            if (!show) noOfRows--;
        } else if (tdID || tdName) {
            let idMatch = tdID && tdID.innerHTML.toUpperCase().indexOf(filter) > -1;
            let nameMatch = tdName && tdName.innerHTML.toUpperCase().indexOf(filter) > -1;

            if (idMatch || nameMatch) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            let rs = tdID ? tdID.getAttribute("rowspan") : null;
            if (rs && rs > 1) {
                show = idMatch || nameMatch;
                spannedRows = rs - 1;
            }
        }
    }

    if (noOfRows == 1) {
        $(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='8'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }
}


function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var AppliTableLength = $('#ApplicabilityListTable tbody tr').filter(function () {
        var visible = $(this).is(':visible');
        var hasData = $(this).find('td').filter(function () {
            return $.trim($(this).text()) !== '';
        }).length > 0;
        return visible && hasData;
    }).length;

    var TableLength = $('#BulkLeaveApplicablityList_Count').length;
    if (TableLength > 0 || AppliTableLength > 0) {
        url = "/DataBank/BulkLeaveApplicablity/ExportToExcel?LeaveTypeCode=" + $('#LeaveType_Code').val() + "&LeaveGroupCode=" + $('#LeaveGroup_Code').val() + "&Name=" + $('#LeaveType_Code option:selected').text() + "&radioSelection=" + $('input[name="radioSelection"]:checked').val() + "&totRecords=" + TableLength + "&data=" + filter;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
        return;
    }

}



function ImportFromExcelPopUp() {
    debugger

    var m = $('.bd-example-modal-lg');

    var LeaveTypeText = $('#LeaveType_Code option:selected').text();
    var LeaveTypeCode = $('#LeaveType_Code').val();
    var LeaveGroupCode = $('#LeaveGroup_Code').val();
    var additionalInfo = "(" + LeaveTypeText  /*+ LeaveTypeCode + LeaveGroupCode*/ + " Should be 'Y' OR 'N')";
    $('#fieldName').html(`${LeaveTypeText} ${additionalInfo}`);


    m.modal('show');
}



function onImportExcelClick() {
    debugger;
    var MPPersonalInfo = new FormData($('#BulkLeaveApplicabilty')[0]);
    $("#Excel_FileData").val('');
    $.ajax({
        type: 'POST',
        url: '/DataBank/BulkLeaveApplicablity/ImportFromExcel?LeaveTypeName=' + $('#LeaveType_Code option:selected').text() + "&LeaveTypeCode=" + $('#LeaveType_Code').val() + " &LeaveGroupCode=" + $('#LeaveGroup_Code').val(),
        contentType: false,
        processData: false,
        data: MPPersonalInfo,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {
                    GetList()
                    var m = $('.bd-example-modal-lg');
                    m.modal('hide');
                });
            }
            else {
                $("#Excel_FileData").val('');
                if (result.message != null) {
                    $('#Unmatched').html('')
                    swal.fire(result.message);
                }
                else {
                    $('#Unmatched').html(result)
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

function onSaveApplicabilityClick() {
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
                url: '/DataBank/BulkLeaveApplicablity/ApplicabilitySave',
                dataType: 'json',
                data: $('#EmpList').serialize(),
                success: function (result) {
                    if (result.success == true) {

                        swal.fire(result.message).then(function () {
                            GetList();
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