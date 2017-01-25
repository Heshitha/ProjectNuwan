var UserController = function ($scope, $location, GetFactory, PostFactory) {
    $scope.userID = Number($('#hdnUserID').val());
    $scope.myprofile = Boolean($('#hdnMyProfile').val())
    $scope.userName = '';
    $scope.designation = '';
    $scope.email = '';
    $scope.address = '';
    $scope.fixedLine = '';
    $scope.imageExtension = '';
    $scope.imagePath = '';
    $scope.currentPassword = '';
    $scope.newPassword = '';
    $scope.confirmPassword = '';

    $scope.loadUserDetails = function () {
        if ($scope.userID != 0) {
            var url = 'api/UserAPI/GetUserDetails'
            var result = PostFactory(url, $scope.userID);
            result.then(function (result) {
                if (result.success) {
                    $scope.userName = result.data.Name;
                    $scope.designation = result.data.Designation;
                    $scope.address = result.data.Address;
                    $scope.email = result.data.Email;
                    $scope.fixedLine = result.data.Telephone;
                    $scope.imageExtension = result.data.ImageExtention;
                    $scope.imagePath = baseUrl + 'Uploads/UserImages/' + $scope.userID + $scope.imageExtension;
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
            data.append('userID', 1);
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
            var url = 'api/UserAPI/ChangePassword'
            var result = PostFactory(url, user);
            result.then(function (result) {
                if (result.success && result.data) {
                    ShowMessage('success', 'Password changed successfully.');
                    var signOutUrl = '/Login/SignOut';
                    var result = PostFactory(signOutUrl);
                    result.then(function (result) {
                        if (result.success) {
                            window.location = document.location.origin + baseUrl + 'Login';
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

    $scope.saveUser = function () {
        var user = {
            UserID: $scope.userID,
            Name: $scope.userName,
            Address: $scope.address,
            Telephone: $scope.fixedLine,
            Email: $scope.email,
            UserCategory: { UserCategoryID: 1 },
            Designation: $scope.designation,
        }
        var url = 'api/UserAPI/SaveUser'
        var result = PostFactory(url, user);
        result.then(function (result) {
            if (result.success && result.data != 0) {
                ShowMessage('success', 'User details saved successfully.');
                $scope.userID = result.data;
            } else {
                ShowMessage('danger', 'Error occured while processing.');
            }
        });
    }
}

UserController.$inject = ['$scope', '$location', 'GetFactory', 'PostFactory']