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
            templateUrl: baseUrl + '/Home/ViewTeam',
            controller: TeamViewerController
        }).
        when('/evouchers', {
            templateUrl: baseUrl + '/Home/EVouchers',
            controller: FinancialController
        }).
        when('/myevouchers', {
            templateUrl: baseUrl + '/Home/MyEVouchers',
            controller: FinancialController
        }).
        when('/transferPoints', {
            templateUrl: baseUrl + '/Financial/TransferPoints',
            controller: TransactionKeyController
        }).
        when('/transfer/:TransactionKey', {
            //templateUrl: baseUrl + '/Financial/FrmTransferPoints',
            templateUrl: function (params) { return baseUrl + '/Financial/FrmTransferPoints?TransactionKey=' + params.TransactionKey },
            controller: UserTransactionsController
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