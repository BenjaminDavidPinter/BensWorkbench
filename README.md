# Ben's Workbench

The goal of this project is to provide useful Extensions and Functions for everyday C# development.

In enterprise development, there are a subset of tasks which sit just above the standard library which would benefit from being encapsulated in idiomatic, simple functions.

Navigating the library:

- BensWorkbench.Extensions *[Namespace]*
  - FileExtensions *[Static Class]*
    - ReadtoBase64() -> Read File to Base64 String
    - WriteBase64AsFile() -> Write Base64 String to File
- BensWorkbench.Models *[Namespace]*
  - Result<T,E> *[Class]*
