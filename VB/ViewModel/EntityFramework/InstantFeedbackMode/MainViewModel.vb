Imports DevExpress.Mvvm
Imports EntityFrameworkIssues.Issues
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Data.Linq
Imports System.Data.Entity
Imports System.Linq
Imports System.Collections

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ItemsSource As EntityInstantFeedbackSource
    Public ReadOnly Property ItemsSource As EntityInstantFeedbackSource
        Get
            If _ItemsSource Is Nothing Then
                _ItemsSource = New EntityInstantFeedbackSource With {
                    .KeyExpression = NameOf(Issue.Id)
                }
                AddHandler _ItemsSource.GetQueryable, Sub(sender, e)
                                                          Dim context = New IssuesContext()
                                                          e.QueryableSource = context.Issues.AsNoTracking()
                                                      End Sub
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