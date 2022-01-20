Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Public Class MainViewModel
    Inherits ViewModelBase
    Private _InstantFeedbackSource As XPInstantFeedbackView

    Public ReadOnly Property InstantFeedbackSource As XPInstantFeedbackView
        Get
            If _InstantFeedbackSource Is Nothing Then
                Dim properties = New ServerViewProperty() {
            New ServerViewProperty("Oid", SortDirection.Ascending, New OperandProperty("Oid")),
            New ServerViewProperty("Subject", SortDirection.None, New OperandProperty("Subject")),
            New ServerViewProperty("UserId", SortDirection.None, New OperandProperty("UserId")),
            New ServerViewProperty("Created", SortDirection.None, New OperandProperty("Created")),
            New ServerViewProperty("Votes", SortDirection.None, New OperandProperty("Votes")),
            New ServerViewProperty("Priority", SortDirection.None, New OperandProperty("Priority"))
                }
                _InstantFeedbackSource = New XPInstantFeedbackView(GetType(Issues.Issue), properties, Nothing)
                AddHandler _InstantFeedbackSource.ResolveSession, Sub(o, e) e.Session = New Session()
            End If
            Return _InstantFeedbackSource
        End Get
    End Property
    Private _Users As System.Collections.IList

    Public ReadOnly Property Users As System.Collections.IList
        Get
            If _Users Is Nothing AndAlso Not IsInDesignMode Then
                Dim session = New DevExpress.Xpo.Session()
                _Users = session.Query(Of XPOIssues.Issues.User).OrderBy(Function(user) user.Oid).[Select](Function(user) New With {
                    .Id = user.Oid,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class