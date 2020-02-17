using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;

namespace RockolaYoutube.Controllers
{
    public class HomeController : Controller
    {
        List<string> videos = new List<string>();
        public ActionResult Index()
        {
            return View();
        }
        //Hosted web API REST Service base url  
        readonly string Baseurl = "http://localhost:60/";
        [HttpGet]
        public  async Task<ActionResult> SearchVideo(string keyword)
        {
            List<SearchResult> SearchLista = new List<SearchResult>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllVideos using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Values?keyword=" + keyword);
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    SearchLista = JsonConvert.DeserializeObject<List<SearchResult>>(EmpResponse);
                }
                //returning the video list to view  
                return PartialView("Search", SearchLista);
            }



            //var youtubeService = new YouTubeService(new BaseClientService.Initializer()

            //{
            //    ApiKey = "AIzaSyA6F3R_kIdjAu7s1i7O7DwMxrvtwbpL1J8",
            //    ApplicationName = this.GetType().ToString()

            //});

            //var searchListRequest = youtubeService.Search.List("snippet");
            //searchListRequest.Q = keyword; // Replace with your search term.
            //searchListRequest.MaxResults = 10;

            //// Call the search.list method to retrieve results matching the specified query term.

            //var searchListResponse = searchListRequest.Execute();
            //return PartialView("Search", searchListResponse.Items);

            //WCFRef.Service1Client client = new WCFRef.Service1Client();
            //var videos = client.GetListYoutube(keyword);
            //return PartialView("Search", videos.ToList());

            //localhost:56007

        }

        [HttpPost]
        public ActionResult AddVideoId(string idVideo)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:59714/");
            var response = client.PostAsync("api/History/PostVideo?VideoId=" + idVideo, new StringContent(""));
            response.Wait();
            return PartialView("Search");
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