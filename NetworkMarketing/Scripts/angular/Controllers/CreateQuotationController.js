var CreateQuotationController = function ($scope, $compile, CreateQuotationFactory, PostFactory, GetFactory) {
    $scope.quotationForm = {
        customerName: '',
        address: '',
        ourRef: '',
        contactName: '',
        contactEmail: '',
        quotationNo: '',
        quotationID: 0,
        quotationDate: '',
        isQuotationSaved: false,
        newQuotation: true,
        itemList: [],
        newCustomer: true,
        customerID: 0,
        contactID:0,
        newContact: true,
        customerList: [],
        contactList:[]
    };

    $scope.itemForm = {
        itemName: '',
        description: '',
        defaultQuantity: '',
        defaultPrice: '',
        itemSelected: false
    }

    $scope.changeNewCustomerCheckBox = function () {
        if ($scope.quotationForm.newCustomer) {
            $scope.quotationForm.newContact = true;
        } else {
            $scope.quotationForm.newContact = false;
        }
    };

    $scope.loadQuotation = function () {
        //debugger;
        var quotationID = $('#hdnQuotationID').val();
        if (quotationID != '' && Number(quotationID) != 0) {
            $scope.quotationForm.quotationID = Number(quotationID);
            $scope.quotationForm.isQuotationSaved = true;
            $scope.quotationForm.newQuotation = false;
            var data = {
                quotationID: $scope.quotationForm.quotationID
            }
            var url = '/api/QuotationAPI/GetQuotation'
            var result = PostFactory(url, $scope.quotationForm.quotationID);
            result.then(function (result) {
                if (result.success) {
                    console.log(result.data);
                    $scope.quotationForm.customerName = result.data.CustomerName;
                    $scope.quotationForm.address = result.data.Address;
                    $scope.quotationForm.ourRef = result.data.OurRef;
                    $scope.quotationForm.contactName = result.data.ContactName;
                    $scope.quotationForm.contactEmail = result.data.ContactEmail;
                    $scope.quotationForm.quotationNo = result.data.QuotationNo;
                    $scope.quotationForm.quotationDate = result.data.QuotationDate;
                    $scope.quotationForm.itemList = result.data.ItemList;
                    $('#dvAddedItemHolder').slideDown();
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    };

    $scope.loadQuotation();



    $scope.addNewQuantityAndPrice = function () {
        var html = '';
        html += '<div class="row newQuantityRow">';
        html += '<div class="col-md-6">';
        html += '<div class="form-group">';
        html += '<label for="txtQuantity">Quantity</label>';
        html += '<input type="text" class="form-control txtQuantity" placeholder="Enter quantity" required>';
        html += '</div>';
        html += '</div>';
        html += '<div class="col-md-6">';
        html += '<div class="form-group">';
        html += '<label for="txtPrice">Price</label>';
        html += '<div class="input-group">';
        html += '<input type="text" class="form-control txtPrice" placeholder="Enter price" required>';
        html += '<span class="input-group-btn">';
        html += '<button type="button" class="btn btn-default btn-flat" ng-click="removeQuantityAndPrice($event)">';
        html += '<i class="fa fa-minus-circle"></i>';
        html += '</button>';
        html += '</span>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';

        var elem = angular.element(html);
        $('#quantityAndPriceHolder').append(elem);
        $compile(elem)($scope);
    };

    $scope.removeQuantityAndPrice = function ($event) {
        var rowElem = $($event.currentTarget).parentsUntil('#quantityAndPriceHolder');
        $(rowElem).remove();
    };

    $scope.insertToQuotation = function () {
        var html = '';
        html += '<tr class="quotItemRow" data-item-name="' + $scope.itemForm.itemName + '" data-description="' + $scope.itemForm.description + '" data-quantity="' + $scope.itemForm.defaultQuantity + '" data-price="' + $scope.itemForm.defaultPrice + '" ng-click="selectInsertedItem($event)">'
        html += '<td class="itemName">' + $scope.itemForm.itemName + '</td>'
        html += '<td class="description">' + $scope.itemForm.description + '</td>'
        html += '<td class="quantity">' + $scope.itemForm.defaultQuantity + '</td>'
        html += '<td class="price">Rs. ' + $scope.itemForm.defaultPrice + '</td>'
        html += '</tr>'

        var additionalQuantites = $('.newQuantityRow');
        if (additionalQuantites.length > 0) {
            var quantity = '';
            var price = '';
            for (var i = 0; i < additionalQuantites.length; i++) {
                quantity = $(additionalQuantites[i]).find('.txtQuantity').val();
                price = $(additionalQuantites[i]).find('.txtPrice').val();

                html += '<tr class="quotItemRow" data-item-name="' + $scope.itemForm.itemName + '" data-description="' + $scope.itemForm.description + '" data-quantity="' + quantity + '" data-price="' + price + '" ng-click="selectInsertedItem($event)">'
                html += '<td class="itemName"></td>'
                html += '<td class="description"></td>'
                html += '<td class="quantity">' + quantity + '</td>'
                html += '<td class="price">Rs. ' + price + '</td>'
                html += '</tr>'
            }
        }

        if ($scope.itemForm.itemSelected == true) {
            $('.quotItemRow[data-item-name="' + $scope.itemForm.itemName + '"]').remove();
        }

        var elem = angular.element(html);
        $('#tblAddedItems').append(elem);
        $compile(elem)($scope);

        $scope.resetItemForm();
        $('#dvAddedItemHolder').slideDown();
    }

    $scope.selectInsertedItem = function ($event) {

        var itemName = $($event.currentTarget).attr('data-item-name');

        var description = $($event.currentTarget).attr('data-description');
        var allItems = $('.quotItemRow[data-item-name="' + itemName + '"]');

        $scope.itemForm.itemName = itemName;
        $scope.itemForm.description = description;
        $scope.itemForm.defaultQuantity = $(allItems[0]).attr('data-quantity');
        $scope.itemForm.defaultPrice = $(allItems[0]).attr('data-price');
        $scope.itemForm.itemSelected = true;
        $('.newQuantityRow').remove();

        if (allItems.length > 1) {
            var html = '';
            for (var i = 1; i < allItems.length; i++) {
                var quantity = $(allItems[i]).attr('data-quantity');
                var price = $(allItems[i]).attr('data-price');

                html += '<div class="row newQuantityRow">';
                html += '<div class="col-md-6">';
                html += '<div class="form-group">';
                html += '<label for="txtQuantity">Quantity</label>';
                html += '<input type="text" class="form-control txtQuantity" placeholder="Enter quantity" required value="' + quantity + '">';
                html += '</div>';
                html += '</div>';
                html += '<div class="col-md-6">';
                html += '<div class="form-group">';
                html += '<label for="txtPrice">Price</label>';
                html += '<div class="input-group">';
                html += '<input type="text" class="form-control txtPrice" placeholder="Enter price" required value="' + price + '">';
                html += '<span class="input-group-btn">';
                html += '<button type="button" class="btn btn-default btn-flat" ng-click="removeQuantityAndPrice($event)">';
                html += '<i class="fa fa-minus-circle"></i>';
                html += '</button>';
                html += '</span>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
                html += '</div>';
            }
            var elem = angular.element(html);
            $('#quantityAndPriceHolder').append(elem);
            $compile(elem)($scope);
        }
    }

    $scope.resetItemForm = function () {
        $scope.itemForm.itemName = '';
        $scope.itemForm.description = '';
        $scope.itemForm.defaultQuantity = '';
        $scope.itemForm.defaultPrice = '';
        $scope.itemForm.itemSelected = false;
        $('.newQuantityRow').remove();
    }

    $scope.removeFromQuotation = function () {
        var allItems = $('.quotItemRow[data-item-name="' + $scope.itemForm.itemName + '"]');
        allItems.remove();
        $scope.resetItemForm();
        var itemsInTable = $('.quotItemRow');
        if (itemsInTable.length == 0) {
            $('#dvAddedItemHolder').slideUp();
        }
    }

    $scope.saveQuotation = function () {
        if (!$('#customerDetails')[0].checkValidity()) {
            $('#hdnBtnFormSubmit').click();
            $('#customerDetails')[0].focus();
            return false;
        } else {
            var allItems = $('.quotItemRow');
            if (allItems.length > 0) {
                var itemList = Array();
                var prevItem = $(allItems[0]).attr('data-item-name');
                var prevDescription = $(allItems[0]).attr('data-description');
                var itemPriceList = Array();
                for (var i = 0; i < allItems.length; i++) {
                    //debugger;
                    if (prevItem != $(allItems[i]).attr('data-item-name')) {
                        var obj = {
                            ItemName: prevItem,
                            Description: prevDescription,
                            QuanityAndPrices: itemPriceList
                        };
                        itemList.push(obj);
                        itemPriceList = Array();
                        prevItem = $(allItems[i]).attr('data-item-name');
                        prevDescription = $(allItems[i]).attr('data-description');

                    }
                    var quantityAndPriceObj = {
                        Quantity: $(allItems[i]).attr('data-quantity'),
                        Price: $(allItems[i]).attr('data-price')
                    };
                    itemPriceList.push(quantityAndPriceObj);


                    //var obj = {
                    //    ItemName: $(allItems[i]).attr('data-item-name'),
                    //    Description: $(allItems[i]).attr('data-description'),
                    //    Quantity: $(allItems[i]).attr('data-quantity'),
                    //    Price: $(allItems[i]).attr('data-price')
                    //};
                    //itemList.push(obj);
                }
                var obj = {
                    ItemName: prevItem,
                    Description: prevDescription,
                    QuanityAndPrices: itemPriceList
                };
                itemList.push(obj);

                var quotation = {
                    CustomerName: $scope.quotationForm.customerName,
                    Address: $scope.quotationForm.address,
                    OurRef: $scope.quotationForm.ourRef,
                    ContactName: $scope.quotationForm.contactName,
                    ContactEmail: $scope.quotationForm.contactEmail,
                    QuotationNo: $scope.quotationForm.quotationNo,
                    ItemList: itemList
                };

                console.log(quotation);
                var result = CreateQuotationFactory(quotation);
                result.then(function (result) {
                    if (result.success) {
                        ShowMessage('success', 'Quotation saved successfully.');
                        $scope.quotationForm.quotationID = result.data;
                        $scope.quotationForm.isQuotationSaved = true;
                        $scope.quotationForm.newQuotation = false;
                        $scope.quotationForm.newCustomer = true;
                        $scope.quotationForm.newContact = true;
                    } else {
                        ShowMessage('danger', 'Error occured while saving the quotation. Please try again.');
                    }
                });
            } else {
                ShowMessage('warning', 'Quotation should contain atleast one item infomation. Please add an item and try again');
                return false;
            }
        }
    }

    $scope.printQuotation = function () {
        var url = baseUrl + '/Quotation/PrintQuotation?quotationID=' + $scope.quotationForm.quotationID;
        var win = window.open(url, "_blank", "toolbar=yes,scrollbars=yes,resizable=yes");
        win.print();
    }

    $scope.emailQuotation = function () {
        //debugger;
        var url = '/api/QuotationAPI/EmailQuotation'
        var result = PostFactory(url, $scope.quotationForm.quotationID);
        Pace.restart();
        result.then(function (result) {
            if (result.success && (result.data == 'true' || result.data == 'True')) {
                Pace.stop();
                console.log(result.data);
                ShowMessage('success', 'Email sent successfully.');
            } else {
                Pace.stop();
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }

    $scope.loadCustomerDropdown = function () {
        var url = '/api/CustomerAPI/GetCustomerListForCustomerSelect'
        var result = GetFactory(url);
        Pace.restart();
        result.then(function (result) {
            if (result.success) {
                Pace.stop();
                $scope.quotationForm.customerList = result.data;
            }
        });
    };

    $scope.loadCustomerDropdown();

    $scope.loadCustomerDetails = function () {
        var url = '/api/CustomerAPI/GetCustomer'
        var result = PostFactory(url, $scope.quotationForm.customerID);
        Pace.restart();
        result.then(function (result) {
            if (result.success) {
                Pace.stop();
                $scope.quotationForm.customerName = result.data.CustomerName,
                $scope.quotationForm.address = result.data.Address,
                $scope.quotationForm.ourRef = result.data.CustomerRef,
                $scope.quotationForm.contactList = result.data.ContactList
            }
        });
    };

    $scope.loadContactDetails = function () {
        var url = '/api/CustomerAPI/GetContact'
        var result = PostFactory(url, $scope.quotationForm.contactID);
        Pace.restart();
        result.then(function (result) {
            if (result.success) {
                Pace.stop();
                $scope.quotationForm.contactName = result.data.ContactName
                $scope.quotationForm.contactEmail = result.data.Email
            }
        });
    }
}

CreateQuotationController.$inject = ['$scope', '$compile', 'CreateQuotationFactory', 'PostFactory', 'GetFactory'];