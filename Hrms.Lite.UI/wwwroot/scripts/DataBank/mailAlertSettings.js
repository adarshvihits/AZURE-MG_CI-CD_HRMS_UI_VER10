var pagenumber = 0;



function onCheckboxChange(sender, index) {

    debugger;
    checked = $(sender).is(':checked');

    if (checked) {
        var row = $(sender).closest('tr').html();
        var empcode = $('#MailAlertSettingsList_' + index + '__EmployeeCode').val();
        
        $('#MailAlertSettingsTableTwo tbody').append('<tr id="otSave_' + pagenumber + '_' + index + '">' + row + '</tr>');

        $('#MailAlertSettingsList_' + index + '__EmployeeCode').val(empcode);
        
        $('#MailAlertSettingsList_' + index + '__Applicable').attr('checked', true);

        var checkedCount = $('#MailAlertSettingsTable').find('input[type=checkbox]:checked').length;
        var totalRows = $('#MailAlertSettingsTable tbody tr').length;

        if (totalRows == checkedCount) {
            $('#SelectAll').attr('checked', true)
        }
        $('#ucheckSave_' + pagenumber + '_' + index).remove();
    }
    else {

        $('#otSave_' + pagenumber + '_' + index).remove();

        var row = $(sender).closest('tr').html();
        var empcode = $('#MailAlertSettingsList_' + index + '__EmployeeCode').val();

        $('#MailAlertSettingsTableThree tbody').append('<tr id="ucheckSave_' + pagenumber + '_' + index + '">' + row + '</tr>');

        $('#MailAlertSettingsUncheckList_' + index + '__EmployeeCode').val(empcode);

        $('#MailAlertSettingsUncheckList_' + index + '__Applicable').attr('checked', true);

       



    }
    

    $('#MailAlertSettingsTableTwo tbody').find("tr").each(function (currindex) {
        debugger;
       
        $(this).find("td").eq(2).html(currindex);
        debugger
        var tdd = $(this).find("td").eq(1);
        tdd.prevObject[1].getElementsByTagName('input')[0].id = 'MailAlertSettingsList_' + currindex + '__EmployeeCode';
        tdd.prevObject[1].getElementsByTagName('input')[0].name = 'MailAlertSettingsList[' + currindex + '].EmployeeCode';
       
    });

    $('#MailAlertSettingsTableThree tbody').find("tr").each(function (currindex) {
        debugger;
       
        $(this).find("td").eq(2).html(currindex);
        debugger
        var tdd = $(this).find("td").eq(1);
        tdd.prevObject[1].getElementsByTagName('input')[0].id = 'MailAlertSettingsUncheckList_' + currindex + '__EmployeeCode';
        tdd.prevObject[1].getElementsByTagName('input')[0].name = 'MailAlertSettingsUncheckList[' + currindex + '].EmployeeCode';
       
    });
}




function onSaveClick() {
    debugger
  
   
    
    var data = $('#Mail_Alert_settings_Form').serialize();

 
    swal.fire({
        text: "Do you want to Save ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/DataBank/MailAlertSettings/Save' ,
                dataType: 'json',
                data: data,
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                       
                            swal.fire("Saved Successfully").then(function () {

                                //window.location.href = "/TimeOffice/HRAttendanceApproval/Index";
                                //fillData();
                                var m = $('.bd-example-modal-lg');
                                m.modal('hide');
                                $('#Remarks').val('');
                                refreshDatatable('#MailAlertSettingsTable');

                                $('#MailAlertSettingsTableTwo tbody tr').remove();
                                $('#MailAlertSettingsTableThree tbody tr').remove();

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

        else
            return false;

    })

}









function fillData() {
    debugger;
    var TransactionType = $('#TransactionType_Code').val();

    var filter1 = $('#BasicFilter').serialize();
    

    var dataTable = $('#MailAlertSettingsTable').DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/MailAlertSettings/GetMailAlertSettingsList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TypeCode=' + TransactionType + '&dummy=dummy';
                    d.filter = temp;
                    d._model = $('#Mail_Alert_settings_Form').serialize();
                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                debugger
                console.log(d.data)
                
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].name = d.data[i].name + '<input type="hidden" id="MailAlertSettingsList_' + i + '__EmployeeCode' + '" name="MailAlertSettingsList[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '">'

                    if (d.data[i].applicable != false) {
                        d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="MailAlertSettingsList_' + i + '__Applicable" name="MailAlertSettingsList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                    }
                    else
                        d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="MailAlertSettingsList_' + i + '__Applicable" name="MailAlertSettingsList[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'


                }

                debugger;
                console.log(d);
                $('#MailAlertSettingsList_Count').val(d.recordsTotal);

                console.log(d.data)
                return d.data;
            }

        },


        "columns": [

            { "data": "employeeId", "width": "20%" },
            { "data": "name", "width": "30%" },
            { "data": "mailId", "width": "40%" },
            { "data": "applicable", "width": "10%" }
        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "autoWidth": false,

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
    $('#MailAlertSettingsTable').on('page.dt', function () {
        debugger
        var info = dataTable.page.info();
        pagenumber = info.page + 1;

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
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}


function ExportToExcel() {
    debugger;
    var tableLength = $('#MailAlertSettingsList_Count').val();

    if (tableLength > 0) {
        var param = $('#BasicFilter').serialize();
        var Type = $('#TransactionType_Code').val();
        url = "/DataBank/MailAlertSettings/ExportToExcel?TotalRows=" + tableLength + "&data=" + param + "&TypeCode=" + Type;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}



$(document).ready(function () {

    toggleSaveButton();

    $('#TransactionType_Code').on('change', function () {
        toggleSaveButton();
    });

});

function toggleSaveButton() {
    var transactionType = $('#TransactionType_Code').val();

    if (transactionType == "0" || transactionType == "" || transactionType == null) {
        $('#HRProceedButton').hide();
    } else {
        $('#HRProceedButton').show();
    }
}
