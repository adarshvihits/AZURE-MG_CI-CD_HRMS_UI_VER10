

function OnAddNewClick() {
    var url = "/DataBank/PoliciesCirculars/Create";
    window.location.href = url;
}

function getCombinationDetails(sender,cmb) {
    debugger;
    var ddid = sender.value;
    /*showSpinner();*/
   
/*    var url = '/DataBank/PoliciesCirculars/GetCombinationDetails?id=' + ddid + '&type=' + cmb;*/
    if (cmb == 0) {
        $('#smallspinnerouter1').addClass('spinner-outer-small');
        $('#smallspinnerinner1').addClass('spinner-small');
        $('.spinner-outer-small').append(showSmallSpinner());
        selectedCombination1name = $('#MultipleCombination1 option:selected').text();
        $.ajax({
            type: 'GET',
            url: '/DataBank/PoliciesCirculars/GetCombinationDetails',
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
            return false;
        }
        else {
            $('#smallspinnerouter2').addClass('spinner-outer-small');
            $('#smallspinnerinner2').addClass('spinner-small');
            $('.spinner-outer-small').append(showSmallSpinner());
            selectedCombination2name = $('#MultipleCombination2 option:selected').text();
            $.ajax({

                type: 'GET',
                url: '/DataBank/PoliciesCirculars/GetCombinationDetails',
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

function AddNewResultRow(tableId, i) {
    debugger;


    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;



    var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });
    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });



    template.find('td[id=grid_slno]').text(count + 1);
    $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
    //------------to clear when new row---------



    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=hidden]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



    //---------------------------------

    //make numbers only inputs.
    $('.numbers-only').keypress(function (e) {
        if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
    });


}


function DeleteResultRow(index) {

    if ($('#Category_' + index + '__GI').val() != "") {
        CategoryDelete(index);
    }
    else {   

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
                var count = 0;
                count = $('tr[id^="row_OI_"]').length;
                if (count > 1) {
                    $("#row_OI_" + index).remove();
                    for (var j = index + 1; j < count; j++) {
                        var template = $('#row_OI_' + j).clone(); // clone the row
                        var code = $("#Category_" + j + "__Name").val();

                        $.each(template.find('input, textarea'), function () {
                            var name = $(this).attr('name');
                            if (typeof name != 'undefined') {
                                name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                                $(this).attr('name', name);
                            }
                            var id = $(this).attr('id');
                            if (typeof id != 'undefined') {
                                id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                                $(this).attr('id', id);
                            }
                            //---------------------------------For row Increment When onkeyup
                            var onkeyup = $(this).attr('onkeyup');
                            if (onkeyup !== undefined) {
                                onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                                $(this).attr('onkeyup', onkeyup);
                            }
                            //---------------------------------------For row Increment When onchange
                            var onchange = $(this).attr('onchange');
                            if (onchange !== undefined) {
                                onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                                $(this).attr('onchange', onchange);
                            }
                            //---------------------------------------
                        });
                        $.each(template.find('select'), function () {
                            var name = $(this).attr('name');
                            name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('name', name);
                            var id = $(this).attr('id');
                            id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('id', id);
                            //---------------------------------------For row Increment When onchange
                            var onchange = $(this).attr('onchange');
                            if (onchange !== undefined) {
                                onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                                // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                                $(this).attr('onchange', onchange);
                            }
                            //---------------------------------------
                        });
                        $.each(template.find('button[type=button]'), function () {
                            var onclick = $(this).attr('onclick');
                            onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                            $(this).attr('onclick', onclick);
                        });
                        //$.each(template.find('.hasDatepicker'), function () {
                        //    $(this).removeClass('hasDatepicker');
                        //});
                        //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                        //    dateFormat: "dd-M-yy", changeMonth: true,
                        //    changeYear: true
                        //});
                        template.find('td[id=grid_slno]').text(j);
                        $("#row_OI_" + j).html(template.html());
                        $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));
                        $("#Category_" + (j - 1) + "__Name").val(code);


                    }
                }
                else {
                    $("#Category_0__Name").val("");


                }
            }
            else {
                return false;
            }
        })
}
}


