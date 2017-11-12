'use strict';

define(['knockout', 'utils/serverDataProvider', 'utils/utilityFunctions', 'utils/endpoints'], function (ko, serverDataProvider, utilityFunctions, endpoints) {

    function uploadFormViewModel() {
        var self = this;

        self.selectedFileName = ko.observable();

        self.canUpload = ko.computed(function () {
            return utilityFunctions.stringIsNotEmpty(self.selectedFileName());
        });

        self.uploadBtnTooltip = ko.computed(function() {
            return self.canUpload() ?
                'Confirm' : 'Select a file first';
        });

        self.initialize = function() {
        };

        self.uploadFile = function () {
            var files = document.getElementById('inputFile').files;
            files = [].slice.call(files);

            if (files.length < 1) {
                return;
            }

            var uploadData = new FormData();
            files.forEach(function(file, i) {
                uploadData.append("file " + i, file);
            });

            serverDataProvider.uploadData(endpoints.uploadFile, uploadData, self.onSuccess, self.onFailure);
        };

        self.onSuccess = function (data) {
            var message = 'stations parsed: [' + data.successfulOperationsCount +
                ']. errors [' + data.unsuccessfulOperationsCount + ']. ' + 
                'new stations (with unique names) added: [' + data.addedCount + '].';

            //toastr should go here
            alert(message);
        };

        self.onFailure = function () {
            //toastr should go here
            alert('error occured');
        };

        return self;
    }

    return uploadFormViewModel;
});