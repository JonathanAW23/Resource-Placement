
$("#btnChange").click(function (event) {
    event.preventDefault();
    var obj_change = new Object();
    var validate = false;
 /*   console.log($("#inputOldPass").val());*/
    obj_change.Email = $("#inputEmail").val();
    obj_change.OldPassword = $("#inputOldPass").val();
    obj_change.NewPassword = $("#inputNewPass").val();
    obj_change.NewPasswordConfirm = $("#inputConfirmPass").val();

    if ($("#inputEmail").val() == "") {
        document.getElementById("inputEmail").className = "form-control is-invalid";
        $("#msgEmail").html("Email can't be empty");
       
    } else {
       
        document.getElementById("inputEmail").className = "form-control is-valid";
        obj_change.Email = $("#inputEmail").val();
    }
    if ($("#inputOldPass").val() == "" && validate==false) {
        document.getElementById("inputOldPass").className = "form-control is-invalid";
        $("#msgOld").html("Old Password can't be empty");
        validate = false;
    } else {
        document.getElementById("inputOldPass").className = "form-control is-valid";
        obj_change.OldPassword = $("#inputOldPass").val();
        
        validate = true;
    }

    if ($("#inputNewPass").val() == "") {
         document.getElementById("inputNewPass").className = "form-control is-invalid";
         $("#msgNew").html("New Password can't be empty");
         validate = false;
    } else {

        document.getElementById("inputNewPass").className = "form-control is-valid";
        obj_change.NewPassword = $("#inputNewPass").val();
        validate = true;
    }

    if ($("#inputConfirmPass").val() == "") {
        document.getElementById("inputConfirmPass").className = "form-control is-invalid";
        $("#msgConfirm").html("Confirm Password can't be empty");
        validate = false;
    } else {

        document.getElementById("inputConfirmPass").className = "form-control is-valid";
        obj_change.NewPasswordConfirm = $("#inputConfirmPass").val();
        validate = true;
    }

    if ((($("#inputNewPass").val()) != ($("#inputConfirmPass").val()))) {
        document.getElementById("inputConfirmPass").className = "form-control is-invalid";
        $("#msgConfirm").html("New Password and Confirm aren't match");
        validate = false;
    } else if ((($("#inputNewPass").val()) == ($("#inputConfirmPass").val())) && (($("#inputNewPass").val()) != "") && ($("#inputConfirmPass").val()) != "") {
        document.getElementById("inputConfirmPass").className = "form-control is-valid";
        obj_change.NewPasswordConfirm = $("#inputConfirmPass").val();
        validate = true;
    } else {
        validate = false;
    }

    //console.log(validate);
    //console.log(obj_change.OldPassword);
    //console.log($("#inputOldPass").val());
    

    if (validate == true) {
        event.preventDefault();
        obj_change.Email = $("#inputEmail").val();
        obj_change.OldPassword = $("#inputOldPass").val();
        obj_change.NewPassword = $("#inputNewPass").val();
        obj_change.NewPasswordConfirm = $("#inputConfirmPass").val();
        console.log(JSON.stringify(obj_change));
        $.ajax({
            url: "/ChangePasswords/changepass",
            method: 'PUT',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded',
            data: obj_change,
            beforeSend: function () {
                Swal.fire({
                    title: "Checking...",
                    text: "Please wait",
                    imageUrl: "https://c.tenor.com/5o2p0tH5LFQAAAAi/hug.gif",
                    imageWidth: 200,
                    imageHeight: 200,
                    imageAlt: 'Custom image',
                    showConfirmButton: false,
                    allowOutsideClick: false
                })
            },
            success: function (data) {
                Swal.fire({
                    title: 'Password Baru Anda Berhasil diganti',
                    text: 'Press Any Button to Continue',
                    icon: 'success',
                    confirmButtonText: 'Okay'
                }).then(function () {
                    window.location = "/logins/Logout";
                    /*          document.location = Url.Action("login-page","Logins");*/
                })
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseJSON.errors);
                if (xhr.responseJSON.errors != undefined) {

                    checkValid(xhr.responseJSON.errors.Email, "inputEmail", "#msgEmail");
                    checkValid(xhr.responseJSON.errors.OldPassword, "inputOldPass", "#msgOld");
                    checkValid(xhr.responseJSON.errors.NewPassword, "inputNewPass", "#msgNew");
                    checkValid(xhr.responseJSON.errors.NewPasswordConfirm, "inputConfirm", "#msgConfirm");

                }

            }
        })
    } else {
       
        event.preventDefault();
        console.log(JSON.stringify(obj_change));
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
       event.stopPropagation();
    }

});