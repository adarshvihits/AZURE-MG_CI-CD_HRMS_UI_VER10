
var currentTab = 1;
var Tab_Name = 'All';
var prevTab = 'All';
var pagenumber = 1;

function tabClick(tabName, tabIndex) {
    debugger
    
    currentTab = tabIndex;
    if (currentTab == 0)
        $("#excelButton").addClass('hide');
    else
        $("#excelButton").removeClass('hide');
    currentTabName = tabName;
    $('#TabCode').val(tabIndex);
    $('#tableAllList tbody tr').remove();

    $("." + prevTab).removeClass("active");
    $("." + tabName).addClass("active");
    ApplyFilter('apply');
    prevTab = tabName;
    showSpinner();
}


//function fillData() {
//    debugger
 
//    var filter1 = $('#AdvancedFilter').serialize();

//    var temp = filter1;


//    if (currentTab == 0) {

//        $("#AllListDiv").removeClass('hide');
//        $("#CoreTeamDiv").addClass('hide');
//        $("#excelButton").addClass('hide');
//        var dataTable = $("#AllListTable").DataTable({
//            //"paging": true,
//            "destroy": true,
//            "pagingType": 'full_numbers',
//            "ajax": {
//                "url": "/DataBank/CoreTeam/GetList",
//                "type": "POST",


//                "datatype": "json",


//                'data':

//                    function (d) {
//                        temp = '';

//                        var searchValue = $('#dataTableSearch').val();

//                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + currentTab + '&dummy=dummy';

//                        d.filter = temp;
//                        d._model = $('#coreEmpList').serialize();
//                        return JSON.stringify(d);
//                    },

               


//                "dataSrc": function (d) {
//                    //$('#AllList_Count').val(d.totalCount);

//                    debugger;
//                    $('.AllCount').html(d.allCount);
//                    $('.CoreTeamCount').html(d.coreTeamCount);


//                    //if (d.data == "") {
//                    //    $('.AllCount').html(0);
//                    //    $('.CoreTeamCount').html(0);
//                    //}
//                    //else {
//                    //    $('.AllCount').html(d.data[0].all);
//                    //    $('.CoreTeamCount').html(d.data[0].coreTeam);
//                    //}
//                    // console.log(d.recordsTotal);

//                    $('#TeamList_Count').val(d.totalCount);

//                    for (var i = 0, ien = d.data.length; i < ien; i++) {
//                        d.data[i].employeeID = d.data[i].employeeID + '<input type="hidden" id="TeamList_' + i + '__EmployeeCode' + '" name="TeamList_[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '">'

//                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
//                        if (d.data[i].applicable != false) {
//                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input id="TeamList_' + i + '__Applicable" name="TeamList[' + i + '].Applicable" type="checkbox" class="CoreTeam_Applicable" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
//                        }
//                        else
//                            d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="TeamList_' + i + '__Applicable" name="TeamList[' + i + '].Applicable" type="checkbox" class="CoreTeam_Applicable" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'


//                    }
//                    console.log(d.data);
//                   /* const myTimeout = setTimeout(ChkBxChecking, 100);*/
//                    return d.data;
//                }

//            },



//            "columns": [

//                { "data": "employeeID", "width": "16%" },
//                { "data": "employeeName", "width": "16%" },
//                { "data": "designation", "width": "11%" },
//                { "data": "department", "width": "14%" },
//                { "data": "location", "width": "10%" },
//                { "data": "category", "width": "10%" },
//                { "data": "doj", "width": "9%" },
//                { "data": "yos", "width": "8%" },
//                { "data": "applicable", "width": "6%" }

//            ],






//            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",

//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {

//                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//                //   emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                sLengthMenu: "_MENU_",
//                paginate: {


//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'




//                }
//            }



//        });
//        //$('#CoreTeamListTable').on('page.dt', function () {
//        //    debugger
//        //    var info = dataTable.page.info();
//        //    pagenumber = info.page + 1;

//        //});

//    }
//    else {

//        $("#AllListDiv").addClass('hide');
//        $("#CoreTeamDiv").removeClass('hide');
//        $("#excelButton").removeClass('hide');

//        var dataTable = $("#CoreTeamListTable").DataTable({
//            //"paging": true,
//            "destroy": true,
//            "pagingType": 'full_numbers',
//            "ajax": {
//                "url": "/DataBank/CoreTeam/GetList",
//                "type": "POST",


//                "datatype": "json",


//                'data':

//                    function (d) {
//                        temp = '';

//                        var searchValue = $('#dataTableSearch').val();

