/// <reference path="angular.min.js" />

var app = angular.module("myapp", [])
.controller("mycontroller", function ($scope) {
    var contries = [
        { name: "Cuba", PIB: 2.3129, Date: new Date("November 25, 2000"), like: 0, dislike: 0, city: "Havana",state:1 },
        { name: "Estados Unidos", PIB: 1.3129, Date: new Date("September 25, 1880"), like: 0, dislike: 0, city: "Miami", state: 2 },
        { name: "Venezuela", PIB: 1.729, Date: new Date("March 25, 1980"), like: 0, dislike: 0, city: "Caracas", state: 2 },
        { name: "España", PIB: 0.547, Date: new Date("May 25, 1980"), like: 0, dislike: 0, city: "Madrid", state: 2 },
    ];
    $scope.step = 1;
    $scope.contries = contries;
    $scope.Like = function (cont) {
        cont.like++;
    }
    $scope.DisLike = function (cont) {
        cont.dislike++;
    }
    $scope.sortOrder = "";
    //sort on th
    $scope.reverseSort = false;
    $scope.sortColmn = "name";
    $scope.changeSort = function (column) {
        $scope.reverseSort = ($scope.sortColmn == column) ? !$scope.reverseSort : false;
        $scope.sortColmn = column;
    };
    $scope.changeClass = function (column) {
        if ($scope.sortColmn == column) {
            return $scope.reverseSort ? 'arrowUp' : 'arrowDown';
        }
        return "";
    };
    // input search
    $scope.search2 = function (item) {
        if ($scope.searchText == undefined) {
            return true;
        }
        else {
            if (item.name.toLowerCase().indexOf($scope.searchText.toLowerCase()) != -1 ||
                item.city.toLowerCase().indexOf($scope.searchText.toLowerCase()) != -1) {
                return true;
            }
        }
        return false;
    };    
})
    //customer Filter
.filter("customerFilter", function () {
    return function (item) {
        return (item == 1) ? "Socialista" : "Capitalista";
    }
});