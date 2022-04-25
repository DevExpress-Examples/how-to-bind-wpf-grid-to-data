using DevExpress.Mvvm;
using EFCoreIssues.Issues;
using Microsoft.EntityFrameworkCore;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Data.Linq.EntityServerModeSource _ItemsSource;
        public DevExpress.Data.Linq.EntityServerModeSource ItemsSource {
            get
            {
                if(_ItemsSource == null) {
                    var context = new IssuesContext();
                    _ItemsSource = new DevExpress.Data.Linq.EntityServerModeSource {
                        KeyExpression = nameof(Issue.Id),
                        QueryableSource = context.Issues.AsNoTracking()
                    };
                }
                return _ItemsSource;
            }
        }
        System.Collections.IList _Users;
        public System.Collections.IList Users {
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