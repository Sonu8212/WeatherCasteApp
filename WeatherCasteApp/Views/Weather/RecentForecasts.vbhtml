@ModelType List(Of WebApplication3.WeatherForecast) 
@Code
    ViewData("Title") = "Recent Forecasts"
End Code

<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
</head>
<body>
    <h2>Recent Forecasts</h2>
    @If Model IsNot Nothing AndAlso Model.Count > 0 Then
@<table>
    <tr><th>Date</th><th>Temperature</th><th>Description</th></tr>
@For Each forecast In Model
            @<tr>
                <td>@forecast.ForecastDate.ToString("yyyy-MM-dd")</td>
                <td>@forecast.Temperature &deg;C</td>
                <td>@forecast.Description</td>
            </tr>
        Next
        </table>
    Else
        @<p>No recent forecasts available.</p>
    End If
    <a href="@Url.Action("Upload", "Weather")">Back to Upload</a>
</body>
</html>