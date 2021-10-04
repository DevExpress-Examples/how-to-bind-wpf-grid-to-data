Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim context = New Issues.IssuesContext()
        Dim source = New DevExpress.Data.Linq.EntityServerModeSource With {
            .KeyExpression = NameOf(Issues.Issue.Id),
            .QueryableSource = context.Issues.AsNoTracking()
        }
        grid.ItemsSource = source
        LoadLookupData()
    End Sub

    Private Sub LoadLookupData()
        Dim context = New EFCoreIssues.Issues.IssuesContext()
        usersLookup.ItemsSource = context.Users.[Select](Function(user) New With {
            .Id = user.Id,
            .Name = user.FirstName & " " + user.LastName
        }).ToArray()
    End Sub

End Class
