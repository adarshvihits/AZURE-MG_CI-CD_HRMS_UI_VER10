
$(document).ready(function () {
    IsHandicaped();
    CriticalAilments();
    DisabilityNature();
})
function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var AssessmentYear = $('#AssessmentYear_Code').val();

    var dataTable = $("#TDSEmployeeListtable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/InvestmentDeclaration/GetTDSEmployeeList",
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

                $('#TDSEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="CreateTdsDeclaration(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'
                    if (d.data[i].rentalCount != true) {
                        d.data[i].rentalCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].rentalCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                    if (d.data[i].dedCount != true) {
                        d.data[i].dedCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].dedCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                    if (d.data[i].chapterCount != true) {
                        d.data[i].chapterCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].chapterCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                    //if (d.data[i].donCount != true) {
                    //    d.data[i].donCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].donCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    if (d.data[i].repayCount != true) {
                        d.data[i].repayCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].repayCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                    if (d.data[i].letoutCount != true) {
                        d.data[i].letoutCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].letoutCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                    if (d.data[i].otherCount != true) {
                        d.data[i].otherCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].otherCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                }

                return d.data;
                debugger;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId", "width": "10%" },
            { "data": "employeeHeader.employeeName", "width": "15%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "rentalCount", "width": "5%" },
            { "data": "dedCount", "width": "5%" },
            { "data": "chapterCount", "width": "5%" },
          /*  { "data": "donCount", "width": "5%" },*/
            { "data": "repayCount", "width": "5%" },
            { "data": "letoutCount", "width": "5%" },
            { "data": "otherCount", "width": "5%" },

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
        //dataTable.search('').draw();
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
    //}
    //else {
    //    return false;
    //}
}


function validateDropdowns() {
    if (validateById('#AssessmentYear_Code', 'Assessmnt_year')) {
        return true;
    }
    else {
        return false;
    }
}
function ApplyFilter(mode) {
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
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
function CreateTdsDeclaration(EmployeeGI) {
    
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var url = "/TDS/InvestmentDeclaration/Create?EmployeeGI=" + EmployeeGI + "&AssessmentYear=" + AssessmentYear ;




    console.log(url);
    window.location.href = url;

}
//----------------------RENTAL & LANDLORD---------------------

function HouseRentalSave() {
    
    //var countt = 0;
    //countt = $('#tbllandlord tr');
    var TotalRent = 0;
    for (let j = 0; j < 12; j++) {


       var Amount= $("#HouseRentalData_" + j + "__Amount").val();
        TotalRent += parseFloat(Amount);
    }

    var i = 0;

    if (TotalRent == "0") {
        swal.fire("Add House Rental Details.");
        i++;
        return false;
    }
    $('#tbllandlord tr').each(function () {
        if ($.trim($(this).text()) == 'No table data') {
            swal.fire("Add Landlord Details.");
            i++;
            return false;
        }
    
    });
    
    if (i > 0) {
        return false;
    }
    else {
       
     
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                  
                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/HouseRentalSave',
                    dataType: 'json',
                    data: $('#HouseRental').serialize(),
                    success: function (result) {
                     
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                              
                                $('.tabOne').addClass('active');
                                $('#tabOne').addClass('active');
                                $('#tabOne').css('display', 'block');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclaration/GetRentalDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabOne').html("");
                                            $('#tabOne').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
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
        });

    }
}
function onCityChange(sender, id) {
    var citycode = $("#HouseRentalData_" + id + "__MetroCity_Code option:selected").val();
    
    if ($("#HouseRentalData_" + id + "__MetroCity_Code option:selected").text() == "Others") {
        for (let i = id; i < 12; i++) {

            $("#HouseRentalData_" + i + "__MetroCity_Code").val(citycode);
                   
            $("#HouseRentalData_" + i + "__MetroNonMetro").val('NonMetro');
            $("#HouseRentalData_" + i + "__Amount").val();
            $("#HouseRentalData_" + i + "__MetroNonMetro").attr("readonly", true);
            $("#HouseRentalData_" + i + "__OtherCityName").attr("disabled", false);
        }
    }
    else {
        for (let j = id; j < 12; j++) {
            $("#HouseRentalData_" + j + "__MetroCity_Code").val(citycode);
           
            $("#HouseRentalData_" + j + "__OtherCityName").val('');
            $("#HouseRentalData_" + j + "__MetroNonMetro").val('Metro');
            $("#HouseRentalData_" + j + "__Amount").val(0);
            $("#HouseRentalData_" + j + "__MetroNonMetro").attr("readonly", false);
            $("#HouseRentalData_" + j + "__OtherCityName").attr("disabled", true);
        }
      
    }
    validateCity(id);

}
function onOtherCityChange(sender, id) {

    var othercity = $("#HouseRentalData_" + id + "__OtherCityName").val();
        for (let i = id + 1; i < 12; i++) {

            if ($("#HouseRentalData_" + i + "__MetroCity_Code option:selected").text() == "Others") {
                $("#HouseRentalData_" + i + "__OtherCityName").val(othercity);
            }
        }
    }
