$(document).ready(function () {
    var dp1 = $('#txtEndDate');
    dp1.datepicker({
        endDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });
    var dp = $('#txtStartDate');

    dp.datepicker({
        endDate: '0',
        changeMonth: true,
        changeYear: true,
        format: "dd/mm/yyyy"
    }).on('changeDate', function (ev) {
        $(this).blur();
        $(this).datepicker('hide');
    });
});