Imports EFCoreIssues.Issues
Imports Microsoft.EntityFrameworkCore
Imports System.Linq
Class MainWindow
    Public Sub New()
        InitializeComponent()
        LoadData()
    End Sub
    Private _Context As IssuesContext

    Private Sub LoadData()
        _Context = New IssuesContext()
        grid.ItemsSource = _Context.Users.ToList()
    End Sub

End Class
