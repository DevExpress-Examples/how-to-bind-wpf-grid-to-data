Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.Entity
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim source = New DevExpress.Data.Linq.EntityInstantFeedbackSource With {
            .KeyExpression = NameOf(Issues.Issue.Id)
        }
        AddHandler source.GetQueryable, Sub(sender, e)
                                            Dim context = New Issues.IssuesContext()
                                            e.QueryableSource = context.Issues.AsNoTracking()
                                        End Sub
        grid.ItemsSource = source
    End Sub

End Class
