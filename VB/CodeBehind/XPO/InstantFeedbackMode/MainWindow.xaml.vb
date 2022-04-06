Imports XPOIssues.Issues
Imports DevExpress.Xpo
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim properties = New ServerViewProperty() {
        New ServerViewProperty("Subject", SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Subject")),
        New ServerViewProperty("UserId", SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("UserId")),
        New ServerViewProperty("Created", SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Created")),
        New ServerViewProperty("Votes", SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Votes")),
        New ServerViewProperty("Priority", SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Priority")),
        New ServerViewProperty("Oid", SortDirection.Ascending, New DevExpress.Data.Filtering.OperandProperty("Oid"))
        }
        Dim source = New XPInstantFeedbackView(GetType(Issue), properties, Nothing)
        AddHandler source.ResolveSession, Sub(o, e) e.Session = New Session()
        grid.ItemsSource = source
        LoadLookupData()
    End Sub

    Private Sub LoadLookupData()
        Dim session = New DevExpress.Xpo.Session()
        usersLookup.ItemsSource = session.Query(Of XPOIssues.Issues.User).OrderBy(Function(user) user.Oid).[Select](Function(user) New With {
            .Id = user.Oid,
            .Name = user.FirstName & " " + user.LastName
        }).ToArray()
    End Sub

End Class
