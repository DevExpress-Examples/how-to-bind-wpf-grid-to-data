using DevExpress.Mvvm;
using EntityFrameworkIssues.Issues;
using System.Data.Entity;
using DevExpress.Mvvm.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {
        IssuesContext _Context;
        IList<User> _ItemsSource;
        public IList<User> ItemsSource
        {
            get
            {
                if(_ItemsSource == null && !DevExpress.Mvvm.ViewModelBase.IsInDesignMode) {
                    _Context = new IssuesContext();
                    _ItemsSource = _Context.Users.ToList();
                }
                return _ItemsSource;
            }
        }
    }
}