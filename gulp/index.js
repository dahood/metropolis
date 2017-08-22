let gulp = require('gulp');
const compile = require('./compile');
const assemblyInfo = require('./assemblyInfo');
const unitTest = require('./unitTest');
const clean = require('./clean');
const copyToBuild = require('./copyToBuild');

//gulp.task('clean', clean);

gulp.task('compile', gulp.series(assemblyInfo, compile));
gulp.task('test', gulp.series(unitTest));
gulp.task('default', gulp.series(clean, assemblyInfo, compile, copyToBuild));
