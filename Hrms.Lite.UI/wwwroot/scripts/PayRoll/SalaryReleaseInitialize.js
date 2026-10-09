function GetList() {
    debugger;
    showSpinner();

    var Month = $('#Month').val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/SalaryReleaseInitialize/GetList?Month=' + Month,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
    
                $('#SalaryReleaseInitializeList').html("");
                $('#SalaryReleaseInitializeList').html(result);
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
function SalaryReleaseInitializeSaveClick() {
    debugger;


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
                url: '/PayRoll/SalaryReleaseInitialize/Save',
                dataType: 'json',
                data: $('#SalaryReleaseInitializeSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            url = "/PayRoll/SalaryReleaseInitialize/Index";
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
function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {

        $('#SRInitializeTable').find('input[type=checkbox]').prop('checked', true);

    }
    else {

        $('#SRInitializeTable').find('input[type=checkbox]').prop('checked', false);

    }


    var totalRows = $('#SRInitializeTable tbody tr').length;
    for (let i = 0; i < totalRows; i++) {
        onCheckboxChange(document.getElementById('SalaryReleaseInitializeList' + i + '__Applicability'), i)
    }
}
function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchInitializeList");
    filter = input.value.toUpperCase();
    table = document.getElementById("SRInitializeTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;

    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];

        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }

        }
    }

    if (noOfRows == 1) {

        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");

    }
}