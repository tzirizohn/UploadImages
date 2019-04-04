using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Homework_4_01_19.Models;

namespace Homework_4_01_19.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
                 

        public ActionResult Upload(HttpPostedFileBase image, Images images)
        {
            string ext = Path.GetExtension(image.FileName);
            string filename = $"{Guid.NewGuid()}{ext}";
            string fullpath = $"{Server.MapPath("/UploadedImages")}\\{filename}";
            image.SaveAs(fullpath);
            PassWordManager pm = new PassWordManager(Properties.Settings.Default.Const);
            images.FileName = filename;
            int id= pm.AddImage(images);
            images.id = id;                
            return View(images);   
        }   

        public ActionResult EnterPassword(int id, string text)
        {
            PassWordManager pm = new PassWordManager(Properties.Settings.Default.Const);
            Images i = pm.GetImage(id, text);
            ViewModel vm = new ViewModel();
            vm.Image = i;

            if (Session["password"] == null)
            {
                Session["password"] = new List<int>();
            }
              
            List<int> ids = (List<int>)Session["password"];
            if (ids.Contains(id))
            {
                text = i.Password;
            }
            else if (text == i.Password)
            {
                ids.Add(id);
            }

            if (text != i.Password)
            {
                vm.Password = text;
                vm.IncorrectPassword = true;
            }
            else
            {
                vm.Password = text;
                vm.IncorrectPassword = false;
                int x = pm.Count(id);
                int y = pm.AddToCount(x, id);
                vm.Image.Count = y;
            }
            return View(vm);
        }      
    }
}