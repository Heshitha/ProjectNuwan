var LandingPageController = function ($scope, $location, PostFactory) {
    $scope.models = {
        helloAngular: 'I work!'
    };

    makeMenuActive('Profile');

    $scope.SignOut = function () {
        //debugger;
        var url = '/User/SignOut';
        var data = {};
        var result = PostFactory(url, data);
        result.then(function (result) {
            if (result.success) {
                window.location = baseUrl + '/User/Login';
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }
}

LandingPageController.$inject = ['$scope', '$location', 'PostFactory'];