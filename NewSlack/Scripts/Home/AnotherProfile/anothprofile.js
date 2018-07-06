$(function () {
    $('.follow button').click(function () {
        subscribe('@user.Id');
    });
});

function afterSubscribe() {
    $('.follow button').removeClass('btn-outline-dark');
    $('.follow button').addClass('following');
}

function afterUnsubscribe() {
    $('.follow button').removeClass('following');
    $('.follow button').addClass('btn-outline-dark');
}
