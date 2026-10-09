function OnEditClick(CatID) {
    debugger;   


    $.ajax({

        type: 'GET',
        url: '/DataBank/GrievanceAuthority/GetDetails',
        dataType: 'html',
        data: { GrievanceCatID: CatID},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#GrievanceEditSlider').html("");
                $('#GrievanceEditSlider').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
        

   
}





function ValidateAuthority()
{
    debugger;
    var i = 0;
    if (!validateMultiselect('EmployeeInfo_EmployeeID', 'authority', 'editAuthority')) {
        i++;
    }
   

    if (i == 0)
    {



        return true;
    }
            else
            {
                return false;
            }
        
            
   
}





function SaveGrievanceAuthority(mode) {
    debugger;
 /*   if (validateEmployee()) {*/
    if (ValidateAuthority()) {
        console.log($('#Grievance_Save').serialize());
        swal.fire({
            /*text: "Do you want to " + mode + "?",*/
            text: "The current authority will transferred to the new authority.Do you want to continue ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/GrievanceAuthority/Save',
                    dataType: 'json',
                    data: $('#Grievance_Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('EditDiv');

                                var url = "/DataBank/GrievanceAuthority/GetList";
                                $('#GrievanceAuthorityList').load(url, function () { });

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
    
    else {
        return false;
    }
}


function ExportToExcel() {
    debugger;
 
    var GrievanceListTable = $('#GrievanceAuthorityList_Count').val();

    if (GrievanceListTable > 0) {
        url = "/DataBank/GrievanceAuthority/ExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}