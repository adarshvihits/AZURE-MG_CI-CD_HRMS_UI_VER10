var radioType = 2;
var pagenumber = 0;
function onTypeChange(Type) {
    var VacancyGI = $('#VacancyManagementGI').val();
    radioType = Type;
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/DataBank/MRFVsResume/GetMRFVsResumeList',
        dataType: 'html',
        data: { Type: Type, VacancyGI: VacancyGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#MRFVsResumeList').html("");
                $('#MRFVsResumeList').html(result);
                hideSpinner();
            }

            
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var Type = $('input[id="BasedOn"]:checked').val();
    var VacancyGI = $('#VacancyManagementGI').val();

    var temp = filter1;

    var dataTable = $("#ResumeData_List").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/MRFVsResume/GetMRFVsResumeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&Type=' + Type + '&VacancyGI=' + VacancyGI+ '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;
                    d._model = $('#Resume_Mapping_form').serialize();
                    console.log(d._model);

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                console.log(d.data)
                $('#ResumeList_List_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    
                    d.data[i].refNo = d.data[i].refNo + '<input type="hidden" id="ResumeList_List_' + i + '__RefNo' + '" name="ResumeList_List[' + i + '].RefNo" value="' + d.data[i].refNo + '"><input type="hidden" id="ResumeList_List_' + i + '__ResumeDataBankGI' + '" name="ResumeList_List[' + i + '].ResumeDataBankGI" value="' + d.data[i].resumeDataBankGI + '"><input type="hidden" id="ResumeList_List_' + i + '__ResumeDataBankCode' + '" name="ResumeList_List[' + i + '].ResumeDataBankCode" value="' + d.data[i].resumeDataBankCode + '">'

                    if (d.data[i].applicable != false) {
                        d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="ResumeList_List_' + i + '__Applicable" name="ResumeList_List[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')" checked="' + d.data[i].applicable + '"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'
                    }
                    else
                        d.data[i].applicable = '<div class="form-group"><div class="checkbox-inline" ><label class="checkbox checkbox-outline checkbox-success"><input  data-val="true" value="true"  id="ResumeList_List_' + i + '__Applicable" name="ResumeList_List[' + i + '].Applicable" type="checkbox" class="HRAtt_Applicability" onchange="onCheckboxChange(this,' + i + ')"><span></span></label><label class="checkbox checkbox-outline checkbox-success"></label></div></div >'


                }
                return d.data;
            }
        },
        "columns": [

            { "data": "refNo", "width": "20%" },
            { "data": "candidateName", "width": "20%" },
            { "data": "designation", "width": "15%" },
            { "data": "workLocation", "width": "15%" },
            { "data": "age", "width": "15%" },
            { "data": "yos", "width": "15%" },
            { "data": "applicable", "width": "15%" },
            
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

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        // dataTable.search('').draw();
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
    $('#ResumeData_List').on('page.dt', function () {
        debugger
        var info = dataTable.page.info();
        pagenumber = info.page + 1;

    });

}


