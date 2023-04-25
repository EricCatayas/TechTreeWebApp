$(function () { //Ref: UserLogin.js

    var RegistrationModal = new bootstrap.Modal(document.getElementById('UserRegistrationModal'), {}) //Bootstrap 5

    $("#UserRegistrationModal").on('hidden.bs.modal', function (e) { //check // Must be set explicitly to 0 because the user might close the registration modal from _DisplayCardRowPartial
        $("#UserRegistrationModal input[name='CategoryId']").val('0');
    })

    $('.RegisterLink').click(onCourseCardClick); // When the course card is clicked by a public user R: _DisplayCardRowPartial.cshtml  -- " " doesn't work       

    function onCourseCardClick() {
        RegistrationModal.show();
        $("#UserRegistrationModal input[name='CategoryId']").val($(this).attr('data-categoryId'));
    }

    $("#UserRegistrationModal button[name='register']").on("click", onUserClick);

    function onUserClick() {
                
        var url = "/UserAuth/RegisterUser";

        var antiForgeryToken = $("#UserRegistrationModal input[name='__RequestVerificationToken']").val();
        var username = $("#username").val();
        var email = $("#UserRegistrationModal input[name='Email']").val();
        var password = $("#UserRegistrationModal input[name='Password']").val();
        var confirmPassword = $("#UserRegistrationModal input[name='ConfirmPassword']").val();
        var firstname = $("#UserRegistrationModal input[name='FirstName']").val();
        var lastname = $("#UserRegistrationModal input[name='LastName']").val();
        var address1 = $("#UserRegistrationModal input[name='Address1']").val();
        var address2 = $("#UserRegistrationModal input[name='Address2']").val();
        var postcode = $("#UserRegistrationModal input[name='PostCode']").val();
        var phonenumber = $("#UserRegistrationModal input[name='PhoneNumber']").val();
        var categoryId = $("#UserRegistrationModal input[name='CategoryId']").val();

        var user = {
            __RequestVerificationToken: antiForgeryToken,
            UserName: username,
            Email: email,   
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstname,
            LastName: lastname,
            Address1: address1,
            Address2: address2,
            PostCode: postcode,
            PhoneNumber: phonenumber,
            CategoryId: categoryId,
            AcceptTermsAndAgreement: true
        };

        $.ajax({
            type: "POST",
            url: url,
            data: user,
            success: function (data) {
                var parsed = $.parseHTML(data)
                var hasErrors = $(parsed).find("input[name='RegistrationInValid']").val() == "true";

                if (hasErrors) {
                    $("#UserRegistrationModal").html(data); 

                    var userLoginButton = $("#UserRegistrationModal button[name='register']").click(onUserClick);
                    var form = $("#UserRegistrationForm");
                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                }
                else {
                    location.href = '/Home/Index';
                }

            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_registration", "danger", "Error!", errorText);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    };
});