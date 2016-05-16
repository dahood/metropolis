# What is Metropolis?

Metropolis is a code visualization aid to help in review and analysis. It creates 3D representations of modules of code and builds a cityscape  to help reviewers pin point areas of interest. It provides a 3D treemap which uses static code metrics, source control data, and other user data to pin point hotspots in the codebase that warrant more investigation (e.g. cyclomatic complexity, lines of code, number of touches in source control, etc).

## Example

Metropolis maps every code unit (for C# or Java this is a class, for Javascript this is a file) into a city building. The default is to create taller building based on lines of code and the color of that building based on how healthy the code is.

All the green and short buildings are good. The tall red ones need to be looked and reviewed to ensure things get better in those areas.

For more information on code health please read Erik Doernenburg's awesome article on [How Toxic is Your Code?](http://erik.doernenburg.com/2008/11/how-toxic-is-your-code/). It explains toxicity scales and how they are calculated for Java.
Toxicity is essentially a [Richter Scale](https://en.wikipedia.org/wiki/Richter_magnitude_scale) of code health. If your code has an average toxicity of higher than 0.5 in most language (C#, Java, Javascript/ECMA) then you probably need to invest more than usual in quality measures.

![Cityscape of Metroplis](https://raw.githubusercontent.com/dahood/metropolis/master/example-metropolis.png)

## How to Install

1. Install NodeJS on [Windows](https://nodejs.org/dist/v6.1.0/node-v6.1.0-x64.msi)
2. Install npm package
`npm install -g metropolis-core`

Currently, Metropolis only support Windows sorry no [WPF on Mono yet](http://www.mono-project.com/docs/gui/wpf/). 
We plan on supporting the command line interface using Mono.

## User Guide

[User Guide Wiki](https://github.com/dahood/metropolis/wiki/User-Guide) - contains info on how to get started after install

## How to anything else...

Learn more by visiting the [project wiki](https://github.com/dahood/metropolis/wiki)

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
