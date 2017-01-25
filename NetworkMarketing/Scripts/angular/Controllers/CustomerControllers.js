var SearchCustomerController = function ($scope, $location, Pagination, PostFactory, GetFactory) {
    $scope.customerName = '';
    $scope.customerRef = '';
    $scope.address = '';
    $scope.contactName = '';
    $scope.customerList = [];
    $scope.pagination = Pagination.getNew(10);

    $scope.searchCustomer = function () {
        var search = {
            CustomerName: $scope.customerName,
            ContactName: $scope.contactName,
            OurRef: $scope.customerRef,
            Address: $scope.address,
        };
        var url = 'api/CustomerAPI/GetCustomerListForSearchResult'
        var result = PostFactory(url, search);
        result.then(function (result) {
            if (result.success) {
                $scope.customerList = result.data;
                $scope.pagination.numPages = Math.ceil($scope.customerList.length / $scope.pagination.perPage);
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }

    $scope.searchCustomer();

    $scope.clearSearch = function () {
        $scope.customerName = '';
        $scope.customerRef = '';
        $scope.address = '';
        $scope.contactName = '';
        $scope.searchCustomer();
    }

    $scope.openCustomer = function (customerID) {
        $location.path('/openCustomer/' + customerID);
    };
}

SearchCustomerController.$inject = ['$scope', '$location', 'Pagination', 'PostFactory', 'GetFactory']

var CustomerController = function ($scope, $location, PostFactory, GetFactory) {
    $scope.customerID = Number($('#hdnCustomerID').val());
    $scope.customerName = '';
    $scope.customerRef = '';
    $scope.address = '';
    $scope.generalLine = '';
    $scope.faxLine = '',
    $scope.generalEmail = '',
    $scope.website = '',
    $scope.vatNo = '',
    $scope.contacts = [];
    $scope.hasUnsavedContact = false;
    $scope.currentContactID = 0;
    $scope.currentContactIndex = 0;

    $scope.loadCustomer = function () {
        if ($scope.customerID != 0) {
            var url = 'api/CustomerAPI/GetCustomer'
            var result = PostFactory(url, $scope.customerID);
            result.then(function (result) {
                if (result.success) {
                    $scope.customerName = result.data.CustomerName;
                    $scope.customerRef = result.data.CustomerRef;
                    $scope.address = result.data.Address;
                    $scope.generalLine = result.data.GeneralLine;
                    $scope.faxLine = result.data.FaxLine,
                    $scope.generalEmail = result.data.GeneralEmail,
                    $scope.website = result.data.Website,
                    $scope.vatNo = result.data.VatNo,
                    $scope.contacts = result.data.ContactList;
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    }

    $scope.loadCustomer();

    $scope.addNewContact = function () {
        if (!$scope.hasUnsavedContact) {
            var obj = {
                ContactID : 0,
                ContactName : '',
                Email : '',
                Mobile : '',
                FixedLine : '',
            }
            $scope.contacts.push(obj);
            $scope.hasUnsavedContact = true;
        }
    }

    $scope.removeContact = function (contactID) {
        for (var i = 0; i < $scope.contacts.length; i++) {
            if ($scope.contacts[i].ContactID == contactID) {
                $scope.contacts.splice(i, 1);
            }
        }
        if (contactID == 0) {
            $scope.hasUnsavedContact = false;
        }
    }

    $scope.saveCustomer = function () {
        var customer = {
            CustomerID: $scope.customerID,
            CustomerName: $scope.customerName,
            CustomerRef: $scope.customerRef,
            Address: $scope.address,
            GeneralLine: $scope.generalLine,
            FaxLine: $scope.faxLine,
            GeneralEmail: $scope.generalEmail,
            Website: $scope.website,
            VatNo: $scope.vatNo
        };
        var url = 'api/CustomerAPI/SaveCustomer'
        var result = PostFactory(url, customer);
        result.then(function (result) {
            if (result.success) {
                if ($scope.customerID == 0) {
                    $scope.addNewContact();
                }
                $scope.customerID = Number(result.data);
                ShowMessage('success', 'Customer details saved successfully.');
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }

    $scope.saveContact = function (contactID) {
        $scope.currentContactID = contactID;
        $scope.currentContactIndex = 0;
        debugger;
        for (var i = 0; i < $scope.contacts.length; i++) {
            if ($scope.contacts[i].ContactID == contactID) {
                $scope.currentContactIndex = i;
                break;
            }
        }
        var contact = JSON.stringify({
            ContactID: $scope.contacts[$scope.currentContactIndex].ContactID,
            ContactName: $scope.contacts[$scope.currentContactIndex].ContactName,
            Email: $scope.contacts[$scope.currentContactIndex].Email,
            Mobile: $scope.contacts[$scope.currentContactIndex].Mobile,
            FixedLine: $scope.contacts[$scope.currentContactIndex].FixedLine,
            Customer: { CustomerID: $scope.customerID }
        })
        var url = 'api/CustomerAPI/SaveContact'
        var result = PostFactory(url, contact);
        result.then(function (result) {
            debugger;
            if (result.success && result.data != 0) {
                $scope.contacts[$scope.currentContactIndex].ContactID = Number(result.data);
                ShowMessage('success', 'Contact details saved successfully.');
                if ($scope.currentContactID == 0) {
                    $scope.hasUnsavedContact = false;
                }
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });

    }

}

CustomerController.$inject = ['$scope', '$location', 'PostFactory', 'GetFactory']