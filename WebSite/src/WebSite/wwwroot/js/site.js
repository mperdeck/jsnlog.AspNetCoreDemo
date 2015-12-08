
function onClick() {
    var message = document.getElementById("log-message").value;

    // Log message to the server. You can also log objects.
    JL().info(message);
}

