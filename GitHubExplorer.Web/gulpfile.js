/// <binding BeforeBuild='clean' AfterBuild='default' />
var gulp = require('gulp');

var plugins = require("gulp-load-plugins")({
 pattern: ['gulp-*', 'gulp.*', 'main-bower-files', 'del'],
 replaceString: /\bgulp[\-.]/
});

gulp.task('clean', function () {
   return plugins.del(['dist/*', 'fonts/*']);
});

gulp.task('min:js', function () {    
   var jsFiles = plugins.mainBowerFiles('**/*.js');
   console.log(jsFiles);
   return gulp.src(plugins.mainBowerFiles('**/*.js'))
       .pipe(plugins.concat('site.min.js'))
       .pipe(plugins.uglify())
       .pipe(gulp.dest('dist/'));
});

gulp.task('min:css', function () {
    console.log('min:css running');
    var cssFiles = ['Content/*'];
    gulp.src(plugins.mainBowerFiles('**/*.css', {
            overrides: {
                bootstrap: {
                    main: [
                        'dist/css/bootstrap.css',
                    ]
                }
            }
        }).concat(cssFiles))
        .pipe(plugins.concat('all.min.css'))
        .pipe(plugins.cleanCss())
        .pipe(gulp.dest('dist/'));
});

gulp.task('fonts', function () {
    gulp.src(['bower_components/**/*.{eot,svg,ttf,woff,woff2}'])
    .pipe(plugins.flatten())
    .pipe(gulp.dest('fonts/'));
});

gulp.task('watch', function () {
    return gulp.watch(plugins.mainBowerFiles, ['min:js', 'min:css', 'fonts']);
});

//Set a default tasks
gulp.task('default', ['clean', 'min:js', 'min:css', 'fonts'], function () { });

