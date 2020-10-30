
app.controller("NavCtrl", ["$scope", "$rootScope", "$location", function ($scope, $rootScope, $location) {

    $scope.navItems = [
<<<<<<< HEAD
        {
            name: "Survey",
            url: "/survey"
        },
=======
        //{
        //    name: "Survey",
        //    url: "/survey"
        //},
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
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
