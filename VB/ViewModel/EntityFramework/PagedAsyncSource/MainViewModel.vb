﻿Imports DevExpress.Mvvm
Imports EntityFrameworkIssues.Issues
Imports System.Data.Entity
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks

Public Class MainViewModel
    Inherits ViewModelBase

    Private Function MakeFilterExpression(ByVal filter As DevExpress.Data.Filtering.CriteriaOperator) As System.Linq.Expressions.Expression(Of System.Func(Of Issue, Boolean))
        Dim converter = New DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter(Of Issue)()
        Return converter.Convert(filter)
    End Function
    <Command>
    Public Sub FetchPage(ByVal args As FetchPageAsyncArgs)
        Const pageTakeCount As Integer = 5
        args.Result = Task.Run(Of DevExpress.Xpf.Data.FetchRowsResult)(Function()
                                                                           Dim context = New IssuesContext()
                                                                           Dim queryable = context.Issues.AsNoTracking().SortBy(args.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issue.Id)).Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                                                           Return queryable.Skip(args.Skip).Take(args.Take * pageTakeCount).ToArray()
                                                                       End Function)
    End Sub
    <Command>
    Public Sub GetTotalSummaries(ByVal args As GetSummariesAsyncArgs)
        args.Result = Task.Run(Function()
                                   Dim context = New IssuesContext()
                                   Dim queryable = context.Issues.Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                   Return queryable.GetSummaries(args.Summaries)
                               End Function)
    End Sub
    Private _Users As System.Collections.IList
    Public ReadOnly Property Users As System.Collections.IList
        Get
            If _Users Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
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