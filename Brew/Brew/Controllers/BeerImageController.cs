using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Brew.Controllers
{
    public class BeerImageController : Controller
    {
        // GET: /BeerImage/
        public FileResult Index(string name)
        {
            //TODO: read image from db by beer id

            return new FilePathResult("/Images/heroAccent.png", "image/png");
        }

    }
}
