app.controller("ClientSurveyEditCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", function ($scope, $rootScope, $routeParams, $http, $location) {
    $scope.newClient = {};
    $scope.survey = {};
    let itemId = $routeParams.id;
    
    $http.get('/api/survey/getanswers/' + itemId)
        .then(function (oneItem) {
            $scope.survey = oneItem.data[0];
        });

   
}]);