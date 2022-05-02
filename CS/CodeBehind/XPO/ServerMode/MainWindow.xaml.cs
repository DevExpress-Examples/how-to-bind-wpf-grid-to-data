using System.Windows;
using XPOIssues.Issues;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Xpf.Grid;

namespace XPOIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var properties = new ServerViewProperty[] {
new ServerViewProperty("Subject", SortDirection.None, new OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new OperandProperty("Priority")),
new ServerViewProperty("Oid", SortDirection.Ascending, new OperandProperty("Oid"))
};
            var session = new Session();
            var source = new XPServerModeView(session, typeof(Issue), null);
            source.Properties.AddRange(properties);
            grid.ItemsSource = source;
            LoadLookupData();
        }

        void LoadLookupData() {
            var session = new Session();
            usersLookup.ItemsSource = session.Query<User>().OrderBy(user => user.Oid).Select(user => new { Id = user.Oid, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
