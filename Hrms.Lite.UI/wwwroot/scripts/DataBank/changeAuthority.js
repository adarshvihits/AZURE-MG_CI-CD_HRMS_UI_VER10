
function GetList() {
    debugger
    showSpinner();
    var filter1 = $('#BasicFilter').serialize();
    var workflowCode = $('#Workflow_Code').val();
    $.ajax({
        type: 'GET',
        url: '/DataBank/ChangeAuthority/GetList',
        dataType: 'html',
        data: $('#BasicFilter').serialize() + "&WorkflowCode=" + workflowCode,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#EmpList').html("");
                $('#EmpList').html(result);
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
function onEmployeeClick(TrnCode, EmployeeGI) {
    debugger;
    showSpinner();
    var workflowCode = $('#Workflow_Code').val();
    $.ajax({
        type: 'GET',
        url: "/DataBank/ChangeAuthority/GetAuthorityPopup?TrnCode= " + TrnCode + "&EmployeeGI=" + EmployeeGI + "&WorkflowCode=" +workflowCode ,
        dataType: 'html',
        success: function (Data) {
            hideSpinner();
            var m = $('#Auth_Popup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            hideSpinner();
            swal.fire(xhr.responseText);
        }
    });
}

function onUpdateClick(TrnCode, LevelCode,HDCODE,i) {
    debugger;
    var AuthorityCode = $('#LevelAuthorityList_'+i+'__Authority_Code').val();
    var workflowCode = $('#Workflow_Code').val();
    swal.fire({
        text: "Do you want to Update?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({
                type: 'GET',
                url: '/DataBank/ChangeAuthority/AuthorityUpdate',
                dataType: 'json',
                data: { TrnCode: TrnCode, LevelCode: LevelCode, AuthorityCode: AuthorityCode, HDCode: HDCODE, WorkflowCode:workflowCode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var m = $('#Auth_Popup');
                            m.modal('hide');
                            hideSpinner();
                            GetList();
                        });
                    }
                    else {
                        hideSpinner();
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

function ApplyFilter(mode) {
    debugger;
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            GetList();
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