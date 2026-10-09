function gratutySave() {
    debugger;
    var i = 0;
    if (!validateById('#GratuityEligibleService', 'gratuityeligibleservice') || $('#GratuityEligibleService').val()==0) {
        i++;
    }
    if (!validateById('#GratuityDays', 'minimumassured') || $('#GratuityDays').val() == 0) {
        i++;
    }
    if (!validateById('#NoOfWorkingDays', 'maximumsealingamount') || $('#NoOfWorkingDays').val() == 0) {
        i++;
    }
    

    if (i == 0) {
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
                    url: '/PayRoll/GratuitySettings/GratutySave',
                    dataType: 'json',
                    data: $('#GratutySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                //var url = "/DataBank/IndustryBenchmarkSalary/Index";
                                //onDropdownsChange();
                                fillData();
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
    else
        return false;
}

function checkBoxcChange(val) {
    debugger;
    if ($('#GratuitySettingsList_' + val + '__Applicable').is(':checked')) {
        $('#GratuitySettingsList_' + val + '__Percentage').val(100);
        $('#GratuitySettingsList_' + val + '__Percentage').attr("disabled", false);
    }
    else {
        $('#GratuitySettingsList_' + val + '__Percentage').val(0);
        $('#GratuitySettingsList_' + val + '__Percentage').attr("disabled", true);
    }
       
}
function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("GratuityTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
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
