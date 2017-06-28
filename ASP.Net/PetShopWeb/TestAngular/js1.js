/// <reference path="angular.min.js" />
var app = angular.module("myapp2", [])
.controller("myController2", function ($scope, $http, $log) {

    var successCallBack = function (response) {
        $scope.success = response.data;
        $log.info(response);
    };
    var errorCallBack = function (response) {
        $scope.error = response.data;
        $log.info(response);
    };

    $http({
        method: "GET",
        url: "http://localhost:34876/api/values"
    }).then(successCallBack, errorCallBack);
});