let gulp = require('gulp');
const del = require('del');


const distDir = 'dist/';

function packageClean(done) {
      del([distDir], {force: true}).then(() => {
        done();
      });
};


module.exports = packageClean;