$(function () {
    $('.user_img img').click(function () {
        $('#upload_image').trigger('click');
    });
    $('#upload_image').change(function () {
        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.user_img img').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    });
});