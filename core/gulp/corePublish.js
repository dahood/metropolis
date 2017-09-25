let gulp = require('gulp');
const {restore, build, test, pack, publish} = require('gulp-dotnet-cli');


function corePublish() {
    return gulp.src('./Metropolis.Cli/Metropolis.Cli.csproj', {read: false})
    .pipe(publish({configuration: 'Release', version: '2.0.0', output: '../publish'}));
}
module.exports = corePublish;