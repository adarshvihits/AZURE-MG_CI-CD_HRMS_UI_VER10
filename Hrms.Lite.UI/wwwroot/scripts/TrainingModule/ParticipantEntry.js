
function ScheduleList() {
    showSpinner();
    debugger
    var status = $('#TrnStatus').val();
    var filter = $("#BasicFilter").serialize();
    var radiomode = $("input[type='radio']:checked").val();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/ParticipantEntry/GetList',
        dataType: 'html',
        data: filter + "&ScheduleCode=" + status + "&radiomode=" + radiomode,
        success: function (result) {
            debugger
            $('#Listview').html('');
            $('#Listview').html(result);
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
            hideSpinner();
        }
    });
}


//function fillData() {
//    var status = $('#TrnStatus').val();
//    var filter = $("#BasicFilter").serialize();
//    var radiomode = $("input[type='radio']:checked").val();
//    debugger;

//        var filter1 = $('#BasicFilter').serialize();
//        var temp = filter1;

//    var dataTable = $("#Participant_List_entry").DataTable({
//            "pagingType": 'full_numbers',
//            "destroy": 'true',
//            "ajax": {
//                "url": "/TrainingModule/ParticipantEntry/GetList",
//                "type": "POST",


//                "datatype": "json",


//                'data':

//                    function (d) {
//                        temp = '';

//                        var start = d.start;

//                        var searchValue = $('#dataTableSearch').val();


//                        temp = filter1 + '&ScheduleCode=' + status + '&radiomode=' + radiomode + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
//                        d.filter = temp;


//                        return JSON.stringify(d);
//                    },




//                "dataSrc": function (d) {





//                    $('#TrainingCompletionList').val(d.recordsTotal);
//                    for (var i = 0, ien = d.data.length; i < ien; i++) {
//                        debugger;
//                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
//                    d.data[i].action = '<a data-bs-toggle="tooltip" title="Edit" class="btn btn-icon btn-edit icon-only" onclick = "EditChange(' + d.data[i].employeeCode +')" > <i class="icon-xl fas fa-pencil-alt"></i></a>'
//                    }

//                    return d.data;
//                }

//            },

//            "columns": [

//                { "data": "employeeID", "width": "20%" },
//                { "data": "employeeName", "width": "20%" },
//                { "data": "designation", "width": "15%" },
//                { "data": "department", "width": "15%" },
//                { "data": "location", "width": "15%" },
//                { "data": "doj", "width": "15%" },
//                { "data": "status", "width": "15%" },
//                { "data": "action", "width": "15%" },
               


//            ],






//            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",

//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {

//                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                sLengthMenu: "_MENU_",
//                paginate: {


//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'




//                }
//            }



//        });



//        $('.ihits-table table').removeClass("dataTable no-footer");


//        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.next').attr("title", "Next");
//        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.previous').attr("title", "Previous");
//        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.first').attr("title", "First");
//        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.last').attr("title", "Last");


//        $("#dataTableSearch").on("keyup search input paste cut", function () {
//            dataTable.search(this.value).draw();

//            $('#noDataFound').css("display", 'none');
//        });

//        $('.search-slider-inner').click(function () {

//            $('#dataTableSearch').val('');
//            $('#dataTableSearch').focus();
//            // dataTable.search('').draw();
//        });

//        dataTable.on('draw', function () {
//            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.next').attr("title", "Next");
//            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.previous').attr("title", "Previous");
//            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.first').attr("title", "First");
//            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//            $('.paginate_button.last').attr("title", "Last");

//        });

//        $.fn.DataTable.ext.pager.numbers_length = 5;

  
//}
function Save() {
    debugger;
    if (validateAll()) {
        var status = $('#TrnStatus').val();
        var filter = $("#updatelist").serialize();
        var radiomode = $("input[type='radio']:checked").val();
        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger


                $.ajax({
                    type: 'POST',
                    url: '/TrainingModule/ParticipantEntry/Save',
                    dataType: 'json',
                    data: filter + "&ScheduleCode=" + status + "&radiomode=" + radiomode,
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {

                                ScheduleList();
                                debugger
                                $('.bd-example-modal-lg').modal('hide')
                              



                                //CloseSlider('salaryHld');



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
    else {
        return false;
    }

}




function onAllClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function validateAll() {
    debugger
    var i = 0;
    var status = $('#TrnStatus').val();
    if (!validateById('#TrnStatus', 'Schedule')) {
        i++;
    }
    debugger

    if (i == 0)
        return true;
    else
        return false;
}


function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            ScheduleList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            ScheduleList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        ScheduleList();


    }
}

function ExportToExcel() {
    debugger;
    var status = $('#TrnStatus').val();
    var statusName = $('#TrnStatus option:selected').text();
    var filter = $("#BasicFilter").serialize();
    var radiomode = $("input[type='radio']:checked").val();
    var TrnParticipantListLength = $('#TrainingCompletionList_Count').val();
    var PARAM = 'data=' + filter;

    if (TrnParticipantListLength > 0) {
        url = '/TrainingModule/ParticipantEntry/ExportToExcel?' + PARAM +"&ScheduleCode=" + status + " &radiomode=" + radiomode;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}


function EditChange(employeecode)
{
    var status = $('#TrnStatus').val();
    var radiomode = $("input[type='radio']:checked").val();


    $.ajax({

        type: 'GET',
        url: '/TrainingModule/ParticipantEntry/EditChange',
        dataType: 'html',
        data: { employeecode: employeecode, status: status, radiomode: radiomode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                swal.fire("Saved Successfully");
                ScheduleList();
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });

}