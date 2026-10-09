var currentTab = 1;
var Tab_Name = 'All';
var prevTab = 'All';

function tabClick(tabName, tabIndex) {
    debugger

    currentTab = tabIndex;
    //if (currentTab == 0)
    //    $("#excelButton").addClass('hide');
    //else
    //    $("#excelButton").removeClass('hide');
    currentTabName = tabName;
    $('#TabCode').val(tabIndex);

    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    ApplyFilter('apply');
    prevTab = tabName;
    showSpinner();
}

function ApplyFilter(mode) {
    debugger


    $("#Tabindex").val(currentTab);

    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            debugger;



            $.ajax({

                type: 'POST',
                url: '/DataBank/LocationMarkingApplicability/GetList?TabIndex=' + currentTab,
                dataType: 'html',
                data: $('#AdvancedFilter').serialize(),
                success: function (result) {
                    debugger

                    $('#AllListDiv').html('');

                    $('#AllListDiv').html(result);

                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');

        }
        else {
            debugger;
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/LocationMarkingApplicability/GetList?TabIndex=' + currentTab,
                dataType: 'html',
                data: $('#AdvancedFilter').serialize(),
                success: function (result) {
                    debugger


                    $('#AllListDiv').html('');

                    $('#AllListDiv').html(result);

                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        debugger
        $('#Advance_Filter_AgeAboveOrBelow').val('');
        $('#Advance_Filter_YSAboveOrBelow').val('');
        $('.reset').val('');
        $('.reset').prop('checked', false);
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');

        showSpinner();
        $.ajax({

            type: 'POST',
            url: '/DataBank/LocationMarkingApplicability/GetList?TabIndex=' + currentTab,
            dataType: 'html',
            data: $('#AdvancedFilter').serialize(),
            success: function (result) {
                debugger

                $('#AllListDiv').html('');

                $('#AllListDiv').html(result);

                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }

}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";
    var tab=$("#Tabindex").val();
    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    if (tab==0)
        table = document.getElementById("AllListTable");
    else
        table = document.getElementById("ApplicableEmpListTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
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
function onSaveClick() {

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
           
            var data = $('#applicableEmpList').serialize();
            $.ajax({

                type: 'POST',
                url: '/Databank/LocationMarkingApplicability/ApplicableEmpSave',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire("Saved Successfully").then(function () {
                            var url = "/DataBank/LocationMarkingApplicability/Index";
                            window.location.href = url;
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

function ExportToExcel() {
    debugger;

    var currentTabIndex = $('#Tabindex').val();

    if (currentTabIndex == 1) {
        var tableLength = $('#ApplicableEmpListTable tbody tr').filter(function () {
            return !$(this).find('td').attr('colspan');
        }).length;

        var filter = $('#AdvancedFilter').serialize();
        var PARAM = 'data=' + filter + '&TabIndex=' + currentTabIndex;
        if (tableLength > 0) {
            url = '/Databank/LocationMarkingApplicability/ExportToExcel?' + PARAM;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }

    }
    else {

        var tableLength = $('#AllListTable tbody tr').filter(function () {
            return !$(this).find('td').attr('colspan');
        }).length;
        var filter = $('#AdvancedFilter').serialize();
        var PARAM = 'data=' + filter + '&TabIndex=' + currentTabIndex;
        if (tableLength > 0) {
            url = '/Databank/LocationMarkingApplicability/ExportToExcel?' + PARAM;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }

    }

}


















