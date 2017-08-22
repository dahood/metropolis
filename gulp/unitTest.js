let gulp = require('gulp');
const nunit = require('gulp-nunit-runner');
let globby = require('globby');

function getNUnitConsole(){
    return globby.sync('packages\\NUnit.ConsoleRunner.*\\tools\\nunit3-console.exe')[0];
}


function unitTest () {
    return new Promise((resolve, error) => {
        gulp.src(['build/**/**.Test.dll'], {read: false})
        .pipe(nunit({
            teamcity: false,
            executable: getNUnitConsole(),
            options: {
                err: 'build\\NUnitErrors.txt',
                result: 'build\\Foo.xml'
            }}))
        .on('error', (msg) => {error(msg);})
        .on('end', () => { resolve(); })
    });
};

module.exports = unitTest;