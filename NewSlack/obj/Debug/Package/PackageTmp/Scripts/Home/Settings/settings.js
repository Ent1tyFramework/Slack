$(function () {
    //Birthday
    $('#userBirthday').val($('#dateClone').val());
    $('#dateClone').click(function () {
        $('#userBirthday').trigger('click');
    });
    $('#userBirthday').change(function () {
        $('#dateClone').val($(this).val());
        $('#dateClone').css('color','#000');
    });


    //uploading img animation
    $('#change_photo').click(function () {
        $('#upload_img').trigger('click');
    });

    $("#upload_img").change(function () {

        var input = this;
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.user_img img').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    });

    //uploading an img to the server and publishing it to the client
    $('#form_submit').click(function () {
        if (window.FormData !== undefined) {
            var data = new FormData();
            var files = document.getElementById('upload_img').files;

            if (files.length > 0) {
                for (var i = 0; i < files.length; i++) {
                    data.append('file' + i, files[i]);
                }

                $.ajax({
                    type: 'POST',
                    url: '/Home/UploadImage',
                    processData: false,
                    contentType: false,
                    data: data,
                    success: function (paths) {
                        var pathsToArray = JSON.parse(paths);
                        $('#img_name').val(pathsToArray[0]);
                        $('.save_changes input[type=submit]').trigger('click');
                    },
                    error: function (e) {
                        alert('Error: ' + e.statusText);
                    }
                });
            }
            else $('.save_changes input[type=submit]').trigger('click');
        }
        else alert("Your browser doesn't support uploading files");
    });
});