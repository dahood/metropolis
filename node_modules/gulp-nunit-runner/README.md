# gulp-nunit-runner

[![NPM version](http://img.shields.io/npm/v/gulp-nunit-runner.svg?style=flat)](http://npmjs.org/gulp-nunit-runner)
[![NPM downloads](http://img.shields.io/npm/dm/gulp-nunit-runner.svg?style=flat)](http://npmjs.org/gulp-nunit-runner)
[![Build Status](http://img.shields.io/travis/keithmorris/gulp-nunit-runner/master.svg?style=flat)](https://travis-ci.org/keithmorris/gulp-nunit-runner)
[![Dependencies Status](http://img.shields.io/david/keithmorris/gulp-nunit-runner.svg?style=flat)](https://david-dm.org/keithmorris/gulp-nunit-runner)
[![DevDependencies Status](http://img.shields.io/david/dev/keithmorris/gulp-nunit-runner.svg?style=flat)](https://david-dm.org/keithmorris/gulp-nunit-runner#info=devDependencies)

A [Gulp.js](http://gulpjs.com/) plugin to facilitate running [NUnit](http://www.nunit.org/) tests on .NET assemblies. Much of this work was inspired by the [gulp-nunit](https://github.com/stormid/gulp-nunit) plugin.

## Installation

From the root of your project (where your `gulpfile.js` is), issue the following command:

```bat
npm install --save-dev gulp-nunit-runner
```

## Usage

The plugin uses standard `gulp.src` globs to retrieve a list of assemblies that should be tested with Nunit. By default the plugin looks for the NUnit console runner in your `PATH`. You can optionally specify the NUnit `bin` folder or the full path of the runner as demonstrated below. You should add `{read: false}` to your `gulp.src` so that it doesn't actually read the files and only grabs the file names.

```javascript
var gulp = require('gulp'),
    nunit = require('gulp-nunit-runner');

gulp.task('unit-test', function () {
	return gulp.src(['**/*.Test.dll'], {read: false})
		.pipe(nunit({
			executable: 'C:/nunit/bin/nunit-console.exe',
		}));
});

```
This would result in the following command being executed (assuming you had Database and Services Test assemblies.)

```bat
C:/nunit/bin/nunit-console.exe "C:\full\path\to\Database.Test.dll" "C:\full\path\to\Services.Test.dll"
```

Note: If you use Windows paths with `\`'s, you need to escape them with another `\`. (e.g. `C:\\nunit\\bin\\nunit-console.exe`). However, you may also use forward slashes `/` instead which don't have to be escaped.

You may also add options that will be used as NUnit command line switches. Any property that is a boolean `true` will simply be added to the command line, String values will be added to the switch parameter separated by a colon and arrays will be a comma seperated list of values.

For more information on available switches, see the NUnit documentation:

[http://www.nunit.org/index.php?p=consoleCommandLine&r=2.6.3](http://www.nunit.org/index.php?p=consoleCommandLine&r=2.6.3)

```javascript
var gulp = require('gulp'),
    nunit = require('gulp-nunit-runner');

gulp.task('unit-test', function () {
	return gulp.src(['**/*.Test.dll'], {read: false})
		.pipe(nunit({
			executable: 'C:/nunit/bin/nunit-console.exe',
			options: {
				nologo: true,
				config: 'Release',
				transform: 'myTransform.xslt'
			}
		}));
});
```
This would result in the following command:

```bat
C:/nunit/bin/nunit-console.exe /nologo /config:"Release" /transform:"myTransform.xslt" "C:\full\path\to\Database.Test.dll" "C:\full\path\to\Services.Test.dll"
```

## Options

Below are all available options. With the release of NUnit 3.x, some options have been removed and some added. Version specific options have been labeled with the version they apply to. For more information on deprecated options see [here](https://github.com/nunit/nunit/wiki/Breaking-Changes#console-command-line-options). For more information on new options see [here](https://github.com/nunit/nunit/wiki/Console-Command-Line).

```js
nunit({

    // The NUnit bin folder or the full path of the console runner.
    // If not specified the NUnit bin folder must be in the `PATH`.
    executable: 'c:/Program Files/NUnit/bin',

    // [2.x] If the full path of the console runner is not specified this determines 
    // what version of the console runner is used. Defaults to anycpu.
    // NOTE: This has been superseded by the 'x86' option below in 3.x.
    // http://www.nunit.org/index.php?p=nunit-console&r=2.6.3
    platform: 'anycpu|x86',

    // [2.x] Output TeamCity service messages.
    // NOTE: This has been superseded by the 'teamcity' option below in 3.x.
    // https://confluence.jetbrains.com/display/TCD8/Build+Script+Interaction+with+TeamCity
    teamcity: true|false,

    // The options below map directly to the NUnit console runner. See here
    // for more info: http://www.nunit.org/index.php?p=consoleCommandLine&r=2.6.3
    options: {

        // [3.x] Name of the test case(s), fixture(s) or namespace(s) to run.
        test: ['TestSuite.Unit', 'TestSuite.Integration'],

        // [3.x] Name of a file containing a list of the tests to run, one per line.
        testist: 'TestsToRun.txt',

        // [2.x] Name of the test case(s), fixture(s) or namespace(s) to run.
        // NOTE: This has been superseded by the 'test' option above in 3.x.
        run: ['TestSuite.Unit', 'TestSuite.Integration'],

        // [2.x] Name of a file containing a list of the tests to run, one per line.
        // NOTE: This has been superseded by the 'testlist' option above in 3.x.
        runlist: 'TestsToRun.txt',

        // List of categories to include.
        include: ['BaseLine', 'Unit'],

        // List of categories to exclude.
        exclude: ['Database', 'Network'],

        // Project configuration (e.g.: Debug) to load.
        config: 'Debug',

        // Process model for tests.
        process: 'Single|Separate|Multiple',

        // AppDomain Usage for tests.
        domain: 'None|Single|Multiple',

        // Framework version to be used for tests.
        framework: 'net-1.1',

        // [3.x] Run tests in a 32-bit process on 64-bit systems.
        x86: true|false,

        // [3.x] Dispose each test runner after it has finished running its tests.
        "dispose-runners": true|false,

        // Timeout for each test case in milliseconds.
        timeout: 1000,

        // [3.x] Random seed used to generate test cases.
        seed: 5150,

        // [3.x] Number of worker threads to be used in running tests.
        workers: 5,

        // Stop after the first test failure or error.
        stoponerror: true|false,

        // Wait for input before closing console window.
        wait: true|false,

        // [3.x] Pause before run to allow debugging.
        pause: true|false,

        // Work directory for output files.
        work: 'BuildArtifacts',

        // File to receive test output.
        output: 'TestOutput.txt',

        // File to receive test error output.
        err: 'TestErrors.txt',

        // Name of XML result file (Default: TestResult.xml)
        result: 'TestResult.xml',

        // [3.x] Save test info rather than running tests. Name of output file.
        explore: 'TestInfo.xml',

        // Suppress XML result output.
        noresult: true|false,

        // Label each test in stdOut.
        labels: true|false,

        // Set internal trace level.
        trace: 'Off|Error|Warning|Info|Verbose',

        // [3.x] Tells .NET to copy loaded assemblies to the shadowcopy directory.
        shadowcopy: true|false,

        // [2.x] Disable shadow copy when running in separate domain.
        // NOTE In 3.x, The console runner now disables shadow copy by 
        // default. use new 'shadowcopy' option in 3.x to turn it on.
        noshadow: true|false,

        // [3.x] Turns on use of TeamCity service messages.
        teamcity: true|false,

        // [3.x] Suppress display of program information at start of run.
        noheader: true|false,

        // [3.x] Displays console output without color.
        nocolor: true|false,

        // [3.x] Display additional information as the test runs.
        verbose: true|false,

        // [2.x] Do not display the logo.
        nologo: true|false,

        // [2.x] Do not display progress.
        nodots: true|false,

        // [2.x] Apartment for running tests (Default is MTA).
        apartment: 'MTA|STA',

        // [2.x] Disable use of a separate thread for tests.
        nothread: true|false,

        // [2.x] Base path to be used when loading the assemblies.
        basepath: 'src',

        // [2.x] Additional directories to be probed when loading assemblies.
        privatebinpath: ['lib', 'bin'],

        // [2.x] Erase any leftover cache files and exit.
        cleanup: true|false

    }
});
```

## Release Notes

### 0.5.2

- Add check for output file before creating the TeamCity output. ([Mike O'Brien](https://github.com/mikeobrien))
- Clean up publish package with `.npmignore`

### 0.5.1
- Fix #13 Running from Visual Studio Task Runner Fails

### 0.5.0
- Make the command line switch aware of OS so that it uses the correct character when running under mono on linux/mac.

### 0.4.2 (28 June 2015)
- Add NUnit 3.x options and labled 2.x options to README.md

### 0.4.1 (20 Nov 2014)
- Remove quotes around multi args.

### 0.4.0 (13 Nov 2014)
- Add build script with test, lint and watch.
- Add Travis CI integration.
- Add a TeamCity reporter.
- Add support for multi options e.g. include, exclude and privatebinpath.
- Add the ability to omit the explicit nunit path entirely and rely on it being in the PATH.
- Add the ability to just supply the path to the bin folder but not explicitly specify the filename.
- Add a platform flag that goes along with both exectable options to chose the x86 or anycpu version of nunit; defaults to anycpu.
- Document all the config options in the README.

### 0.3.0 (30 Sept 2014)
- Fixes large amount of writes by NUnit tests causing node to crash
- Switched to using `child_process::spawn`, much simpler command building.

### 0.2.0 (28 Sept 2014)
- Fixes #2 "Simultaneous runs of test tasks cause duplication"
- Major rearchitecture of plugin by @VoiceOfWisdom
- Adds release notes to README.md

### 0.1.2 (4 Sept 2014)
- Fixes #1 "runner fails if executable path has space"

### 0.1.1 (10 Aug 2014)
- Documentation update

### 0.1.0 (10 Aug 2014)
- Initial release