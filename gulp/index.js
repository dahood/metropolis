let gulp = require('gulp');
const compile = require('./compile');
const assemblyInfo = require('./assemblyInfo');
const unitTest = require('./unitTest');
const clean = require('./clean');
const copyToBuild = require('./copyToBuild');
const packageClean = require('./packageClean');
const copyPackageCollectionBinaries = require('./copyPackageCollectionBinaries');
const package = require('./package');
const dist = require('./dist');

//gulp.task('clean', clean);

gulp.task('compile', gulp.series(assemblyInfo, compile));
gulp.task('default', gulp.series(clean, assemblyInfo, compile, copyToBuild, unitTest));
gulp.task('test', gulp.series(copyPackageCollectionBinaries, package));
gulp.task('publish', gulp.series(packageClean, copyPackageCollectionBinaries, package, dist));