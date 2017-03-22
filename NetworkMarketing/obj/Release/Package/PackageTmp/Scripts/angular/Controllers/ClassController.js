var ClassController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.classList = Array();
    $scope.currentIndex = 0;
    $scope.classDetails = Array();
    $scope.classID = 0;
    $scope.classType = '';

    $scope.loadClassList = function () {
        if ($scope.userID != 0) {
            var url = '/api/ClassAPI/GetClassHistoty';
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
                    $scope.classList = result.data;
                    $scope.classID = $scope.classList[$scope.currentIndex];
                    $scope.loadClassDetails();
                    console.log(result.data);
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    }

    $scope.loadClassList();

    $scope.loadClassDetails = function () {
        var url = '/api/ClassAPI/GetClassDetailsForViewClass';
        var classID = $scope.classID;
        var result = PostFactory(url, classID);
        result.then(function (result) {
            if (result.success) {
                $scope.classDetails = Array();
                $scope.classType = result.data.ClassType;
                for (var i = 0; i < 13; i++) {
                    if (typeof (result.data.UserList[i]) !== "undefined") {
                        $scope.classDetails.push(result.data.UserList[i]);
                    } else {
                        var obj = {
                            FirstName: 'No User',
                            FollowerCount: 0,
                            Followers: [],
                            ImageExt: '',
                            LastName: '',
                            Position: 0,
                            UserID: 0,
                            UserName: '--'
                        }
                        $scope.classDetails.push(obj);
                    }
                }
                console.log(result.data);
                console.log($scope.classDetails);
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }
}

ClassController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']