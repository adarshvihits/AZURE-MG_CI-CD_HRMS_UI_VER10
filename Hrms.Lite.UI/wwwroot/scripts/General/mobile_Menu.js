function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
   
}
function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function onCheckBoxClick(tableid) {
    if ($('#' + tableid).find('input[type=checkbox]').is(':checked')) {
        return true;
    }
    else {
        return false;
    }
}
function Save() {


    /*if (validateUserRole('MenuAllCheckBox')) {*/
        
        swal.fire({
            text: "Do you want to Save",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/General/Mobile_Menu/Create',
                    dataType: 'json',
                    data: $('#mobilemenu').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/General/Mobile_Menu/Create";
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
    /*}*/
}
function onNextClick(TabIndex, count, tableId) {
    debugger;
    if (TabIndex == 'Menu') {
        if (onCheckBoxClick(tableId)) {
            var Code = [];
            var Appl = [];
            var Name = [];
            for (i = 0; i < count; i++) {
                if ($('#MenuList_' + i + '__MenuApplicability').is(':checked')) {
                    Appl.push($('#MenuList_' + i + '__MenuApplicability').is(':checked'));
                    Code.push($('#MenuList_' + i + '__MenuCode').val());
                    Name.push($('#MenuList_' + i + '__MenuName').val());
                }

            }
            var url = "/General/Mobile_Menu/onMenuChange";

            $("#SubMenu").load(url, { Code: Code, Appl: Appl, Name: Name }, function () {
            });
            console.log(Code);
            console.log(Appl);
            console.log(Name);
            $('#SubMenu').removeClass('hide');
            $('#Menu').addClass('hide');
   
        }
        else {
            swal.fire("Please Select atleast One Module");
        }
    }
    

  
}

function onBackArrowClck(view) {
    debugger;
   
        $('#Menu').removeClass('hide');
        $('#SubMenu').addClass('hide');
      
   
    
}
