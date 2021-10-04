using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void FetchRows(DevExpress.Mvvm.Xpf.FetchRowsAsyncArgs args) {
            args.Result = Task.Run<DevExpress.Xpf.Data.FetchRowsResult>(() =>
            {
                var context = new EFCoreIssues.Issues.IssuesContext();
                var queryable = context.Issues.AsNoTracking()
                    .SortBy(args.SortOrder, defaultUniqueSortPropertyName: nameof(EFCoreIssues.Issues.Issue.Id))
                    .Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter));
                return queryable.Skip(args.Skip).Take(args.Take ?? 100).ToArray();
            });
        }
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void GetTotalSummaries(DevExpress.Mvvm.Xpf.GetSummariesAsyncArgs args) {
            args.Result = Task.Run(() =>
            {
                var context = new EFCoreIssues.Issues.IssuesContext();
                var queryable = context.Issues.Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter));
                return queryable.GetSummaries(args.Summaries);
            });
        }

        System.Linq.Expressions.Expression<System.Func<EFCoreIssues.Issues.Issue, bool>> MakeFilterExpression(DevExpress.Data.Filtering.CriteriaOperator filter) {
            var converter = new DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter<EFCoreIssues.Issues.Issue>();
            return converter.Convert(filter);
        }
    }
}