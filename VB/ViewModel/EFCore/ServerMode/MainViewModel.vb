Imports DevExpress.Mvvm
Imports EFCoreIssues.Issues
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Data.Linq
Imports Microsoft.EntityFrameworkCore
Imports System.Linq
Imports System.Collections

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ItemsSource As EntityServerModeSource
    Public ReadOnly Property ItemsSource As EntityServerModeSource
        Get
            If _ItemsSource Is Nothing Then
                Dim context = New IssuesContext()
                _ItemsSource = New EntityServerModeSource With {
                    .KeyExpression = NameOf(Issue.Id),
                    .QueryableSource = context.Issues.AsNoTracking()
                }
            End If
            Return _ItemsSource
        End Get
    End Property
    Private _Users As IList
    Public ReadOnly Property Users As IList
        Get
            If _Users Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                Dim context = New IssuesContext()
                _Users = context.Users.[Select](Function(user) New With {
                    .Id = user.Id,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class