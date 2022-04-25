using System.Windows;
using EFCoreIssues.Issues;
using Microsoft.EntityFrameworkCore;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new DevExpress.Data.Linq.EntityInstantFeedbackSource {
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
            var context = new EFCoreIssues.Issues.IssuesContext();
            usersLookup.ItemsSource = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
