
var prevTab = 'CheckupNotdone';
var currentTab = 'Not_Done';


function previewDocument(absoluteURI) {
    debugger;
    if (absoluteURI != '' && absoluteURI != null)
        window.open(absoluteURI, "_blank");
    else
        swal.fire("No file uploaded");
}


function fillData() {
    var tabIndex = currentTab;
    if (tabIndex =='Not_Done') {

        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var Type = $('#CheckupType_Code').val();
        /*var tabIndex = $('#Tabindex').val();*/
        var tabIndex = currentTab;
        var Type = $('#CheckupType_Code').val();

        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#NotDoneTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/MedicalCheckup/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + tabIndex + '&Type=' + Type + '&dummy=dummy';
                        d.filter = temp;
                        d.Type = Type;
                        /* d.tabIndex = TabIndex;*/

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    $('#MedicalCheckupList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        var abcd = d.data[i].employeeGI;
                        var ddd = abcd.toString();                      
                        d.data[i].employeeName = '<a onclick="onCheckupNotDoneEmpClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].medicalCheckupGI + "'" + ')">' + d.data[i].employeeName + '</a>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","width":"10%" },
                { "data": "employeeName", "width": "25%" },
                { "data": "designation", "width": "22%" },
                { "data": "department", "width": "22%" },
                { "data": "location", "width": "21%"},



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
    else {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var Type = $('#CheckupType_Code').val();
        /*var tabIndex = $('#Tabindex').val();*/
        var tabIndex = currentTab;
        var Type = $('#CheckupType_Code').val();

        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#DoneTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/MedicalCheckup/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + tabIndex + '&Type=' + Type + '&dummy=dummy';
                        d.filter = temp;
                        d.Type = Type;
                        /* d.tabIndex = TabIndex;*/

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    $('#MedicalCheckupList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        var abcd = d.data[i].employeeGI;
                        var ddd = abcd.toString();
                        d.data[i].validity = _GetFormattedDate(d.data[i].validity);
                        d.data[i].employeeName = '<a onclick="onCheckUpDoneEmpClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].medicalCheckupGI + "'" + ')">' + d.data[i].employeeName + '</a>'
                        d.data[i].dateOfCheckup = _GetFormattedDate(d.data[i].dateOfCheckup);

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "15%"},
                { "data": "designation", "width": "11%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "11%"},
                { "data": "dateOfCheckup", "width": "11%" },
                { "data": "checkupType", "width": "11%" },
             
                { "data": "validity", "width": "10%" },
                { "data": "healthCardNo", "width": "10%" },



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





}









function Preview() {
    debugger
    var path = URL.createObjectURL($('.previewImage')[0].files[0]);
    window.open(path, "_blank");
}


function OnTabClick(Tab, TabIndex) {
    debugger;
    //$.ajaxSetup({
    //    async: false
    //});
    showSpinner();
    currentTab = TabIndex;
    var Type = $('#CheckupType_Code').val();
    
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
  
   
    $.ajax({

        type: 'GET',
        url: '/DataBank/MedicalCheckup/GetMedicalCheckupList',
        dataType: 'html',
        data: { Type: Type, TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#MedicalCheckupListID').html("");
                $('#MedicalCheckupListID').html(result);
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
   
    prevTab = Tab;
}


function GetList() {
    debugger;
   
    var Type = $('#CheckupType_Code').val();
   
    if (Type !== '')
    {
       
        $.ajax({

            type: 'GET',
            url: '/DataBank/MedicalCheckup/GetMedicalCheckupList',
            dataType: 'html',
            data: { Type: Type, TabIndex: currentTab },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#MedicalCheckupListID').html("");
                    $('#MedicalCheckupListID').html(result);
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

        /*$('body').append('<div class="form-overlay"></div>');*/
    }
    else {
        return false;

    }
}


function onCheckupNotDoneEmpClick(EmpGI,MedicalGI) {
    debugger;
    showSpinner();
    var Type = $('#CheckupType_Code').val();
    var TypeName = $('#CheckupType_Code option:selected').text()
    //var url = "/DataBank/MedicalCheckup/Create?Type=" + Type + "&EmpGI=" + EmpGI + "&MedicalGI=" + MedicalGI;
    //$('#AddNewSlider').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/MedicalCheckup/Create',
        dataType: 'html',
        data: { Type: Type, TypeName: TypeName, EmpGI: EmpGI, MedicalGI: MedicalGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AddNewSlider').html("");
                $('#AddNewSlider').html(result);
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

    $('body').append('<div class="form-overlay"></div>');
   /* window.location.href = url;*/
    
}

function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    console.log(size);
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }

}



function validateCheckup() {
    debugger;
    var i = 0;
    if (!validateById('#CheckupDate', 'dateCheckup')) {
        i++;
    }
    if (!validateById('#HospitalDetails', 'Details')) {
        i++;
    }
    if (!validateById('#CheckupValidity', 'Validity')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function MedicalCheckUpSave() {
    debugger;

    if (validateCheckup()) {




        var TabIndex;
        var Type = $('#CheckupType_Code').val();

        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger;
                var medicalCheckupData = new FormData($('#Medical_CheckUpSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/MedicalCheckup/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: medicalCheckupData,
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNew');
                                //var url = "/DataBank/MedicalCheckup/GetMedicalCheckupList?Type=" + Type + "&TabIndex=" + 'Done';;
                                //$('#MedicalCheckupListID').load(url, { TabIndex: TabIndex }, function () { });
                                fillData();

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
    else {
        return false;
    }
}

function onCheckUpDoneEmpClick(EmpGI,MedicalGI) {
    debugger;
    
    var Type = $('#CheckupType_Code').val();
    //var url = "/DataBank/MedicalCheckup/Delete?Type=" + Type + "&EmpGI=" + EmpGI + "&MedicalGI=" + MedicalGI;
    //$('#DeleteSlider').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/MedicalCheckup/Delete',
        dataType: 'html',
        data: { Type: Type, EmpGI: EmpGI, MedicalGI: MedicalGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DeleteSlider').html("");
                $('#DeleteSlider').html(result);
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
    $('body').append('<div class="form-overlay"></div>');
   
}


function confirmDelete(MedicalGI) {
    debugger;
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            url = "/DataBank/MedicalCheckup/DeleteMedicalCheckUp?MedicalGI=" + MedicalGI;
            window.location.href = url;
          
        }
        else {
            return false;
        }
    })
}


function ExportToExcel() {

    debugger;
    var Type = $('#CheckupType_Code').val();
    var MedType = $('#CheckupType_Code option:selected').text();

    if (currentTab == 'Done') {
        var tableLength = $('#MedicalCheckupList_Count').val();
        if (tableLength > 0) {
            url = "/DataBank/MedicalCheckup/ExportToExcel?TabIndex=" + currentTab + "&Type=" + Type + "&MedType=" + MedType +"&totRecords=" + tableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

    else {
        var tableLength = $('#MedicalCheckupList_Count').val();
        if (tableLength > 0) {
            url = "/DataBank/MedicalCheckup/ExportToExcel?TabIndex=" + currentTab + "&Type=" + Type + "&MedType=" + MedType + "&totRecords=" + tableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}




function ApplyFilter(mode) {
    debugger

    $("#SearchId").val($('#FilterList_Code').val())

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*   showSpinner();*/
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
       /* CloseSlider('basicFilter');*/
    }
}