'use strict';

define([], function () {

    function stringIsNotEmpty(value) {
        return typeof value === 'string' &&
                value.length > 0;
    }

    return {
        stringIsNotEmpty: stringIsNotEmpty
    };
});