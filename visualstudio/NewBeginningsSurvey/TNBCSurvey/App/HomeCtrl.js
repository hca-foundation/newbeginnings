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

}]);
