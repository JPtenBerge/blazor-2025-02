export function showMap(objReference, id, lat, lon, msg) {
    const map = L.map(`map-${id}`).setView([lat, lon], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var marker = L.marker([lat, lon]).addTo(map);

    marker.bindPopup(msg).openPopup();

    map.on('click', (e) => {
        const popup = L.popup();
        popup.setLatLng(e.latlng).setContent(msg).openOn(map);
        marker.setLatLng(e.latlng);
        objReference.invokeMethodAsync('UpdateCoordinates', e.latlng.lat, e.latlng.lng);
    });
}