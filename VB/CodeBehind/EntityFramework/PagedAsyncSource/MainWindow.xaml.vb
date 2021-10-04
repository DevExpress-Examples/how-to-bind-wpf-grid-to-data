Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.Entity
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim source = New PagedAsyncSource With {
            .ElementType = GetType(Issues.Issue),
            .KeyProperty = NameOf(Issues.Issue.Id),
            .PageNavigationMode = PageNavigationMode.ArbitraryWithTotalPageCount
        }
        AddHandler source.FetchPage, AddressOf OnFetchPage
        AddHandler source.GetTotalSummaries, AddressOf OnGetTotalSummaries
        grid.ItemsSource = source
    End Sub

    Private Sub OnFetchPage(ByVal sender As System.Object, ByVal e As DevExpress.Xpf.Data.FetchPageAsyncEventArgs)
        Const pageTakeCount As Integer = 5
        e.Result = Task.Run(Of DevExpress.Xpf.Data.FetchRowsResult)(Function()
                                                                        Dim context = New Issues.IssuesContext()
                                                                        Dim queryable = context.Issues.AsNoTracking().SortBy(e.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issues.Issue.Id)).Where(MakeFilterExpression(CType(e.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                                                        Return queryable.Skip(e.Skip).Take(e.Take * pageTakeCount).ToArray()
                                                                    End Function)
    End Sub

    Private Sub OnGetTotalSummaries(ByVal sender As System.Object, ByVal e As DevExpress.Xpf.Data.GetSummariesAsyncEventArgs)
        e.Result = Task.Run(Function()
                                Dim context = New Issues.IssuesContext()
                                Dim queryable = context.Issues.Where(MakeFilterExpression(CType(e.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                Return queryable.GetSummaries(e.Summaries)
                            End Function)
    End Sub

    Private Function MakeFilterExpression(ByVal filter As DevExpress.Data.Filtering.CriteriaOperator) As System.Linq.Expressions.Expression(Of System.Func(Of EntityFrameworkIssues.Issues.Issue, Boolean))
        Dim converter = New DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter(Of EntityFrameworkIssues.Issues.Issue)()
        Return converter.Convert(filter)
    End Function

End Class
