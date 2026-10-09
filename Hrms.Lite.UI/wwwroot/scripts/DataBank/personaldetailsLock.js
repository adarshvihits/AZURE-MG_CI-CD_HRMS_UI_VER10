var prevTab = 'UNLOCKED';

function tabClick(tab) {
    $("." + prevTab).removeClass("active");
    $("." + tab).addClass("active");
    if (tab == 'UNLOCKED') { $(".unlockbutton").addClass('hide'); $(".lockbutton").removeClass('hide'); }
    else { $(".lockbutton").addClass('hide'); $(".unlockbutton").removeClass('hide'); }
    prevTab = tab;
    fillData(tab);

}

function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#personalDtLocktablehide tbody tr').remove();
        $('#personalDtLocktable').find('input[type=checkbox]').prop('checked', true);
        //$('.HRAtt_Applicability').attr('checked', true);
    }
    else {
        //$('.HRAtt_Applicability').attr('checked', false);
        $('#personalDtLocktable').find('input[type=checkbox]').prop('checked', false);
        $('#personalDtLocktablehide tbody tr').remove();
    }


    var totalRows = $('#personalDtLocktable tbody tr').length;
    for (let i = 0; i < totalRows; i++) {
        onCheckboxChange(document.getElementById('PersonalDetailsLockList_' + i + '__ActiveStatus'), i)
    }
}


function onCheckboxChange(sender, index) {
    debugger;
    checked = $(sender).is(':checked');

    if (checked) {
        var row = $(sender).closest('tr').html();
        var empcode = $('#PersonalDetailsLockList_' + index + '__EmployeeCode').val();  

        $('#personalDtLocktablehide tbody').append('<tr id="otSave_'  + index + '">' + row + '</tr>');

        $('#PersonalDetailsLockList_' + index + '__EmployeeCode').val(empcode);      
        $('#PersonalDetailsLockList_' + index + '__ActiveStatus').attr('checked', true);

        var checkedCount = $('#personalDtLocktable').find('input[type=checkbox]:checked').length;
        var totalRows = $('#personalDtLocktable tbody tr').length;

        if (totalRows == checkedCount) {
            $('#SelectAll').attr('checked', true)
        }

    }
    else {
        $('.selectAllChkBx').html('')
        $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

    }

    $('#personalDtLocktablehide tbody').find("tr").each(function (currindex) {
        debugger;
        // if (currindex > 0) {
        $(this).find("td").eq(2).html(currindex);
        debugger
        var tdd = $(this).find("td").eq(2);
        tdd.prevObject[0].getElementsByTagName('input')[0].id = 'PersonalDetailsLockList_' + currindex + '__EmployeeCode';
        tdd.prevObject[0].getElementsByTagName('input')[0].name = 'PersonalDetailsLockList[' + currindex + '].EmployeeCode';
        tdd.prevObject[9].getElementsByTagName('input')[0].id = 'PersonalDetailsLockList_' + currindex + '__ActiveStatus';
        tdd.prevObject[9].getElementsByTagName('input')[0].name = 'PersonalDetailsLockList[' + currindex + '].ActiveStatus';


        // }
    });
}

function fillData(tab) {
    debugger
    $('#personalDtLocktablehide tbody tr').remove();
    var filter1 = $('#BasicFilter').serialize();
    debugger
    var temp = filter1;
    var dataTable = $("#personalDtLocktable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsLock/GetEmployeeList",
                "type": "POST",
                "datatype": "json",
                "async":false,
                'data':
                    function (d) {
                        debugger
                        temp = '';             
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tab=' + tab + '&dummy=dummy';                       
                        d.filter = temp;
                        d._model = $("#LockListsave").serialize();
                        return JSON.stringify(d);
                    },
                



                "dataSrc": function (d) {

                    $('#PersonalDetailsLockList_Count').val(d.recordsTotal);   
                    $(".UnlockedCount").html(d.unlocked);
                    $(".LockedCount").html(d.locked);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].employeeId = '<input id="PersonalDetailsLockList_' + i + '__EmployeeCode" type="hidden" value="' + d.data[i].employeeCode + '">' + d.data[i].employeeId

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].activeStatus = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="PersonalDetailsLockList_' + i + '__ActiveStatus" name="PersonalDetailsLockList[' + i + '].ActiveStatus" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'

                    }
                    console.log(d.data);
                    return d.data;
                }              

            },

            "columns": [

                { "data": "employeeId", "width": "17%"},
                { "data": "employeeName", "width": "17%"},
                { "data": "designation.name", "width": "17%" },
                { "data": "department.name", "width": "12%" },
                { "data": "location.name", "width": "12%" },
                { "data": "category.name", "width": "10%" },
                { "data": "mobile", "width": "17%"},
                { "data": "email", "width": "17%"},
                { "data": "doj", "width": "17%" },
                 { "data": "activeStatus", "width": "8%" },
        ],

            

      


            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "ordering": false,
            "order": [0, "asc"],
            "processing": "true",
            "language": {
                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {
        /*    dataTable.search('').draw();*/
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
function ApplyFilter(mode) {
    debugger 

    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            fillData(prevTab);
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            fillData(prevTab);
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData(prevTab);

    }
}


function LockUnlockSave(Mode) {
    debugger
    var msg = ""; var TabIndex = "";
    if (Mode == "Lock") { msg = "Do you want to Lock?"; TabIndex = 'UNLOCKED' }
    else { msg = "Do you want to UnLock ?"; TabIndex = 'LOCKED' }
        
    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = $('#LockListsave').serialize();
            $.ajax({
                type: 'POST',
                url: '/DataBank/PersonalDetailsLock/Save?TabIndex=' + TabIndex,
                dataType: 'json',
                data: data,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        if (Mode == "Lock") {
                            swal.fire("Locked Successfully").then(function () {
                                window.location.href = '/DataBank/PersonalDetailsLock/Index';
                            });
                        }
                        else {
                            swal.fire("UnLocked Successfully").then(function () {
                                window.location.href = '/DataBank/PersonalDetailsLock/Index';
                            });
                        }

                    }
                    else swal.fire(result.message);               

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else
            return false;

    })

}
function BatchUpdate() {
    debugger
    var msg = ""; 
    if (prevTab == "UNLOCKED") { msg = "On Batch Updating, all the employees in this list will get locked directly.  Do you want to Lock?";  }
    else {
        msg = "On Batch Updating, all the employees in this list will get Unlocked directly.Do you want to UnLock ?"; }

    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var data = $('#BasicFilter').serialize();
            $.ajax({
                type: 'POST',
                url: '/DataBank/PersonalDetailsLock/BatchUpdate?Tab=' + prevTab,
                dataType: 'json',
                data: data,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        if (prevTab == "UNLOCKED") {
                            swal.fire("Locked Successfully").then(function () {
                                window.location.href = '/DataBank/PersonalDetailsLock/Index';
;                            });
                        }
                        else {
                            swal.fire("UnLocked Successfully").then(function () {
                                window.location.href = '/DataBank/PersonalDetailsLock/Index';
                            });
                        }

                    }
                    else swal.fire(result.message);

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else
            return false;

    })

}

function ExportToexcel() {
    debugger; 
    var filter = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filter;
    TableLength = $('#PersonalDetailsLockList_Count').val();
        if (TableLength > 0) {

            url = "/DataBank/PersonalDetailsLock/ExportToExcelEmployeeList?" + PARAM +  "&Tab=" + prevTab + "&TotalRows=" +
                TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }  
}
