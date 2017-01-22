angular.module('animalApp').filter('average', function () {
    return function (animals,property) {
        var sum = 0;
        for (var i = 0; i < animals.length; i++) {
            sum += animals[i][property];
        }
        return parseInt(sum / animals.length, 10);
    };
});