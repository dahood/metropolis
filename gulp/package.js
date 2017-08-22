let gulp = require('gulp');

function package () {
    return gulp.src(['build/*.dll', 'build/*.exe', 'build/*.config',
        // exclude all these test files
        '!build/Metropolis.Test.dll',
        '!build/FluentAssertions.Core.dll', 
        '!build/FluentAssertions.dll', 
        '!build/nunit.framework.dll', 
        '!build/Moq.dll'])
        .pipe(gulp.dest('dist'));
};

module.exports = package;