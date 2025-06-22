
Public Class GlobalUserStore
    Private Shared _currentUserId As Integer?
    Private Shared _currentUsername As String

    Public Shared Property CurrentUserId As Integer?
        Get
            Return _currentUserId
        End Get
        Set(value As Integer?)
            _currentUserId = value
        End Set
    End Property

    Public Shared Property CurrentUsername As String
        Get
            Return _currentUsername
        End Get
        Set(value As String)
            _currentUsername = value
        End Set
    End Property

    Public Shared Sub Clear()
        _currentUserId = Nothing
        _currentUsername = Nothing
    End Sub
End Class