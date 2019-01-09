using Bissoft.CouncilCMS.BLL.Services;
using Bissoft.CouncilCMS.BLL.ViewModels;
using Bissoft.CouncilCMS.BLL.ViewModels.Admin;
using Bissoft.CouncilCMS.Web.Controllers;
using Bissoft.CouncilCMS.Web.Core.ExternalLogins;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bissoft.CouncilCMS.Web.Areas.ControlPanel.Controllers
{
    public class FacebookLoginController : BaseCmsController
    {
        private CmsSettingsAdminService settingsAdminService;

        public FacebookLoginController()
        {
            settingsAdminService = new CmsSettingsAdminService(this.connectionString);
        }

		public ActionResult InvokeTheLoginDialogWindow()
		{
			// trot
			//https://www.facebook.com/v3.2/dialog/oauth?
			//    client_id ={ app - id}
			//    &redirect_uri ={ redirect - uri}
			//    &state ={ state - param }

			String host = Request.Url.Host;
			int port = Request.Url.Port;

			string baseUrl = "https://www.facebook.com/v3.2/dialog/oauth?";
			string client_id = "client_id=" + CmsSettings.FacebookAppId;
			string redirect_uri = string.Format("&redirect_uri=https://{0}/FacebookLogin/GetExternalAccessToken", Request.Url.Authority);
			string state = "&state={st=state013gti,ds=300601903}";

			//String scope = "&scope=publish_stream,manage_pages";
			StringBuilder sb = new StringBuilder();

			sb.Append(baseUrl);
			sb.Append(client_id);
			sb.Append(redirect_uri);
			sb.Append(state);

			string url = sb.ToString();
			return Redirect(url);
		}

		[HttpGet]
        public ActionResult GetExternalAccessToken(string code)
        {
            var Route = Request.RequestContext.RouteData.Route;
            if (String.IsNullOrEmpty(code) == true)
            {
                return new HttpStatusCodeResult(400);
            }
            
            if (String.IsNullOrEmpty(code))
                return Redirect("/FacebookLogin/InvokeTheLoginDialogWindow");
            

            //GET https://graph.facebook.com/v3.2/oauth/access_token?
            //client_id ={ app - id}
            //&redirect_uri ={ redirect - uri }
            //&client_secret ={ app - secret }
            //&code ={ code - parameter }

            // token expires
            //GET graph.facebook.com / debug_token ?
            //input_token ={ token - to - inspect}
            //&access_token ={ app - token - or - admin - token}


            StringBuilder sb = new StringBuilder();

            sb.Append("https://graph.facebook.com/v3.2/oauth/access_token?");
            sb.AppendFormat("client_id={0}", CmsSettings.FacebookAppId);
            sb.AppendFormat("&redirect_uri=https://{0}/FacebookLogin/GetExternalAccessToken", Request.Url.Authority);
            sb.AppendFormat("&client_secret={0}", CmsSettings.FacebookSecretKey);
            sb.AppendFormat("&code={0}", code);

            string url = sb.ToString();

            FacebookAccessToken token = null;
            HttpClient client = new HttpClient();
            try
            {
                var answer = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<FacebookAccessToken>(answer);
            }
            catch(Exception)
			{
                return new HttpStatusCodeResult(HttpStatusCode.UpgradeRequired); ;
            }

            if (token.access_token != null)
            {
                CmsSettings.FacebookUserAccessToken = token.access_token;
                CmsSettings.FacebookUserAccessTokenExpires = token.expires_in;

                var settingsForEdit = settingsAdminService.GetForEdit();
                settingsForEdit.FacebookUserAccessToken = token.access_token;
                settingsForEdit.FacebookUserAccessTokenExpires = token.expires_in;
                settingsAdminService.Save(settingsForEdit);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.UpgradeRequired);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostOnPage(FacebookPostModel postModel)
        {
            string appPath = HttpRuntime.AppDomainAppPath;
            postModel.link = Request.Url.Scheme + "://" + Request.Url.Authority + postModel.link;
            FacebookPostEntity facebookPostEntity = new FacebookPostEntity(postModel);

            var isTokenIsExpired = !string.IsNullOrEmpty(CmsSettings.FacebookUserAccessToken);
            bool result = false; 
            if (isTokenIsExpired)
            {
                facebookPostEntity.access_token = CmsSettings.FacebookUserAccessToken;

                FacebookPagePostService service = new FacebookPagePostService(facebookPostEntity, 
                    CmsSettings.FacebookPublicPageId, 
                    facebookPostEntity.access_token, 
                    appPath);

                result = await service.PostOnPage();
            }
            if (result == true)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}

