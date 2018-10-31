function MonthPick() {
    $('#txtMonthPick').MonthPicker({
        MinMonth: '-1y',
        MaxMonth: '0',
        Button: false, MonthFormat: 'MM yy',
        OnAfterChooseMonth: function () { $("#txtMonthPick").trigger("change"); }
    });
}

$(document).ready(drawChart);
$(document).ready(MonthPick);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(drawChart);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MonthPick);

function drawChart() {
    var ctx = document.getElementById("myChart").getContext('2d');
    $.ajax({
        url: "StoreDashboard.aspx/getChartData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var chartLabel = eval(response.d[0]); //Labels
            var chartData = eval(response.d[1]); //Data
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: chartLabel,
                    datasets: [{
                        label: 'ChargeBack (SGD)',
                        data: chartData,
                        backgroundColor: [
                            'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)',
                            'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)',
                            'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)',
                            'rgba(54, 162, 235, 0.2)', 'rgba(54, 162, 235, 0.2)'
                        ],
                        borderColor: [
                            'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)',
                            'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)',
                            'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)',
                            'rgba(54, 162, 235, 1)', 'rgba(54, 162, 235, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                responsive: true,
                options:
                    {

                        maintainAspectRatio: false,
                        scales:
                            {
                                yAxes:
                                    [{
                                        ticks:
                                            {
                                                beginAtZero: true,
                                                fontSize: 15
                                            },
                                        scaleLabel: {
                                            display: true,
                                            labelString: 'ChargeBack (SGD)',
                                            fontStyle: 'bold',
                                            fontFamily: "'Raleway', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                                            fontSize: 18
                                        }
                                    }],
                                xAxes:
                                    [{
                                        ticks:
                                            {
                                                fontSize: 14
                                            },
                                        scaleLabel: {
                                            display: true,
                                            labelString: 'Department',
                                            fontStyle: 'bold',
                                            fontFamily: "'Raleway', 'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                                            fontSize: 18
                                        }
                                    }]
                            }
                    }
            })
        }
    })
}