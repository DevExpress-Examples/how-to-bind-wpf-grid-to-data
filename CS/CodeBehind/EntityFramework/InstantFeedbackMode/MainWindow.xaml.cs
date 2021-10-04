using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new DevExpress.Data.Linq.EntityInstantFeedbackSource
            {
                KeyExpression = nameof(EntityFrameworkIssues.Issues.Issue.Id)
            };
            source.GetQueryable += (sender, e) =>
            {
                var context = new EntityFrameworkIssues.Issues.IssuesContext();
                e.QueryableSource = context.Issues.AsNoTracking();
            };
            grid.ItemsSource = source;
            LoadLookupData();
        }

        void LoadLookupData() {
            var context = new EntityFrameworkIssues.Issues.IssuesContext();
            usersLookup.ItemsSource = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
