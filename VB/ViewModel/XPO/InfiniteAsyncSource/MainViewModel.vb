Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports DevExpress.Xpo

Public Class MainViewModel
    Inherits ViewModelBase
    Private _DetachedObjectsHelper As DetachedObjectsHelper(Of XPOIssues.Issues.Issue)

    Public ReadOnly Property DetachedObjectsHelper As DetachedObjectsHelper(Of XPOIssues.Issues.Issue)
        Get
            If _DetachedObjectsHelper Is Nothing Then
                Using session = New Session()
                    Dim classInfo = session.GetClassInfo(Of XPOIssues.Issues.Issue)()
                    Dim properties = classInfo.Members.Where(Function(member) member.IsPublic AndAlso member.IsPersistent).[Select](Function(member) member.Name).ToArray()
                    _DetachedObjectsHelper = DevExpress.Xpf.Data.DetachedObjectsHelper(Of XPOIssues.Issues.Issue).Create(classInfo.KeyProperty.Name, properties)
                End Using
            End If
            Return _DetachedObjectsHelper
        End Get
    End Property

    Public ReadOnly Property Properties As System.ComponentModel.PropertyDescriptorCollection
        Get
            Return DetachedObjectsHelper.Properties
        End Get
    End Property
    <DevExpress.Mvvm.DataAnnotations.Command>
    Public Sub FetchRows(ByVal args As DevExpress.Mvvm.Xpf.FetchRowsAsyncArgs)
        args.Result = Task.Run(Of DevExpress.Xpf.Data.FetchRowsResult)(Function()
                                                                           Using session = New DevExpress.Xpo.Session()
                                                                               Dim queryable = session.Query(Of Issues.Issue)().SortBy(args.SortOrder, defaultUniqueSortPropertyName:=NameOf(Issues.Issue.Oid)).Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator)))
                                                                               Dim items = queryable.Skip(args.Skip).Take(If(args.Take, 100)).ToArray()
                                                                               Return DetachedObjectsHelper.ConvertToDetachedObjects(items)
                                                                           End Using
                                                                       End Function)
    End Sub
    <DevExpress.Mvvm.DataAnnotations.Command>
    Public Sub GetTotalSummaries(ByVal args As DevExpress.Mvvm.Xpf.GetSummariesAsyncArgs)
        args.Result = Task.Run(Function()
                                   Using session = New DevExpress.Xpo.Session()
                                       Return session.Query(Of Issues.Issue)().Where(MakeFilterExpression(CType(args.Filter, DevExpress.Data.Filtering.CriteriaOperator))).GetSummaries(args.Summaries)
                                   End Using
                               End Function)
    End Sub

    Private Function MakeFilterExpression(ByVal filter As DevExpress.Data.Filtering.CriteriaOperator) As System.Linq.Expressions.Expression(Of System.Func(Of XPOIssues.Issues.Issue, Boolean))
        Dim converter = New DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter(Of XPOIssues.Issues.Issue)()
        Return converter.Convert(filter)
    End Function

End Class