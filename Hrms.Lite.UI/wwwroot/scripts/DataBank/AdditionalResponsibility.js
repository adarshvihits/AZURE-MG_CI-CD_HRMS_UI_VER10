var prevTab = "General";
var currentTab = "General";

function OnTabClick(TabIndex,Tab) {


    currentTab = TabIndex;

   
    $(".btn-title").removeClass("active");
    $("." + Tab).addClass("active");
   
    $.ajax({

        type: 'GET',
        url: '/DataBank/AdditionalResponsibility/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#AdditionalResponsibilityList').html("");
                $('#AdditionalResponsibilityList').html(result);
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

function GetAdditionalResponsibility(EmployeeGI, TabIndex) {
    debugger;
    var url = "/DataBank/AdditionalResponsibility/GetAdditionalResponsibility?EmployeeGI=" + EmployeeGI + "&TabIndex=" + TabIndex;
    window.location.href = url;
}

function SaveAdditionalResponsibility() {
    debugger;
    data = new FormData($('#AdditionalResponsibility_Entry')[0]);

    swal.fire({
        text: "Do you want to Apply?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;

            $.ajax({
                type: 'POST',
                url: '/DataBank/AdditionalResponsibility/SaveAdditionalResponsibilityDetails',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            window.location.href = "/DataBank/AdditionalResponsibility/Index";
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });

        }
        else
            return false;
    })


}
function showDecompressedDocument(RequestGI, EmployeeGI) {
    debugger;
    var url = "/DataBank/AdditionalResponsibility/DownloadDoc?RequestGI=" + RequestGI + "&EmployeeGI=" + EmployeeGI;
    window.open(url, "_blank");
}