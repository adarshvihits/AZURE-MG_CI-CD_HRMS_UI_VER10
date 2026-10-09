var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;    
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownSocial').val(currentTab);

    GetList(TabIndex);

    prevTab = Tab;
}


function onAddNewClick() {
    $.ajax({

        type: 'GET',
        url: '/DataBank/SocialWall/Create',
        dataType: 'html',        
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#sliderView').html("");
                $('#sliderView').html(result);
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}



function GetDetails(SocialWallGI) {
    $.ajax({

        type: 'GET',
        url: '/DataBank/SocialWall/GetDetails',
        dataType: 'html',
        data: { SocialWallGI: SocialWallGI},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#sliderView').html("");
                $('#sliderView').html(result);
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}


function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        /* showSpinner();*/
        if (filterApplicable >= 1) {
            GetList(currentTab)
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetList(currentTab)
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList(currentTab)
        //    CloseSlider('basicFilter');
    }
}

function GetList(tabindex) {
    $.ajax({

        type: 'GET',
        url: '/DataBank/SocialWall/GetList?TabIndex=' + tabindex,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {                
                $('#ListView').html("");
                $('#ListView').html(result);
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}
function Save() {
    debugger
    //if (validateBank()) {
    var i = 0;
    if ($('#Description').val() == '' && $('#file_FileData').val() == '') {
        i++;
    }
   
    validateMultiselect('GroupCompany', 'grpC', 'GC');//only for showing validation

    if (i == 0) {

        if (validateMultiselect('GroupCompany', 'grpC', 'GC')) { 
           

            var data = new FormData($('#SocialWallSave')[0]);
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
                        url: '/DataBank/SocialWall/Save',
                        contentType: false,
                        processData: false,
                        dataType: 'json',
                        data: data,
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {

                                    CloseSlider('AddNewDiv');
                                    //var url = "/Master/Bank/GetBankList?TabIndex=" + currentTab;
                                    //$('#BankList').load(url, function () { });

                                    GetList("ACTIVE");
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
        }
        else {
            swal.fire("Please enter atleast one");
        }
    
}

function Delete(SocialWallGI) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'GET',
                url: '/DataBank/SocialWall/Delete',
                dataType: 'html',
                data: { SocialWallGI: SocialWallGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        swal.fire("Deleted Successfully");
                        GetList("ACTIVE");
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
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

function ApproveOrReject(SocialWallGI, Mode) {
    var i = 0;
    if (!validateMultiselect('GroupCompany', 'grpC', 'GC'))
        i++;
    if (i == 0) {
        swal.fire({
            text: "Do you want to " + Mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'GET',
                    url: '/DataBank/SocialWall/ApproveOrReject?Mode=' + Mode,
                    dataType: 'json',
                    data: $('#SocialWallApprove').serialize(),
                    success: function (result) {
                        debugger
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            if (result.success == true) {
                                if (Mode == "Approve") {
                                    swal.fire("Approved Successfully");
                                }
                                else {
                                    swal.fire("Rejected Successfully");
                                }
                                CloseSlider('approveSlider')
                            }
                            else
                                swal.fire(result.message);
                            GetList("PENDING");
                        }

                    },



                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
  




}