function SaveCategory() {
    debugger;
    /*    if (validateRewards()) {*/
    debugger
    console.log($('#Categorysave').serialize());
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
                url: '/DataBank/PoliciesCirculars/SaveCategory',
                dataType: 'json',
                data: $('#Categorysave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {

                            var url = "/DataBank/PoliciesCirculars/Index";
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

    //}
    //else {
    //    return false;
    //}
}



function CategoryDelete(i) {

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
                url: '/DataBank/PoliciesCirculars/DeleteCategory',
                dataType: 'json',
                data: { CategoryGI: $('#Category_'+i+'__GI').val() },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            //$('.customFieldPopUp').modal('hide');
                            GetCategoryPopUp();
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


function SavePolicy(mode) {
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;

    debugger;
    var i = 0;
    if (!validateById('#Policyname', 'policyname')) {
        i++;
    }
    if (!validateById('#CategoryCode', 'category')) {
        i++;
    }
    if (!validateById('#VisibleFrom', 'visiblefrom')) {
        i++;
    }
    if (!validateById('#VisibleTo', 'visibleto')) {
        i++;
    }
    if (!validateById('#AcceptanceRequired', 'acceptedrequired')) {
        i++;
    }
    if (!validateVisibleDate('#VisibleTo', 'visibleto')) {
        i++;
    }
    if (!validateVisibleDate('#VisibleFrom', 'visiblefrom')) {
        i++;
    }
    if ($('#AcceptanceRequired').val() == 1) {
        if (!validateById('#Priority', 'priority')) {
            i++;
        }
    }
    
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
                var PCData = new FormData($('#PolicyCircularSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/PoliciesCirculars/Create',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: PCData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/PoliciesCirculars/Index";
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

function GetCategoryPopUp() {
    debugger
    /* showSpinner();*/
    //var url = "/Master/ExitQuestionnaire/GetQuestionnairePopUp";
    //$('#possibleResultPopUp').load(url, {}, function () { hideSpinner(); });
    $.ajax({
        type: 'GET',
        url: "/DataBank/PoliciesCirculars/GetCategoryPopUp",

        dataType: 'html',

        success: function (Data) {
            debugger;
            console.log(Data);
            var m = $('.addcategory');
            m.find('.modal-body').html('');
            debugger;
            m.find('.modal-body').html(Data);
            /*$("#myDiv").css("display", "block");*/

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
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
}

function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
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

function CheckExtensionAndSize(e) {
    debugger;

   
    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf', 'doc', 'docx'];
    //if (size > 1024 * 1024)  //1,048,576 Bytes=1 MB
    //{
    //    swal.fire('File size should be less than 1 MB');
    //    $(e).val('');
    //    return false;
    //}
    if (size >= 3 * 1024 * 1024)  // 3 MB
    {
        swal.fire('File size should be less than 3 MB');
        $(e).val('');
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf,doc and docx files are allowed");
        $(e).val('');
        return false;

    }
    return true;

}

function Edit(GI) {
    debugger;
    var url = "/DataBank/PoliciesCirculars/Edit?GI=" + GI;
    window.location.href = url;
}

function onPreviewClick(url) {
    debugger
    if (url == "")
        swal.fire("No documents Upladed");
    else {
        window.open(url,'_blank');
//        var win = window.open();
//            win.document.write('<iframe src="' + url +'#toolbar=0" allowfullscreen width="100%" height="100%" ><br/><button type="button" value="accept"></button></iframe>')
        //var url123 = "/DataBank/PoliciesCirculars/PdfViewer?url=" + url;
        //window.location.href = url123;
        
    }
    }
   


       




function validateResponsiblePersons(val, divId, id) {
    debugger;
    if (val.value == "") {
        $("." + divId + "").addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $("." + divId + "").removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
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
                url: '/DataBank/PoliciesCirculars/Delete',
                dataType: 'json',
                data: { PolicyGI:GI},
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PoliciesCirculars/GetPolicyListList";
                            $('#PoliciesCircularList').load(url, function () { });
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

function validateVisibleDate(val, id) {
    debugger;
    /*alert($(val).val());*/
    var from = $('#VisibleFrom').val();
    var to = $('#VisibleTo').val();
    date1 = new Date(from);
    date2 = new Date(to);
   
        if (date1 > date2) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Visible To Date should be greater than Visible From Date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
}

function onAccRequiredClick(sender) {
    debugger;
    if (sender.value == 1) {
        $('#AccPrioirity').removeClass('hide');
    }
    else {
        $('#AccPrioirity').addClass('hide');
    }
}

function showPolicyDocument(PolicyGI) {
    debugger
    var url = "/DataBank/PoliciesCirculars/ShowDoc?PolicyGI=" + PolicyGI;
    window.open(url, "_blank");
}