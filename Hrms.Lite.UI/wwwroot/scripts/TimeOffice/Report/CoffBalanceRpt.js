var balanceprevtabcolor = 'tabcolor_bal';
var coffBalancetype = 'Balance';
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    //var month = $('#Month').val();


    var dataTable = $("#CoffBalanceReportTable").DataTable({
        
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            
            "url": "/TimeOffice/CoffBalanceRpt/GetCoffBalanceRptList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();




                    debugger;

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue  + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#CoffBalanceReportList_Count').val(d.recordsTotal);

                console.log(d);

                for (var i = 0; i < d.data.length; i++) {
                    debugger
                    //d.data[i].employeeName = '<a  onclick  /GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + d.data[i].employeeName + '</a>'

                    d.data[i].employeeName = '<a onclick="onEmployeeClick(' + "'" + d.data[i].employeeGI + "'" + ' )">' + d.data[i].employeeName + '</a>'
                    
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },
            { "data": "category" },
            { "data": "balance" },
            { "data": "taken" },
            { "data": "expired" },
         
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

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        /*  dataTable.search('').draw();*/
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

function onEmployeeClick(EmployeeGI) {
    debugger;

    window.location.href = "/TimeOffice/CoffBalanceRpt/GetDetails?EmployeeGI=" + EmployeeGI;


        //$.ajax({

        //    type: 'GET',
        //    url: '/TimeOffice/CoffBalanceRpt/GetCoffBalDetails',
        //    dataType: 'html',
        //    data: { empGI: EmployeeGI },
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }

        //        else {
        //            $('body').append('<div class="form-overlay"></div>');
        //            $('#EditLoanIssueSlider').html("");
        //            $('#EditLoanIssueSlider').html(result);
                   
        //        }

        //        hideSpinner();
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        debugger
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //        hideSpinner();

        //    }
        //});

}

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

function onCoffBalTypeChange(CoffBalType, tabcolor, EmployeeGI) {
    debugger
    $("#" + balanceprevtabcolor).removeClass("active");
    $("#" + tabcolor).addClass("active");
    //showSpinner();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/CoffBalanceRpt/GetList',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, TYPE: CoffBalType },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".ESSP_LoginSession").length > 0) {
                window.location.reload();
            }

            else {
                //$('body').append('<div class="form-overlay"></div>');
                $('.coff_list_Dashboard').html("");
                $('.coff_list_Dashboard').html(result);
            }

            //hideSpinner();
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
    coffBalancetype = CoffBalType;
    balanceprevtabcolor = tabcolor;
}



function ExportToExcel() {
    debugger;
 
    var CoffBalanceReportListLength = $('#CoffBalanceReportList_Count').val();

    if (CoffBalanceReportListLength > 0) {
        url = "/TimeOffice/CoffBalanceRpt/ExportToExcel?totalrows=" + CoffBalanceReportListLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}
   
