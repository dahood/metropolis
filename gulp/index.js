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
const nuget = require('./nuget');


gulp.task('compile', gulp.series(nuget, assemblyInfo, compile));
gulp.task('default', gulp.series(clean, nuget, assemblyInfo, compile, copyToBuild, unitTest));
gulp.task('dist', gulp.series(version, assemblyInfo, compile, packageClean, copyPackageCollectionBinaries, package, dist));

gulp.task('test', gulp.series(nuget));