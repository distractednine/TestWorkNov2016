'use strict';

define(['knockout', 'vm/uploadFormViewModel', 'text!/Templates/UploadForm'], function (ko, viewModel, template) {

    function createViewModel(params) {
        var vm = new viewModel(params);
        vm.initialize();

        return vm;
    }

    return { viewModel: { createViewModel: createViewModel }, template: template };
});