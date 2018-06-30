$(function () {
    $('.add_post .container1 .upload').hover(function () {
        $('.add_post .container1 img').attr('src', '/Content/Images/gray_upload.png');
        $('.add_post .container1 h4').css('color', '#d9d9d9');
    }, function () {
        $('.add_post .container1 img').attr('src', '/Content/Images/blue_upload.png');
        $('.add_post .container1 h4').css('color', '#4a76a8');
    });

    //uploading img animation
    $('.add_post .container1 .upload').click(function () {
        $(this).siblings('input[type=file]').trigger('click');
    });

    $(".add_post .container1 input[type=file]").change(function () {

        $('.add_post .container1 .upload h4').text('Upload another photo');
        $('.add_post .container1 .upload').css('width', '300px');
        $('.add_post .container2').removeAttr('hidden');
        $('.add_post .container3').removeAttr('hidden');

        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.add_post .container2 img').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    });

    //uploading an img to the server and publishing it to the client
    $('.add_post .container3').click(function () {
        if (window.FormData !== undefined) {
            var data = new FormData();
            var files = document.getElementById('img_upload').files;

            if (files.length > 0) {
                var file_names = [files.length];

                for (var i = 0; i < files.length; i++) {
                    data.append('file' + i, files[i]);
                    file_names[i] = files[i].name;
                }

                $.ajax({
                    type: 'POST',
                    url: '/Home/UploadImage',
                    processData: false,
                    contentType: false,
                    data: data,
                    success: function () {
                        send(file_names);
                    },
                    error: function (e) {
                        alert('Error: ' + e.responseText)
                    }
                });

                //change animation back
                $('.add_post .container1 .upload h4').text('Upload photo');
                $('.add_post .container1 .upload').css('width', '200px');
                $('.add_post .container2').attr('hidden','');
                $('.add_post .container3').attr('hidden','');
            }
        }
        else alert("Your browser doesn't support uploading files");
    });
})