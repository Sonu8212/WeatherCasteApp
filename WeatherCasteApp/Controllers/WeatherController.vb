' Controllers/WeatherController.vb
Imports System.Threading.Tasks
Imports System.Web.Mvc

Public Class WeatherController
    Inherits Controller
    Private ReadOnly _csvService As New CsvService()
    Private ReadOnly _weatherService As New WeatherService()

    Public Function Upload() As ActionResult
        If Not GlobalUserStore.CurrentUserId.HasValue Then
            Return RedirectToAction("Login", "Account")
        End If
        Return View()
    End Function

    <HttpPost>
    Public Async Function Upload(file As HttpPostedFileBase) As Task(Of ActionResult)
        If Not GlobalUserStore.CurrentUserId.HasValue Then
            Return RedirectToAction("Login", "Account")
        End If

        If file Is Nothing OrElse file.ContentLength = 0 Then
            ModelState.AddModelError("", "Please upload a valid CSV file.")
            Return View()
        End If

        Try
            Dim userId = GlobalUserStore.CurrentUserId.Value
            Dim locations As List(Of Location) = _csvService.ParseCsv(file)
            Dim viewModels As New List(Of WeatherViewModel)()

            For i As Integer = 0 To locations.Count - 1
                Dim location = locations(i)
                Dim forecasts As List(Of WeatherForecast) = Await _weatherService.GetForecastAsync(location.Latitude, location.Longitude, i, userId, location.LocationName, GlobalUserStore.CurrentUsername)
                viewModels.Add(New WeatherViewModel With {
                    .Location = location,
                    .Forecasts = forecasts
                })
            Next

            Return View("Forecast", viewModels)
        Catch ex As Exception
            ModelState.AddModelError("", $"Error processing file: {ex.Message}")
            Return View()
        End Try
    End Function

    Public Function RecentForecasts() As ActionResult
        If Not GlobalUserStore.CurrentUserId.HasValue Then
            Return RedirectToAction("Login", "Account")
        End If
        Dim dataAccess As New ForecastDataAccess()
        Dim recentForecast = dataAccess.LoadRecentForecasts()
        Return View(recentForecast)
    End Function
End Class