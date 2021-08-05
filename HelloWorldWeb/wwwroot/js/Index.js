$(document).ready(function () {

    $("#createButton").click(function () {
        var newcomerName = $("#nameField").val();
        var length = $("#teamMembers").children().length;
      
        $.ajax({
            url: "/Home/AddTeamMember",
            method: "POST",
            data: {
                "name": newcomerName
            },
            success: function (result) {
                $("#teamMembers").append(`
            <li class="member">
            <span class="memberName">${newcomerName}</span>
            <span class="delete fa fa-remove" onclick="deleteMember(${length})"></span>
            <span class="edit fa fa-pencil"></span>
             </li>`);
                $("#nameField").val("");
                document.getElementById("createButton").disabled = true;
            }
        })  
    })
});

(function () {

    $('#nameField').on('change textInput input', function () {
        var inputVal = this.value;
        if (inputVal != "") {
            document.getElementById("createButton").disabled = false;
        } else {
            document.getElementById("createButton").disabled = true;
        }
    });
}());

function deleteMember(index) {
    $.ajax({
        url: "/Home/RemoveMember",
        method: "DELETE",
        data: {
            memberIndex: index
        },
        success: function (result) {
            location.reload();
        }
    })
}

(function () {
    $("#clearButton").click(function () {
        document.getElementById("nameField").value = "";
    });
}());