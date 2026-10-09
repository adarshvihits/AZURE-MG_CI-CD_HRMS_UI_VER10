var gen_month = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");


var preventENterFlag = 0;
var currentPage = 0;
function getMimeTypeFromFilename(filename) {
    debugger
    const extension = filename.split('.').pop().toLowerCase();
    const mimeTypeMap = {
        'jpg': 'data:image/jpeg;base64,',
        'jpeg': 'data:image/jpeg;base64,',
        'png': 'data:image/png;base64,',
        // Add more mappings for other supported extensions
    };
    return mimeTypeMap[extension] || null;
}
function _GetFormattedDate(date) {
    

    if (date != null) {
        var formattedDate = new Date(date);
        var cDate = formattedDate.getDate();
        var cMonth = formattedDate.getMonth();
        var cYear = formattedDate.getFullYear();
        if (cDate < 10)
            cDate = '0' + cDate;
        formattedDate = (cDate + "-" + gen_month[cMonth] + "-" + cYear);
        return formattedDate;
    }
    else {
        return null
    }
}
function showDocument(filename, absoluteuri) {
    debugger
  
    if (filename == "" || absoluteuri == "" || absoluteuri == null)
        swal.fire("No file uploaded");
    else {

        $.ajax({

            type: 'GET',
            url: '/AccessFile/GetFileStorageType',   
       
            success: function (data) {
                alert(data);
                if (data == 'BLOB') {
                    debugger
                    if (filename != "") {
                        var url = "/AccessFile/ViewDocument?filename=" + filename;
                        window.open(url, "_blank");
                    }
                }
                else
                    window.open(absoluteuri, "_blank");

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
}
function _GetFormattedDateWithSlash(date) {
    debugger

    if (date != null) {
        var formattedDate = new Date(date);
        var cDate = formattedDate.getDate();
        var cMonth = formattedDate.getMonth();
        var cYear = formattedDate.getFullYear();
        if (cDate < 10)
            cDate = '0' + cDate;
        formattedDate = (cDate + "/" + gen_month[cMonth] + "/" + cYear);
        return formattedDate;
    }
    else {
        return null
    }
}


function close_modal_popup(className)
{
    var m = $('.' + className);
    m.find('.modal-body').html('');
    m.modal('hide');
}
function CloseSlider(id) {
    $("." + id).removeClass('slider-forms-open');
    $(".slider-forms").removeClass('new-slider-expand');
    $(".form-overlay").remove();
}

function CloseFilterSlider(id, divId) {
    $("#" + divId).hide();
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
}

function CloseCard() {
    $('.search-profile-details').hide(300);
    $('.filter-option-inner-inner').html("Search by Employee name/ID");
    $('.dropdown-toggle').prop('title', 'Search by Employee name/ID');
    $('.sliderSearchInput').val(0);
}


function moveCursorToEnd(id) {
    var el = document.getElementById(id)
    el.focus()
    if (typeof el.selectionStart == "number") {
        el.selectionStart = el.selectionEnd = el.value.length;
    } else if (typeof el.createTextRange != "undefined") {
        var range = el.createTextRange();
        range.collapse(false);
        range.select();
    }
}
function CursorFocusOnSearch() {
    if (focus == 0) {
        $('.DataSearchField').focus();
        focus = 1;
    }
    else {
        focus = 0;
    }
}
function SearchClick() {
    debugger;
    if (searchClick == 0) {
        searchClick = 1;
        $('.DataSearchField').addClass('field-width-big');
        CursorFocusOnSearch();
    }
    else {
        searchClick = 0;
        $('.DataSearchField').removeClass('field-width-big');
    }
}

function ViewProfile(id) {
    var FinancialYear = null;
    url = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=" + id + "&FinancialYear=" + FinancialYear;
    window.location.href = url;
}

function showSpinner() {
    $('.spinner-outer').show();
    //const myTimeout = setTimeout(addSpinner, 500);
   
}
function showSmallSpinner() {
    $('.spinner-outer-small').show();
    //const myTimeout = setTimeout(addSpinner, 500);

}
function hideSmallSpinner() {
    $('.spinner-outer-small').hide();
}
function addSpinner() {
    $('.spinner-outer').show();
}

function hideSpinner() {
    $('.spinner-outer').hide();
}
function PreviewFile() {
    debugger
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    window.open(path, "_blank");
}

function tooltipHover() {
  
    $('[data-bs-toggle="tooltip"]').tooltip();
}

function setDateHidePicker() {
    debugger; 
    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        }
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    }

    $('.kt_datepicker_11, .kt_datepicker_11_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows,

        endDate: _GetFormattedDate($("#To").val()),
        autoclose: true,



        format: "dd-M-yyyy",
        orientation: "auto",
        startDate: _GetFormattedDate($("#From").val()),
        clearBtn: true
    });

}


function datePicker() {    
    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        }
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    }

   
    $('.kt_datepicker_2, .kt_datepicker_2_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows,


        format: "dd-M-yyyy",
        orientation: "auto",
        
        clearBtn: true,
        autoclose: true
    });

    // input group layout for modal demo
    $('.kt_datepicker_2_modal').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows
    });

}


function previewDocument(absoluteURI) {
    debugger;
    try {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    catch (err) {
        if (absoluteURI != '' && absoluteURI != undefined)
            window.open(absoluteURI, "_blank");
        else
            swal.fire("No file uploaded");
    }
}

function DisableAllElements(id) {
    $(id).find('input, textarea, button, select').attr('disabled', 'disabled');
}

function searchWithoutDatatable(tableid)
{
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById(tableid);
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
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


    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}

function refreshDatatable(TableId) {
    var table = $(TableId).DataTable();
    table.ajax.reload(null, false);
}

function monthDiff(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth();
    months += d2.getMonth();
    return months <= 0 ? 0 : months;
}

function showHelp() {
    $('.helpIcon').removeClass('helpIcon');
}