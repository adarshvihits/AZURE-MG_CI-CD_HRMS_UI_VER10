function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var dataTable = $("#RecruitmentTrackerTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/RecruitmentTrackerReport/ApplyFilter",
            "type": "POST",

            "datatype": "json",

            'data':

                function (d) {
                    temp = '';

                    var searchId = $('#Filter_Code').val();
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                console.log(d);

                $('#TrackingReporttList_Count').val(d.recordsTotal)

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].action = '<td data-label="Tracker"><i class="icon-xl la la-arrow-circle-right" ' + 'data-bs-toggle="tooltip" title="Tracker" ' + 'onclick="onTrackerClick(' + "'" + d.data[i].applicantGI + "'"+ ',' + "'" + d.data[i].employeeGI + "'" + ')"></i></td>';
                    d.data[i].name = '<a href="/DataBank/RecruitmentTrackerReport/Details?applicantGI=' + d.data[i].applicantGI + '">' + d.data[i].name + '</a>'
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                }
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "applicantId" },
            { "data": "name"},
            { "data": "designation"},
            { "data": "functionalDesignation" },
            { "data": "location" },
            { "data": "salary" },
            { "data": "reason" },
            { "data": "replacementEmployee" },
            { "data": "source" },
            { "data": "consultancy" },
            { "data": "referenceEmployee" },
            { "data": "sourceDetails" },
            { "data": "status" },
            { "data": "enterby"},
            { "data": "entryDate"},
            { "data": "action" },
            
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
        dataTable.search('').draw();
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

//    $.fn.DataTable.ext.pager.numbers_length = 5;
//    const myTimeout = setTimeout(tooltipHover, 500);
}



function onTrackerClick(ApplicantGI, EmployeeGI) {
    debugger;
    $.ajax({

        type: 'GET',
        url: "/DataBank/RecruitmentTrackerReport/GetApproveTrackerDetails",
        dataType: 'html',
        data: { ApplicantGI: ApplicantGI, EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#trackerSlider').html("");
                $('#trackerSlider').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}


function validateDocument(filename) {
    var fname = filename;
    var re = /(\pdf|doc|docx)$/i;
    if (re.exec(fname)) {
        DocumentsUpload(1);
        return true;
    }
    else {
        $("#GetPreviousEmployment_ExCertificate_FileName").val('');
        alert("Please upload *.pdf|doc|docx file");
    }
}
function DocumentsUpload() {

    var xhr = new XMLHttpRequest();
    var fd = new FormData();
    var id = $('#RowID').val();
    if (id != null && id != '' && id != 0 && id != undefined) {
        fd.append("file", document.getElementById('GetPreviousEmployment_ExCertificate_FileName').files[0]);
        var url = '@Url.Action("DocumentsUpload", "CandidateProfile")';
        xhr.open("POST", url + "?id=" + id, true);
        xhr.send(fd);
        toastr.info('Documents Uploaded Successfully!')
    }
}
function DocumentsUpload() {
    var id = $('#RowID').val();
    var fileInput = $('#GetPreviousEmployment_ExCertificate_FileName')[0];

    if (id && fileInput.files.length > 0) {
        var fd = new FormData();
        fd.append("file", fileInput.files[0]);
        fd.append("id", id); // Pass id as part of form data

        $.ajax({
            url: '@Url.Action("DocumentsUpload", "CandidateProfile")',
            type: "POST",
            data: fd,
            contentType: false, // Important for file upload
            processData: false, // Prevent jQuery from processing data
            success: function (response) {
                toastr.success('Document uploaded successfully!');
            },
            error: function (xhr, status, error) {
                toastr.error('Error uploading document: ' + error);
            }
        });
    } else {
        toastr.warning('Please select a file and a valid ID.');
    }
}


function ExportToExcel() {

    debugger;
    //if (validateDropdowns()) {


    var TableLength = $('#TrackingReporttList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/RecruitmentTrackerReport/ExportToExcel?totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else
    {
        swal.fire("No data found to export");
    }
        
    
    

}







//function ExportToExcel() {
//    debugger;
//    //if (validateDropdowns()) {

//    var TableLength = $('#AbscondingReporttList_Count').val();
//    if (TableLength > 0) {
//        url = "/DataBank/RecruitmentTrackerReport/ExportToExcel?Policy=" + $('#InsurancePolicy_Code').val() + "&totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//    //}
//}
function showDocumentPopup(ApplicantGi, DocType, RowId) {
    $.ajax({

        url: '/DataBank/RecruitmentTrackerReport/GetDocumentPopup',
        dataType: 'html',
        data: { ApplicantGI: ApplicantGi, DocType: DocType, RowId: RowId },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.View_Document_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}
function showDecryptDocument(ApplicantGi, DocType, RowId) {
    debugger
    var url = "/DataBank/RecruitmentTrackerReport/ShowDoc?ApplicantGI=" + ApplicantGi + "&DocType=" + DocType + "&RowID=" + RowId;
    window.open(url, "_blank");
}