(function () {
    'use strict';
    angular.module('animalApp').factory('AnimalService', ['$http', '$q', animalService]);
    function animalService($http, $q) {
        var service = {
            getAnimals: getAnimals,
            saveAnimal: saveAnimal,
            deleteAnimal: deleteAnimal,          
        }
        var serviceEndpoints = {
            getAnimals: "/api/animal/GetAnimals",
            saveAnimal: "/api/animal/AddOrUpdateAnimal",
            deleteAnimal: "/api/animal/DeleteAnimal?animalId="
        };
        return service;

        function getAnimals() {
            var def = $q.defer();
            $http({ method: "get", cache: true, url: serviceEndpoints.getAnimals }).success(function (data) { def.resolve(data); }).error(function (data) { def.reject("failed request"); });;
            return def.promise;
        }
        function saveAnimal(animal) {
            var def = $q.defer();
            $http({ method: "post", cache: false, url: serviceEndpoints.saveAnimal, data: JSON.stringify(animal) }).success(function (data) { def.resolve(data); }).error(function (data) { def.resolve(data); });;
            return def.promise;
        }

        function deleteAnimal(animalId) {
            var def = $q.defer();
            $http({ method: "delete", cache: false, url: serviceEndpoints.deleteAnimal + animalId }).success(function (data) { def.resolve(data); }).error(function (data) { def.reject("failed request"); });;
            return def.promise;
        }
    };
})();