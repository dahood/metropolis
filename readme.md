# What is Metropolis?

Metropolis is a code visualization aid to help in review and analysis. It creates 3D representations of modules of code and builds a cityscape  to help reviewers pin point areas of interest. It provides a 3D treemap which uses static code metrics, source control data, and other user data to pin point hotspots in the codebase that warrant more investigation (e.g. cyclomatic complexity, lines of code, number of touches in source control, etc).

## How to Install

Grab the latest [MSI installer](https://github.com/dahood/metropolis/raw/master/publish/Metropolis.msi). Currently only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/).

## How To ... do that

Learn more by visiting the [project wiki](https://github.com/dahood/metropolis/wiki)


## License

Metropolis is licensed under BSD (see LICENSE). 
Metropolis depends on:

* CSVHelper (Dual licensing under MS-PL and Apache 2.0) - http://joshclose.github.io/CsvHelper/
* Newtonsoft.JSON (The MIT License). - https://github.com/JamesNK/Newtonsoft.Json
* ESLint (JQuery Foundation) - https://github.com/eslint/eslint
* Checkstyle (GNU LGPL 2.1) - https://github.com/checkstyle/checkstyle
