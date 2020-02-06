using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldCuisinesRestaurants.Controllers
{
    public class ReservationPrintController : Controller
    {
        // GET: ReservationPrint
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public FileResult PrintOrder(string orderNumber, string date, string sum, string restaurant)
        {
            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            //Create a PDF Document
            var PDF = Renderer.RenderHtmlAsPdf("Please print your Order " + "<br>" + "<br>" +
                "<br>" + " Order Number :    " + orderNumber + "<br>" +
             "<br>" + " Restaurant Name:    " + restaurant + "<br>" +
             "<br>" + " Order Total:    " + sum + "<br>" +
             "<br>" + " Date  :    " + date
               );
            //return a  pdf document from a view
            var contentLength = PDF.BinaryData.Length;
            Response.AppendHeader("Content-Length", contentLength.ToString());
            Response.AppendHeader("Content-Disposition", "inline; filename=Document_" + "142" + ".pdf");
            return File(PDF.BinaryData, "application/pdf;");

            // return View();
        }
    }
}