function onRentAmountChange(sender, id) {
    var Amount = $("#HouseRentalData_" + id + "__Amount").val();
    for (let i = id + 1; i < 12; i++) {

       
        $("#HouseRentalData_" + i + "__Amount").val(Amount);
      
    }
}
function LandlordCreate(Mode, rowid) {


    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var Name = $('#LandlordList_' + rowid + '__Name').val();
    var Address = $('#LandlordList_' + rowid + '__Address').val();
    var From = $('#LandlordList_' + rowid + '__From_Name').val();
    var FromCode = $('#LandlordList_' + rowid + '__From_Code').val();
    var To = $('#LandlordList_' + rowid + '__To_Name').val();
    var ToCode = $('#LandlordList_' + rowid + '__To_Code').val();
    var PAN = $('#LandlordList_' + rowid + '__Pan').val();
    
    $.ajax({
        type: 'GET',
        url: '/TDS/InvestmentDeclaration/LandLordCreate',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear, Mode: Mode, Rowid: rowid, Name: Name, Address: Address, From: From, FromCode: FromCode, To: To, ToCode: ToCode, PAN: PAN},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#LandlordAdd').html("");
                $('#LandlordAdd').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    debugger;
}
function LandlordAddtoGrid(Mode, rowid) {
    var TotalRent = 0;
    var EMPPANNO;
    var Name = $('#LandlordDetails_Name').val();
    var Address = $('#LandlordDetails_Address').val();
    var From = $('#LandlordDetails_From_Code option:selected').text();
    var FromCode = $('#LandlordDetails_From_Code option:selected').val();
    var To = $('#LandlordDetails_To_Code option:selected').text();
    var ToCode = $('#LandlordDetails_To_Code option:selected').val();
    var PAN = $('#LandlordDetails_Pan').val();
    var EmployeeGI = $('#EmployeeGuid').val();


    var i = 0;
    if (!validateById('#LandlordDetails_Name', 'landlordname')) {
        i++;
    }
    if (!validateById('#LandlordDetails_Address', 'address')) {
        i++;
    }
    if (!validateById('#LandlordDetails_From_Code', 'from')) {
        i++;
    }
    if (!validateById('#LandlordDetails_To_Code', 'to')) {
        i++;
    }
 
    for (let j = 0; j < 12; j++) {
        var Amount = $("#HouseRentalData_" + j + "__Amount").val();
        TotalRent += parseFloat(Amount);
    }
    if (TotalRent > 100000) {
        if (!validateById('#LandlordDetails_Pan', 'pan')) {
            i++;
        }
    }
    if (PAN != "") {
        $.ajax({
            url: '/TDS/InvestmentDeclaration/GetEmployeePANNumber',
            async: false,
            dataType: 'json',
            data: {
                EmployeeGI: EmployeeGI
            },
            success: function (data) {
                EMPPANNO = data;
            }
        });
        if (!validateEmpPan('#LandlordDetails_Pan', EMPPANNO, 'pan')) {
            i++;
        }
    }
  
    if (i > 0)
    {
        return false;
    }
    else {
        $('#tbllandlord tr').each(function () {
          
            if ($.trim($(this).text())=='No table data')
                $(this).remove();
        });


        var count = $('#tbllandlord tr').length;

       
     
       
        var NewRow = "<tr id='row_OI_" + (count - 1) + "' ><td>" + count + "</td><td><input id='LandlordList_" + (count - 1) + "__Name'  name = 'LandlordList[" + (count - 1) + "].Name' type='text' class = 'form-control' value= '" + Name + "'></td><td><input id='LandlordList_" + (count - 1) + "__Address' name = 'LandlordList[" + (count - 1) + "].Address' type='text' class = 'form-control' value=" + Address + "></td><td><input id='LandlordList_" + (count - 1) + "__From_Code' name = 'LandlordList[" + (count - 1) + "].From.Code' type='hidden' class = 'form-control' value=" + FromCode + "><input id='LandlordList_" + (count - 1) + "__From_Name' name = 'LandlordList[" + (count - 1) + "].From' type='text' class = 'form-control' value=" + From + "></td><td><input id='LandlordList_" + (count - 1) + "__To_Code' name = 'LandlordList[" + (count - 1) + "].To.Code' type='hidden' class = 'form-control' value=" + ToCode + "><input id='LandlordList_" + (count - 1) + "__To_Name' name = 'LandlordList[" + (count - 1) + "].To' type='text' class = 'form-control' value=" + To + "></td><td><input id='LandlordList_" + (count - 1) + "__Pan' name = 'LandlordList[" + (count - 1) + "].PAN' type='text' class = 'form-control' value=" + PAN + "></td><td><a type='button' data-bs-toggle='tooltip' title='Edit' name='record' class='btn btn-icon btn-edit icon-only'><i class='icon-xl fas fa-pencil-alt' onclick='LandlordCreate(\"Modify\"," + (count - 1) + ")' ></i><a type='button' data-bs-toggle='tooltip' title='Cancel' name='record' class='btn btn-icon btn-delete icon-only'><i class='icon-xl far fa-trash-alt' onclick='onCancelClick("+ (count - 1) +")' ></i></td></tr>";

        if (Mode == 'Save') {
            $("#tbllandlord").append(NewRow);
            $('#LandlordList_' + (count - 1) + '__Name').attr("readonly", true);
            $('#LandlordList_' + (count - 1) + '__Address').attr("readonly", true);
            $('#LandlordList_' + (count - 1) + '__From_Name').attr("readonly", true);
            $('#LandlordList_' + (count - 1) + '__To_Name').attr("readonly", true);
            $('#LandlordList_' + (count - 1) + '__Pan').attr("readonly", true);
        
          
        }
          
        else {
       
            $('#LandlordList_' + rowid + '__Name').val(Name);
            $('#LandlordList_' + rowid + '__Address').val(Address);
            $('#LandlordList_' + rowid + '__From_Name').val(From);
            $('#LandlordList_' + rowid + '__From_Code').val(FromCode);
            $('#LandlordList_' + rowid + '__To_Name').val(To);
            $('#LandlordList_' + rowid + '__To_Code').val(ToCode);
            $('#LandlordList_' + rowid + '__Pan').val(PAN);
            }
           
            
        CloseSlider('land');


    }
}
function validateEmpPan(elementId,pan, spanId) {
    debugger;
    if ($(elementId).val() == pan) {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("Please Enter LandLord PAN Number, Not Employee PAN Number...");
        return false;
      
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        return true;
     
    }
}
function validateCity(index) {
  
    if (parseInt($('#HouseRentalData_' + index + '__Amount').val()) > 0 && $('#HouseRentalData_' + index + '__MetroCity_Code  option:selected').val()=="") {
        $('#HouseRentalData_' + index + '__MetroCity_Code').addClass('is-invalid');
        $("#city_" + index).addClass('error-message');
        $("#city_" + index).html("Select City");
        return false;
    }
    else {
        $('#HouseRentalData_' + index + '__MetroCity_Code').removeClass('is-invalid');
        $("#city_" + index).removeClass('error-message');
        $("#city_" + index).html("");
        return true;
    }
    
}
function onCancelClick(index) {
    $("#tbllandlord").find('[id=LandlordList_' + (index) + '__Name]').each(function () {
        $(this).parents("tr").remove();
        if ($('#tbllandlord tr').length == 1)
        var NoDataRow = "<tr><td colspan='7' align='center'><em>No table data</em></td></tr>";
        $("#tbllandlord").append(NoDataRow);
    });
}
function LandlordDelete(TrnCode, rowid) {
    var TotalRent = 0;
    var Total = 0;
    var table = document.getElementById("tbllandlord");
    var totalRowCount = table.rows.length -1 ;


    for (let k = 0; k <= totalRowCount; k++) {
        var trnCode = $("#LandlordList_" + k + "__TrnCode").val();
       
        if (trnCode > 0) {
            Total = parseFloat(Total)+1;
          
        }
            
  
    }



    for (let j = 0; j < 12; j++) {
        var Amount = $("#HouseRentalData_" + j + "__Amount").val();
        TotalRent += parseFloat(Amount);
    }

    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var i = 0;

    if (Total==1 && TotalRent >0) {
        swal.fire("Atleast one Landlord details is required when declaring House Rental");
        i++;
        return false;
    }
   

    if (i > 0) {
        return false;
    }
    else {


        swal.fire({
            text: "Do you want to Delete?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                //    // Save it

                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/DeleteLandLordDetails',
                    dataType: 'json',
                    data: { TrnCode: TrnCode, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear, TotalRent: TotalRent },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabOne').addClass('active');
                                $('#tabOne').addClass('active');
                                $('#tabOne').css('display', 'block');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclaration/GetRentalDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: AssessmentYear },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabOne').html("");
                                            $('#tabOne').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

                            });
                        }

                        else {
                            swal.fire("Something went wrong.").then(function () {


                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })
    }
}

