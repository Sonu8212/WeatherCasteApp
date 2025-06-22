Imports CsvHelper
Imports System.IO
Public Class CsvService
    Public Function ParseCsv(file As HttpPostedFileBase) As List(Of Location)
        Try
            Using reader As New StreamReader(file.InputStream)
                Using csv As New CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture)
                    Return csv.GetRecords(Of Location)().ToList()
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception("Invalid CSV format.", ex)
        End Try
    End Function
End Class
