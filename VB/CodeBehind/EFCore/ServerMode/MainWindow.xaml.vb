Imports EFCoreIssues.Issues
Imports DevExpress.Data.Linq
Imports Microsoft.EntityFrameworkCore
Imports System.Linq
Imports DevExpress.Xpf.Grid
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim context = New IssuesContext()
        Dim source = New EntityServerModeSource With {
            .KeyExpression = NameOf(Issue.Id),
            .QueryableSource = context.Issues.AsNoTracking()
        }
        grid.ItemsSource = source
        LoadLookupData()
    End Sub

    Private Sub LoadLookupData()
        Dim context = New IssuesContext()
        usersLookup.ItemsSource = context.Users.[Select](Function(user) New With {
            .Id = user.Id,
            .Name = user.FirstName & " " + user.LastName
        }).ToArray()
    End Sub

End Class
