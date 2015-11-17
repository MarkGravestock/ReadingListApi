/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var jshint = require('gulp-jshint');
var del = require('del');
var Server = require('karma').Server;
var protractor = require("gulp-protractor").protractor;


var paths = {
    src: "./app/js/*.js",
    dest: "./wwwroot/app/js"
}

gulp.task("clean", function () {
    del(paths.dest + '**/*');    // Delete everything in 'wwwroot/js'
});

gulp.task('default', ['clean'], function () {
    return gulp.src(paths.src)         // Returns a stream
        .pipe(jshint())
        .pipe(jshint.reporter('default'))
        .pipe(gulp.dest(paths.dest))   // Pipes the stream somewhere
});

gulp.task('unit-test', function (done) {
    new Server({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }, done).start();

});

gulp.task('tdd', function (done) {
    new Server({
        configFile: __dirname + '/karma.conf.js'
    }, done).start();
});

gulp.task('integration-test', function(done) {

    console.log("prot: " + require("gulp-protractor").getProtractorDir());

    return gulp.src(['./test/integration/*.js'])
        .pipe(protractor({
            configFile: __dirname + '/protractor.config.js',
            args: ['--baseUrl', 'http://127.0.0.1:8000']
        }))
        .on('error', function (e) { throw e });
});


