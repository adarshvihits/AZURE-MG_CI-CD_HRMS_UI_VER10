function OnTypeChange(sender, id) {
    debugger
    var type = $('#Year_Code').val();
    var i = 0;
    if (!validateById('#Year_Code', 'entrytype')) {
        i++;
    }

    if (i == 0) {
        $.getJSON("/Master/SpecialHolidayApplicability/GetSpecialHolidayDropdown", { TypeCode: type }, function (data) {
          /*  $('#HolidayCode option').remove();*/

            for (var i = 0; i < data.length; i++) {
                $('#HolidayCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
    }
    }
function GetList() {
  
    var type = $('#HolidayCode').val();

        showSpinner();
        $.ajax({
            type: 'GET',
            url: '/Master/SpecialHolidayApplicability/GetSpecialholidayEmpList?HolidayCode=' + type,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#List').html("");
                    $('#List').html(result);
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
function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function searchRows() {
    debugger;
    var input, filter, table, tr, tdID, tdName, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("dataTableSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("SpecialHolidaytable");
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
    var activeTableLength = $('#SpecialHolidayEmpList_Count').val();
    var type = $('#Year_Code').val();
    var YearName = $('#Year_Code option:selected').text();
    var HolidayCode = $('#HolidayCode').val();
    var HolidayName = $('#HolidayCode option:selected').text();
  
    if (activeTableLength > 0) {
        var url = "/Master/SpecialHolidayApplicability/ExportToExcel?year=" + YearName + "&holiday=" + HolidayName + "&HolidayCode=" + HolidayCode + "&data=" + filter;
        window.location.href = url; // triggers the Excel download
    } else {
        Swal.fire(NoDataFoundMsg); // ensure correct casing: Swal not swal
    }
}

function Save() {
    debugger;
    var Year = $('#Year_Code').val()
    var type = $('#HolidayCode').val();
    if (Year != '' && type != '') {
        // var type = $('#HolidayCode').val();
        swal.fire({
            text: "Do you want to Save?",
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
                    url: '/Master/SpecialHolidayApplicability/Save',
                    dataType: 'json',
                    data: $('#SpecialList').serialize(),
                    success: function (result) {
                        hideSpinner();
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

                    error: function (xhr) {
                        hideSpinner();
                        $('body').html(xhr.responseText);
                    }
                });

            }
            else {
                return false;
            }
        });
    }
}

function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();


    }
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;

    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;

        $('#FullPermission').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}
            
    