//---------------------80C DEDUCTION------------------
function DeductionSave() {
    debugger;
    
    var i = 0;
  
    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/DeductionSave',
                    dataType: 'json',
                    data: $('#deduction').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabOne').removeClass('active');
                                $('#tabOne').removeClass('active');
                                $('#tabOne').css('display', 'none');
                                $('.tabTwo').addClass('active');
                                $('#tabTwo').addClass('active');
                                $('#tabTwo').css('display', 'block');

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
        });

    }
}

//---------------------TAB-INVESTMENT UNDER 80 C------------------
function LICCreate(TrnCode) {

    debugger;
    var EmployeeGI = $('#EmployeeGuid').val();
    $.ajax({
        type: 'GET',
        url: '/TDS/InvestmentDeclaration/LICCreate',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, TrnCode: TrnCode, AssessmentYear: $('#AssessmentYear_Code').val()},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#LICSave').html("");
                $('#LICSave').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    debugger;
}

function LICSave(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;

    if (!validateById('#LICDetails_Name', 'licname')) {
        i++;
    }
    if (!validateById('#LICDetails_RelationShip_Code', 'relation')) {
        i++;
    }
    if (!validateById('#LICDetails_PolicyTakenDate', 'policydate')) {
        i++;
    }
    if (!validateById('#LICDetails_PremiumAmount', 'premium')) {
        i++;
    }
    if (!validateById('#LICDetails_SumAssured', 'sumassured')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    
                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/LICSave',
                    dataType: 'json',
                    data: { TrnCode: $('#LICDetails_TrnCode').val(), Name: $('#LICDetails_Name').val(), RelationShip: $('#LICDetails_RelationShip_Code').val(), PolicyTakenDate: $('#LICDetails_PolicyTakenDate').val(), PolicyNo: $('#LICDetails_PolicyNo').val(), PremiumAmount: $('#LICDetails_PremiumAmount').val(), SumAssured: $('#LICDetails_SumAssured').val(), Remarks: $('#LICDetails_Remarks').val(), TabIndex: "LIC", AssessmentYear: $('#AssessmentYear_Code').val() , EmpGI: $('#EmployeeGuid').val() },
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('lic');

                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclaration/GetLICList',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabTwo').html("");
                                            $('#tabTwo').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

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
        });

    }
}

