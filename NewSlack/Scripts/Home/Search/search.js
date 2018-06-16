$(function () {
   $('.search_input input').keypress(function (e) {
        if (e.which == 13) {
            $('.search_submit a').trigger('click');
            return false;
        }
    });
    $('.search_submit a').click(function () {
        $.ajax({
            type: 'POST',
            url: '/search',
            data: { query: $('.search_input input').val() },
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