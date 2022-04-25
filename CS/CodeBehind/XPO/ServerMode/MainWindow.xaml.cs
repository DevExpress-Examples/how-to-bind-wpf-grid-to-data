using System.Windows;
using XPOIssues.Issues;
using DevExpress.Xpo;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;

namespace XPOIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var properties = new ServerViewProperty[] {
new ServerViewProperty("Subject", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Priority")),
new ServerViewProperty("Oid", SortDirection.Ascending, new DevExpress.Data.Filtering.OperandProperty("Oid"))
};
            var session = new Session();
            var source = new XPServerModeView(session, typeof(Issue), null);
            source.Properties.AddRange(properties);
            grid.ItemsSource = source;
            LoadLookupData();
        }

        void LoadLookupData() {
            var session = new DevExpress.Xpo.Session();
            usersLookup.ItemsSource = session.Query<XPOIssues.Issues.User>().OrderBy(user => user.Oid).Select(user => new { Id = user.Oid, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
