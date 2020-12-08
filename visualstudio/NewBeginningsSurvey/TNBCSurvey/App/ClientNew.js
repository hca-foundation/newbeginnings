
app.controller("ClientNewCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.newItem = {};
    $scope.newItem.Active = true;

    $scope.addNewItem = function () {
        $http.post('/api/client/new', $scope.newItem)
            .then(function(res) {
                $location.url("/client/list");
                $scope.newItem = {};
            })
            .catch(function (error) {
                alert("This email address already exists or required fields missing!");
        });
    };

}]);