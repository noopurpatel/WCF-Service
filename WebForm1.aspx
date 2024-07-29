<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Module7_WCF.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <script type="text/javascript">
        function GetMovieList() {
            MovieService.GetAllMovies(onSuccess);
        }

        function GetMovie() {
            debugger
            let movieId = document.getElementById("txtPayload").value;
            MovieService.GetMovieDetail(movieId,onSuccess);
        }

    function onSuccess(result) {
        var res = $get("result");
        res.innerHTML = result;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="payload">Payload:</label>
            <input type="text" id="txtPayload" name="payload" size="10" />
        </div>

        <input type="button" value="Get Movies" onclick="GetMovieList()"/>
        <span><input type="button" value="Get Movie" onclick="GetMovie()"/></span>
        <div>    
     </div>    
<br />
<span id="result"></span>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/MovieService.svc" />
            </Services>
        </asp:ScriptManager>
    </form>
</body>
</html>
