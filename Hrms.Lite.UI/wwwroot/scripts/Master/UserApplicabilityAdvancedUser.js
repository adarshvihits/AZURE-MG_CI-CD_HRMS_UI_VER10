const { hide } = require("@popperjs/core");

function onClassificationTypeChange(sender, id) {
    debugger;
    GroupingCode = $(sender).val();
    if (GroupingCode == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
    $.getJSON("/Master/UserApplicabilityAdvancedUser/GetClassificationType", { id: GroupingCode }, function (data) {

        $('.Classification option').remove();

        $('.Classification').append(('<option value>--Select--</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('.Classification').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function fillData() {
    debugger;
    var GroupingCode = $("#GroupingCode").val();
    var ClassificationCode = $("#ClassificationCode").val();
    document.getElementById('save').style.display = 'none';
    document.getElementById('checkbox2').style.display = 'none';
    //document.getElementById('checkbox1').style.display = 'none';
    document.getElementById('checkbox3').style.display = 'block';


    $.ajax({

        type: 'GET',
        url: '/Master/UserApplicabilityAdvancedUser/GetList',
        dataType: 'html',
        data: { GroupingCode: GroupingCode, ClassificationCode: ClassificationCode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#UserApplicabilityAdvancedUser').html("");
                $('#UserApplicabilityAdvancedUser').html(result);
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





function OnClick(UserCode, Mode) {

    debugger;
    var GroupingCode = $("#GroupingCode").val();
    var ClassificationCode = $("#ClassificationCode").val();

    swal.fire({
        text: "Do you want to " + Mode + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/UserApplicabilityAdvancedUser/Save',
                dataType: 'json',
                data: { GroupingCode: GroupingCode, ClassificationCode: ClassificationCode, UserCode: UserCode, Mode: Mode },
                success: function (result) {
                    if (result.success == true) {
                        debugger;
                        swal.fire(result.message).then(function () {
                            debugger;
                            fillData();


                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            fillData();

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

//function validatedropdown() {
//    var i = 0;
//    var reviewcode = $('#Reviewtype').val();
//    if (!validateById('#GroupingCode', 'classificationtype')) {

//        i++;
//    }
//    if (!validateById('#ClassificationCode', 'classification')) {

//        i++;
//    }
//    if (i == 0)
//        return true;
//    else
//        return false;
//}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("UserApplicabilityAdvancedUser");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];

        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");

            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }

    if (noOfRows == 1) {

        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");

    }
}



function onAllClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function checkBoxClick(sender, TableId, checkId) {
    debugger;
    if ($(sender).is(':checked')) {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length - 1) {

            $('#' + checkId).prop('checked', true)
        }

        //else if ($('#' + TableId).find('input[type="checkbox"]:checked').length-1 == $('#' + TableId).find('input[type="checkbox"]').length-1)
        //{
        //    $('#' + checkId).prop('checked', true)
        //}
        else
            $('#' + checkId).prop('checked', false)
    }
    else {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length) {

            $('#' + checkId).prop('checked', true)
        }
        else
            $('#' + checkId).prop('checked', false)
    }
}

function SaveBatchClick() {
    debugger;
    var GroupingCode = $("#GroupingCode").val();
    var ClassificationCode = $("#ClassificationCode").val();

    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var formData = $("#UserApplicabilityAdvancedUserform").serialize();

            $.ajax({

                type: 'POST',
                url: '/Master/UserApplicabilityAdvancedUser/BatchSave',
                dataType: 'json',
                data: formData,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            fillData();

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

function BatchUpdateClick() {
    debugger;
    var GroupingCode = $("#GroupingCode").val();
    var ClassificationCode = $("#ClassificationCode").val();

    var elements = document.getElementsByClassName('AddOrRemove');
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.display = 'none';
    }

    if (GroupingCode && ClassificationCode) {
        document.getElementById('save').style.display = 'block';
        document.getElementById('checkbox2').style.display = 'block';


    } else {
        document.getElementById('save').style.display = 'none';

    }


}