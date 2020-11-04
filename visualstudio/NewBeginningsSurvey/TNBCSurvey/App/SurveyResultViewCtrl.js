app.controller("SurveyResultViewCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", function ($scope, $rootScope, $routeParams, $http, $location) {
    $scope.survey = {};
    var itemId = $routeParams.id;
    var timePeriod = $routeParams.TimePeriod;
    
    $http.get(`/api/survey/getanswers/${itemId}/${timePeriod}`)
        .then(function (oneItem) {
            $scope.survey = oneItem.data[0];
        });

   
}]);