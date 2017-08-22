let gulp = require('gulp');


function copyPackageCollectionBinaries () {
    return gulp.src('build/Collection/**/*')
        .pipe(gulp.dest('dist/Collection'));
}

module.exports = copyPackageCollectionBinaries;