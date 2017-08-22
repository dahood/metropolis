let gulp = require('gulp');
const del = require('del');


const distDir = __dirname + '/../dist/';

function packageClean() {
    return new Promise((resolve, error) => {
      del([distDir], {force: true}).then(() => {
        resolve();
      });
    });
};


module.exports = packageClean;