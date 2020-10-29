
app.controller("QuestionNewCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.newQuestion = {};

    $scope.addNewItem = function () {
        $scope.newQuestion.Active = true;
        $http.post('/api/question/new', $scope.newQuestion)
            .then(function (res) {
                $location.url("/question/list");
                $scope.newQuestion = {};
            })
    };
}]);