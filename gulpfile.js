// Gulp Modules
var gulp = require('gulp');
var shell = require('gulp-shell');
var nunit = require('gulp-nunit-runner');

// Gulp Variables
var buildPath = '%CD%\\build';
var maxThreads = 8;

// Gulp Tasks

gulp.task('compile', shell.task([
  '\"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=' + buildPath + ' /maxcpucount:' + maxThreads
]))

gulp.task('test', function () {
    return gulp.src(['build\\*.Test.dll'], {read: false})
        .pipe(nunit({
            executable: 'packages\\NUnit.ConsoleRunner.3.2.1\\tools\\nunit3-console.exe',
            result: 'build\\TestResults.xml',
            workers: maxThreads
        }));
});

gulp.task('default', ['compile', 'test']);