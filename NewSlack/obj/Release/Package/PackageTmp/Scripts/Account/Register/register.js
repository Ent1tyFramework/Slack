$(function () {
    $('#goNext').click(function () {
        $('.step1').hide();
        $('.step2').show();
        $(this).text('Continue');
    });
    $('#goBack').click(function () {
        $('.step2').hide();
        $('.step1').show();
    });
});