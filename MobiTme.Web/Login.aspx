<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MobiTme.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <script src="Scripts/jquery-2.0.3.min.js"></script>
    <link href="Styles/Login.css" rel="stylesheet" />



    <script type="text/javascript">


        $(document).ready(function () {
            $('#btnLogin').click(function (e) {
                login();
            });
        });

        function login() {
            var username = $('#txtUsername').val();
            var password = $('#txtPassword').val();
            var rememberMe = $("#remember_me").prop('checked');
            if (username.trim() == '') {
                alert('Please enter username.');
                return;
            }

            if (password.trim() == '') {
                alert('Please enter password.');
                return;
            }
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/LoginUser",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'username': '" + username + "', 'password':'" + password + "' , 'rememberMe':'" + rememberMe + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        if (data == "0") {
                            alert('wrong username or password')
                        } else {
                            // login successfull call parent to swap to gse 
                            parent.slideTo('Map', false);
                        }
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
         

        <div id="doc">

            <div id="page-outer">
                <div class="front-container front-container-full-signup  " id="front-container">
                    <noscript>
        <div class="front-warning">
          <h3> MobiTime makes heavy use of JavaScript</h3>
          <p> Please enable your browsers javascript.</p>
        </div>
        </noscript>
                    <div class="front-card">

                        <img src="Styles/Images/welcome.png" style="margin-left: -130px; padding-left: -120px; padding-top: 40px; margin-top: 40px;">
                        <div class="front-welcome">
                            <div class="front-welcome-text"></div>
                        </div>
                        <div class="front-signin js-front-signin">
                            <div class="placeholding-input username">
                                <input type="text" class="text-input email-input" name="txtUsername" id="txtUsername"
                                    value="musa.sithole@mobitime.co.za" title="Username or email">
                            </div>
                            <table class="flex-table password-signin">
                                <tbody>
                                    <tr>
                                        <td class="flex-table-primary">
                                            <div class="placeholding-input password flex-table-form">
                                                <input type="password" class="text-input flex-table-input" id="txtPassword" value="2mice2fish">
                                            </div>
                                        </td>
                                        <td class="flex-table-secondary">
                                            <button id="btnLogin" type="button" class="submit btn primary-btn flex-table-btn js-submit" tabindex="4">
                                                log in
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="remember-forgot">
                                <label class="remember" style="display: none">
                                    <input type="checkbox" value="1" id="remember_me" name="remember_me" tabindex="3">
                                    <span>Remember me</span>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
