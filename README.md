# Ben's Workbench

The goal of this project is to provide useful Extensions and Functions for everyday C# development.

In enterprise development, there are a subset of tasks which sit just above the standard library which would benefit from being encapsulated in idiomatic, simple functions.

Some preliminary ideas;

- Methods for performing slightly more complicated tasks with files
	- string ReadAllTextAsBase64(string filePath);
	- bool WriteBase64AsFile(string base64, string path, string extension);
- Result<T> implementation which mimics that of Rust;


I might build the Result<T> type as the main object which my functions return
