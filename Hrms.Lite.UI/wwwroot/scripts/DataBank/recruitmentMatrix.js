
function CloseeSlider(id) {
    debugger;
    $("." + id).removeClass('slider-forms-open');
    $(".slider-forms").removeClass('new-slider-expand');
    $("." + id).removeClass('slider_new_close');
    $(".form-overlay").remove();
    if ($.fn.DataTable.isDataTable('.RoleMapping')) {
        debugger;
        $('.RoleMapping').DataTable().destroy(); // destroy it
    }
}
function initRecruitmentMatrixTable() {
    debugger;
    //  Check if the DataTable already exists
    if ($.fn.DataTable.isDataTable('#RecruitmentMatrixTable')) {
        $('#RecruitmentMatrixTable').DataTable().destroy(); // destroy it
    }
    if ($.fn.DataTable.isDataTable('#RoleMappingTable')) {
        debugger;
        $('#RoleMappingTable').DataTable().destroy(); // destroy it
    }
}

function fillData() {

    var WorkFlow = $('#WorkFlowList').val();


    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#RecruitmentMatrixTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/RecruitmentMatrix/GetListData",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + 'draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&WorkFlow=' + WorkFlow + '&Region=' + Region + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#RecruitmentMatrixList_Count').val(d.recordsTotal);

                debugger;


                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].department = '<a data-bs-toggle="tooltip" title="Edit"  type="button" ' +
                        'onclick="onEditClick(' + "'" + d.data[i].RecruitmentMatrixGI + "'" + ')" ' +
                        'class="btn btn-icon btn-edit icon-only slider-btn">' +
                        '<i class="icon-xl fas fa-pencil-alt"></i></a>';
                }
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "workFlowName", "width": "10%" },
            { "data": "roleName", "width": "15%" },
            { "data": "department", "width": "15%" },

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
        //dataTable.search('').draw();
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




}



