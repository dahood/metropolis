# What is Metropolis?

Metropolis is a code visualization aid to help in review and analysis. It creates 3D representations of modules of code and builds a cityscape  to help reviewers pin point areas of interest. It provides a 3D treemap which uses static code metrics, source control data, and other user data to pin point hotspots in the codebase that warrant more investigation (e.g. cyclomatic complexity, lines of code, number of touches in source control, etc).

## How to Install

Grab the latest [MSI installer](https://github.com/dahood/metropolis/raw/master/publish/Metropolis.msi). Currently only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/).


# User Guide

1. Collect Metrics
* Java Metrics - 

* checkstyle

1. First make sure to download and install checkstyle-all-jar.jar into your CLASSPATH
2. Run Checkstyle command below:

```
#!sh

java com.puppycrawl.tools.checkstyle.Main -c metropolis_checkstyle_metrics.xml -f xml -o metropolis_import.xml src/folder
```
3. Import metropolis_import.xml into Metropolis

* Eclipse + metrics2 plugin (http://metrics2.sourceforge.net/) - ideally wrap metrics2 JAR into a command line utility or directly into metropolis

* IntelliJ?

* C# Metrics - Visual Studio Metrics

2. Navigate City, layouts... etc
3. Enrich dataset (need logs to find duplicates)






# Developer's Guide

For the installer to work you need the Visual Studio Extension for 2015 installers - https://visualstudiogallery.msdn.microsoft.com/f1cc3f3e-c300-40a7-8797-c509fb8933b9


## Components

There are 4 components to Metropolis:

1. Parsers - for data aquisition and parsing of information (enrich information with Analyzers)
1. Analyzers - for calculating static properties (e.g. Toxicity, Code Ownership Distribution)
1. Domain - representing the abstraction of a Codebase (e.g. Class, Namespace, ClassVersionInfo, CodeGraph)
1. 3DModels - for projecting the properties of the Codebases

### Parsers

Impelement IClassParser. Each parser impelments IClassParser so that it can parse and aquire information. This information can then be passed onto one or more Analyzers to enrich the data.

### Analyzers

Implements ICodebaseAnalyzer. Perform more complex parsing logic or calculations (e.g. calculating toxicity involves taking the natural log of all the properties).

### Domain

Abstract aspects of soure code from a static analysis, source control, codebase test results (e.g. code coverage), and reusable software patterns (e.g. Repository pattern)

### 3DModels

This is where all the Model3D libraries are used. All Matrix Transformations and Layouts (e.g. SquaredLayout, MagnitoLayout).

## License

Metropolis is licensed under BSD (see LICENSE). 
Metropolis depends on:

* CSVHelper (Dual licensing under MS-PL and Apache 2.0) - http://joshclose.github.io/CsvHelper/
* Newtonsoft.JSON (The MIT License). - https://github.com/JamesNK/Newtonsoft.Json
* ESLint (JQuery Foundation) - https://github.com/eslint/eslint
* Checkstyle (GNU LGPL 2.1) - https://github.com/checkstyle/checkstyle
