$(function () {
    //to dialogs
    $('.back_to_dialogs').click(function () {
        window.location.href = "/dialogs";
    });

    //send message
    $('#input_msg').keypress(function (e) {
        if (e.which == 13) {
            var content = $('#input_msg').val();
            $('#input_msg').val('');

            var lastIndex = document.URL.lastIndexOf('/');
            var dialogId = document.URL.substring(lastIndex + 1);

            send(dialogId, content);
        }
    });

    //scroll div to down
    var dialog_content = $('.dialog');
    dialog_content.scrollTop(dialog_content.prop('scrollHeight'));
});