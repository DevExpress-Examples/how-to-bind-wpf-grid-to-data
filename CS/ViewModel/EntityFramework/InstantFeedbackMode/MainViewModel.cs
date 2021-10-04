using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Data.Linq.EntityInstantFeedbackSource _InstantFeedbackSource;

        public DevExpress.Data.Linq.EntityInstantFeedbackSource InstantFeedbackSource
        {
            get
            {
                if(_InstantFeedbackSource == null) {
                    _InstantFeedbackSource = new DevExpress.Data.Linq.EntityInstantFeedbackSource
                    {
                        KeyExpression = nameof(EntityFrameworkIssues.Issues.Issue.Id)
                    };
                    _InstantFeedbackSource.GetQueryable += (sender, e) =>
                    {
                        var context = new EntityFrameworkIssues.Issues.IssuesContext();
                        e.QueryableSource = context.Issues.AsNoTracking();
                    };
                }
                return _InstantFeedbackSource;
            }
        }
        System.Collections.IList _Users;

        public System.Collections.IList Users
        {
            get
            {
                if(_Users == null && !IsInDesignMode) {
                    var context = new EntityFrameworkIssues.Issues.IssuesContext();
                    _Users = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
                }
                return _Users;
            }
        }
    }
}