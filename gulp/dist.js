let gulp = require('gulp');
var exec = require('sync-exec');

function dist() {
    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
};

module.exports = dist;