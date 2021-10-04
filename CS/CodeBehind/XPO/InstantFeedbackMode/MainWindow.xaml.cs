using System.Windows;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace XPOIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var properties = new DevExpress.Xpo.ServerViewProperty[] {
new DevExpress.Xpo.ServerViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, new DevExpress.Data.Filtering.OperandProperty("Oid")),
new DevExpress.Xpo.ServerViewProperty("Subject", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Subject")),
new DevExpress.Xpo.ServerViewProperty("UserId", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("UserId")),
new DevExpress.Xpo.ServerViewProperty("Created", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Created")),
new DevExpress.Xpo.ServerViewProperty("Votes", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Votes")),
new DevExpress.Xpo.ServerViewProperty("Priority", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Priority"))
};
            var source = new DevExpress.Xpo.XPInstantFeedbackView(typeof(XPOIssues.Issues.Issue), properties, null);
            source.ResolveSession += (o, e) =>
            {
                e.Session = new DevExpress.Xpo.Session();
            };
            grid.ItemsSource = source;
            LoadLookupData();
        }

        void LoadLookupData() {
            var session = new DevExpress.Xpo.Session();
            usersLookup.ItemsSource = session.Query<XPOIssues.Issues.User>().OrderBy(user => user.Oid).Select(user => new { Id = user.Oid, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
