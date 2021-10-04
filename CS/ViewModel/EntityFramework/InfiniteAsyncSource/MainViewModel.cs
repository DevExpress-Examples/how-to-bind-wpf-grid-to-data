using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void FetchRows(DevExpress.Mvvm.Xpf.FetchRowsAsyncArgs args) {
            args.Result = Task.Run<DevExpress.Xpf.Data.FetchRowsResult>(() =>
            {
                var context = new EntityFrameworkIssues.Issues.IssuesContext();
                var queryable = context.Issues.AsNoTracking()
                    .SortBy(args.SortOrder, defaultUniqueSortPropertyName: nameof(EntityFrameworkIssues.Issues.Issue.Id))
                    .Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter));
                return queryable.Skip(args.Skip).Take(args.Take ?? 100).ToArray();
            });
        }
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void GetTotalSummaries(DevExpress.Mvvm.Xpf.GetSummariesAsyncArgs args) {
            args.Result = Task.Run(() =>
            {
                var context = new EntityFrameworkIssues.Issues.IssuesContext();
                var queryable = context.Issues.Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter));
                return queryable.GetSummaries(args.Summaries);
            });
        }

        System.Linq.Expressions.Expression<System.Func<EntityFrameworkIssues.Issues.Issue, bool>> MakeFilterExpression(DevExpress.Data.Filtering.CriteriaOperator filter) {
            var converter = new DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter<EntityFrameworkIssues.Issues.Issue>();
            return converter.Convert(filter);
        }
    }
}