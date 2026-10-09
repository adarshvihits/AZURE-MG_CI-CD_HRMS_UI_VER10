





function GetList() {

    debugger;
    var GroupbyX = $('#GroupByX').val();
    var GroupbyY = $('#GroupByY').val();
  
    var GroupbyXName = $('#GroupByX option:selected').text();
    var GroupbyYName = $('#GroupByY option:selected').text();    


    if (GroupbyX == GroupbyY)
    {
        debugger;
        if (GroupbyXName == '--Select--' && GroupbyYName == '---Select---')
        {
            document.getElementById('buttonID').style.visibility = 'hidden';
          
        }
        else
        {
            swal.fire("Select Different Combinations");
            document.getElementById('buttonID').style.visibility = 'hidden';
     
        }
        $.ajax({

            type: 'POST',
            url: '/DataBank/EmployeeGroupSummaryReport/GetList',
            dataType: 'html',
            data: { GroupbyX: 0, GroupbyY: 0, GroupbyXName: '' },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#Employeegroupsummaryreportlist').html("");
                    $('#Employeegroupsummaryreportlist').html(result);
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        return false;
        
    }
    else {

        if (GroupbyX !== '' && GroupbyY !== '') {          
            $('.reset').val('');            
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmployeeGroupSummaryReport/GetList',
                dataType: 'html',
                data: { GroupbyX: GroupbyX, GroupbyY: GroupbyY, GroupbyXName: GroupbyXName },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#Employeegroupsummaryreportlist').html("");
                        $('#Employeegroupsummaryreportlist').html(result);
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                   

                }
            });
            return true;
        }
      
    
    }


}


function ChangeButtonName() {
    debugger;
    var GroupbyYName = $('#GroupByY option:selected').text();
    $('#buttonID').val("Select " + GroupbyYName);
    $('.Name').html(GroupbyYName);

    showButton();
  
}

function showButton()
{
    debugger;
    document.getElementById('buttonID').style.visibility = 'Visible';
}


function OnSelectButtonClick() {

    debugger;

    var GroupbyY = $('#GroupByY').val();
    var GroupbyYName = $('#GroupByY option:selected').text();

    var GroupbyX = $('#GroupByX').val();
   

  
        $.ajax({
            type: 'GET',
            url: "/DataBank/EmployeeGroupSummaryReport/GetPopUp?GroupbyY=" + GroupbyY + "&GroupbyYName=" + GroupbyYName,
            dataType: 'html',

            success: function (Data) {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    
   
}






function OnApplyClick() {


    debugger;
    var GroupbyX = $('#GroupByX').val();
    var GroupbyY = $('#GroupByY').val();
    var GroupbyXName = $('#GroupByX option:selected').text();
   
        showSpinner();
        console.log($('#Emp-popup').serialize());
        $.ajax({

            type: 'POST',
            url: '/DataBank/EmployeeGroupSummaryReport/FilterColumn?GroupbyX=' + GroupbyX + "&GroupbyY=" + GroupbyY + "&GroupbyXName=" + GroupbyXName,

            dataType: 'html',
            data: $('#Emp-popup').serialize(),
            success: function (result) {


                debugger;
                $('.bd-example-modal-lg').modal('hide');


                $('#Employeegroupsummaryreportlist').html(result);

                hideSpinner();





            },
            error: function (xhr, textStatus, errorThrown) {

                toastr.error(xhr.responseText);
            }
        });
    
}






function ExportToExcel() {

    debugger;
    var GroupbyY = $('#GroupByY').val();
    var GroupbyYName = $('#GroupByY option:selected').text();
    var GroupbyX = $('#GroupByX').val();
    var GroupbyXName = $('#GroupByX option:selected').text();

    var TableLength = document.getElementById("EmpGroupSummaryTable").rows.length - 1;
    if (TableLength > 0) {
        url = "/DataBank/EmployeeGroupSummaryReport/ExportToExcel?GroupbyY=" + GroupbyY + "&GroupbyYName=" + GroupbyYName + "&GroupbyXName=" + GroupbyXName + "&GroupbyX=" + GroupbyX + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}





function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}