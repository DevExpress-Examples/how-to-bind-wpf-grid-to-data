using System.Windows;
using System.Linq;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Refresh();
        }
        EntityFrameworkIssues.Issues.IssuesContext _Context;

        void Refresh() {
            _Context = new EntityFrameworkIssues.Issues.IssuesContext();
            grid.ItemsSource = _Context.Users.ToList();
        }
    }
}
