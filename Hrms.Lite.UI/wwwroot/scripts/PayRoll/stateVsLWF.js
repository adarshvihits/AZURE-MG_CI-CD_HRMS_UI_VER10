

function GetList() {
    debugger;
    var DeductionCode = $('#DeductionCode').val();
    $.ajax({

        type: 'POST',
        url: '/PayRoll/StateVsLWF/GetList',
        dataType: 'html',
        data: { DeductionCode: DeductionCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#listLWF').html("");
                $('#listLWF').html(result);
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


function GetPopUp(statecode, state, DeductionCode) {

    debugger;

  


    $.ajax({
        type: 'GET',
        url: "/PayRoll/StateVsLWF/GetPopUp?StateCode=" + statecode + "&state=" + state + "&DeductionCode=" + DeductionCode,
        dataType: 'html',

        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            $('#exampleModalLabel').html(state);
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });


}


function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}



function OnSaveClick() {
    debugger
    var DeductionCode = $('#DeductionCode').val();
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
                url: '/PayRoll/StateVsLWF/PopUpSave',
                dataType: 'json',
                data: $('#Contribution_PopUp').serialize() + "&DeductionCode=" + DeductionCode,
                success: function (result) {
                    debugger
                    if (result.success == true) {

                        swal.fire(result.message).then(function () {


                            $('.bd-example-modal-lg').modal('hide');
                        
                         
                            var url = "/PayRoll/StateVsLWF/GetList?DeductionCode=" + DeductionCode;
                            $('#listLWF').load(url, function () { });
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



function ExportToExcel() {
    debugger;


    var DeductionCode = $('#DeductionCode').val();
    var  Deduction= $('#DeductionCode option:selected').text();
   


    var TableLength = $('#LWFSettingsList_Count').val();
   
        if (TableLength > 0) {

            url = "/PayRoll/StateVsLWF/ExportToExcel?Deduction=" + Deduction + '&DeductionCode=' + DeductionCode;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }

    


}