## What is Metropolis?

Metropolis is a code review and analysis tool that will help you highlight areas of interest or concern. To learn more please visit our [Beginner's Guide](https://dahood.io/metropolis-user-guide/).

<a href="https://scan.coverity.com/projects/dahood-metropolis">
  <img alt="Coverity Scan Build Status"
       src="https://scan.coverity.com/projects/9653/badge.svg"/>
</a>

## How to Install

### Windows Instructions
1. Install [Java](https://java.com/en/download/)
1. Install NodeJS on [Windows](https://nodejs.org/dist/v6.1.0/node-v6.1.0-x64.msi)
1. `npm install -g metropolis` 

Currently, Metropolis only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/). 
We also plan on supporting the command line interface using Mono at some point in the near future which will solve part of this problem.

### Mac Instructions
1. Run Windows by installing [Parallels](http://www.parallels.com/ca/products/desktop/buy/?pd&new)
1. Get a copy an [Windows](http://www.microsoftstore.com/store/msca/en_CA/pdp/productID.320386900)
1. Follow Windows instructions above

## License

Metropolis is licensed under BSD (see LICENSE).

Metropolis depends on the following open source software:

* CSVHelper (Dual licensing under MS-PL and Apache 2.0) - http://joshclose.github.io/CsvHelper/
* Newtonsoft.JSON (The MIT). - https://github.com/JamesNK/Newtonsoft.Json
* NLog (BSD) - https://github.com/NLog/NLog
* Avalon Edit (MIT) - https://github.com/icsharpcode/AvalonEdit
* Dynamic Content Control (Apache 2.0) - https://github.com/Sturnus/DynamicContentControl 
* ESLint (JQuery Foundation) - https://github.com/eslint/eslint
* Checkstyle (GNU LGPL 2.1) - https://github.com/checkstyle/checkstyle
* PMP's CPD aka copy paste detector (PMP Style BSD) - https://github.com/pmd/pmd 
* D3 (BSD) - https://github.com/mbostock/d3
* radar-chart-d3 (Apache 2.0) - https://github.com/alangrafu/radar-chart-d3
