## What is Metropolis?

Metropolis is a code review and analysis tool that will help you highlight areas of interest or concern. Read the **How it Works** section for more details.

## How to Install

1. Install NodeJS on [Windows](https://nodejs.org/dist/v6.1.0/node-v6.1.0-x64.msi)
2. Run `Set-ExecutionPolicy remotesigned` in your powershell console
1. `npm install -g metropolis` 

Currently, Metropolis only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/). 

We plan on supporting the command line interface using Mono at some point in the near future.

## How it Works

Metropolis maps every code unit (for C# or Java this is a class, for Javascript this is a file) into a city building. The default is to create taller building based on lines of code and the color of that building based on how healthy the code is. All the green and short buildings are good. The tall red ones need to be reviewed by the techinical team to ensure that code quality is maintained or improved.

For more information on code health please read Erik Doernenburg's awesome article on [How Toxic is Your Code?](http://erik.doernenburg.com/2008/11/how-toxic-is-your-code/). It explains toxicity scales and how they are calculated for Java.
Toxicity is essentially a [Richter Scale](https://en.wikipedia.org/wiki/Richter_magnitude_scale) of code health. If your code has an average toxicity of higher than 0.500 (e.g. C#, Java, Javascript/ECMA) then it's likely a good idea to refocus on code quality to improve the maintainability and extensibility of the code.

### Example - Hibernate
<p align="center"><img alt="Cityscape of Hibernate" src="https://raw.githubusercontent.com/dahood/metropolis/master/example-metropolis.png"/></p>

Hibernate is an object relational mapping tool used to speed up development of applications. Hibernate has over 330,000 lines of code and an average toxicity of 1.335. You can see some areas are very good, but given the size and complexity of this project there are areas of concern highlighted in red. Using Metropolis's code inspector you can view these classes to review this code.

## Getting Started

[Beginner's Guide](https://github.com/dahood/metropolis/wiki/Beginner's-Guide) is a good place to start as is the [Code Analysis Primer](https://github.com/dahood/metropolis/wiki/Code-Analysis-Primer).

## License

Metropolis is licensed under BSD (see LICENSE).

Metropolis depends on the following open source software:

* CSVHelper (Dual licensing under MS-PL and Apache 2.0) - http://joshclose.github.io/CsvHelper/
* Newtonsoft.JSON (The MIT). - https://github.com/JamesNK/Newtonsoft.Json
* Galatic Powershell & Event Log (MIT) - https://raw.githubusercontent.com/GalacticAPI/Galactic/master/LICENSE
* ESLint (JQuery Foundation) - https://github.com/eslint/eslint
* Checkstyle (GNU LGPL 2.1) - https://github.com/checkstyle/checkstyle
* D3 (BSD) - https://github.com/mbostock/d3
* radar-chart-d3 (Apache 2.0) - https://github.com/alangrafu/radar-chart-d3
