﻿Imports DevExpress.Mvvm
Imports EFCoreIssues.Issues
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Linq.Expressions
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore
Imports DevExpress.Mvvm.Xpf
Imports System.Collections

Public Class MainViewModel
    Inherits ViewModelBase

    Private Function MakeFilterExpression(ByVal filter As CriteriaOperator) As Expression(Of Func(Of Issue, Boolean))
        Dim converter = New GridFilterCriteriaToExpressionConverter(Of Issue)()
        Return converter.Convert(filter)
    End Function
    <Command>
    Public Sub FetchRows(ByVal args As FetchRowsAsyncArgs)
        args.Result = Task.Run(Of FetchRowsResult)(Function()
                                                       Dim context = New IssuesContext()
                                                       Dim queryable = context.Issues.AsNoTracking().SortBy(args.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issue.Id)).Where(MakeFilterExpression(CType(args.Filter, CriteriaOperator)))
                                                       Return queryable.Skip(args.Skip).Take(If(args.Take, 100)).ToArray()
                                                   End Function)
    End Sub
    <Command>
    Public Sub GetTotalSummaries(ByVal args As GetSummariesAsyncArgs)
        args.Result = Task.Run(Function()
                                   Dim context = New IssuesContext()
                                   Dim queryable = context.Issues.Where(MakeFilterExpression(CType(args.Filter, CriteriaOperator)))
                                   Return queryable.GetSummaries(args.Summaries)
                               End Function)
    End Sub
    Private _Users As IList
    Public ReadOnly Property Users As IList
        Get
            If _Users Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                Dim context = New IssuesContext()
                _Users = context.Users.[Select](Function(user) New With {
                    .Id = user.Id,
                    .Name = user.FirstName & " " + user.LastName
                }).ToArray()
            End If
            Return _Users
        End Get
    End Property

End Class