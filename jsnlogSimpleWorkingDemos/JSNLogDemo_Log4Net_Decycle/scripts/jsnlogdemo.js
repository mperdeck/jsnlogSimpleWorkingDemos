


	// -----------------------
	// decycle example
	//
	// Shows how to use the serialize option on JL to override the method used to serialize (or "stringify")
	// an object into a string.
	// And specifically how to use this to log objects with cyclic structures.

	// This method turns the given object into a string.
	// Make sure that the cycle.js file is loaded before this runs.

	var decyclingSerializer = function(obj) {
		var decycledObject = JSON.decycle(obj);
		var result = JSON.stringify(decycledObject);
		return result;
	}
	
	// Get JSNLog to use the new serialize method
    JL.setOptions({
        "serialize": decyclingSerializer
    });

    // ----- end of decycle example -------


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


