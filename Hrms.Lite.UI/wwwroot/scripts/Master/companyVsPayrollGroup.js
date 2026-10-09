function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
   
    table = document.getElementById("CompanyVsPayrollGroupListTable");
   
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
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
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}






function fillData() {
    debugger;
    var GroupCompanyCode = $("#Code").val();
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/Master/CompanyVsPayrollGroup/GetList',
        dataType: 'html',
        data: { GroupCompanyCode: GroupCompanyCode },
        success: function (result) {
            showSpinner();
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#CompanyVsPayrollGroupList').html("");
                $('#CompanyVsPayrollGroupList').html(result);
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
    hideSpinner();
}


function onSaveApplicabilityClick() {
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
                url: '/Master/CompanyVsPayrollGroup/Save',
                dataType: 'json',
                data: $('#CompanyVsPayrollGroup').serialize(),
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

