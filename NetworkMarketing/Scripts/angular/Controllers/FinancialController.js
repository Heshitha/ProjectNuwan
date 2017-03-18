var FinancialController = function ($scope, $location, PostFactory)
{
    $scope.userID = Number($('#hdnUserID').val());
    debugger;
    $scope.SaveBankDetails = function () {
        var BankTransferModel = {
            userID: $scope.userID,
            TransferType: $scope.TransferType,
            AccType: $scope.AccType,
            AccountName: $scope.AccountName,
            AccountNumber: $scope.AccountNumber
        };

        if ($scope.userID != 0) {
            var url = '/api/FinancialAPI/SaveBankDetails';
            var result = PostFactory(url, BankTransferModel);
            result.then(function (result) {
                if (result.success && result.data) {
                    if (result.data == 0) {
                        ShowMessage('danger', 'Invalied Transaction Key.');

                    }
                    else {
                        ShowMessage('success', 'Bank Details added.');
                    }
                }
                else {
                    ShowMessage('danger', 'Opps We got an error.');
                }
            });
        }
    };
}

FinancialController.$inject = ['$scope', '$location', 'PostFactory']