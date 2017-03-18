var app = angular.module("app", ['ngRoute', 'simplePagination']);
var baseUrl = $("base").first().attr("href");

//Controllers
app.controller('LandingPageController', LandingPageController);
app.controller('TransactionController', TransactionController);

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
        when('/transferPoints', {
            templateUrl: baseUrl + '/Financial/TransferPoints'
        }).
        when('/transfer', {
            templateUrl: baseUrl + '/Financial/FrmTransferPoints'
        }).
        when('/Transactionhistory', {
            templateUrl: baseUrl + '/Home/TransactionHistory',
            controller: TransactionController
        }).
        when('/transactionCode', {
            templateUrl: baseUrl + '/Financial/TransactionCode',
            controller: TransactionKeyController
        }).
        when('/evoucherdetails', {
            templateUrl: baseUrl + '/Test/EvoucherDetails'
        }).
        when('/financialmanager/:TransactionKey', {
            templateUrl: function (params) { return baseUrl + '/Financial/FinancialManager?TransactionKey=' + params.TransactionKey },
            //templateUrl: baseUrl + '/Financial/FinancialManager',
            controller: FinancialController
        }).
        when('/', {
            templateUrl: baseUrl + '/User/MyProfile',
            controller: UserController
        });
}

configFunction.$inject = ['$routeProvider', '$httpProvider'];

app.config(configFunction);