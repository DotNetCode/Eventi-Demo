﻿(function () {
    "use strict";

    angular.module("myapp", ["ionic", "myapp.controllers", "myapp.services"])
        .run(function ($ionicPlatform) {
            $ionicPlatform.ready(function () {
                if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
                    cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
                }
                if (window.StatusBar) {
                    StatusBar.styleDefault();
                }
            });
        })
        .config(function ($stateProvider, $urlRouterProvider) {
            $stateProvider
            .state("homepage", {
                url: "/homepage",

                templateUrl: "app/templates/view-homepage.html",
                controller: "appCtrl"
            })
             .state("registrazione", {
                 url: "/registrazione",

                 templateUrl: "app/templates/view-registrazione.html",
                 controller: "appCtrl"
             })
              .state("app", {
                  url: "/app",
                  abstract: true,
                  templateUrl: "app/templates/view-menu.html",
                  controller: "appCtrl"
              })
            .state("app.home", {
                url: "/home",
                templateUrl: "app/templates/view-home.html",
                controller: "homeCtrl"
            });
            $urlRouterProvider.otherwise("/homepage");
        });
})();