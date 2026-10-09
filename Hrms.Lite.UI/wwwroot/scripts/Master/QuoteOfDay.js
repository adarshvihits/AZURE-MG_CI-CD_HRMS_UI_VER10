////function fillData() {

////    debugger;
////    var filter = $('#BasicFilter').serialize();


   
////    var FYCODE = $('#FYCODE').val();

////    /*  $('#JoiningReportTable').html('');*/
////    var dataTable = $("#QuotesTable").DataTable({

////        "pagingType": 'full_numbers',
////        "destroy": 'true',
////        "ajax": {
////            "url": "/Master/QuotesOfDay/GetList",
////            "type": "POST",


////            "datatype": "json",


////            'data':
////                function (d) {
////                    temp = '';
////                    var start = d.start;

////                    var searchValue = $('#dataTableSearch').val();
////                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FYCODE=' + FYCODE+  '&dummy=dummy';
////                    d.filter = temp;

////                    return JSON.stringify(d);
////                },
////            "dataSrc": function (d) {
////                debugger;

////                $('#QuotesOfDayDetails_Count').val(d.recordsTotal);
////                for (var i = 0, ien = d.data.length; i < ien; i++) {



////                    d.data[i].fycode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ', ' + "'" + d.data[i].thought + "'" + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].ThoughtGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                 
////                    d.data[i].thought = '<span id="hideThought_' + i + '">' + d.data[i].thought + '</span ><span id="thoughtTxtbox_' + i + '"class="hide"><input id="newThought_' + i + '" class="form-control form-control-sm" placeholder="Thought" ></span>'


////                    d.data[i].date = '<input type="hidden" id="thoughtCode_' + i + '" value="' + d.data[i].thoughtCode + '"><input type="hidden" id="date_' + i + '" value="' + d.data[i].date + '"> ' + _GetFormattedDate(d.data[i].date);





////                }


////                console.log(d.data);
////                return d.data;
           
////            }


////        },

function fillData() {
        debugger;
    var FYCODE = $("#FYCODE").val();
    
        $.ajax({

            type: 'GET',
            url: '/Master/QuotesOfDay/GetList',
            dataType: 'html',
            data: { FYCODE: FYCODE },
            success: function (result) {
                debugger;
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#QuotesList').html("");
                    $('#QuotesList').html(result);
                }

                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });

}

       

function onEditClick(i) {
    debugger
  
    $('#newThought_' + i).val($('#QuotesOfDayDetails_' + i + '__Thought').val());

    $('#hideThought_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#thoughtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
    $('#newThought_' + i).removeClass('hide');
    
 

}

function onUpdateClick(index,code) {
    debugger;
    var UpdatedThought = $('#newThought_' + index).val()
 
    var ThoughtCode =$("#QuotesOfDayDetails_" + index + "__ThoughtCode").val()

    if (UpdatedThought == "" || UpdatedThought == null) {
        swal.fire("Thought should not be null");
        return false;
    }


    $.ajax({
        type: 'POST',
        url: '/Master/QuotesOfDay/QuotesOfDaySave',
        dataType: 'json',
        data: { Thought: UpdatedThought, ThoughtCode: ThoughtCode, Date: $("#QuotesOfDayDetails_" + index + "__Date").val()},
        success: function (result) {
            debugger;
            if (result.success == true) {

                swal.fire(result.message).then(function () {
                    fillData();
                   
                });

            }
            else {
                swal.fire(result.message).then(function () {

                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });

    
}
function onCancelClick(i) {
    debugger

    $('#hideThought_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#newThought_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newThought_' + i).val(0); 

}

function confirmDelete(ThoughtCode) {
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
                url: '/Master/QuotesOfDay/Delete',
                dataType: 'json',
                data: { ThoughtCode: ThoughtCode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message);
                       
                        url = "/Master/QuotesOfDay/Index";
                        window.location.href = url;
                            
                     
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
function batchupdateClick() {
    debugger;
    var FYCODE = $('#FYCODE').val();
    var FYNAME = $("#FYCODE option:selected").text();

    $.ajax({

        type: 'GET',
        url: '/Master/QuotesOfDay/BatchUpdate',
        dataType: 'html',
        data: { FYCODE,FYNAME: FYNAME },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#batchupdateSlider').html("");
                $('#batchupdateSlider').html(result);
            }
            hideSpinner();


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}
function batchUpdateSave() {
    var i = 0;
    if (!validateById('#From', 'prevcombination')) {
      i++;
    }
    if (!validateById('#To', 'newcombination')) {
        i++;
    }
   
    debugger;
    var FYCODE = $('#FYCODE').val();
  

    var FYNAME = $('#FYNAME').val();
    var From = $('#From').val();
    var To = $('#To').val();
   
    if (validateYear()) {
    if (i == 0) {




            swal.fire({
                text: "Do you want to Update?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.ajax({

                        type: 'POST',
                        url: "/Master/QuotesOfDay/BatchUpdateSave",
                        dataType: 'json',
                        data: $('#offdayBatchUpdateSave').serialize() + "&From=" + From + "&To=" + To,
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire("Updated Successfully").then(function () {
                                    var url = "/Master/QuotesOfDay/Index";
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

        }
        else
            return false;
    }
    else
        return false;
}
function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("QuotesTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
       
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
          
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
   
    if (noOfRows == 1) {
       
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
       
    }
}

function validateYear() {
    debugger;


    //if ($(val).val() == "") {
    //    $(val).addClass('is-invalid');
    //    $("#" + id + "").addClass('error-message');
    //    $("#" + id + "").html("This field is required");
    //}
 /*   else {*/
        debugger;
        var To = ($('#To').val());
        var From = ($('#From').val());
        if (To != "" & From != "") {
            debugger;
        if (To == From) {
           // $(val).addClass('is-invalid');
            //$("#" + id + "").addClass('error-message');

            //$("#" + id + "").html("From And To shouldnot be same");
            swal.fire("From And To shouldnot be same");
            return false;
        }
        else if (From > To) {
            debugger
            //$(val).addClass('is-invalid');
            //$("#" + id + "").addClass('error-message');
            //$("#" + id + "").html("From should be smaller than To ");
            swal.fire("From should be smaller than To ");
        }
        else if (To < From) {
            debugger
            //$(val).addClass('is-invalid');
            //$("#" + id + "").addClass('error-message');
            //$("#" + id + "").html("To should not be smaller than From ");
            swal.fire("To should not be smaller than From ");
        }
        else {
            return true;
        }
        }
        else
            return false;
    
}

function validate(val, id) {

    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");

        /* val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");*/
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");

        return val.value;
    }
}

