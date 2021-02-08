
app.controller("QuestionListCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.items = [];
    var getItems = function () {
        $http.get('/api/question/list')
            .then(function (res) {
                $scope.items = res.data;
            });
    }
    getItems();

    $scope.deleteItem = function (itemId) {
        $http.post(`/api/question/delete/${itemId}`)
            .then(function (res) {
                getItems();
            });
    };

    $scope.inputChange = function (itemId) {
        $http.put(`/api/question/editstatus/${itemId}`)
            .then(function (res) {
                getItems();
            });
    };

    $scope.loadItem = function (itemId) {
        $location.url("/question/view/" + itemId);
    }

}]);