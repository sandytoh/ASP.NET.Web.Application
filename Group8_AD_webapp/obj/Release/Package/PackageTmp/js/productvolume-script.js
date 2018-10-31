
$(document).ready(start);

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(start);

var todayDate = new Date().getDate();

function start() {
    var dp1 = $('#txtEndDate');
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
    var dp = $('#txtStartDate');

    dp.datepicker({
        startDate: '-1y',
        endDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd-M-yyyy"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });

    if ($('#IsDesc').val() == "false") {
        $('#lstProductVolume').prepend($("<thead></thead>").append($('#lstProductVolume').find("tr:first"))).dataTable(
            {
                "order": [[2, "asc"]],
                responsive: true
            });
    }
    else {
        $('#lstProductVolume').prepend($("<thead></thead>").append($('#lstProductVolume').find("tr:first"))).dataTable(
            {
                "order": [[2, "desc"]],
                responsive: true
            });
    }
}
