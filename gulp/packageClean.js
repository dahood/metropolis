let gulp = require('gulp');
const del = require('del');


const distDir = __dirname + '/../dist/';

function packageClean(done) {
      del([distDir], {force: true}).then(() => {
        done();
      });
};


module.exports = packageClean;