
app.controller("SurveyCtrl", function ($scope, $rootScope, $location, $http) {
    $scope.dataLoading = true;
    $scope.validLink = false;
    $scope.thankyou = false;
    $scope.selectedItem = {};
    $scope.survey = {};
    var initSurveyAnswers = function () {
        var i;
        for (i = 1; i < 17; i++) {
            $scope.survey["Q" + i] = null;
        }
    };

    var url = window.location.href;
    var strs = url.split("/");
    var id = strs[strs.length - 2];
    if (isNaN(id)) {
        $scope.dataLoading = false;
    } else {
        var token = strs[strs.length - 1];
        $http.get(`/api/survey/${id}/${token}`)
            .then(function (res) {
                $scope.dataLoading = false;
                if (res.data !== null) {
                    $scope.validLink = true;
                    $scope.selectedItem = res.data;
                    initSurveyAnswers();
                }
            });
    }

    $scope.sendAnswers = function () {
        var i;
        var isCompleted = true;
        for (i = 1; i < 17; i++) {
            if (i <= 7 || i === 11 || i === 12 || i === 14 || i === 15) {
                $scope["errorMessageQ" + i] = null;
                if ($scope.survey["Q" + i] == null) {
                    $scope["errorMessageQ" + i] = "Please provide an answer to this question.";
                    isCompleted = false;
                }
            }
        }
        if (!isCompleted) return;
        $http.post(`/api/survey/answers/${id}/${token}`, { survey: $scope.survey })
            .then(function(res) {
                $scope.thankyou = true;
                $scope.survey = {};
            });
    };

    $scope.closeAlert = function () {
        for (i = 1; i < 17; i++) {
            if ((i <= 7 || i === 11 || i === 12 || i === 14 || i === 15) && $scope["errorMessageQ" + i] !== null) {
                $scope["errorMessageQ" + i] = null;
            }
        }
        return;
    };
});