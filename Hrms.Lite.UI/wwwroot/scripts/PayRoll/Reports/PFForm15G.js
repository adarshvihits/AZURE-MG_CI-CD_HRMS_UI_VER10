function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();


    var temp = filter1;

    var dataTable = $("#PfFormX_empList").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PFForm15G/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length  + '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                $('#PF15GSummary_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].fullName = '<div><a onclick="GetPdf(' + d.data[i].employeeCode + ')">' + d.data[i].fullName + ' </a ></div></div>'
                    d.data[i].fullName = '<div><a onclick="AddPlaceDate(' + d.data[i].employeeCode + ')">' + d.data[i].fullName + ' </a ></div></div>'




                }
                return d.data;
            }
        },
        "columns": [

            { "data": "employeeID", "width": "20%" },
            {"data": "fullName", "width": "20%" },
            { "data": "dsgName", "width": "15%" },
            { "data": "dptName", "width": "15%" },
            { "data": "locName", "width": "15%" },
       
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

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        // dataTable.search('').draw();
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

function GetPdf(EmpCode,Date,Place) {

    debugger;
    var url = "/PayRoll/PFForm15GReport/GetForm15GData?EmpCode=" + EmpCode + "&Date=" + Date + "&Place=" + Place;

    window.location.href = url;
}




function AddPlaceDate(EmpCode) {
    debugger;

    var title = '';

    swal.fire({
        title: title,
        html: `
            <label for="datePickerInput">Select Date:</label>
            <input type="text" id="datePickerInput" class="form-control kt_datepicker_2" readonly placeholder="Date">
            <br>
            <label for="placeInput">Enter Place:</label>
            <input type="text" id="placeInput" class="form-control" placeholder="Place">
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        didOpen: () => {
            // Initialize the datepicker after the modal opens
            $('.kt_datepicker_2').datepicker({
                format: "dd-M-yyyy",
                autoclose: true
            });
        },
        preConfirm: () => {
            var selectedDate = $('#datePickerInput').val();
            var place = $('#placeInput').val().trim();

            if (!selectedDate) {
                Swal.showValidationMessage('Date selection is mandatory');
                return false;
            }
            if (!place) {
                Swal.showValidationMessage('Place is mandatory');
                return false;
            }
            return { selectedDate, place }; // Return selected values
        }
    }).then((result) => {
        debugger;
        if (result.isConfirmed && result.value) {
            var selectedDate = result.value.selectedDate;
            var place = result.value.place;
            this.GetPdf(EmpCode, selectedDate,place);
        }
    });
}

