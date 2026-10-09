function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;

    var TabIndex = $('input[name="TabIndex"]:checked').val(); 0


    if (TabIndex == 0) {

        $("#AcceptedListDiv").removeClass('hide');
        $("#RejectedListDiv").addClass('hide');

        var dataTable = $("#offerAcceptanceReportAcceptedTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/OfferAcceptanceReport/GetOfferAcceptanceReportList",
                "type": "POST",


                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#OfferAcceptanceReportList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        d.data[i].offerAcceptedDateAndTime = _GetFormattedDate(d.data[i].offerAcceptedDateAndTime);

                        d.data[i].offerLetter = `
                            <div class="d-flex align-items-center">                              
                                    <div>
                                        <a onclick = "showDocument('${d.data[i].offerFileName}','${d.data[i].offerLetterAbsoluteUri}')">  <i class="icon-xl far fa-eye">  </i>
                        
                                         </a>
                                    </div>
                            </div>`;
                    }
                    console.log(d.data);
                    return d.data;
                },
            },

            "columns": [

                { "data": "applicantID", "width": "10%" },
                { "data": "applicantName", "width": "10%" },
                { "data": "employeeID", "width": "15%" },
                { "data": "mrfno", "width": "15%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "acceptedBy", "width": "10%" },
                { "data": "offerAcceptedDateAndTime", "width": "15%" },
                { "data": "offerLetter", "width": "10%" },

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
    }

    else {
        $("#AcceptedListDiv").addClass('hide');
        $("#RejectedListDiv").removeClass('hide');

        var dataTable = $("#offerAcceptanceReportRejectedTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/OfferAcceptanceReport/GetOfferAcceptanceReportList",
                "type": "POST",

                "datatype": "json",

                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#OfferAcceptanceReportList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++)
                    {
                        debugger;
                        d.data[i].offerRejectedDateAndTime = _GetFormattedDate(d.data[i].offerRejectedDateAndTime);
                        d.data[i].offerLetter = `
                            <div class="d-flex align-items-center">
                                    <div>
                                        <a onclick = "showDocument('${d.data[i].offerFileName}','${d.data[i].offerLetterAbsoluteUri}')">  <i class="icon-xl far fa-eye">  </i>                      
                                         </a>
                                    </div>
                            </div>`;
                    }
                    console.log(d.data);
                    return d.data;
                },
            },

            "columns": [

                { "data": "applicantID", "width": "10%" },
                { "data": "applicantName", "width": "10%" },
                { "data": "mrfno", "width": "15%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "rejectedBy", "width": "10%" },
                { "data": "rejectReason", "width": "15%" },
                { "data": "offerRejectedDateAndTime", "width": "15%" },
                { "data": "offerLetter", "width": "10%" },

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
    }

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

function ApplyFilter(mode) {
    debugger

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
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}

function ExportToExcel() {
    debugger;
    var TabIndex = $('input[name="TabIndex"]:checked').val();
    var Filter = $('#BasicFilter').serialize();
    var tlength = 0;
    if (TabIndex == 0)
    {
        var currentTabName = "Accepted";
        tlength = $('#OfferAcceptanceReportList_Count').val();
    }
    else {
        var currentTabName = "Rejected";
        tlength = $('#OfferAcceptanceReportList_Count').val();
    }

    if (tlength != 0) {
        url = "/DataBank/OfferAcceptanceReport/ExportToExcel?TabIndex=" + TabIndex + "&currentTabName=" + currentTabName +
            "&Filter=" + Filter + "&TotalRows=" + tlength;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}