function LICDelete(TrnCode) {

    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
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
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/TDS/InvestmentDeclaration/DeleteLICDetails',
                dataType: 'json',
                data: { TrnCode: TrnCode, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');
                            $.ajax({

                                type: 'GET',
                                url: '/TDS/InvestmentDeclaration/GetLICList',
                                dataType: 'html',
                                data: { EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear },
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabTwo').html("");
                                        $('#tabTwo').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });

                        });
                    }

                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}

//----------------------CHAPTERVIA
function ChapterVIASave() {
    debugger;
  
    var i = 0;

    if ($('#ChapterVIADetails_IsDepndHandicaped_Code option:selected').text() == "Yes") {
       
        if ($('#ChapterVIADetails_DisabilitySevere_Code option:selected').val() == 0) {
           
            $('#ChapterVIADetails_DisabilitySevere_Code').addClass('is-invalid');
            $("#DisabilitySevere").addClass('error-message');
            $("#DisabilitySevere").html("Select Nature of Disability Severe.");
            i++;
        }
        if ($('#ChapterVIADetails_DependentRelation_Code option:selected').val() == 0) {

            $('#ChapterVIADetails_DependentRelation_Code').addClass('is-invalid');
            $("#DependentRelation").addClass('error-message');
            $("#DependentRelation").html("Select Dependent Relationship.");
            i++;
        }
       
    }
    if ($('#ChapterVIADetails_InterestPaid').val() >0) {

        if ($('#ChapterVIADetails_HouseLoanDate').val() == "") {

            $('#ChapterVIADetails_HouseLoanDate').addClass('is-invalid');
            $("#HouseLoanDate").addClass('error-message');
            $("#HouseLoanDate").html("Select Date of Availing Loan.");
            i++;
        }
        if ($('#ChapterVIADetails_HouseLoanAmt').val() == 0) {

            $('#ChapterVIADetails_HouseLoanAmt').addClass('is-invalid');
            $("#HouseLoanAmt").addClass('error-message');
            $("#HouseLoanAmt").html("Enter Loan Amount.");
            i++;
        }
        if ($('#ChapterVIADetails_ValueofHouse').val() == 0) {

            $('#ChapterVIADetails_ValueofHouse').addClass('is-invalid');
            $("#ValueofHouse").addClass('error-message');
            $("#ValueofHouse").html("Enter Value of House.");
            i++;
        }
        if ($('#ChapterVIADetails_IsOtherHouse_Code option:selected').val() == 0) {
            $('#ChapterVIADetails_IsOtherHouse_Code').addClass('is-invalid');
            $("#IsOtherHouse").addClass('error-message');
            $("#IsOtherHouse").html("Select Yes or No.");
            i++;
        }

    }

    if ($('#ChapterVIADetails_EEAInterestPaid').val() > 0) {

        if ($('#ChapterVIADetails_EEAHouseLoanDate').val() == "") {

            $('#ChapterVIADetails_EEAHouseLoanDate').addClass('is-invalid');
            $("#EEAHouseLoanDate").addClass('error-message');
            $("#EEAHouseLoanDate").html("Select Date of Availing Loan.");
            i++;
        }
        if ($('#ChapterVIADetails_EEAHouseLoanAmt').val() == 0) {

            $('#ChapterVIADetails_EEAHouseLoanAmt').addClass('is-invalid');
            $("#EEAHouseLoanAmt").addClass('error-message');
            $("#EEAHouseLoanAmt").html("Enter Loan Amount.");
            i++;
        }
        if ($('#ChapterVIADetails_EEAValueofHouse').val() == 0) {

            $('#ChapterVIADetails_EEAValueofHouse').addClass('is-invalid');
            $("#EEAValueofHouse").addClass('error-message');
            $("#EEAValueofHouse").html("Enter Value of House.");
            i++;
        }
        if ($('#ChapterVIADetails_IsEEAOtherHouse_Code option:selected').val() == 0) {
            $('#ChapterVIADetails_IsEEAOtherHouse_Code').addClass('is-invalid');
            $("#IsEEAOtherHouse").addClass('error-message');
            $("#IsEEAOtherHouse").html("Select Yes or No");
            i++;
        }

    }

    if ($('#ChapterVIADetails_ECInterest').val() > 0) {

        if ($('#ChapterVIADetails_ECPurchaseDate').val() == "") {

            $('#ChapterVIADetails_ECPurchaseDate').addClass('is-invalid');
            $("#ECPurchaseDate").addClass('error-message');
            $("#ECPurchaseDate").html("Select Date of Purchase.");
            i++;
        }
        if ($('#ChapterVIADetails_ECLoanAmount').val() == 0) {

            $('#ChapterVIADetails_ECLoanAmount').addClass('is-invalid');
            $("#ECLoanAmount").addClass('error-message');
            $("#ECLoanAmount").html("Enter Loan Amount.");
            i++;
        }
        if ($('#ChapterVIADetails_ECBank').val() == "") {

            $('#ChapterVIADetails_ECBank').addClass('is-invalid');
            $("#ECBank").addClass('error-message');
            $("#ECBank").html("Enter Name of Bank.");
            i++;
        }
        

    }
    if ($('#ChapterVIADetails_CriticalAilments_Code option:selected').val() >=1) {
        
        if ($('#ChapterVIADetails_RelationShip_Code option:selected').val() == 0) {

            $('#ChapterVIADetails_RelationShip_Code').addClass('is-invalid');
            $("#RelationShip").addClass('error-message');
            $("#RelationShip").html("Select Relationship.");
            i++;
        }
        if ($('#ChapterVIADetails_PatientAge').val() == 0) {

            $('#ChapterVIADetails_PatientAge').addClass('is-invalid');
            $("#patientage").addClass('error-message');
            $("#patientage").html("Enter Patient Age.");
            i++;
        }
        if ($('#ChapterVIADetails_DepndExpenditure').val() == 0) {

            $('#ChapterVIADetails_DepndExpenditure').addClass('is-invalid');
            $("#expenditure").addClass('error-message');
            $("#expenditure").html("Enter Expenditure.");
            i++;
        }
        

    }
    
    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/ChapterVIASave',
                    dataType: 'json',
                    data: $('#ChapterVIA').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                //$('.tabOne').removeClass('active');
                                //$('#tabOne').removeClass('active');
                                //$('#tabOne').css('display', 'none');
                                $('.tabThree').addClass('active');
                                $('#tabThree').addClass('active');
                                $('#tabThree').css('display', 'block');

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
        });

    }
}

