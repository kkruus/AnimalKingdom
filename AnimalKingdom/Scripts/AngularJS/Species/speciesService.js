(function () {
    'use strict';
    angular.module('animalApp').factory('SpeciesService', ['$http', '$q', speciesService]);
    function speciesService($http, $q) {
        var service = {
            getSpecies: getSpecies
        }
        var serviceEndpoints = {
            getSpecies: "/api/species/getspecies"
        };
        return service;

        function getSpecies() {
            var def = $q.defer();
            $http({ method: "get", cache: true, url: serviceEndpoints.getSpecies }).success(function (data) { def.resolve(data); });
            return def.promise;
        }
    };
})();