tvGeneratorApp.controller('mainController', function ($scope, mainService) {
    var moviesAndOrShows = {};
    var programTimeout;
    var programWindow;

    $scope.homeClick = function (event) {
    };

    $scope.startClick = function (event) {
        var includeMovies = false;
        var includeShows = true;
        var genreIds = [1];

        mainService.getMoviesAndOrShows(includeMovies, includeShows, genreIds).then(function (result) {
            moviesAndOrShows = result.data;

            startNextProgram();
        }, function (error) {
            console.log(error.data);
        });
    };

    function startNextProgram() {
        if (programWindow) {
            programWindow.close();
        }

        if (programTimeout) {
            clearTimeout(programTimeout);
        }

        var chosenMovieOrShow = moviesAndOrShows[getRandomNumber(moviesAndOrShows.length)];
        var chosenEpisode = chosenMovieOrShow.Episodes.length == 0 ? null : chosenMovieOrShow.Episodes[getRandomNumber(chosenMovieOrShow.Episodes.length)];
        var url = 'https://www.netflix.com/watch/' + chosenMovieOrShow.ShowId + (chosenEpisode != null ? '?trackId=' + chosenEpisode.EpisodeId : '');

        mainService.validateMovieOrShowExistence(chosenMovieOrShow.ShowId, chosenMovieOrShow.Name).then(function (result) {
            programWindow = window.open(url);

            var runtime = chosenMovieOrShow.Runtime ? chosenMovieOrShow.Runtime : 30000; //21 minutes 1260000

            programTimeout = setTimeout(function () { startNextProgram(); }, runtime);
        }, function (error) {
            startNextProgram();
        });
    }

    function getRandomNumber(max) {
        return Math.floor(Math.random() * (max - 0)) + 0;
    }
});