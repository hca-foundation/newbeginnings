
app.controller("SurveyCtrl", function ($scope, $rootScope, $location, $http) {
    $scope.dataLoading = true;
    $scope.validLink = false;
    $scope.thankyou = false;
    $scope.selectedItem = {};
    var url = window.location.href;
    var strs = url.split("/");
    var id = strs[strs.length - 2];
    if (isNaN(id)) {
        $scope.dataLoading = false;
    } else {
        var token = strs[strs.length - 1];
        $http.get(`/api/survey/${id}/${token}`)
            .then(function (res) {
                if (res.data !== null) {
                    $scope.validLink = true;
                    $scope.selectedItem = res.data;
                }
            });
    }

    $scope.dataLoading = false;

    $scope.survey = {};
    $scope.sendAnswers = function () {
        $http.post(`/api/survey/answers/${id}/${token}`, { survey: $scope.survey })
            .then(function(res) {
                $scope.thankyou = true;
                $scope.survey = {};
            });
    };
});