//                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + currentTab + '&dummy=dummy';

//                        d.filter = temp;
//                        //d._model = $('#coreEmpList').serialize();

//                        // alert(d.filter);

//                        return JSON.stringify(d);
//                    },





//                "dataSrc": function (d) {
//                    $('#CoreTeamList_Count').val(d.totalCount);

//                    debugger;
//                    $('.AllCount').html(d.allCount);
//                    $('.CoreTeamCount').html(d.coreTeamCount);
//                    //if (d.data == "") {
//                    //    $('.AllCount').html(0);
//                    //    $('.CoreTeamCount').html(0);
//                    //}
//                    //else {
//                    //    $('.AllCount').html(d.data[0].all);
//                    //    $('.CoreTeamCount').html(d.data[0].coreTeam);
//                    //}
//                    // console.log(d.recordsTotal);

//                    $('#CoreTeamList_Count').val(d.recordsTotal);

//                    for (var i = 0, ien = d.data.length; i < ien; i++) {

//                        debugger;
//                        d.data[i].employeeID = d.data[i].employeeID + '<input type="hidden" id="TeamList_' + i + '__EmployeeCode' + '" name="TeamList_[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '">'

//                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                       
//                    }

//                    return d.data;
//                    console.log(d.data);
//                }

//            },



//            "columns": [


//                { "data": "employeeID", "width": "16%" },
//                { "data": "employeeName", "width": "16%" },
//                { "data": "designation", "width": "11%" },
//                { "data": "department", "width": "14%" },
//                { "data": "location", "width": "10%" },
//                { "data": "category", "width": "10%" },
//                { "data": "doj", "width": "9%" },
//                { "data": "yos", "width": "8%" },


//            ],


//            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",

//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {

//                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//                //   emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                sLengthMenu: "_MENU_",
//                paginate: {


//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'



//                }
//            }



//        });
       
//    }


//    $('.ihits-table table').removeClass("dataTable no-footer");


//    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.next').attr("title", "Next");
//    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.previous').attr("title", "Previous");
//    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.first').attr("title", "First");
//    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.last').attr("title", "Last");


//    $("#dataTableSearch").on("keyup", function () {

//        dataTable.search(this.value).draw();
//        $('#noDataFound').css("display", 'none');
//    });

//    $('.search-slider-inner').click(function () {
//        dataTable.search('').draw();
//        $('#dataTableSearch').val('');
//        $('#dataTableSearch').focus();
//    });

//    dataTable.on('draw', function () {
//        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.next').attr("title", "Next");
//        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.previous').attr("title", "Previous");
//        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.first').attr("title", "First");
//        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.last').attr("title", "Last");

//    });

//    $.fn.DataTable.ext.pager.numbers_length = 5;


//    //$.getJSON("/DataBank/EmployeeProfile/GetTabCount", {}, function (data) {


//    //    console.log(data);
//    //    if (data.length != 0) {

//    //        $('.ActiveCount').html(data.activeCount);
//    //        $('.ResignedCount').html(data.resignedCount);

//    //    }
//    //    else {

//    //        $('.ActiveCount').html(0);
//    //    }


//    //});
//}

