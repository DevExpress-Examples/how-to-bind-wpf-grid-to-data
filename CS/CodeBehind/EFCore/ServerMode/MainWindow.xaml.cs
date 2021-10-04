using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var context = new EFCoreIssues.Issues.IssuesContext();
            var source = new DevExpress.Data.Linq.EntityServerModeSource
            {
                KeyExpression = nameof(EFCoreIssues.Issues.Issue.Id),
                QueryableSource = context.Issues.AsNoTracking()
            };
            grid.ItemsSource = source;
        }
    }
}