function onCheckboxChange(sender, index) {

    debugger;
    checked = $(sender).is(':checked');

    if (checked) {
        var row = $(sender).closest('tr').html();
        var resumeDataBankGI = $('#ResumeList_List_' + index + '__ResumeDataBankGI').val();
        var resumeDataBankCode = $('#ResumeList_List_' + index + '__ResumeDataBankCode').val();
        var refNo = $('#ResumeList_List_' + index + '__RefNo').val();

        $('#ResumeData_ListTwo tbody').append('<tr id="otSave_' + pagenumber + '_' + index + '">' + row + '</tr>');

        $('#ResumeList_List_' + index + '__ResumeDataBankGI').val(resumeDataBankGI);
        $('#ResumeList_List_' + index + '__ResumeDataBankCode').val(resumeDataBankCode);
      
        $('#ResumeList_List_' + index + '__RefNo').val(refNo);
        $('#ResumeList_List_' + index + '__Applicable').attr('checked', true);

        var checkedCount = $('#ResumeData_List').find('input[type=checkbox]:checked').length;
        var totalRows = $('#ResumeData_List tbody tr').length;

        //if (totalRows == checkedCount) {
        //    $('#SelectAll').attr('checked', true)
        //}

    }
    else {

        $('#otSave_' + pagenumber + '_' + index).remove();
        $('.selectAllChkBx').html('')
        $('.selectAllChkBx').html('<label class="checkbox checkbox-outline checkbox-success"><input data-val="true" value="true" id="SelectAll" name="SelectAll" type="checkbox" onchange="onSelectAllClick()"><span cursorshover="true"></span></label><label class="checkbox checkbox-outline checkbox-success"></label>')

    }

    $('#ResumeData_ListTwo tbody').find("tr").each(function (currindex) {
        debugger;
        // if (currindex > 0) {
        $(this).find("td").eq(2).html(currindex);
        debugger
        var tdd = $(this).find("td").eq(2);
        tdd.prevObject[0].getElementsByTagName('input')[2].id = 'ResumeList_List_' + currindex + '__ResumeDataBankCode';
        tdd.prevObject[0].getElementsByTagName('input')[2].name = 'ResumeList_List[' + currindex + '].ResumeDataBankCode';
        tdd.prevObject[0].getElementsByTagName('input')[1].id = 'ResumeList_List_' + currindex + '__ResumeDataBankGI';
        tdd.prevObject[0].getElementsByTagName('input')[1].name = 'ResumeList_List[' + currindex + '].ResumeDataBankGI';
     
        tdd.prevObject[0].getElementsByTagName('input')[0].id = 'ResumeList_List_' + currindex + '__RefNo';
        tdd.prevObject[0].getElementsByTagName('input')[0].name = 'ResumeList_List[' + currindex + '].RefNo';
        tdd.prevObject[6].getElementsByTagName('input')[0].id = 'ResumeList_List_' + currindex + '__Applicable';
        tdd.prevObject[6].getElementsByTagName('input')[0].name = 'ResumeList_List[' + currindex + '].Applicable';


        // }
    });









}

















function GetList(VacancyGI) {
    debugger
 
    //var VacancyGI = $('#VacancyManagement_List_VacancyManagementGI').val();
    var url = '/DataBank/MRFVsResume/GetMRFVsResume?VacancyGI='+VacancyGI;
    window.location.href = url;
}

function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}





function MapResumes() {
    debugger
    if ($('#ResumeList_List_Count').val() != '') {

       // var table = $('#ResumeList').DataTable();
       // var data = table.$('input,select,textarea').serializeArray();
        var data = $('#Resume_Mapping_form').serialize();
        console.log(data);
        //console.log(table);
        //console.log(data);
        //if (validateDropdowns()) {
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
                    url: '/DataBank/MRFVsResume/Create',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                window.location.href = "/DataBank/MRFVsResume/Index";


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
            else {
                return false;
            }
        })

    }
    else {
        swal.fire("No data found");
    }

}

