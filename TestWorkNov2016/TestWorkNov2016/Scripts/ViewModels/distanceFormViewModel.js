'use strict';

define(['knockout', 'jquery', 'utils/utilityFunctions', 'utils/distanceCalculator', 'utils/serverDataProvider', 'utils/endpoints'],
    function (ko, $, utilityFunctions, distanceCalculator, serverDataProvider, endpoints) {

        function distanceFormViewModel() {
            var self = this;

            self.slect1Id = 'station1Input';
            self.slect2Id = 'station2Input';

            self.distanceResult = ko.observable();

            self.data = [];

            self.station1Name = ko.observable();
            self.station1Name.subscribe(function(val) {
                self.resetDistance();
            });

            self.station2Name = ko.observable();
            self.station2Name.subscribe(function (val) {
                self.resetDistance();
            });

            self.stationOptions = {};

            self.stationDataNames = ko.observableArray();

            self.stationData = ko.observableArray();
            self.stationData.subscribe(function (arr) {
                self.stationDataNames(['']);
                arr.forEach(function (val) {
                    self.stationDataNames.push(val.name);
                });
                self.setStationOptions();
                self.selectOptionsInitialized(true);
            });

            self.selectOptionsInitialized = ko.observable(false);

            self.isDistanceVisible = ko.computed(function() {
                return !!self.distanceResult();
            });

            self.canCalculate = ko.computed(function() {
                return utilityFunctions.stringIsNotEmpty(self.station1Name()) &&
                    utilityFunctions.stringIsNotEmpty(self.station2Name());
            });

            self.calculateBtnTooltip = ko.computed(function() {
                return self.canCalculate() ?
                    'Confirm' : 'Select both station names';
            });

            self.initialize = function () {
                serverDataProvider.getData(endpoints.getStations, self.onSuccess, self.onFailure);
            };

            self.onSuccess = function(response) {
                self.stationData(response.data);
            };

            self.onError = function () {
                //toastr should go here
                alert('error occurred!');
            };

            self.setStationOptions = function () {
                self.stationOptions = {
                    data: self.stationDataNames(),
                    placeholder: 'select a station name',
                    multiple: false,
                    allowClear: true
                };
            };

            self.resetDistance = function() {
                self.distanceResult(null);
            };

            self.findStation = function(name) {
                var predicate = function(item) {
                    return item.name === name;
                };

                return ko.utils.arrayFirst(self.stationData(), predicate);
            }

            self.calculateDistance = function () {
                var station1 = self.findStation(self.station1Name());
                var station2 = self.findStation(self.station2Name());

                var distance = null;
                try {
                    distance = distanceCalculator.getDistanceFromLatLonInKm(
                        station1.latitude, station1.longitude, station2.latitude, station2.longitude);
                } catch (exception){
                    self.onError();
                }

                self.distanceResult(distance);
            };

        return self;
    }

    return distanceFormViewModel;
});