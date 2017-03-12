var TransactionController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.TransactionData = Array();

    $scope.GetAllTransactionsByUser = function () {
        if ($scope.userID != 0) {
            var url = 'api/TransactionAPI/GetAllTransactionsByUser';
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
                    $scope.TransactionID = result.data.TransactionID;
                    $scope.SenderName = result.data.SenderName;
                    $scope.RecieverName = result.data.RecieverName;
                    $scope.Amount = result.data.Amount;
                    $scope.Description = result.data.Description;
                    $scope.TransactionDate = result.data.TransactionDate;
                    $scope.TransactionType = result.data.TransactionType;
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    }
}

TransactionController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']