function GetResumeList()
{
    showSpinner();
    debugger;
    var type = $('#MappedResumes').val();
    //var url = '/DataBank/MRFVsResume/GetList?Type=' + type;
    /*$('#resumeList').load(url, {}, function () { hideSpinner(); });*/
    $.ajax({

        type: 'GET',
        url: '/DataBank/MRFVsResume/GetList',
        dataType: 'html',
        data: { Type: type },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#resumeList').html("");
                $('#resumeList').html(result);
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



function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('#Advance_Filter_AgeAboveOrBelow').val('');
        $('#Advance_Filter_YSAboveOrBelow').val('');
        $('.reset').val('');
        $('.resetdd').val('');
        $(".reset").multiselect('destroy');
        $(".reset").multiselect();
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');


        //$('#Advance_Filter_AgeAboveOrBelow').val('');
        //$('#Advance_Filter_YSAboveOrBelow').val('');
        //$('.reset').val('');
        //$('.reset').prop('checked', false);
        //$('#Optional_Filter_EmploymentStatus_Code').val(0);
        //$('#filterForm').removeClass('filter-apply');

        fillData();

    }
}



//function ApplyFilter(mode) {
//    debugger
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/MRFVsResume/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&Type=" + radioType + "&vacancyGI=" + $('#VacancyManagementGI').val(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#MRFVsResumeList').html("");
//                        $('#MRFVsResumeList').html(result);
//                        $('#MRFVsResumeList').removeClass('hide');
//                    }
//                    hideSpinner();

//                    //$('#rewardsListPartial').addClass('hide');               
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            showSpinner();
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/MRFVsResume/ApplyFilter',
//                dataType: 'html',
//                data: $('#BasicFilter').serialize() + "&Type=" + radioType + "&vacancyGI=" + $('#VacancyManagementGI').val(),
//                success: function (result) {
//                    debugger
//                    CloseSlider('basicFilter');
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }

//                    else {
//                        $('#MRFVsResumeList').html("");
//                        $('#MRFVsResumeList').html(result);
//                        $('#MRFVsResumeList').removeClass('hide');
//                    }
//                    hideSpinner();
//                    //$('#rewardsListPartial').addClass('hide');               
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//        showSpinner();
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('.resetdd').val('');
//        $(".reset").multiselect('destroy');
//        $(".reset").multiselect();
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $('#filterForm').removeClass('filter-apply');
//        $.ajax({

//            type: 'POST',
//            url: '/DataBank/MRFVsResume/ApplyFilter',
//            dataType: 'html',
//            data: $('#BasicFilter').serialize() + "&Type=" + radioType + "&vacancyGI=" + $('#VacancyManagementGI').val(),
//            success: function (result) {
//                debugger

//                //CloseSlider('assetAllocationFilter');

//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }

//                else {
//                    $('#MRFVsResumeList').html("");
//                    $('#MRFVsResumeList').html(result);
//                    $('#MRFVsResumeList').removeClass('hide');
//                }
//                hideSpinner();
//                //$('#rewardsListPartial').addClass('hide');

//            },


//            error: function (xhr, textStatus, errorThrown) {
//                $('body').html(xhr.responseText);
//            }
//        });
//    }
//}
function onYOSChange(val) {
    debugger;
    $('#YOSFrom').val('');
    $('#YOSTo').val('');
    YosText = $('#YSAboveOrBelow_Code option:selected').text();
    if (YosText == 'Between') {

        $('#YOSTo').removeClass('hide');
    }
    else {
        $('#YOSTo').addClass('hide');

    }
}


function onAgeChange(val) {
    debugger;
    $('#AgeFrom').val('');
    $('#AgeTo').val('');
    AgeText = $('#AgeAboveOrBelow_Code option:selected').text();
    if (AgeText == 'Between') {
        $('#AgeTo').removeClass('hide');

    }
    else {
        $('#AgeTo').addClass('hide');

    }
}
function onmappedResumeClick(VacancyGI) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/MRFVsResume/GetMappedResumePopUp?VacancyGI=" + VacancyGI,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }
            /* $("#myDiv").css("display", "block");*/

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}
function viewResume(GI) {
    debugger;
    var url = "/DataBank/MRFVsResume/ResumeView";
    /* window.location.href = url + '?ResumeGI=' + GI;*/
    window.open(url + '?ResumeGI=' + GI, '_blank');
}




function ExportToExcel() {
    debugger
    var type = $('#MappedResumes').val();
    var TableLength = $('#VacancyManagement_List_Count').val();
   /* var TableLength = document.getElementById("vacList").rows.length - 1;*/
    if (TableLength > 0) {
        url = "/DataBank/MRFVsResume/ExportToExcel?Type=" + type;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}