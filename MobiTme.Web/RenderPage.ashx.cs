using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MobiTme
{
    /// <summary>
    /// Summary description for RenderPage
    /// </summary>
    public class RenderPage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string pageUrl = context.Request.QueryString["pageID"].ToString() +".ascx";
            context.Response.ContentType = "text/plain";
            string htmlString = RenderHtml(pageUrl);
            context.Response.Write(htmlString);
        }

        private string RenderHtml(string controlName)
        {
            Page page = new Page();
            Control control = page.LoadControl(controlName);
            HtmlForm form = new HtmlForm();
            form.Controls.Add(control);
            page.Controls.Add(form);
            StringWriter writer = new StringWriter();
            HttpContext.Current.Server.Execute(page, writer, false);
            return writer.ToString();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}