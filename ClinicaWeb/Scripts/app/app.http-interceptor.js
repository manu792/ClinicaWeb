(function () {
    'use strict';
    angular.module('app')
        .config(function ($httpProvider) {
            $httpProvider.interceptors.push(function ($q) {
                return {
                    'response': function (response) {
                        // do something on success
                        console.log('hi');
                        if ($("#spinner-container").length > 0) {
                            $("#spinner-container").hide();
                        }
                        return response;
                    },
                    'responseError': function (response) {
                        //do something on success
                        if ($("#spinner-container").length > 0) {
                            $("#spinner-container").hide();
                        }
                        if (canRecover(rejection)) {
                            return responseOrNewPromise
                        }
                        return $q.reject(rejection);
                    }
                };
            });
        });
})();