using DevExpress.Mvvm;
using System.Linq;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {
        EntityFrameworkIssues.Issues.IssuesContext _Context;
        System.Collections.Generic.IList<EntityFrameworkIssues.Issues.User> _ItemsSource;

        public System.Collections.Generic.IList<EntityFrameworkIssues.Issues.User> ItemsSource
        {
            get
            {
                if(_ItemsSource == null && !IsInDesignMode) {
                    _Context = new EntityFrameworkIssues.Issues.IssuesContext();
                    _ItemsSource = _Context.Users.ToList();
                }
                return _ItemsSource;
            }
        }
    }
}