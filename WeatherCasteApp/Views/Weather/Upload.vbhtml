@Code       ViewData("Title") = "Upload CSV"
End Code
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-5">
        <h2>Upload Locations CSV</h2>
        @Using Html.BeginForm("Upload", "Weather", FormMethod.Post, New With {.enctype = "multipart/form-data", .class = "form-group"})
            @Html.AntiForgeryToken()
            @<div class="form-group">
                <label for="file">Select CSV File</label>
                <input type="file" name="file" id="file" class="form-control-file" accept=".csv" />
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
            </div>

 
          @<button type="submit" class="btn btn-primary">Upload</button>
        End Using
    </div>
</body>
</html>