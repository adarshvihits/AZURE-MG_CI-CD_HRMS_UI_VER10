
function fillData() {
    debugger;
    var Filter = $('#BasicFilter').serialize();
    var AssessmentYear = parseInt($('#AssessmentYear_Code').val());
    var temp = Filter;
    var dataTable = $("#EmployeeTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/FORM16/GetEmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger;
                console.log(d.data);
                $('#EmployeeList_Count').val(d.recordsTotal);
                $('#Url_Urllink').val(d.url);
                $('#AssessmentYearCode').val(d.assessmentYearCode);

                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].employeeName = ' <a  onclick="onEmployeeClick(' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].employeeName + '</a><input type="hidden" id="employeeCode_' + i + '" value="' + d.data[i].employeeCode + '"> </div></div>'
                    //    d.data[i].employeeName = ' <a  onclick="onEmployeeClick(' + "'" + i + "'" + ',' + d.data[i].employeeGI +  ')">' + d.data[i].employeeName + '</a><input type="hidden" id="employeeCode_' + i + '" value="' + d.data[i].employeeCode + '"> </div></div>'



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
function onAssessmentYearChange() {

    fillData();
}
function onEmployeeClick(employeeGI) {
    debugger;
    var Link = $('#Url_Urllink').val();

    var AssessmentYear = $('#AssessmentYear_Code').val()
    var viewer = $("#Form16Viewer").data("telerik_ReportViewer");
    viewer.reportSource({
        report: viewer.reportSource().report,
        parameters:
        {
            Url: Link + '/api/ReportJson/GetForm16?EmployeeGI=' + employeeGI + "&AssessmentYear=" + parseInt(AssessmentYear),
        }
    });
    $('#Form16Viewer').removeClass('hide');
}

