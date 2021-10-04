using System.Windows;
using System.Linq;

namespace EntityFrameworkIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            LoadData();
        }
        EntityFrameworkIssues.Issues.IssuesContext _Context;

        void LoadData() {
            _Context = new EntityFrameworkIssues.Issues.IssuesContext();
            grid.ItemsSource = _Context.Users.ToList();
        }
    }
}
