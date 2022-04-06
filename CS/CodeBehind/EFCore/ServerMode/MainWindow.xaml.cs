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
            var context = new IssuesContext();
            var source = new DevExpress.Data.Linq.EntityServerModeSource
            {
                KeyExpression = nameof(Issue.Id),
                QueryableSource = context.Issues.AsNoTracking()
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
