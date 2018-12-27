using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.BLL.Services.Admin
{
    class FacebookLoginService
    {
        /*
        public void InvokeTheLoginDialogWindow()
        {
            // trot
            //https://www.facebook.com/v3.2/dialog/oauth?
            //    client_id ={ app - id}
            //    &redirect_uri ={ redirect - uri}
            //    &state ={ state - param}

            String host = HttpContext.Current.Request.UserHostName;

            String baseUrl = "https://www.facebook.com/v3.2/dialog/oauth?";
            String client_id = "client_id=" + ExternalLoginsProvider.FacebookSecret.ClientId;
            String redirect_uri = "&redirect_uri=" + host + "/FacebookLogin/GetExternalAccessToken";
            String state = "&state={st=state013gti,ds=300601903}";
            String scope = "&scope=publish_stream,manage_pages";
            StringBuilder sb = new StringBuilder();

            sb.Append(baseUrl);
            sb.Append(client_id);
            sb.Append(redirect_uri);
            sb.Append(state);
            sb.Append(scope);

            String address = sb.ToString();
            
            WebClient wc = new WebClient()
            {
                Proxy = null
            };
            String answer = wc.DownloadString(address);
        }

        public IHttpActionResult GetExternalAccessToken(String code)
        {
            if (String.IsNullOrEmpty(code))
                return Redirect("/FacebookLogin/InvokeTheLoginDialogWindow");

            //GET https://graph.facebook.com/v3.2/oauth/access_token?
            //client_id ={ app - id}
            //&redirect_uri ={ redirect - uri }
            //&client_secret ={ app - secret }
            //&code ={ code - parameter}

            String host = HttpContext.Current.Request.UserHostName;
            String baseUrl = "https://graph.facebook.com/v3.2/oauth/access_token?";

            String client_id = "client_id=" + ExternalLoginsProvider.FacebookSecret.ClientId;
            String redirect_uri = "&redirect_uri=" + host + "/FacebookLogin/GetExternalAccessToken";
            String client_secret = "&client_secret=" + ExternalLoginsProvider.FacebookSecret.ClientSecret;

            StringBuilder sb = new StringBuilder();

            sb.Append(baseUrl);
            sb.Append(client_id);
            sb.Append(redirect_uri);
            sb.Append(code);

            String address = sb.ToString();
            WebClient wc = new WebClient()
            {
                Proxy = null
            };
            String answer = wc.DownloadString(address);

            FacebookAccessToken fat = JsonConvert.DeserializeObject<FacebookAccessToken>(answer);

            _userAccessToken = fat.access_token;

            return Ok();
        }
        */
    }
}
