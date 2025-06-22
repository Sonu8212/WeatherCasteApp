@ModelType WebApplication3.LoginModel 
@Code
    ViewData("Title") = "Login"
End Code

<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-5">
        <h2>Login</h2>
        @Using Html.BeginForm()
            @Html.AntiForgeryToken()
            @If Not ViewData.ModelState.IsValid Then
                @<div class="alert alert-danger">
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                </div>
            End If
            @<div class="form-group">
    @Html.LabelFor(Function(m) m.Username, New With {.class = "control-label"})
    @Html.TextBoxFor(Function(m) m.Username, New With {.class = "form-control", .autofocus = "autofocus"})
    @Html.ValidationMessageFor(Function(m) m.Username, "", New With {.class = "text-danger"})
                </div>
               @<div class="form-group">
    @Html.LabelFor(Function(m) m.Password, New With {.class = "control-label"})
    @Html.PasswordFor(Function(m) m.Password, New With {.class = "form-control"})
    @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger"})
                </div>
                @<button type = "submit" class="btn btn-primary">Login</button>
    @If GlobalUserStore.CurrentUserId.HasValue Then
    @<a href="@Url.Action("Logout", "Account")" class="btn btn-secondary ml-2">Logout</a>
                End If
    End Using
    </div>

    @Section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section
</body>
</html>