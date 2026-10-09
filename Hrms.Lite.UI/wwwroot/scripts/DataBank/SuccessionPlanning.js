var prevTab = 'ActiveTab';
var currentTab = 'ALL';

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {
        if (
            $('#dsg').find('input[type="checkbox"]:checked').length == $('#dsg').find('input[type="checkbox"]').length &&
            $('#dept').find('input[type="checkbox"]:checked').length == $('#dept').find('input[type="checkbox"]').length &&
            $('#loc').find('input[type="checkbox"]:checked').length == $('#loc').find('input[type="checkbox"]').length &&
            $('#cat').find('input[type="checkbox"]:checked').length == $('#cat').find('input[type="checkbox"]').length &&
            $('#grd').find('input[type="checkbox"]:checked').length == $('#grd').find('input[type="checkbox"]').length &&
            $('#pay').find('input[type="checkbox"]:checked').length == $('#pay').find('input[type="checkbox"]').length &&
            $('#div').find('input[type="checkbox"]:checked').length == $('#div').find('input[type="checkbox"]').length &&
            $('#sec').find('input[type="checkbox"]:checked').length == $('#sec').find('input[type="checkbox"]').length &&
            $('#empType').find('input[type="checkbox"]:checked').length == $('#empType').find('input[type="checkbox"]').length) {
            debugger;
            $('#SelectAll').prop('checked', true);
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
        }
        else {
            debugger;

            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
            if ($('#allDsg').is(':checked') && $('#allDept').is(':checked') &&
                $('#allLoc').is(':checked') && $('#allCat').is(':checked') &&
                $('#allGrd').is(':checked') && $('#allPay').is(':checked') &&
                $('#allDiv').is(':checked') && $('#allSec').is(':checked') &&
                $('#allEmpType').is(':checked')) {
                $('#SelectAll').prop('checked', true);
            }
            else {
                $('#SelectAll').prop('checked', false);
            }
        }
    }
    else {
        debugger;
        $('#SelectAll').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}


function EmployeeSearch() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("EmployeeTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
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



function OnTabClick(Tab, TabIndex) {
    debugger;
    /*showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/SuccessionPlanning/GetSuccessionPlanningList?TabIndex=" + currentTab;
    //$('#SuccessionPlanningListID').load(url, function () { });
   ApplyFilter('apply');
    prevTab = Tab;
}


function OnKeyPositionClick(KeyPositionGI, EmployeeGI) {
    debugger
    var url = "/DataBank/SuccessionPlanning/Successionplanning?KeyPositionGI=" + KeyPositionGI + "&EmployeeGI=" + EmployeeGI;
    window.location.href = url;
}

function onAddNewSuccessorClick(KeyPositionGI, EmployeeGI)
{
    debugger

    $.ajax({

        type: 'GET',
        url: '/DataBank/SuccessionPlanning/AddNewSuccessor',
        dataType: 'html',
        data: { KeyPositionGI: KeyPositionGI, EmployeeGI: EmployeeGI },
        success: function (result) {
       
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
              
            }
            else {
                debugger;
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewSlider').html("");
                $('#AddNewSlider').html(result);
                SelectEmployees(1, KeyPositionGI, EmployeeGI);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    
}


function fillData(EmployeesType, KeyPositionGI, EmployeeGI)
{
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;



    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#EmployeeTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/SuccessionPlanning/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();




                 
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&EmployeesType=' + EmployeesType+ '&KeyPositionGI=' + KeyPositionGI + '&EmployeeGI=' + EmployeeGI+ '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



               
        

            
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger;

                    if (d.data[i].matching > 75)
                    {
                        d.data[i].matching = '<td data-label="Matching"><div class="view-details"><div class="profile-matching-line"><div class="progress-bar"><div class="high" data-bs-toggle="tooltip" title="data-bs-original-title="' + d.data[i].matching + '">' + d.data[i].matching +'</div></div></div></div></td>'

                    }

                    if (d.data[i].matching >= 50) {

                        d.data[i].matching = '<td data-label="Matching"><div class="view-details"><div class="profile-matching-line"><div class="progress-bar"><div class="medium" data-bs-toggle="tooltip" title="data-bs-original-title="' + d.data[i].matching + '">' + d.data[i].matching +'</div></div></div></div></td>'

                    }

                    if (d.data[i].matching < 50) {
                        d.data[i].matching = '<td data-label="Matching"><div class="view-details"><div class="profile-matching-line"><div class="progress-bar"><div class="low" data-bs-toggle="tooltip" title="data-bs-original-title="' + d.data[i].matching + '">' + d.data[i].matching +'</div></div></div></div></td>'

                    }

                    d.data[i].applicable = '<td data-label="Count"><div class="checkbox-inline justify-content-center"><label class="checkbox checkbox-outline checkbox-outline-1x checkbox-primary text-muted"><input type="checkbox" id="accept" name="accept" value="yes"><span></span></label></div></td>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID","width":"15%" },
            { "data": "employeeName","width":"45%" },
            { "data": "matching","width":"22%" },
          { "data": "applicable","width":"18%" },
          

        ],






        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {

            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            sLengthMenu: "_MENU_",
            paginate: {


                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'




            }
        }



    });



    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();

    });

    $('.search-slider-inner').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });

    $.fn.DataTable.ext.pager.numbers_length = 5;


}







function SelectEmployees(EmployeesType, KeyPositionGI, EmployeeGI) {
    debugger
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/DataBank/SuccessionPlanning/GetEmployeeList',
        dataType: 'html',
        data: { EmployeesType: EmployeesType, KeyPositionGI: KeyPositionGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {               
                $('#EmployeeListID').html("");
                $('#EmployeeListID').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    
}


function ValidateSuccessors() {
    debugger;
    var checked_checkboxes = $("#EmployeeTable input[type=checkbox]:checked");
    if (checked_checkboxes.length == 0) {
        swal.fire("Select atleast one successor");
       /* $(".error").show();*/
        return false;
    }
    return true;
}



