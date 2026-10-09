function compareStartDateAndMaturityDate() {
    debugger;
    var strtDate = new Date($('#StartDate').val());
    var matDate = new Date($('#MaturityDate').val());
    if ($('#StartDate').val() != "" && $('#MaturityDate').val() != "") {
        if (strtDate > matDate) {
            $('#MaturityDate').addClass('is-invalid');
            $("#mDate").addClass('error-message');
            $("#mDate").html("Maturity Date should be greater than Start Date");
            return false;
        }
        else {
            $('#MaturityDate').removeClass('is-invalid');
            $("#mDate").removeClass('error-message');
            $("#mDate").html("");
            return true;
        }
    }
}

function validateAll() {
    var i = 0;    
    var TableLength = document.getElementById("popupTable").rows.length - 1;
    for (j = 0; j < TableLength; j++) {
        if (!validateById('#EmployeeVsInsuranceDetailsList_' + j + '__StartDate', 'stDate_' + j)) {
            i++;
        }
        if (!validateById('#EmployeeVsInsuranceDetailsList_' + j + '__MaturityDate', 'MtDate_' + j)) {
            i++;
        }
        if (!validateById('#EmployeeVsInsuranceDetailsList_' + j + '__PrincipalAmount', 'princAmt_' + j)) {
            i++;
        }
        if (!validateById('#EmployeeVsInsuranceDetailsList_' + j + '__PremiumAmount', 'PremAmt_' + j)) {
            i++;
        }
    }
    if (i == 0)
        return true
    else
        return false
    
}
function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var policy = $('#InsurancePolicy_Code').val();    
    
        var dataTable = $("#empVsInsuranceTable").DataTable({
            //"paging": true,
            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/EmployeeVsInsuranceDetails/ApplyFilter",
                "type": "POST",

                "datatype": "json",

                'data':

                    function (d) {
                        temp = '';

                        var searchId = $('#Filter_Code').val();
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&Policy=' + policy + '&dummy=dummy';                        
                        d.filter = temp;
                        

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#InsuranceDetailsEmployeeList_Count').val(d.recordsTotal)
                                      
                    for (var i = 0, ien = d.data.length; i < ien; i++) {                  

                        d.data[i].employeeName = '<input id="InsuranceDetailsEmployeeList_' + i + '__EmployeeGUID" type="hidden" value="' + d.data[i].employeeGUID + '">' + '<a onclick="onEmployeeClick(' + i+')">' + d.data[i].employeeName + '</a>'
                        //d.data[i].edit = '<a data-bs-toggle="tooltip" title="Edit" id = "empattendancesettings" type = "button" class="btn btn-icon btn-edit icon-only slider-btn" onclick="onEditClick('+i+')" > <i class="icon-xl fas fa-pencil-alt"></i> </a >'
                        d.data[i].edit = '   <a class="btn btn-icon btn-edit icon-only " onclick="onEditClick(' + i +')" data-bs-toggle="tooltip" title="Add"><i class="fa fa-plus" aria-hidden="true" ></i></a>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "id", "width": "10%" },
                { "data": "employeeName", "width": "25%"},
                { "data": "designation", "width": "15%"},
                { "data": "department", "width": "15%"},
                { "data": "location", "width": "15%"},
                { "data": "noOfInsurance", "width": "10%"},
                { "data": "edit", "width": "10%"},



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



function ApplyFilter(mode) {    
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData()
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            //showSpinner();
            fillData()
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}


function onEmployeeClick(i) {

    var EmployeeGI = $('#InsuranceDetailsEmployeeList_'+i+'__EmployeeGUID').val()
    var policy = $('#InsurancePolicy_Code').val();
    $.ajax({
        type: 'GET',
        url: "/PayRoll/EmployeeVsInsuranceDetails/GetEmployeeVsInsuranceDetails?EmployeeGUID=" + EmployeeGI + "&PolicyCode=" + policy,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.EmployeeVsInsuranceDetails-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}

function onDeleteClick(i) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $('#row_' + i).remove();
            var tableLength = $('#popupTable tr').length;
            var k = 0;
            for (let j = 0; j < tableLength; j++) {
                if (document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__InsurancePolicy_InsuranceCompany_Code') != null) {

                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__StartDate').name = 'EmployeeVsInsuranceDetailsList[' + k + '].StartDate';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__MaturityDate').name = 'EmployeeVsInsuranceDetailsList[' + k + '].MaturityDate';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PrincipalAmount').name = 'EmployeeVsInsuranceDetailsList[' + k + '].PrincipalAmount';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PremiumAmount').name = 'EmployeeVsInsuranceDetailsList[' + k + '].PremiumAmount';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__InsurancePolicy_InsuranceCompany_Code').name = 'EmployeeVsInsuranceDetailsList[' + k + '].InsurancePolicy.InsuranceCompany.Code';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__InsurancePolicy_Code').name = 'EmployeeVsInsuranceDetailsList[' + k + '].InsurancePolicy.Code';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PolicyNumber').name = 'EmployeeVsInsuranceDetailsList[' + k + '].PolicyNumber';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__TransactionCode').name = 'EmployeeVsInsuranceDetailsList[' + k + '].TransactionCode';

                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__StartDate').id = 'EmployeeVsInsuranceDetailsList_' + k + '__StartDate';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__MaturityDate').id = 'EmployeeVsInsuranceDetailsList_' + k + '__MaturityDate';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PrincipalAmount').id = 'EmployeeVsInsuranceDetailsList_' + k + '__PrincipalAmount';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PremiumAmount').id = 'EmployeeVsInsuranceDetailsList_' + k + '__PremiumAmount';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__InsurancePolicy_InsuranceCompany_Code').id = 'EmployeeVsInsuranceDetailsList_' + k + '__InsurancePolicy_InsuranceCompany_Code';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__InsurancePolicy_Code').id = 'EmployeeVsInsuranceDetailsList_' + k + '__InsurancePolicy_Code';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__PolicyNumber').id = 'EmployeeVsInsuranceDetailsList_' + k + '__PolicyNumber';
                    document.getElementById('EmployeeVsInsuranceDetailsList_' + j + '__TransactionCode').id = 'EmployeeVsInsuranceDetailsList_' + k + '__TransactionCode';
                    debugger
                    
                    k = k + 1;
                }
            }
        }
        else {
            return false;
        }
    })
    
}


