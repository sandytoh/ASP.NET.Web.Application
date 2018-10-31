
$(function () {
    LoadChart();
    $("[id*=ddlMonth]").bind("change", function () {
        LoadChart();
    });
    $("[id*=rblChartType] input").bind("click", function () {
        LoadChart();
    });
});
function LoadChart() {
    var chartType = parseInt($("[id*=rblChartType] input:checked").val());
    $.ajax({
        type: "POST",
        url: "Dashboard.aspx/GetChart",
        data: "{month: '" + $("[id*=ddlMonth]").val() + "'}",
        //labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $("#dvChart").html("");
            $("#dvLegend").html("");
            var data = eval(r.d);
            // alert(data);
            var aLabels = [];
            var aValues = [];
            var aColor = [];
            var aDatasets1 = eval(r.d);

            for (var i = 0; i < data.length; i++) {
                aLabels.push(data[i].Label);
                aValues.push(data[i].Val1);
                aColor.push(data[i].color);
            };

            // alert(aLabels);
            //alert(aValues);
            var barOptions = {
                responsive: true,
                maintainAspectRatio: true,

                scales: {
                    yAxes: [{

                        gridLines: {
                            display: true
                        },
                        ticks:
                            {
                                beginAtZero: true,
                                fontSize: 14
                            },
                        scaleLabel: {
                            display: true,
                            labelString: 'ChargeBack (SGD)',
                            fontStyle: 'bold',
                            fontFamily: "'Raleway', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                            fontSize: 14
                        }
                    }],
                    xAxes: [{
                        gridLines: {
                            display: true

                        },
                        ticks:
                            {
                                beginAtZero: true,
                                fontSize: 14
                            }
                    }]
                }
            };
            var data1 = {
                labels: aLabels,

                datasets: [{
                    label: 'Months',
                    data: aValues,

                    //backgroundColor: aColor,
                    backgroundColor: [

                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)'
                    ],
                    // borderColor: aColor,
                    borderColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                    ],
                    borderWidth: 1

                }]

            };




            var el = document.createElement('canvas');
            $("#dvChart")[0].appendChild(el);

            //Fix for IE 8
            //if ($.browser.msie && $.browser.version == "8.0") {
            //    G_vmlCanvasManager.initElement(el);
            //}
            var ctx = el.getContext('2d');



            var userStrengthsChart;

            switch (chartType) {
                case 1:
                    //  userStrengthsChart = new Chart(ctx).Bar(data1);
                    userStrengthsChart = new Chart(ctx, { type: 'bar', data: data1, options: barOptions })
                    break;
                case 2:
                    //userStrengthsChart = new Chart(ctx).Pie(data);
                    userStrengthsChart = new Chart(ctx, { type: 'pie', data: data1 })
                    break;
                case 3:
                    //userStrengthsChart = new Chart(ctx).Doughnut(data);
                    userStrengthsChart = new Chart(ctx, { type: 'doughnut', data: data1 })
                    break;

            }

            for (var i = 0; i < data.length; i++) {
                var div = $("<div />");
                div.css("margin-bottom", "10px");
                div.html("<span style='display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
                $("#dvLegend").append(div);
            }
        },
        failure: function (response) {
            alert('There was an error.');
        }
    });
}



    var dp1 = $('#txtToDate');
    dp1.datepicker({
        startDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd/MM/yyyy"
        //language: "tr"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).blur();
        $(this).datepicker('hide');
    });

    var dp = $('#txtFromDate');

    dp.datepicker({
        startDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd/MM/yyyy"
        //language: "tr"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });



function removeemptydelwarning() {
    swal("Warning!", "Sorry, there is no current delegate to remove.", "warning");
}

