/* global require */
'use strict';
var _ = require('lodash'),
	child_process = require('child_process'),
	gutil = require('gulp-util'),
	PluginError = gutil.PluginError,
	es = require('event-stream'),
	path = require('path'),
	temp = require('temp'),
	fs = require('fs'),
	teamcity = require('./teamcity'),

	PLUGIN_NAME = 'gulp-nunit-runner',
	NUNIT_CONSOLE = 'nunit-console.exe',
	NUNIT_X86_CONSOLE = 'nunit-console-x86.exe',

	runner;

// Main entry point
runner = function gulpNunitRunner(opts) {
	var stream,
		files;
	opts = opts || {};

	files = [];

	stream = es.through(function write(file) {
		if (_.isUndefined(file)) {
			fail(this, 'File may not be null.');
		}

		files.push(file);
		this.emit('data', file);
	}, function end() {
		run(this, files, opts);
	});

	return stream;
};

runner.getExecutable = function (options) {
	var executable,
		consoleRunner;
	consoleRunner = options.platform === 'x86' ? NUNIT_X86_CONSOLE : NUNIT_CONSOLE;
	if (!options.executable) {
		return consoleRunner;
	}
	// trim any existing surrounding quotes and then wrap in ""
	executable = trim(options.executable, '\\s', '"', "'");
	return !path.extname(options.executable) ?
			path.join(executable, consoleRunner) : executable;
};

runner.getArguments = function (options, assemblies) {
	var args = [];

	if (options.options) {
		args = args.concat(parseSwitches(options.options));
	}
	args = args.concat(assemblies);

	return args;
};

function parseSwitches(options) {
	var filtered,
		switches,
		isWin = /^win/.test(process.platform),
	// when running under mono on linux/mac switches must be specified with a - not a /
		switchChar = isWin ? '/' : '-';

	switches = _.map(options, function (val, key) {
		var qualifier;
		if (typeof val === 'boolean') {
			if (val) {
				return (switchChar + key);
			}
			return undefined;
		}
		if (typeof val === 'string') {
			qualifier = val.trim().indexOf(' ') > -1 ? '"' : '';
			return (switchChar + key + ':' + qualifier + val + qualifier);
		}
		if (val instanceof Array) {
			return (switchChar + key + ':' + val.join(','));
		}
	});

	filtered = _.filter(switches, function (val) {
		return !_.isUndefined(val);
	});

	return filtered;
}

function fail(stream, msg) {
	stream.emit('error', new gutil.PluginError(PLUGIN_NAME, msg));
}

function end(stream) {
	stream.emit('end');
}

function run(stream, files, options) {

	var child,
		args,
		exe,
		opts,
		assemblies,
		cleanupTempFiles;

	options.options = options.options || {};

	if (!options.options.result && options.teamcity) {
		temp.track();
		options.options.result = temp.path({ suffix: '.xml' });
		cleanupTempFiles = temp.cleanup;
	}

	assemblies = files.map(function (file) {
		return file.path;
	});

	if (assemblies.length === 0) {
		return fail(stream, 'Some assemblies required.'); //<-- See what I did there ;)
	}

	opts = {
		stdio: [null, process.stdout, process.stderr, 'pipe']
	};

	exe = runner.getExecutable(options);
	args = runner.getArguments(options, assemblies);

	child = child_process.spawn(exe, args, opts);

	child.on('error', function (e) {
		fail(stream, e.code === 'ENOENT' ? 'Unable to find \'' + exe + '\'.' : e.message);
	});

	child.on('close', function (code) {
		if (options.teamcity) {
			if (fs.existsSync(options.options.result)) {
				gutil.log.apply(null, teamcity(fs.readFileSync(options.options.result, 'utf8')));
			} else {
				fail(stream, 'NUnit output not found: ' + options.options.result);
			}
		}
		if (cleanupTempFiles) {
			cleanupTempFiles();
		}
		if (code !== 0) {
			gutil.log(gutil.colors.red('NUnit tests failed.'));
			fail(stream, 'NUnit tests failed.');
		} else {
			gutil.log(gutil.colors.cyan('NUnit tests passed'));
		}
		return end(stream);
	});
}

function trim() {
	var args = Array.prototype.slice.call(arguments),
		source = args[0],
		replacements = args.slice(1).join(','),
		regex = new RegExp("^[" + replacements + "]+|[" + replacements + "]+$", "g");
	return source.replace(regex, '');
}

module.exports = runner;
