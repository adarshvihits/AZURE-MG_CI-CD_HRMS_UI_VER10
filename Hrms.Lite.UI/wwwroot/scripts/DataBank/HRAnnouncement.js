function OnAddNewClick() {
    var url = "/DataBank/HRAnnouncement/Create";
    window.location.href = url;
}
function CheckExtensionAndSize(e) {
    debugger;


    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpg', 'jpeg'];
    if (size > 1024 * 1024)  //1,048,576 Bytes=1 MB
    {
        swal.fire('File size should be less than 1 MB.');
        $(e).val('');
        pressed();
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only jpg and jpeg files are allowed.");
        $(e).val('');
        pressed();
        return false;

    }
    return true;

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
            url: '/DataBank/HRAnnouncement/GetCombinationDetails',
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
                url: '/DataBank/HRAnnouncement/GetCombinationDetails',
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


function SaveAnnounment(mode) {
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;

    debugger;
    var i = 0;
    if (!validateById('#Announcementname', 'AnnouncementnameSpan')) {
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
                var PCData = new FormData($('#AnnouncementSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/HRAnnouncement/Create',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: PCData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/HRAnnouncement/Index";
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

function radiobtnChange(sender) {
    debugger;
    var radioval = sender.value;
    if (radioval == 1) {
        $('.cmb2').removeClass('hide');
        $("#MultipleCombination1").find("option").eq(1).hide();
        $("#MultipleCombination2").find("option").eq(1).hide();
        if ($('#MultipleCombination1 option:selected').text() == "Employee") {
            $('#combination1tblbodyID').html("");
            $('#cmb1theadID').html("");
            $('#cmb1tID').html("");
            $('#MultipleCombination1').val("");
        }

        //$("#MultipleCombination1 option:contains('Employee')").remove();
        //$("#MultipleCombination2 option:contains('Employee')").remove();
    }

    else {
        debugger;
        /*$("#MultipleCombination1 option:contains('Department')").append($('<option></option>').val(1).html("Employee"));*/
        $("#MultipleCombination1").find("option").eq(1).show();
        $('#combination2tblbodyID').html("");
        $('#cmb2theadID').html("");
        $('#MultipleCombination2').val("");
        $('#totcomb2Count').html("");
        $('.cmb2').addClass('hide');
    }

}

function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function Edit(GI)
{
    debugger;
    var url = "/DataBank/HRAnnouncement/Edit?GI=" + GI;
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
                url: '/DataBank/HRAnnouncement/Delete',
                dataType: 'json',
                data: { AnnouncementGI: GI },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/HRAnnouncement/GetAnnouncementList";
                            $('#AnnouncementList').load(url, function () { });
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

function onPreviewClick(url) {
    debugger
    if (url == "")
        swal.fire("No documents Upladed");
    else
        window.open(url, "_blank");
}

function Preview() {
    debugger
    if ($('#Attachment_FileData').val() == '' && $('#Attachment_AbsoluteUri').val() == '') {
        swal.fire("No file uploaded");
    }

    else if ($('#Attachment_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    else {
        window.open($('#Attachment_AbsoluteUri').val(), '_blank');
    }
}

function DateSelect()
{
    if ($('#VisibleTo').val() < $('#VisibleFrom').val())
    {
        swal.fire("FRom");
    }

}


function DateSelect(val, id) {
  
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var FromDate = new Date($('#VisibleFrom').val());
        var Todate = new Date($('#VisibleTo').val());
       
        var iFromDate = new Date(FromDate.toLocaleDateString());
        var iTodate = new Date(Todate.toLocaleDateString());

        if (iTodate < iFromDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Visible To Date should be greater than Visible From date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }
}


function combination1searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination1tblID");
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

    //for (i = 0; i < tr.length; i++) {
    //    debugger;
    //    td = tr[i].getElementsByTagName("td")[0];
    //    if (td) {
    //        txtValue = td.textContent || td.innerText;
    //        if (txtValue.toUpperCase().indexOf(filter) > -1) {
    //            tr[i].style.display = "";
    //        } else {
    //            tr[i].style.display = "none";
    //            noOfRows--;
    //        }
    //    }
    //}
}

function combination2searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList2");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination2tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
}