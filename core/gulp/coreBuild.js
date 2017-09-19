let gulp = require('gulp');
const {restore, build, test, pack, publish} = require('gulp-dotnet-cli');


function _restore () {
    return gulp.src('./core/**/*.csproj', {read: false})
    .pipe(restore());
};

function _build() {
    return gulp.src('./Metropolis.Core.sln', {read: false})
    .pipe(build({configuration: 'Debug', version: '2.0.0'}));
}

function coreBuild() {
    return _restore().on('end', _build);
}
module.exports = coreBuild;