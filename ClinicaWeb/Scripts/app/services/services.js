(function () {
    'use strict';

    var url = 'http://localhost:8459/';

    angular.module('app')
        .factory("PacienteService", function ($http) {

            var factory = {
                savePaciente: function (paciente, callback, errorCallback, finallyCallback) {
                    $http.post(url + '/api/pacientes', paciente)
                        .then(function (data) {
                            callback(data);
                        }, function (error) {
                            errorCallback(error);
                        }).finally(function () {
                            finallyCallback();
                        });
                },

                getPacientes: function (callback, errorCallback, finallyCallback) {
                    $http.get(url + '/api/pacientes')
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        }).finally(function () {
                            finallyCallback();
                        });
                },

                getPacientePorId: function (id, callback, errorCallback, finallyCallback) {
                    $http.get(url + '/api/pacientes/' + id)
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        }).finally(function () {
                            finallyCallback();
                        });
                },

                editPaciente: function (paciente, callback, errorCallback) {
                    $http.put(url + '/api/pacientes/' + paciente.PacienteId, paciente)
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        });
                },

                deletePaciente: function (pacienteId, callback, errorCallback) {
                    $http.delete(url + '/api/pacientes/' + pacienteId)
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        });
                }
            }
            return factory;
        })
        .factory("TratamientoService", function ($http) {

            var factory = {
                editTratamiento: function (tratamiento, callback, errorCallback) {
                    $http.put(url + '/api/tratamientos/' + tratamiento.Id, tratamiento)
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        });
                },

                saveTratamiento: function (tratamiento, callback, errorCallback, finallyCallback) {
                    $http.post(url + '/api/tratamientos', tratamiento)
                        .then(function (data) {
                            callback(data);
                        }, function (error) {
                            errorCallback(error);
                        }).finally(function () {
                            finallyCallback();
                        });
                },
                deleteTratamiento: function (tratamientoId, callback, errorCallback) {
                    $http.delete(url + '/api/tratamientos/' + tratamientoId)
                        .then(function (response) {
                            callback(response.data);
                        }, function (error) {
                            errorCallback(error);
                        });
                }
            }
            return factory;
        })
})();