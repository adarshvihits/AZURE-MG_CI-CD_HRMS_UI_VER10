function AddNewPreTrainingSchedule() {
    debugger;



        $.ajax({

            type: 'GET',
            url: '/TrainingModule/PreTestSchedule/GetPretestSchedulePopup',
            dataType: 'html',
           // data: { JobRole: JobRole },

            success: function (Data) {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
  
}


function PreTrainingScheduleSave() {
    var i = 0;
    debugger
    var TrainingCode = $("#TrainingCode").val();
    if (TrainingCode == '')
    {
        swal.fire("Please select Training");
        return false;
    }
    if (TrainingCode != '')
    {

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
                    url: '/TrainingModule/PreTestSchedule/PreScheduleTestSave',
                    dataType: 'json',
                    data: $('#Pre_Training_Schedule_Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function ()
                            {
                                debugger;
                                var url = '/TrainingModule/PreTestSchedule/Index';
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



function fillData()
{

    debugger;

    var PreTestScheduleCode = $('#PreTestScheduleCode').val();
    var Filter = $("#BasicFilter").serialize();
    var BasedOn = $('input[name="Status"]:checked').val()
    var temp = Filter;




        var dataTable = $("#test").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TrainingModule/PreTestSchedule/GetAddEmployeeList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        console.log(d);
                        var searchValue = $('#dataTableSearch').val();
                        temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                            '&ScheduleCode=' + PreTestScheduleCode + '&BasedOn=' + BasedOn + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#PreTrainingScheduleList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++)
                    {
                        /*   d.data[i].doj = _GetFormattedDate(d.data[i].doj);*/
                        if (BasedOn == 0) {
                            d.data[i].action = '<a data-bs-toggle="tooltip" title="Proceed" onclick="onAddClick(' + "'" + d.data[i].employeeGI + "'" + ',' + d.data[i].preTestScheduleCode + ')"  > <i class="icon-xl fas fa-plus"></i></a > ';
                        }
                        else
                        {
                            d.data[i].action = '<a  data-bs-toggle="tooltip" title="Delete" onclick="onDeleteClick(' + "'" + d.data[i].employeeGI + "'" + ',' + d.data[i].preTestScheduleCode + ')"  > <i class="icon-xl far fa-trash-alt"></i></a > ';

                        }
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "fullname", "width": "20%" },
                { "data": "designation", "width": "20%" },
                { "data": "department", "width": "20%" },
                { "data": "location", "width": "20%" },
                { "data": "action", "width": "10%" },



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

function FilteredList()
{
    debugger
    //var status = $('#TrnStatus').val();
    //var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/PreTestSchedule/GetFilteredList',
        dataType: 'html',
        data: $("#BasicFilter").serialize(),
        success: function (result) {
            debugger
        }
    });
}

function validateAll(PreTestScheduleCode)
{
    debugger

        $.ajax({
            type: 'GET',
            url: '/TrainingModule/PreTestSchedule/schedulefilter?PreTestScheduleCode=' + PreTestScheduleCode,
            dataType: 'html',
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



function ScheduleFilter()
{

    var PreTestScheduleCode = $('.tabindexcheck').val();
    var Filter = $("#ScheduledFilter").serialize();
    var temp = Filter;
    var filter = $("#BasicFilter").serialize();
    //$.ajax({
    //    type: 'POST',
    //    url: '/TrainingModule/PreTestSchedule/AddEmployee?PreTestScheduleCode=' + PreTestScheduleCode,
    //    dataType: 'html',
    //    data: $("#BasicFilter").serialize(),
    //    success: function (result)
    //    {
    //        debugger
    //    }
    //});
    url = "/TrainingModule/PreTestSchedule/AddEmployee?ScheduleCode=" + PreTestScheduleCode + "&data=" + Filter ;
    window.location.href = url;
}

function onAddClick(employeeGI, PreTestScheduleCode)
{
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
                url: '/TrainingModule/PreTestSchedule/PreTestPeoceed',
                dataType: 'json',
                data: { employeeGI: employeeGI, PreTestScheduleCode: PreTestScheduleCode},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            debugger;
                            fillData();
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

function DeleteAll(PreTestScheduleCode) {
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
                url: '/TrainingModule/PreTestSchedule/PreTestDelete',
                dataType: 'json',
                data: { PreTestScheduleCode: PreTestScheduleCode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            debugger;
                            var url = '/TrainingModule/PreTestSchedule/Index';
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

function onDeleteClick(employeeGI, PreTestScheduleCode) {
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
                url: '/TrainingModule/PreTestSchedule/PreTestDeleteEmployeeWise',
                dataType: 'json',
                data: { employeeGI: employeeGI, PreTestScheduleCode: PreTestScheduleCode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            debugger;
                            fillData();
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


function ExportToExcel()
{
    debugger;
   

    var pendingTableLength = $('#PreTrainingScheduleList_Count').val();
    if (pendingTableLength > 0)
    {
        url = "/TrainingModule/PreTestSchedule/ExportToExcel?totRecords=" + pendingTableLength;
    window.location.href = url;
   
    }
    else {
        swal.fire(NoDataFoundMsg);
    }

}

function ExportToExcelEmployeeWise()
{
    debugger;
    var PreTestScheduleCode = $('#PreTestScheduleCode').val();
    var Filter = $("#BasicFilter").serialize();
    var BasedOn = $('input[name="Status"]:checked').val()
  

    var pendingTableLength = $('#PreTrainingScheduleList_Count').val();
    if (pendingTableLength > 0) {
        url = "/TrainingModule/PreTestSchedule/ExportToExcel_EmployeeWise?totRecords=" + pendingTableLength + "&PreTestScheduleCode=" + PreTestScheduleCode + "&BasedOn=" + BasedOn + "&filter=" + Filter;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }

}