using System.Windows;
using System.Linq;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            LoadData();
        }
        EFCoreIssues.Issues.IssuesContext _Context;

        void LoadData() {
            _Context = new EFCoreIssues.Issues.IssuesContext();
            grid.ItemsSource = _Context.Users.ToList();
        }
    }
}
