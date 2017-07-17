(function () {
    'use strict';
    angular.module('app').
        controller('LoginController', function ($scope, $location, $rootScope, AuthenticationService) {
            $scope.usernameRequiredMessage = false;
            $scope.passwordRequiredMessage = false;
            $scope.error = '';

            // reset login status
            AuthenticationService.ClearCredentials();

            $scope.login = function () {
                $scope.error = '';
                AuthenticationService.Login($scope.username, $scope.password, function (response) {
                    if (response.data.success) {
                        AuthenticationService.SetCredentials($scope.username, $scope.password);
                        $location.path('/pacientes');
                    } else {
                        $scope.error = response.data.message;
                    }
                });
            };
        });
})();