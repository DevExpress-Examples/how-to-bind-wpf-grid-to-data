Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.Entity

Public Class MainViewModel
    Inherits ViewModelBase
    Private _InstantFeedbackSource As DevExpress.Data.Linq.EntityInstantFeedbackSource

    Public ReadOnly Property InstantFeedbackSource As DevExpress.Data.Linq.EntityInstantFeedbackSource
        Get
            If _InstantFeedbackSource Is Nothing Then
                _InstantFeedbackSource = New DevExpress.Data.Linq.EntityInstantFeedbackSource With {
                    .KeyExpression = NameOf(Issues.Issue.Id)
                }
                AddHandler _InstantFeedbackSource.GetQueryable, Sub(sender, e)
                                                                    Dim context = New Issues.IssuesContext()
                                                                    e.QueryableSource = context.Issues.AsNoTracking()
                                                                End Sub
            End If
            Return _InstantFeedbackSource
        End Get
    End Property

End Class