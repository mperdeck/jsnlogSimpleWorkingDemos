


// Create custom onerror handler, which overrides the onerror handler set by JSNLog.
window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
	// Send object with all data to server side log, using severity fatal, 
	// from logger "onerrorLogger"
	JL("customlogger").fatalException({
		"msg": "Custom message", 
		"errorMsg": errorMsg, "url": url, 
		"line number": lineNumber, "column": column
	}, errorObj);
		
	// Tell browser to run its own error handler as well   
	return false;
}


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


