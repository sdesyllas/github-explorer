// include plug-ins
var gulp = require('gulp');
var mainBowerFiles = require('main-bower-files');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');
var order = require('gulp-order');
var cleanCSS = require('gulp-clean-css');
var del = require('del');

gulp.task('cleanjs', function () {
   //del is an async function and not a gulp plugin (just standard nodejs)
   //It returns a promise, so make sure you return that from this task function
   //  so gulp knows when the delete is complete
   return del(['app/all.min.js']);
});

gulp.task('cleancss', function () {
   //del is an async function and not a gulp plugin (just standard nodejs)
   //It returns a promise, so make sure you return that from this task function
   //  so gulp knows when the delete is complete
   return del(['app/all.min.css']);
});

gulp.task('min:js', ['cleanjs'], function () {
    console.log('min:js running');
    var bowerJsFiles = mainBowerFiles("**/*.js");
    console.log('bower js files: ', bowerJsFiles);
    gulp.src(bowerJsFiles)
        .pipe(uglify())
        .pipe(concat('all.min.js'))
        .pipe(gulp.dest('app/'));
});

gulp.task('min:css', ['cleancss'], function () {
    console.log('min:css running');
    var bowerCssFiles = mainBowerFiles("*.css");
    console.log('bower css files: ', bowerCssFiles);
    gulp.src(bowerCssFiles)
        .pipe(concat('all.min.css'))
        .pipe(cleanCSS())
        .pipe(gulp.dest('app/'));
});

gulp.task('watch', function () {
    return gulp.watch(mainBowerFiles, ['min:js', 'min:css']);
});

//Set a default tasks
gulp.task('default', ['min:js', 'min:css'], function () { });

