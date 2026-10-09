$(document).ready(function () {
    $("#dataChart").hide();
    $("#barChart").hide();
    $("#pieChart").hide();
    $("#payRollChart").hide();

    $("#export-pdf").click(function () {
        var chartType = $("#chartType").val();
        if (chartType == 1) {
            kendo.drawing.drawDOM($("#dataChart")).then(function (group) {
                kendo.drawing.exportPDF(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                }).done(function (data) {
                    kendo.saveAs({
                        dataURI: data,
                        fileName: "chart.pdf"
                    });
                });
            });
        }
        else if (chartType == 3) {
            kendo.drawing.drawDOM($("#pieChart")).then(function (group) {
                kendo.drawing.exportPDF(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                }).done(function (data) {
                    kendo.saveAs({
                        dataURI: data,
                        fileName: "chart.pdf"
                    });
                });
            });
        }

        else if (chartType == 2) {
            kendo.drawing.drawDOM($("#barChart")).then(function (group) {
                kendo.drawing.exportPDF(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                }).done(function (data) {
                    kendo.saveAs({
                        dataURI: data,
                        fileName: "chart.pdf"
                    });
                });
            });
        }

    });
});

function onModuleChange(sender, id) {
    debugger;
    var cid = $(sender).val();
   
  
    $.getJSON("/MIS/CompanyMIS/GetDropDown", { moduleCode: $(sender).val() }, function (data) {
            $('#submodule option').remove();
            $('#submodule').append('<option value="0">Select Report</option');

            for (var i = 0; i < data.length; i++) {
                $('#submodule').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
   
}
function onChartChange() {
    getList();
}


function getList() {
    var report = $('#submodule').val();
    var chart = $("#dataChart").data("kendoChart");
    var barChart = $("#barChart").data("kendoChart");
    var pieChart = $("#pieChart").data("kendoChart");
    var payRollChart = $("#payRollChart").data("kendoChart");
    var total = 0;
    var totalSep = 0;
    var misArray = [];
    var options = barChart.options;
    var i = 0;
    $("#tableData").empty();
    var chartTypeDropdown = $("#chartType").data("kendoDropDownList");
    if (report == 8) {

        chartTypeDropdown.enable(true); // Ensure the control is enabled
        chartTypeDropdown.select(1); // Select the second option (Bar)
        chartTypeDropdown.enable(false); // Disable the control to access options
        chartTypeDropdown.enable(0, true); // Enable the first option (Donut)
        chartTypeDropdown.enable(2, true); // Enable the third option (Pie)

        //$('#chartType option[value="1"]').prop('disabled', true);
        //$('#chartType option[value="3"]').prop('disabled', true);

        // Set the selected option in the chart type dropdown to the one you want
        //  $('#chartType option[value="2"]').prop('selected', true);
    }
    else {
        // $('#chartType option').prop('disabled', false);
        chartTypeDropdown.enable(true);
    }

    if (report == 16) {
        chartTypeDropdown.enable(false);

        $("#dataChart").hide();
        $("#pieChart").hide();
        $("#barChart").hide();
        $("#payRollChart").show();
        payRollChart.dataSource.read({ report: report });

        payRollChart.refresh();


    }
    else {
        $.ajax({

            type: 'GET',
            url: '/MIS/CompanyMIS/GetList',
            dataType: 'json',
            data: { report: report },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {

                    if (report == 8) {                                            //Joining and seperation report
                        var head = "<thead>< tr class='table-light'><th scope='col'>" + result.misDetailsList[0].nameHead + "</th><th style='text-align:end;' scope='col'>Joins</th><th style='text-align:end;' scope='col'>%</th><th style='text-align:end;' scope='col'>Seps.</th><th style='text-align:end;' scope='col'>%</th></thead > ";
                    }
                    else {
                        var head = "<thead>< tr class='table-light'><th scope='col'>" + result.misDetailsList[0].nameHead + "</th><th style='text-align:end;' scope='col'>Count</th><th style='text-align:end;' scope='col'>%</th></thead > ";
                    }
                    $("#tableData").append(head);

                    $.each(result.misDetailsList, function (index, item) {
                        // Generate an HTML table row for each item
                        row = "<tr>";
                        row += "<td>" + item.name + "</td>";
                        row += "<td align='right'>" + item.count + "</td>";
                        row += "<td align='right'>" + item.valuePercentage + "</td>";
                        if (report == 8) {                                           //Joining and seperation report additional column
                            row += "<td align='right'>" + item.sepCount + "</td>";
                            row += "<td align='right'>" + item.sepPercentage + "</td>";
                        }
                        row += "</tr>";

                        // Append the row to the HTML table

                        $("#tableData").append(row);

                        total = total + item.count;

                        if (report == 8) {
                            totalSep = totalSep + item.sepCount;
                        }

                        i++;

                    });




                    if (report == 8) {                               //Joining and seperation report
                        $("#tableData").append("<tr style='background-color:#bebeca'><td style='font-weight: bold'>Total</td><td style='font-weight: bold' align='right' > " + total + "</td><td></td><td style='font-weight: bold' align='right'>" + totalSep + "</td><td></td>");
                    }
                    else {
                        $("#tableData").append("<tr style='background-color:#bebeca'><td style='font-weight: bold'>Total</td><td style='font-weight: bold' align='right'>" + total + "</td><td></td>");
                    }

                    if ($("#chartType").val() == 2) {    //bar chart
                        $("#dataChart").hide();
                        $("#pieChart").hide();
                        $("#barChart").show();
                        $("#payRollChart").hide();
                        var maxCount = Math.max.apply(Math, result.misDetailsList.map(function (o) { return o.count; }));

                        var maxLength = Math.max.apply(Math, result.misDetailsList.map(function (item) {
                            return item.name.length;
                        }));



                        var majorUnit = Math.ceil(maxCount / 10); // Set MajorUnit to 10% of the maximum count

                        var series = barChart.options.series;
                        series[0].name = result.misDetailsList[0].nameHead;
                        if (report == 8) {                                    //Joining and seperation report 2 series visible

                            series[1].visible = true;
                            series[1].visibleInLegend = true;
                            series[1].labels.position = 'top';
                            series[1].labels.visible = true;
                            series[0].name = 'Joining';
                            // barChart.options.subtitle.text = 'Joining : ' + total.toString() + ' Separation : ' + totalSep.toString();
                        }
                        else {
                            series[1].visible = false;
                            series[1].visibleInLegend = false;


                        }

                        series[0].labels.position = 'center';
                        series[0].labels.visible = true;

                        barChart.setOptions({ series: series });
                        barChart.dataSource.read({ report: report });

                        if (i >= 6 || maxLength > 20) {    //check whether x axis and y axis length for avoiding overlapping labels in the axix

                            barChart.options.categoryAxis.labels.rotation = 20;

                        }
                        barChart.options.title.text = result.misDetailsList[0].reportName;

                        if (report == 8) {
                            barChart.options.subtitle.text = 'Joining : ' + total.toString() + ', Separation : ' + totalSep.toString();
                        }
                        else {
                            barChart.options.subtitle.text = 'Total : ' + total.toString();
                        }

                        barChart.refresh();

                    }
                    else if ($("#chartType").val() == 1) {  //donut chart

                        $("#dataChart").show();
                        $("#pieChart").hide();
                        $("#barChart").hide();
                        $("#payRollChart").hide();
                        chart.dataSource.read({ report: report });


                        chart.options.title.text = result.misDetailsList[0].reportName;
                        chart.options.subtitle.text = 'Total : ' + total.toString();
                        chart.refresh();
                    }
                    else {

                        $("#dataChart").hide();
                        $("#pieChart").show();
                        $("#barChart").hide();
                        $("#payRollChart").hide();
                        pieChart.dataSource.read({ report: report });
                        pieChart.options.title.text = result.misDetailsList[0].reportName;
                        pieChart.options.subtitle.text = 'Total : ' + total.toString();
                        pieChart.refresh();

                    }


                    // Step 4: Handle the button click event and export the chart to PDF

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
}


