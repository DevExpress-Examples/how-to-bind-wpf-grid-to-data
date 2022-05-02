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
            var context = new IssuesContext();
            var source = new EntityServerModeSource {
                KeyExpression = nameof(Issue.Id),
                QueryableSource = context.Issues.AsNoTracking()
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
