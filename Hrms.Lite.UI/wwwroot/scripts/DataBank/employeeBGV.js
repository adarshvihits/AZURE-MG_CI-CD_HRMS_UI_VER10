var currentTab;
var currentTab = 0;
var prevTab = 'PendingTab';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    fillData();
    prevTab = Tab;
}

function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    hideSpinner();
    var dataTable = $("#EmpBGVTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/EmployeeBGV/GetList",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var searchId = $('#Filter_Code').val();
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&TabIndex=' + currentTab + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                $('#EmployeeBGVList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    if (currentTab==0)
                        d.data[i].employeeName = '<a href="/DataBank/EmployeeBGV/Create?EmployeeGI=' + d.data[i].employeeGI + '&ApplicantCode=' + d.data[i].applicantCode + '">' + d.data[i].employeeName + '</a>'
                    else
                        d.data[i].employeeName = '<a href="/DataBank/EmployeeBGV/Preview?EmployeeGI=' + d.data[i].employeeGI + '&ApplicantCode=' + d.data[i].applicantCode + '">' + d.data[i].employeeName + '</a>'

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                }
                $('.PendingTabCount').html(d.pendingCount);
                $('.CompletedTabCount').html(d.completedCount);
                return d.data;
            }
        },
        "columns": [
            { "data": "employeeID"},
            { "data": "employeeName"},
            { "data": "applicantID" },
            { "data": "location" },
            { "data": "designation"},
            { "data": "funcDesignation" },
            { "data": "doj" },
            { "data": "funcState" },
        ],
        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
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
    $('.ihits-table table').removeClass("dataTable no-footer");
    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");
    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });
    $('.search-slider-inner').click(function () {
        dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
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
    const myTimeout = setTimeout(tooltipHover, 500);
}
function onRelationshipChange() {
    debugger
    var code = $('#Details_RelationshipWithCandidate_Code').val();
    if (code == 4)
        $("#detailText").removeClass('hide');
    else
        $("#detailText").addClass('hide');
}
function SaveBGV() {
    var i = 0;

        if (!validateById('#Details_RefereeName', 'referee_Name')) {
            i++;
        }
        if (!validateById('#Details_ContactInformation', 'contactinfo')) {
            i++;
        }
        if (!validateById('#Details_TitleAndCompanyName', 'titleAndCompany')) {
            i++;
        }
        if (!validateById('#Details_RelationshipWithCandidate_Code', 'relationwithCand')) {
            i++;
        }
        if ($('#Details_RelationshipWithCandidate_Code').val() == 4) {
            if (!validateById('#Details_OtherDetails', 'otherDetail')) {
                i++;
            }
        }
        if (!validateById('#Details_HowManyYears_Code', 'year')) {
            i++;
    }
    if ($('#Details_TypeOfReference').val() == '') {
        if (!validateById('#Details_TypeOfReference', 'reftype')) {
            i++;
        }
    }
        if (!validateById('#Details_BGVCompanyName', 'bgvCompany')) {
            i++;
        }
        if ($('#Details_HowManyYears_Code').val() != 1) {
            if (!validateById('#Details_DurationYears_Name', 'yearsduration')) {
                i++;
            }
        }
        if (!validateById('#Details_DurationMonths_Code', 'monthsduration')) {
            i++;
        }
        if (!validateById('#Details_BGVCandidatePosition', 'candposition')) {
            i++;
        }
        if (!validateById('#Details_BGVRehire', 'bgvRehiring')) {
            i++;
        }
        if (!validateById('#Details_BGVReasonForExit', 'exitReason')) {
            i++;
        }

    if (i == 0) {

        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/EmployeeBGV/SaveBGV',
                    dataType: 'json',
                    data: $('#EmployeebgvSave').serialize(),
                    success: function (result) {
                        debugger
                        if (result.success) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = "/DataBank/EmployeeBGV/Index";
                                window.location.href = url;
                            });
                        }
                        else {
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
            else {
                return false;
            }
        })
    }
    else
        return false;
}
function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}
function ExportToExcel() {
    debugger;
    var tableLength = $('#EmployeeBGVList_Count').val();
    var tabName;
    if (currentTab == 0)
        tabName = "Pending";
    else
        tabName = "Completed";
    if (tableLength > 0) {
        var param = $('#BasicFilter').serialize() +'&TabIndex=' + currentTab + '&tabName=' + tabName + '&TotalRows=' + tableLength;
        url = '/DataBank/EmployeeBGV/ExportToExcel?data=' + param;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function capitalizeAllLetter(val) {

    if (val.value === "") {
        return;
    }

    // Trim spaces and normalize multiple spaces
    val.value = val.value
        .replace(/(^\s*)|(\s*$)/gi, "")   // remove leading & trailing spaces
        .replace(/[ ]{2,}/gi, " ")        // replace multiple spaces with single space
        .replace(/\n +/, "\n");

    // Convert entire string to uppercase
    val.value = val.value.toUpperCase();

    return val.value;
}