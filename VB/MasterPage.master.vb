Imports System
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Web
Imports System.IO

Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim collection As SiteMapNodeCollection = siteMapDS.Provider.GetChildNodes(siteMapDS.Provider.RootNode)
        pageControl.TabPages.Clear()
        For Each item As SiteMapNode In collection
            Dim tabPage As New TabPage()
            tabPage.Text = item.Title
            tabPage.TabTemplate = New CustomTabTemplate(item)
            pageControl.TabPages.Add(tabPage)
        Next item
    End Sub

    Public Class CustomTabTemplate
        Implements ITemplate

        Public Property currentNode() As SiteMapNode

        Public Sub New(ByVal item As SiteMapNode)
            currentNode = item
        End Sub
        Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
            Dim hyperLink As New ASPxHyperLink()
            hyperLink.ID = "hl" & currentNode.Title
            container.Controls.Add(hyperLink)
            hyperLink.NavigateUrl = currentNode.Url
            hyperLink.Text = currentNode.Title
        End Sub
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        pageControl.ActiveTabPage = pageControl.TabPages.FindByText(Path.GetFileNameWithoutExtension(HttpContext.Current.Request.Url.AbsolutePath) & ".aspx")
    End Sub
End Class
