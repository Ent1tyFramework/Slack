$(function () {
    $('.add_post > .btn').click(function () {
        $('#upload_img').trigger('click');
    });
    $('#upload_img').change(function () {
        if (window.FormData !== undefined) {
            var data = new FormData();
            var files = document.getElementById('upload_img').files;

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
                        alert('Error: ' + e.responseText);
                    }
                });
            }
        }
        else alert("Your browser doesn't support uploading files");
    });
});