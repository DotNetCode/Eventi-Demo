(function () {
    "use strict";

    angular.module("myapp.services", []).factory("myappService", ["$rootScope", "$http", "$q", "$ionicLoading", function ($rootScope, $http, $q, $ionicLoading) {
        var myappService = {};
        //var apiHost = 'http://localhost:17769/';
        var apiHost = 'http://eventi-labriola.dotnetschool.it';
        //starts and stops the application waiting indicator
        myappService.wait = function (show) {
            if (show)
                $(".spinner").show();
            else
                $(".spinner").hide();
        };

        myappService.getLoginToken = function (username, password) {
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'attendere prego...'
            });
            var url = apiHost + '/token';
            var postdata = $.param({

                Username: username,
                Password: password,
                grant_type: 'password'
            });

            $http({
                method: 'POST',
                url: url,
                data: postdata,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) { // optional
                $ionicLoading.hide();
                deferred.reject({ error: response.data.error_description });
            });
            return deferred.promise;
        };


        myappService.registerEvent = function (userId, IdEvento) {
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'attendere prego...'
            });
            var url = apiHost + '/api/eventi/' + IdEvento + '/RegistrazioneUtente/' + userId;
            var postdata = $.param({

                idEvento: IdEvento,
                userId: userId
            });

            $http({
                method: 'POST',
                url: url,
                data: postdata,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) { // optional
                $ionicLoading.hide();
                deferred.reject({ error: response.data.error_description });
            });
            return deferred.promise;
        };

        myappService.unregisterEvent = function (userId, IdEvento) {
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'attendere prego...'
            });
            var url = apiHost + '/api/eventi/' + IdEvento + '/CancellaRegistrazione/' + userId;
            var postdata = $.param({

                idEvento: IdEvento,
                userId: userId
            });

            $http({
                method: 'POST',
                url: url,
                data: postdata,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(true);
            },
            function (response) { // optional
                $ionicLoading.hide();
                deferred.reject({ error: response.data.error_description });
            });
            return deferred.promise;
        };

        myappService.register = function (user) {
            var url = apiHost + '/api/account/register';
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            var postdata = $.param({

                Username: user.username,
                Nome: user.nome,
                Cognome: user.cognome,
                Email: user.email,
                Password: user.password
            });

            $http({
                method: 'POST',
                url: url,
                data: postdata,

                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };

        myappService.getEventi = function (token) {
            var url = apiHost + '/api/eventi';
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            $http({
                method: 'GET',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };

        myappService.getEvento = function (IdEvento, token) {
            var url = apiHost + '/api/eventi/' + IdEvento;
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            $http({
                method: 'GET',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };

        myappService.getPostiDisponibili = function (IdEvento) {
            var url = apiHost + '/api/eventi/' + IdEvento + '/postidisponibili';
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            $http({
                method: 'GET',
                url: url
                //,headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data.PostiDisponibili);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };


        myappService.getArchivioEventi = function (token) {
            var url = apiHost + '/api/archivio';
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            $http({
                method: 'GET',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };

        myappService.getMyEventi = function (username, token) {
            var url = apiHost + '/api/myeventi/' + username;
            var deferred = $q.defer();
            $ionicLoading.show({
                template: 'Attendere...'
            });
            $http({
                method: 'GET',
                url: url,
                headers: { 'Authorization': 'Bearer ' + token }
            }).then(function (response) {
                $ionicLoading.hide();
                deferred.resolve(response.data);
            },
            function (response) {
                $ionicLoading.hide();
                deferred.reject({ message: "Error: " + response.data.Message });
            });
            return deferred.promise;
        };


        return myappService;
    }]);
})();