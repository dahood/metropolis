let gulp = require('gulp');
const del = require('del');
const mkdirp = require('mkdirp');

const buildDir = __dirname + '/../build/';
const distDir = __dirname + '/../dist/';
const buildDistDir = __dirname + '/../build/dist';

function clean() {
    return new Promise((resolve, error) => {
      del([buildDir, distDir], {force: true}).then(() => {
        mkdirp(buildDistDir, () => mkdirp(distDir, resolve));
      });
    });
};


module.exports = clean;