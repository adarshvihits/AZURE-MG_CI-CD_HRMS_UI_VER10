
function onClassfnTypeChange(sender) {
    debugger
    var code = $(sender).val();
    $.ajaxSetup({
        async: false
    });
    //if (code == 0) {
    //    $("#" + spanId + "").addClass('error-message');
    //    $(sender).addClass('is-invalid');
    //    $("#" + spanId + "").html("This field is required");
    //}
    //else {
    //    $("#" + spanId + "").removeClass('error-message');
    //    $(sender).removeClass('is-invalid');
    //    $("#" + spanId + "").html("");

    $.getJSON("/DashBoard/RecruitmentDashBoard/GetClassifnDropdown", { ClassifnType: code }, function (data) {
            console.log(data);
        $('#Classification_Code option').remove();
       
        $('#Classification_Code').append('<option value=0>All</option');
            debugger;
            for (var i = 0; i < data.length; i++) {
                $('#Classification_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

    });
    //window.location.href = '/DashBoard/RecruitmentDashBoard/Index?ClassifnType=' + code + '&Classifn=' + 0;
    var url = '/DashBoard/RecruitmentDashBoard/GetPieChart';
    $('.graph-pie').load(url, { ClassifnType: code, Classifn: 0 }, function () { });

   
}

function onClassfnChange()
{
    var url = '/DashBoard/RecruitmentDashBoard/GetPieChart';
    if ($("#Classification_Code").val() == '') {
        $('.graph-pie').load(url, { ClassifnType: $("#ClassificationType_Code").val(), Classifn: 0 }, function () { });
        //window.location.href = '/DashBoard/RecruitmentDashBoard/Index?ClassifnType=' + $("#ClassificationType_Code").val() + '&Classifn=' + 0;
    }
    else {
        $('.graph-pie').load(url, { ClassifnType: $("#ClassificationType_Code").val(), Classifn: $("#Classification_Code").val() }, function () { });
        //window.location.href = '/DashBoard/RecruitmentDashBoard/Index?ClassifnType=' + $("#ClassificationType_Code").val() + '&Classifn=' + $("#Classification_Code").val() ;
    }
}

function GetVacancyNearlyDuePopUp() {
    $.ajax({

        type: 'GET',
        url: '/DashBoard/RecruitmentDashBoard/GetVacancyNearlyDuesPopUp',
        dataType: 'html',


        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.nearlydue_popup_head').html();
                var m = $('.nearlydue_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);

                m.modal('show');
            }



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}
