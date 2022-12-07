using System;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        SiteMapNodeCollection collection = siteMapDS.Provider.GetChildNodes(siteMapDS.Provider.RootNode);
        pageControl.TabPages.Clear();
        foreach (SiteMapNode item in collection)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = item.Title;
            tabPage.TabTemplate = new CustomTabTemplate(item);
            pageControl.TabPages.Add(tabPage);
        }
    }

    public class CustomTabTemplate : ITemplate
    {
        public SiteMapNode currentNode { get; set; }

        public CustomTabTemplate(SiteMapNode item)
        {
            currentNode = item;
        }
        public void InstantiateIn(Control container)
        {
            ASPxHyperLink hyperLink = new ASPxHyperLink();
            hyperLink.ID = "hl" + currentNode.Title;
            container.Controls.Add(hyperLink);
            hyperLink.NavigateUrl = currentNode.Url;
            hyperLink.Text = currentNode.Title;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pageControl.ActiveTabPage = pageControl.TabPages.FindByText(Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath) + ".aspx");
    }
}
