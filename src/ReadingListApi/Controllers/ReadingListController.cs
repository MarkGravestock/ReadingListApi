using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using ReadlingList.Domain;

namespace ReadingListApi.Controllers
{
    [Route("api/[controller]")]
    public class ReadingListController : Controller
    {
        [HttpGet]
        public IEnumerable<ItemToRead> Get()
        {
            return new[] {new ItemToRead {Description = "Solid Javascript", Uri = new Uri("https://www.youtube.com/watch?v=TAVn7s-kO9o"), Tags = new List<string>() {"Development", "SOLID"} } };
        }
    }
}