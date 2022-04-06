Imports DevExpress.Mvvm
Imports EntityFrameworkIssues.Issues
Imports System.Data.Entity
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ItemsSource As DevExpress.Data.Linq.EntityServerModeSource
    Public ReadOnly Property ItemsSource As DevExpress.Data.Linq.EntityServerModeSource
        Get
            If _ItemsSource Is Nothing Then
                Dim context = New IssuesContext()
                _ItemsSource = New DevExpress.Data.Linq.EntityServerModeSource With {
                    .KeyExpression = NameOf(Issue.Id),
                    .QueryableSource = context.Issues.AsNoTracking()
                }
            End If
            Return _ItemsSource
        End Get
    End Property
    Private _Users As System.Collections.IList
    Public ReadOnly Property Users As System.Collections.IList
        Get
            If _Users Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                Dim context = New EntityFrameworkIssues.Issues.IssuesContext()
                _Users = context.Users.[Select](Function(user) New With {
                    .Id = user.Id,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class