$(function(){
    //	1. When the On-change Event is fired, the partialview containing the User checkbox List View data is added to the appropriate element in the document obj model
    $("#SaveSelectedUsers_btn").prop('disabled', true);
    $(document).on('change', 'select', retrieveUsers);

    function retrieveUsers() {        
        if (this.value != null) {
            var categoryId = $("#SelectCategoryId").val();
            var url = "/Admin/UserCategory/GetUsersForCategory?categoryId=" + categoryId;

            $.ajax({
                type: "GET",
                url: url,   
                success: function (data) {
                    $("#UsersCheckList").html(data); 
                    $("#SaveSelectedUsers_btn").prop('disabled', false);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    var errorText = "An error has occured. The error will be sent to the Administrator. Please try again later";

                    PresentClosableBootstrapAlert("#alert_placeholder_usercat_index", "danger", "Error!", errorText);

                    console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
                }
            })
        }
        else {
            $("#SaveSelectedUsers_btn").prop('disabled', true);
            $("input[type=checkbox]").prop('checked', false);
            $("input[type=checkbox]").prop('disabled', true);
        }

    }
        
    //  2.When the administrator saves the relevant input data to the system
    $("#SaveSelectedUsers_btn").click(function () {
        var url = "/Admin/UserCategory/SaveSelectedUsers";
        var categoryId = $("#SelectCategoryId").val();
        var antiforgeryToken = $("input[name='__RequestVerificationToken']").val();
        var usersSelected = [];

        DisableControls(true);        
        $(".progress").show("fade"); //<-- progress bar
        //  3.Collect the data that will be passed into the server. This part the Admin chooses the List of Users, checkboxes that are "checked" are instanciated as a userModel
        $("input[type=checkbox]:checked").each(function () {
            var userModel = {
                Id: $(this).attr("value"), 
            };
            usersSelected.push(userModel);
        });
        var usersSelectedForCategory = {
            __RequestVerificationToken: antiforgeryToken,
            CategoryId: categoryId,
            UsersSelected: usersSelected
        }; 
        $.ajax({
            type: "POST",
            url: url,
            data: usersSelectedForCategory,
            success: function (data) {
                $("#UsersCheckList").html(data);
                $(".progress").hide("fade", function () {
                    $(".alert-success").fadeTo(2000, 500).slideUp(500, function () { //slideUp(time_arg)
                        DisableControls(false);
                    })
                })
            },
            error: function (xhr, ajaxOptions, thrownError) {
                var errorText = "An error has occured. The error will be sent to the Administrator. Please try again later";

                PresentClosableBootstrapAlert("#alert_placeholder_usercat_index", "danger", "Error!", errorText);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);

                DisableControls(false);
            }
        })
    });
    function DisableControls(disable) {
        $("#SaveSelectedUsers_btn").prop('disabled', disable);
        $('select').prop('disabled', disable);
        $("input[type=checkbox]").prop('disabled', disable);
    }
    
});