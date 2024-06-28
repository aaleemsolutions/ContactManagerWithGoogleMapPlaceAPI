
function initMap(initialAddress = null) {
    var mapElements = document.getElementsByClassName('map');
    var addressInputs = document.getElementsByClassName('addressMapper');

    // Loop through all elements with class 'map'
    for (var i = 0; i < mapElements.length; i++) {
        var mapElement = mapElements[i];
        var addressInput = addressInputs[i];

        var map = new google.maps.Map(mapElement, {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

        var marker = new google.maps.Marker({
            map: map,
            draggable: true,
            animation: google.maps.Animation.DROP
        });


        if (initialAddress) {
            // Set initial marker position based on initialAddress
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': initialAddress }, function (results, status) {
                if (status === 'OK') {
                    map.setCenter(results[0].geometry.location);
                    marker.setPosition(results[0].geometry.location);
                    marker.setVisible(true);
                } else {
                    console.error('Geocode was not successful for the following reason: ' + status);
                }
            });
        }

        var autocomplete = new google.maps.places.Autocomplete(addressInput, { types: ['geocode'] });


        autocomplete.addListener('place_changed', function () {
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                return;
            }

            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);
            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);
            }

            marker.setPosition(place.geometry.location);
            marker.setVisible(true);
        });
    }
}
