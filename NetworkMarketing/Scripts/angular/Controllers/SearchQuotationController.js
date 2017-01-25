var SearchQuotationController = function ($scope, $compile, Pagination, SearchQuotationFactory, $location) {
    $scope.customerName = '';
    $scope.ourRef = '';
    $scope.quotationNo = '';
    $scope.itemName = '';
    $scope.searchedQuotation = [];
    $scope.pagination = Pagination.getNew(10);

    $scope.searchQuotations = function () {
        var sarchQuery = {
            CustomerName: $scope.customerName,
            CustomerRef: $scope.ourRef,
            QuotationNo: $scope.quotationNo,
            ItemName: $scope.itemName
        };

        var result = SearchQuotationFactory(sarchQuery);
        result.then(function (result) {
            if (result.success) {
                $scope.searchedQuotation = result.data;
                $scope.pagination.numPages = Math.ceil($scope.searchedQuotation.length / $scope.pagination.perPage);
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    };

    $scope.clearSearch = function () {
        $scope.customerName = '';
        $scope.ourRef = '';
        $scope.quotationNo = '';
        $scope.itemName = '';
        $scope.searchQuotations();
    };

    $scope.openQuotation = function (quotationID) {
        $location.path('/openQuotation/' + quotationID);
        //alert(quotationID);
    };

    $scope.searchQuotations();
}

SearchQuotationController.$inject = ['$scope', '$compile', 'Pagination', 'SearchQuotationFactory', '$location'];