var isCall = false;
var only;
var userId;

$(window).scroll(function () {
    var maxHeight = (document.scrollingElement.scrollHeight - document.scrollingElement.clientHeight) - 300;

    if ($('div.post').length > 0 && document.scrollingElement.scrollTop >= maxHeight && !isCall) {
        data_loading();
    }
});

function data_loading(isFirstCall) {
    //render posts from DB
    $.ajax({
        type: 'GET',
        url: '/Home/Posts',
        data: { skip: $('div.post').length, take: 10, only: only, userId: userId, first: isFirstCall  },
        beforeSend: function () {
            isCall = true;
        },
        success: function (data) {
            var divPosts = $('div.posts');
            $('div.posts').append(data);
            isCall = false;
        },
        error: function (e) { alert(e.statusText); }
    });
}