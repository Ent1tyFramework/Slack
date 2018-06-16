$(function () {
    $('.post_content').hover(function () {
        var parent_id = $($(this).parent()).attr('id');

        $('#' + parent_id + ' .img').css('opacity', '.5');
        $('#' + parent_id + ' .like').css('width', $('#' + parent_id + ' .img img').css('width'));
        $('#' + parent_id + ' .like').removeAttr('hidden');
    }, function () {
        var parent_id = $($(this).parent()).attr('id');

        $('#' + parent_id + ' .img').css('opacity', '1');
        $('#' + parent_id + ' .like').attr('hidden', '');
    });
    $('.post_content .like').click(function () {
        var parent_id = $($($(this).parent()).parent()).attr('id');

        var img = $('#' + parent_id + ' .like img').attr('src');
        if (img == '/Content/Images/white_like.png') {
            $('#' + parent_id + ' .like img').attr('src', '/Content/Images/black_like.png');
        }
        else {
            $('#' + parent_id + ' .like img').attr('src', '/Content/Images/white_like.png');
        }
    });

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.first_menu').css('background', '#fff');
        }
        else {
            $('.first_menu').css('background', 'none');
        }
    });
    //hover on menu icon
    $('#menu  li a:first-child').hover(function () {
        var nav = $($(this).siblings());
        item_first_hover(nav);
    }, function () {
        var nav = $($(this).siblings());
        item_last_hover(nav);
    });

    //hover
    $('#menu  li a.nav_profile').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_user.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_user.png) no-repeat center');
    });

    $('#menu  li a.nav_mail').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_mail.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_mail.png) no-repeat center');
    });

    $('#menu  li a.nav_news').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_news.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_news.png) no-repeat center');
    });

    $('#menu  li a.nav_search').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_search.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_search.png) no-repeat center');
    });

    $('#menu  li a.nav_settings').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_settings.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_settings.png) no-repeat center');
    });

    $('#menu  li a.nav_signout').hover(function () {
        $(this).css('background', 'url(/Content/Images/gray_signout.png) no-repeat center');
    }, function () {
        $(this).css('background', 'url(/Content/Images/blue_signout.png) no-repeat center');
    });

    function item_first_hover(item) {
        $(item).css('color', '#d9d9d9');
    }
    function item_last_hover(item) {
        $(item).css('color', '#4a76a8');
    }

    $('#menu .first_menu').hover(function () {
        $('#menu .hidden_menu').show(500);
        $(this).hide();
    });
    $('#menu .hidden_menu').on('mouseleave', function () {
        $('#menu .first_menu').show();
        $(this).hide(500);
    });
})