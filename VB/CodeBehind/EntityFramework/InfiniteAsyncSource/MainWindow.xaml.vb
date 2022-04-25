﻿Imports EntityFrameworkIssues.Issues
Imports System.Data.Entity
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Class MainWindow
    Public Sub New()
        InitializeComponent()
        Dim source = New InfiniteAsyncSource With {
            .ElementType = GetType(Issue),
            .KeyProperty = NameOf(Issue.Id)
        }
        AddHandler source.FetchRows, AddressOf OnFetchRows
        AddHandler source.GetTotalSummaries, AddressOf OnGetTotalSummaries
        grid.ItemsSource = source
        LoadLookupData()
    End Sub

    Private Function MakeFilterExpression(ByVal filter As DevExpress.Data.Filtering.CriteriaOperator) As System.Linq.Expressions.Expression(Of System.Func(Of Issue, Boolean))
        Dim converter = New DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter(Of Issue)()
        Return converter.Convert(filter)
    End Function

    Private Sub OnFetchRows(ByVal sender As Object, ByVal e As FetchRowsAsyncEventArgs)
        e.Result = Task.Run(Of DevExpress.Xpf.Data.FetchRowsResult)(Function()
                                                                        Dim context = New IssuesContext()
                                                                        Dim queryable = context.Issues.AsNoTracking().SortBy(e.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issue.Id)).Where(MakeFilterExpression(e.Filter))
                                                                        Return queryable.Skip(e.Skip).Take(If(e.Take, 100)).ToArray()
                                                                    End Function)
    End Sub

    Private Sub OnGetTotalSummaries(ByVal sender As Object, ByVal e As GetSummariesAsyncEventArgs)
        e.Result = Task.Run(Function()
                                Dim context = New IssuesContext()
                                Dim queryable = context.Issues.Where(MakeFilterExpression(CType(e.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                Return queryable.GetSummaries(e.Summaries)
                            End Function)
    End Sub

    Private Sub LoadLookupData()
        Dim context = New EntityFrameworkIssues.Issues.IssuesContext()
        usersLookup.ItemsSource = context.Users.[Select](Function(user) New With {
            .Id = user.Id,
            .Name = user.FirstName & " " + user.LastName
        }).ToArray()
    End Sub

End Class
