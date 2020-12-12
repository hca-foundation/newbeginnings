
app.controller("NavCtrl", ["$scope", "$rootScope", "$location", function ($scope, $rootScope, $location) {
    $scope.showMobileNavMenu = false;
    $scope.showDesktopNav = true;

    $scope.toggleNav = function () {
        $scope.showMobileNavMenu = !$scope.showMobileNavMenu;
        $scope.showDesktopNav = !$scope.showDesktopNav;
    }

    $scope.navItems = [
        //{
        //    name: "Survey",
        //    url: "/survey"
        //},
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
