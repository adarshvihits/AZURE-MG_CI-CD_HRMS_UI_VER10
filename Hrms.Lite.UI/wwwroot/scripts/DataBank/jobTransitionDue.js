var prevTab = 'all';
var currentTab = 0;  //code for 'all'
var CurrTabName = 'All Dues';
function ontabClick(Tab, TabIndex,tabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    CurrTabName = tabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var searchID = $("#Filter_Code").val();;
    //var url = "/DataBank/JobTransitionDueList/GetJobTransitionDueList";
    //$('#jobTransition-Due-List').load(url, { TabIndex: TabIndex, SearchId: searchID }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;        
   
}

function TabsDropdown(TabIndex) {
    debugger;
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    ApplyFilter('apply');

    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'tab3';
    else if (TabCode == 4)
        return 'tab4';
    else
        return 'more';
}

function onFilterDropdownChange(SearchId) {
    debugger;
    showSpinner();
    //var url = "/DataBank/JobTransitionDueList/GetJobTransitionDueList";
   
    var search = SearchId.value;
    var search = $('#classification').val();

    $('#Filter_Code').val(search);
    $('.filterDropDown').val(search);
    //$('#jobTransition-Due-List').load(url, { TabIndex: currentTab, SearchId: search }, function () { });
    ApplyFilter('reset');
}

function onExportToExcelClick() {
    debugger;
    var searchid = $('#Filter_Code').val();

    var TableLength = $('#JobTransitionDueList_Count').val();
    if (TableLength > 1) {
        url = "/DataBank/JobTransitionDueList/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + CurrTabName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#classification').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionDueList/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransition-Due-List').html("");
                        $('#jobTransition-Due-List').html(result);
                        $('#jobTransition-Due-List').removeClass('hide');
                    }
                    hideSpinner();
                          
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/JobTransitionDueList/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#jobTransition-Due-List').html("");
                        $('#jobTransition-Due-List').html(result);
                        $('#jobTransition-Due-List').removeClass('hide');
                    }
                    hideSpinner();
                         
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/JobTransitionDueList/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#jobTransition-Due-List').html("");
                    $('#jobTransition-Due-List').html(result);
                    $('#jobTransition-Due-List').removeClass('hide');
                }
          
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}
