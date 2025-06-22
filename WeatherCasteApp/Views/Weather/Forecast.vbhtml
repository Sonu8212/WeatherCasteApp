@Code       ViewData("Title") = "Weather Forecast"
    Dim model As List(Of WeatherViewModel) = ViewData.Model
End Code
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-5">
        <h2>Weather Forecast</h2>
        @For Each vm In model
            @<div class="card mb-3">
                <div class="card-header">
                    <h5>@vm.Location.LocationName</h5>
                    <small>Lat: @vm.Location.Latitude, Long: @vm.Location.Longitude</small>
                </div>
                <div class="card-body">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Temperature (°C)</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            @For Each forecast In vm.Forecasts
                                @<tr>
                                    <td>@forecast.ForecastDate.ToShortDateString()</td>
                                    <td>@forecast.Temperature</td>
                                    <td>@forecast.Description</td>
                                </tr>
                            Next
                        </tbody>
                    </table>
                </div>
            </div>
        Next
        <a href="@Url.Action("Upload", "Weather")" class="btn btn-secondary">Back to Upload</a>
    </div>
</body>
</html>