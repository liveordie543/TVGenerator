tvGeneratorApp.service('mainService', function ($http) {
    this.getMoviesAndOrShows = function (includeMovies, includeShows, genreIds) {
        return $http({
            method: 'post',
            data: { IncludeMovies: includeMovies, IncludeShows: includeShows, GenreIds: genreIds },
            url: 'Media'
        });
    };

    this.validateMovieOrShowExistence = function (id, name) {
        return $http({
            method: 'get',
            url: 'Validate?id=' + id + '&name=' + name
        });
    };

    this.getAvailableGenres = function () {
        return $http({
            method: 'get',
            url: 'Genres'
        });
    };
});