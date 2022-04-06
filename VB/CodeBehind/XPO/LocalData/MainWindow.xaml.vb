Imports XPOIssues.Issues
Imports DevExpress.Xpo
Imports System.Linq
Class MainWindow
    Public Sub New()
        InitializeComponent()
        LoadData()
    End Sub
    Private _UnitOfWork As UnitOfWork

    Private Sub LoadData()
        _UnitOfWork = New UnitOfWork()
        Dim xpCollection = New XPCollection(Of User)(_UnitOfWork)
        xpCollection.Sorting.Add(New SortProperty(NameOf(User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending))
        grid.ItemsSource = xpCollection
    End Sub

End Class
