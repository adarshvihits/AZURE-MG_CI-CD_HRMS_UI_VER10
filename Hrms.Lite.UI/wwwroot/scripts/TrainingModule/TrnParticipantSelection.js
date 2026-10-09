function ScheduleFilter(mode) {
    debugger;
    showSpinner();
    var status = $('#TrnStatus').val();
    var filter = $("#ScheduledFilter").serialize();
    $.ajax({
        type: 'POST',
        url: '/TrainingModule/TrnParticipantSelection/GetFilteredList',
        dataType: 'html',
        data: filter + "&ScheduleCode=" + status,
        success: function (Data) {
           
            var m = $('#dutyRostershiftPopUP');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            hideSpinner();
            CloseSlider('basicFilter');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
            hideSpinner();
        }
    });
}


function ScheduleList() {
    showSpinner();
    debugger
    var status = $('#TrnStatus').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/TrnParticipantSelection/GetList',
        dataType: 'html',
        data: filter + "&ScheduleCode=" + status ,
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

function FilteredList() {
    debugger
    //var status = $('#TrnStatus').val();
    //var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/TrnParticipantSelection/GetFilteredList',
        dataType: 'html',
        data: $("#BasicFilter").serialize(),
        success: function (result) {
            debugger
        }
    });
}


function Save() {
    debugger;
    var status = $('#TrnStatus').val();
    var filter = $("#SaveService").serialize();
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
                    url: '/TrainingModule/TrnParticipantSelection/Save',
                    dataType: 'json',
                    data: filter + "&ScheduleCode=" + status,
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                debugger
                                $('.bd-example-modal-lg').modal('hide')
                                fillData();
                                
                                
                              
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



function DeleteAll() {
    debugger;
    var status = $('#TrnStatus').val();
    debugger
    //console.log($('#rewardsSave').serialize());
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
                url: '/TrainingModule/TrnParticipantSelection/DeleteAll?ScheduleCode=' + status,
                dataType: 'json',
                
                success: function (result) {
                    if (result.success == true) {
                        //swal.fire(result.message).then(function () {

                        //    ScheduleList();


                        //});
                        swal.fire("Deleted Successfully");
                        fillData();
                        debugger;


                    }

                    else {
                        swal.fire(result.message).then(function () {
                           /* ScheduleList();*/
                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        fillData();
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







function onDeleteClick(EmployeeGI) {
    debugger
    var status = $('#TrnStatus').val();
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
                url: '/TrainingModule/TrnParticipantSelection/DeleteParticipant?EmployeeGI=' + EmployeeGI,
                dataType: 'json',
                data: { ScheduleCode: status },
                success: function (result) {
                    if (result.success == true) {
                        //swal.fire(result.message).then(function () {

                        //    ScheduleList();


                        //});
                        swal.fire("Deleted Successfully");
                        fillData();
                        debugger;


                    }

                    else {
                        swal.fire(result.message).then(function () {
                            /* ScheduleList();*/
                        });
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

function onAllClick(chkid, tableid) {
    debugger;
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

//function validateAll() {
//    debugger
//    var i = 0;
//    var status = $('#TrnStatus').val();
//    if (!validateById('#TrnStatus', 'Schedule')) {
        
//        i++;
//    }
//    debugger
//    if (i == 0) {
//        $(".databank_rbfilteropen").addClass('slider-btn');
//        $('.slider-btn').click(function () {
//            $('body').append('<div class="form-overlay"></div>');
//            $("div").removeClass('slider-forms-open');
//            var elementClassName = $(this).attr('Id');

//            $('div#' + elementClassName).toggleClass('slider-forms-open');
//        });
//    }
      
//    else
//        return false;
//}




function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            fillData();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            fillData();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();


    }
}

function validateAll() {
    debugger
    var i = 0;
    var status = $('#TrnStatus').val();
    if (!validateById('#TrnStatus', 'Schedule')) {

        i++;
    }
    debugger
    if (i == 0) {
        $.ajax({
            type: 'GET',
            url: '/TrainingModule/TrnParticipantSelection/schedulefilter',
            dataType: 'html',
            //data: filter + "&ScheduleCode=" + status,
            success: function (result) {
                debugger
                $('#Shcedulefilter').html('');
                $('#Shcedulefilter').html(result);
                $('body').append('<div class="form-overlay"></div>');

            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    }

    else
        return false;
}



function ExportToExcel() {
    debugger;
    var status = $('#TrnStatus').val();
    var statusName = $('#TrnStatus option:selected').text();
    var filter = $("#BasicFilter").serialize();
    var TrnParticipantListLength = $('#TrnParticipantList_Count').val();
    var PARAM = 'data=' + filter;
    var Length = $('#TrnParticipantList_Count').val();

    if (TrnParticipantListLength > 0) {
        url = '/TrainingModule/TrnParticipantSelection/ExportToExcel?' + PARAM + "&ScheduleCode=" + status + "&Length=" + Length;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}


function fillData() {

    debugger;

    var status = $('#TrnStatus').val();
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;


    if (status != '') {


        var dataTable = $("#test").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TrainingModule/TrnParticipantSelection/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        console.log(d);
                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&ScheduleCode=' + status + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#TrnParticipantList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].action = '<a  onclick="onDeleteClick(' + "'" + d.data[i].employeeGI + "'" + ')"  > <i class="icon-xl far fa-trash-alt"></i></a > ';

                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "8%" },
                { "data": "employeeName", "width": "15%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "12%" },
                { "data": "location", "width": "11%" },
                { "data": "doj", "width": "12%" },
                //{ "data": "trnStatus", "width": "10%" },
                { "data": "trnStatus", "width": "5%" },
                { "data": "action", "width": "5%" },



            ],






            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "order": [0, "asc"],
            "processing": "true",
            "language": {

                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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
}