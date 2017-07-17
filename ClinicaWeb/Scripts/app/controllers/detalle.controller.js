(function () {
    'use strict';
    angular.module('app').
        controller('DetalleController', function ($scope, $location, $rootScope, PacienteService, TratamientoService) {

            $scope.pacienteId = sessionStorage.pacienteId;
            $scope.nombre;
            $scope.ultimaVisitaDatepickerVisible = false;
            $scope.proximaVisitaDatepickerVisible = false;
            $scope.fechaUltimaVisita;
            $scope.fechaProximaVisita;
            $scope.paciente = {};
            $scope.gridData = [];


            //Modal tratamiento
            $scope.modalWrapper = document.getElementById('editModal');
            $scope.modalToDisplay = '';
            $scope.tratamiento = {};
            $scope.tratamientoFechaInicioVisible = false;
            $scope.tratamientoFechaConclusionVisible = false; 

            //Grid tratamientos
            $scope.gridOptions = {
                data: 'gridData',
                columnDefs: [
                    { field: 'FechaInicio', displayName: 'Fecha Inicio', width: 200 },
                    { field: 'FechaConclusion', displayName: 'Fecha Conclusion', width: 200 },
                    { field: 'Costo', width: 200 },
                    { field: 'Detalle', width: 280 },
                    { field: 'actions', displayName: 'Acciones', cellTemplate: '<button ng-click="grid.appScope.editarTratamiento(row)" class="btn btn-info">Editar</button><button ng-click="grid.appScope.eliminarTratamiento(row)" class="btn btn-danger">Eliminar</button>', width: 200}
                ],
                enableSorting: true,
                enableFiltering: true,
                flatEntityAccess: true,
                fastWatch: true,
                showGridFooter: true,
                showColumnMenu: true
            };

            $scope.editarPaciente = function () {
                PacienteService.editPaciente($scope.paciente, function (data) {
                    alert('Informacion actualizada correctamente');
                }, function (error) {
                    alert('Se produjo un error. Verifique que los datos ingresados sean validos');
                });
            }

            $scope.agregarTratamiento = function (row) {
                $scope.tratamiento.PacienteId = $scope.paciente.PacienteId;
                $scope.modalToDisplay = 'agregarTratamiento';
                $scope.modalWrapper.style.display = 'block';
            }

            $scope.guardarTratamiento = function () {
                TratamientoService.saveTratamiento($scope.tratamiento, function (data) {
                    alert('Tratamiento agregado correctamente');
                    $scope.gridData.push($scope.tratamiento);
                    $scope.cerrarModal();
                }, function (error) {
                    alert('Se produjo un error. Verifique que los datos sean validos');
                }, function () {
                    $scope.tratamiento = {};
                });
            }

            $scope.editarTratamiento = function (row) {
                $scope.modalToDisplay = 'editarTratamiento';
                $scope.tratamientoEditado = row.entity;
                $scope.tratamiento = angular.copy(row.entity);
                $scope.modalWrapper.style.display = 'block';
            }

            $scope.actualizarTratamiento = function () {
                TratamientoService.editTratamiento($scope.tratamiento, function (data) {
                    updateGrid();
                    $scope.cerrarModal();
                    alert('Tratamiento actualizado correctamente');
                }, function (error) {
                    alert('Se produjo un error. Verifique que los datos ingresados sean validos');
                });
            }

            $scope.eliminarTratamiento = function (row) {
                TratamientoService.deleteTratamiento(row.entity.Id, function (data) {
                    removeEntity(row.entity);
                }, function (error) {
                    alert('Se produjo un error. Contacte al administrador para resolver el problema');
                });   
            }

            $scope.cerrarModal = function () {
                $scope.modalWrapper.style.display = "none";
                $scope.tratamiento = {};
            }

            $scope.toggleUltimaVisitaDatepicker = function () {
                $scope.ultimaVisitaDatepickerVisible = !$scope.ultimaVisitaDatepickerVisible;
            }

            $scope.toggleProximaVisitaDatepicker = function () {
                $scope.proximaVisitaDatepickerVisible = !$scope.proximaVisitaDatepickerVisible;
            }

            $scope.toggleTratamientoFechaInicioDatepicker = function () {
                $scope.tratamientoFechaInicioVisible = !$scope.tratamientoFechaInicioVisible;
            }

            $scope.toggleTratamientoFechaConclusionDatepicker = function () {
                $scope.tratamientoFechaConclusionVisible = !$scope.tratamientoFechaConclusionVisible;
            }

            PacienteService.getPacientePorId($scope.pacienteId, function (data) {
                $scope.paciente = data;
                $scope.paciente.FechaUltimaVisita = formatDate(new Date($scope.paciente.FechaUltimaVisita));
                $scope.paciente.FechaProximaVisita = formatDate(new Date($scope.paciente.FechaProximaVisita));
                $scope.nombre = $scope.paciente.Nombre;
                convertFechasTratamientos();
                $scope.gridData = $scope.paciente.Tratamientos;
            });

            function updateGrid() {
                $scope.tratamientoEditado.FechaInicio = $scope.tratamiento.FechaInicio;
                $scope.tratamientoEditado.FechaConclusion = $scope.tratamiento.FechaConclusion;
                $scope.tratamientoEditado.Costo = $scope.tratamiento.Costo;
                $scope.tratamientoEditado.Detalle = $scope.tratamiento.Detalle;
            }

            function removeEntity(entity) {
                var index = $scope.gridData.indexOf(entity);
                if (index !== -1) {
                    $scope.gridData.splice(index, 1);
                }
            }

            function convertFechasTratamientos() {
                for (var i = 0; i < $scope.paciente.Tratamientos.length; i++) {
                    $scope.paciente.Tratamientos[i].FechaInicio = formatDate(new Date($scope.paciente.Tratamientos[i].FechaInicio));
                    $scope.paciente.Tratamientos[i].FechaConclusion = formatDate(new Date($scope.paciente.Tratamientos[i].FechaConclusion));
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