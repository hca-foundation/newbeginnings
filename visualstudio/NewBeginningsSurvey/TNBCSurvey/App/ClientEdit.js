
<<<<<<< HEAD
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
=======
app.controller("ClientEditCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", "$filter", function ($scope, $rootScope, $routeParams, $http, $location, $filter) {
    $scope.newItem = {};

    var itemId = $routeParams.id;
    $http.get(`/api/client/view/${itemId}`)
        .then(function (oneItem) {
            $scope.newItem = oneItem.data;
            $scope.newItem.ProgramStartDate = new Date(oneItem.data.ProgramStartDate);
        });

    $scope.addNewItem = function () {
        console.log($scope.newItem);
        $http.put('/api/client/editcontent', $scope.newItem)
            .then(function (res) {
                $location.url("/client/list");
                $scope.newItem = {};
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
            })
    };
}]);