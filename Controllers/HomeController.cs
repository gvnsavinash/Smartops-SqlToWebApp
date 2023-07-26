using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Diagnostics;
using System.IO;


namespace SqlToWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "HYG File Upload Center And Pregis Web App";
            return View();
        }
        //public ActionResult Index(HttpPostedFileBase files)

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            try
            {
                // Verify that the user selected a file
                if (files != null && files.ContentLength > 0)
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(files.FileName);
                    // store the file inside ~/App_Data/uploads folder

                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

                    files.SaveAs(path);

                    HYGHelper.LoadData(path);

                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();

                // redirect back to the index action to show the form once again
            }
            catch (Exception e)
            {
                ViewBag.Message = "File upload failed!!";
                Debug.WriteLine("############   Exception while uploading file   #############");
                Debug.WriteLine(e.ToString());
                Console.WriteLine("############   Exception while uploading file   #############");
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }
    }

}
