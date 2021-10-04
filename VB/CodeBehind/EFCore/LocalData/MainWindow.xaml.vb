Imports System.Linq
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Refresh()
    End Sub
    Private _Context As Issues.IssuesContext

    Private Sub Refresh()
        _Context = New Issues.IssuesContext()
        grid.ItemsSource = _Context.Users.ToList()
    End Sub

End Class
