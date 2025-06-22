' Services/ForecastDataAccess.vb
Imports System.Data.OleDb

Public Class ForecastDataAccess
    ' Private ReadOnly _connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Forecasts.accdb;Persist Security Info=False;"

    Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("AccessConnection").ConnectionString

    Public Function ValidateUser(username As String, password As String) As Integer?
        Using conn As New OleDbConnection(_connectionString)
            conn.Open()
            Dim cmd As New OleDbCommand("SELECT Id FROM [User] WHERE Username = ? AND Password = ?", conn)
            cmd.Parameters.AddWithValue("?", username)
            cmd.Parameters.AddWithValue("?", password) ' Note: Use hashing in production
            Dim result = cmd.ExecuteScalar()
            conn.Close()
            Return If(result IsNot DBNull.Value, Convert.ToInt32(result), Nothing)
        End Using
    End Function

    Public Class ForecastDataAccess
        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("AccessConnection").ConnectionString

        Public Function ValidateUser(username As String, password As String) As Integer?
            Using conn As New OleDbConnection(_connectionString)
                conn.Open()
                Dim cmd As New OleDbCommand("SELECT Id FROM [User] WHERE Username = ? AND Password = ?", conn)
                cmd.Parameters.AddWithValue("?", username)
                cmd.Parameters.AddWithValue("?", password) ' Note: Use hashing in production
                Dim result = cmd.ExecuteScalar()
                conn.Close()
                Return If(result IsNot DBNull.Value, Convert.ToInt32(result), Nothing)
            End Using
        End Function
    End Class

    Public Sub SaveForecasts(userId As Integer, locationId As Integer, forecasts As List(Of WeatherForecast))
        Using conn As New OleDbConnection(_connectionString)
            conn.Open()
            Dim cmd As New OleDbCommand("DELETE FROM Forecasts WHERE UserId = @UserId AND LocationId = @LocationId", conn)
            cmd.Parameters.AddWithValue("@UserId", userId)
            cmd.Parameters.AddWithValue("@LocationId", locationId)
            cmd.ExecuteNonQuery()

            Dim insertCmd As String = "INSERT INTO Forecasts (UserId, LocationId, ForecastDate, Temperature, Description) VALUES (@UserId, @LocationId, @ForecastDate, @Temperature, @Description)"
            Using insertCmdObj As New OleDbCommand(insertCmd, conn)
                For Each forecast In forecasts
                    insertCmdObj.Parameters.Clear()
                    insertCmdObj.Parameters.AddWithValue("@UserId", userId)
                    insertCmdObj.Parameters.AddWithValue("@LocationId", locationId)
                    insertCmdObj.Parameters.AddWithValue("@ForecastDate", forecast.ForecastDate)
                    insertCmdObj.Parameters.AddWithValue("@Temperature", forecast.Temperature)
                    insertCmdObj.Parameters.AddWithValue("@Description", If(forecast.Description, DBNull.Value))
                    insertCmdObj.ExecuteNonQuery()
                Next
            End Using
            conn.Close()
        End Using
    End Sub

    Public Function LoadRecentForecasts() As List(Of WeatherForecast)
        Dim forecasts As New List(Of WeatherForecast)()
        Using conn As New OleDbConnection(_connectionString)
            conn.Open()
            Dim cmd As New OleDbCommand("SELECT UserId, LocationId, ForecastDate, Temperature, Description FROM Forecasts ORDER BY ForecastDate DESC", conn)
            Using reader As OleDbDataReader = cmd.ExecuteReader()
                While reader.Read()
                    forecasts.Add(New WeatherForecast With {
                        .UserId = Convert.ToInt32(reader("UserId")),
                        .LocationId = Convert.ToInt32(reader("LocationId")),
                        .ForecastDate = Convert.ToDateTime(reader("ForecastDate")),
                        .Temperature = Convert.ToDouble(reader("Temperature")),
                        .Description = If(reader("Description") Is DBNull.Value, Nothing, reader("Description").ToString())
                    })
                End While
            End Using
            conn.Close()
        End Using
        Return forecasts
    End Function
End Class