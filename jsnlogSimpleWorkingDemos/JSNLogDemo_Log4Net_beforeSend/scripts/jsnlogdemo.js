


      // -----------------------
      // beforeSend example
      //
      // Shows a number of things you can do by setting the beforeSend option
      // to a function.

    var beforeSendExample = function (xhr, json) {

        // ------------------------------------------------------------------
        // Modify the message of the log request to the server.

        json.lg[0].m += " - added by function beforeSendExample";

        // ------------------------------------------------------------------
        // Add request header

        xhr.setRequestHeader('JSNLog-Custom-TestHeader', 'test value');

        // ------------------------------------------------------------------
        // Do something if the log message could not be sent to the server

        // Replace send function with one that saves the message in the xhr, for
        // use when response indicates failure.
        xhr.originalSend = xhr.send;
        xhr.send = function (msg) {
            xhr.msg = msg; // Store message in xhr
            xhr.originalSend(msg);
        }

        // Set response handler that checks if response received (readyState == 4)
        // and response status is not 200 (OK). In that case, do something to deal with
        // failure to log the message.
        xhr.onreadystatechange = function () {
            if (xhr.readyState != 4) { return; }
            if (xhr.status == 200) { return; }

            console.log('Cannot log to server. Status: ' + xhr.status + '. Messsage: ' + xhr.msg);
        };

        // Handle timeouts
        xhr.timeout = 10000; // set timeout to 10 seconds
        xhr.ontimeout = function () {
            console.log('Cannot log to server. Timed out after ' + xhr.timeout + '. Messsage: ' + xhr.msg);
        };
    };

	// Create new appender, so you can set its beforeSend option.
    var appender = JL.createAjaxAppender("example appender");
    appender.setOptions({
        "beforeSend": beforeSendExample
    });

	// Get the root logger to use the new appender.
    JL().setOptions({
        "appenders": [appender]
    });

    // ----- end of beforeSend example -------


// Log with every severity
JL("jsLogger").debug("debug client log message");
JL("jsLogger").info("info client log message");
JL("jsLogger").warn({ msg: 'warn client log message - logging object', x: 5, y: 88 });
JL("jsLogger").error(function() { return "error client log message - returned by function"; });
JL("jsLogger").fatal("fatal client log message");

// Log caught exception
try {
	// ReferenceError: xyz is not defined
	xyz;
} catch (e) {
	// Log the exception
	JL().fatalException("Something went wrong!", e);
}

// ReferenceError: xyz2 is not defined. Should be caught by onerror handler.
xyz2;


