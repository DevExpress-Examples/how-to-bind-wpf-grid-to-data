Imports DevExpress.Mvvm
Imports EFCoreIssues.Issues
Imports Microsoft.EntityFrameworkCore
Imports DevExpress.Mvvm.DataAnnotations
Imports System.Linq
Imports System.Collections.Generic

Public Class MainViewModel
    Inherits ViewModelBase
    Private _Context As IssuesContext
    Private _ItemsSource As IList(Of User)
    Public ReadOnly Property ItemsSource As IList(Of User)
        Get
            If _ItemsSource Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                _Context = New IssuesContext()
                _ItemsSource = _Context.Users.ToList()
            End If
            Return _ItemsSource
        End Get
    End Property

End Class