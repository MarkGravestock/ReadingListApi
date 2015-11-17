var readingListApp = angular.module('readingListApp', []);

readingListApp.controller('readingListController', function ($scope, readingListFactory) {
    $scope.items = [];

    readingListFactory.getReadingList().success(function (data) {
        $scope.items = data;
    }).error(function (error) { });
});

readingListApp.factory('readingListFactory', ['$http', readingListFactory]);
   
function readingListFactory($http) {
    function getReadingList() {
        return $http.get('/api/readingList');
    }

    var service = {
        getReadingList: getReadingList
    };

    return service;
}



