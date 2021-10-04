Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo

Public Class MainViewModel
    Inherits ViewModelBase
    Private _InstantFeedbackSource As DevExpress.Xpo.XPInstantFeedbackView

    Public ReadOnly Property InstantFeedbackSource As DevExpress.Xpo.XPInstantFeedbackView
        Get
            If _InstantFeedbackSource Is Nothing Then
                Dim properties = New DevExpress.Xpo.ServerViewProperty() {
            New DevExpress.Xpo.ServerViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, New DevExpress.Data.Filtering.OperandProperty("Oid")),
            New DevExpress.Xpo.ServerViewProperty("Subject", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Subject")),
            New DevExpress.Xpo.ServerViewProperty("UserId", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("UserId")),
            New DevExpress.Xpo.ServerViewProperty("Created", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Created")),
            New DevExpress.Xpo.ServerViewProperty("Votes", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Votes")),
            New DevExpress.Xpo.ServerViewProperty("Priority", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Priority"))
                }
                _InstantFeedbackSource = New DevExpress.Xpo.XPInstantFeedbackView(GetType(Issues.Issue), properties, Nothing)
                AddHandler _InstantFeedbackSource.ResolveSession, Sub(o, e) e.Session = New DevExpress.Xpo.Session()
            End If
            Return _InstantFeedbackSource
        End Get
    End Property

End Class