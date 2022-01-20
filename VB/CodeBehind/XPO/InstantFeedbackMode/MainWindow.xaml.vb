Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim properties = New ServerViewProperty() {
        New ServerViewProperty("Oid", SortDirection.Ascending, New OperandProperty("Oid")),
        New ServerViewProperty("Subject", SortDirection.None, New OperandProperty("Subject")),
        New ServerViewProperty("UserId", SortDirection.None, New OperandProperty("UserId")),
        New ServerViewProperty("Created", SortDirection.None, New OperandProperty("Created")),
        New ServerViewProperty("Votes", SortDirection.None, New OperandProperty("Votes")),
        New ServerViewProperty("Priority", SortDirection.None, New OperandProperty("Priority"))
        }
        Dim source = New XPInstantFeedbackView(GetType(Issues.Issue), properties, Nothing)
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
