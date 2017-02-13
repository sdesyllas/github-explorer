/// <binding BeforeBuild='clean' AfterBuild='default' />
var gulp = require('gulp');

var plugins = require("gulp-load-plugins")({
 pattern: ['gulp-*', 'gulp.*', 'main-bower-files', 'del'],
 replaceString: /\bgulp[\-.]/
});

gulp.task('clean', function () {
   return plugins.del(['dist/*', 'fonts/*']);
});

var config = {
    distFolder : 'dist/', 
    minjs:{
        jsBowerFiles : '**/*.js',
        jsConcatFile : 'all.min.js'
    },
    minCss:{
        bowerCssFiles : '**/*.css',
        bootstrapOverrideCssPath : 'dist/css/bootstrap.css',
        cssConcatFile : 'all.min.css',
    },
    fonts:{
        fontFiles : 'bower_components/**/*.{eot,svg,ttf,woff,woff2}',
        fontsFolder : 'fonts/'
    }
}

gulp.task('min:js', function () {    
   return gulp.src(plugins.mainBowerFiles(config.minjs.jsBowerFiles))
       .pipe(plugins.concat(config.minjs.jsConcatFile))
       .pipe(plugins.uglify())
       .pipe(gulp.dest(config.distFolder));
});

gulp.task('min:css', function () {
    var cssFiles = ['Content/*'];
    gulp.src(plugins.mainBowerFiles(config.minCss.bowerCssFiles, {
            overrides: {
                bootstrap: {
                    main: [
                        config.minCss.bootstrapOverrideCssPath,
                    ]
                }
            }
        }).concat(cssFiles))
        .pipe(plugins.concat(config.minCss.cssConcatFile))
        .pipe(plugins.cleanCss())
        .pipe(gulp.dest(config.distFolder));
});

gulp.task('fonts', function () {
    gulp.src(config.fonts.fontFiles)
    .pipe(plugins.flatten())
    .pipe(gulp.dest(config.fonts.fontsFolder));
});

gulp.task('watch', function () {
    return gulp.watch(plugins.mainBowerFiles(), ['min:js', 'min:css', 'fonts']);
});

//Set a default tasks
gulp.task('default', ['clean', 'min:js', 'min:css', 'fonts'], function () { });
