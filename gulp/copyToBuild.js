let gulp = require('gulp');
const debug = require('gulp-debug');

const buildDir = 'build/';

function copyTBuild() {
    return gulp.src('test/Metropolis.Test/bin/Debug/**/*')
        .pipe(debug({title: 'inside codey'}))
        .pipe(gulp.dest(buildDir));
};

module.exports = copyTBuild;