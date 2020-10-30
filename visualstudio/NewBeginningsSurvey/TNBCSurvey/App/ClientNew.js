
app.controller("ClientNewCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.newItem = {};

    $scope.addNewItem = function () {
        $scope.newItem.Survey_Status = "Unknown";
        $http.post('/api/client/new', $scope.newItem)
            .then(function (res) {
                $location.url("/client/list");
                $scope.newItem = {};
            })
    };
}]);