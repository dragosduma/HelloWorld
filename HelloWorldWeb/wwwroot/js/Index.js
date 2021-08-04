$(document).ready(function () {

    $("#createButton").click(function () {
        var newcomerName = $("#nameField").val();
      
        $.ajax({
            url: "https://localhost:44313/Home/AddTeamMember",
            method: "POST",
            data: {
                "name": newcomerName
            },
            success: function (result) {
                $("#teamMembers").append(`<li>${newcomerName}</li>`);
                $("#nameField").val("");
            }
        })  
    })
});