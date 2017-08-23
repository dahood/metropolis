let gulp = require('gulp');
const del = require('del');
const mkdirp = require('mkdirp');

const buildDir = 'build/';
const distDir = 'dist/';

function clean() {
    return new Promise((resolve, error) => {
      del([buildDir, distDir], {force: true}).then(() => {
        mkdirp(buildDir, () => mkdirp(distDir, resolve));
      });
    });
};


module.exports = clean;