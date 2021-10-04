Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim properties = New DevExpress.Xpo.ServerViewProperty() {
        New DevExpress.Xpo.ServerViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, New DevExpress.Data.Filtering.OperandProperty("Oid")),
        New DevExpress.Xpo.ServerViewProperty("Subject", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Subject")),
        New DevExpress.Xpo.ServerViewProperty("UserId", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("UserId")),
        New DevExpress.Xpo.ServerViewProperty("Created", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Created")),
        New DevExpress.Xpo.ServerViewProperty("Votes", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Votes")),
        New DevExpress.Xpo.ServerViewProperty("Priority", DevExpress.Xpo.SortDirection.None, New DevExpress.Data.Filtering.OperandProperty("Priority"))
        }
        Dim source = New DevExpress.Xpo.XPInstantFeedbackView(GetType(Issues.Issue), properties, Nothing)
        AddHandler source.ResolveSession, Sub(o, e) e.Session = New DevExpress.Xpo.Session()
        grid.ItemsSource = source
    End Sub

End Class