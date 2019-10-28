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
    const nuevoInsumoSnippet = '<div class="form-row insumo"><div class="form-group col-md-2"><label for="CantidadInsumo_{numeroInsumo}_">Cantidad:</label><input class="form-control" id="CantidadInsumo_{numeroInsumo}_" name="CantidadInsumo[{numeroInsumo}]" type="number" value=""></div><div class="form-group col-md-4"><label for="NombreInsumo_{numeroInsumo}_">Nombre insumo:</label><input class="form-control" id="NombreInsumo_{numeroInsumo}_" name="NombreInsumo[{numeroInsumo}]" type="text" value=""></div><div class="form-group col-md-6"><label for="DescripcionInsumo_{numeroInsumo}_">Descripción del insumo:</label><input class="form-control" id="DescripcionInsumo_{numeroInsumo}_" name="DescripcionInsumo[{numeroInsumo}]" type="text" value=""></div></div>';
    const numeroInsumo = $('.insumo').length;
    $('#contenedor-insumos').append(nuevoInsumoSnippet.replace(/{numeroInsumo}/g, numeroInsumo));
    $('#boton-quitar-insumo').removeAttr('disabled');
};

const quitarInsumo = () => {
    $('.insumo').last().remove();
    if ($('.insumo').length === 1) $('#boton-quitar-insumo').attr('disabled', 'disabled');
};

$('#Imagen').on('change', function () {
    var fileName = $(this).val().indexOf('fakepath\\') !== -1 ? $(this).val().split('fakepath\\')[1] : $(this).val();
    $(this).next('.custom-file-label').html(fileName);
});