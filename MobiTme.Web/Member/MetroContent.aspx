<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetroContent.aspx.cs" Inherits="MobiTme.Member.MetroContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-2.0.3.min.js"></script>
    <script src="../Scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            fixLayout();
        });
        
        function getPageSize() {
            var width = $(document).width();
            var height = $(document).height();
            return { width: width, height: height };
        }
        
        function fixLayout()
        {
            var parentSize = parent.getPageSize();
            $('#framMetroContent').css('width', parentSize.width);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <iframe runat="server" data-index="1" id="framMetroContent" data-pageurl="" class="mainIframe  active" src="" frameborder="0" scrolling="yes"></iframe>

        </div>
    </form>
</body>
</html>
