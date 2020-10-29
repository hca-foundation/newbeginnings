﻿var app = angular.module("TNBCSurveyApp", ["ngRoute"]);

var isAuth = ($rootScope) => new Promise((resolve, reject) => {
    if ($rootScope.user ? true : false) {
        resolve();
    } else {
        reject();
    }
})

app.config(function ($routeProvider) {
    $routeProvider
		.when('/auth', {
		    templateUrl: 'App/partials/auth.html',
		    controller: 'AuthCtrl'
		})
		.when('/home', {
		    templateUrl: 'App/partials/Home.html',
		    controller: 'HomeCtrl',
		    resolve: { isAuth }
		})
        .when('/logout', {
		    templateUrl: 'App/partials/auth.html',
		    controller: 'AuthCtrl'
        })
        .when('/client/list', {
            templateUrl: 'App/partials/ClientList.html',
            controller: 'ClientListCtrl',
            resolve: { isAuth }
        })
        .when('/client/new', {
            templateUrl: 'App/partials/ClientNew.html',
            controller: 'ClientNewCtrl',
            resolve: { isAuth }
        })
        .when('/client/view/:id', {
            templateUrl: 'App/partials/ClientView.html',
            controller: 'ClientViewCtrl',
            resolve: { isAuth }
        })
        .when('/client/edit/:id', {
            templateUrl: 'App/partials/ClientNew.html',
            controller: 'ClientEditCtrl',
            resolve: { isAuth }
        })
        //.when('/survey/:id/:token', {
        //    templateUrl: 'App/partials/Survey.html',
        //    controller: 'SurveyCtrl'
        //})
        .when('/survey', {
            templateUrl: 'App/partials/Survey.html',
            controller: 'SurveyCtrl',
            resolve: { isAuth }
        })
		.otherwise('/auth');
});