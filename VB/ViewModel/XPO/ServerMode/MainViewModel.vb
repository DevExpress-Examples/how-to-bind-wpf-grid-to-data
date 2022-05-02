Imports DevExpress.Mvvm
Imports XPOIssues.Issues
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo
Imports System.Linq
Imports System.Collections

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ItemsSource As XPServerModeView
    Public ReadOnly Property ItemsSource As XPServerModeView
        Get
            If _ItemsSource Is Nothing Then
                Dim properties = New ServerViewProperty() {
            New ServerViewProperty("Subject", SortDirection.None, New OperandProperty("Subject")),
            New ServerViewProperty("UserId", SortDirection.None, New OperandProperty("UserId")),
            New ServerViewProperty("Created", SortDirection.None, New OperandProperty("Created")),
            New ServerViewProperty("Votes", SortDirection.None, New OperandProperty("Votes")),
            New ServerViewProperty("Priority", SortDirection.None, New OperandProperty("Priority")),
            New ServerViewProperty("Oid", SortDirection.Ascending, New OperandProperty("Oid"))
                }
                Dim session = New Session()
                _ItemsSource = New XPServerModeView(session, GetType(Issue), Nothing)
                _ItemsSource.Properties.AddRange(properties)
            End If

            Return _ItemsSource
        End Get
    End Property
    Private _Users As IList
    Public ReadOnly Property Users As IList
        Get
            If _Users Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                Dim session = New Session()
                _Users = session.Query(Of User).OrderBy(Function(user) user.Oid).[Select](Function(user) New With {
                    .Id = user.Oid,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class