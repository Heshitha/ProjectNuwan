var FinancialController = function ($scope, $location, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.EpinVal=0;
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
                        $scope.GetUserTransactions();
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

    $scope.GenerateEpins = function () {
        var EpinGenerateModel = {
            userID: $scope.userID,
            TotalPoints: $scope.Points,
            EpinVal: $scope.EpinVal,
            NoOfPins: $scope.NoOfPins,
        };
        if ($scope.userID != 0) {
            var url = '/api/FinancialAPI/GenerateEpins';
            var result = PostFactory(url, EpinGenerateModel);
            result.then(function (result) {
                if (result.success && result.data) {
                    if (result.data == 0) {
                        ShowMessage('danger', 'Opps We got an error.');
                    }
                    else if (result.data == -1) {
                        ShowMessage('danger', 'Insufficient Points.');
                    }
                    else {
                        ShowMessage('success', 'Epin Generated.');
                        $scope.GetUserTransactions();
                    }
                }
                else {
                    ShowMessage('danger', 'Opps We got an error.');
                }
            });
        }
    }

    $scope.GetUserTransactions = function () {
        if ($scope.userID != 0) {
            var url = '/api/TransactionAPI/GetUserTransactions';
            var result = PostFactory(url, $scope.userID);
            debugger;
            result.then(function (result) {
                if (result.success) {
                    $scope.Points = result.data;
                }
                else {
                    $scope.Points = 0.00;
                }
            });
        }
    }
    $scope.GetUserTransactions();

};


FinancialController.$inject = ['$scope', '$location', 'PostFactory']