
var prevTab = 'tab1';
var currentTab = 'ALLOWANCE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
 

    $.ajax({

        type: 'GET',
        url: '/Master/WPSAccountHead/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#allowdedaccountheadList').html("");
                $('#allowdedaccountheadList').html(result);
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

    prevTab = Tab;
}


function onEditClick(i) {
    debugger;
    $('#amtLabel2_' + i).addClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
}

function onCancelClick(i) {
    debugger;
    $('#amtLabel2_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}


function onUpdateClick(i) {

    $('#amtLabel2_' + i).html($('#WPSAccountHeadTablist_' + i + '__WPSHeader option:selected').text());
    $('#amtLabel2_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}

function SaveAllowanceDeductionAccountHead() {
    var table = $('#AllowDedtable').DataTable();
    var data = table.$('input,select,textarea').serializeArray();

    console.log(data);
    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/Master/WPSAccountHead/Save?TabName=' + $('#TabName').val(),
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            OnTabClick(prevTab, currentTab);
                            $(".form-overlay").remove();

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
            $(".form-overlay").remove();
            return false;
        }
    })
}

function ExportToExcel() {

    debugger;
    var tableLength = $('#WPSAccountHeadTablist_Count').val();
    if (tableLength > 0) {
          url = "/Master/WPSAccountHead/ExportToExcel?TabIndex=" + currentTab;
          window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}