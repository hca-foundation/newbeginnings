app.controller("AuthCtrl", function ($scope, $rootScope, $location, $http) {
    $scope.loading = false;
    $rootScope.loading
    // Holds the login form:
    $scope.loginContainer = false;
    // Holds the register form:
    $scope.registerContainer = false;
    $scope.register = {};
    // Shows the login form on click:
    $scope.showLoginButton = true;
    // Shows the register form on click:
    $scope.showRegisterButton = true;
    $scope.showBackButton = false;
    $scope.errorMessage = null;
    $scope.authButtonGroupClass = "auth_button-group";

    //$scope.register.username = 'b@b.com';
    //$scope.register.email = 'b@b.com';
    //$scope.register.password = '123456Nss!';
    //$scope.register.passwordconfirm = '123456Nss!';
    //$scope.login = {};
    //$scope.login.email = 'b@b.com';
    //$scope.login.username = 'b@b.com';
    //$scope.login.password = '123456Nss!';

    if ($location.path() === "/logout") {

        sessionStorage.removeItem('token');
        $http.defaults.headers.common['Authorization'] = "";
        $rootScope = {};
        $location.url("/auth");
    }

    // Show login form and hide irrelevant buttons:
    $scope.setLoginContainer = function () {
        $scope.loginContainer = true;
        $scope.registerContainer = false;
        $scope.showBackButton = true;
        $scope.showLoginButton = false;
        $scope.showRegisterButton = false;
        $scope.authButtonGroupClass = "auth_button-group-hide";
    };

    // Show register form and hide irrelevant buttons:
    $scope.setRegisterContainer = function () {
        $scope.loginContainer = false;
        $scope.registerContainer = true;
        $scope.showBackButton = true;
        $scope.showRegisterButton = false;
        $scope.showLoginButton = false;
        $scope.authButtonGroupClass = "auth_button-group-hide";
    };

    // Resets the view to show the Login and Register buttons without a form:
    $scope.setDefaultContainer = function () {
        $scope.errorMessage = null;
        $scope.loginContainer = false;
        $scope.registerContainer = false;
        $scope.showBackButton = false;
        $scope.showLoginButton = true;
        $scope.showRegisterButton = true;
        $scope.authButtonGroupClass = "auth_button-group";
    }

    $scope.closeAlert = function () {
        $scope.errorMessage = null;
    };

    $scope.registerUser = function (registerNewUser) {
        $scope.errorMessage = null;
        $scope.loading = true;
        $scope.setRegisterContainer();
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
                $scope.loading = false;
                $scope.loginUser({ email: registerNewUser.email, password: registerNewUser.password });
            })
            .catch(function (err) {
                $scope.loading = false;
                console.log("register error: ", err)
                // We set the error message to err.data.Message here 
                // instead of err.data.error_description because a 
                // different object structure is returned from the endpoint
                // for the register request than for the authorization request.
                $scope.errorMessage = err.data.Message;
        });
    };

    $scope.loginUser = function (login) {
        $scope.errorMessage = null;
        $scope.loading = true;
        $scope.setLoginContainer();
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
                $scope.loading = false;
                sessionStorage.setItem('token', result.data.access_token);
                $rootScope.user = result.data;
                $http.defaults.headers.common['Authorization'] = `bearer ${result.data.access_token}`;

                $location.url("/home");
            })
            .catch(function (err) {
                $scope.loading = false;
                // Notice we save the error message as err.data.error_description here.
                // This is different from err.data.Message on the register function.
                // See the note in the register function for an explanation.
                $scope.errorMessage = err.data.error_description;
            });
    };
});