using System.Windows;
using EntityFrameworkIssues.Issues;
using System.Data.Entity;
using System.Linq;

namespace EntityFrameworkIssues {
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
