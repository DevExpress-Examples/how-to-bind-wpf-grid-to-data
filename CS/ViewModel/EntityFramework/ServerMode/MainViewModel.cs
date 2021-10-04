using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Data.Linq.EntityServerModeSource _ServerModeSource;

        public DevExpress.Data.Linq.EntityServerModeSource ServerModeSource
        {
            get
            {
                if(_ServerModeSource == null) {
                    var context = new EntityFrameworkIssues.Issues.IssuesContext();
                    _ServerModeSource = new DevExpress.Data.Linq.EntityServerModeSource
                    {
                        KeyExpression = nameof(EntityFrameworkIssues.Issues.Issue.Id),
                        QueryableSource = context.Issues.AsNoTracking()
                    };
                }
                return _ServerModeSource;
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