var UserController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.title = '';
    $scope.userName = '';
    $scope.firstName = '';
    $scope.lastName = '';
    $scope.address = '';
    $scope.country = '';
    $scope.district = '';
    $scope.mobile = '';
    $scope.telephone = '';
    $scope.email = '';
    $scope.imageExt = '';
    $scope.currentPassword = '';
    $scope.newPassword = '';
    $scope.confirmPassword = '';


    $scope.loadUserDetails = function () {
        if ($scope.userID != 0) {
            var url = '/api/UserAPI/GetUserDetails';
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
                    $scope.title = result.data.Title;
                    $scope.userName = result.data.Username;
                    $scope.firstName = result.data.FirstName;
                    $scope.lastName = result.data.LastName;
                    $scope.address = result.data.Address;
                    $scope.country = result.data.Country;
                    $scope.district = result.data.District;
                    $scope.mobile = result.data.Mobile;
                    $scope.telephone = result.data.Telephone;
                    $scope.email = result.data.Email;
                    $scope.imageExt = result.data.ImageExt;
                    console.log(result.data);
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });
        }
    }

    $scope.loadUserDetails();

    $scope.uploadPreviewImage = function (element) {
        debugger;
        var uploader = $('#txtChooseFile')[0];
        if (uploader.files && uploader.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imgPreview').attr('src', e.target.result);
                $('#imgPreview').show();
            };

            reader.readAsDataURL(uploader.files[0]);

        } else {
            $('#imgPreview').hide();
        }
    }

    $scope.uploadProfileImage = function () {
        var uploader = $('#txtChooseFile')[0];
        if (uploader.files && uploader.files[0]) {
            var data = new FormData();
            data.append('userID', $scope.userID);
            data.append('file', uploader.files[0]);
            debugger;
            var url = 'User/SaveProfileImage';
            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    ShowMessage('success', 'Image uploaded successfully.');
                    console.log(data);
                    location.reload();
                },
                error: function () {
                    ShowMessage('danger', 'Error occured while uploading. Please try again.');
                }
            });

        } else {
            ShowMessage('danger', 'Select image before uploading.');
        }
    }

    $scope.changePassword = function () {
        if ($scope.newPassword == $scope.confirmPassword) {
            var user = {
                UserID: $scope.userID,
                CurrentPassword: $scope.currentPassword,
                NewPassword: $scope.newPassword,
                ConfirmPassword: $scope.confirmPassword
            }
            var url = '/api/UserAPI/ChangePassword'
            var result = PostFactory(url, user);
            result.then(function (result) {
                if (result.success && result.data) {
                    ShowMessage('success', 'Password changed successfully.');
                    var signOutUrl = '/User/SignOut';
                    var result = PostFactory(signOutUrl);
                    result.then(function (result) {
                        if (result.success) {
                            window.location = baseUrl + '/User/Login';
                        }
                    });
                } else {
                    ShowMessage('danger', 'Error occured while processing.');
                }
            });

        } else {
            ShowMessage('danger', 'New password and confirm password mismatched.');
        }
    }

    $scope.saveUserDetails = function () {

        var user = {
            UserID: $scope.userID,
            Title: $scope.title,
            FirstName: $scope.firstName,
            LastName: $scope.lastName,
            Address: $scope.address,
            Country: $scope.country,
            District: $scope.district,
            Mobile: $scope.mobile,
            Telephone: $scope.telephone,
            Email: $scope.email
        }
        var url = '/api/UserAPI/UpdateUserDetails'
        var result = PostFactory(url, user);
        result.then(function (result) {
            if (result.success && result.data) {
                ShowMessage('success', 'Changes saved successfully.');
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }
}

UserController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']