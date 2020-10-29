
app.controller("NavCtrl", ["$scope", "$rootScope", "$location", function ($scope, $rootScope, $location) {

    $scope.navItems = [
        {
            name: "Survey",
            url: "/survey"
        },
        {
		    name: "Home",
		    url: "/home"
		},
        {
            name: "All Clients",
            url: "/client/list"
        },
        {
            name: "New Client",
            url: "/client/new"
        }
    ];

    $scope.loadPartials = function (link) {
        if (link === "/logout") {
            $rootScope.user = "";
        } 
        $location.url(link);
    }
}]);
