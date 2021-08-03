$(document).ready(function () {

    $("#createButton").click(function () {
        var newcomerName = $("#nameField").val();
        $("#teamMembers").append(`<li>${newcomerName}</li>`);
        $("#nameField").val("");
    })
});