// Gulp Modules
var del = require('del');
var gulp = require('gulp');
var shell = require('gulp-shell');
var nunit = require('gulp-nunit-runner');

// Gulp Variables
var buildPath = '%CD%\\build';
var maxThreads = 8;

// Gulp Tasks

gulp.task('clean', function () {
  return del(['build/**/*','dist/**/*']);
});

gulp.task('compile', shell.task([
  '\"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=' + buildPath + ' /maxcpucount:' + maxThreads
]))

gulp.task('test', function () {
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

gulp.task('dist', ['clean', 'compile'], function(){
	return gulp.src(['build\\*.dll', 'build\\*.exe', 'build\\*.config', 
			'!build\\Metropolis.Test.dll','!build\\FluentAssertions.Core.dll', '!build\\FluentAssertions.dll', '!build\\nunit.framework.dll'])
		.pipe(gulp.dest('dist'));
});

gulp.task('default', ['compile', 'test']);