<%@ Page Language="c#" Inherits="System.Web.UI.Page" CodePage="65001" %>
<%@ OutputCache Location="None" VaryByParam="none" %>

<%@ Import namespace="System.Reflection" %>
<%@ Import namespace="Sitecore.Mvc" %>
<%@ Import namespace="Tealium.Sitecore.TagManagement" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

  <title>Welcome to Sitecore 4</title>
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta name="CODE_LANGUAGE" content="C#" />
  <meta name="vs_defaultClientScript" content="JavaScript" />
  <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
  <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300italic,400italic,600italic,700italic,300,600,700,800" rel="stylesheet" type="text/css" />
  <link href="/default.css" rel="stylesheet" />
  <sc:VisitorIdentification runat='server'/> 

  </head>
<body>

<% 
   var tealiumManager = TealiumFactory.TealiumManager;
   Response.Write( tealiumManager.BodyInjections().ToString() );
%>

  <form id="mainform" method="post" runat="server">
    <div id="MainPanel">
      <sc:placeholder key="main" runat="server" /> 
    </div>
  </form>
 </body>
</html>

