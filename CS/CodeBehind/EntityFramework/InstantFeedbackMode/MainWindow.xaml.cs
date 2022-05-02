using System.Windows;
using EntityFrameworkIssues.Issues;
using DevExpress.Data.Linq;
using System.Data.Entity;
using System.Linq;
using DevExpress.Xpf.Grid;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new EntityInstantFeedbackSource {
                KeyExpression = nameof(Issue.Id)
            };
            source.GetQueryable += (sender, e) => {
                var context = new IssuesContext();
                e.QueryableSource = context.Issues.AsNoTracking();
            };
            grid.ItemsSource = source;
            LoadLookupData();
        }

        void LoadLookupData() {
            var context = new IssuesContext();
            usersLookup.ItemsSource = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
