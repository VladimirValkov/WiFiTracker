// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function generateId(elementId) {
    var random = 'xxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });

    $("#" + elementId).val(random);
}

var path_to_delete;

$(".deleteItem").click(function (e) {
    path_to_delete = $(this).data('path');
});

$('#btnContinueDelete').click(function () {
    window.location = path_to_delete;
});

var gMap;
function initializeGMap(contrainerId, Oldlon, Oldlat) {
    mapcode = new google.maps.Geocoder();
    var loc = {};
    if (google.loader.ClientLocation) {
        loc.lat = google.loader.ClientLocation.latitude;
        loc.lng = google.loader.ClientLocation.longitude;
    }
    else {
        if (Oldlat != null && Oldlon != null) {
            loc.lat = Oldlat;
            loc.lng = Oldlon;
        }
        else {
            loc.lat = 42.68583;
            loc.lng = 26.32917;
        }
    }

    var lnt = new google.maps.LatLng(loc.lat, loc.lng);
    var diagChoice = {
        zoom: 15,
        center: lnt,
        diagId: google.maps.MapTypeId.ROADMAP
    }
    gMap = new google.maps.Map(document.getElementById(contrainerId), diagChoice);


}



function TransmittersCoords(contrainerId, Oldlon, Oldlat) {
    initializeGMap(contrainerId, Oldlon, Oldlat);
    var marker = addMarkerToMap(new google.maps.LatLng(Oldlat, Oldlon), "");
    google.maps.event.addListener(gMap, 'click', function (args) {
        console.log('latlng', args.latLng);
        if (marker != null) {
            marker.setMap(null);
        }
        marker = addMarkerToMap(args.latLng, null)
        $('#lonBox').val(args.latLng.lng());
        $('#latBox').val(args.latLng.lat());
    });
}

function addMarkerToMap(pointlatlng, title, blue = false) {

    const blueMarker = {
        path: "M10.453 14.016l6.563-6.609-1.406-1.406-5.156 5.203-2.063-2.109-1.406 1.406zM12 2.016q2.906 0 4.945 2.039t2.039 4.945q0 1.453-0.727 3.328t-1.758 3.516-2.039 3.070-1.711 2.273l-0.75 0.797q-0.281-0.328-0.75-0.867t-1.688-2.156-2.133-3.141-1.664-3.445-0.75-3.375q0-2.906 2.039-4.945t4.945-2.039z",
        fillColor: "blue",
        fillOpacity: 0.6,
        strokeWeight: 0,
        rotation: 0,
        scale: 2,
        anchor: new google.maps.Point(15, 30),
    };

    if (blue) {
        return new google.maps.Marker({
            position: pointlatlng,
            title: title,
            icon: blueMarker,
            map: gMap,
            draggable: false
        });
    } else {
        return new google.maps.Marker({
            position: pointlatlng,
            title: title,
            map: gMap,
            draggable: false
        });
    }
}

function TrackHistory(contrainerId, data) {
    if (data.length > 0) {
        initializeGMap(contrainerId, data[0].lon, data[0].lat)
    } else {
        initializeGMap(contrainerId)
    }

    
    const routes = [];
    data.forEach(e => {
        var l = { lat: e.lat, lng: e.lon }
        routes.push(l)
        addMarkerToMap(l, e.name)
    })
    const flightPath = new google.maps.Polyline({
        path: routes,
        geodesic: true,
        strokeColor: "#FF0000",
        strokeOpacity: 1.0,
        strokeWeight: 2,
    });

    flightPath.setMap(gMap);
}

function LiveView(contrainerId, data) {
    if (data.length > 0) {
        initializeGMap(contrainerId, data[0].lon, data[0].lat)
    } else {
        initializeGMap(contrainerId)
    }

    gMap.center
    data.forEach(e => {
        var l = { lat: e.lat, lng: e.lon }
        addMarkerToMap(l, e.name)
    })
}

function CenterMarker(latitudes, longitutes) {
    var lnt = new google.maps.LatLng(latitudes, longitutes);
    gMap.setCenter(lnt)
    gMap.setZoom(18)
}