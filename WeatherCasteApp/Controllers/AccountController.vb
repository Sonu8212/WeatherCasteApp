' Controllers/AccountController.vb
Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Public Class AccountController
    Inherits Controller
    Private ReadOnly _dataAccess As New ForecastDataAccess()

    Public Function Login() As ActionResult
        If GlobalUserStore.CurrentUserId.HasValue Then
            Return RedirectToAction("Upload", "Weather")
        End If
        Return View()
    End Function

    <HttpPost>
    Public Function Login(model As LoginModel) As ActionResult
        If ModelState.IsValid Then
            Dim userId = _dataAccess.ValidateUser(model.Username, model.Password)
            If userId.HasValue AndAlso userId.Value > 0 Then
                GlobalUserStore.CurrentUserId = userId.Value
                GlobalUserStore.CurrentUsername = model.Username
                ' Check if user is admin (e.g., UserId = 1)
                If userId.Value = 1 Then ' Adjust admin UserId as per your database
                    Return RedirectToAction("RecentForecasts", "Weather")
                Else
                    Return RedirectToAction("Upload", "Weather")
                End If
            Else
                ModelState.AddModelError("", "Invalid username or password.")
            End If
        End If
        Return View(model)
    End Function


    Public Function Logout() As ActionResult
        GlobalUserStore.Clear()
        Return RedirectToAction("Login")
    End Function
End Class

