using System;
using System.Web;
using System.Web.Http;
using System.Drawing;
using System.Web.Http.Cors;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.IO;
using xTab.Tools.Helpers;
using xTab.Tools.Extensions;

namespace Bissoft.CouncilCMS.Web.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadController : ApiController
    {
        [HttpPost]
        [ActionName("UploadFile")]
        public IHttpActionResult UploadFile(
            String Type = "image", 
            String Directory = "other",
            String OldFile = null,
            Boolean WithThumbs = false,
            Boolean SaveOriginal = false,
            Boolean Crop = false,
            Int32? LimitWidth = 1920, 
            Int32? LimitHeight = 1200, 
            Int32? CropWidth = null,
            Int32? CropHeight = null)
        {
            try
            {
                var chunks = HttpContext.Current.Request.Params["chunks"] != null ? (int?)int.Parse(HttpContext.Current.Request.Params["chunks"]) : null;
                var chunk = HttpContext.Current.Request.Params["chunk"] != null ? (int?)int.Parse(HttpContext.Current.Request.Params["chunk"]) : null;
                var name = HttpContext.Current.Request.Params["name"];
                var file = HttpContext.Current.Request.Files[0];
                var dPath = IoHelper.FormatPath("/upload/" + Directory);
                var fName = chunk >= 0 ? name : file.FileName;
                var fExt = fName.Substring(fName.LastIndexOf(".") + 1);
                var fPath = chunk >= 0 ? (dPath + fName) : IoHelper.GetRandomFileName(dPath, fExt, 15, false);
                var ePath = IoHelper.GetSubName(fPath, "_original");
                var fSize = file.ContentLength / 1024;
                var fPrev = fPath;
                var fCrop = false;
                var fOrig = SaveOriginal ? ePath : fPath;
                var uploaded = true;

                IoHelper.CreateDirectory(dPath);

                if (chunk >= 0)
                {
                    uploaded = false;

                    using (var fs = new FileStream(HttpContext.Current.Server.MapPath(fPath), chunk == 0 ? FileMode.Create : FileMode.Append))
                    {
                        var buffer = new byte[file.InputStream.Length];
                        file.InputStream.Read(buffer, 0, buffer.Length);
                        fs.Write(buffer, 0, buffer.Length);
                    }

                    if (chunk == chunks - 1)
                        uploaded = true;
                }
                else
                {
                    if (Type != "image" || fExt.ToLower() == "svg")
                        file.SaveAs(HttpContext.Current.Server.MapPath(fPath));
                }

                if (uploaded)
                {
                    switch (Type)
                    {
                        case "file":
                            fPrev = "http://cdn.bissoft.org/img/parts/file-icon.png";
                            break;
                        case "image":
                            if (fExt.ToLower() != "svg")
                            {                               
                                var stream = chunk >= 0 ?
                                    new FileInfo(HttpContext.Current.Server.MapPath(fPath)).OpenRead() :
                                    file.InputStream;

                                var img = Image.FromStream(stream);

                                stream.Close();

                                if (LimitWidth > 0 || LimitHeight > 0)
                                    img = ImgHelper.LimitSize(img, new Size(LimitWidth ?? 0, LimitHeight ?? 0));

                                if (SaveOriginal)
                                    ImgHelper.Save((Bitmap)img, ePath);

                                if (CropWidth > 0 && CropHeight > 0)
                                {
                                    if ((img.Width != CropWidth || img.Height != CropHeight) && Crop)
                                        fCrop = true;

                                    img = ImgHelper.Resize(img, new Size(CropWidth.Value, CropHeight.Value), true);
                                }

                                if (WithThumbs)
                                {
                                    var xxxImg = ImgHelper.Resize(img, new Size(1250, 800), true);
                                    var xxImg = ImgHelper.Resize(img, new Size(600, 450), true);
                                    var xImg = ImgHelper.Resize(img, new Size(320, 240), true);

                                    ImgHelper.Save(xxxImg, fPath.Insert(fPath.LastIndexOf("."), "_th3x"), 95);
                                    ImgHelper.Save(xxImg, fPath.Insert(fPath.LastIndexOf("."), "_th2x"), 95);
                                    ImgHelper.Save(xImg, fPath.Insert(fPath.LastIndexOf("."), "_th1x"), 95);

                                    xxxImg.Dispose();
                                    xxImg.Dispose();
                                    xImg.Dispose();
                                }

                                IoHelper.DeleteFile(fPath);
                                ImgHelper.Save((Bitmap)img, fPath, 95);
                              
                                img.Dispose();
                            }
                            break; 
                        default:
                            goto case "file";
                    }

                    if (chunk >= 0)
                    {
                        var fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(fPath));
                        fSize = (int)(fileInfo.Length / 1024);
                    }

                    if (!String.IsNullOrEmpty(OldFile))
                        IoHelper.DeleteFile(OldFile, "_original,_th3x,_th2x,_th1x");

                    return Json(new { path = fPath, preview = fPrev, crop = fCrop, original = fOrig, size = fSize, ext = fExt, name = name.Substring(0, name.LastIndexOf(".") - 1) });
                }
                else
                {
                    return Json(new { preview = "chunkUploaded" });
                } 
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("ResizeImage")]
        public IHttpActionResult ResizeImage(
            string Path, 
            int Width, 
            int Height, 
            int X, 
            int Y, 
            int? ResizeWidth = null, 
            int? ResizeHeight = null, 
            bool WithThumbs = false,
            string Directory = null,
            bool DeleteOriginal = false)
        {
            var stream = new FileInfo(HttpContext.Current.Server.MapPath(Path)).OpenRead();
            var img = Image.FromStream(stream);
            var imgPath = Path.Contains("_original") ? Path.Replace("_original", String.Empty) : Path;
            var origPath = Path.Contains("_original") ? Path : IoHelper.GetSubName(Path, "_original");
            var dPath = !String.IsNullOrEmpty(Directory) ? ("/upload/" + Directory + "/") : IoHelper.GetFileDirectoryPath(imgPath);
            var fPath = IoHelper.GetRandomFileName(dPath, imgPath.Substring(imgPath.LastIndexOf(".") + 1), 20, false);
            var fOrig = DeleteOriginal ? fPath : IoHelper.GetSubName(fPath, "_original");

            stream.Close();

            img = ImgHelper.Crop(img, new Rectangle(X, Y, Width, Height));

            if (ResizeWidth > 0 && ResizeHeight > 0)
                img = ImgHelper.Resize(img, new Size(ResizeWidth.Value, ResizeHeight.Value), true);

            ImgHelper.Save((Bitmap)img, fPath, 95);

            if (WithThumbs)
            {
                var xxxImg = ImgHelper.Resize(img, new Size(1250, 800), true);
                var xxImg = ImgHelper.Resize(img, new Size(600, 450), true);
                var xImg = ImgHelper.Resize(img, new Size(320, 240), true);

                ImgHelper.Save(xxxImg, fPath.Insert(fPath.LastIndexOf("."), "_th3x"), 95);
                ImgHelper.Save(xxImg, fPath.Insert(fPath.LastIndexOf("."), "_th2x"), 95);
                ImgHelper.Save(xImg, fPath.Insert(fPath.LastIndexOf("."), "_th1x"), 95);

                xxxImg.Dispose();
                xxImg.Dispose();
                xImg.Dispose();
            }

            img.Dispose();

            IoHelper.DeleteFile(imgPath);

            if (DeleteOriginal)
                IoHelper.DeleteFile(origPath);
            else
                IoHelper.MoveFile(origPath, fOrig);

            return Json(new { path = fPath, preview = fPath, original = fOrig });
        }

        [HttpPost]
        [ActionName("CkeImageUpload")]
        public IHttpActionResult CkeImageUpload(string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var message = String.Empty;
            var upload = HttpContext.Current.Request.Files[0];
            var ext = upload.FileName.Substring(upload.FileName.LastIndexOf(".") + 1);
            var fPath = IoHelper.GetRandomFileName("/upload/editor/", upload.FileName.Substring(upload.FileName.LastIndexOf(".") + 1), 15, false);

            try
            {
                if (upload.ContentType.ToLower().Contains("image"))
                {
                    Image Img = Image.FromStream(upload.InputStream);
                    ImgHelper.Save(ImgHelper.LimitSize(Img, new Size(1280, 2000)), fPath);
                    Img.Dispose();

                    message = "Изображение успешно загружено";
                }
                else
                    message = "Выбранный файл не является изображением";
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            var result = "<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + fPath + "','" + message + "');</script></body></html>";
         
            return Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter() { }, "text/html");
        }

        [HttpPost]
        [ActionName("CkeFileUpload")]
        public IHttpActionResult CkeFileUpload(string CKEditorFuncNum, string CKEditor, string langCode)
        {
            var message = String.Empty;
            var upload = HttpContext.Current.Request.Files[0];
            var ext = upload.FileName.Substring(upload.FileName.LastIndexOf("."));
            var name = upload.FileName.Substring(0, upload.FileName.LastIndexOf("."));
            var fName = name.Replace(" ", "_").Translit() + ext;
            var fPath = "/upload/editor/" + fName;

            try
            {
                upload.SaveAs(HttpContext.Current.Server.MapPath(fPath));
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            var result = "<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + fPath + "','" + message + "');</script></body></html>";

            return Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter() { }, "text/html");
        }


        [HttpPost]
        [ActionName("CkeImageUploadNew")]
        public IHttpActionResult CkeImageUploadNew()
        {
            var message = String.Empty;
            var upload = HttpContext.Current.Request.Files[0];
            var ext = upload.FileName.Substring(upload.FileName.LastIndexOf(".") + 1);
            var fPath = IoHelper.GetRandomFileName("/upload/editor/", upload.FileName.Substring(upload.FileName.LastIndexOf(".") + 1), 15, false);
            var fName = fPath.Substring(fPath.LastIndexOf("/") + 1);

            try
            {
                if (upload.ContentType.ToLower().Contains("image"))
                {
                    Image Img = Image.FromStream(upload.InputStream);
                    ImgHelper.Save(ImgHelper.LimitSize(Img, new Size(1280, 2000)), fPath);
                    Img.Dispose();

                    message = "Изображение успешно загружено";
                }
                else
                    message = "Выбранный файл не является изображением";
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return Json(new { uploaded = 1, fileName = fName, url = fPath });
        }

        [HttpPost]
        [ActionName("DeleteFile")]
        public IHttpActionResult DeleteFile(String File)
        {
            try
            {
                IoHelper.DeleteFile(File);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

