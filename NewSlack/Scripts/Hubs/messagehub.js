var messageHub;

$(function () {
    messageHub = $.connection.messageHub;

    messageHub.client.send = function (user, content, dialogId) {

        if (document.URL.endsWith("/dialogs")) {
            sendToDialogs(user, content, dialogId);
        }
        else {
            sendToDialog(user, content, dialogId);
        }
    }

    function sendToDialog(user, content, dialogId) {

        var code = `<div class="message row">
                        <div class="user_img">
                            <img src="`+ user.ImagePath +`" class="rounded-circle" />
                        </div>
                        <div class="content col-10 text-light">
                            <div>
                                <a class="font-weight-bold text-light" href="/user/`+ user.UserName + `">` + user.FirstName + ` ` + user.LastName +`</a>
                            </div>
                            <div>
                                <p>`+ content + `</p>
                            </div>
                        </div>
                    </div>`;

        var dialog_content = $('.dialog_content');

        if ($('.message').length == 0) {
            dialog_content.empty();
            dialog_content.append('<div class="messages"></div>');
        }

        var lastIndex = document.URL.lastIndexOf('/');
        var urlDialogId = document.URL.substring(lastIndex + 1);

        if (urlDialogId == dialogId) {
            $('.messages').append(code);
        }

        //scroll to down
        var dialog_content = $('.dialog_content');
        dialog_content.scrollTop(dialog_content.prop('scrollHeight'));
    }

    function sendToDialogs(user, content, dialogId) {
        var code = `
<div class="dialog_name">
    <a href="/user/`+ user.UserName + `">` + user.FirstName + ` ` + user.LastName + `</a>
</div>
<div class="dialog_last_msg">
    <p>`+ content + `</p>
</div>`;

        var dialog_info = $('.dialogId[id=' + dialogId + ']').siblings('.dialog_content').children('.dialog_info');

        dialog_info.empty();
        dialog_info.append(code);
    }

    $.connection.hub.start().done();
});

function send(dialogId, content) {
    $.connection.hub.start().done(function () {
        messageHub.server.send(dialogId, content);
        $('.type_message textarea').val('');
    });
}