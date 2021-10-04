using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new DevExpress.Data.Linq.EntityInstantFeedbackSource
            {
                KeyExpression = nameof(EFCoreIssues.Issues.Issue.Id)
            };
            source.GetQueryable += (sender, e) =>
            {
                var context = new EFCoreIssues.Issues.IssuesContext();
                e.QueryableSource = context.Issues.AsNoTracking();
            };
            grid.ItemsSource = source;
        }
    }
}
