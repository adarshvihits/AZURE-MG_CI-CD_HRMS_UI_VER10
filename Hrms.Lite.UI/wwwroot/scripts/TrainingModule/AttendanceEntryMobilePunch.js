
function ScheduleList() {
    showSpinner();
    debugger
    var status = $('#TrnStatus').val();
    var filter = $("#BasicFilter").serialize();
    var radiomode = $("input[id='radiomode']:checked").val();
    var radio1 = $("input[id='radio1']:checked").val();
    $.ajax({
        type: 'GET',
        url: '/TrainingModule/AttendanceEntryMobilePunch/GetList',
        dataType: 'html',
        data: filter + "&ScheduleCode=" + status + "&radiomode=" + radiomode + "&radio1=" + radio1,
        success: function (result) {
            debugger
            $('#Listview').html('');
            $('#Listview').html(result);
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
            hideSpinner();
        }
    });
}



function EditChange(employeecode) {
    var status = $('#TrnStatus').val();
    var radiomode = $("input[id='radiomode']:checked").val();
    var radio1 = $("input[id='radio1']:checked").val();

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/AttendanceEntryMobilePunch/EditChange',
        dataType: 'html',
        data: { employeecode: employeecode, status: status, radiomode: radiomode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                swal.fire("Saved Successfully");
                ScheduleList();
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });

}

function OnTopicsClick(EmployeeCode,Employeename,Id) {
    debugger
   
   
    var status = $('#TrnStatus').val();

    $.ajax({
        type: 'GET',
        url: "/TrainingModule/AttendanceEntryMobilePunch/GetTopicsPopUp",
        data: { ScheduleCode:status,EmployeeCode: EmployeeCode },

        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.topicsdetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#PunchingLabel').html('Punching Details -' + Employeename + ' (' + Id + ')');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            ScheduleList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            ScheduleList();
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        ScheduleList();


    }
}