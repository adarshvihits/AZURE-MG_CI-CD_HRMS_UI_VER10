var currentTab = 0;
var Tab_Name = 'activeTab';
var prevTab = 'activeTab';
var pagenumber = 1;
function tabClick(tabName, tabIndex) {
    debugger

    currentTab = tabIndex;
    currentTabName = tabName;
    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    GetList();
    prevTab = tabName;
    showSpinner();
}
function GetList() {
    debugger
    var TabIndex = currentTab;
    $.ajax({

        type: 'GET',
        url: '/Master/EventGroup/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html('');
                $('#ActiveList').html(result);
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
function OnAddNewClick() {
    var url = "/Master/EventGroup/Create";
    window.location.href = url;
}
function onEventChange() {
    var Event = $('#Event_Code').val();
    if (Event != 1) {
        $("#yearOfExpTab").removeClass('hide');
        $("#MultipleCombination1_Code").find("option").eq(1).show();
    }
    else
    {
        $("#yearOfExpTab").addClass('hide');
        $("#MultipleCombination1_Code").find("option").eq(1).hide();
    }
}
function onApplicabilityTick(checkbox) {
    if (checkbox.checked) {
        $('.YOExp').removeClass('hide');
    } else {
        $('.YOExp').addClass('hide');
    }
}
function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
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
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}
function radiobtnChange(sender) {
    debugger;
    var radioval = sender.value;
    if (radioval == 1) {
        $('.cmb2').removeClass('hide');
        $("#MultipleCombination1_Code").find("option").eq(1).hide();
        $("#MultipleCombination2_Code").find("option").eq(1).hide();
        if ($('#MultipleCombination1_Code option:selected').text() == "Employee") {
            $('#combination1tblbodyID').html("");
            $('#cmb1theadID').html("");
            $('#cmb1tID').html("");
            $('#MultipleCombination1_Code').val("");
        }
    }

    else {
        debugger;
        $("#MultipleCombination1_Code").find("option").eq(1).show();
        $('#combination2tblbodyID').html("");
        $('#cmb2theadID').html("");
        $('#MultipleCombination2_Code').val("");
        $('#totcomb2Count').html("");
        $('.cmb2').addClass('hide');
    }
}
function getCombinationDetails(sender, cmb) {
    debugger;
    var ddid = sender.value;
    if (cmb == 0) {
        $('#smallspinnerouter1').addClass('spinner-outer-small');
        $('#smallspinnerinner1').addClass('spinner-small');
        $('.spinner-outer-small').append(showSmallSpinner());
        selectedCombination1name = $('#MultipleCombination1_Code option:selected').text();
        $.ajax({
            type: 'GET',
            url: '/Master/EventGroup/GetCombinationDetails',
            dataType: 'html',
            data: { id: ddid, type: cmb, combinationName: selectedCombination1name },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#CombinationList1').html("");
                    $('#CombinationList1').html(result);
                    $('#totcomb1Count').html($('#Combinations1_Count').val());
                }
                hideSmallSpinner();
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSmallSpinner();

            }
        });
    }
    else {
        if ($('#MultipleCombination1_Code').val() == ddid) {
            swal.fire("Please Choose Different Combination");
            return false;
        }
        else {
            $('#smallspinnerouter2').addClass('spinner-outer-small');
            $('#smallspinnerinner2').addClass('spinner-small');
            $('.spinner-outer-small').append(showSmallSpinner());
            selectedCombination2name = $('#MultipleCombination2_Code option:selected').text();
            $.ajax({

                type: 'GET',
                url: '/Master/EventGroup/GetCombinationDetails',
                dataType: 'html',
                data: { id: ddid, type: cmb, combinationName: selectedCombination2name },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList2').html("");
                        $('#CombinationList2').html(result);
                        $('#totcomb2Count').html($('#Combinations2_Count').val());
                    }
                    hideSmallSpinner();
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });
        }
    }
}
function combination1searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination1tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
}
function combination2searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList2");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination2tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
}
function GetEmpList() {
    debugger
    $.ajax({
        type: 'GET',
        url: '/Master/EventGroup/GetEmpList',
        dataType: 'html',
        success: function (result) {
            var m = $('.addRecipientPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}
function fillData() {
    var filter1 = $('#BasicFilter').serialize();
    debugger
    const rows = $('#receipientListTable tbody tr');
    let dataList = [];
    rows.each(function () {
        const row = $(this);
        const appl = row.find('input[name$=".Applicable"]').val();
        const employeeCode = row.find('input[type=hidden]').val();
        dataList.push({
            EmployeeCode: employeeCode,
            Applicable: appl
        });
    });
    var dataTable = $("#addRecipientTable").DataTable({
            "pagingType": 'full_numbers',
            "destroy": true,
        "ajax": {

            "url": "/Master/EventGroup/GetEmpList",
            "type": "POST",
            "datatype": "json",
            "contentType": 'application/json',
            "data": function (d) {
                const filter = {
                    draw: d.draw,
                    start: d.start,
                    length: d.length,
                    searchValue: $('.dataTableSearchPopup').val() || ""
                };
                const model = {
                    Mode: $('#Mode').val(),
                    EventGroupCode: $('#EventGroupCode').val(),
                    filter: filter,
                    EmpList: dataList
                };
                return JSON.stringify(model);
            },
                "dataSrc": function (d) {
                    $('#EmpList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].designation = d.data[i].designation +'<input type="hidden" id="EmpList_' + i + '__EmployeeCode" name="EmpList[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '">';

                        if (d.data[i].applicable != false) {
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="EmpList_' + i + '__Applicable" name="EmpList[' + i + '].Applicable" type="checkbox" class="EmpList_Applicability" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                        }
                        else
                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="EmpList_' + i + '__Applicable" name="EmpList[' + i + '].Applicable" type="checkbox" class="EmpList_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'

                    }
                    return d.data;
                }

            },

            "columns": [
                { "data": "employeeID", "width": "30%" },
                { "data": "employeeName", "width": "30%" },
                { "data": "designation", "width": "30%" },
                { "data": "applicable", "width": "10%" }
            ],
            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "ordering": false,
            "order": [0, "asc"],
            "processing": "true",
            "language": {
                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
                sLengthMenu: "_MENU_",
                paginate: {
                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                    first: '<img src="/images/icons/Icon metro-next-1.png">',
                    last: '<img src="/images/icons/Icon metro-next.png">'
                }
            }
        });

    $('#addRecipientTable').on('page.dt', function () {
            debugger
            var info = dataTable.page.info();
            pagenumber = info.page + 1;

        });

    $('.ihits-table table').removeClass("dataTable no-footer");
    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");
    $(".dataTableSearchPopup").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });
    $('.search-slider-inner').click(function () {
        /*    dataTable.search('').draw();*/
        $('.dataTableSearchPopup').val('');
        $('.dataTableSearchPopup').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });
    $.fn.DataTable.ext.pager.numbers_length = 5;

}

function onCheckboxChange(sender, index) {

    debugger;
    checked = $(sender).is(':checked');
    var row = $(sender).closest('tr').html();
    var empcode = $('#EmpList_' + index + '__EmployeeCode').val();

    if (checked) {
        $('#receipientListTable tbody').append('<tr id="otSave_' + pagenumber + '_' + index + '">' + row + '</tr>');
        $('#EmpList_' + index + '__EmployeeCode').val(empcode);
        $('#EmpList_' + index + '__Applicable').attr('checked', true);
    }
    else {
       // $('#otSave_' + pagenumber + '_' + index).remove();

        // Remove the row with matching employee code
        $('#receipientListTable tbody tr').each(function () {
            const rowEmpcode = $(this).find('input[name$=".EmployeeCode"]').val();
            console.log(rowEmpcode)
            console.log(empcode)

            if (rowEmpcode === empcode) {
                $(this).remove();
            }
        });
    }

    $('#receipientListTable tbody').find("tr").each(function (currindex) {
        var tdd = $(this).find("td").eq(2);
        tdd.prevObject[2].getElementsByTagName('input')[0].id = 'EmpList_' + currindex + '__EmployeeCode';
        tdd.prevObject[2].getElementsByTagName('input')[0].name = 'EmpList[' + currindex + '].EmployeeCode';
    });
}
function onSaveClick(mode) {
    debugger
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;
    var Event = $('#Event_Code').val();
    var i = 0;
    if (!validateById('#Name', 'eventgrp_name')) {
        i++;
    }
    if (!validateById('#ShortName', 'shrtName')) {
        i++;
    }
    if (!validateById('#Event_Code', 'evnt')) {
        i++;
    }
    if (Event != 1 && $('#IsYOEApplicable').is(':checked')) {
        if (!validateById('#Years', 'yoe_Year')) {
            i++;
        }
        if (!validateById('#Months', 'yoe_Month')) {
            i++;
        }
        if ($('#Months').val() < 1 && $('#Years').val() < 1) {
            i++;
            swal.fire("Please enter Year of Experience");
        }
    }
    if ($('#combination1tblID').find('input[type="checkbox"]:checked').length < 1) {
        i++;
        swal.fire("Please select atleast one combination");
    }
    if ($('#IsMultiCombination:checked').val() == 1) {
        if ($('#combination2tblID').find('input[type="checkbox"]:checked').length < 1) {
            i++;
            swal.fire("Please select atleast one multiple combinations");
        }
    }    
    if ($('#receipientListTable tbody tr').length < 1) {
        i++;
        swal.fire("Please select atleast one recipient");
    }
    if (i == 0) {
        swal.fire({
            text: "Do you want to " + modeName,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                const rows = $('#receipientListTable tbody tr');
                let dataList = [];
                let formData = {

                    EventGroupCode: $('#EventGroupCode').val(),
                    Name: $('#Name').val(),
                    ShortName: $('#ShortName').val(),
                    IsYOEApplicable: $('#IsYOEApplicable').is(':checked'),
                    Event: {
                        Code: parseInt($('#Event_Code').val(), 10)
                    },
                    Remarks: $('#Remarks').val(),
                    Years: $('#Years').val(),
                    Months: $('#Months').val(),
                    Mode: $('#Mode').val(),
                    Active: $('#ActiveinactivedocCat').is(':checked'),
                    IsMultiCombination: $('input[name="IsMultiCombination"]:checked').val(),
                    MultipleCombination1: {
                        Code: parseInt($('#MultipleCombination1_Code').val(), 10)
                    },
                    MultipleCombination2: {
                        Code: $('#MultipleCombination2_Code').val() || 0
                    },
                    Combinations1: [],
                    Combinations2: [],
                    EmpList: dataList
                };
                $('#combination1tblID input[type=checkbox]:checked').each(function () {
                    const row = $(this).closest('tr');
                    if (parseInt(row.find('input[type=hidden]').val(), 10) != 0 && row.find('input[type=hidden]').val() != undefined) {
                        formData.Combinations1.push({
                            Applicable: row.find('input[type=checkbox]').is(':checked'),
                            Code: row.find('input[type=hidden]').val() || 0,
                        });
                    }
                });
                $('#combination2tblID input[type=checkbox]:checked').each(function () {
                    const row = $(this).closest('tr');
                    if (parseInt(row.find('input[type=hidden]').val(), 10) != 0 && row.find('input[type=hidden]').val() != undefined) {
                        formData.Combinations2.push({
                            Applicable: row.find('input[type=checkbox]').is(':checked'),
                            Code: row.find('input[type=hidden]').val() || 0,
                        });
                    }
                });
                rows.each(function () {
                    const row = $(this);
                    
                        const employeeCode = row.find('input[type=hidden]').val();

                        const appl = row.find('input[type=checkbox]').val();

                        dataList.push({
                            EmployeeCode: employeeCode,
                            Applicable: appl
                        });
                    
                });
                    $.ajax({

                        type: 'POST',
                        url: '/Master/EventGroup/Save',
                        dataType: 'json',
                        contentType: 'application/json',
                        data: JSON.stringify(formData),
                        success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/Master/EventGroup/Index";
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
    else
        return false;
}
function Edit(code) {
    debugger;
    var url = "/Master/EventGroup/Edit?Code=" + code;
    window.location.href = url;
}
function confirmDelete(Code) {
    debugger
    var TabIndex = currentTab;
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
                url: '/Master/EventGroup/Delete',
                dataType: 'json',
                data: { Code: Code },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/EventGroup/Index";
                            window.location.href = url;

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