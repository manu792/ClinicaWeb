(function () {
    'use strict';
    angular.module('app')
        .config(function ($routeProvider) {
            $routeProvider
                .when("/", {
                    templateUrl: "login"
                })
                .when("/pacientes", {
                    templateUrl: "listaPacientes"
                })
                .when("/detalle", {
                    templateUrl: "detalle",
                })
                .otherwise({ redirectTo: '/' });

        })
        .run(function ($rootScope, $location, $cookieStore, $http) {
                // keep user logged in after page refresh
                $rootScope.globals = $cookieStore.get('globals') || {};
                if ($rootScope.globals.currentUser) {
                    $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata;
                }

                $rootScope.$on('$locationChangeStart', function (event, next, current) {
                    // send to login page if not logged in
                    if ($location.path() !== '/' && !$rootScope.globals.currentUser) {
                        $location.path('/');
                    }
                });
        });
})();