(function () {
    'use strict';

    define(['knockout', 'jquery', 'utils/router', 'select2', 'knockout-select'], function (ko, $, Router) {
        var mainVm = {};

        mainVm.tabNames = { distance: 'distance', upload: 'upload' };

        mainVm.selectedTabName = ko.observable();

        mainVm.isTabSelected = function (tabName) {
            return mainVm.route().tab === tabName;
        };

        mainVm.initialize = function() {
        };

        var router = new Router({
            routes: [
                { url: '', params: { tab: mainVm.tabNames.distance, component: 'distanceForm' } },
                { url: 'distance', params: { tab: mainVm.tabNames.distance, component: 'distanceForm' } },
                { url: 'upload', params: { tab: mainVm.tabNames.upload, component: 'uploadForm' } }
            ]
        });

        mainVm.route = router.currentRoute;

        ko.components.register('distanceForm', { require: 'comps/distanceForm' });
        ko.components.register('uploadForm', { require: 'comps/uploadForm' });

        ko.applyBindings(mainVm);
    });
})();