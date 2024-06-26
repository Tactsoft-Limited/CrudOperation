﻿//Student Modal Popup
var AddStudent = function () {
    var url = "/Student/Create";
    $('#titleMediumModal').html("Student Create");
    loadMediumModal(url);
};

var EditStudent = function (id) {
    var url = "/Student/Edit?id=" + id;
    $('#titleMediumModal').html("Student Update");
    loadMediumModal(url);
};

var DelStudent = function (id) {
    var url = "/Student/Details?id=" + id;
    $('#titleMediumModal').html("Student Details");
    loadMediumModal(url);
};



var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Student/Delete?id=" + id,
                success: function (result) {
                    var message = "Student has been deleted successfully. Student ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};



var loadMediumModal = function (url) {
    $("#MediumModalDiv").load(url, function () {
        $("#MediumModal").modal("show");
        $("#Name").focus();
    });
};
