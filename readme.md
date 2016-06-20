## What is Metropolis?

Metropolis is a code review and analysis tool that will help you highlight areas of interest or concern. To learn more please visit our [Wiki Home](https://github.com/dahood/metropolis/wiki). 

## How to Install

### Windows Instructions
1. Install NodeJS on [Windows](https://nodejs.org/dist/v6.1.0/node-v6.1.0-x64.msi)
1. `npm install -g metropolis` 

Currently, Metropolis only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/). 
We also plan on supporting the command line interface using Mono at some point in the near future which will solve part of this problem.

### Mac Instructions
1. Run Windows by installing [Parallels](http://www.parallels.com/ca/products/desktop/buy/?pd&new)
1. Get a copy an [Windows](http://www.microsoftstore.com/store/msca/en_CA/pdp/productID.320386900)
1. Follow Windows instructions above

### Getting Started

[Beginner's Guide](https://github.com/dahood/metropolis/wiki/Beginner-Guide) is a good place to start as is the [Code Analysis Primer](https://github.com/dahood/metropolis/wiki/Code-Analysis-Primer).


## How it Works

Metropolis maps every code unit (for C# or Java this is a class, for Javascript this is a file) into a city building. The default is to create taller building based on lines of code and the color of that building based on how healthy the code is. All the green and short buildings are good. The tall red ones need to be reviewed by the techinical team to ensure that code quality is maintained or improved.

Toxicity is essentially a [Richter Scale](https://en.wikipedia.org/wiki/Richter_magnitude_scale) of code health. If your code tends to  have a high average toxicity (e.g. 2.5) then it's a good idea to refocus on code quality to improve the maintainability and extensibility of the code. For more on code health read the [Code Analysis Primer](https://github.com/dahood/metropolis/wiki/Code-Analysis-Primer). The [Beginner Guide's](https://github.com/dahood/metropolis/wiki/Beginner-Guide) also has information on how to score your code.

### Example - Hibernate
<p align="center"><img alt="Cityscape of Hibernate" src="https://raw.githubusercontent.com/dahood/metropolis/master/example-metropolis.png"/></p>

Hibernate is an object relational mapping tool used to speed up development of applications. Hibernate has over 338,052 lines of code and an average toxicity of 1.3681. You can see some areas are very good, but given the size and complexity of this project there are areas of concern highlighted in red. Using Metropolis's code inspector you can view these classes to review this code.


## License

Metropolis is licensed under BSD (see LICENSE).

Metropolis depends on the following open source software:

* CSVHelper (Dual licensing under MS-PL and Apache 2.0) - http://joshclose.github.io/CsvHelper/
* Newtonsoft.JSON (The MIT). - https://github.com/JamesNK/Newtonsoft.Json
* Dynamic Content Control (Apache 2.0) - https://github.com/Sturnus/DynamicContentControl 
* ESLint (JQuery Foundation) - https://github.com/eslint/eslint
* Checkstyle (GNU LGPL 2.1) - https://github.com/checkstyle/checkstyle
* PMP's CPD aka copy paste detector (PMP Style BSD) - https://github.com/pmd/pmd 
* D3 (BSD) - https://github.com/mbostock/d3
* radar-chart-d3 (Apache 2.0) - https://github.com/alangrafu/radar-chart-d3
