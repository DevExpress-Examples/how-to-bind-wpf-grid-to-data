using DevExpress.Mvvm;
using System.Linq;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        EFCoreIssues.Issues.IssuesContext _Context;
        System.Collections.Generic.IList<EFCoreIssues.Issues.User> _ItemsSource;

        public System.Collections.Generic.IList<EFCoreIssues.Issues.User> ItemsSource
        {
            get
            {
                if(_ItemsSource == null && !IsInDesignMode) {
                    _Context = new EFCoreIssues.Issues.IssuesContext();
                    _ItemsSource = _Context.Users.ToList();
                }
                return _ItemsSource;
            }
        }
    }
}