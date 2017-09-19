let gulp = require('gulp');
const {restore, build, test, pack, publish} = require('gulp-dotnet-cli');


function corePublish() {
    return gulp.src('./core/Metropolis.Cli/Metropolis.Cli.csproj', {read: false})
    .pipe(publish({configuration: 'Debug', version: '2.0.0'}));
}
module.exports = corePublish;