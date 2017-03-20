var TransactionController = function ($scope, $location, Pagination, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.TransactionData = [];
    $scope.pagination = Pagination.getNew(10);

    //debugger;

    $scope.GetAllTransactionsByUser = function () {
        if ($scope.userID != 0) {
            var url = '/api/TransactionAPI/GetAllTransactionsByUser';
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
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


var TransactionKeyController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    debugger;

    $scope.CheckTranactionKey = function ($Page) {
        var TransactionKeyMV = {

            UserID: $scope.userID,
            TransctionKey: $scope.TransactionKey
        }

        if ($scope.userID != 0) {
            var url = '/api/TransactionAPI/CheckTransactionKey';
            var result = PostFactory(url, TransactionKeyMV);
            result.then(function (result) {
                if (result.success && result.data) {
                    if (result.data == "false") {
                        ShowMessage('danger', 'Invalied Transaction Key.');
                    }
                    else {
                        window.location = baseUrl + '#/'+$Page+'/' + $scope.TransactionKey;
                    }
                }
                else {
                ShowMessage('danger', 'Opps We got an error.');
            }
            });
        }

    };
    //$scope.CheckTranactionKey();
};

TransactionKeyController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']

