Array.from($('input[type="date"]')).forEach((e) => {
    //Inicializa los input type date con la fecha actual
    e.value = new Date().toISOString().substring(0, 10);
});


$('#TipoDonacion').on('change', (e) => {
    //Al cambiar el tipo de propuesta muestra los campos exclusivos del tipo de propuesta seleccionado
    const camposExclusivosAMostrarId = '#campos-exclusivos-' + e.target.value;
    const camposExclusivosContainerIds = [
        '#campos-exclusivos-1',
        '#campos-exclusivos-2',
        '#campos-exclusivos-3'
    ];

    camposExclusivosContainerIds
        .filter(containerId => containerId != camposExclusivosAMostrarId)
        .forEach((containerId) => {
            $(containerId).addClass('d-none');
        });

    $(camposExclusivosAMostrarId).removeClass('d-none');
});


const agregarInsumo = () => {
    const nuevoInsumoSnippet = '<div class="form-row insumo"><div class="form-group col-md-3"><label for="Cantidad_{numeroInsumo}_">Cantidad:</label><input class="form-control" id="Cantidad_{numeroInsumo}_" name="Cantidad[{numeroInsumo}]" type="number" value=""></div><div class="form-group col-md-9"><label for="Nombres_{numeroInsumo}_">Nombre insumo:</label><input class="form-control" id="Nombres_{numeroInsumo}_" name="Nombres[{numeroInsumo}]" type="text" value=""></div></div>';
    
    const numeroInsumo = $('.insumo').length;
    $('#contenedor-insumos').append(nuevoInsumoSnippet.replace(/{numeroInsumo}/g, numeroInsumo));
    $('#input-cantidad-insumos').val($('.insumo').length);
    $('#boton-quitar-insumo').removeAttr('disabled');
};

const quitarInsumo = () => {
    $('.insumo').last().remove();
    if ($('.insumo').length === 1) $('#boton-quitar-insumo').attr('disabled', 'disabled');
    $('#input-cantidad-insumos').val($('.insumo').length);
};

$('#Imagen').on('change', function () {
    var fileName = $(this).val().indexOf('fakepath\\') !== -1 ? $(this).val().split('fakepath\\')[1] : $(this).val();
    $(this).next('.custom-file-label').html(fileName);
});