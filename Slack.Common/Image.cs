using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Slack.Common
{
    public static class Image
    {
        public static string Upload(this HttpPostedFileBase file)
        {
            //if file is not image type(jpg,png)
            if (!file.ContentType.Contains("jpg") && !file.ContentType.Contains("jpeg")
                && !file.ContentType.Contains("png"))
                throw new Exception("Couldn't upload file");

            string virtualPath = $"/Content/Users/Images/{HttpContext.Current.User.Identity.GetUserId()}";
            string physicPath = HttpContext.Current.Server.MapPath(virtualPath);

            if (!Directory.Exists(physicPath))
                Directory.CreateDirectory(physicPath);

            physicPath += $"/{file.FileName}";//to save file
            file.SaveAs(physicPath);

            virtualPath += $"/{file.FileName}"; //in order to return full 

            return virtualPath;
        }

        public static string Upload(this HttpFileCollectionBase files)
        {
            string[] paths = new string[files.Count + 1];
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];

                //if file is not image type(jpg,png)
                if (!file.ContentType.Contains("jpg") && !file.ContentType.Contains("jpeg")
                    && !file.ContentType.Contains("png"))
                    throw new Exception("Couldn't upload file");

                string virtualPath = $"/Content/Users/Images/{HttpContext.Current.User.Identity.GetUserId()}";
                string physicPath = HttpContext.Current.Server.MapPath(virtualPath);

                if (!Directory.Exists(physicPath))
                    Directory.CreateDirectory(physicPath);

                physicPath += $"/{file.FileName}";//to save file
                file.SaveAs(physicPath);

                virtualPath += $"/{file.FileName}"; //in order to return full 
                paths[i] = virtualPath; //virtual path for client
            }
            return JsonConvert.SerializeObject(paths);
        }
    }
}
