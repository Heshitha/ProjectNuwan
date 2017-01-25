var CreateInventoryPOController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.newSupplier = true;
    $scope.newItem = true;
    $scope.itemTypeList = [];
    $scope.poNo = 0;

    $scope.selectedItem = {
        itemID: 0,
        itemName:'',
        itemTypeID: 0,
        itemTypeName: '',
        itemContainerName: '',
        itemContainerUnit: '',
        itemMeasurements: [],
        itemContainerContains: '',
        itemOrderingQuantity: '',
        itemOrderingPrice:''
        
    };

    $scope.toggleSupplier = function () {
        $scope.newSupplier = !$scope.newSupplier;
    }

    $scope.toggleItem = function () {
        $scope.newItem = !$scope.newItem;
    }

    $scope.loadItemTypeDropDown = function () {
        var url = '/api/InventoryAPI/GetItemTypes'
        var result = PostFactory(url);
        Pace.restart();
        result.then(function (result) {
            if (result.success) {
                Pace.stop();
                $scope.itemTypeList = result.data;
            }
        });
    };

    $scope.loadItemTypeDropDown();

    $scope.loadItemProperties = function () {
        var url = '/api/InventoryAPI/GetItemTypeDetails'
        var result = PostFactory(url, $scope.selectedItem.itemTypeID);
        Pace.restart();
        result.then(function (result) {
            debugger;
            if (result.success) {
                Pace.stop();
                $scope.selectedItem.itemTypeName = result.data.Name;
                $scope.selectedItem.itemContainerName = result.data.ContainerName;
                $scope.selectedItem.itemContainerUnit = result.data.ContainerUnit;
                var ItemMeasurements = [];
                for (var i = 0; i < result.data.ItemMeasurements.length; i++) {
                    var obj = {
                        ID: result.data.ItemMeasurements[i].ID,
                        Name: result.data.ItemMeasurements[i].Name,
                        Unit: result.data.ItemMeasurements[i].Unit,
                        Value:''
                    }
                    ItemMeasurements.push(obj);
                }
                $scope.selectedItem.itemMeasurements = ItemMeasurements;
            }
        });
    };
}

CreateInventoryPOController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory'];