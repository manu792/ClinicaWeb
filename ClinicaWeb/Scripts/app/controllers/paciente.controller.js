(function () {
    'use strict';
    angular.module('app').
        controller('PacienteController', function ($scope, $location, $rootScope, PacienteService) {
            
            $scope.gridData = [];

            //Modal
            $scope.modalWrapper = document.getElementById('addModal');
            $scope.paciente = {};
            $scope.modalToDisplay = '';
            $scope.pacienteFechaUltimaVisitaVisible = false;
            $scope.pacienteFechaProximaVisitaVisible = false;

            $scope.gridOptions = {
                data: 'gridData',
                columnDefs: [
                    { field: 'PacienteId', displayName: 'Cedula', width: 150 },
                    { field: 'Nombre', width: 150 },
                    { field: 'Edad', width: 100 },
                    { field: 'Contacto', width: 150},
                    { field: 'FechaUltimaVisita', width: 180 },
                    { field: 'FechaProximaVisita', width: 180 },
                    { field: 'actions', displayName: 'Acciones', cellTemplate: '<button ng-click="grid.appScope.detalles(row)" class="btn btn-info">Ver Detalles</button><button ng-click="grid.appScope.eliminar(row)" class="btn btn-danger">Eliminar</button>', width: 200 }

                ],
                enableSorting: true,
                enableFiltering: true,
                flatEntityAccess: true,
                fastWatch: true,
                showGridFooter: true,
                showColumnMenu: true
            };

            $scope.cerrarModal = function () {
                $scope.modalWrapper.style.display = "none";
                $scope.paciente = {};
            }

            $scope.togglePacienteFechaUltimaVisitaDatepicker = function () {
                $scope.pacienteFechaUltimaVisitaVisible = !$scope.pacienteFechaUltimaVisitaVisible;
            }

            $scope.togglePacienteFechaProximaVisitaDatepicker = function () {
                $scope.pacienteFechaProximaVisitaVisible = !$scope.pacienteFechaProximaVisitaVisible;
            }

            $scope.guardarPaciente = function () {
                PacienteService.savePaciente($scope.paciente, function (data) {
                    alert('Paciente agregado correctamente');
                    $scope.gridData.push($scope.paciente);
                    $scope.cerrarModal();
                }, function (error) {
                    alert('Se produjo un error. Verifique que los datos sean validos');
                }, function () {
                    $scope.paciente = {};
                });
            }

            $scope.agregar = function () {
                $scope.modalToDisplay = 'agregarPaciente';
                $scope.modalWrapper.style.display = 'block';
            }

            $scope.eliminar = function (row) {
                PacienteService.deletePaciente(row.entity.PacienteId, function (data) {
                    eliminarEntity(row.entity);
                }, function (error) {
                    alert('Se produjo un error. Contacte al administrador para resolver el problema');
                });
            }

            $scope.detalles = function (row) {
                sessionStorage.pacienteId = row.entity.PacienteId;
                $location.path("/detalle");
            }

            PacienteService.getPacientes(function (data) {
                convertFechas(data);
                $scope.gridData = data;
            });

            function convertFechas(pacientes) {
                for (var i = 0; i < pacientes.length; i++) {
                    pacientes[i].FechaUltimaVisita = formatDate(new Date(pacientes[i].FechaUltimaVisita));
                    pacientes[i].FechaProximaVisita = formatDate(new Date(pacientes[i].FechaProximaVisita));
                }
            }

            function eliminarEntity(entity) {
                var index = $scope.gridData.indexOf(entity);
                if (index !== -1) {
                    $scope.gridData.splice(index, 1);
                }
            }

            function formatDate(date) {
                var year = date.getFullYear();
                var month = (1 + date.getMonth()).toString();
                month = month.length > 1 ? month : '0' + month;
                var day = date.getDate().toString();
                day = day.length > 1 ? day : '0' + day;
                return year + '-' + month + '-' + day;
            }
        });
})();