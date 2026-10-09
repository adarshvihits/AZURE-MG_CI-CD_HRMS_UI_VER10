
function ScheduleList() {
    showSpinner();
    debugger
    var status = $('#TrnStatus').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/TraineeCost/GetList',
        dataType: 'html',
        data: filter + "&ScheduleCode=" + status,
        success: function (result) {
            debugger
            $('#Listview').html('');
            $('#Listview').html(result);
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
            hideSpinner();
        }
    });
}


function onEmployeeClick(EmployeeGI) {
    debugger;

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TraineeCost/GetCostDetails',
        dataType: 'html',
        data: { empGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EditLoanIssueSlider').html("");
                $('#EditLoanIssueSlider').html(result);

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

function Save() {
    debugger;
    var status = $('#TrnStatus').val();
 
    var model = $("#SaveList").serialize();
    console.log($("#SaveList").serialize());
    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger


            $.ajax({
                type: 'POST',
                url: '/TrainingModule/TraineeCost/Save',
                dataType: 'json',
                data: $("#SaveList").serialize() + "&ScheduleCode=" + status,
                success: function (result) {

                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            debugger
                            $('.bd-example-modal-lg').modal('hide')
                            ScheduleList();



                            CloseSlider('EditLoanIssueSlider');



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

function Batchupdate() {
    debugger
    var status = $('#TrnStatus').val();
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TraineeCost/Batchupdation',
        dataType: 'html',
        data: { ScheduleCode: status },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#updateSlider').html("");
                $('#updateSlider').html(result);

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


function BatchSave() {
    debugger;
    var status = $('#TrnStatus').val();

    var model = $("#BatchList").serialize();
   
    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger


            $.ajax({
                type: 'POST',
                url: '/TrainingModule/TraineeCost/BatchSave',
                dataType: 'json',
                data: model + "&ScheduleCode=" + status,
                success: function (result) {

                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            debugger
                            $('.bd-example-modal-lg').modal('hide')
                            ScheduleList();



                            CloseSlider('IssueSlider');



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

//function TotalCost() {
//    var total = 0;

//    // Loop through each row with class "cost-row" and find the CostHead value
//    $("#CostDetails").each(function () {
//        var costHead = parseFloat($(this).find(".cost-head").val());
//        console.log(costHead);
//        total += costHead; // Add to total
//    });

//    // Display the total
//    $("#totalCost").text(total);
//}


