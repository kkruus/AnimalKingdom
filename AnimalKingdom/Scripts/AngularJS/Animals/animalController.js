(function () {
    'use strict';
    angular.module('animalApp').controller('AnimalController', ['$scope', '$timeout', 'AnimalService', 'SpeciesService', animalController]);
    function animalController($scope, $timeout, animalService, speciesService) {
        $scope.animals = [];
        $scope.species = [];
        $scope.headerFields = [
            'Liik', 'Nimi', 'Sünniaasta', 'Vanus', 'Lisamise kuupäev'
        ];
        $scope.animalTemplate = {
            Name: "",
            Species: null,
            BirthYear: new Date().getFullYear(),
            Age: 0,
            Created: new Date()
        };
        $scope.successMessage = '';
        $scope.errorMessage = "";
        $scope.showSuccess = false;

        $scope.changeBirthYear = changeBirthYear;
        $scope.addNewAnimal = addNewAnimal;
        $scope.saveAnimal = saveAnimal;
        $scope.deleteAnimal = deleteAnimal;
        init();
        function init() {
            speciesService.getSpecies().then(function (speciesData) {
                $scope.species = speciesData;
                animalService.getAnimals().then(function (animalData) {
                    $scope.animals = animalData;
                });
            });
        }

        function addNewAnimal() {
            $scope.animals.push(angular.copy($scope.animalTemplate));
        }

        function saveAnimal(animal, isValid) {
            if (isValid == true) {
                if (animal.SpeciesId == null || animal.SpeciesId == undefined) {
                    $scope.errorMessage = "Looma liik on valimata";
                } else {
                    animalService.saveAnimal(animal).then(function (animalData) {
                        if (animalData.Message != null) {
                            $scope.errorMessage = animalData.Message;
                        } else if (animalData != "") {
                            animal = animalData;
                            $scope.successMessage = "Loom salvestatud";
                            $scope.errorMessage = "";
                            setSuccessTimeout();
                        } else {
                            $scope.errorMessage = "Viga";
                        }
                    });
                }
            } else {
                if (animal.Name == undefined || animal.Name == "") {
                    $scope.errorMessage = "Looma nimi peab olema täidetud";
                }
            }
        }

        function deleteAnimal(animalId) {
            animalService.deleteAnimal(animalId).then(function (data) {
                if (animalData.Message != null) {
                    $scope.errorMessage = data.Message;
                } else {
                    var animalIndex = $scope.animals.map(function (x) { return x.AnimalId; }).indexOf(animalId);
                    $scope.animals.splice(animalIndex, 1);
                    $scope.successMessage = "Loom kustutatud";
                    setSuccessTimeout();
                }
            });
        }

        function setSuccessTimeout() {
            $scope.showSuccess = true;
            $timeout(function () {
                $scope.showSuccess = false;
            }, 2000);
        }

        function changeBirthYear(animal) {
            var currentYear = new Date().getFullYear();
            if (animal.BirthYear > currentYear) {
                animal.BirthYear = currentYear;
                animal.Age = 0;
            } else {
                animal.Age = currentYear - animal.BirthYear;
            }
        }
    }
})();