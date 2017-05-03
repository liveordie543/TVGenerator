using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using TVGenerator.Data.Models;
using TVGenerator.Data.Stores;

namespace TVGenerator.Controllers
{
    public class DataController : ApiController
    {
        [HttpPost]
        [Route("Media")]
        public IHttpActionResult GetMoviesAndOrShows([FromBody]DataFilter filter)
        {
            return Ok(new MoviesAndShowsStore().GetMoviesAndShows(filter));
        }

        [HttpGet]
        [Route("Validate")]
        public IHttpActionResult ValidateMovieOrShowExistence(int id, string name)
        {
            HttpResponseMessage response = null;

            using (HttpClient client = new HttpClient())
            {
                string fullUrl = $"https://www.netflix.com/watch/{id}";

                response = client.GetAsync(fullUrl).Result;
            }

            if(Regex.IsMatch(response.Content.ReadAsStringAsync().Result, name, RegexOptions.IgnoreCase))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Genres")]
        public IHttpActionResult GetAvailableGenres()
        {
            return Ok(new GenresStore().GetGenreOptions());
        }
    }
}
