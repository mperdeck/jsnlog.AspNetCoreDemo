
function GenerateException() {
    // Try to access variable that doesn't exist
    var x = xyz;
}

function LogMessage() {
    var message = document.getElementById('message').value;

    if (document.getElementById('trace').checked) { console.log("trace " + message); JL().trace(message); }
    if (document.getElementById('debug').checked) { console.log("debug " + message); JL().debug(message); }
    if (document.getElementById('info').checked) { console.log("info " + message); JL().info(message); }
    if (document.getElementById('warn').checked) { console.log("warn " + message); JL().warn(message); }
    if (document.getElementById('error').checked) { console.log("error " + message); JL().error(message); }
    if (document.getElementById('fatal').checked) { console.log("fatal " + message); JL().fatal(message); }
}

