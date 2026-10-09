function fillData() {
    $.ajaxSetup({ async: false });
    debugger
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    debugger;

    AssignOrUnassign = $('input[id="AssignOrUnassign"]:checked').val();
   if (AssignOrUnassign != "Assigned") {
      $('#update1').removeClass('hide');
    }
    else {
      $('#update1').addClass('hide');
    }



    var dataTable = $("#EmployeeVsCostTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/EmployeeVsCostCenter/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + AssignOrUnassign + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





                console.log(d);
                $('#CostCenterTablist_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger;
                    if (d.data[i].doj != null)
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);


                    /*d.data[i].name = '<div class="d-flex align-items-center">   <a onclick="getCostCenter(' + "'" + d.data[i].employeeGuid + "'" + ')" >' + d.data[i].name + ' </a > </div></div> </div>'*/
                    d.data[i].costCenterUserCode = '<span id="amtLabel2_' + i + '">' + d.data[i].costCenterUserCode + '</span><span id="amtTxtbox2_' + i + '" class="hide"><div style="position:relative;"><select class="form-control form-control-sm costcombo" id="CostCenterList_' + i + '__CostCenterCode" onchange="validate(this,' + "'costval_" + i + "'" +')"><option value="">--Select--</option></select><span id="costval_' + i +'"></span></div>'
                    d.data[i].action = '<span id="editbt2_' + i + '"> <a data-bs-toggle="tooltip" title="Edit"  id="ceedit" class="btn btn-icon btn-edit icon-only" onclick="onEditClick(' + i + ')"><i class="icon-xl fas fa-pencil-alt"></i></a></span> <span id="updateCancel2_' + i + '" class="hide"> <a data-bs-toggle="tooltip" title="Update" id="cupdate" class="btn btn-icon btn-edit icon-only" onclick="onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"><i class="icon-xl fas fa-sync-alt"></i> </a></span> <span id="Cancelbtn_' + i + '" class="hide"><a data-bs-toggle="tooltip" title="Update" id="ccancel"class="btn btn-icon btn-cancel icon-only" onclick="onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a><span>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "id", "width":"10%" },
            { "data": "name", "width":"18%" },
            { "data": "designation","width":"15%"},
            { "data": "department","width":"15%" },
            { "data": "location","width":"12%" },
            { "data": "costCenterName","width":"12%" },
            { "data": "costCenterUserCode", "width": "10%"  },
            { "data": "action", "width": "8%"  }
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


        fillCostCenterCombo();
    });

    $.fn.DataTable.ext.pager.numbers_length = 5;

  

    fillCostCenterCombo();

}

function fillCostCenterCombo() {


    $.getJSON("/DataBank/EmployeeVsCostCenter/GetCostCenterCombo", function (data) {
        console.log(data);
        $('.costcombo option').remove();
        $('.costcombo').append('<option value>--Select Cost--</option');

        for (var i = 0; i < data.length; i++) {
            $('.costcombo').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function costCenterSave() {

                $.ajax({

                    type: 'POST',
                    url: '/DataBank/EmployeeVsCostCenter/Create',
                    dataType: 'json',
                    data: $('#CostCenterSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('cost-save');
                                fillData();
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
          
}

function ApplyFilter(mode) {

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}

function getCostCenter() {
    debugger;

    if ($('#CostCenterTablist_Count').val() != 0) {
        var url = "/DataBank/EmployeeVsCostCenter/AddCostCenter";
        $('body').append('<div class="form-overlay"></div>');
        $('#AddcostcenterSlider').load(url, function () { });
    }


}
function getPopUp() {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
}
function onImportExcelClick() {
    debugger
    AssignOrUnassign = $('input[id="AssignOrUnassign"]:checked').val();
    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger
            var MPPersonalInfo = new FormData($('#EmployeeCostCenterUpload')[0]);
            $("#Excel_FileData").val('');
            MPPersonalInfo.append('TabIndex', AssignOrUnassign);
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmployeeVsCostCenter/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        debugger;
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

function onEditClick(i) {
    debugger;
    $('#amtLabel2_' + i).addClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
    $('#Cancelbtn_' + i).removeClass('hide');
}

function onCancelClick(i) {
    debugger;
    $('#amtLabel2_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
    $('#Cancelbtn_' + i).addClass('hide');
    $('#CostCenterList_' + i + '__CostCenterCode').removeClass('is-invalid');
    $('#costval_' + i).removeClass('error-message');
    $('#costval_' + i).html("");
}


function onUpdateClick(i,GI) {
    debugger;
    var costCode = 0;
    var flag = 0;
    if (i == "batchupdate") {
        costCode = $('#CostCenterCode').val();
        if ($('#CostCenterCode').val() == "") {
            validateById('#CostCenterCode', 'costcenter')
            flag = 1;
        }
    }
    else {
        if ($('#CostCenterList_' + i + '__CostCenterCode').val() != '') {
        $('#amtLabel2_' + i).html($('#CostCenterList_' + i + '__CostCenterCode option:selected').text());
        $('#amtLabel2_' + i).removeClass('hide');
        $('#editbt2_' + i).removeClass('hide');
        $('#amtTxtbox2_' + i).addClass('hide');
        $('#updateCancel2_' + i).addClass('hide');
        $('#Cancelbtn_' + i).addClass('hide');
        costCode = $('#CostCenterList_' + i + '__CostCenterCode').val();
    }
            else {
            validateById('#CostCenterList_' + i + '__CostCenterCode', 'costval_' + i)
        flag = 1;
    }
    }
    if (flag == 0) {
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
            url: '/DataBank/EmployeeVsCostCenter/Create',
            dataType: 'json',
            data: { CostCode: costCode, EmployeeGI: GI },
            success: function (result) {
                if (result.success == true) {
                    swal.fire("Updated Successfully").then(function () {
                        CloseSlider('cost-save');
                        fillData();
                    });

                }

                else {
                    swal.fire(result.message).then(function () {

                    });
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

    debugger;
    var Assignorunassign = $('input[id="AssignOrUnassign"]:checked').val();
    var tableLength = $('#CostCenterTablist_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/EmployeeVsCostCenter/ExportToExcel?TabName=" + Assignorunassign + "&totRecords=" + tableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}