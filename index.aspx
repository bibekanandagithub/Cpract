<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="fileupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi File Upload With Progress Bar | YogiHosting Demo</title>
    <link rel="icon" type="image/png" href="http://www.yogihosting.com/wp-content/themes/yogi-yogihosting/Images/favicon.ico" />
    <script src="JS/jquery.min.js"></script>
    <script>
        var counter;
        function UploadFile() {
            var files = $("#<%=file1.ClientID%>").get(0).files;
            counter = 0;
            // Loop through files
            for (var i = 0; i < files.length ; i++) {
                var file = files[i];
                var formdata = new FormData();
                formdata.append("file1", file);
                var ajax = new XMLHttpRequest();

                ajax.upload.addEventListener("progress", progressHandler, false);
                ajax.addEventListener("load", completeHandler, false);
                ajax.addEventListener("error", errorHandler, false);
                ajax.addEventListener("abort", abortHandler, false);
                ajax.open("POST", "FileUploadHandler.ashx");
                ajax.send(formdata);
            }
        }
        function progressHandler(event) {
            $("#loaded_n_total").html("Uploaded " + event.loaded + " bytes of " + event.total);
            var percent = (event.loaded / event.total) * 100;
            $("#progressBar").val(Math.round(percent));
            $("#status").html(Math.round(percent) + "% uploaded... please wait");
        }
        function completeHandler(event) {
            counter++
            $("#status").html(counter + " " + event.target.responseText);
        }
        function errorHandler(event) {
            $("#status").html("Upload Failed");
        } function abortHandler(event) {
            $("#status").html("Upload Aborted");
        }
    </script>
    <style>
        body {
            background: #111 no-repeat;
            background-image: -webkit-gradient(radial, 50% 0, 150, 50% 0, 300, from(#444), to(#111));
        }

        h1, h2 {
            text-align: center;
            color: #FFF;
        }

            h2 a {
                color: #0184e3;
                text-decoration: none;
            }

                h2 a:hover {
                    text-decoration: underline;
                }

        .wrapper {
            width: 960px;
            margin: 0 auto;
            background-color: #FFF;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Multi File Upload With Progress Bar</h1>
        <h2><a href="http://www.yogihosting.com/multi-file-upload-with-progress-bar-in-asp-net/">Read the tutorial on YogiHosting »</a></h2>
        <div class="wrapper">
            <asp:FileUpload ID="file1" runat="server" AllowMultiple="true" /><br>
            <input type="button" value="Upload File" onclick="UploadFile()" />
            <progress id="progressBar" value="0" max="100" style="width: 300px;"></progress>
            <h3 id="status">
                
            </h3>
            <p id="loaded_n_total"></p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        </div>
    </form>
</body>
</html>