function getList() {
    debugger;
    var TemplateList = $('#TemplateList').val();
    var Country = $('#Country').val();
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/DataBank/RecruitmentMatrix/GetList',
        dataType: 'html',
        data: { TemplateList: TemplateList, Country: Country },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload(); initRecruitmentMatrixTable(); // <-- reinitialize here after DOM is updated
            }
            else {

                $('#RecruitmentMatrixlist').html("");
                $('#RecruitmentMatrixlist').html(result);
                initRecruitmentMatrixTable(); // <-- reinitialize here after DOM is updated
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
function getCreate() {
    debugger;
    $.ajax({

        type: 'GET',
        url: '/DataBank/RecruitmentMatrix/Create',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();

            }

            else {
                /*$('body').append('<div class="form-overlay"></div>');*/
                $('#CreateSlider').html("");
                $('#CreateSlider').html(result);

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
function getDefaultSettings() {
    var url = "/DataBank/RecruitmentMatrixDefaultSettings/Index";

    //console.log(url);
    window.location.href = url;
    return true
}

function validateProceed() {
    debugger
    var i = 0;

    if (!validateById('#NoOfLevels', 'noOfLevels'))
        i++;

    if (i == 0) {
        return true;
    }

    else {
        return false;
    }
}

function GetRoleListDetails() {
    if (validateProceed()) {
        $.ajax({
            type: 'POST',
            url: '/DataBank/RecruitmentMatrix/GetAuthorityEntity',
            data: $('#RecruitmentMatrixSave').serialize(), cache: false,
            success: function (result) {
                //console.log(result);
                // Inject the returned partial view into the target div
                $('#RoleList').html(result);
                $('#RoleList').show();

                //$('#RoleList').empty().html(result).show();
                $('#ValidationMessage').val($('#ValidationMessageTemp').val());
                $('#AlreadyExistsStatus').val($('#AlreadyExistsStatusTemp').val());

                // Now update ApprovalRoleList role codes and short names
                var count = parseInt($('#ApprovalRoleListCount').val() || 0);
                for (let i = 0; i < count; i++) {
                    let roleCode = $(`#RoleCodeTemp_${i}`).val();
                    let shortName = $(`#NameTemp_${i}`).val();

                    // Set selected role code in dropdown ApprovalRoleList[0].Role.Code
                    let $dropdown = $(`select[name='ApprovalRoleList[${i}].Role.Code']`);//$(`#RoleCode_${i}`);
                    if ($dropdown.length && roleCode) {
                        $dropdown.val(roleCode);
                    }

                    //// Set shortname if needed in a hidden input
                    //let $shortnameInput = $(`input[name='ApprovalRoleList[${i}].Role.Name']`);
                    //if ($shortnameInput.length) {
                    //    $shortnameInput.val(Name);
                    //}
                }
                $('#AMSubmit').prop('disabled', false);
            },
            error: function (xhr) {
                // Handle unauthorized/session timeout gracefully
                if ($(xhr.responseText).find(".Login_Session").length > 0) {
                    window.location.reload();
                } else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        });
    }
}


function GetRoleDetails() {
    NoOfLevels = $('#NoOfLevels').val();

    if (parseInt(NoOfLevels) > 0) {
        $('#RoleList').show();
        var url = "/DataBank/RecruitmentMatrix/GetRoleList";
        $('#RoleList').load(url, { NoOfLevels: NoOfLevels }, function () { });
    }
}
function GetEditRoleDetails() {
    //NoOfLevels = $('#NoOfLevels').val();
    //RecruitmentMatrixGI = $('#RecruitmentMatrixGI').val();
    //if (parseInt(NoOfLevels) > 0) {
    //    $('#RoleList').show();
    //    var url = "/DataBank/RecruitmentMatrix/GetEditRoleList";
    //    $('#RoleList').load(url, { NoOfLevels: NoOfLevels, RecruitmentMatrixGI: RecruitmentMatrixGI }, function () { });
    //}

    if (validateProceed()) {
        $.ajax({
            type: 'POST',
            url: '/DataBank/RecruitmentMatrix/GetEditRoleList',
            data: $('#RecruitmentMatrixSave').serialize(),
            success: function (result) {

                // Inject the returned partial view into the target div
                $('#RoleList').html(result);
                $('#RoleList').show();
            },
            error: function (xhr) {
                // Handle unauthorized/session timeout gracefully
                if ($(xhr.responseText).find(".Login_Session").length > 0) {
                    window.location.reload();
                } else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        });
    }
}
function onEditClick(RecruitmentMatrixGI) {
    var TemplateList = $('#TemplateList').val();
    showSpinner();
    debugger;
    $.ajax({

        type: 'GET',
        url: '/DataBank/RecruitmentMatrix/Edit',
        dataType: 'html',
        data: { TemplateList: TemplateList },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EditSlider').html("");
                $('#EditSlider').html(result);
                //$('#LevelList').html("");
                //$('#LevelList').html(result);
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


function getDetails(RecruitmentMatrixGI) {
    $.ajax({

        type: 'GET',
        url: '/DataBank/RecruitmentMatrix/GetDetails',
        dataType: 'html',
        data: { RecruitmentMatrixGI: RecruitmentMatrixGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ViewSlider').html("");
                $('#ViewSlider').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    //$('#AddLevelSlider').removeClass('hide');
}

function validateSave() {
    debugger
    var i = 0;
    if (!validateById('#WorkFlow_Code', 'workFlow'))
        i++;
    if (!validateById('#NoOfLevels', 'noOfLevels'))
        i++;

    var MappingTableLength = document.getElementById("RoleMappingTable").rows.length - 1;
    for (let index = 0; index < MappingTableLength; index++) {
        if (!validateById('#ApprovalRoleList_' + index + '__Role_Code', 'RoleCode_' + index))
            i++;

    }
    if (i == 0) {
        return true;
    }

    else {
        return false;
    }
}

function Save() {
    debugger;
    var ValidationMessage = $('#ValidationMessage').val();
    var AlreadyExistsStatus = $('#AlreadyExistsStatus').val();
    if (AlreadyExistsStatus == '1') {
        var msg = ValidationMessage;
    }
    else {
        var msg = "Do you want to Save?";
    }
    if (validateSave()) {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/RecruitmentMatrix/RecruitmentMatrixSave',
                    dataType: 'json',
                    data: $('#RecruitmentMatrixSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('RecruitmentMatrix');
                                var url = "/DataBank/RecruitmentMatrix/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {
                            });
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
}
function Modify(Template) {


    if (validateSave()) {
        $('.Role').prop('disabled', false);
        $('.ResponsiblePerson_Employee').prop('disabled', false);
        $('.ResponsiblePerson_Employee').multiselect('destroy');
        $('.ResponsiblePerson_Employee').multiselect('rebuild');
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/RecruitmentMatrix/RecruitmentMatrixUpdate?Template=' + Template,
                    dataType: 'json',
                    data:$('#RecruitmentMatrixSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('RecruitmentMatrix');
                                var url = "/DataBank/RecruitmentMatrix/Index";
                                window.location.href = url;
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {
                            });
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
}

function OnDeleteClick(RecruitmentMatrixGI) {
    var WorkFlow = $('#WorkFlow_Code').val();

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

                type: 'GET',
                url: '/DataBank/RecruitmentMatrix/RecruitmentMatrixDelete',
                dataType: 'json',
                data: { RecruitmentMatrixGI: RecruitmentMatrixGI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('RecruitmentMatrix');
                            //var url = "/DataBank/RecruitmentMatrix/Index";
                            //window.location.href = url;
                            getList();
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {
                        });
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
function Delete() {

    RecruitmentMatrixGI = $('#RecruitmentMatrixGI').val();
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

                type: 'GET',
                url: '/DataBank/RecruitmentMatrix/RecruitmentMatrixDelete',
                dataType: 'json',
                data: { RecruitmentMatrixGI: RecruitmentMatrixGI },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('RecruitmentMatrix');
                            var url = "/DataBank/RecruitmentMatrix/Index";
                            window.location.href = url;
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {
                        });
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
function DeleteLevel(Level) {

    RecruitmentMatrixGI = $('#RecruitmentMatrixGI').val();
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

                type: 'GET',
                url: '/DataBank/RecruitmentMatrix/RecruitmentMatrixLevelDelete',
                dataType: 'json',
                data: { RecruitmentMatrixGI: RecruitmentMatrixGI, Level: Level },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('RecruitmentMatrix');
                            //var url = "/DataBank/RecruitmentMatrix/Edit?RecruitmentMatrixGI=" + RecruitmentMatrixGI;
                            //window.location.href = url;
                            onEditClick(RecruitmentMatrixGI);
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {
                        });
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

function ExportToExcel() {
    debugger;
    var WorkFlow = $('#WorkFlowList').val();
    var WorkFlowName = $('#WorkFlowList option:selected').text();

    var TableLength = $('#RecruitmentMatrixList_Count').val()
    if (TableLength > 0) {
        url = '/DataBank/RecruitmentMatrix/ExportToExcel?WorkFlowName=' + WorkFlowName + '&WorkFlow=' + WorkFlow + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

//function rolechange(i) {
//    debugger;
//    var tablecount = $('#ApprovalRoleList_Count').val();
//    // Loop through all the roles
//    for (var j = 0; j < tablecount; j++) {

//        // Compare the selected role at index i with the role at index j
//        if ($('#ApprovalRoleList_' + i + '__Level_Code').val() != $('#ApprovalRoleList_' + j + '__Level_Code').val()) {
//            if ($('#ApprovalRoleList_' + i + '__Role_Code').val() == $('#ApprovalRoleList_' + j + '__Role_Code').val()) {
//                swal.fire("Please select distinct roles in different levels");
//                $('#ApprovalRoleList_' + i + '__Role_Code').val('');
//                return false;
//            }
//        }
//        // If they are the same and not the same index, show an error message

//        // Stop the function execution


//    }

//    // If no duplicate roles were found, you can proceed with your logic
//    // Optional, but ensures the function proceeds if no duplicates
//}



function rolechange(i) {
    debugger;
    $('#ApprovalRoleList_' + i + '__EmployeeCode').val('')
    $('#ApprovalRoleList_' + i + '__Role_Code').val(0)
    if ($('#ApprovalRoleList_' + i + '__RoleOrEmp_Code').val() == 1) {
        $('#ApprovalRoleList_' + i + '__Role_Code').prop('disabled', false);
        $('#ApprovalRoleList_' + i + '__EmployeeCode').prop('disabled', true);
        $('#ApprovalRoleList_' + i + '__EmployeeCode').multiselect('destroy');
        $('#ApprovalRoleList_' + i + '__EmployeeCode').multiselect('refresh');
    }
    else if ($('#ApprovalRoleList_' + i + '__RoleOrEmp_Code').val() == 2) {
        $('#ApprovalRoleList_' + i + '__EmployeeCode').prop('disabled', false);
        $('#ApprovalRoleList_' + i + '__Role_Code').prop('disabled', true);
        $('#ApprovalRoleList_' + i + '__EmployeeCode').multiselect('rebuild');


    }
    else
    {
        $('#ApprovalRoleList_' + i + '__EmployeeCode').prop('disabled', true);
        $('#ApprovalRoleList_' + i + '__EmployeeCode').multiselect('destroy');
        $('#ApprovalRoleList_' + i + '__EmployeeCode').multiselect('refresh');
        $('#ApprovalRoleList_' + i + '__Role_Code').prop('disabled', true);
    }
    
}