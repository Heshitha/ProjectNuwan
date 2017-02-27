var printingapp = angular.module("printingapp", ['ngRoute', 'simplePagination']);

printingapp.controller('LandingPageController', LandingPageController);
printingapp.controller('CreateQuotationController', CreateQuotationController);
printingapp.controller('SearchQuotationController', SearchQuotationController);
printingapp.controller('SearchCustomerController', SearchCustomerController);
printingapp.controller('CustomerController', CustomerController);
printingapp.controller('UserController', UserController);
printingapp.controller('CreateInventoryPOController', CreateInventoryPOController);

printingapp.factory('CreateQuotationFactory', CreateQuotationFactory);
printingapp.factory('SearchQuotationFactory', SearchQuotationFactory);
printingapp.factory('PostFactory', PostFactory);
printingapp.factory('GetFactory', GetFactory);

var baseUrl = $("base").first().attr("href");



var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider.
        when('/searchQuotation', {
            templateUrl: baseUrl + '/Quotation/SearchQuotation',
            controller: SearchQuotationController
        }).
        when('/createQuotation', {
            templateUrl: baseUrl + '/Quotation/CreateQuotation',
            controller: CreateQuotationController
        }).
        when('/openQuotation/:quotationid', {
            templateUrl: function (params) { return baseUrl + '/Quotation/CreateQuotation?quotationID=' + params.quotationid },
            controller: CreateQuotationController
        }).
        when('/searchCustomer', {
            templateUrl: baseUrl + '/Customer/Index',
            controller: SearchCustomerController
        }).
        when('/createCustomer', {
            templateUrl: baseUrl + '/Customer/CreateCustomer',
            controller: CustomerController
        }).
        when('/openCustomer/:customerID', {
            templateUrl: function (params) { return baseUrl + '/Customer/CreateCustomer?customerID=' + params.customerID },
            controller: CustomerController
        }).
        when('/myprofile', {
            templateUrl: function (params) { return baseUrl + '/User/Profile?userID=-1' },
            controller: UserController
        }).
        when('/createInventoryPurchaseOrder', {
            templateUrl: baseUrl + '/InventoryPurchaseOrder/Index',
            controller: CreateInventoryPOController
        }).
        //when('/linkThree', {
        //    templateUrl: baseUrl + '/PrePress/TestLinkThree'
        //}).
        //when('/linkFour', {
        //    templateUrl: baseUrl + '/PrePress/TestLinkFour'
        //}).
        when('/', {
            templateUrl: baseUrl + '/DashBoard/Index'
        });

}

configFunction.$inject = ['$routeProvider', '$httpProvider'];

printingapp.config(configFunction);