let gulp = require('gulp');
const msbuild = require('gulp-msbuild');


function compile () {
    return gulp.src('./Metropolis.sln')
        .pipe(msbuild({
            toolsVersion : 'auto',
            configuration: 'Debug',
            stderr : true,
            nologo : true,
            errorOnFail: true
        }));
}

module.exports = compile;