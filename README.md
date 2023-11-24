# Ben's Workbench

The goal of this project is to provide useful Extensions and Functions for everyday C# development.

In enterprise development, there are a subset of tasks which sit just above the standard library which would benefit from being encapsulated in idiomatic, simple functions.

Some preliminary ideas;

- Methods for performing slightly more complicated tasks with files
	- string ReadAllTextAsBase64(string filePath);
	- bool WriteBase64AsFile(string base64, string path, string extension);
- Result<T> implementation which mimics that of Rust;


I might build the Result<T> type as the main object which my functions return


## Notes from the developer

- Accidentally discovered that this is a very null-safe way to deal with errors. Normally, when an exception is thrown, the container you expected to put the result in is left behind. As is the rest of the stack at that moment in time. Under the result model, you can handle your errors, but you still must return a value from the switch statement which you're using to match the Result. This is a happy accident because of the way switch statements work in C#; you cannot return more than one datatype (even if you capture the result in a var). So even if you decide to handle your error, you have to specify a default, non nullable, acceptable value for the result of the operation.