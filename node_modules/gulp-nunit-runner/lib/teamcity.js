"use strict";

var path = require('path'),
	_ = require('lodash'),
	sax = require('sax');

module.exports = function (results) {

	var getSuiteName,
		parser = sax.parser(true),
		log = [],
		ancestors = [],
		message, stackTrace;

	getSuiteName = function (node) {
		return node.attributes.type === 'Assembly' ?
			path.basename(node.attributes.name.replace(/\\/g, path.sep)) : node.attributes.name;
	};

	parser.onopentag = function (node) {
		ancestors.push(node);
		switch (node.name) {
			case 'test-suite':
				log.push('##teamcity[testSuiteStarted name=\'' + getSuiteName(node) + '\']');
				break;
			case 'test-case':
				if (node.attributes.executed === 'True') {
					log.push('##teamcity[testStarted name=\'' + node.attributes.name + '\']');
				}
				message = '';
				stackTrace = '';
				break;
		}
	};

	parser.oncdata = function (data) {
		data = data.
			replace(/\|/g, '||').
			replace(/\'/g, '|\'').
			replace(/\n/g, '|n').
			replace(/\r/g, '|r').
			replace(/\u0085/g, '|x').
			replace(/\u2028/g, '|l').
			replace(/\u2029/g, '|p').
			replace(/\[/g, '|[').
			replace(/\]/g, '|]');

		switch (_.last(ancestors).name) {
			case 'message':
				message += data;
				break;
			case 'stack-trace':
				stackTrace += data;
				break;
		}
	};

	parser.onclosetag = function (node) {
		node = ancestors.pop();
		switch (node.name) {
			case 'test-suite':
				log.push('##teamcity[testSuiteFinished name=\'' + getSuiteName(node) + '\']');
				break;
			case 'test-case':
				if (node.attributes.result === 'Ignored') {
					log.push('##teamcity[testIgnored name=\'' + node.attributes.name + '\'' +
						(message ? ' message=\'' + message + '\'' : '') + ']');
				} else if (node.attributes.executed === 'True') {
					if (node.attributes.success === 'False') {
						log.push('##teamcity[testFailed name=\'' + node.attributes.name + '\'' +
							(message ? ' message=\'' + message + '\'' : '') +
							(stackTrace ? ' details=\'' + stackTrace + '\'' : '') + ']');
					}
					var duration = node.attributes.time ? ' duration=\'' + parseInt(
						node.attributes.time.replace(/[\.\:]/g, '')) + '\'' : '';
					log.push('##teamcity[testFinished name=\'' + node.attributes.name + '\'' + duration + ']');
				}
				break;
		}
	};

	parser.write(results).close();

	return log;
};
