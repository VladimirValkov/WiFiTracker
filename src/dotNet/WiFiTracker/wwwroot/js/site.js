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

function addMarkerToMap(pointlatlng, title) {
    var marker = new google.maps.Marker({
        position: pointlatlng,
        title: title,
        map: gMap,
        draggable: false
    });
    return marker;
    
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