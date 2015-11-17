module.exports = function(config) {
    config.set({
        browsers: ['Chrome'],
        frameworks: ['jasmine'],
        files: ['wwwroot/lib/**/angular.js', 'wwwroot/lib/**/angular-mocks.js', 'app/js/**/*.js', 'test/unit/**/*.js']
    });
}
