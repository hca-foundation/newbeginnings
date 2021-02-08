using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNBCSurvey.Service
{
    public class EmailTemplateService
    {
        private static string _EmailTemplate = @"
<html>
<head>
    <style type='text/css'>
        body {
            font-family: avenir-lt-w01_35-light1475496,Arial,Helvetica,sans-serif;
            line-height: 1.5;
        }

        p {
            margin-bottom: 20px;
        }

        .content {
            max-width: 500px;
            margin-left: auto;
            margin-right: auto;
            margin-top: 15px;
        }

        .header-image {
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 195px;
        }
    </style>
</head>
<body>
    <div class='content'>
        <div>
            <img src='https://newbegininingcenter.azurewebsites.net/Content/images/newBeginnings.png' alt='New Beginnings logo' class='header-image' />
        </div>
        <p>
            Greetings from The New Beginnings Center!
        </p>
        <p>
            I hope this finds you healthy and well. We are reaching out to all of our New Beginnings clients and would love to hear how you’re doing. Please take a moment to complete our
            <a href='{surveyLinkUrl}'>New Beginnings Client Follow Up Survey</a>.
        </p>
        <p>
            If you would like a check-in call from a coach, please reply to me via email or phone. We’d be very happy to set up a call!
        </p>
        <p>
            Be well,
            <br />
            Karen
        </p>
        <img src='https://newbegininingcenter.azurewebsites.net/Content/images/newBeginnings-small.png' alt='New Beginnings logo' />
        <div>
            <a href='http://www.thenewbeginningscenter.org'>http://www.thenewbeginningscenter.org</a>
        </div>
    </div>
</body>
</html>
";

        public string getMailBody(string surveyLinkUrl)
        {
            var body = _EmailTemplate.Replace("{surveyLinkUrl}", surveyLinkUrl);
            return body;
        }
    }
}