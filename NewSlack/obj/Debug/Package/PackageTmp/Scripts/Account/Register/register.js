$(function () {
    //On click change color 
    $('.auth .group select option').click(function () {
        $('.group select').css('color', '#000');
    });

    //Birthday
    $('#dateClone').click(function () {
        $('#userBirthday').click();
    });

    $('#userBirthday').change(function () {
        $('#dateClone').val($(this).val()); 
        $('#dateClone').css('color','#000'); 
    });
});