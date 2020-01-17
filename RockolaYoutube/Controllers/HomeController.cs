using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace RockolaYoutube.Controllers
{
    public class HomeController : Controller
    {
        List<string> videos = new List<string>();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchVideo(string keyword)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()

            {
                ApiKey = "AIzaSyA6F3R_kIdjAu7s1i7O7DwMxrvtwbpL1J8",
                ApplicationName = this.GetType().ToString()

            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = keyword; // Replace with your search term.
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.

            var searchListResponse = searchListRequest.Execute();
            return PartialView("Search", searchListResponse.Items);

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}