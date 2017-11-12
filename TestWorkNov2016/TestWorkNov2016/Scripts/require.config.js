require.config({
    baseUrl: '/Scripts/',
    waitSeconds: 0,
    paths: {
        'jquery': 'wwwroot/lib/jquery/dist/jquery',
        'jquery-ui': 'wwwroot/lib/jquery-ui/jquery-ui',
        'knockout': 'wwwroot/lib/knockout/dist/knockout',
        'knockstrap': 'wwwroot/lib/knockstrap/build/knockstrap',
        'knockout-select': 'wwwroot/lib/knockout-select2v4/src/knockout-select2',
        'bootstrap': 'wwwroot/lib/bootstrap-sass/assets/javascripts/bootstrap',
        'toastr': 'wwwroot/lib/toastr/toastr',
        'postbox': 'wwwroot/lib/knockout-postbox/build/knockout-postbox',
        'text': 'wwwroot/lib/text/text',
        'select2': 'wwwroot/lib/select2/dist/js/select2.full',
        'signals': 'wwwroot/lib/js-signals/dist/signals',
        'hasher': 'wwwroot/lib/hasher/dist/js/hasher',
        'crossroads': 'wwwroot/lib/crossroads/dist/crossroads',

        // aliases
        'vm': 'ViewModels',
        'comps': 'Components',
        'utils': 'Utils'
    },
    shim: {
        'bootstrap': { deps: ['jquery', 'jquery-ui'] },
        'select2': { deps: ['jquery'] },
        'knockstrap': { deps: ['jquery', 'bootstrap', 'knockout'] }
    }
});