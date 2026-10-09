
function GetWageRegisterPDF() {
   
    var EmployeeCode = $('#EmployeeCode').val();

    var SalaryPeriod = $('#PayrollMonth_Code').val();
  
    var Month = $('#PayrollMonth_Code option:selected').text();
   
   if (validateById("#PayrollMonth_Code", "pay_month")) {
        if (EmployeeCode == "") {
            EmployeeCode = "0";
        }


        var allFormElements = $("#WageRegisterPDF").find(":input").toArray();

        // Construct the serialized string manually
        var serializedStrings = "";
        $.each(allFormElements, function (index, element) {
            var name = $(element).attr("name");
            var value = $(element).val();
            if (value == "") {
                value = "0";
            }
            // Skip elements without a name or disabled elements
            if (!name || $(element).is(":disabled")) {
                return true; // Equivalent to continue in each loop
            }
            if (name != '__RequestVerificationToken' && name != 'PayrollMonth.Code' && name != 'EmployeeCode') {
                if (serializedStrings !== "") {
                    serializedStrings += "&";
                }
                serializedStrings += encodeURIComponent(name) + "=" + encodeURIComponent(value);
                serializedStrings = serializedStrings.replace(/%2C/g, ',');
            }
        });

  

       var url = "/PayRoll/WageRegister/GetWageRegisterPdf?EmployeeCode=" + EmployeeCode + "&SalaryPeriod=" + SalaryPeriod + "&Month=" + Month+ "&" + serializedStrings;




        console.log(url);
        window.location.href = url;
       return true
    }
    else {
       return false;
    }

  

}

//function GetWageRegisterPDF() {
//    debugger;

//    alert("hi");
//    console.log($('#WageRegisterPDF').serialize());
//        $.ajax({

//            type: 'POST',
//            url: '/PayRoll/WageRegister/GetWageRegisterPdf',
//            async: false,

//            data: $('#WageRegisterPDF').serialize(),
//            success: function (result) {
//                debugger;
//                if (result) {
//                    $('#wpsregister').html(result);
//                    //alert(result);
//                }
//                else {
//                    swal.fire("No data found");
//                }
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                debugger;
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });



//}



