
app.controller("ClientEditCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", function ($scope, $rootScope, $routeParams, $http, $location) {
    $scope.newClient = {};
    let itemId = $routeParams.id;
    $http.get(`/api/client/view/${itemId}`)
        .then(function (oneItem) {
            $scope.newClient = oneItem.data;
        });

    $scope.addNewItem = function () {
        $scope.newClient.Active = true;
        $http.put('/api/client/editcontent', $scope.newClient)
            .then(function (res) {
                $location.url("/client/list");
                $scope.newClient = {};
            })
    };
}]);