var CreateQuotationFactory = function ($http, $q) {
    return function (quotation) {
        var deferredObject = $q.defer();
        //debugger;
        $http.post(
             baseUrl + '/api/QuotationAPI/SaveQuotation', quotation
        ).
        success(function (data) {
            //debugger;
            if (data != null) {
                deferredObject.resolve({ success: true, data: data });
            } else {
                deferredObject.resolve({ success: false });
            }
        }).
        error(function () {
            deferredObject.resolve({ success: false });
        });

        return deferredObject.promise;
    }
}

CreateQuotationFactory.$inject = ['$http', '$q'];