function PPSChange() {
    var PPSAmt = parseFloat($('#ChapterVIADetails_PPSAmt').val());
    if (isNaN(PPSAmt) || PPSAmt == '' || PPSAmt == null) {
        PPSAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_PPSEAmt').val(PPSAmt);
}
function NPSChange() {
    var NPSAmt = parseFloat($('#ChapterVIADetails_NPSAmt').val());
    if (isNaN(NPSAmt) || NPSAmt == '' || NPSAmt == null) {
        NPSAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_NPSEAmt').val(NPSAmt);
}
function MedExpChange() {
    var MExpAmt = parseFloat($('#ChapterVIADetails_MedExpSrCtznAmt').val());
    if (isNaN(MExpAmt) || MExpAmt == '' || MExpAmt == null) {
        MExpAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_MedExpSrCtznEAmt').val(MExpAmt);
}
function IsHandicaped() {
    if ($('#ChapterVIADetails_IsDepndHandicaped_Code option:selected').text() == "Yes") {
        $('#ChapterVIADetails_DisabilitySevere_Code').attr("disabled", false);
        $('#ChapterVIADetails_DependentRelation_Code').attr("disabled", false);

    }
    else {
        $('#ChapterVIADetails_DisabilitySevere_Code').val('');
        $('#ChapterVIADetails_DependentRelation_Code').val('');
        $('#ChapterVIADetails_DisabilitySevere_Code').attr("disabled", true);
        $('#ChapterVIADetails_DependentRelation_Code').attr("disabled", true);

    }
}
function CriticalAilments() {

    if ($('#ChapterVIADetails_CriticalAilments_Code').val() >=1)
   {
        $('#ChapterVIADetails_RelationShip_Code').attr("disabled", false);
        $('#ChapterVIADetails_PatientAge').attr("disabled", false);
        $('#ChapterVIADetails_DepndExpenditure').attr("disabled", false);

    }
    else {
        $('#ChapterVIADetails_RelationShip_Code').val('');
        $('#ChapterVIADetails_PatientAge').val('0');
        $('#ChapterVIADetails_DepndExpenditure').val('0.0');
        $('#ChapterVIADetails_RelationShip_Code').attr("disabled", true);
        $('#ChapterVIADetails_PatientAge').attr("disabled", true);
        $('#ChapterVIADetails_DepndExpenditure').attr("disabled", true);

    }
   
}
function EducationLoan() {
    var interest = parseFloat($('#ChapterVIADetails_EduLoanInterest').val());
    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_EduLoanInterestEAmt').val(interest);
}
function HouseLoan()
{
    var InterestPaid = parseFloat($('#ChapterVIADetails_InterestPaid').val());
    if (isNaN(InterestPaid) || InterestPaid == '' || InterestPaid == null) {
        InterestPaid = parseFloat("0.00");
    }
    $('#ChapterVIADetails_InterestEAmt').val(InterestPaid);
}
function EEAHouseLoan() {
    var EEAInterestPaid = parseFloat($('#ChapterVIADetails_EEAInterestPaid').val());
    if (isNaN(EEAInterestPaid) || EEAInterestPaid == '' || EEAInterestPaid == null) {
        EEAInterestPaid = parseFloat("0.00");
    }
    $('#ChapterVIADetails_EEAInterestEAmt').val(EEAInterestPaid);
}
function ElectricCarLoan() {
    var ECInterest = parseFloat($('#ChapterVIADetails_ECInterest').val());
    if (isNaN(ECInterest) || ECInterest == '' || ECInterest == null) {
        ECInterest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_ECInterestEA').val(ECInterest);
}
function SBAccountInterest() {
    var SBAccInterest = parseFloat($('#ChapterVIADetails_SBAccInterest').val());
    if (isNaN(SBAccInterest) || SBAccInterest == '' || SBAccInterest == null) {
        SBAccInterest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_SBAccInterestEA').val(SBAccInterest);
}
function DisabilityNature() {

    if ($('#ChapterVIADetails_NatureofDisability_Code').val() >= 1) {
        $('#ChapterVIADetails_PercentageofDisability_Code').attr("disabled", false);
    }
    else {
        $('#ChapterVIADetails_PercentageofDisability_Code').val('');
        $('#ChapterVIADetails_PercentageofDisability_Code').attr("disabled", true);
    }

}
    

//---------------------80G DONATION------------------
function EightyGDonationSave() {
    debugger;
 
    var i = 0;

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/DonationSave',
                    dataType: 'json',
                    data: $('#donation').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabTwo').removeClass('active');
                                $('#tabTwo').removeClass('active');
                                $('#tabTwo').css('display', 'none');
                                $('.tabFour').addClass('active');
                                $('#tabFour').addClass('active');
                                $('#tabFour').css('display', 'block');

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
        });

    }
}

//---------------------HOUSE LOAN REPAYMENT------------------
function HLRepayCreate(TrnCode) {
   
    debugger;
    var EmployeeGI = $('#EmployeeGuid').val();
      $.ajax({
        type: 'GET',
        url: '/TDS/InvestmentDeclaration/HouseLoanRepayCreate',
        dataType: 'html',
          data: { EmployeeGI: EmployeeGI, TrnCode: TrnCode, AssessmentYear: $('#AssessmentYear_Code').val()},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#HLRepaySave').html("");
                $('#HLRepaySave').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    debugger;
}

function HouseLoanRepaySave(mode) {
        debugger;
        var msg;
        if (mode != "Modify") {
            msg = "Do you want to Save?";
        }
        else {
            msg = "Do you want to Modify?";
        }
        var i = 0;

    if (!validateById('#HLRepayDetails_HouseAddress', 'address')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_LocationStatus_Code', 'locationstatus')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_OwnerStatus_Code', 'ownerstatus')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_Percentage', 'percentage')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_Purpose_Code', 'purpose')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_DateofLoan', 'dateofloan')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_DateofCompletion', 'dateofcmptn')) {
        i++;
    }

    if (!validateById('#HLRepayDetails_PrincipalAmt', 'principleamt')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_Interest', 'interest')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_NameofBank', 'nameofbank')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_PANofBank', 'panofbank')) {
        i++;
    }
    if (!validateById('#HLRepayDetails_BankAddress', 'bankaddress')) {
        i++;
    }
    if ($('#HLRepayDetails_Percentage').val() == "0") {
        $('#HLRepayDetails_Percentage').addClass('is-invalid');
        $("#percentage").addClass('error-message');
        $("#percentage").html("Percentage should n't be less than Zero.");
        i++;
    }
    if (!validatePan('HLRepayDetails_PANofBank', 'panofbank')) {
        i++;
    }
        if (i > 0) {
            return false;
        }
        else {
            swal.fire({
                text: msg,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
      
                        type: 'POST',
                        url: '/TDS/InvestmentDeclaration/HouseLoanRepaySave',
                        dataType: 'json',
                        data: { TrnCode: $('#HLRepayDetails_TrnCode').val(), HAddress: $('#HLRepayDetails_HouseAddress').val(), LocStatus: $('#HLRepayDetails_LocationStatus_Code').val(), OwnerStatus: $('#HLRepayDetails_OwnerStatus_Code').val(), Percentage: $('#HLRepayDetails_Percentage').val(), Purpose: $('#HLRepayDetails_Purpose_Code').val(), LDate: $('#HLRepayDetails_DateofLoan').val(), CDate: $('#HLRepayDetails_DateofCompletion').val(), PrincipleAmt: $('#HLRepayDetails_PrincipalAmt').val(), Interest: $('#HLRepayDetails_Interest').val(), EMIInterest: $('#HLRepayDetails_PreEMIInterest').val(), TotalInterest: $('#HLRepayDetails_TotalInterest').val(), Bank: $('#HLRepayDetails_NameofBank').val(), PAN: $('#HLRepayDetails_PANofBank').val(), BankAddress: $('#HLRepayDetails_BankAddress').val(), Remarks: $('#HLRepayDetails_Remarks').val(), TabIndex: "House Loan Repayment", AssessmentYear: $('#AssessmentYear_Code').val(),  EmpGI: $('#EmployeeGuid').val() },
                        success: function (result) {
                            debugger;
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    CloseSlider('repay');

                                    $.ajax({

                                        type: 'GET',
                                        url: '/TDS/InvestmentDeclaration/GetHouseLoanRepayList',
                                        dataType: 'html',
                                        data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                        success: function (result) {
                                            debugger
                                            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                                window.location.reload();
                                            }
                                            else {

                                                $('#tabFive').html("");
                                                $('#tabFive').html(result);
                                            }


                                        },


                                        error: function (xhr, textStatus, errorThrown) {
                                            debugger
                                            document.body.id = "kt_body";
                                            $('body').html(xhr.responseText);


                                        }
                                    });

                                });

                            }

                            //else {
                            //    if (result.message == "FATHER_DUPLICATION_ERROR" || result.message == "MOTHER_DUPLICATION_ERROR" || result.message == "SPOUSE_DUPLICATION_ERROR") {
                            //        swal.fire("Selected relation already exists");
                            //    }
                            //    else {
                            //        swal.fire(result.message);
                            //    }
                            //}

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
            });

        }
}

