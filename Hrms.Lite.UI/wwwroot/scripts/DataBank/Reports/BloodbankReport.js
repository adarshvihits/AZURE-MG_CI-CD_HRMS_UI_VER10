

function fillData() {

    debugger;
    var bloodgroup = $('#BloodGroup').val();
    if (bloodgroup.length == 0)
        $('.multiselect-selected-text').html("none selected");

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;   

  
    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#bloodgrouptable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/BloodBankReport/GetBloodBankList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();    

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&bloodgroup=' + bloodgroup + '&dummy=dummy';
                    d.filter = temp;
                    d.bloodgroup = bloodgroup;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#BloodBankReportList_Count').val(d.recordsTotal);

                debugger;
                

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    debugger
                    if (d.data[i].profileFilename != "" && d.data[i].profileFilename != null) {
                        d.data[i].employeeName = `<div class="d-flex align-items-center" ><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=${d.data[i].employeeGI}&Breadcrumb=BloodBankReport&BreadcrumbName=BloodBank Report List">${d.data[i].employeeName}</a></div></div></div>`;


                    }
                    else
                        d.data[i].employeeName = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=BloodBankReport&BreadcrumbName=BloodBank Report List">' + d.data[i].employeeName + '</a></div></div></div>'

                }
               
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "15%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation","width": "9%" },
            { "data": "department","width": "9%" },
            { "data": "location","width": "8%" },
            { "data": "gender","width": "7%" },
            { "data": "age","width": "5%" },
            { "data": "bloodGroup","width": "8%"},
            { "data": "phoneNumber","width": "10%" },
            { "data": "mailId","width": "14%"},
            { "data": "address","width": "15%" },


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


    $("#dataTableSearch").on("keyup search paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        if (!$('.sraech-slider #dataTableSearch').hasClass('field-width-big'))
            dataTable.search('').draw();
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


function GetBloodBankReportList() {
    debugger;

    var bloodgroup = $('#BloodGroup').val();
   

    if (bloodgroup !== '') {
        showSpinner();       
        $.ajax({

            type: 'POST',
            url: '/DataBank/BloodBankReport/GetBloodBankReportList',
            dataType: 'html',
            data: { Boodgroups: bloodgroup },

            success: function (result) {
                debugger               
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#BloodBankList').html("");
                    $('#BloodBankList').html(result);
                }
                hideSpinner();

                         
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
        return true;
    }
    else 
      return false;
    
}



function ApplyFilter(mode) {
    debugger  
  
    var filterData = $('#BasicFilter').serialize()
   
    if (mode == 'apply') {
       /* showSpinner();*/
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');}
        else {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
       /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        fillData();
      /*  CloseSlider('basicFilter');*/
        $('#filterForm').removeClass('filter-apply');
    }
}



function ExportToExcel() {
    debugger;
   
    var bloodgroup = $('#BloodGroup').val();
    var filter1 = $('#BasicFilter').serialize();

    var texts = $('#BloodGroup option:selected').map(function () {
        return $(this).text();
    }).get();  



    var tableLength = $('#BloodBankReportList_Count').val();

    if (tableLength > 0) {
        url = "/DataBank/BloodBankReport/ExportToExcel?data=" + filter1 + "&bloodgroup=" + bloodgroup + "&groups=" + encodeURIComponent(texts)+ "&totRecords=" + tableLength;
      window.location.href = url;
         
        }
        else {
            swal.fire("No data found to export");
        }
    }

