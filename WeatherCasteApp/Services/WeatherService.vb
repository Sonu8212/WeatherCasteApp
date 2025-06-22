' Services/WeatherService.vb
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json.Linq

Public Class WeatherService
    Private ReadOnly _httpClient As HttpClient
    Private ReadOnly _cache As Dictionary(Of String, List(Of WeatherForecast))
    Private ReadOnly _dataAccess As New ForecastDataAccess()

    Public Sub New()
        _httpClient = New HttpClient()
        _cache = New Dictionary(Of String, List(Of WeatherForecast))()
    End Sub

    Public Async Function GetForecastAsync(latitude As Double, longitude As Double, locationId As Integer, userId As Integer, locationName As String, UserName As String) As Task(Of List(Of WeatherForecast))
        Dim cacheKey As String = $"{latitude},{longitude}"
        If _cache.ContainsKey(cacheKey) Then
            Return _cache(cacheKey)
        End If

        Dim url As String = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m&timezone=auto"
        Dim response As HttpResponseMessage = Await _httpClient.GetAsync(url)
        response.EnsureSuccessStatusCode()

        Dim json As String = Await response.Content.ReadAsStringAsync()
        Console.WriteLine("API Response: " & json) ' Debug output
        Dim data As JObject = JObject.Parse(json)
        Dim forecasts As New List(Of WeatherForecast)()

        Dim hourlyTime = data("hourly")("time")
        Dim hourlyTemp = data("hourly")("temperature_2m")

        If hourlyTime IsNot Nothing AndAlso hourlyTemp IsNot Nothing Then
            Dim currentDate As Date = Date.Parse(hourlyTime(0).ToString()).Date
            Dim dailyTempSum As Double = 0
            Dim count As Integer = 0

            For i As Integer = 0 To hourlyTime.Count - 1
                Dim time As Date = Date.Parse(hourlyTime(i).ToString())
                Dim temp As Double
                If Double.TryParse(hourlyTemp(i).ToString(), temp) Then
                    If time.Date = currentDate Then
                        dailyTempSum += temp
                        count += 1
                    Else
                        If count > 0 Then
                            forecasts.Add(New WeatherForecast With {
                                .ForecastDate = currentDate,
                                .Temperature = Math.Round(dailyTempSum / count, 1),
                                .Description = $"{locationName} - {UserName}"
                            })
                        End If
                        currentDate = time.Date
                        dailyTempSum = temp
                        count = 1
                    End If
                End If
            Next

            If count > 0 Then
                forecasts.Add(New WeatherForecast With {
                    .ForecastDate = currentDate,
                    .Temperature = Math.Round(dailyTempSum / count, 1),
                    .Description = $"{locationName} - {UserName}"
                })
            End If
        Else
            Throw New Exception("No valid hourly forecast data in API response.")
        End If

        ' Save to Access database with UserId
        _dataAccess.SaveForecasts(userId, locationId, forecasts)
        _cache(cacheKey) = forecasts
        Return forecasts
    End Function

End Class