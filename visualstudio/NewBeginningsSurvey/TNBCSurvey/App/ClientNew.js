
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
                if (error.data.InnerException.InnerException.ExceptionMessage.toString().indexOf("Violation of UNIQUE KEY constraint") !== -1)
                    alert("This email address already exists!")
        });
    };

}]);