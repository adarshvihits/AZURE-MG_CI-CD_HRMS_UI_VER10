
    var tooltips = JSON.parse('@Html.Raw(ViewBag.TooltipsJson)');
$(document).ready(function () {

    $('.search-profile-details').hide(300);

        $("#dataTableSearch").on("keyup search paste cut", function () {

            $('#Report').data('kendoGrid').dataSource.read();


            $('#Report').data('kendoGrid').refresh();


            // dataTable.search(this.value).draw();

        });


    function showDetails(e) {
        // var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var wnd = $("#Skills").data("kendoWindow");
        // empID = dataItem.EmployeeID;
        // wnd.center().open();
        wnd.content("loading user info...").refresh({
            url: "@Url.Action("ChildGrid", "SkillMatrixReport")"
        }).center().open();

    }

    function saveSelectedSkills() {
        var grid = $("#skillGrid").data("kendoGrid");
        var selectedItems = grid.select(); // Get the selected rows

        var skillCodes = [];
        selectedItems.each(function () {
            var dataItem = grid.dataItem(this);
            skillCodes.push(dataItem.SkillCode);
        });
        console.log(skillCodes.join(","));
        // Save the selected skill codes to the hidden field
        $("#selectedSkillCodes").val(skillCodes.join(","));

        GetList();

    }
    function clearSelection() {
        var grid = $("#skillGrid").data("kendoGrid");
        grid.clearSelection();
    }

    $('.search-slider-inner').click(function () {
        // dataTable.search('').draw();
        $('#Report').data('kendoGrid').dataSource.read({ page: 1 });
    $('#Report').data('kendoGrid').pager.page(1);
    $('#Report').data('kendoGrid').refresh();
    $('#dataTableSearch').val('');
    $('#dataTableSearch').focus();
        });
       
    });

    function contentHandler(e) {
        // If the element is the header, return the text of the cell.
        var headerTooltip="";
    if (e.target.is("th")) {
            var targetText = e.target.text().trim();

    for (var key in tooltips) {
               
                if (key == targetText) {
                   var value = tooltips[key];
    headerTooltip=value

    break;
                }
    else
    {
        headerTooltip = targetText;
                  
                }
            }
    return headerTooltip;
        }

       // var dataItem = $("#grid").data("kendoGrid").dataItem(e.target.closest("tr"));
       // var content = dataItem.QuantityPerUnit;
       // return content;
    }

    function employeeParam() {

        var formArray = $("#BasicFilter").serializeArray();

    var tabIndex = 0;
    var searchValue = $('#dataTableSearch').val();

    var temp = { };
    for (var i = 0; i < formArray.length; i++) {
        temp[formArray[i]['name']] = formArray[i]['value']; // Map the current key-value pairs into the filter variable object.
        }


    var type = $('#Type').val();

    var skill = $('#selectedSkillCodes').val();

    temp["searchValue"] = searchValue;

    console.log(type);

    return {filter1: temp, type: type,skills:skill };
    }

    function GetList() {
        var skill = $('#selectedSkillCodes').val();
    var selectedType = $('#Type').val();
    var url = '@Url.Action("ReloadDiv", "SkillMatrixReport", new {area = "DataBank"})';

    url += '?type=' + selectedType +'&skills='+skill ; // Append the type value as a query parameter

    $("#grid").empty();
    $("#grid").load(url);


    }
    function ApplyFilter(mode) {

        if (mode == 'apply') {

            if (filterApplicable >= 1) {

        $('#Report').data('kendoGrid').dataSource.read();

    //refresh will re-render items in grid from the current datasource
    $('#Report').data('kendoGrid').refresh();

    CloseSlider('basicFilter');
    $('#filterForm').removeClass('filter-apply');
            }
    else {
        $('#Report').data('kendoGrid').dataSource.read();


    $('#Report').data('kendoGrid').refresh();
    CloseSlider('basicFilter');
    $('#filterForm').addClass('filter-apply');
            }


        }

    else {
        filterApplicable = 1;
    $('.reset').val('');
    $('#Optional_Filter_EmploymentStatus_Code').val(0);
    $('#filterForm').removeClass('filter-apply');

    $('#Report').data('kendoGrid').dataSource.read();


    $('#Report').data('kendoGrid').refresh();

        }


    }

    function applyCellColor(value, field) {
       
        if (value == 5) {
            return "<div class='greencolor'>" + value + "</div>";
        }
        else if (value >= 4 && value<5) {
            return "<div class='normalcolor'>" + value + "</div>";
        }
        else if (value >= 3 && value<4) {
            return "<div class='yellowcolor'>" + value + "</div>";
        }
        else if (value >= 2 && value < 3) {
            return "<div class='bluecolor'>" + value + "</div>";
        }
        else if (value >= 1 && value<2) {
            return "<div class='redcolor'>" + value + "</div>";
        }
    else {
            return value;
        }
    }
    function columnHeaderTemplate(columnName, tooltip) {
        return '<span title="' + tooltip + '">' + columnName + '</span>';
    }



