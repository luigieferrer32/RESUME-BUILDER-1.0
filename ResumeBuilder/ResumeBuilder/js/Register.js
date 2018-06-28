$("#btnRegister").on('click', function () {
    var errorCtr = 0;

    var username = $('#Username').val();
    var password = $('#Password').val();
    var emailaddress = $('#EmailAddress').val();

    if (username == '' || username == null || username === undefined) {
        $('#username-error-msg').html('Username is a required field.');
        $('#username-error-msg').show();
        errorCtr++;
    }

    if (password == '' || password == null || password === undefined) {
        $('#password-error-msg').html('Password is a required field.');
        $('#password-error-msg').show();
        errorCtr++;
    }

    if (emailaddress == '' || emailaddress == null || emailaddress === undefined) {
        $('#password-error-msg').html('Password is a required field.');
        $('#password-error-msg').show();
        errorCtr++;
    }

    if (errorCtr == 0) {
        $.ajax({
            type: "POST",
            url: userRegisterUrl,
            data: {
                UserName: $("#Username").val(),
                Password: $("#Password").val(),
                EmailAddress: $("#EmailAddress").val()
            },
            dataType: "json",
            success: function (data) {
                AuthenticationMessage(data);
            }
        });
    }
})

function AuthenticationMessage(data) {
    if (data.access == true) {
        alert("User Access is Granted");
    }
    else {
        alert("User Access is Denied");

    }
}
