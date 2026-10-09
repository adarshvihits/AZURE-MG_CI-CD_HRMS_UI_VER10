//----------Position Manager--------------------

function getStatusPill(status) {
    var pillClass = 'pill-neutral';
    switch ((status || '').toUpperCase()) {
        case 'FILLED': pillClass = 'pill-success'; break;
        case 'VACANT': pillClass = 'pill-warning'; break;
        case 'FROZEN': pillClass = 'pill-neutral'; break;
    }
    return '<span class="pill ' + pillClass + '">' + (status || '') + '</span>';
}

function fillData() {
    var filter1 = $('#popupFilter').serialize();
    var temp = filter1;

    var dataTable = $("#PositionManagerListtable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/PositionManager/GetPositionManagerList",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var searchValue = encodeURIComponent($('#dataTableSearch').val());
                    // leading '&' so the first form field is not absorbed into the JSON prefix key
                    temp = '&' + filter1 + '&Filter.draw=' + d.draw + '&Filter.start=' + d.start + '&Filter.length=' + d.length + '&Filter.searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#PositionManagerList_Count').text(d.recordsFiltered);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].positionID = '<span class="id-cell">' + d.data[i].positionID + '</span>';
                    d.data[i].status = getStatusPill(d.data[i].status);
                }
                return d.data;
            },
            "error": function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        },
        "columns": [
            { "data": "positionName", "width": "15%" },
            { "data": "branchName", "width": "15%" },
            { "data": "departmentName", "width": "14%" },
            { "data": "sectionName", "width": "14%" },
            { "data": "gradeName", "width": "10%" },
            { "data": "positionNo", "width": "9%", "className": "text-center" },
            { "data": "reportsTo", "width": "14%" },
            { "data": "status", "width": "9%" },
        ],
        "createdRow": function (row, data) {
            $(row).addClass('pm-row-select').attr('data-gi', data.positionGI);
        },
        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "ordering": false,
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

    setPaginationTooltips();

    $("#dataTableSearch").off(".positionManager").on("keyup.positionManager search.positionManager input.positionManager paste.positionManager cut.positionManager", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    // namespaced so the layout's search-box expand toggle (custom.js) is not unbound
    $('.search-slider-inner').off('click.positionManager').on('click.positionManager', function () {
        var hadSearch = $('#dataTableSearch').val() != '';
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        if (hadSearch)
            dataTable.search('').draw();
    });

    dataTable.on('draw', function () {
        setPaginationTooltips();
    });

    // clicking a row opens the position details slider
    $('#PositionManagerListtable tbody').off('click.positionManager').on('click.positionManager', 'tr.pm-row-select', function () {
        getPositionDetails($(this).attr('data-gi'), this);
    });

    $.fn.DataTable.ext.pager.numbers_length = 5;
    setTimeout(tooltipHover, 500);
}

function getPositionDetails(GI, row) {
    $.ajax({
        type: 'GET',
        url: '/DataBank/PositionManager/PositionDetails',
        dataType: 'html',
        data: { PositionGI: GI },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $(".form-overlay").remove();
                $('body').append('<div class="form-overlay"></div>');
                $('.form-overlay').on('click', closePositionDetails);
                $('#positionDetailsSlider').html(result);

                // highlight the row being viewed
                $('#PositionManagerListtable tbody tr').removeClass('pm-row-active');
                $(row).addClass('pm-row-active');

                // render closed first, then open so the slider slides in from the right
                requestAnimationFrame(function () {
                    requestAnimationFrame(function () {
                        $('#PositionDetailsSlider').addClass('slider-forms-open');
                    });
                });
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

function closePositionDetails() {
    CloseSlider('PositionDetails');
    $('#PositionManagerListtable tbody tr').removeClass('pm-row-active');
    // clear the markup once the slide-out transition has finished
    setTimeout(function () {
        if (!$('#PositionDetailsSlider').hasClass('slider-forms-open'))
            $('#positionDetailsSlider').html("");
    }, 300);
}

function setPaginationTooltips() {
    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");
}

function loadSummary() {
    $.ajax({
        type: 'POST',
        url: '/DataBank/PositionManager/GetPositionManagerSummary',
        dataType: 'html',
        data: $('#popupFilter').serialize(),
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#PositionManagerSummary').html(result);
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

function setFilterAppliedIcon() {
    var applied = $('#popupFilter .reset').filter(function () { return $(this).val() != ''; }).length > 0;
    if (applied) {
        $('#filterForm').addClass('filter-apply');
    }
    else {
        $('#filterForm').removeClass('filter-apply');
    }
}

function onApplyFilter() {
    fillData();
    loadSummary();
    setFilterAppliedIcon();
    CloseSlider('basicFilter');
}

function onResetFilter() {
    $('#popupFilter .reset').val('');
    fillData();
    loadSummary();
    setFilterAppliedIcon();
}