function DeleteHouseLoanRepay(TrnCode) {

    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
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
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/TDS/InvestmentDeclaration/DeleteHouseLoanRepayDetails',
                dataType: 'json',
                data: { TrnCode: TrnCode, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');
                            $.ajax({

                                type: 'GET',
                                url: '/TDS/InvestmentDeclaration/GetHouseLoanRepayList' ,
                                dataType: 'html',
                                data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: AssessmentYear },

                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabFive').html("");
                                        $('#tabFive').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });

                        });
                    }

                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}
function HLOwnerStausChange(sender, id) {

    debugger;

    if ($('#HLRepayDetails_OwnerStatus_Code option:selected').text()=="Self")
    {
        $('#HLRepayDetails_Percentage').val(100);
        $('#HLRepayDetails_Percentage').attr("disabled", true);
    }
    else {
        $('#HLRepayDetails_Percentage').val(0);
        $('#HLRepayDetails_Percentage').attr("disabled", false);
    }
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    if ($('#HLRepayDetails_Percentage').val() == "") {
        $('#HLRepayDetails_Percentage').addClass('is-invalid');
        $('#percentage').addClass('error-message');
        $('#percentage').html("This field is required");
    }
    else {
        $('#HLRepayDetails_Percentage').removeClass('is-invalid');
        $('#percentage').removeClass('error-message');
        $('#percentage').html("");
    }

}
function HLInterestChange(sender, id) {
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    var interest = parseFloat($('#HLRepayDetails_Interest').val());
    var emiinterest = parseFloat($('#HLRepayDetails_PreEMIInterest').val());

    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }

    if (isNaN(emiinterest) || emiinterest == '' || emiinterest == null) {
        emiinterest = parseFloat("0.00");
    }

    var totalInterest = (interest + emiinterest);
    $('#HLRepayDetails_TotalInterest').val(totalInterest);

}
//---------------------LET OUT HOUSE------------------
function LetOutHouseCreate(TrnCode) {

    debugger;
    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
   
    $.ajax({
        type: 'GET',
        url: '/TDS/InvestmentDeclaration/LetOutHouseCreate',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear ,TrnCode: TrnCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#LetOutHouseSave').html("");
                $('#LetOutHouseSave').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    debugger;
}

function LetOutHouseSave(mode) {
    debugger;
    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;

    if (!validateById('#LetOutDetails_HouseAddress', 'address')) {
        i++;
    }
    if (!validateById('#LetOutDetails_OwnerStatus_Code', 'ownerstatus')) {
        i++;
    }
    if (!validateById('#LetOutDetails_Percentage', 'percentage')) {
        i++;
    }
    if (!validateById('#LetOutDetails_Purpose_Code', 'purpose')) {
        i++;
    }
    if (!validateById('#LetOutDetails_DateofLoan', 'dateofloan')) {
        i++;
    }
    if (!validateById('#LetOutDetails_DateofCompletion', 'dateofcmptn')) {
        i++;
    }
    if (!validateById('#LetOutDetails_DateofLetOut', 'dateofLetOut')) {
        i++;
    }
    if (!validateById('#LetOutDetails_RentalIncome', 'rentalincme')) {
        i++;
    }
    if (!validateById('#LetOutDetails_PrincipalAmt', 'principleamt')) {
        i++;
    }
    if (!validateById('#LetOutDetails_Interest', 'interest')) {
        i++;
    }
    if (!validateById('#LetOutDetails_NameofBank', 'nameofbank')) {
        i++;
    }
    if (!validateById('#LetOutDetails_PANofBank', 'panofbank')) {
        i++;
    }
    if (!validateById('#LetOutDetails_BankAddress', 'bankaddress')) {
        i++;
    } 
    if ($('#LetOutDetails_Percentage').val() == "0") {
        $('#LetOutDetails_Percentage').addClass('is-invalid');
        $("#percentage").addClass('error-message');
        $("#percentage").html("Percentage should n't be less than Zero.");
        i++;
    }
 
  
   
    if (!validatePan('LetOutDetails_PANofBank', 'panofbank')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/LetOutHouseSave',
                    dataType: 'json',
                    data: { TrnCode: $('#LetOutDetails_TrnCode').val(), HAddress: $('#LetOutDetails_HouseAddress').val(), OwnerStatus: $('#LetOutDetails_OwnerStatus_Code').val(), Percentage: $('#LetOutDetails_Percentage').val(), Purpose: $('#LetOutDetails_Purpose_Code').val(), LDate: $('#LetOutDetails_DateofLoan').val(), CDate: $('#LetOutDetails_DateofCompletion').val(), LetOutDate: $('#LetOutDetails_DateofLetOut').val(), RentalIncome: $('#LetOutDetails_RentalIncome').val(), MunicipalTax: $('#LetOutDetails_MunicipalTax').val(), PrincipleAmt: $('#LetOutDetails_PrincipalAmt').val(), Interest: $('#LetOutDetails_Interest').val(), EMIInterest: $('#LetOutDetails_PreEMIInterest').val(), TotalInterest: $('#LetOutDetails_TotalInterest').val(), Bank: $('#LetOutDetails_NameofBank').val(), PAN: $('#LetOutDetails_PANofBank').val(), BankAddress: $('#LetOutDetails_BankAddress').val(), Remarks: $('#LetOutDetails_Remarks').val(), TabIndex: "Let Out House", AssessmentYear: $('#AssessmentYear_Code').val(),  EmpGI: $('#EmployeeGuid').val() },
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('letouthouse');

                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclaration/GetLetOutHouseList',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val()},
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabSix').html("");
                                            $('#tabSix').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });

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
        });

    }
}
function DeleteLetOutDetails(TrnCode) {

    var EmployeeGI = $('#EmployeeGuid').val();
    var AssessmentYear = $('#AssessmentYear_Code').val();
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
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/TDS/InvestmentDeclaration/DeleteLetOutDetails',
                dataType: 'json',
                data: { TrnCode: TrnCode, EmployeeGI: EmployeeGI, AssessmentYear: AssessmentYear },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');
                            $.ajax({

                                type: 'GET',
                                url: '/TDS/InvestmentDeclaration/GetLetOutHouseList',
                                dataType: 'html',
                                data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: AssessmentYear },
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabSix').html("");
                                        $('#tabSix').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });

                        });
                    }

                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}
