
//function SaveAmount(index) {
//    debugger;
//    alert($("#TrainingCostAllotedForm").serialize());
//    var Amount = $('#.TraineeCostList_' + index + '__AllotedAmount').val();
    

//    $.ajax({
//        url: "/TrainingModule/TrainingCostAlloted/Save",
//        type: "POST",
//        dataType: 'json',

//        data: $("#TrainingCostAllotedForm").serialize(),
//        success: function (response) {
//            alert(response.message);
//            window.location.href = '/TrainingModule/TrainingCostAlloted/Index';
//        },
//        error: function (request, status, error) {
//            alert(request.responseText);
//        }
//    });
    
//}








function SaveAmount() {
    debugger;

    var Count = $('tr[id^="row_"]').length;
    var isValid = true;

    if (Count > 0) {
        for (var i = 0; i < Count; i++) {
            var amount = parseFloat($('#TraineeCostList_' + i + '__AllotedAmount').val());

            if (isNaN(amount) || amount > 10000000) {
                isValid = false;
                swal.fire({
                   
                    text: 'Allotted Amount should be less than or equal to 10,000000.'
                   
                }).then(() => {
                    $('#TraineeCostList_' + i + '__AllotedAmount').focus();
                });
                return false; // Exit the loop and prevent form submission
            }
        }
    }

    if (isValid) {
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
                    url: '/TrainingModule/TrainingCostAlloted/Save',
                    dataType: 'json',
                    data: $('#TrainingCostAllotedForm').serialize(),
                    success: function (result) {
                        swal.fire(result.message).then(function () {
                            if (result.success) {
                                window.location.href = '/TrainingModule/TrainingCostAlloted/Index';
                            }
                        });
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger;
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        } else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            } else {
                return false;
            }
        });
    }
}







