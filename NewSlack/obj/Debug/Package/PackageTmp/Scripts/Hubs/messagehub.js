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

        var code = `
    <div class="message">
        <div class="message_header">
            <img src="`+ user.ImagePath + `" alt="">
            <a href="/user/`+ user.UserName + `">` + user.FirstName + ` ` + user.LastName + `</a>
        </div>
        <div class="message_content">
            <p>`+ content + `</p>
    </div>`;

        var dialog_content = $('.dialog_content');

        if ($('.messages').length == 0) {
            dialog_content.empty();
            dialog_content.append('<div class="messages"></div>');

            //attach messages div to bottom of .dialogs_content
            var full_height = $('.dialog_content').height();
            var messages_height = $('.messages').height();

            if (messages_height < full_height) {
                $('.messages').css('margin-top', (full_height - messages_height) - 20);
            }
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