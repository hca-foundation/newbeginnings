
app.controller("ClientEditCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", "$filter", function ($scope, $rootScope, $routeParams, $http, $location, $filter) {
    $scope.newItem = {};

    var itemId = $routeParams.id;
    $http.get(`/api/client/view/${itemId}`)
        .then(function (oneItem) {
            $scope.newItem = oneItem.data;
            $scope.newItem.ProgramStartDate = new Date(oneItem.data.ProgramStartDate);
        });

    $scope.addNewItem = function () {
        $http.put('/api/client/editcontent', $scope.newItem)
            .then(function (res) {
                $location.url("/client/list");
                $scope.newItem = {};
            })
    };
}]);