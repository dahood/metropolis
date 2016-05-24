// Gulp Modules
var del = require('del');
var gulp = require('gulp');
var childProcess = require('child_process').exec;
var nunit = require('gulp-nunit-runner');

// Gulp Variables
var buildPath = '%CD%\\build';
var maxThreads = 8;
var msBuildConfiguration = 'Debug'

// Gulp Default

gulp.task('default', ['test', 'compile', 'clean']);

// Gulp Tasks

gulp.task('clean', function () {
  return del(['dist','build']);
});

gulp.task('compile', function (cb) {
  var cmd = '"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=' + 
    buildPath + ';Configuration=' + msBuildConfiguration + ' /maxcpucount:' + maxThreads
  //console.log(cmd);  
  childProcess(cmd, function (err, stdout, stderr) {
    	    	console.log(stdout);
    	    	console.log(stderr);
    	    	cb(err);
  			});
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

// Dist depends on both metropolis binaries, Collection Settings (e.g. checkstyle xml config), 
// Collection Binaries (e.g. checkstyle .jar) for eslint, checkstyle, fxcop, etc that parsers 
// use to automate the collection of metrics 


gulp.task('dist', ['package'],  function(cb) {
    console.log('Please wait while npm trys to install your release candidate...');
    childProcess('npm install . -g', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err)});
    //Next step is npm version patch -m "patch notes" 
    //Last step is npm publish
});

gulp.task('set-release', function(){
    msBuildConfiguration = 'Release';
});

gulp.task('package', ['package-collection-binaries', 'package-collection-settings', 'compile', 'set-release'], function() {
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
    return gulp.src(['build\\Collection\\Settings\\**', 
        'build\\Collection\\Settings\\.eslintrc.json'])
        .pipe(gulp.dest('dist\\Collection\\Settings'));
});

gulp.task('package-collection-binaries', ['compile'], function() {
     return gulp.src(['build\\Collection\\Binaries\\*.jar'])
        .pipe(gulp.dest('dist\\Collection\\Binaries'));
});