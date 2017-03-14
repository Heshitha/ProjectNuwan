var TransactionController = function ($scope, $location, Pagination, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.TransactionData = [];
    $scope.pagination = Pagination.getNew(10);

    debugger;

    $scope.GetAllTransactionsByUser = function () {
        if ($scope.userID != 0) {
            var url = '/api/TransactionAPI/GetAllTransactionsByUser';
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
                    console.log(result.data)
                    $scope.TransactionData = result.data;
                    $scope.pagination.numPages = Math.ceil($scope.TransactionData.length / $scope.pagination.perPage);
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    };

    //var resultData = GetFactory(result);
    //resultData.then(function (result) {
    //    if (result.success) {
    //        $scope.TransactionData = result.data;
    //        $scope.pagination.numPages = Math.ceil($scope.TransactionData.length / $scope.pagination.perPage);
    //    } else {
    //        ShowMessage('danger', 'Error occured while processing.');
    //    }
    //});


    $scope.GetAllTransactionsByUser();

};


TransactionController.$inject = ['$scope', '$location','Pagination', 'GetFactory', 'PostFactory']