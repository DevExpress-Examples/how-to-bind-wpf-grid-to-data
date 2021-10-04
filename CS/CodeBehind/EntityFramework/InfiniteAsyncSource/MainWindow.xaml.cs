using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new InfiniteAsyncSource
            {
                ElementType = typeof(EntityFrameworkIssues.Issues.Issue),
                KeyProperty = nameof(EntityFrameworkIssues.Issues.Issue.Id)
            };
            source.FetchRows += OnFetchRows;
            source.GetTotalSummaries += OnGetTotalSummaries;
            grid.ItemsSource = source;
        }

        void OnFetchRows(System.Object sender, DevExpress.Xpf.Data.FetchRowsAsyncEventArgs e) {
            e.Result = Task.Run<DevExpress.Xpf.Data.FetchRowsResult>(() =>
            {
                var context = new EntityFrameworkIssues.Issues.IssuesContext();
                var queryable = context.Issues.AsNoTracking()
                    .SortBy(e.SortOrder, defaultUniqueSortPropertyName: nameof(EntityFrameworkIssues.Issues.Issue.Id))
                    .Where(MakeFilterExpression(e.Filter));
                return queryable.Skip(e.Skip).Take(e.Take ?? 100).ToArray();
            });
        }

        void OnGetTotalSummaries(System.Object sender, DevExpress.Xpf.Data.GetSummariesAsyncEventArgs e) {
            e.Result = Task.Run(() =>
            {
                var context = new EntityFrameworkIssues.Issues.IssuesContext();
                var queryable = context.Issues.Where(MakeFilterExpression(e.Filter));
                return queryable.GetSummaries(e.Summaries);
            });
        }

        System.Linq.Expressions.Expression<System.Func<EntityFrameworkIssues.Issues.Issue, bool>> MakeFilterExpression(DevExpress.Data.Filtering.CriteriaOperator filter) {
            var converter = new DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter<EntityFrameworkIssues.Issues.Issue>();
            return converter.Convert(filter);
        }
    }
}
