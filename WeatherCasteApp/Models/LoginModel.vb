' Models/LoginModel.vb
Imports System.ComponentModel.DataAnnotations

Public Class LoginModel
    <Required(ErrorMessage:="Username is required.")>
    Public Property Username As String

    <Required(ErrorMessage:="Password is required.")>
    Public Property Password As String
End Class