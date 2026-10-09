function onApplicabilityClick()
{
    debugger
    var details = $("#databankLeft :input").serializeArray();

    $.ajax({

        type: 'POST',
        url: '/General/ReportBuilder/fillCombinationGrid',
        dataType: 'html',
        data: details,
        success: function (result) {
   
            $("#databank_combnGrid").html('');
            $("#databank_combnGrid").html(result);

        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

    //$.ajax({

    //    type: 'GET',
    //    url: '/General/ReportBuilder/fillCombinationGrid',
    //    dataType: 'html',
    //    data: details,
    //    success: function (result) {
    //        debugger
    //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //            window.location.reload();
    //        }
    //        else {
    //            $('body').append('<div class="form-overlay"></div>');
    //            $('#databank_combnGrid').html("");
    //            $('#databank_combnGrid').html(result);
    //        }


    //    },


    //    error: function (xhr, textStatus, errorThrown) {
    //        debugger
    //        document.body.id = "kt_body";
    //        $('body').html(xhr.responseText);


    //    }
    //});

}
function Save() {
    debugger

    var params = $("#dataBankReportBuilder").serialize();
   
    $.ajax({

        type: 'POST',
        url: '/General/ReportBuilder/Save',
        dataType: 'json',
        data: params,
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (result) {
            if (result.success == true) {               
                swal.fire(result.message).then(function () {

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

