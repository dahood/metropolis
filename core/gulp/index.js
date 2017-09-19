let gulp = require('gulp');
const corebuild = require('./coreBuild');
const coretest = require('./coreTest');
const corePublish = require('./corePublish');


gulp.task('corebuild', gulp.series(corebuild));
gulp.task('coretest', gulp.series(corebuild, coretest));
gulp.task('core', gulp.series(corebuild, corePublish));