function OnProceedClick(KeyPositionGI, EmployeeGI) {
    debugger
    var markedCheckbox = document.getElementsByName('accept');
    for (var checkbox of markedCheckbox) {
        if (checkbox.checked)
            document.body.append(checkbox.value + ' ');
    }

    if (ValidateSuccessors()) {
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
                    url: '/DataBank/SuccessionPlanning/Save',
                    dataType: 'json',
                    data: $('#Successor_Save').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                var url = "/DataBank/SuccessionPlanning/Successionplanning?KeyPositionGI=" + KeyPositionGI + "&EmployeeGI=" + EmployeeGI;
                                window.location.href = url;


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

           
    
    })

}

    else {
        return false;
    }
}





function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);


    
}

function DeleteSuccessor(SuccessionPlanningGI, EmpGI, KeyPositionGI) {
    debugger;
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {          
            const data = { SuccessionPlanningGI: SuccessionPlanningGI };
            console.log(data);
            $.ajax({
                type: 'POST',
                url: '/DataBank/SuccessionPlanning/Delete?SuccessionPlanningGI=' + SuccessionPlanningGI,
                dataType: 'json',               
              
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/SuccessionPlanning/Successionplanning?KeyPositionGI=" + KeyPositionGI + "&EmployeeGI=" + EmpGI;
                            window.location.href = url;
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
function ExportToExcel() {
    debugger
    if (currentTab == 'ALL')
    {
        var leaveOpeningTableLength = $('#SuccessionPlanningList_Count').val();
        if (leaveOpeningTableLength > 0)
        {
            url = "/DataBank/SuccessionPlanning/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else
        {
            swal.fire("No data found to export");
        }
    }
    else
    {
        var leaveOpeningTableLength = $('#SuccessionPlanningList_Count').val();
        if (leaveOpeningTableLength > 0)
        {
            url = "/DataBank/SuccessionPlanning/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }       
        else
        {
            swal.fire("No data found to export");
        }
    }
}





//function ApplyFilter(mode) {
//    debugger
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/SuccessionPlanning/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        $('#SuccessionPlanningListID').html("");
//                        $('#SuccessionPlanningListID').html(result);
//                    }
//                    hideSpinner();
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }

//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/SuccessionPlanning/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');

//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        $('#SuccessionPlanningListID').html("");
//                        $('#SuccessionPlanningListID').html(result);
//                    }
//                    hideSpinner();
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }

//            });
//            $('#filterForm').addClass('filter-apply');
//        }
//    }
//    else {
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $('#filterForm').removeClass('filter-apply');
//        $.ajax({

//            type: 'POST',
//            url: '/DataBank/SuccessionPlanning/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize(),
//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('#SuccessionPlanningListID').html("");
//                    $('#SuccessionPlanningListID').html(result);
//                }
//                hideSpinner();
//            },


//            error: function (xhr, textStatus, errorThrown) {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });

//    }
//}









function ApplyFilter(mode) {
    debugger
    
    console.log($('#BasicFilter').serialize());
    $("#Tabindex").val(currentTab);
    var tabIndex = $('#Tabindex').val();
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/SuccessionPlanning/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&TabIndex=" + currentTab ,
                success: function (result) {
                    debugger;
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        if (tabIndex != 1001) {
                            debugger;
                            $('#SuccessionPlanningListID').html("");
                            $('#SuccessionPlanningListID').html(result);

                            $('#SuccessionPlanningListID').removeClass('hide');
                            $('#pending').addClass('hide');
                            $(".filterList").show();
                           


                        }
                        else {
                            debugger;
                            $(".filterList").hide();
                            $('#pending').html("");
                            $('#pending').html(result);
                            $('#pending').removeClass('hide');
                            $('#SuccessionPlanningListID').addClass('hide');
                           
                        }
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/SuccessionPlanning/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        if (tabIndex != 1001) {

                            $('#SuccessionPlanningListID').html("");
                            $('#SuccessionPlanningListID').html(result);

                            $('#SuccessionPlanningListID').removeClass('hide');
                            $('#pending').addClass('hide');
                            $(".filterList").show();            


                        }
                        else {
                            $(".filterList").hide();
                            $('#pending').html("");
                            $('#pending').html(result);
                            $('#pending').removeClass('hide');
                            $('#SuccessionPlanningListID').addClass('hide');
                            
                        }
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/SuccessionPlanning/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    if (tabIndex != 1001) {

                        $('#SuccessionPlanning').html("");
                        $('#SuccessionPlanning').html(result);

                        $('#SuccessionPlanning').removeClass('hide');
                        $('#pending').addClass('hide');
                    }
                    else {
                        $('#pending').html("");
                        $('#pending').html(result);
                        $('#pending').removeClass('hide');
                        $('#SuccessionPlanning').addClass('hide');
                        
                    }
                }
                hideSpinner();
            },

            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function ViewEmployee(EmpGI) {
    debugger;
    var FinancialYear = null;
    url = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=" + EmpGI + "&FinancialYear=" + FinancialYear + "&Breadcrumb=" + 'SuccessionPlanning' + "&BreadcrumbName=" +'Succession Planning List';
    window.location.href = url;
}