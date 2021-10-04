using System.Windows;
using System.Linq;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Refresh();
        }
        EFCoreIssues.Issues.IssuesContext _Context;

        void Refresh() {
            _Context = new EFCoreIssues.Issues.IssuesContext();
            grid.ItemsSource = _Context.Users.ToList();
        }
    }
}
