app.controller("SurveyResultViewCtrl", ["$scope", "$rootScope", "$routeParams", "$http", "$location", function ($scope, $rootScope, $routeParams, $http, $location) {
    $scope.survey = {};
    var itemId = $routeParams.id;
    var timePeriod = $routeParams.TimePeriod;
    console.log("SurveyResultViewCtrl > scope: ", $scope);
    console.log("SurveyResultViewCtrl > scope.survey: ", $scope.survey);
    
    $http.get(`/api/survey/getanswers/${itemId}/${timePeriod}`)
        .then(function (oneItem) {
            $scope.survey = oneItem.data[0];
        });

   
}]);