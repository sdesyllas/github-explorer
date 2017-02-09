/// <binding BeforeBuild='clean' AfterBuild='default' />
// include plug-ins
var gulp = require('gulp');
var mainBowerFiles = require('main-bower-files');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var order = require('gulp-order');
var cleanCSS = require('gulp-clean-css');
var del = require('del');
var flatten = require('gulp-flatten');

gulp.task('clean', function () {
   return del(['app/all.min.js', 'app/all.min.css', 'fonts/*']);
});

gulp.task('min:js', function () {
    console.log('min:js running');
    var bowerJsFiles = mainBowerFiles("**/*.js");
    console.log('bower js files: ', bowerJsFiles);
    gulp.src(bowerJsFiles)
        .pipe(uglify())
        .pipe(concat('all.min.js'))
        .pipe(gulp.dest('app/'));
});

gulp.task('min:css', function () {
    console.log('min:css running');
    var cssFiles = ['Content/*'];
    gulp.src(mainBowerFiles('**/*.css', {
            overrides: {
                bootstrap: {
                    main: [
                        'dist/css/bootstrap.css',
                    ]
                }
            }
        }).concat(cssFiles))
        .pipe(concat('all.min.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('app/'));
});

gulp.task('watch', function () {
    return gulp.watch(mainBowerFiles, ['min:js', 'min:css']);
});

gulp.task('fonts', function () {
    gulp.src(['bower_components/**/*.{eot,svg,ttf,woff,woff2}'])
    .pipe(flatten())
    .pipe(gulp.dest('fonts/'));
});


//Set a default tasks
gulp.task('default', ['clean', 'min:js', 'min:css', 'fonts'], function () { });

