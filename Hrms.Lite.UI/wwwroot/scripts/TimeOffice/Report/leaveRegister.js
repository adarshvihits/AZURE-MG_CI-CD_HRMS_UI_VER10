var tableData = '';
var dataTable = '';

$(document).ready(function () {
   
    fillData();

   

    $('#myonoffswitch').on('change', function () {
      
        if (this.checked) {
          
            dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
            // $("#info").text("U checked me, place some code here");
        }
        else {
            dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
            //  $("#info").text("U unchecked me, another piece of code here");
        }
    });

    $('#leaveRegisterTable').on('click', 'tr td.details-control', function () {

        var current = $('#leaveRegisterTable').DataTable();

        var tr = $(this).closest('tr');
        var row = dataTable.row(tr);

        // console.log(row.data());
        //  alert(row.child.isShown());

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row

            row.child(format(row.data())).show();
            tr.addClass('shown');
        }


    });


    function format(d) {
        // `d` is the original data object for the row

        // console.log(tableData);
        // console.log('hai');
        debugger

        var tableHtml = '', thead = '', tbody = '';
        tableHtml = "";
       // console.log(tableData.data.length);
        for (var key in d) {
            if (key == 'leave' || key == 'opening' || key == 'cf' || key == 'credit' || key == 'debit' || key == 'surrender' || key == 'taken' || key == 'balance' || key == 'applied') {

                if (key == 'cf')
                    thead += '<th width="3%">' + key.toUpperCase() + '</th>';
                else if (key == 'surrender')
                    thead += '<th width="3%">' + "Encash" + '</th>';

                else
                    thead += '<th width="3%">' + key.charAt(0).toUpperCase() + key.slice(1) + '</th>';
            }

        }

        for (var i = 0, ien = tableData.data.length; i < ien; i++) {


            if (tableData.data[i].employeeGI == d.employeeGI) {



                tbody += "<tr><td  align='left'>" + tableData.data[i].leave + "</td><td  align='left'>" + tableData.data[i].opening +
                    "</td><td  align='left'>"
                    + tableData.data[i].cf +
                    "</td><td  align='left'>"
                    + tableData.data[i].credit +
                    "</td><td  align='left'>"
                    + tableData.data[i].debit +
                    "</td><td  align='left'>"
                    + tableData.data[i].surrender +
                    "</td><td  align='left'>"
                    + tableData.data[i].taken +
                    "</td><td  align='left'>"
                    + tableData.data[i].balance +
                    "</td><td  align='left'>"
                    + tableData.data[i].applied +
                    "</td></tr > ";

            }

        }



        tableHtml = "<table id='detailTable' style='width:80%'  class='row-border cell-border table-bordered' >" + thead + tbody + '</table>';

       // console.log(tableHtml);


        return (
            tableHtml
        );
    }

    //$('.can-toggle__switch').on('click', function () {
    //    // Expand row details


    //    //  dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
    //    dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
    //    //dataTable.rows().every(function () {
    //    //    // If row has details collapsed
    //    //    if (!this.child.isShown()) {
    //    //        // Open this row
    //    //        this.child(format(this.data())).show();
    //    //        $(this.node()).addClass('shown');
    //    //    }
    //    //});
    //});

});


function onLR_Applicability_AllClick(colchkid, tableid) {
    
    if ($('.' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function fillData() {
    var filter1 = $('#BasicFilter').serialize();
    var leavePeriod = $('#LeavePeriod_Code').val();
    var temp = filter1;
    var TabIndex = $('input[name="BasedOn"]:checked').val();
     dataTable = $("#leaveRegisterTable").DataTable({
        //"paging": true,
        "destroy": true,
         "pagingType": 'full_numbers',
         
        "ajax": {
            "url": "/TimeOffice/LeaveRegister/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                   


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                   

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + leavePeriod + '&TabIndex=' + TabIndex+ '&dummy=dummy';

                    d.filter = temp;

                    d.gridSettings = $("#LRGridSettingsTable :input").serialize();

                    d.LeavePeriodCode = $("#LeavePeriod_Code").val();

                  

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#LeaveRegisterList_Count').val(d.recordsTotal);

                tableData = "";

                tableData = d;
               

               

                var ids = [];
                var clean = [];

                $.each(d.data, function (index, value) {
                    
                    if ($.inArray(value.employeeID, ids) == -1) {
                        ids.push(value.employeeID);
                        clean.push(value);
                    }
                });

                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    continue;

                }

                return clean; 
            },

              "initComplete": function (settings, json) {

                $('#btn-show-all-children').trigger('click');

            }

        },


        "columns": [

            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": '', "width": "4%"
            },

            { "data": "employeeID", "width": "16%" },
            { "data": "employeeName", "width": "16%" },
            { "data": "designation", "width": "16%" },
            { "data": "department", "width": "16%" },
            { "data": "location", "width": "16%" },
            { "data": "totalbalance", "width": "16%" },
          


        ],


         "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "autoWidth": false,

        "scrollCollapse": false,
        "processing": "true",
         language: {
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


    dataTable.on('page', function () {
        // Pagination event occurred

        var isChecked = $("#myonoffswitch").prop("checked");
        if (isChecked==true) {
            console.log('Pagination event detected');
            dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
            
        }



      
        // You can perform any actions you want here
    });



    dataTable.on('draw', function () {
        var isChecked = $("#myonoffswitch").prop("checked");
        if (isChecked == true) {
            dataTable.rows(':not(.parent)').nodes().to$().find('td:first-child').trigger('click');
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

    $.fn.DataTable.ext.pager.numbers_length = 5;

   

} 



function onLeavePeriodChange(elemntId, spanID)
{
    
    if (validateById(elemntId, spanID))
        ApplyFilter('apply');
}

//function ApplyFilter(mode) {
//    fillData();
//    CloseSlider('basicFilter');
//}



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

function onGridApplyClick()
{


    fillData();
    $('.bd-example-modal-lg').modal('hide');

   
   
}

function POPUP() {

    
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/LeaveRegister/GetGridSettings',
        dataType: 'html',

        success: function (Data) {

            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('#LR_Settings');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }
        
        },
        error: function (xhr, textStatus, errorThrown) {
            
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}

function onExportToExcelClick()
{
    debugger;
    var pendingTableLength = $('#LeaveRegisterList_Count').val();
    
    var leavePeriodName = $("#LeavePeriod_Code :selected").text();
    if ($('#LeaveRegisterList_Count').val()>0) {
        url = "/TimeOffice/LeaveRegister/OnExportToExcelClick?data=" + $("#LRGridSettingsTable :input").serialize() + "&LeavePeriodCode=" + $('#LeavePeriod_Code').val() + "&LeavePeriodName=" + leavePeriodName + "&totalrow=" + pendingTableLength + "&TabIndex=" + $('input[name="BasedOn"]:checked').val();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}