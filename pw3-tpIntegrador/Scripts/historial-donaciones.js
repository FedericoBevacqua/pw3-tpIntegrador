let pedirHistorial = (idUsuario) => {
    const cardSnippet = '<div class="card"><img src="{Foto}" class="card-img-top" alt="Imagen de propuesta" onerror="this.src=\'https://www.loottis.com/wp-content/uploads/2014/10/default-img.gif\'"><div class="card-body"><h5 class="card-title">{Nombre}</h5><div class="card-subtitle mb-2 text-muted">{TipoDonacion}</div><div class="card-text">Donaste <span class="font-weight-bold">{MiDonacion}</span></div><div class="card-text">Total recaudado: {TotalRecaudado}</div><a href="{LinkAPublicacion}" class="btn btn-primary mt-4">Ver publicación</a></div><div class="card-footer"><div>Publicación {Estado}</div></div></div>';
    $.get(window.location.origin + "/api/HistorialDonaciones/" + idUsuario, function (data) {
        Array.from(data).forEach((e) => {
            let card = cardSnippet.replace("{Foto}", e.Foto)
                .replace("{Nombre}", e.Nombre)
                .replace("{TipoDonacion}", e.TipoDonacion)
                .replace("{MiDonacion}", e.MiDonacion)
                .replace("{TotalRecaudado}", e.TotalRecaudado)
                .replace("{LinkAPublicacion}", e.LinkAPublicacion)
                .replace("{Estado}", e.Estado);

            $('#contenedor-historial-donaciones').append(card);
        })
    });
}