let gulp = require('gulp');
const msbuild = require('gulp-msbuild');


function compile () {
    return gulp.src('./Metropolis.sln')
        .pipe(msbuild({
            toolsVersion : 'auto',
            //stdout : true,
            stderr : true,
            nologo : true,
            errorOnFail: true
        }));
}

module.exports = compile;