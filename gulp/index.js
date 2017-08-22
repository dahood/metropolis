let gulp = require('gulp');
const compile = require('./compile');
const assemblyInfo = require('./assemblyInfo');
const unitTest = require('./unitTest');
const clean = require('./clean');
const copyToBuild = require('./copyToBuild');
const packageClean = require('./packageClean');
const copyPackageCollectionBinaries = require('./copyPackageCollectionBinaries');
const package = require('./package');
const version = require('./version');
const dist = require('./dist');


gulp.task('compile', gulp.series(assemblyInfo, compile));
gulp.task('default', gulp.series(clean, assemblyInfo, compile, copyToBuild, unitTest));
gulp.task('publish', gulp.series(version, compile, packageClean, copyPackageCollectionBinaries, package, dist));

//gulp.task('test', gulp.series(dist));