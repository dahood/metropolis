let gulp = require('gulp');
const {test} = require('gulp-dotnet-cli');


function coretest () {
    return gulp.src('./core/**/*Test*.csproj', {read: false})
    .pipe(test());
};


module.exports = coretest;