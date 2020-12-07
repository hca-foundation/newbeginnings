
app.controller("ClientListCtrl", ['$scope', '$rootScope', '$http', '$location', '$filter', function ($scope, $rootScope, $http, $location, $filter) {
    $scope.items = [];

    var getItems = function () {
        $http.get('/api/client/list')
            .then(function (res) {
                $scope.items = res.data;
                console.log("getItems > res.data: ", res.data);
                $scope.total = $scope.items.length;
                var i;
                for (i = 0; i < $scope.items.length; i++) {
                    if ($scope.items[i].Active == true) $scope.items[i].Active = "Yes";
                    else $scope.items[i].Active = "No";
                    $scope.items[i].ProgramStartDate = $filter('date')($scope.items[i].ProgramStartDate, 'MM-dd-yyyy');
                }
            });
    }
    getItems();

    $scope.deleteItem = function (itemId) {

        $http.post(`/api/client/delete/${itemId}`)
            .then(function (res) {
                getItems();
            });
    };

    $scope.editItem = function (itemId) {
        $location.url("/client/edit/" + itemId);
    }

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
        $http.post(`/api/survey/create/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    $scope.resendquestionItem = function (itemId) {
        $http.post(`/api/survey/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    $scope.currentPage = 1;
    $scope.itemPerPage = 10;
    $scope.start = 1;
    $scope.end = 10;

    $scope.setItems = function (n) {
        $scope.itemPerPage = n;
        $scope.currentPage = 1;
        $scope.start = 1;
        $scope.end = ($scope.total < $scope.itemPerPage) ? $scope.total : $scope.itemPerPage;
    };

    $scope.previousPage = function () {
        if ($scope.currentPage > 1) {
            $scope.currentPage = $scope.currentPage - 1;
            $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage + 1;
            $scope.end = ($scope.total < $scope.currentPage * $scope.itemPerPage) ? $scope.total : $scope.currentPage * $scope.itemPerPage;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage * $scope.itemPerPage < $scope.total) {
            $scope.currentPage = $scope.currentPage + 1;
            $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage + 1;
            $scope.end = ($scope.total < $scope.currentPage * $scope.itemPerPage) ? $scope.total : $scope.currentPage * $scope.itemPerPage;
        }
    };
}]);

app.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});