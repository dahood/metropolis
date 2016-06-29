// Gulp Modules
var del = require('del');
var argv = require('yargs').argv;
var gulp = require('gulp');
var exec = require('sync-exec');
var nunit = require('gulp-nunit-runner');

// Gulp Variables
var buildPath = '%CD%\\build';
var maxThreads = 8;
var msBuildConfiguration = 'Release'
var version = '0.0.1'; //using package.json

// Gulp Default

gulp.task('default', ['test']);

// Gulp Tasks

gulp.task('clean', function () {
  return del(['dist','build']);
});

gulp.task('compile', function (cb) {
  var package = require('./package.json');
  version = package.version;
  console.log('MSBuild Release Configuration: ' + msBuildConfiguration);
  console.log('Version Number: ' + version);
  var cmd = '"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=' + 
    buildPath + ';Configuration=' + msBuildConfiguration + ';VersionNumber=0.' 
    + version + ' /maxcpucount:' + maxThreads;
  console.log(exec(cmd).stdout);
  cb();
});

gulp.task('test', ['compile'], function () {
    return gulp.src(['build\\*.Test.dll'], {read: false})
        .pipe(nunit({
        	noresult: true, //TODO: Fix this
            result: 'build\\Foo.xml',
            err: 'build\\NUnitErrors.txt',
            teamcity: false,
            nologo: true,
            workers: maxThreads,
            executable: 'packages\\NUnit.ConsoleRunner.3.2.1\\tools\\nunit3-console.exe'
        }));
});

// Usage: gulp dist -m "patch notes"

gulp.task('dist', ['package', 'version'],  function() {
    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
    if (argv.m)
    {
        console.log(exec('git commit -a -m \'' + argv.m + '\'').stdout);
        console.log("Pushing to GitHub...");
        console.log(exec('git push origin master').stdout);
        console.log("Publishing to npm...");
        console.log(exec('npm publish').stdout);
    }
});

gulp.task('version', function() {
    console.log('in version...');
    if (argv.m)
    {
        console.log(exec('npm version patch').stdout);
    }
});

// Dist depends on both metropolis binaries, Collection Settings (e.g. checkstyle xml config), 
// Collection Binaries (e.g. checkstyle .jar) for eslint, checkstyle, fxcop, etc that parsers 
// use to automate the collection of metrics 

gulp.task('package', ['package-collection-cpd', 'package-collection-checkstyle', 
    'package-collection-settings', 'compile'], function() {
	return gulp.src(['build\\*.dll', 'build\\*.exe', 'build\\*.config',
        // exclude all these test files
        '!build\\Metropolis.Test.dll',
        '!build\\FluentAssertions.Core.dll', 
        '!build\\FluentAssertions.dll', 
        '!build\\nunit.framework.dll', 
        '!build\\Moq.dll'])
        .pipe(gulp.dest('dist'));
});

gulp.task('package-collection-settings', ['compile'], function() {
    return gulp.src(['build\\Collection\\Settings\\**'])
        .pipe(gulp.dest('dist\\Collection\\Settings'));
});

gulp.task('package-collection-checkstyle', ['compile'], function() {
     return gulp.src(['build\\Collection\\Binaries\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries'));
});

gulp.task('package-collection-cpd', ['compile'], function() {
     return gulp.src(['build\\Collection\\Binaries\\cpd\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries\\cpd'));
});