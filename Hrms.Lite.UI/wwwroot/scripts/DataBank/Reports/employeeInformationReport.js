
//function ExportToExcel() {
//    debugger;
//    $.ajax({

//        type: 'POST',
//        url: '/DataBank/EmployeeInformationReport/GetDetails',
//        dataType: 'json',
//        data: $('#employeeInformationSave').serialize(),
      
//        success: function (result) {
//            debugger;

//            if (result == true) {
//                debugger;
//                var flag = 0;
//                debugger;
//                for (let i = 0; i < 12; i++) {
//                    debugger;
//                    if ($('#EmployeeGrouping_' + i + '__Applicability').is(':checked')) {
//                        flag = 1;
//                        i = 12;
//                    }
//                }
//                if (flag == 1) {
//                    debugger;
//                    //var url = '/DataBank/EmployeeInformationReport/ExportToexcel?data=' + $('#employeeInformationSave').serialize();
//                    //window.location.href = url;
//                    $('#employeeInformationSave').submit();
//                    return true;

//                }
//                else {
//                    swal.fire("Please Select atleast one Employee Grouping");
//                    return false;
//                }
                
//            }
//            else {
//                debugger;
//                swal.fire("No data found to export");
               
//                return false;
               
//            }
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            debugger;
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//        }
//    });









   
//}

function ExportToExcel() {
    debugger;


    var flag = 0;
    //debugger;
    //for (let i = 0; i < 12; i++) {
    //    debugger;
    //    if ($('#EmployeeGrouping_' + i + '__Applicability').is(':checked')) {
    //        flag = 1;
    //        i = 12;
    //    }
    //}
/*    if (flag == 1) {*/
        debugger;
        $('#employeeInformationSave').submit();
    /*    return true;*/

    //}
    //else {
    //    swal.fire("Please Select atleast one Employee Grouping");
    //    return false;
    //}

    //$.ajax({

    //    type: 'POST',
    //    url: '/DataBank/EmployeeInformationReport/GetDetails',
    //    dataType: 'json',
    //    data: $('#employeeInformationSave').serialize(),

    //    success: function (result) {
    //        debugger;

    //        if (result == true) {
    //            debugger;
               
    //        }
    //        else {
    //            debugger;
    //            swal.fire("No data found to export");

    //            return false;

    //        }
    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        debugger;
    //        document.body.id = "kt_body";
    //        $('body').html(xhr.responseText);
    //    }
    //});
}