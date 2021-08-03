// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

    $(document).ready(function () {

        $("#createButton").click(function () {
            var newcomerName = $("#nameField").val();

            $("#teamMembers").append(`<li>${newcomerName}</li>`);

            $("#nameField").val("");
        })
    });
 
// Write your JavaScript code.
