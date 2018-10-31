function openModal() {
    $('#mdlConfirm').modal('show');
}
function openCancelModal() {
    $('#mdlCancel').modal('show');
}
$(function () {
    $('a[href*=#]').on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({ scrollTop: $($(this).attr('href')).offset().top }, 1500, 'linear');
    });
});