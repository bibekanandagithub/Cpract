<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.IO;

public class FileUploadHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        HttpPostedFile file = context.Request.Files[0];
        string genFilename = System.DateTime.Now.ToFileTimeUtc().ToString();
        string filenmae = "FN_" + System.DateTime.Now.ToFileTimeUtc().ToString();
        fileupload.StoredFileName = filenmae;
        System.IO.Directory.CreateDirectory(context.Request.MapPath("~/uploads/"+filenmae) );

        string fname = context.Server.MapPath("~/uploads/"+filenmae+"//"+ file.FileName);
        file.SaveAs(fname);
        context.Response.ContentType = "text/plain";
        context.Response.Write("File Uploaded Successfully!");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}