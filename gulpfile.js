// Gulp Modules
var del = require('del');
var argv = require('yargs').argv;
var gulp = require('gulp');
var exec = require('sync-exec');
var nunit = require('gulp-nunit-runner');

// Gulp Variables
var buildPath = '%CD%\\build';
var msBuildConfiguration = 'Release'
var version = '0.0.1'; //using package.json
var nunitConsole = 'packages\\NUnit.ConsoleRunner.3.4.1\\tools\\nunit3-console.exe'

// Gulp Default

gulp.task('default', ['test']);

// Gulp Tasks

gulp.task('clean', function () {
  return del(['dist','build']);
});

gulp.task('compile', function () {
  var package = require('./package.json');
  version = package.version;
  console.log('MSBuild Release Configuration: ' + msBuildConfiguration);
  console.log('Version Number: ' + version);
  var cmd = '"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=' + 
    buildPath + ';Configuration=' + msBuildConfiguration + ';VersionNumber=0.' 
    + version + ' /maxcpucount';
  console.log(exec(cmd).stdout);
});

gulp.task('test', ['compile'], function () {
    return gulp.src(['build\\*.Test.dll'], {read: false})
        .pipe(nunit({
        	noresult: true, //TODO: Fix this
            result: 'build\\Foo.xml',
            err: 'build\\NUnitErrors.txt',
            teamcity: false,
            nologo: true,
            executable: nunitConsole
        }));
});

// Usage: gulp dist -m "patch notes"
// Usage: gulp dist (test mode)
gulp.task('dist', function() {
    if (argsv.m)
      console.log(exec('gulp version').stdout);
    else
      console.log(exec('gulp version "' + argv.m + '"').stdout);

    console.log(exec('gulp package').stdout);

    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
    
    if (argv.m)
    {
        console.log("Publishing to npm...");
        console.log(exec('npm publish').stdout);
    }
});

// Usage: gulp version -m "patch notes"
gulp.task('version', function() {
    console.log('Versioning...');
    if (argv.m)
    {
        console.log('Versioning...');
        console.log(exec('npm version patch').stdout);
        require('child_process').syncExec('sleep 1');
        console.log(exec('gulp compile').stdout);
        require('child_process').syncExec('sleep 2');
        console.log(exec('git commit -a -m \'' + argv.m + '\'').stdout);
        console.log("Pushing to GitHub...");
        console.log(exec('git push origin master').stdout);
    }
    else
    {
        console.log('Compiling...');
        console.log(exec('gulp compile').stdout);
    }
});

// Dist depends on both metropolis binaries, Collection Settings (e.g. checkstyle xml config), 
// Collection Binaries (e.g. checkstyle .jar) for eslint, checkstyle, fxcop, etc that parsers 
// use to automate the collection of metrics 

gulp.task('package', ['package-collection-cpd', 'package-collection-checkstyle', 
    'package-collection-settings'], function() {
	return gulp.src(['build\\*.dll', 'build\\*.exe', 'build\\*.config',
        // exclude all these test files
        '!build\\Metropolis.Test.dll',
        '!build\\FluentAssertions.Core.dll', 
        '!build\\FluentAssertions.dll', 
        '!build\\nunit.framework.dll', 
        '!build\\Moq.dll'])
        .pipe(gulp.dest('dist'));
});

gulp.task('package-collection-settings', function() {
    return gulp.src(['build\\Collection\\Settings\\**'])
        .pipe(gulp.dest('dist\\Collection\\Settings'));
});

gulp.task('package-collection-checkstyle', function() {
     return gulp.src(['build\\Collection\\Binaries\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries'));
});

gulp.task('package-collection-cpd', function() {
     return gulp.src(['build\\Collection\\Binaries\\cpd\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries\\cpd'));
});