function onCheckboxChange(sender, tableId) {

    debugger;
    if ($(sender).is(':checked')) {

        debugger;
       
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;

        $('#FullPermission').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    //checked = $(sender).is(':checked');

    //if (checked) {
    //    var row = $(sender).closest('tr').html();
    //    var empcode = $('#TeamList_' + index + '__EmployeeCode').val();

    //    $('#tableAllList tbody').append('<tr id="tableCoreteamSave_' + pagenumber + '_' + index + '">' + row + '</tr>');

    //    $('#TeamList_' + index + '__EmployeeCode').val(empcode);
    //    $('#TeamList_' + index + '__Applicable').attr('checked', true);

    //    var checkedCount = $('#AllListTable').find('input[type=checkbox]:checked').length;
    //    var totalRows = $('#AllListTable tbody tr').length;

        

    //}
    //else {

    //    $('#tableCoreteamSave_' + pagenumber + '_' + index).remove();

    //    //$('.selectAllChkBx').html('')
    //    //$('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

    //}

    //$('#tableAllList tbody').find("tr").each(function (currindex) {
    //    debugger;
    //     //if (currindex > 0) {
    //    $(this).find("td").eq(2).html(currindex);
    //    debugger
    //    var tdd = $(this).find("td").eq(2);
    //    tdd.prevObject[0].getElementsByTagName('input')[0].id = 'TeamList_' + currindex + '__EmployeeCode';
    //    tdd.prevObject[0].getElementsByTagName('input')[0].name = 'TeamList[' + currindex + '].EmployeeCode';
    //    //tdd.prevObject[1].getElementsByTagName('input')[1].id = 'TeamList_' + currindex + '__LeaveApplicationGI';
    //    //tdd.prevObject[1].getElementsByTagName('input')[1].name = 'TeamList[' + currindex + '].LeaveApplicationGI';
    //    //tdd.prevObject[1].getElementsByTagName('input')[2].id = 'TeamList_' + currindex + '__AttendanceStatusTypeCode';
    //    //tdd.prevObject[1].getElementsByTagName('input')[2].name = 'TeamList[' + currindex + '].AttendanceStatusTypeCode';
    //    tdd.prevObject[8].getElementsByTagName('input')[0].id = 'TeamList_' + currindex + '__Applicable';
    //        tdd.prevObject[8].getElementsByTagName('input')[0].name = 'TeamList[' + currindex + '].Applicable';


    //     //}
    //});



}

function ApplyFilter(mode) {
    debugger

    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);

    //$("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
           
                /*fillData();*/
            debugger;
           
            

            $.ajax({

                type: 'POST',
                url: '/DataBank/CoreTeam/GetList?TabIndex=' + currentTab,
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
            /*fillData();*/
            debugger;
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/CoreTeam/GetList?TabIndex=' + currentTab,
                dataType: 'html',
                data: $('#AdvancedFilter').serialize() ,
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

        /*fillData();*/
        showSpinner();
        //filterApplicable = 1;
        //$('.reset').val('');
        //$('#Optional_Filter_EmploymentStatus_Code').val(0);
        //$('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/CoreTeam/GetList?TabIndex=' + currentTab,
            dataType: 'html',
            data: $('#AdvancedFilter').serialize() ,
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

function onSaveClick() {

    debugger;
    //if ($('.CoreTeam_Applicable:checked').length == 0) {
    //    swal.fire("Please select Atleast one Entry");
    //    //$('#saveButton').attr('data-target', '');
    //}

    //else {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
            //    // Remove unchecked checkboxes from coreEmpList
            //    $('.CoreTeam_Applicable').each(function () {
            //        if (!$(this).is(':checked')) {
            //            $(this).closest('tr').remove();
            //        }
            //    });
            //    // Add checked checkboxes to coreEmpList
            //$('.CoreTeam_Applicable:checked').each(function () {
            //    var row = $(this).closest('tr').clone();
            //    $('#coreEmpList tbody').append(row);
            //});
                var data = $('#coreEmpList').serialize();
                $.ajax({

                    type: 'POST',
                    url: '/Databank/CoreTeam/CoreTeamSave',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            debugger
                            swal.fire("Saved Successfully").then(function () {
                                //$('#tableAllList tbody tr').remove();
                                var url = "/DataBank/CoreTeam/Index";
                                window.location.href = url;
                                //fillData();
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


    //}

}

function ExportToExcel() {
    debugger;
    var tableLength = $('#CoreTeamListTable tr').length;
    var currentTabIndex = 1;
    var filter = $('#AdvancedFilter').serialize();
    //var filter = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filter + '&TabIndex=' + currentTabIndex ;
    if (tableLength > 0) {
        url = '/Databank/CoreTeam/ExportToExcel?' + PARAM   ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function searchRows() {
    debugger;
    var input, filter, table, tr, tdID, tdName, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = tab == 0 ? document.getElementById("AllListTable") : document.getElementById("CoreTeamListTable");
    tr = table.getElementsByTagName("tr");
    var tbody = table.getElementsByTagName("tbody")[0];
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 1; i < tr.length; i++) {  
        tdID = tr[i].getElementsByTagName("td")[0];  
        tdName = tr[i].getElementsByTagName("td")[1]; 

        if (spannedRows > 0) {
            tr[i].style.display = show ? "" : "none";
            spannedRows--;
            if (!show) noOfRows--;
        } else if (tdID || tdName) {
            let idMatch = tdID && tdID.innerHTML.toUpperCase().indexOf(filter) > -1;
            let nameMatch = tdName && tdName.innerHTML.toUpperCase().indexOf(filter) > -1;

            if (idMatch || nameMatch) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            let rs = tdID ? tdID.getAttribute("rowspan") : null;
            if (rs && rs > 1) {
                show = idMatch || nameMatch;
                spannedRows = rs - 1;
            }
        }
    }

    if (noOfRows == 1) {
        $(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='8'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }
}
