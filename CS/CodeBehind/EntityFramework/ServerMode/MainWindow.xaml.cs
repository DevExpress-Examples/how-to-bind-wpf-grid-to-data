using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var context = new EntityFrameworkIssues.Issues.IssuesContext();
            var source = new DevExpress.Data.Linq.EntityServerModeSource
            {
                KeyExpression = nameof(EntityFrameworkIssues.Issues.Issue.Id),
                QueryableSource = context.Issues.AsNoTracking()
            };
            grid.ItemsSource = source;
        }
    }
}
