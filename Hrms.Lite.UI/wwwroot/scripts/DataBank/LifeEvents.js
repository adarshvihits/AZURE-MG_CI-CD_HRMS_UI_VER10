var eventType = 'upcoming';
function AddNew() {
   var url = "/DataBank/LifeEvents/Create";
    window.location.href = url;

}

function GetEventDetails(EventGI) {
    $.ajax({
        type: 'GET',
        url: '/DataBank/LifeEvents/GetEventDetails',
        dataType: 'html',
        data: { EventGI: EventGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#sliderDiv').html("");
                $('#sliderDiv').html(result);
                $('body').append('<div class="form-overlay"></div>');
            }

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}


function GetEventDetailsDelete(EventGI) {
    $.ajax({
        type: 'GET',
        url: '/DataBank/LifeEvents/GetEventDetailsDelete',
        dataType: 'html',
        data: { EventGI: EventGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#sliderDiv').html("");
                $('#sliderDiv').html(result);
            }

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}

function GetEventList(type) {
    eventType = type;
    $.ajax({
        type: 'GET',
        url: '/DataBank/LifeEvents/GetEventList',
        dataType: 'html',
        data: { type: type },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#listDiv').html("");
                $('#listDiv').html(result);
            }

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}

function GetUpcomingCelebrations() {

    var Celebration = $('#celebType').val();
    var Classification = $('#classifications').val();
    if (Celebration != '' && Classification != '') {
        $.ajax({
            type: 'GET',
            url: '/DataBank/LifeEvents/GetUpcomingCelebrationsList',
            dataType: 'html',
            data: { Celebration: Celebration, Classification: Classification },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#upcomingCelebs').html("");
                    $('#upcomingCelebs').html(result);
                }

            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
   
}


function SaveEvent(msg,mode) {
    swal.fire({
        text: "Do you want to " + msg + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $('#Mode').val(mode);
            var Evntsdata = new FormData($('#addEvnts')[0]);
            $.ajax({

                type: 'POST',
                url: '/DataBank/LifeEvents/Save',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: Evntsdata,
                success: function (result) {
                    if (result.success == true) {
                        if (mode == 'MODIFY') {
                            swal.fire("Modified Successfully").then(function () {
                                //var url = "/Master/SalaryStructure/Index";
                                //window.location.href = url;
                                CloseSlider('addEvts');
                                GetEventList(eventType);
                            });
                        }
                        else {
                            swal.fire("Saved Successfully").then(function () {
                                //var url = "/Master/SalaryStructure/Index";
                                //window.location.href = url;
                                CloseSlider('addEvts');
                                window.location.reload();
                            });
                        }

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

function DeleteEvent(EventGI) {
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {              
            $.ajax({

                type: 'POST',
                url: '/DataBank/LifeEvents/Delete',                
                dataType: 'json',
                data: { EventGI: EventGI},
                success: function (result) {
                    if (result.success == true) {                       
                            swal.fire("Deleted Successfully").then(function () {
                                //var url = "/Master/SalaryStructure/Index";
                                //window.location.href = url;
                                CloseSlider('delEvts');
                                GetEventList(eventType);
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



function ExportToExcel() {
    debugger;
    var LifeEventsEventList = $('#LifeEventsEventList_Count').val();
    /* var CancellationTable = document.getElementById("CancellationListTable").rows.length - 1;*/

    if (LifeEventsEventList > 0) {
        url = "/DataBank/LifeEvents/ExportToExcel?type=" + eventType;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}