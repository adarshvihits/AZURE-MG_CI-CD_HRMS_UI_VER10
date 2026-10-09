function OnAddNewClick() {
    var url = "/DataBank/NewsFeed/Create";
    window.location.href = url;
}
function radiobtnChange(sender) {
    debugger;
    var radioval = sender.value;
    if (radioval == 1) {
        $('.cmb2').removeClass('hide');
        $('#CombinationList2').removeClass('hide');
    }
    else {
        $('.cmb2').addClass('hide');
        $('#CombinationList2').addClass('hide');
    }

}
function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}


function getCombinationDetails(sender, cmb) {
    debugger;
    var ddid = sender.value;

    if (cmb == 0) {
        $('#smallspinnerouter1').addClass('spinner-outer-small');
        $('#smallspinnerinner1').addClass('spinner-small');
        $('.spinner-outer-small').append(showSmallSpinner());
        selectedCombination1name = $('#MultipleCombination1 option:selected').text();
        $.ajax({
            type: 'GET',
            url: '/DataBank/NewsFeed/GetCombinationDetails',
            dataType: 'html',
            data: { id: ddid, type: cmb, combinationName: selectedCombination1name },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#CombinationList1').html("");
                    $('#CombinationList1').html(result);
                    $('#totcomb1Count').html($('#Combinations1_Count').val());
                }
                hideSmallSpinner();


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSmallSpinner();

            }
        });
    }
    else {
        if ($('#MultipleCombination1').val() == ddid) {
            swal.fire("Please Choose Different Combination");
            $('#MultipleCombination2').val('');
            return false;
        }
        else {
            $('#smallspinnerouter2').addClass('spinner-outer-small');
            $('#smallspinnerinner2').addClass('spinner-small');
            $('.spinner-outer-small').append(showSmallSpinner());
            selectedCombination2name = $('#MultipleCombination2 option:selected').text();
            $.ajax({

                type: 'GET',
                url: '/DataBank/NewsFeed/GetCombinationDetails',
                dataType: 'html',
                data: { id: ddid, type: cmb, combinationName: selectedCombination2name },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList2').html("");
                        $('#CombinationList2').html(result);
                        $('#totcomb2Count').html($('#Combinations2_Count').val());
                    }
                    hideSmallSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });
        }
    }
}

function SaveNewsFeed(mode) {
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;

    debugger;
    var i = 0;
    if (!validateById('#NewsFeedname', 'NewsFeednameSpan')) {
        i++;
    }
    if (!validateById('#VisibleFrom', 'visiblefrom')) {
        i++;
    }
    if (!validateById('#VisibleTo', 'visibleto')) {
        i++;
    }
    //if (!validateVisibleDate('#VisibleTo', 'visibleto')) {
    //    i++;
    //}
    //if (!validateVisibleDate('#VisibleFrom', 'visiblefrom')) {
    //    i++;
    //}

    if (!validateById('#FileType', 'filetype')) {
        i++;
    }
    if ($('#Attachment_FileData').val() == '' && $('#Attachment_AbsoluteUri').val() == '') {
        i++;
        swal.fire("Please upload the document");
    }
    if ($('#combination1tblID').find('input[type="checkbox"]:checked').length < 1) {
        i++;
        swal.fire("Please select atleast one combination");
    }
    debugger;
    if ($('#Multiple:checked').val() == 1) {
        debugger;
        if ($('#combination2tblID').find('input[type="checkbox"]:checked').length < 1) {
            i++;
            swal.fire("Please select atleast one multiple combinations");
        }
    }

    if (i == 0) {
        swal.fire({
            text: "Do you want to " + modeName,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var PCData = new FormData($('#NewsFeedSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/NewsFeed/Create',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: PCData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/NewsFeed/Index";
                                window.location.href = url;

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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

function Edit(GI) {
    debugger;
    var url = "/DataBank/NewsFeed/Edit?GI=" + GI;
    window.location.href = url;
}

function confirmDelete(GI) {
    debugger
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

                type: 'POST',
                url: '/DataBank/NewsFeed/Delete',
                dataType: 'json',
                data: { NewsFeedGI: GI },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(() => {
                            window.location.reload();
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