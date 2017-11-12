'use strict';

define(['jquery'], function ($) {

    function getData(url, callback, errorCallback) {
        $.ajax({
            method: "GET",
            url: url,
            dataType: "json",
            success: function(data) {
                $(document).ready(function() {
                    callback(data);
                });
            },
            error: function(httpRequest, textStatus, errorThrown) {
                //log
                if (errorCallback) errorCallback();
            }
        });
    }

    function postData(url, data, callback, errorCallback) {
        $.ajax({
            method: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            
            success: function (response) {
                $(document).ready(function () {
                    callback(response);
                });
            },
            error: function (httpRequest, textStatus, errorThrown) {
                //log
                if (errorCallback) errorCallback(httpRequest, textStatus, errorThrown);
            }
        });
    }

    function uploadData(url, data, callback, errorCallback) {
        $.ajax({
            method: "POST",
            url: url,
            contentType: false,
            processData: false,
            data: data,

            success: function (response) {
                $(document).ready(function () {
                    callback(response);
                });
            },
            error: function (httpRequest, textStatus, errorThrown) {
                //log
                if (errorCallback) errorCallback(httpRequest, textStatus, errorThrown);
            }
        });
    }

    return {
        getData: getData,
        postData: postData,
        uploadData: uploadData
    };
});