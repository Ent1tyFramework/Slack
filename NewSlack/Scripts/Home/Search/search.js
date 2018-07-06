$(function () {
    $('.search input').keypress(function (e) {
        if (e.which == 13) {
            $('.search button').trigger('click');
            return false;
        }
    });
    $('.search button').click(function () {
        $.ajax({
            type: 'POST',
            url: '/search',
            data: { query: $('.search input').val() },
            success: function (users)
            {
                $('.results').empty();
                $('.results').append(users);
            },
            error: function (e) {
                alert('Error: ' + e.statusText);
            }
        });
    });
});
function afterSubscribe() { }
function afterUnsubscribe() { }