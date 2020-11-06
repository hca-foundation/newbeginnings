app.controller("HomeCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {

    $scope.qtrs = [];
    $scope.selectedQtr = "";
    $scope.unsubmitteditems = [];
    $scope.submittedItems = [];
    $scope.isSubmitted = true;

    var getQtrs = function() {
        $scope.qtrs.push("2020Q4");
        var today = new Date();
        var yyyy = today.getFullYear();
        if (yyyy <= 2020) return $scope.qtrs.push("all");
        var mm = parseInt(String(today.getMonth() + 1).padStart(2, "0"), 10);
        var qq = Math.floor((mm + 2) / 3);
        var i;
        for (i = 2021; i < yyyy; i++) {
            $scope.qtrs.unshift(i + "Q1");
            $scope.qtrs.unshift(i + "Q2");
            $scope.qtrs.unshift(i + "Q3");
            $scope.qtrs.unshift(i + "Q4");
        }
        for (i = 1; i <= qq; i++) {
            $scope.qtrs.unshift(yyyy + "Q" + i);
        }
        $scope.qtrs.push("all");
    }
    getQtrs();

    $scope.getSurveyItems = function (timePeriod) {
        $http.get(`/api/survey/list/${timePeriod}`)
            .then(function (res) {
                if (res && res.data && res.data != undefined) {
                    $scope.submittedItems = res.data.filter(x => x.TokenUsed == true)
                    $scope.unsubmitteditems = res.data.filter(x => x.TokenUsed == false);
                }
            });
    }
    //getSurveyItems();

    $scope.ChangeTab = function (type) {
        if (type == "Submitted")
            $scope.isSubmitted = true;
        else
            $scope.isSubmitted = false;
    }

    $scope.exportExcel = function (TimePeriod) {
        $http.get('/api/survey/csv/' + TimePeriod)
            .then(function (res) {
                var hiddenElement = document.createElement('a');
                hiddenElement.href = 'data:attachment/csv,' + encodeURI(res.data);
                hiddenElement.target = '_blank';
                hiddenElement.download = TimePeriod + '.csv';
                hiddenElement.click();
        });
    }

    $scope.resendLinkEmail = function (itemId) {
        $http.post(`/api/survey/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    $scope.resendLink = function (id) {
        $http.post('/api/survey/resend/' + id)
            .then(function (res) {
                $scope.copyToClipboard(res.data);
            });
    }

    $scope.viewResponse = function (id, TimePeriod) {
        //$location.url(`/client/viewResponse/${id}/${TimePeriod}`);
        $http.get(`/api/survey/getanswers/${id}/${TimePeriod}`)
            .then(function (oneItem) {
                $scope.survey = oneItem.data[0];
            });
    }

    $scope.copyToClipboard = function (name) {
        var copyElement = document.createElement("textarea");
        copyElement.style.position = 'fixed';
        copyElement.style.opacity = '0';
        copyElement.textContent = decodeURI(name);
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(copyElement);
        copyElement.select();
        document.execCommand('copy');
    }
}]);
