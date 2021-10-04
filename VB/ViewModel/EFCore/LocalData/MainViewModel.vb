Imports DevExpress.Mvvm
Imports System.Linq

Public Class MainViewModel
    Inherits ViewModelBase
    Private _Context As Issues.IssuesContext
    Private _ItemsSource As System.Collections.Generic.IList(Of EFCoreIssues.Issues.User)

    Public ReadOnly Property ItemsSource As System.Collections.Generic.IList(Of EFCoreIssues.Issues.User)
        Get
            If _ItemsSource Is Nothing AndAlso Not IsInDesignMode Then
                _Context = New Issues.IssuesContext()
                _ItemsSource = _Context.Users.ToList()
            End If
            Return _ItemsSource
        End Get
    End Property

End Class