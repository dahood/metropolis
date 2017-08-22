let gulp = require('gulp');
const nugetRestore = require('gulp-nuget-restore');


function nuget() {
    return gulp.src('./Metropolis.sln')
        .pipe(nugetRestore());
};

module.exports = nuget;