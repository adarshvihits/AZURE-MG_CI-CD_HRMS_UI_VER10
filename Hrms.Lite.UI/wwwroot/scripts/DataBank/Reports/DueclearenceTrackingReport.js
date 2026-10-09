function statusButton(value) {
    if (value && value != 0) {
        return '<button class="btn btn-icon btn-tick float-none"><i class="icon-xl fas fa-check"></i></button>';
    } else {
        return '<button class="btn btn-icon btn-close float-none"><i class="icon-xl fas fa-times"></i></button>';
    }
}

function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;

    var dataTable = $("#DueClearenceListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/DueClearenceTrackingReport/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue  + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#DueClearenceList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        d.data[i].lastWorkingdate = _GetFormattedDate(d.data[i].lastWorkingdate);
                        var a = d.data[i].resignationCode;
                        
                        d.data[i].resignationDate = _GetFormattedDate(d.data[i].resignationDate);
                        d.data[i].pdf =
                            '<span id="edit_' + i + '">' +
                            '<a onmouseenter="tooltipHover()" ' +
                        'onclick="onDownloadClick(' + d.data[i].employeeCode + ',' + d.data[i].resignationCode + ',' + d.data[i].userCode + ',' + d.data[i].gcCode + ')" ' +
                            'data-bs-toggle="tooltip" title="Download" type="button" class="btn icon-only">' +
                            '<i class="fa fa-download" aria-hidden="true" style="font-size:22px !important;"></i>' +
                            '</a></span>';

                        if (d.data[i].l1 != 1) {
                            d.data[i].l1 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l1 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l2 != 1) {
                            d.data[i].l2 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l2 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l3 != 1) {
                            d.data[i].l3 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l3 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l4 != 1) {
                            d.data[i].l4 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l4 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l5 != 1) {
                            d.data[i].l5 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l5 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l6 != 1) {
                            d.data[i].l6 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l6 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l7 != 1) {
                            d.data[i].l7 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l7 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }
                        if (d.data[i].l8 != 1) {
                            d.data[i].l8 = '<div><button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button></div>'
                        }
                        else {
                            d.data[i].l8 = '<div><button type="button" class="btn btn-icon "><i class="icon-xl fas fa-check btn-tick2"></i></button></div>'
                        }

                    }
                    console.log(d.data);
                    return d.data;
                },
            },

            "columns": [

                { "data": "resignationCode" },
                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "department" },
                { "data": "location" },
                { "data": "lastWorkingdate" },
                { "data": "resignationDate" },
                { "data": "functionalDesignation" },
                { "data": "l1" },
                { "data": "l2" },
                { "data": "l3" },
                { "data": "l4" },
                { "data": "l5" },
                { "data": "l6" },
                { "data": "l7" },
                { "data": "l8" },
                { "data": "pdf" },
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

function ApplyFilter(mode) {
    debugger
    var Type = $('#Console').val();
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}
function ExportToExcel() {

    debugger;
    var filter = $('#BasicFilter').serialize();

    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();


    var tableLength = $('#DueClearenceList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/DueClearenceTrackingReport/ExportToExcel?data=" + filter + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&TotalRows=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function onDownloadClick(EmployeeCode,ResignationCode,UserCode,GcCode) {
    debugger

    var url = "/DataBank/DueClearenceTrackingReport/GetDueClearencePDF?EmployeeCode=" + EmployeeCode + "&ResignationCode=" + ResignationCode + "&UserCode=" + UserCode + "&GcCode=" + GcCode;
    window.location.href = url;
}