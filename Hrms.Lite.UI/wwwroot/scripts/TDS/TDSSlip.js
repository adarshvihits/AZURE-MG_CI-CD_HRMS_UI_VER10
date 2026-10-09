var currentTab = 'monthTab';
var prevTab = 'monthTab';
function OnAssessmentYrChange() {
    $.ajax({

        type: 'GET',
        url: '/TDS/TDSSlip/GetMonths',
        dataType: 'html',
        data: { AssessmentYrCode: $('#AssessmentYear_Code').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#monthsDiv').html("");
                $('#monthsDiv').html(result);
            }

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
    fillData();
}
function onEmployeeClick(index) {
    var Link = $('#Url_Urllink').val();
    var a = $('#employeeCode_' + index).val();
    var b = $('#CurrentSalaryPeriodCode').val();

    var viewer = $("#reportViewer1").data("telerik_ReportViewer");
    viewer.reportSource({
        report: viewer.reportSource().report,
        parameters: {
            Url: Link +"/api/ReportJson/GetTDSSlipReportJSON?EmployeeCode=" + parseInt($('#employeeCode_' + index).val()) + "&SalaryPeriod=" + parseInt($('#CurrentSalaryPeriodCode').val())
        
            //Url:"http://localhost:5001/api/PaySlipReport/GetPaySlipTestJSON?EmployeeCode=" + parseInt($('#employeeCode_' + index).val()) + "&SalaryPeriod=" + parseInt($('#CurrentSalaryPeriodCode').val())
        }
    });
    $('#reportViewer1').removeClass('hide');
}
function fillData() {
    var Filter = $('#BasicFilter').serialize();
    var SalPeriodCode = parseInt($('#CurrentSalaryPeriodCode').val());
    var temp = Filter;
    var dataTable = $("#EmployeeTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/TDSSlip/GetEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&CurrentSalryPeriodCode=' + SalPeriodCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger;
                $('#EmployeeList_Count').val(d.recordsTotal);
                $('#Url_Urllink').val(d.url);

                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].employeeName = ' <a  onclick="onEmployeeClick(' + i + ')">' + d.data[i].employeeName + '</a><input type="hidden" id="employeeCode_' + i + '" value="' + d.data[i].employeeCode + '"> </div></div>'



                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "30%" },
            { "data": "employeeName", "width": "70%" },

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
function onMonthTabClick(Tab, index) {
    $("#" + prevTab).removeClass("active");
    $("#" + Tab).addClass("active");
    $('#CurrentSalaryPeriodCode').val($('#Months_' + index + '__SalaryPeriodCode').val());
    fillData();
    prevTab = Tab;
}