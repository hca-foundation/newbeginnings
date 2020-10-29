
app.controller("SurveyCtrl", function ($scope, $rootScope, $location, $http) {
    //$scope.dataLoading = true;
    //$scope.validLink = false;
    //var url = window.location.href;
    //var strs = url.split("/");
    //var id = strs[strs.length - 2];
    //if (isNaN(id)) $scope.dataLoading = false;
    //
    //var token = strs[strs.length - 1];

    $scope.survey = {};
    $scope.thankyou = false;
    $scope.sendAnswers = function () {
        $scope.survey.client_SID = 1; 
        console.log($scope.survey);
        $http.post('/api/survey/answers', { survey: $scope.survey })
            .then(function(res) {
                $scope.thankyou = true;
                $scope.survey = {};
            });
    };
});