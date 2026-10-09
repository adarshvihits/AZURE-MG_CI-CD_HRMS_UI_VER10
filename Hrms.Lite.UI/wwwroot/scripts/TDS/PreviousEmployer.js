function onSubmit() {
    debugger
    if (validateEntry()) {
        var data = new FormData($('#PreEmployer_Form')[0]);

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
                    url: '/TDS/PreviousEmployer/SavePreviousEmployer',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                window.location.href = "/TDS/PreviousEmployer/Index";
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
                            window.location.reload();
                        }
                        else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });

            }
            else
                return false;
        })
    }
    else
        return false;
}
function validateEntry() {
    var i = 0;
    if (!validateById('#PrevEmployerName', 'emplyerName'))
        i++;
    if (!validateById('#PrevEmployerPAN', 'pan'))
        i++;
    if (!validatePan('PrevEmployerPAN', 'pan'))
        i++;
    if (!validateById('#PrevEmployerTAN', 'tan'))
        i++;
    if (!validateById('#PrevEmployerFromDate', 'fromdate'))
        i++;
    if (!validateById('#PrevEmployerToDate', 'todate'))
        i++;
    if (!validateById('#GrossIncome', 'grosstotal'))
        i++;
    if ($('#GrossIncome').val() == 0) {

        $('#GrossIncome').addClass('is-invalid');
        $("#grosstotal").addClass('error-message');
        $("#grosstotal").html("Enter Gross Total.");
        i++;
    }
    if (i == 0)
        return true;

    else {
        return false;
    }

}
function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;

    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    console.log(size);
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }

}


//------------------HRMS---------------------------
function ApplyFilter(mode) {
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
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}
function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var AssessmentYear = $('#AssessmentYear_Code').val();

    var dataTable = $("#TDSEmployeeListtable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/PreviousEmployer/GetTDSEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#TDSEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="EmployeeClick(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'

                }

                return d.data;
                debugger;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId", "width": "10%" },
            { "data": "employeeHeader.employeeName", "width": "15%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },


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
    setTimeout(tooltipHover, 500);
    //}
    //else {
    //    return false;
    //}
}
function EmployeeClick(EmployeeGI) {

    var AssessmentYear = $('#AssessmentYear_Code').val();
    var url = "/TDS/PreviousEmployer/Create?EmployeeGI=" + EmployeeGI + "&AssessmentYear=" + AssessmentYear;




    console.log(url);
    window.location.href = url;

}
function showDocument2(filename, absoluteuri) {
    if (filename == "" || absoluteuri == "" || absoluteuri == null)
        swal.fire("No file uploaded");
    else {
        if (filename != "") {
            var url = "/AccessFile/ViewDocument?filename=" + filename;
            window.open(url, "_blank");
        }
    }
}

function showDocument(filename, absoluteuri) {

    if (filename == "" || absoluteuri == "" || absoluteuri == null)
        swal.fire("No file uploaded");
    else {
        if (filename != "") {
            var url = "/AccessFile/ViewDocument?filename=" + filename;
            window.open(url, "_blank");
        }
    }

}

function Clear(EmployeeGI,Filename1,Filename2) {
    swal.fire({
        text: "Do you want to Clear?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'GET',
                url: '/TDS/PreviousEmployer/DeletePreviousEmployerData',               
                dataType: 'json',
                data: { EmployeeGI: EmployeeGI, Filename1: Filename1, Filename2: Filename2 },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Cleared Successfully").then(function () {
                            //window.location.href = "/TDS/PreviousEmployer/Index";
                            window.location.reload();
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });

        }
        else
            return false;
    })
   
}