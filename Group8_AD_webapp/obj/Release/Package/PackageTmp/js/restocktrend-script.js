//alert("hello");

//function openTrendModal() {
//    alert("two");
//    $('.mdlTrend').modal('show');
//}
$(document).ready(drawChart);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(drawChart);


function drawChart() {

    var ctx = document.getElementById("myChart").getContext('2d');
    $.ajax({
        url: "RestockLevel.aspx/getChartData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var chartLabel = eval(response.d[0]); //Labels
            var chartData = eval(response.d[1]); //Data
            var chartData2 = eval(response.d[2]); //Data
            var chartData3 = eval(response.d[3]); //Data
            var label1 = response.d[4];
            var label2 = response.d[5];
            var label3 = response.d[6];
            var myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: chartLabel,
                    datasets: [{
                        label: label1,
                        data: chartData,
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1,
                        fill:false
                    },
                        {
                            label: label2,
                            data: chartData2,
                            backgroundColor: 'rgba(255, 165, 0, 0.2)',
                            borderColor: 'rgba(255, 165, 0, 1)',
                            borderWidth: 1,
                            fill: false
                        },
                        {
                            label: label3,
                            data: chartData3,
                            backgroundColor: 'rgba(34,139,34, 0.2)',
                            borderColor: 'rgba(34,139,34, 1)',
                            borderWidth: 1,
                            fill: false
                        }]
                },
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
                                            labelString: 'Request Quantity',
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
                                            display: false,
                                            labelString: 'Time',
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