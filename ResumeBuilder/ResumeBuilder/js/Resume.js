$("#btnSave").on('click', function () {
   
   
    
    alert($("#txtUserID").val());
        $.ajax({
            type: "POST",
            url: UpdateInfoUrl,
            data: {
                UserId: $("#txtUserID").val(),
                Firstname: $("#txtFirstname").val(),
                Lastname: $("#txtLastname").val(),
                StreetAddress: $("#txtAddress").val(),
                City: $("#txtCity").val(),
                StateProvince: $("#txtProvince").val(),
                ZipCode: $("#txtZipcode").val(),
                Number: $("#txtContactNo").val()
            },
            dataType: "json",


        });
      
     
            
    })


function AuthenticationMessage(data) {
    if (data.access == true) {
        alert("User Access is Granted");
    }
    else {
        alert("User Access is Denied");

    }
}
