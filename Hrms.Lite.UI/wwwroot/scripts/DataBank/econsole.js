var currentTab = 0;
var Tab_Name = 'activeTab';
var prevTab = 'activeTab';

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


function ApplyFilter(mode) {
    debugger
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
function GetList() {
    debugger
    var TabIndex = currentTab;
        $.ajax({

            type: 'GET',
            url: '/DataBank/Econsole/GetList',
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
    var url = "/DataBank/Econsole/Create";
    window.location.href = url;
}

function ontypechange()
{
    var TypeName = $('#ConsoleType_Code option:selected').text();
    
    if (TypeName === 'Event wise')  {
        $("#perp").addClass('hide');
        $("#eventwise").removeClass('hide');
        if ($('#IsMultiCombination:checked').val() == 0)
            $("#MultipleCombination1_Code").find("option").eq(1).show();

    }
    else {
        $("#eventwise").addClass('hide');
        $("#perp").removeClass('hide');
        $("#MultipleCombination1_Code").find("option").eq(1).hide();

    }
}
function radiobtnChange(sender) {
    debugger;
    var radioval = sender.value;
    if (radioval == 1) {
        $('.emp_doj_div').addClass('hide');
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
        if ($('#ConsoleType_Code option:selected').text() === 'Event wise') {
            $("#MultipleCombination1_Code").find("option").eq(1).show();
        }
        $('#combination2tblbodyID').html("");
        $('#cmb2theadID').html("");
        $('#MultipleCombination2_Code').val("");
        $('#totcomb2Count').html("");
        $('.cmb2').addClass('hide');
    }

}

function onDOJClick(sender) {
    debugger;
    if ($(sender).is(':checked')) {
        $('#ifdjoAppli').removeClass('hide');
        $('#ifdjoAppli_date').removeClass('hide');        
    }
    else {
        $('#ifdjoAppli').addClass('hide');
        $('#ifdjoAppli_date').addClass('hide');
    }
}
function getCombinationDetails(sender, cmb) {
    debugger;
    var ddid = sender.value;
    var id = $('#MultipleCombination1_Code option:selected').val();
    var DojApplcheck = $('#DojAppli').is(':checked');
    var DojAppl;
    if (DojApplcheck == true)
        DojAppl = 1;
    else
        DojAppl = 0;
    var multicombi = $('#IsMultiCombination:checked').val();
    var BeforeAfter = $('#BeforeAfterButton:checked').val();
    var Date = $('#Date').val();
    if ($('#MultipleCombination1_Code option:selected').text() == "Employee")
        $('.emp_doj_div').removeClass('hide');
    else
        $('.emp_doj_div').addClass('hide');
    if (multicombi == 0 && $('#MultipleCombination1_Code option:selected').text() == "Employee") {

        $('#smallspinnerouter1').addClass('spinner-outer-small');
        $('#smallspinnerinner1').addClass('spinner-small');
        $('.spinner-outer-small').append(showSmallSpinner());
        selectedCombination1name = $('#MultipleCombination1_Code option:selected').text();
        $.ajax({
            type: 'GET',
            url: '/DataBank/Econsole/GetCombinationDetails',
            dataType: 'html',
            data: { id: id, type: multicombi, combinationName: selectedCombination1name, DojAppl: DojAppl, BeforeAfter: BeforeAfter, Date: Date },

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
    else if (cmb == 0 && $('#MultipleCombination1_Code option:selected').text() != "Employee") {
        $('#smallspinnerouter1').addClass('spinner-outer-small');
        $('#smallspinnerinner1').addClass('spinner-small');
        $('.spinner-outer-small').append(showSmallSpinner());
        selectedCombination1name = $('#MultipleCombination1_Code option:selected').text();
        $.ajax({
            type: 'GET',
            url: '/DataBank/Econsole/GetCombinationDetails',
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
                url: '/DataBank/Econsole/GetCombinationDetails',
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
function Preview() {

    debugger;
    if ($('#File_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);//($('.' + Doc)[0].files[0]);
        if (path == "") {
            swal.fire("No file Uploaded")
        }
        else {
            window.open(path);
        }
    }
    else {
        var url = "/DataBank/Econsole/ViewDocument?Code=" + $('#consoleCode').val();
        $.ajax({
            url: url,
            type: "GET",
            success: function () {
                // File exists → open in new tab
                window.open(url, "_blank");
            },
            error: function (xhr) {
                let response = JSON.parse(xhr.responseText);
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message  // localized message
                });
            }
        });
    }
    
}
function ViewDocument(Code) {

    debugger;
   
    var url = "/DataBank/Econsole/ViewDocument?Code=" + Code;
        $.ajax({
            url: url,
            type: "GET",
            success: function () {
                // File exists → open in new tab
                window.open(url, "_blank");
            },
            error: function (xhr) {
                let response = JSON.parse(xhr.responseText);
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: response.message  // localized message
                });
            }
        });

}

function CheckExtensionAndSize(e) {
    debugger;
    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf', 'doc', 'docx'];
    if (size > 5 * 1024 * 1024)
    {
        swal.fire('File size should be less than 5 MB');
        $(e).val('');
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf files are allowed");//,doc and docx
        $(e).val('');
        return false;
    }
    return true;
}
function onFileChange() {
    debugger;

    var fileInput = document.getElementById('File_FileData');
    var label = document.getElementById('fileLabel');

    if (fileInput.files.length === 0) {
        label.innerHTML = "No File Chosen";
    } else {
        var fileName = fileInput.files[0].name;

        let replacemnt = '..';
        let shortName = fileName.slice(0, 18);

        label.innerHTML = shortName.substring(0, shortName.length - 2) + replacemnt;
    }
}
function onDateChange() {
    debugger;
    var AssignmentDate = $('#AssignmentDate').val();
    var FirstCompletionDate = $('#FirstCompletionDate').val();
    var SecondCompletionDate = $('#SecondCompletionDate').val();
    var D1 = new Date(AssignmentDate);
    var D2 = new Date(FirstCompletionDate);
    var D3 = new Date(SecondCompletionDate);
    if (D1 > D2) {
        swal.fire("First Completion date should be greater than Assignment date");
        $("#FirstCompletionDate").val('');
        return false;
    }
    else if (D2 > D3) {
        swal.fire("Second Completion date should be greater than First Completion date");
        $("#SecondCompletionDate").val('');
        return false;
    }
    else if (D1 > D3) {
        swal.fire("Second Completion date should be greater than Assignment date");
        $("#FirstCompletionDate").val('');
        return false;
    }
    else
        return true;
}
function onDayChange() {
    debugger;
    var AssignmentDay = parseInt($('#AssignmentDay').val(), 10);
    var FirstCompletionDay = parseInt($('#FirstCompletionDay').val(), 10);
    var SecondCompletionDay = parseInt($('#SecondCompletionDay').val(), 10);
    //if (AssignmentDay == 0 || FirstCompletionDay == 0 || SecondCompletionDay == 0 ) {
    //    return;
    //}
    if (AssignmentDay > FirstCompletionDay) {
        if (AssignmentDay == 0 || FirstCompletionDay == 0 ) {
            return;
        }
        swal.fire("First Completion day should be greater than Assignment day");
        $("#FirstCompletionDay").val('');
        return false;
    }
    else if (FirstCompletionDay > SecondCompletionDay) {
        if ( FirstCompletionDay == 0 || SecondCompletionDay == 0) {
            return;
        }
        swal.fire("Second Completion day should be greater than First Completion day");
        $("#SecondCompletionDay").val('');
        return false;
    }
    else if (AssignmentDay > SecondCompletionDay) {
        if (AssignmentDay == 0  || SecondCompletionDay == 0) {
            return;
        }
        swal.fire("Second Completion day should be greater than Assignment day");
        $("#FirstCompletionDay").val('');
        return false;
    }
    else
        return true;
}
function onSaveClick(mode) {
    debugger
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;

    var TypeName = $('#ConsoleType_Code option:selected').text();

    var i = 0;
    if (!validateById('#Name', 'econsole_name')) {
        i++;
    }
    if (!validateById('#ConsoleType_Code', 'type')) {
        i++;
    }
    if (!validateById('#ConsoleCategory_Code', 'category')) {
        i++;
    }
    if (!validateById('#ViewOrDownloadable', 'filetype')) {
        i++;
    }    
    if (TypeName === 'Event wise') {
        if (!validateById('#AssignmentDate', 'assignDate')) {
            i++;
        }
        if (!validateById('#FirstCompletionDate', 'frstComDate')) {
            i++;
        }
        if (!validateById('#SecondCompletionDate', 'secComDate')) {
            i++;
        }
    }
    else {
        if (!validateById('#AssignmentDay', 'assignDay')) {
            i++;
        }
        if (!validateById('#FirstCompletionDay', 'frstComDay')) {
            i++;
        }
        if (!validateById('#SecondCompletionDay', 'secComDay')) {
            i++;
        }
    }   
    if ($('#File_FileData').val() == '' && $('#File_FileName').val() == '') {
        i++;
        swal.fire("Please upload the document");
    }
    if ($('#combination1tblID').find('input[type="checkbox"]:checked').length < 1) {
        i++;
        swal.fire("Please select atleast one combination");
    }
    debugger;
    if ($('#IsMultiCombination:checked').val() == 1) {
        debugger;
        if ($('#combination2tblID').find('input[type="checkbox"]:checked').length < 1) {
            i++;
            swal.fire("Please select atleast one multiple combinations");
        }
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
                var ECData = new FormData($('#consoleSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/Econsole/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: ECData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/Econsole/Index";
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
function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
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
function decimalValidation(event) {
    debugger;

    const input = event.target.value;
    const parts = input.split('.');
    if (parts.length === 2) {
        if (parts[1].length > 0 && !['0', '5'].includes(parts[1])) {
            event.target.value = parts[0] + '.' + parts[1].slice(0, -1);
            swal.fire("only 0 and 5 are allowed after decimal");
            return false;
        }
        return true;
    }
}
function Edit(code) {
    debugger;
    var url = "/DataBank/Econsole/Edit?Code=" + code;
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
                url: '/DataBank/Econsole/Delete',
                dataType: 'json',
                data: { Code: Code },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/Econsole/Index";
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