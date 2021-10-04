Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Data.Entity

Public Class MainViewModel
    Inherits ViewModelBase
    <DevExpress.Mvvm.DataAnnotations.Command>
    Public Sub FetchRows(ByVal args As DevExpress.Mvvm.Xpf.FetchRowsAsyncArgs)
        args.Result = Task.Run(Of DevExpress.Xpf.Data.FetchRowsResult)(Function()
                                                                           Dim context = New Issues.IssuesContext()
                                                                           Dim queryable = context.Issues.AsNoTracking().SortBy(args.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issues.Issue.Id)).Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                                                           Return queryable.Skip(args.Skip).Take(If(args.Take, 100)).ToArray()
                                                                       End Function)
    End Sub
    <DevExpress.Mvvm.DataAnnotations.Command>
    Public Sub GetTotalSummaries(ByVal args As DevExpress.Mvvm.Xpf.GetSummariesAsyncArgs)
        args.Result = Task.Run(Function()
                                   Dim context = New Issues.IssuesContext()
                                   Dim queryable = context.Issues.Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                   Return queryable.GetSummaries(args.Summaries)
                               End Function)
    End Sub

    Private Function MakeFilterExpression(ByVal filter As DevExpress.Data.Filtering.CriteriaOperator) As System.Linq.Expressions.Expression(Of System.Func(Of EntityFrameworkIssues.Issues.Issue, Boolean))
        Dim converter = New DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter(Of EntityFrameworkIssues.Issues.Issue)()
        Return converter.Convert(filter)
    End Function
    Private _Users As System.Collections.IList

    Public ReadOnly Property Users As System.Collections.IList
        Get
            If _Users Is Nothing AndAlso Not IsInDesignMode Then
                Dim context = New EntityFrameworkIssues.Issues.IssuesContext()
                _Users = context.Users.[Select](Function(user) New With {
                    .Id = user.Id,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class