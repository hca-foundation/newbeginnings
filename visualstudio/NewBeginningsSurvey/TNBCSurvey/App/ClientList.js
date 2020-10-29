
app.controller("ClientListCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.items = [];

    let getItems = function () {
        $http.get('/api/client/list')
            .then(function (res) {
                $scope.items = res.data;
            });
    }
    getItems();

    $scope.deleteItem = function (itemId) {
        $http.post(`/api/client/delete/${itemId}`)
            .then(function (res) {
                getItems();
            });
    };

    $scope.inputChange = function (itemId) {
        $http.put(`/api/client/editstatus/${itemId}`)
            .then(function (res) {
                getItems();
            });
    };

    $scope.loadItem = function (itemId) {
        $location.url("/client/view/" + itemId);
    }

    $scope.questionItem = function (itemId) {
        $http.post(`/api/survey/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

}]);