var postHub;
$(function () {
    postHub = $.connection.postHub;

    postHub.client.send = function (content, user) {
        for (var i = 0; i < content.length; i++) {
            var code = `
                <div class="post">
                    <div class="post_header clearfix">
                        <div class="about_user left">
                            <img src="`+user.ImagePath+`" alt="">
                            <a href="/user/`+user.UserName+`">`+ user.FirstName + ' ' + user.LastName + `</a>
                        </div>
                    </div>
                    <div class="post_content">
                        <div class="img">
                            <img src="/Content/Users/Images/`+ user.Id + `/` + content[i] + `" alt="">
                        </div>
                        <div class="like" hidden>
                            <img src="/Content/Images/white_like.png" alt="">
                        </div>
                    </div>
                </div>`;
            $('div.posts').prepend(code);
        }
    }

    postHub.client.error = function (error) {
        alert(error);
    }

    $.connection.hub.start().done();
});
function send(content) {
    postHub.server.send(content);
};