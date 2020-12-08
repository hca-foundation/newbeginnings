app.controller("HomeCtrl", ['$scope', '$rootScope', '$http', '$location', '$filter', function ($scope, $rootScope, $http, $location, $filter) {
    $scope.loading = false;
    $scope.errorMessage = null;
    $scope.qtrs = [];
    $scope.selectedQtr = "";
    $scope.unsubmitteditems = [];
    $scope.submittedItems = [];
    $scope.isSubmitted = true;
    $scope.Clients_Completed = null;
    $scope.Days_Until_Deadline = null;
    // Default App Status toggle is ON
    $scope.showAppStatus = true;

    /* TODO - For response rate, % completed, etc.
    $scope.totalSurveyResponses = null;
    $scope.submittedSurveyResponses = null;
    $scope.responseRate = null;
    */

    $scope.closeAlert = function () {
        $scope.errorMessage = null;
    };

    var getAppStatus = function () {
        $http.get('/api/survey/appstatus')
            .then(function (res) {
                $scope.showAppStatus = res.data;
            })
            .catch(function (err) {
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }
    getAppStatus();

    var setAppStatus = function (appStatus) {
        $http.post(`/api/survey/appstatus/${appStatus}`)
            .then(function (res) {
                getAppStatus();
            })
            .catch(function (err) {
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }

    $scope.setAppStatusToggle = function (t) {
        if (t == true)
            setAppStatus("0");
        else
            setAppStatus("1");
    }

    var getQtrs = function () {
        $http.get('/api/survey/timeperiods')
            .then(function (res) {
                $scope.qtrs = res.data;
                $scope.qtrs.push({ Time_Period : "all", Expiration_Date : "" });
            })
            .catch(function (err) {
                $scope.errorMessage = `${err.data.Message} Details: ${err.status} - ${err.statusText}`;
            });
    }
    getQtrs();

    $scope.getSurveyItems = function (timePeriod) {
        $scope.loading = true;
        $http.get(`/api/survey/list/${timePeriod}`)
            .then(function (res) {
                if (res && res.data && res.data != undefined) {
                    $scope.submittedItems = res.data.filter(x => x.TokenUsed == true)
                    var i;
                    for (i = 0; i < $scope.submittedItems.length; i++) {
                        $scope.submittedItems[i].TokenUsedDate = $filter('date')($scope.submittedItems[i].TokenUsedDate, 'MM-dd-yyyy');
                    }
                    $scope.unsubmitteditems = res.data.filter(x => x.TokenUsed == false);
                    $scope.Clients_Completed =
                        timePeriod == "all" ? null : Math.round($scope.submittedItems.length / res.data.length * 100);
                    for (i = 0; i < $scope.qtrs.length; i++) {
                        if ($scope.qtrs[i].Time_Period == timePeriod) {
                            $scope.Days_Until_Deadline =
                                timePeriod == "all" ? null : Math.floor((Date.parse($scope.qtrs[i].Expiration_Date) - Date.now()) / 86400000);
                            break;
                        }
                    }
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
