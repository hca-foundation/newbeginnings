app.controller("HomeCtrl", ['$scope', '$rootScope', '$http', '$location', function ($scope, $rootScope, $http, $location) {

    $scope.worldGame =
    [
      {
          name: "Manage User",
          url: "/user/list",
          description: "View, add or delete users"
      },
      {
          name: "Manage Qustions",
          url: "/question/list",
          description: "View, add or delete questions"
      },
      {
          name: "Review Results",
          url: "/survey",
          description: "View, download survey results"
      }
     ];


    $scope.unsubmitteditems = [];
    $scope.submittedItems = [];
    $scope.isSubmitted = true;

    let getSurveyItems = function () {
        $http.get('/api/survey/list')
            .then(function (res) {
                if (res && res.data && res.data != undefined) {
                    $scope.submittedItems = res.data.filter(x => x.Survey_Status == 'Submitted')
                    $scope.unsubmitteditems = res.data.filter(x => x.Survey_Status == 'Pending');
                }
            });
    }
    getSurveyItems();

    $scope.ChangeTab = function (type) {
        if (type == "Submitted")
            $scope.isSubmitted = true;
        else
            $scope.isSubmitted = false;
    }

    $scope.exportExcel = function () {
        $http.get('/api/survey/excel/2020Q4');    
    }

    $scope.resendLink = function (id) {
        $http.post('/api/survey/resend/' + id)
            .then(function (res) {
                $scope.copyToClipboard(res.data);
            });
    }

    $scope.editResponse = function (id) {
        $location.url("/client/editResponse/" + id);
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