function LetOutOwnerStausChange(sender, id) {

    debugger;

    var cid = $(sender).val();

    if ($('#LetOutDetails_OwnerStatus_Code option:selected').text() == "Self") {
        $('#LetOutDetails_Percentage').val(100);
        $('#LetOutDetails_Percentage').attr("disabled", true);
    }
    else {
        $('#LetOutDetails_Percentage').val(0);
        $('#LetOutDetails_Percentage').attr("disabled", false);
    }
   
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }

    if ($('#LetOutDetails_Percentage').val() == "") {
        $('#LetOutDetails_Percentage').addClass('is-invalid');
        $('#percentage').addClass('error-message');
        $('#percentage').html("This field is required");
    }
    else {
        $('#LetOutDetails_Percentage').removeClass('is-invalid');
        $('#percentage').removeClass('error-message');
        $('#percentage').html("");
    }

   
    

}
function LOInterestChange(sender, id) {
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    var interest = parseFloat($('#LetOutDetails_Interest').val());
    var emiinterest = parseFloat($('#LetOutDetails_PreEMIInterest').val());

    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }

    if (isNaN(emiinterest) || emiinterest == '' || emiinterest == null) {
        emiinterest = parseFloat("0.00");
    }

    var totalInterest = (interest + emiinterest);
    $('#LetOutDetails_TotalInterest').val(totalInterest);

}
//---------------------OTHER INCOME------------------
function OtherIncomeCreate(TrnCode) {

    debugger;
    var EmployeeGI = $('#EmployeeGuid').val();
    $.ajax({
        type: 'GET',
        url: '/TDS/InvestmentDeclaration/OtherIncomeCreate',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, TrnCode: TrnCode, AssessmentYear: $('#AssessmentYear_Code').val() },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#OtherIncomeSave').html("");
                $('#OtherIncomeSave').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
    debugger;
}

function OtherIncomeSave(mode) {
    debugger;

    var msg;
    if (mode != "Modify") {
        msg = "Do you want to Save?";
    }
    else {
        msg = "Do you want to Modify?";
    }
    var i = 0;
    if (!validateById('#OtherIncomeDetails_NatureofIncome', 'natureofincme')) {
        i++;
    }
    if (!validateById('#OtherIncomeDetails_Amount', 'amt')) {
        i++;
    }
   
    if (i > 0) {
        return false;
    }
    else {
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclaration/OtherIncomeSave',
                    dataType: 'json',
                    data: { TrnCode: $('#OtherIncomeDetails_TrnCode').val(), NatureofIncome: $('#OtherIncomeDetails_NatureofIncome').val(), Amount: $('#OtherIncomeDetails_Amount').val(), tdsortcs: $('#OtherIncomeDetails_TdsorTcs').val(), TabIndex: "Other Income", AssessmentYear: $('#AssessmentYear_Code').val()  , EmpGI: $('#EmployeeGuid').val() },
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('OtherIncome');

                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclaration/GetOtherIncomeList',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {

                                            $('#tabSeven').html("");
                                            $('#tabSeven').html(result);
                                        }


                                    },


                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
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
        });

    }
}
function DeleteOtherIncomeDetails(TrnCode) {

    var EmployeeGI = $('#EmployeeGuid').val();
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
            //    // Save it

            $.ajax({

                type: 'POST',
                url: '/TDS/InvestmentDeclaration/DeleteOtherIncomeDetails',
                dataType: 'json',
                data: { TrnCode: TrnCode, EmployeeGI: EmployeeGI, AssessmentYear: $('#AssessmentYear_Code').val() },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.tabOne').removeClass('active');
                            $('#tabOne').removeClass('active');
                            $('#tabOne').css('display', 'none');
                            $.ajax({

                                type: 'GET',
                                url: '/TDS/InvestmentDeclaration/GetOtherIncomeList',
                                dataType: 'html',
                                data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }
                                    else {
                                        $('#tabSeven').html("");
                                        $('#tabSeven').html(result);
                                    }
                                },
                                error: function (xhr, textStatus, errorThrown) {
                                    debugger
                                    document.body.id = "kt_body";
                                    $('body').html(xhr.responseText);


                                }
                            });

                        });
                    }

                    else {
                        swal.fire("Something went wrong.").then(function () {


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}