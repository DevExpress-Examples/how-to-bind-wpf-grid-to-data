using DevExpress.Mvvm;
using XPOIssues.Issues;
using DevExpress.Xpo;
using DevExpress.Mvvm.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        UnitOfWork _UnitOfWork;
        IList<User> _ItemsSource;
        public IList<User> ItemsSource
        {
            get
            {
                if(_ItemsSource == null && !DevExpress.Mvvm.ViewModelBase.IsInDesignMode) {
                    _UnitOfWork = new UnitOfWork();
                    var xpCollection = new XPCollection<User>(_UnitOfWork);
                    xpCollection.Sorting.Add(new SortProperty(nameof(User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending));
                    _ItemsSource = xpCollection;
                }
                return _ItemsSource;
            }
        }
    }
}