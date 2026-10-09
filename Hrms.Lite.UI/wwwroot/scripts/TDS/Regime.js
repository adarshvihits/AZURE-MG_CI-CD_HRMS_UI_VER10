var isselected;
function validateDropdowns() {
    if (validateById('#AssessmentYear_Code', 'Assessment')) {
        return true;
    }
    else {
        return false;
    }
}

function fillData() {

  
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var AssessmentYear = $('#AssessmentYear_Code').val();
  

        var dataTable = $("#RegimeSelectionListtable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TDS/Regime/GetEmpListforRegimeSelection",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#RegimeEmployeeList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        var a = "regimetypevalid_" + i;

                        d.data[i].employee.employeeCode = '<span  onmouseenter="tooltipHover()" id = "hideAction_' + i + '" ><a data-bs-toggle="tooltip" title="Edit" type="button"class="btn btn-icon btn-edit icon-only"style="float: none;" onclick="onEditClick(' + i + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span ><div class="small-btn-group save_Cancel_' + i + ' hide"><a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="onUpdateClick(' + i + ', ' + "'" + d.data[i].employee.employeeGuid + "'" + ')"></i></a><a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-cancel icon-only"><i class="icon-xl fas fa-times cancel-btn" onclick="onCancelClick(' + i + ')"></i></a></div>'

                        if (d.data[i].regimeType.code == 0)
                            d.data[i].regimeType.name = '<td data-label="RegimeType" id="row_' + i + '"><div class="form-group"><span id="hideregimeType_' + i + '">' + d.data[i].regimeType.name + '</span><span id="regimeTyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "regimeTypeCode_' + i + '"  name = "regimeTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="" selected>' + '--Select--' + '</option> <option value=' + '1' + '>Old</option>  <option value=' + '2' + '>New</option> </select><span id="regimetypevalid_' + i + '"></span></div>'
                        else if (d.data[i].regimeType.code == 1)
                            d.data[i].regimeType.name = '<td data-label="RegimeType" id="row_' + i + '"><div class="form-group"><span id="hideregimeType_' + i + '">' + d.data[i].regimeType.name + '</span><span id="regimeTyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "regimeTypeCode_' + i + '"  name = "regimeTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="">' + '--Select--' + '</option> <option value=' + '1' + ' selected>Old</option>  <option value=' + '2' + '>New</option> </select><span id="regimetypevalid_' + i + '"></span></div>'
                        else
                            d.data[i].regimeType.name = '<td data-label="RegimeType" id="row_' + i + '"><div class="form-group"><span id="hideregimeType_' + i + '">' + d.data[i].regimeType.name + '</span><span id="regimeTyperow_' + i + '" class="hide"><span id="calc"> </span></span></td><select class="form-control form-control-sm hide"  id = "regimeTypeCode_' + i + '"  name = "regimeTypeCode" onchange = "validate(this,\'' + a + '\')"><option value="">' + '--Select--' + '</option> <option value=' + '1' + '>Old</option>  <option value=' + '2' + ' selected>New</option> </select><span id="regimetypevalid_' + i + '"></span></div>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employee.employeeId", "width": "10%" },
                { "data": "employee.employeeName", "width": "20%" },
                { "data": "designationName", "width": "15%" },
                { "data": "departmentName", "width": "15%" },
                { "data": "locationName", "width": "10%" },
                { "data": "regimeType.name", "width": "12%" },

                { "data": "employee.employeeCode", "width": "8%" },

            ],






            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

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


        setTimeout(tooltipHover, 500);

}
function onEditClick(index) {
    if (isselected == null)
    isselected = 1;
    $('#hideAction_' + index).addClass('hide'); 
    $('#hideregimeType_' + index).addClass('hide');
    $('#regimeTyperow_' + index).removeClass('hide');
    $('.save_Cancel_' + index).removeClass('hide');
    $('#regimeTypeCode_' + index).removeClass('hide');
}
function onCancelClick(index) {
    debugger;
    //var i = 0;
    //if (!validateById('#regimeTypeCode_' + index, 'regimetypevalid_' + index)) {
    //    i++;
    //}
    //if (i > 0) {
    //    return false;
    //}
    //else {
      
        $('#hideregimeType_' + index).removeClass('hide');
        $('#hideAction_' + index).removeClass('hide');
        $('.save_Cancel_' + index).addClass('hide');
        $('#regimeTyperow_' + index).addClass('hide');
        $('#regimeTypeCode_' + index).addClass('hide');
        isselected = null;
    /*}*/
}
function onUpdateClick(index, EmployeeGI) {

    RegimeType = $('#regimeTypeCode_' + index).val();
    AssessmentYear  = $('#AssessmentYear_Code').val();
    var i = 0;
    if (!validateById('#regimeTypeCode_' + index, 'regimetypevalid_' + index)) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/TDS/Regime/SaveRegimeSelection',
            dataType: 'json',
            data: { UpdatedRegimeType: RegimeType, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear },
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        
                        fillData();
                    

                    });

                }
                else {
                    swal.fire(result.message).then(function () {

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
    isselected = null;

}
function sliderChange() {
   
    var i = 0;
    if (!validateById('#RegimeType_Code', 'RegimType')) {
        i++;
    } 
    if (i == 0) {
        preventENterFlag = 0;
        AssessmentYearName = $('#AssessmentYear_Code option:selected').text();
        AssessmentYearCode = $('#AssessmentYear_Code').val();

       
        if (validateDropdowns()) {
           
            $.ajax({

                type: 'GET',
                url: '/TDS/Regime/GetBatchUpdateDetails',
                dataType: 'html',
                data: { AssessmentYearCode: AssessmentYearCode, AssessmentYearName: AssessmentYearName },
                success: function (result) {
                    debugger;
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#batchUpdate').html("");
                        $('#batchUpdate').html(result);
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

        }
    }
}
function validateBatchUpdateDefaultDropdowns() {
    debugger

    var i = 0;
    if (!validateById('#RegimeType_Code', 'RegimType')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;


}
function BatchUpdateClick() {
    debugger;
    if (validateBatchUpdateDefaultDropdowns()) {
        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TDS/Regime/BatchUpdateRegimeSelection',
                    dataType: 'json',
                    data: $('#BatchUpdateRegime').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                           
                                ApplyFilter('apply');
                                CloseSlider('RBatchUpdateDiv');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

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
            else {
                return false;
            }
        })

    }
}
function ExportToExcel() {
    var AssessmentYearName = $('#AssessmentYear_Code option:selected').text();

     if (validateDropdowns()) {

    var RegimeEmployeeListtableLength = $('#RegimeEmployeeList_Count').val();
    if (RegimeEmployeeListtableLength > 0) {
        url = "/TDS/Regime/ExportToExcel?AssessmentYear=" + $('#AssessmentYear_Code').val() + '&AssessmentYearName=' + AssessmentYearName + '&Totalrow=' + RegimeEmployeeListtableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
     }
}
function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        //showSpinner();
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
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}