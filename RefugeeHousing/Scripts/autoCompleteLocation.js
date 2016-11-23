﻿var autocomplete;

function initAutocomplete() {
    autocomplete = new google.maps.places.Autocomplete(document.getElementById("location"), {
        componentRestrictions: { "country": "gr" }
    });
    autocomplete.addListener("place_changed", placeChanged);
}

function placeChanged() {
    var place = autocomplete.getPlace();
    if (!place.geometry) {
        window.alert("No details available for input: '" + place.name + "'");
    } else {
        document.getElementById("place_id").value = place.place_id;
    }
}