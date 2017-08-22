let gulp = require('gulp');
var argv = require('yargs').argv;
var exec = require('sync-exec');

// Usage: gulp version -m "patch notes"
function version(done) {
    if (argv.m) {
        console.log('Versioning...');
        console.log(exec('npm version patch').stdout);
        done();
    }
};


module.exports = version;