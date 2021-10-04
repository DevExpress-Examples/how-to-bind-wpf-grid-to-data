using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Data.Linq.EntityServerModeSource _ServerModeSource;

        public DevExpress.Data.Linq.EntityServerModeSource ServerModeSource
        {
            get
            {
                if(_ServerModeSource == null) {
                    var context = new EFCoreIssues.Issues.IssuesContext();
                    _ServerModeSource = new DevExpress.Data.Linq.EntityServerModeSource
                    {
                        KeyExpression = nameof(EFCoreIssues.Issues.Issue.Id),
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
                    var context = new EFCoreIssues.Issues.IssuesContext();
                    _Users = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
                }
                return _Users;
            }
        }
    }
}