var gulp = require('gulp');
var shell = require('gulp-shell');
var nunit = require('gulp-nunit-runner');

gulp.task('compile', shell.task([
  'echo compiling',
  '\"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\" Metropolis.sln /p:OutDir=%CD%\\build /maxcpucount:8'
]))

gulp.task('default', function() {
  // place code for your default task here
});