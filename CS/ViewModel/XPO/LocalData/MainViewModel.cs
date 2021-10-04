using DevExpress.Mvvm;
using System.Linq;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Xpo.UnitOfWork _UnitOfWork;
        System.Collections.Generic.IList<XPOIssues.Issues.User> _ItemsSource;

        public System.Collections.Generic.IList<XPOIssues.Issues.User> ItemsSource
        {
            get
            {
                if(_ItemsSource == null && !IsInDesignMode) {
                    _UnitOfWork = new DevExpress.Xpo.UnitOfWork();
                    var xpCollection = new DevExpress.Xpo.XPCollection<XPOIssues.Issues.User>(_UnitOfWork);
                    xpCollection.Sorting.Add(new DevExpress.Xpo.SortProperty(nameof(XPOIssues.Issues.User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending));
                    _ItemsSource = xpCollection;
                }
                return _ItemsSource;
            }
        }
    }
}