app.controller("HomeCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {
    $scope.loading = false;
    $rootScope.loading
    $scope.errorMessage = null;
    $scope.qtrs = [];
    $scope.selectedQtr = "";
    $scope.unsubmitteditems = [];
    $scope.submittedItems = [];
    $scope.isSubmitted = true;
    // Default Global Survey Status toggle is ON
    $scope.showGlobalSurveyStatus = true;

    /* TODO - For response rate, % completed, etc.
    $scope.totalSurveyResponses = null;
    $scope.submittedSurveyResponses = null;
    $scope.responseRate = null;
    */

    $scope.closeAlert = function () {
        $scope.errorMessage = null;
    };

    $scope.setGlobalSurveyToggle = function (t) {
        if (t == true)
            $scope.showGlobalSurveyStatus = false;
        else
            $scope.showGlobalSurveyStatus = true;
    }

    //$scope.getSurveyResponseRate = function () {
    //    $http.get('/api/client/list')
    //        .then(function (response) {
    //            $scope.totalSurveyResponses = response.length;
    //            //var clients = response.data;
    //            var i;
    //            for (i = 0; i < $scope.totalSurveyResponses; i++) {

    //                if ($scope.items[i].Active == true) {
    //                    console.log("$scope.items[i].Active: ", $scope.items[i].Active);
    //                    $scope.submittedSurveyResponses = $scope.items[i].Active;
    //                }

    //                $scope.responseRate = $scope.submittedSurveyResponses / $scope.totalSurveyResponses;
    //            }
    //        });
    //};

    var getQtrs = function () {
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
        $scope.loading = true;
        $http.get(`/api/survey/list/${timePeriod}`)
            .then(function (res) {
                if (res && res.data && res.data != undefined) {
                    $scope.submittedItems = res.data.filter(x => x.TokenUsed == true)
                    $scope.unsubmitteditems = res.data.filter(x => x.TokenUsed == false);
                    $scope.loading = false;
                }
            })
            .catch(function (err) {
                $scope.loading = false;
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }

    $scope.ChangeTab = function (type) {
        if (type == "Submitted")
            $scope.isSubmitted = true;
        else
            $scope.isSubmitted = false;
    }

    $scope.exportExcel = function (TimePeriod) {
        $scope.loading = true;
        if (TimePeriod == undefined || TimePeriod.length < 1) {
            alert("A quarter option is needed!");
            return;
        }
        $http.get('/api/survey/csv/' + TimePeriod)
            .then(function (res) {
                console.log("exportExcel > res: ", res);
                var hiddenElement = document.createElement('a');
                hiddenElement.href = 'data:attachment/csv,' + encodeURI(res.data);
                hiddenElement.target = '_blank';
                hiddenElement.download = TimePeriod + '.csv';
                hiddenElement.click();
                $scope.loading = false;
            })
            .catch(function (err) {
                $scope.loading = false;
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }

    $scope.resendLinkEmail = function (itemId) {
        $http.post(`/api/survey/${itemId}`)
            .then(function (res) {
                getItems();
            });
    }

    $scope.resendLink = function (item) {
        var link = "https://newbegininingcenter.azurewebsites.net/#!/survey/" + item["Client_SID"] + "/" + item["Token"];
        $scope.copyToClipboard(link);
    }

    $scope.viewResponse = function (id, TimePeriod) {
        $http.get(`/api/survey/getanswers/${id}/${TimePeriod}`)
            .then(function (oneItem) {
                $scope.survey = oneItem.data[0];
            })
            .catch(function (err) {
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }

    $scope.copyToClipboard = function (link) {
        var copyElement = document.createElement("textarea");
        copyElement.style.position = 'fixed';
        copyElement.style.opacity = '0';
        copyElement.textContent = link;
        var body = document.getElementsByTagName('body')[0];
        body.appendChild(copyElement);
        copyElement.select();
        document.execCommand('copy');
    }
}]);
