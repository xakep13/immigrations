using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.Web.Core.ExternalLogins
{
    class FacebookShareEntity
    {
        public String app_id { get; set; } // Your app's unique identifier. Required.
        public String redirect_uri { get; set; } // The URL to redirect to after a person clicks a button on the dialog. Required when using URL redirection.
        public String display { get; set; } // Determines how the dialog is rendered.
        /*
            The ID of the person posting the message.
            If this is unspecified, it defaults to the current person.
            If specifie, it must be the ID of the person or of a page that the person administers.
        */
        public String from { get; set; }
        /*
            The ID of the profile that this story will be published to. 
            If this is unspecified, it defaults to the value of from.
            The ID must be a friend who also uses your app.
        */
        public String to { get; set; }
        // The link attached to this post.
        public String link { get; set; }
        /*
            The URL of a media file (either SWF or MP3) attached to this post.
            If SWF, you must also specify picture to provide a thumbnail for the video.
        */
        public String source { get; set; }
        /*
            This argument is a comma-separated list, consisting of at most 5 distinct items,
            each of length at least 1 and at most 15 characters drawn from the set '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_'. 
            Each category is used in Facebook Insights to help you measure the performance of different types of post
        */
        public String @ref { get; set; }

        /* 
                
        If you are using the URL redirect dialog implementation,
        then this will be a full page display, 
        shown within Facebook.com.This display type is called page.

        If you are using one of our iOS or Android SDKs to invoke the dialog,
        this is automatically specified and chooses an appropriate display type for the device.
        If you are using the JavaScript SDK, 
        this will default to a modal iframe type for people logged into your app 
        or async when using within a game on Facebook.com, 
        and a popup window for everyone else. 
        You can also force the popup or page types when using the JavaScript SDK, if necessary.
        Mobile web apps will always default to the touch display type.
        
        */

    }
}
