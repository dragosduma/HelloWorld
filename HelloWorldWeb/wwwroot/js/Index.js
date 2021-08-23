'use strict';
$(document).ready(function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

    connection.on("NewTeamMemberAdded", function (name, Id) {
        createNewcomer(name, Id)
    });

    connection.on("TeamMemberDeleted", function (Id) {
        removeTeamMemberFromList(Id);
    });

    connection.start().then(function () {
        console.log("signalr connected");
    }).catch(function (err) {
        return console.error(err.toString());
    });


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
                $("#nameField").val("");
                document.getElementById("createButton").disabled = true;
            }
        })  
    })

    $("#teamMembers").on("click", ".pencil", function () {
        var targetMemberTag = $(this).closest('li');
        var id = targetMemberTag.attr('member-id');
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("member-id", id);
        $('#classmateName').val(currentName);
        $('#editClassmate').modal('show');
    })


    $("#editClassmate").on("click", "#submit", function () {
        const id = $('#editClassmate').attr('member-id');
        console.log('submit changes to server');
        const newName = $('#classmateName').val();
        $.ajax({
            url: "/Home/UpdateMemberName",
            method: "POST",
            data: {
                memberId: id,
                name: newName
            },
            success: function (result) {
            }
        })
    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
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

function createNewcomer(name, id) {
    // Remember string interpolation
    $("#teamMembers").append(
        `<li class="member" member-id=${id}>
            <span class="name" >${name}</span>
            <span class="delete fa fa-remove" onclick="deleteMember(${id})"></span>
               <span class="pencil fa fa-pencil"></span>
        </li>`);
}

const removeTeamMemberFromList = (teamMemberId) => $(`li[member-id=${teamMemberId}]`).remove();