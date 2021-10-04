using System.Windows;
using System.Linq;

namespace XPOIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Refresh();
        }
        DevExpress.Xpo.UnitOfWork _UnitOfWork;

        void Refresh() {
            _UnitOfWork = new DevExpress.Xpo.UnitOfWork();
            var xpCollection = new DevExpress.Xpo.XPCollection<XPOIssues.Issues.User>(_UnitOfWork);
            xpCollection.Sorting.Add(new DevExpress.Xpo.SortProperty(nameof(XPOIssues.Issues.User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending));
            grid.ItemsSource = xpCollection;
        }
    }
}
