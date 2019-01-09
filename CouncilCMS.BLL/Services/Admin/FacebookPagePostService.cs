using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.ViewModels.Admin;
using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bissoft.CouncilCMS.Web
{
    public class FacebookPagePostService
    {
        // temporal variables

        private string publicPageId;
        private string userId;
        private string access_token;
        private string page_access_token;
        private string serverPath;

        public Exception exceptionOfOperation;
        public string whatIsWrong = "";

        private FacebookPostEntity postObj;

        public FacebookPagePostService(FacebookPostEntity entity, string pageId, string token, string appPath)
        {
            publicPageId = pageId;
            postObj = entity;
            access_token = token;
            serverPath = appPath;
        }

        public async Task<bool> PostOnPage()
        {
			bool operationStatus = true;

            operationStatus = await GetUserId();
            if (operationStatus == true) 
            {
                operationStatus = await GetPageAccessToken();
                if (operationStatus == true)
                {
                    operationStatus = Post(postObj);
                }
            }
            return operationStatus;
        }

        private async Task<bool> GetUserId()
        {
            // https://graph.facebook.com/me?fields=id&access_token="xxxxx"

            string str = "https://graph.facebook.com/v3.2/me?access_token=" + access_token;

            HttpClient client = new HttpClient();
            HttpResponseMessage msg = null;
            try
            {
                msg = await client.GetAsync(str);
                string contentAsString = await msg.Content.ReadAsStringAsync();
                dynamic o = JsonConvert.DeserializeObject(contentAsString);
                userId = o.id;
                return true;
            }
            catch(Exception)
			{
                return false;
            }
            
        }
        private async Task<bool> GetPageAccessToken()
        {
            string url = "https://graph.facebook.com/v3.2/" + userId + $"/accounts?access_token={access_token}";
            HttpClient client = new HttpClient();
            try
            {
                var msg = await client.GetAsync(url);
                string contentAsString = await msg.Content.ReadAsStringAsync();
                dynamic o = JsonConvert.DeserializeObject(contentAsString);
                page_access_token = o.data[0].access_token;
                return true;
            }
            catch(Exception)
			{
                return false;
            }
        }

		private bool Post(FacebookPostModel entity)
		{
			try
			{
				FacebookClient facebookClient = new FacebookClient(page_access_token);
				Dictionary<string, object> photoArgs = new Dictionary<string, object>();
				Dictionary<string, object> messageArgs = new Dictionary<string, object>();
				if(string.IsNullOrEmpty(entity.source) == false)
				{
					// create an media object
					FacebookMediaObject facebookUploader = new FacebookMediaObject
					{
						FileName = entity.name,
						ContentType = "image/jpg"
					};

					// set the media object
					byte[] bytes = null;
					string imgPath = entity.source;
					imgPath = imgPath.Replace('/', '\\');
					imgPath = new string(imgPath.ToCharArray().ToArray());
					string filePath = serverPath + imgPath;
					try
					{
						bytes = File.ReadAllBytes(filePath);
					}
					catch(Exception)
					{
						return false;
					}
					facebookUploader.SetValue(bytes);
					// 
					photoArgs["image"] = facebookUploader;

					string fullStr = "";

					if(string.IsNullOrEmpty(entity.name) == false)
					{
						if(string.IsNullOrEmpty(entity.message) == false)
						{
							fullStr = entity.name + "\n" + entity.message;
						}
						else
						{
							fullStr = entity.name;
						}
					}
					else if(string.IsNullOrEmpty(entity.message) == false)
					{
						fullStr = entity.message;
					}

					if(string.IsNullOrEmpty(entity.link) == false)
					{
						fullStr = fullStr + "\n" + entity.link;
					}
					photoArgs["name"] = fullStr;
					facebookClient.Post(publicPageId + "/photos", photoArgs);
				}
				else
				{
					if(string.IsNullOrEmpty(entity.link) == false)
					{
						/*
                        if (string.IsNullOrEmpty(entity.message) == false)
                        {
                            entity.message += '\n';
                            entity.message += entity.link;
                        }
                        if (string.IsNullOrEmpty(entity.name) == false)
                        {
                            entity.name += '\n';
                            entity.name += entity.link;
                        }
                        */
						messageArgs["link"] = entity.link;
					}
					string fullMsg = "";

					if(string.IsNullOrEmpty(entity.name) == false)
					{
						if(string.IsNullOrEmpty(entity.message) == false)
						{
							fullMsg = entity.name + "\n" + entity.message;
						}
						else
						{
							fullMsg = entity.name;
						}
						messageArgs["message"] = fullMsg;
					}
					else

					if(string.IsNullOrEmpty(entity.message) == false)
					{
						messageArgs["message"] = entity.message;
					}

					facebookClient.Post(publicPageId + "/feed", messageArgs);
				}

				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
	}
}