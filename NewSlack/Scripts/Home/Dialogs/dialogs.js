$(function () {
    $('.hide_dialog img').click(function () {

        var hide = confirm('Hide this dialog?');

        if (hide) {
            var dialogDiv = $(this).parent().parent();
            var dialogId = $(this).parent().siblings('.dialogId').attr('id');

            $.ajax({
                type: 'GET',
                url: '/dialog/hide',
                data: { dialogId },
                success: function (isRemoved) {
                    if (isRemoved) {
                        //remove dialog
                        dialogDiv.remove();
                    }
                },
                error: function (e) {
                    alert(e.statusText);
                }
            });
        }
    });

    $('.dialog .content').click(function () {
        var dialogId = $(this).siblings('.dialogId').attr('id');
        window.location.href = "/dialog/" + dialogId;
    });
});
