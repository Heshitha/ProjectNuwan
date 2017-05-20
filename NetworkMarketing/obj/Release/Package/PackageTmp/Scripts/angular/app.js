var app = angular.module("app", ['ngRoute', 'simplePagination']);
var baseUrl = $("base").first().attr("href");
var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9+/=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/rn/g, "n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } }


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
            templateUrl: function (params) { return baseUrl + '/Financial/FrmTransferPoints?TransactionKey=' + Base64.decode(params.TransactionKey)},
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
            templateUrl: baseUrl + '/Home/EvoucherDetails',
            controller: EvoucherController
        }).
        when('/financialmanager/:TransactionKey', {
            templateUrl: function (params) { return baseUrl + '/Financial/FinancialManager?TransactionKey=' + Base64.decode(params.TransactionKey) },
            //templateUrl: baseUrl + '/Financial/FinancialManager',
            controller: FinancialController
        }).
        when('/AllBankDetails', {
            templateUrl: baseUrl + '/Admin/AllbankDetails',
            controller: AdminController
        }).
        when('/UploadProof/:SelectedID', {
            templateUrl: function (params) { return baseUrl + '/Admin/UploadProof?SelectedID=' + params.SelectedID },
            //templateUrl: baseUrl + '/Admin/UploadProof',
            controller: AdminController
        }).
        when('/GenerateEpins', {
            templateUrl: baseUrl + '/Admin/GenerateEpin',
            controller: FinancialController
        }).
        when('/', {
            templateUrl: baseUrl + '/User/MyProfile',
            controller: UserController
        });
}

configFunction.$inject = ['$routeProvider', '$httpProvider'];

app.config(configFunction);