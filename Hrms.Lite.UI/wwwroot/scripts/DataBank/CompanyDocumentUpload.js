function displayDoc(EmployeeGuid,index, Code) {

    var url = "/DataBank/CompanyDocumentUpload/ViewDocument?Index=" + index + "&EmployeeGI=" + EmployeeGuid + "&TypeCode=" + Code;

    $.ajax({
        url: url,
        type: "GET",
        success: function () {
            // File exists → open in new tab
            window.open(url, "_blank");
        },
        error: function (xhr) {
            let response = JSON.parse(xhr.responseText);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: response.message  // localized message
            });
        }
    });

}


function fillData() {
    debugger;

    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;
    var doctypeCode = $('#DocumentType_Code').val();

    let radioVal = $('input[name="Type"]:checked').val();

    var dataTable = $("#emptable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": true,
        "ajax": {
            "url": "/DataBank/CompanyDocumentUpload/GetList?&doctypeCode=" + doctypeCode,
            "type": "POST",
            "datatype": "json",

            "data": function (d) {
                temp = '';

                var searchValue = $('#dataTableSearch').val();
                temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length +
                    '&searchValue=' + searchValue + '&tabIndex=' + radioVal + '&dummy=dummy';
                d.filter = temp;

                return JSON.stringify(d);
            },

            "dataSrc": function (d) {
                $('#EmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);

     
                    d.data[i].fullName = '<input id="EmployeeList_' + i + '__EmployeeGI" type="hidden" value="' + d.data[i].employeeGI + '">' + '<a onclick="onEmployeeClick(' + i + ')">' + d.data[i].fullName + '</a>'

                    d.data[i].add = '<a class="btn btn-icon btn-edit icon-only " onclick="onAddClick(' + i + ')" data-bs-toggle="tooltip" title="Add"><i class="fa fa-plus" aria-hidden="true" ></i></a>'

                }
                return d.data;
            }
        },

        "columns": [
            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "18%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "8%" },
            { "data": "doj", "width": "10%" },
            { "data": "count", "width": "10%" },
            { "data": "add", "width": "10%" },
        ],

        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": true,
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": true,
        "language": {
            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div>',
            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div>',
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

    $('.paginate_button.next').attr({ "data-bs-toggle": "tooltip", "title": "Next" });
    $('.paginate_button.previous').attr({ "data-bs-toggle": "tooltip", "title": "Previous" });
    $('.paginate_button.first').attr({ "data-bs-toggle": "tooltip", "title": "First" });
    $('.paginate_button.last').attr({ "data-bs-toggle": "tooltip", "title": "Last" });

    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
    });

    $('.search-slider-inner').click(function () {
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr({ "data-bs-toggle": "tooltip", "title": "Next" });
        $('.paginate_button.previous').attr({ "data-bs-toggle": "tooltip", "title": "Previous" });
        $('.paginate_button.first').attr({ "data-bs-toggle": "tooltip", "title": "First" });
        $('.paginate_button.last').attr({ "data-bs-toggle": "tooltip", "title": "Last" });
    });

    $.fn.DataTable.ext.pager.numbers_length = 5;
}



function onAddClick(i) {
    debugger
    var EmployeeGI = $('#EmployeeList_' + i + '__EmployeeGI').val()
    var doctypeCode = $('#DocumentType_Code').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/CompanyDocumentUpload/GetSliderDetails?&EmployeeGI=" + EmployeeGI + "&doctypeCode=" + doctypeCode,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#sliderView').html("");
                $('#sliderView').html(Data);
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


function onEmployeeClick(i) {
    debugger
    var EmployeeGI = $('#EmployeeList_' + i + '__EmployeeGI').val()
    var doctypeCode = $('#DocumentType_Code').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/CompanyDocumentUpload/GetDocumentDetails?EmployeeGI=" + EmployeeGI + "&doctypeCode=" + doctypeCode,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('#DOC_Popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
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





function CheckImageExtensionSize(e) {
    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf', 'jpeg', 'jpg', 'png', 'bmp'];
    if (size > 5 * 1024 * 1024) {
        swal.fire('File size cannot exceeds 5 Mb');
        $(e).val('');
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
        return false;
    }
    else
    {
    return true;
    }
 }


function onDeleteClick(trncode, EmpCode) {
    debugger;

    swal.fire({
        title: "Do you want to Delete?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/DataBank/CompanyDocumentUpload/DeleteDocument',
                data: { trncode: trncode, EmpCode: EmpCode },
                success: function (result) {
                    swal.fire(result.message).then(function () {
                        close_modal_popup('compdoc-modal-lg');
                        fillData();                           
                           

                    });
                },
                error: function (xhr) {
                    debugger;
                    swal.fire("Error", "Something went wrong while deleting the document.", "error");
                }
            });
        }
    });
}





function DocSave() {
    debugger;

    var docCode = $('#DocumentType_Code').val();
    var empGUID = $('#EmployeeHeader_EmployeeGuid').val();

    if ($('#File_FileData').val() === '') {
        Swal.fire("Please upload the document");
        return;
    }
    $.ajax({
        type: 'GET',
        url: '/DataBank/CompanyDocumentUpload/CheckDocExists',
        data: { docCode: docCode, employeeGI: empGUID },
        success: function (result) {

           

        
                Swal.fire({
                    text: result.message,
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: "Yes",
                    cancelButtonText: "No"
                }).then((res) => {
                    if (res.isConfirmed) {
                        saveDocument();
                    }
                });
          
        },
        error: function () {
            Swal.fire("Error checking document existence");
        }
    });
}


function saveDocument() {
    var data = new FormData($('#SliderSave')[0]);

    $.ajax({
        type: 'POST',
        url: '/DataBank/CompanyDocumentUpload/DocSave',
        contentType: false,
        processData: false,
        dataType: 'json',
        data: data,
        success: function (result) {
            if (result.success) {
                Swal.fire(result.message).then(function () {
                    CloseSlider('addslider');
                    fillData();
                });
            } else {
                Swal.fire(result.message);
            }
        },
        error: function (xhr) {
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}
