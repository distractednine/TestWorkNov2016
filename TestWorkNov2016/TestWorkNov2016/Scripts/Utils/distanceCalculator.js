'use strict';

define([], function () {
    // ReSharper disable once InconsistentNaming
    var R = 6371; // Radius of the earth in km

    function deg2Rad(deg) {
        return deg * (Math.PI / 180);
    }
    function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
        var dLat = deg2Rad(lat2 - lat1);  // deg2rad below
        var dLon = deg2Rad(lon2 - lon1);
        var a =
          Math.sin(dLat / 2) * Math.sin(dLat / 2) +
          Math.cos(deg2Rad(lat1)) * Math.cos(deg2Rad(lat2)) *
          Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var d = R * c; // Distance in km
        return d;
    }

    return {
        getDistanceFromLatLonInKm: getDistanceFromLatLonInKm
    };
});