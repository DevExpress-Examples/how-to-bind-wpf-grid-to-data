using System.Windows;
using EFCoreIssues.Issues;
using System.Linq;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            LoadData();
        }
        IssuesContext _Context;

        void LoadData() {
            _Context = new IssuesContext();
            grid.ItemsSource = _Context.Users.ToList();
        }
    }
}
