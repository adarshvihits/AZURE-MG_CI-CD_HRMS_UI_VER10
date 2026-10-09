
function onProcMonthChange(sender) {
    debugger;
    $.getJSON("/PayRoll/BonusProcessing/GetMonthDropDown", { Month: $(sender).val() }, function (data) {
        $('#PeriodFrom_Code option').remove();
        for (var i = 0; i < data.length; i++) {
            $('#PeriodFrom_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('#PeriodTo_Code option').remove();
        for (var i = 0; i < data.length; i++) {
            $('#PeriodTo_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
     
    });

}

function onProcessClick() {
    debugger
   
    var i = 0;
    if (!validateById("#ProcessMonth_Code", 'pMonth'))
        i++;
    if (!validateById("#PeriodFrom_Code", 'from'))
        i++;
    if (!validateById("#PeriodTo_Code", 'to'))
        i++;
    if (!validateById("#BonusGroup_Code", 'bgroup'))
        i++;
    if (i == 0) {
        $.ajax({
            type: 'POST',
            url: '/PayRoll/BonusProcessing/Process',
            dataType: 'json',
            data: $('#Bonus_process').serialize(),
            success: function (result) {
              
                debugger;
                if (result.success == true) {
                    
                    swal.fire(result.message).then(function () {                 
                        /*        ProcessedIndex($('#Bonus_process').serialize());*/
                        window.location.href = '/PayRoll/BonusProcessing/ProcessedIndex?P_Month='
                            + $("#ProcessMonth_Code").val() + '&F_Month=' + $("#PeriodFrom_Code").val() + '&T_Month=' + $("#PeriodTo_Code").val() +
                            '&B_Group=' + $("#BonusGroup_Code").val();
                    });
                }
              
            },
           
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }

    else
        return false;
}

function ProcessedIndex(form)
{
    $.ajax({
        type: 'POST',
        url: '/PayRoll/BonusProcessing/ProcessedIndex',
        data: form,
        success: function (result)
        {
            $('body').html('');
            $('body').html(result);

        },

        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}



function GetEmpPopUpDetails(EmployeeGI, Month, EmployeeCode, Bonus) {
   
    $.ajax({
        type: 'GET',
        url: '/PayRoll/BonusProcessing/GetEmployeeDetails',
        dataType: 'html',
        data: { EmployeeCode: EmployeeCode, MonthCode: Month, EmployeeGI: EmployeeGI, Bonus: Bonus },
        success: function (Data) {
            var m = $('.bonuspopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var _model = $('#processed_form').serialize();
    var dataTable = $("#bonusEmpTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": '/PayRoll/BonusProcessing/GetList',
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();                

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&P_MONTH=' + $("#ProcessMonth_Code").val()  +'&dummy=dummy';
                    d.filter = temp;
                    d._model = _model;
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#BonusProcessingList_Count').val(d.recordsTotal);
                $('.p_month').html(d.p_month);
                $('.from_month').html(d.from_month);
                $('.to_month').html(d.to_month);
                $('.bonus').html(d.bonus);
                $('.grid_bonus').html(d.grid_bonus);
                $('.grid_salary').html(d.grid_salary);
                $('.grid_lop').html(d.grid_lop);
                
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].employeeName = '<a class="" data-bs-toggle="modal" data-target=".bonuspopup" onclick="GetEmpPopUpDetails(' + "'" + d.data[i].employeeGI + "'" + ', ' + $("#ProcessMonth_Code").val() + ', ' + d.data[i].employeeCode + ',' + $("#BonusGroup_Code").val()+ ')">' + d.data[i].employeeName + '' + '' + '</a>'
                    if (d.data[i].lwd != "")
                        d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                }
                debugger
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "doj" },
            { "data": "lwd" },
            { "data": "lopTotal" },
            { "data": "bonusTotal" },
           
            { "data": "salaryTotal" }
        
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

    $.fn.DataTable.ext.pager.numbers_length = 5;


}
function ExportToExcel() {
    var param = $('#BasicFilter').serialize() + '&Month=' + $("#ProcessMonth_Code").val() + '&Bonus=' + $("#BonusGroup_Code").val();
    var TableLength = $('#BonusProcessingList_Count').val();
    if (TableLength > 0) {       
        window.location.href = '/PayRoll/BonusProcessing/ExportToExcel?data=' + param + '&Total=' + TableLength;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {  
  
    $("#SearchId").val($('#FilterList_Code').val())
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

