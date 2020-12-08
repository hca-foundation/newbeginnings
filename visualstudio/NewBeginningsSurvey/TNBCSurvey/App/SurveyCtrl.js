
app.controller("SurveyCtrl", function ($scope, $rootScope, $location, $http) {
    $scope.dataLoading = true;
    $scope.validLink = false;
    $scope.thankyou = false;
    $scope.selectedItem = {};
    $scope.survey = {};
    var initSurveyAnswers = function () {
        var i;
        for (i = 1; i < 20; i++) {
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
        for (i = 1; i < 20; i++) {
            if ($scope.survey["Q" + i] == null) {
                alert("All fields are required for submitting the answers!");
                return;
            }
        }
        $http.post(`/api/survey/answers/${id}/${token}`, { survey: $scope.survey })
            .then(function(res) {
                $scope.thankyou = true;
                $scope.survey = {};
            });
    };

});