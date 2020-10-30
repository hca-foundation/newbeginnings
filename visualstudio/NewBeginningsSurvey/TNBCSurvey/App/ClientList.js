
app.controller("ClientListCtrl", ['$scope', '$rootScope', '$http', '$location', '$filter', function ($scope, $rootScope, $http, $location, $filter) {
    $scope.items = [];

    var getItems = function () {
        $http.get('/api/client/list')
            .then(function (res) {
                $scope.items = res.data;
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
        console.log(itemId);
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
        console.log("aa");
        console.log(itemId);
        $http.post(`/api/survey/create/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    $scope.resendquestionItem = function (itemId) {
        console.log("bb");
        console.log(itemId);
        $http.post(`/api/survey/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    //$scope.questionItem = function (itemId) {
    //    $http.post(`/api/survey/${itemId}`)
    //        .then(function (res) {
    //            getItems();
    //        });
    //}
    //
    //$scope.makeTodos = function () {
    //    $scope.todos = [];
    //    for (i = 1; i <= 1000; i++) {
    //        $scope.todos.push({ text: "todo " + i, done: false });
    //    }
    //};
    //$scope.makeTodos();
    //$scope.total = $scope.todos.length;

    $scope.currentPage = 1;
    $scope.itemPerPage = 10;
    $scope.start = 0;

    $scope.setItems = function (n) {
        $scope.itemPerPage = n;
    };

    $scope.pageChanged = function () {
        $scope.start = ($scope.currentPage - 1) * $scope.itemPerPage;
    };

    $scope.previousPage = function () {
        if ($scope.currentPage > 1) $scope.currentPage = $scope.currentPage - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage * $scope.itemPerPage < $scope.total)
        $scope.currentPage = $scope.currentPage + 1;
    };
}]);

app.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});