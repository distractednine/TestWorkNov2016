'use strict';

define(['knockout', 'vm/distanceFormViewModel', 'text!/Templates/DistanceForm'], function (ko, viewModel, template) {

    function createViewModel(params) {
        var vm = new viewModel(params);
        vm.initialize();

        return vm;
    }

    return { viewModel: { createViewModel: createViewModel }, template: template };
});