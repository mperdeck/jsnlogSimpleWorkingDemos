


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

        xhr.setRequestHeader('X-Forwarded-For', '99.88.77.66');
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


