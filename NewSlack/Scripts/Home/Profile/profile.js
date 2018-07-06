$(function () {
    $('div.posts').empty();
    only = false;
    data_loading(true);

    if ($('.no_post').length > 0) {
        $('.posts').css('border-top', '1px solid #000');
    }
});
