function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    const earthRadius = 6371;
    const dLat = deg2rad(lat2 - lat1);
    const dLon = deg2rad(lon2 - lon1);
    const a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
    ;
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return earthRadius * c;
}

function deg2rad(deg) {
    return deg * Math.PI / 180;
}

$(async function () {
    const map = new ol.Map({
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

    const bikestyle = new ol.style.Style({
        image: new ol.style.Icon({
            anchor: [0.5, 1],
            scale: [0.08, 0.08],
            anchorXUnits: 'fraction',
            anchorYUnits: 'fraction',
            src: 'marker.png'
        })
    });

    async function apiGet(endpoint) {
        return await (await fetch($("#apiUrl").val() + "rest/" + endpoint)).json();
    }

    async function apiPost(endpoint, body) {
        return await (await fetch($("#apiUrl").val() + "rest/" + endpoint, {
            method: "POST",
            body: JSON.stringify(body),
            headers: {
                "Content-Type": "application/json"
            }
        })).json();
    }

    const stations = await apiGet("GetStations");
    const stationsSource = new ol.source.Vector({
        features: stations.map(station => {
            return new ol.Feature({
                geometry: new ol.geom.Point(ol.proj.fromLonLat([station.position.longitude, station.position.latitude])),
                name: station.name
            });
        }),
    });
    map.addLayer(new ol.layer.Vector({
        source: stationsSource,
        style: bikestyle,
        zIndex: 10
    }));
    map.getView().fit(stationsSource.getExtent(), {
        size: map.getSize(),
        padding: [50, 50, 50, 50]
    });

    let requested = false;
    let loading = false;

    async function setacdata(ac) {
        if (loading) {
            return;
        }
        const value = ac.field.value;
        if (!value) {
            ac.setData([]);
        } else {
            loading = true;
            const [longitude, latitude] = ol.proj.toLonLat(map.getView().getCenter());
            const geocode = JSON.parse(await apiPost("Geocode", {
                query: value,
                focus: {
                    longitude,
                    latitude
                }
            }));
            ac.setData(geocode.features.map(f => {
                return {
                    label: f.properties.label,
                    value: JSON.stringify(f.geometry.coordinates)
                };
            }));
            loading = false;
        }
        requested = false;
        setTimeout(() => {
            if (ac.field.value !== value) {
                requested = true;
            }
            if (requested) {
                setacdata(ac);
            } else {
                requested = false;
            }
        }, 200);
    }

    let computing = false;
    let routeLayer = null;

    const colors = ["red", "green", "blue"];

    const steplist = $("#steps");

    async function updateRoute() {
        if (computing) return;
        if (!acStart.value || !acEnd.value) {
            return;
        }
        computing = true;
        const [startLon, startLat] = acStart.value;
        const [endLon, endLat] = acEnd.value;

        const start = {"longitude": startLon, "latitude": startLat};
        const end = {"longitude": endLon, "latitude": endLat};
        const geojson = await apiPost("GetRoute", {start, end});
        const group = new ol.layer.Group();
        const features = [];
        const steps = [];
        for (let [i, path] of Object.entries(geojson)) {
            const data = JSON.parse(path);
            steps.push(...data.features[0].properties.segments[0].steps);
            const points = new ol.format.GeoJSON().readFeatures(data, {
                dataProjection: 'EPSG:4326',
                featureProjection: 'EPSG:3857',
            });
            features.push(...points);
            const vectorSource = new ol.source.Vector({
                features: points
            });
            const layer = new ol.layer.Vector({
                source: vectorSource,
                style: [new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        color: colors[i],
                        width: 10
                    })
                })]
            });
            group.getLayers().push(layer);
        }
        steplist.empty();
        for (let step of steps) {
            steplist.append($("<li>").addClass("list-group-item").text(step.instruction))
        }
        $("#routeInfo").show();
        map.removeLayer(routeLayer);
        map.addLayer(routeLayer = group);
        map.getView().fit(new ol.source.Vector({features}).getExtent(), {
            size: map.getSize(),
            padding: [50, 50, 50, 50]
        });
        computing = false;
    }

    const acStart = new Autocomplete(document.getElementById("posStart"), {
        onSelectItem: () => {
            updateRoute();
        },
        onInput: () => {
            if (!requested) {
                requested = true;
                setacdata(acStart);
            } else {
                requested = true;
            }
        }
    });

    const acEnd = new Autocomplete(document.getElementById("posEnd"), {
        onSelectItem: () => {
            updateRoute();
        },
        onInput: () => {
            if (!requested) {
                requested = true;
                setacdata(acEnd);
            } else {
                requested = true;
            }
        }
    });
});
