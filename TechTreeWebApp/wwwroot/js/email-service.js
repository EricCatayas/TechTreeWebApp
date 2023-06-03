$(document).ready(function () {
    $("#mailform").submit(function (e) {
        e.preventDefault(); // Prevent the form from submitting normally

        var formData = $(this).serialize(); // Serialize the form

        var mailPopUp = $('<div>');

        $.ajax({
            url: "/Home/SendEmail",
            type: "POST",
            data: formData,
            success: function (result) {
                mailPopUp
                    .removeClass()
                    .addClass("alert alert-success alert-dismissible")
                    .attr("role", "alert")
                    .text("Your message has been sent.")
                    .append('<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');

                $('#mailResult').append(mailPopUp);
                $('input[name="name"]').val('');
                $('input[name="recipientEmail"]').val('');
                $('textarea[name="body"]').val('');
            },
            error: function (error) {
                mailPopUp
                    .removeClass()
                    .addClass("alert alert-danger alert-dismissible")
                    .attr("role", "alert")
                    .text("Error, something went wrong. Please try again later")
                    .append('<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>');

                $('#mailResult').append(mailPopUp);
            }
        });
    });
});