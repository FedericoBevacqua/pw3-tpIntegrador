
$('#boton-me-gusta').on('click', function (event) {
    event.preventDefault();

    event.target.classList.add('btn-primary');
    event.target.classList.remove('btn-secondary');
    $('#boton-no-me-gusta').addClass('btn-secondary');
    $('#boton-no-me-gusta').removeClass('btn-primary');


    var urlAjax = event.currentTarget.href;

    $.post(urlAjax, function (data) {
        $('#valoracion-span').html(data);
    });
})


$('#boton-no-me-gusta').on('click', function (event) {
    event.preventDefault();
    event.target.classList.add('btn-primary');
    event.target.classList.remove('btn-secondary');
    $('#boton-me-gusta').addClass('btn-secondary');
    $('#boton-me-gusta').removeClass('btn-primary');

    var urlAjax = event.currentTarget.href;

    $.post(urlAjax, function (data) {
        $('#valoracion-span').html(data);
    });
})