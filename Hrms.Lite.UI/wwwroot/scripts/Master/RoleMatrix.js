var currentRoleCode = 0;
var ParameterCode_=0



function fillData(sender)
{
    
    debugger
    var Role = $('#Role_Code').val();
    currentRoleCode = Role;
    $.ajax({

        type: 'GET',
        url: '/Master/RoleMatrix/GetList',
        dataType: 'html',
        data: { RoleCode: Role },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#RoleMatrixList').html("");
                $('#RoleMatrixList').html(result);
               
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


function AddNewOrEditClick(ParameterCode, ParameterName) {
    debugger;
    var Role = $('#Role_Code option:selected').text();
    $('#AddAuthority').text(Role + "-"+ ParameterName);
    ParameterCode_ = ParameterCode;
    $('#CreateOrEditSlider').removeClass("hide");
    //var url = "/Master/RoleMatrix/AddAuthority?ParameterCode=" + ParameterCode;
    //$('#CreateOrEditSlider').load(url, function () { });
}


function CloseCard_RoleMatrix(id) {
    debugger;
    $('.search-profile-details').hide(300);
    $('.filter-option-inner-inner').html("Search by Employee name/ID");
    $('.dropdown-toggle').prop('title', 'Search by Employee name/ID');
    $('.sliderSearchInput').val(0);
    CloseSlider(id)
    var url = "/Master/RoleMatrix/GetList?RoleCode=" + currentRoleCode;
    $('#RoleMatrixList').load(url, function () { });
}

function OnEmployeeChange(sender) {
    debugger;
    var EmpCode = $(sender).val();
    if (EmpCode != 0) {

        $.ajax({

            type: 'GET',
            url: '/Master/RoleMatrix/EmployeeCard',
            dataType: 'html',
            data: { EmpCode: EmpCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                   
                }
                else {
                    $('.search-profile-details').html("");
                    $('.search-profile-details').html(result);
                  
                }
                

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('.search-profile-details').show(300);

    }
}
function SaveAuth(Position) {
    debugger;
    var TabIndex;
    var Role = $('#Role_Code').val();
    var ParameterCode = $('#ParameterCode').val();

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
                    url: '/Master/RoleMatrix/SaveAuth',
                    dataType: 'json',
                    data: { Position: Position, RoleCode: currentRoleCode, ParameterCode: ParameterCode_ } ,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseCard_RoleMatrix('rolemat');
                                $('#CreateOrEditSlider').addClass("hide");
                                var url = "/Master/RoleMatrix/GetList?RoleCode=" + currentRoleCode;
                                    $('#RoleMatrixList').load(url, function () { });
                                //window.location.reload();
                                //fillData();

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                CloseCard_RoleMatrix('rolemat');
                                $('#CreateOrEditSlider').addClass("hide");
                                var url = "/Master/RoleMatrix/GetList?RoleCode=" + currentRoleCode;
                                   $('#RoleMatrixList').load(url, function () { });
                                //window.location.reload();
                                //fillData();
                                

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
function ExportToExcel() {
    debugger;
    var Role = $('#Role_Code').val();
    var RoleName = $('#Role_Code option:selected').text();

    var TableLength = $('#RoleMatrixList_Count').val()
    if (TableLength > 0) {
        url = '/Master/RoleMatrix/ExportToExcel?RoleName=' + RoleName + '&RoleCode=' + currentRoleCode + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}