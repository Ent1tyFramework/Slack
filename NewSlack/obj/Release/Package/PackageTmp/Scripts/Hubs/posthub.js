var postHub;
$(function () {
    postHub = $.connection.postHub;

    postHub.client.send = function (content, user) {
        for (var i = 0; i < content.length; i++) {
            var code = `
            <div class="post">
                <div class="post_header">
                    <img class="user_img rounded-circle" src="`+ user.ImagePath + `" />
                    <a href="/user/`+ user.UserName + `" class="user_name text-dark font-weight-light">` + user.FirstName + ' ' + user.LastName + `</a>
                </div>
                <div class="post_body">
                    <img class="post_image img-fluid" src="/Content/Users/Images/`+ user.Id + `/` + content[i] + `" />
                </div>
            </div>`;
            $('div.posts').prepend(code);
            $('div.no_posts').remove();
        }
    }

    postHub.client.error = function (error) {
        alert(error);
    }
    $.connection.hub.start().done();
});
function send(content) {
    $.connection.hub.start().done();
    postHub.server.send(content);
};