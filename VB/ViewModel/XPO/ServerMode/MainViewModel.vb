Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ServerModeSource As XPServerModeView

    Public ReadOnly Property ServerModeSource As XPServerModeView
        Get
            If _ServerModeSource Is Nothing Then
                Dim properties = New ServerViewProperty() {
            New ServerViewProperty("Oid", SortDirection.Ascending, New OperandProperty("Oid")),
            New ServerViewProperty("Subject", SortDirection.None, New OperandProperty("Subject")),
            New ServerViewProperty("UserId", SortDirection.None, New OperandProperty("UserId")),
            New ServerViewProperty("Created", SortDirection.None, New OperandProperty("Created")),
            New ServerViewProperty("Votes", SortDirection.None, New OperandProperty("Votes")),
            New ServerViewProperty("Priority", SortDirection.None, New OperandProperty("Priority"))
                }
                Dim session = New Session()
                _ServerModeSource = New XPServerModeView(session, GetType(Issues.Issue), Nothing)
                _ServerModeSource.Properties.AddRange(properties)
            End If

            Return _ServerModeSource
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