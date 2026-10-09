var GenderText = '';
var YosText = '';
var AgeText = '';
var searchClick = 0;





function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var model= $('#Classifications').serialize();
    var temp = filter1;
 


  
    var dataTable = $("#talentPoolTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/TalentPool/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + model + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





                

                $('#TalentPoolList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {




                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID"},
            { "data": "fullName"},
            { "data": "designation"},
            { "data": "location"},
        
           


        ],







        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',


        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

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
        //dataTable.search('').draw();
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



function OnApplyButtonClick() {
    debugger;
    url="/DataBank/TalentPool/GetTalentPoolDetails";
    $('#TalentPoolDetails').load(url, { }, function () { });


}





function onApplyClick() {
    $('#TalentPoolDetails').removeClass('hide');
    showSpinner();
    $.ajax({

        type: 'POST',
        url: '/DataBank/TalentPool/GetTalentPoolDetails',
        dataType: 'html',
        data: $('#Classifications').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {

                $('#TalentPoolDetails').html("");
                $('#TalentPoolDetails').html(result);
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


function onGenderChange() {
    debugger;
    var rr = [];
    $('#GenderCodes :selected').each(function (i, selected) {
        rr[i] = $(selected).text();
    });
    //alert(rr);
    GenderText = $('#Gender_Code option:selected').text();
    $('#Gender_Name').val(rr);
}



function onAllClick(chkid, tableid)
{
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function checkBoxClick(sender,TableId,checkId) {
    debugger;
    if ($(sender).is(':checked')) {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length-1) {

            $('#' + checkId).prop('checked', true)
        }

        //else if ($('#' + TableId).find('input[type="checkbox"]:checked').length-1 == $('#' + TableId).find('input[type="checkbox"]').length-1)
        //{
        //    $('#' + checkId).prop('checked', true)
        //}
        else
            $('#' + checkId).prop('checked', false)
    }
    else {
        if ($('#' + TableId).find('input[type="checkbox"]:checked').length == $('#' + TableId).find('input[type="checkbox"]').length) {

            $('#' + checkId).prop('checked', true)
        }
        else
            $('#' + checkId).prop('checked', false)
    }
}



function onFilterClick() {
    debugger;
    $('#filterSlider').removeClass('hide');
    $('body').append('<div class="form-overlay"></div>');
    $('.basicFilter').addClass('slider-forms-open');
}

function onSearchClick() {
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

function ApplyFilter(mode) {
    debugger    
    if (mode == 'apply') {
       /* showSpinner();*/
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/TalentPool/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();

            //        }
            //        else {
            //            $('#TalentPoolDetails').html("");
            //            $('#TalentPoolDetails').html(result);

            //            $('#TalentPoolDetails').removeClass('hide');
            //        }
            //        hideSpinner();
                              
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //        hideSpinner();
            //    }
            //});
            $('.filter-button').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            $('.filter-button').addClass('filter-apply');
        }

    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('.filter-button').removeClass('filter-apply');
        fillData();
       
    }
}

function onYOSChange(val) {
    $('#YOSFrom').val('');
    $('#YOSTo').val('');
    YosText = $('#YSAboveOrBelow_Code option:selected').text();
    $('#YSAboveOrBelow_Name').val(YosText);
    if (val.value == 3) {
        
        $('#YOSTo').removeClass('hide');

        $('#yosDiv').removeClass('col-md-1');
        $('#yosDiv').addClass('col-md-1');
        //$('#yosDiv').addClass('two-small-input');
    }
    else
    {        
        $('#YOSTo').addClass('hide');

        $('#yosDiv').removeClass('col-md-1');
        //$('#yosDiv').removeClass('two-small-input');
        $('#yosDiv').addClass('col-md-1');
    }
}


function onAgeChange(val) {
    debugger;
    $('#AgeFrom').val('');
    $('#AgeTo').val('');
    AgeText = $('#AgeAboveOrBelow_Code option:selected').text();
    $('#AgeAboveOrBelow_Name').val(AgeText);
    if (val.value == 3) {        
        $('#ageToDiv').removeClass('hide');

        $('#ageDiv').removeClass('col-md-1');
        $('#ageDiv').addClass('col-md-1');
        //$('#ageDiv').addClass('two-small-input');
    }
    else
    {        
        $('#ageToDiv').addClass('hide');

        $('#ageDiv').removeClass('col-md-1');
        //$('#ageDiv').removeClass('two-small-input');
        $('#ageDiv').addClass('col-md-1');
    }
}



function onExportToExcelClick()
{
    debugger;
    var tableLength = $('#TalentPoolList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/TalentPool/ExportToExcel?totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

function POPUP(TalentType, Code) {

    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/TalentPool/GetEmployeeTalentList?TalentType= " + TalentType +"&Code=" +Code,

        dataType: 'html',

        success: function (Data) {
            var m = $('#UHID_Popup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}