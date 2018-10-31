$(document).ready(function () {
    var dp1 = $('#txtFromDate');
    dp1.datepicker({
        startDate: '-1y',
        endDate: '-1',
        changeMonth: true,
        changeYear: true,
        format: "dd-M-yyyy"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });
});

$(document).ready(MonthPick);
function MonthPick() {
    $('#txtMonthPick').MonthPicker({
        MinMonth: '-1y',
        MaxMonth: '0',
        Button: false, MonthFormat: 'MM yy',
        OnAfterChooseMonth: function () { $("#txtMonthPick").trigger("change"); }
    });
}

$(document).ready(function () {
    var dp1 = $('#txtToDate');
    dp1.datepicker({
        startDate: '-1y',
        endDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd-M-yyyy"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });

    $('#ByDept').click(function () {
        $('#IsDept').val("true");
    });
    $('#BySupp').click(function () {
        $('#IsDept').val("false");
    });

    $('#ByMonth').click(function () {
        $('#IsMonth').val("true");
    });
    $('#ByDate').click(function () {
        $('#IsMonth').val("false");
    });


});

$(document).ready(drawChart);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(drawChart);
$(document).ready(checkDept);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(checkDept);

function checkDept() {
    if ($('#IsDept').val() == "false") {
        $('#BySupp').trigger("click");
    }
    if ($('#IsMonth').val() == "false") {
        $('#ByDate').trigger("click");
    }
}

function drawChart() {
    var ctx = document.getElementById("myChart").getContext('2d');
    $.ajax({
        url: "Reports.aspx/getChartData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var chartLabel = eval(response.d[0]); //Labels
            var chartData = eval(response.d[1]); //Data
            var chartData2 = eval(response.d[2]); //Data
            var label1 = response.d[3];
            var label2 = response.d[4];
            var label3 = response.d[5];
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: chartLabel,
                    datasets: [{
                        label: label1,
                        data: chartData,
                        backgroundColor: 'rgba(255, 165, 0, 0.2)',
                        borderColor: 'rgba(255, 165, 0, 1)',
                        borderWidth: 1
                    },
                    {
                        label: label2,
                        data: chartData2,
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
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
                                            labelString: label3,
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
                                            labelString: 'Dates',
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