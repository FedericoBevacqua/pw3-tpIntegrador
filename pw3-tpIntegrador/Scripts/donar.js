$('#donar-submit').on('click', (e) => {
    e.preventDefault();
    let inputCantidadesDonadas = $('.cantidad-insumo-donado');
    if (inputCantidadesDonadas.length !== 0) {
        let seDonoAlMenosUnInsumo = false;
        Array.from(inputCantidadesDonadas).forEach((input) => {
            if (input.value > 0) seDonoAlMenosUnInsumo = true;
        });
        if (!seDonoAlMenosUnInsumo) {
            alert('Se debe donar al menos un insumo para poder continuar')
        } else {
            $('#donar-formulario').submit();
        }
    } else {
        $('#donar-formulario').submit();
    }
})