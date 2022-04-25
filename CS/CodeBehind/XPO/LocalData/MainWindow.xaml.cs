using System.Windows;
using XPOIssues.Issues;
using DevExpress.Xpo;
using System.Linq;

namespace XPOIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            LoadData();
        }
        UnitOfWork _UnitOfWork;

        void LoadData() {
            _UnitOfWork = new UnitOfWork();
            var xpCollection = new XPCollection<User>(_UnitOfWork);
            xpCollection.Sorting.Add(new SortProperty(nameof(User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending));
            grid.ItemsSource = xpCollection;
        }
    }
}
