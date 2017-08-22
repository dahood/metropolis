var gulp = require('gulp');
var nunit = require('gulp-nunit-runner');

gulp.task('test', ['compile'], function () {
    gulp.src(['build\\*.Test.dll'], {read: false})
        .pipe(nunit({
        	noresult: true, //TODO: Fix this
            result: 'build\\Foo.xml',
            err: 'build\\NUnitErrors.txt',
            teamcity: false,
            nologo: true,
            executable: nunitConsole
        }));
});