// Gulp Modules
var del = require('del');
var argv = require('yargs').argv;
var gulp = require('gulp');
var exec = require('sync-exec');
var nunit = require('gulp-nunit-runner');
var assemblyInfo = require('gulp-dotnet-assembly-info');

// Gulp Variables
var buildPath = '%CD%\\build';
var msBuildConfiguration = 'Release';
var version = '0.0.1'; //using package.json
var nunitConsole = 'packages\\NUnit.ConsoleRunner.3.5.0\\tools\\nunit3-console.exe';

// Gulp Default

gulp.task('default', ['test']);

// Gulp Tasks



gulp.task('test', ['compile'], function () {
    gulp.src(['build\\*.Test.dll'], {read: false})
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
gulp.task('dist',['version', 'compile', 'package'], function() {

    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
    
    if (argv.m)
    {
        console.log("Publishing to npm...");
        console.log(exec('npm publish').stdout);
        console.log("Pushing to GitHub...");
        console.log(exec('git commit -a -m \"' + argv.m + '\"').stdout);
        console.log(exec('git push origin master').stdout);
    }
});

// Usage: gulp version -m "patch notes"
gulp.task('version', function() {
    if (argv.m)
    {
        console.log('Versioning...');
        console.log(exec('npm version patch').stdout);
    }
});

// Dist depends on both metropolis binaries, Collection Settings (e.g. checkstyle xml config), 
// Collection Binaries (e.g. checkstyle .jar) for eslint, checkstyle, fxcop, etc that parsers 
// use to automate the collection of metrics 

gulp.task('package', ['package-clean', 'package-collection-cpd', 'package-collection-checkstyle', 
    'package-collection-settings'], function() {   
	gulp.src(['build\\*.dll', 'build\\*.exe', 'build\\*.config',
        // exclude all these test files
        '!build\\Metropolis.Test.dll',
        '!build\\FluentAssertions.Core.dll', 
        '!build\\FluentAssertions.dll', 
        '!build\\nunit.framework.dll', 
        '!build\\Moq.dll'])
        .pipe(gulp.dest('dist'));
});

gulp.task('package-clean'), function(){
  del(['dist']);
}

gulp.task('package-collection-settings', function() {
    gulp.src(['build\\Collection\\Settings\\**'])
        .pipe(gulp.dest('dist\\Collection\\Settings'));
});

gulp.task('package-collection-checkstyle', function() {
    gulp.src(['build\\Collection\\Binaries\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries'));
});

gulp.task('package-collection-cpd', function() {
    gulp.src(['build\\Collection\\Binaries\\cpd\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries\\cpd'));
});