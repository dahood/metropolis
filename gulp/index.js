let gulp = require('gulp');
const compile = require('./compile');
//const clean = require('./clean');
const assemblyInfo = require('./assemblyInfo');
//const version = require('./version');


//gulp.task('clean', clean);

gulp.task('compile', gulp.series(assemblyInfo, compile));
gulp.task('default', gulp.series(assemblyInfo));
