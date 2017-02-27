var app = angular.module("app", ['ngRoute', 'simplePagination']);
var baseUrl = $("base").first().attr("href");

//Controllers
app.controller('LandingPageController', LandingPageController);

//Factories
app.factory('PostFactory', PostFactory);
app.factory('GetFactory', GetFactory);



var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider.
        when('/viewClass', {
            templateUrl: baseUrl + '/Home/ViewClass',
            controller: ClassController
        }).
        when('/viewTeam', {
            templateUrl: baseUrl + '/Home/ViewTeam'
        }).
        when('/evouchers', {
            templateUrl: baseUrl + '/Home/EVouchers'
        }).
        when('/myevouchers', {
            templateUrl: baseUrl + '/Home/MyEVouchers'
        }).
        when('/evoucherdetails', {
            templateUrl: baseUrl + '/Home/EVoucherDetails'
        }).
        when('/', {
            templateUrl: baseUrl + '/User/MyProfile'
        });
}

configFunction.$inject = ['$routeProvider', '$httpProvider'];

app.config(configFunction);