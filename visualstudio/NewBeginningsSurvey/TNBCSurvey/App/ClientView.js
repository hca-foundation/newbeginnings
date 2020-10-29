
app.controller("ClientViewCtrl", ["$scope", '$rootScope', "$routeParams", "$http", "$location", function ($scope, $rootScope, $routeParams, $http, $location) {
    $scope.selectedItem = {};
    var itemId = $routeParams.id;
    $http.get(`/api/client/view/${itemId}`)
        .then(function (oneItem) {
            $scope.selectedItem = oneItem.data;
        });

    $scope.editItem = function (itemId) {
        $location.url("/client/edit/" + itemId);
    }
}]);