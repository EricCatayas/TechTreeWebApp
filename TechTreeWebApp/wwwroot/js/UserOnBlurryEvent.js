$(function () { //Ref: UserLogin.js

    $("#UserRegistrationModal button[name='register']").prop('disabled', true);
    $("#checkbox").click(onTermsAndAgreementClick);

    function onTermsAndAgreementClick() { 
        if ($(this).is(":checked")){ 
            $("#UserRegistrationModal button[name='register']").prop('disabled', false);
        }
        else {
            $("#UserRegistrationModal button[name='register']").prop('disabled', true);
        }
    }
 
    $(document).on('change', '#username', function () { 
        var username = $("#UserRegistrationModal input[name='UserName']").val();
        if (username == "") { return; }
        //var url = "/UserAuth/UserNameExists?userName=" + username;
        var url = "UserAuth/UserNameExists?userName=" + username;
        $.ajax({ //calling UserNameExists() in UserAuthController
            type: "GET",
            url: url,                                                       
            success: function (data) {
                if (data == true) {
                    PresentClosableBootstrapAlert("#alert_placeholder_registration", "warning", "Invalid Username", "The current username has already been taken");
                }
                else {
                    CloseBootstrapAlert("#alert_placeholder_registration");
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "Status: " + xhr.status + " - " + xhr.statusText;

                PresentClosableBootstrapAlert("#alert_placeholder_registration", "danger", "Error!", errorText);

                console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
            }
        });
    }); 
    // blur event handler fires only once
    //$("#username").blur(function () {
    //    var username = $("#UserRegistrationModal input[name='UserName']").val();
    //    var url = "UserAuth/UserNameExists?userName=" + username;
    //    var alertplaceholder = $("#alert_placeholder_registration");
    //    $.ajax({ //calling UserNameExists() in UserAuthController
    //        type: "GET",
    //        url: url,
    //        success: function (data) {
    //            if (data == true) {
    //                PresentClosableBootstrapAlert(alertplaceholder, "warning", "Invalid Username", "The current username has already been taken");
    //            }
    //            else {
    //                CloseBootstrapAlert(alertplaceholder);
    //            }
    //        }
    //    });
    //}).blur();   
});