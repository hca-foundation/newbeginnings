
app.controller("ClientNewCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.newItem = {};
    $scope.newItem.Active = true;
    $scope.loading = false;
    $scope.errorMessage = null;

    $scope.closeAlert = function () {
        $scope.errorMessage = null;
    };

    $scope.addNewItem = function () {
        $scope.loading = true;
        $http.post('/api/client/new', $scope.newItem)
            .then(function(res) {
                $location.url("/client/list");
                $scope.newItem = {};
                $scope.loading = false;
            })
            .catch(function (error) {
                $scope.loading = false;
                $scope.errorMessage = `
                    This email address already exists or required fields are missing. Please check the form and try again.`;
        });
    };
}]);