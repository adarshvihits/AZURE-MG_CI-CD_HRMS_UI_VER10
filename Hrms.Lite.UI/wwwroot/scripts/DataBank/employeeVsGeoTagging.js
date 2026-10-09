function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
   

    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#EmployeeVsGeoTaggingTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/EmployeeVsGeoTagging/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                debugger;
                $('#EmployeeVsGeoTaggingList_Count').val(d.recordsTotal);
                //$('#totEmpCnt').html(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].employeeGuid = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + "'" + d.data[i].employeeGuid + "'" + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><input id="employeeGuid_'+i+'" type="hidden" value="'+d.data[i].employeeGuid+'"/></span>';
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "empId", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width":"15%" },
            { "data": "location", "width": "12%" },
            { "data": "punchingLocations", "width": "15%" },

            { "data": "outsidePunching", "width": "10%" },
            { "data": "employeeGuid", "width": "8%" },
            /*{ "data": "ptMonth" },*/



        ],







        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "autoWidth": false,

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


    const myTimeout = setTimeout(tooltipHover, 500);


    //}
    //else {
    //    return false;
    //}

}


function onEditClick(EmployeeGI) {
    debugger;

        $.ajax({

            type: 'GET',
            url: '/DataBank/EmployeeVsGeoTagging/GetDetails',
            dataType: 'html',
            data: { EmployeeGI: EmployeeGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#editSlider').html("");
                    $('#editSlider').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });

}


function MultiselectValidations(val) {
    debugger;
    if ($('#' + val).val().length == 0) {
        //$('.' + divId).addClass('is-invalid');
        //$("#" + id + "").addClass('error-message');
        //$("#" + id + "").html("This field is required");
        return false;
    }
    else {
        //$('.' + divId).removeClass('is-invalid');
        //$("#" + id + "").removeClass('error-message');
        //$("#" + id + "").html("");

        return true;
    }
}


function Save() {
    var i = 0;
    if (!MultiselectValidations('ApplicablePunchingLocations') && $('#EmployeeVsGeoTaggingDetails_OutsidePunchingApplicable').is(':checked') == false) {
        i++
    }    
    if (i == 0) {
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
                    url: '/DataBank/EmployeeVsGeoTagging/Save',
                    dataType: 'json',
                    data: $('#employeeVsGeoTaggingDetailsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(SavedSuccessfullyMsg).then(function () {
                                CloseSlider('employeeVsGeoTaggingDetails');
                                fillData();
                            });
                        }
                        else {
                            swal.fire(result.message);
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
    else {
        swal.fire("Please enter atleast one");
    }
}


function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        /* showSpinner();*/
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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}

function ExportToExcel() {

    debugger;

    var TableLength = $('#EmployeeVsGeoTaggingList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/EmployeeVsGeoTagging/ExportToExcel?totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }


}