function onEditClick(i) {
    debugger
    var EmployeeGI = $('#InsuranceDetailsEmployeeList_' + i + '__EmployeeGUID').val()
    $.ajax({
        type: 'GET',
        url: "/PayRoll/EmployeeVsInsuranceDetails/GetEmployeeInsuranceDetails?Policy=" + $('#InsurancePolicy_Code').val() + "&EmployeeGI=" + EmployeeGI,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#sliderView').html("");
                $('#sliderView').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}

function validateInsuranceDetails() {
    var i = 0;
    if (!validateById('#PolicyNumber', 'pNumber'))
        i++;
    if (!validateById('#StartDate', 'stDate'))
        i++;
  
    if (!validateById('#MaturityDate', 'mDate'))
        i++;
    else if (!compareStartDateAndMaturityDate())
        i++;
    if (!validateById('#PrincipalAmount', 'prAmt'))
        i++;
    if (!validateById('#PremiumAmount', 'prmAmt'))
        i++;
   
    if (i == 0)
        return true;
    else
        return false;

}

function onInsuranceDetailsSaveClick() {
    debugger;   
    if (validateInsuranceDetails()) {
        
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
                    url: '/PayRoll/EmployeeVsInsuranceDetails/Create',
                    dataType: 'json',
                    data: $('#InsuranceDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('addInsuranceDetails');
                                ApplyFilter('apply');

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
    else {
        return false;
    }
}
function onInsuranceDetailsListSaveClick() {
    debugger;   
    if (validateAll()) {
        
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
                    url: '/PayRoll/EmployeeVsInsuranceDetails/Update',
                    dataType: 'json',
                    data: $('#InsuranceDetailsListSave').serialize(),                    
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var m = $('.EmployeeVsInsuranceDetails-modal-lg');                                
                                m.modal('hide');
                                ApplyFilter('apply');

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
    else {
        return false;
    }
}

function ExportToExcel() {
    debugger;
    //if (validateDropdowns()) {

        var TableLength = $('#InsuranceDetailsEmployeeList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/EmployeeVsInsuranceDetails/ExportToExcel?Policy=" + $('#InsurancePolicy_Code').val() + "&totRecords=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    //}
}


function getPopup() {
    debugger;
    if ($('#InsurancePolicy_Code').val() != '') {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }
    else {
        validateById('#InsurancePolicy_Code', 'IPolicy');
        return false;
    }
    }

function onImportExcelClick() {

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#EmployeeInsuranceUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/EmployeeVsInsuranceDetails/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                            //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                            //});
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
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