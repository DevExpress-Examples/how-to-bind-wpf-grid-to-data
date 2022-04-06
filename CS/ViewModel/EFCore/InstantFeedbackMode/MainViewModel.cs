using DevExpress.Mvvm;
using EFCoreIssues.Issues;
using Microsoft.EntityFrameworkCore;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Data.Linq.EntityInstantFeedbackSource _ItemsSource;
        public DevExpress.Data.Linq.EntityInstantFeedbackSource ItemsSource
        {
            get
            {
                if(_ItemsSource == null) {
                    _ItemsSource = new DevExpress.Data.Linq.EntityInstantFeedbackSource
                    {
                        KeyExpression = nameof(Issue.Id)
                    };
                    _ItemsSource.GetQueryable += (sender, e) =>
                    {
                        var context = new IssuesContext();
                        e.QueryableSource = context.Issues.AsNoTracking();
                    };
                }
                return _ItemsSource;
            }
        }
        System.Collections.IList _Users;
        public System.Collections.IList Users
        {
            get
            {
                if(_Users == null && !DevExpress.Mvvm.ViewModelBase.IsInDesignMode) {
                    var context = new EFCoreIssues.Issues.IssuesContext();
                    _Users = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
                }
                return _Users;
            }
        }
    }
}