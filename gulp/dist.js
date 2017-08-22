let gulp = require('gulp');
var exec = require('sync-exec');
var argv = require('yargs').argv;

function dist(done) {
    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
    if (argv.m) {
        console.log("Publishing to npm...");
        console.log(exec('npm publish').stdout);
        console.log("Pushing to GitHub...");
        console.log(exec('git commit -a -m \"' + argv.m + '\"').stdout);
        console.log(exec('git push origin master').stdout);
    };

    done();
};

module.exports = dist;