$(document).ready(function () {
    $("#mailform").submit(function (e) {
        e.preventDefault(); // Prevent the form from submitting normally

        var formData = $(this).serialize(); // Serialize the form data

        $.ajax({
            url: "/Home/SendEmail",
            type: "POST",
            data: formData,
            success: function (result) {
                $("#mailResult")
                    .removeClass()
                    .addClass("alert alert-success")
                    .attr("role", "alert")
                    .text("Email sent! Your message is highly appreciated.");

                $('input[name="name"]').val('');
                $('input[name="recipientEmail"]').val('');
                $('textarea[name="body"]').val('');
            },
            error: function (error) {
                $("#mailResult")
                    .removeClass()
                    .addClass("alert alert-danger")
                    .attr("role", "alert")
                    .text("Error! Something went wrong.");
            }
        });
    });
});