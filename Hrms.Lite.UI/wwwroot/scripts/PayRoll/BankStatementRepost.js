var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "Reposting";
function OnTabClick(Tab, TabIndex, TabName) {
    debugger;
    currentTab = TabIndex;
    currentTabName = TabName;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    GetList(TabIndex);
    prevTab = Tab;
}


function GetList(TabIndex) {
  
    debugger
    var Month = $('#PayrollMonth_Code').val();
   
    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'Month'))
        i++;
   
    if (i == 0) {
        showSpinner();
        $.ajax({

            type: 'POST',
            url: '/PayRoll/BankStatementRepost/GetList?TabIndex=' + TabIndex,
            dataType: 'html',
            data: { Month: Month },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#RepostList').html("");
                    $('#RepostList').html(result);

                }
                hideSpinner();


            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }

    else
        return false;
}
function onRepostClick(FileName, PostedUser, GroupCompanyGI) {
    var Month = $('#PayrollMonth_Code').val();
    debugger;
    $.ajaxSetup({
        async: false
    });

    swal.fire({
        text: "Do you want to Repost?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: '/PayRoll/BankStatementRepost/RepostingSave?Month=' + Month + "&FileName=" + FileName + "&PostedUser=" + PostedUser + "&GroupCompanyGI=" + GroupCompanyGI,
                dataType: 'json',
                data:'',
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire(result.message).then(() => {
                            GetList(0);

                        });
                    }
                    else {
                        debugger;
                        swal.fire(result.message);
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
    });
}
function onApproveClick(FileName, PostedUser, GroupCompanyGI) {
    var Month = $('#PayrollMonth_Code').val();
    debugger;
    $.ajaxSetup({
        async: false
    });

    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: '/PayRoll/BankStatementRepost/RepostingApproval?Month=' + Month + "&GroupCompanyGI=" + GroupCompanyGI,
                dataType: 'json',
                data: '',
                success: function (result) {
                    debugger;
                    if (result.success) {
                        swal.fire(result.message).then(() => {
                            GetList(1);

                        });
                    }
                    else {
                        debugger;
                        swal.fire(result.message);
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
    });
}