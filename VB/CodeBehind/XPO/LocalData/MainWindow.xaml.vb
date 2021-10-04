Imports System.Linq
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Refresh()
    End Sub
    Private _UnitOfWork As DevExpress.Xpo.UnitOfWork

    Private Sub Refresh()
        _UnitOfWork = New DevExpress.Xpo.UnitOfWork()
        Dim xpCollection = New DevExpress.Xpo.XPCollection(Of Issues.User)(_UnitOfWork)
        xpCollection.Sorting.Add(New DevExpress.Xpo.SortProperty(NameOf(Issues.User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending))
        grid.ItemsSource = xpCollection
    End Sub

End Class
