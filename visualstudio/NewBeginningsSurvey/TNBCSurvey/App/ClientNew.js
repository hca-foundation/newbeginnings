
app.controller("ClientNewCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.newItem = {};
<<<<<<< HEAD

    $scope.addNewItem = function () {
        $scope.newItem.Survey_Status = "Unknown";
        $http.post('/api/client/new', $scope.newItem)
            .then(function (res) {
                $location.url("/client/list");
                $scope.newItem = {};
            })
    };
=======
    $scope.newItem.Active = true;

    $scope.addNewItem = function () {
        $http.post('/api/client/new', $scope.newItem)
            .then(function(res) {
                $location.url("/client/list");
                $scope.newItem = {};
            });
    };

>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
}]);