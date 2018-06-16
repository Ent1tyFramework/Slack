$(function () {
    //animation on hover on .back_to_dialogs
    $('.back_to_dialogs').hover(function () {
        $(this).children('img').attr('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAACKSURBVEhLYxgFlALPsuU53uUr9KFc+gDP0hXtXqXLP3gXr9CBCtEewC0tW2kGFaI9GLWUpmDUUpqCUUtpCrxKlxUCLf1MV0tBAFjoe3qVLf8B9HUKVIh+wLtkWeCo5VAh+oFRy0ctpysYHJaXrUyFCtEPICxf5goVoh8AWmzlW7lMHModBVDAwAAA93WTYoZn6s0AAAAASUVORK5CYII=');
        $(this).children('h4').css('color', '#4a76a8');
    }, function () {
        $(this).children('img').attr('src', 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAYAAAA7MK6iAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAB9SURBVEhL7c+9CYAwGIThbOESgp2FvYWlO2hn4xTOk5/BBBtLTzjBXr4Pi3sghFzzkiBf5ZwXnIZPHwhuKaUdd83J3ivacrKnqClFTSlqCsEVwcM1ekN4wDlxJk5+8NtRcU5+FFfc1S/iuGdOfp54jLHn5KeU0iFc8SkUwgUtrsfc94yoWQAAAABJRU5ErkJggg==');
        $(this).children('h4').css('color', '#b3b3b3');
    });

    //to dialogs
    $('.back_to_dialogs').click(function () {
        window.location.href = "/dialogs";
    });

    //animation on input in the textarea
    var textarea = $('.dialog_footer .type_message textarea');

    if (textarea.val().length > 0)
        textarea.val('');

    textarea.on('input', function () {
        if ($(this).val().length > 0) {
            $('.dialog_footer .send_message').removeAttr('hidden');
            $(this).css('max-width', '710px');
            $(this).css('min-width', '710px');
        }
        else {
            $('.dialog_footer .send_message').attr('hidden', '');
            $(this).css('max-width', '220px');
            $(this).css('min-width', '220px');
        }
    });

    //attach messages div to bottom of .dialogs_content
    var full_height = $('.dialog_content').height();
    var messages_height = $('.messages').height();

    if (messages_height < full_height) {
        $('.messages').css('margin-top', (full_height - messages_height) - 20);
    }

    //send message
    $('.send_message input').click(function () {
        var content = $('.type_message textarea').val();

        var lastIndex = document.URL.lastIndexOf('/');
        var dialogId = document.URL.substring(lastIndex + 1);

        send(dialogId, content);
    });

    //scroll div to down
    var dialog_content = $('.dialog_content');
    dialog_content.scrollTop(dialog_content.prop('scrollHeight'));
});