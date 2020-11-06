
app.controller("AuthCtrl", function ($scope, $rootScope, $location, $http) {
    $scope.loginContainer = true;
    $scope.registerContainer = false;
    $scope.register = {};
    
    if ($location.path() === "/logout") {

        sessionStorage.removeItem('token');
        $http.defaults.headers.common['Authorization'] = "";
        $rootScope = {};
        $location.url("/auth");
    }

    $scope.setLoginContainer = function () {
        $scope.loginContainer = true;
        $scope.registerContainer = false;

    };

    $scope.setRegisterContainer = function () {
        $scope.loginContainer = false;
        $scope.registerContainer = true;

    };

    $scope.registerUser = function (registerNewUser) {
        $http({
            method: 'POST',
            url: "api/Account/Register",
            data: {
                "Email": registerNewUser.email,
                "Password": registerNewUser.password,
                "ConfirmPassword": registerNewUser.passwordconfirm
            }
        })
        .then(function (result) {
            $scope.loginUser({ email: registerNewUser.email, password: registerNewUser.password });
        });
    };

    $scope.loginUser = function (login) {
        $http({
            method: 'POST',
            url: "/Token",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: { grant_type: "password", username: login.email, password: login.password }
        })
        .then(function (result) {
            //console.log(result);
            sessionStorage.setItem('token', result.data.access_token);
            $rootScope.user = result.data;
            $http.defaults.headers.common['Authorization'] = `bearer ${result.data.access_token}`;

            $location.url("/home");
        });
    };

});