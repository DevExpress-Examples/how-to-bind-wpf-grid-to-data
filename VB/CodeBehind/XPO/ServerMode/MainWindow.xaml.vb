Imports XPOIssues.Issues
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo
Imports System.Linq
Imports DevExpress.Xpf.Grid
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim properties = New ServerViewProperty() {
            New ServerViewProperty("Subject", SortDirection.None, New OperandProperty("Subject")),
            New ServerViewProperty("UserId", SortDirection.None, New OperandProperty("UserId")),
            New ServerViewProperty("Created", SortDirection.None, New OperandProperty("Created")),
            New ServerViewProperty("Votes", SortDirection.None, New OperandProperty("Votes")),
            New ServerViewProperty("Priority", SortDirection.None, New OperandProperty("Priority")),
            New ServerViewProperty("Oid", SortDirection.Ascending, New OperandProperty("Oid"))
        }
        Dim session = New Session()
        Dim source = New XPServerModeView(session, GetType(Issue), Nothing)
        source.Properties.AddRange(properties)
        grid.ItemsSource = source
        LoadLookupData()
    End Sub

    Private Sub LoadLookupData()
        Dim session = New Session()
        usersLookup.ItemsSource = session.Query(Of User).OrderBy(Function(user) user.Oid).[Select](Function(user) New With {
            .Id = user.Oid,
            .Name = user.FirstName & " " + user.LastName
        }).ToArray()
    End Sub

End Class
