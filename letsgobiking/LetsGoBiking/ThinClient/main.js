$(async function () {
    var map = new ol.Map({
        target: 'map', // <-- This is the id of the div in which the map will be built.
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            })
        ],

        view: new ol.View({
            center: ol.proj.fromLonLat([7.0985774, 43.6365619]), // <-- Those are the GPS coordinates to center the map to.
            zoom: 10 // You can adjust the default zoom.
        })

    });

    var lineStyle = new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: '#ffcc33',
            width: 100
        })
    });

    async function api(endpoint) {
        return JSON.parse(await (await fetch($("#apiUrl").val() + "rest/" + endpoint)).json());
    }

    async function computeRoute() {
        const geojson = await api("GetRoute/7.0985774,43.6365619/8.687872,49.420318");
        const vectorSource = new ol.source.Vector({
            features: new ol.format.GeoJSON().readFeatures(geojson),
        });
        vectorSource.transform('EPSG:4326', 'EPSG:3857');
        vectorSource.addFeature(new ol.Feature(new ol.geom.Circle([5e6, 7e6], 1e6)));
        const vectorLayer = new ol.layer.Vector({
            source: vectorSource,
            style: [lineStyle]
        });
        map.addLayer(vectorLayer);
    }

    await computeRoute();
});
