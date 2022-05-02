using DevExpress.Mvvm;
using XPOIssues.Issues;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System.Linq;
using System.Collections;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        XPServerModeView _ItemsSource;
        public XPServerModeView ItemsSource {
            get
            {
                if(_ItemsSource == null) {
                    var properties = new ServerViewProperty[] {
new ServerViewProperty("Subject", SortDirection.None, new OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new OperandProperty("Priority")),
new ServerViewProperty("Oid", SortDirection.Ascending, new OperandProperty("Oid"))
    };
                    var session = new Session();
                    _ItemsSource = new XPServerModeView(session, typeof(Issue), null);
                    _ItemsSource.Properties.AddRange(properties);
                }
                return _ItemsSource;
            }
        }
        IList _Users;
        public IList Users {
            get
            {
                if(_Users == null && !DevExpress.Mvvm.ViewModelBase.IsInDesignMode) {
                    {
                        var session = new Session();
                        _Users = session.Query<User>().OrderBy(user => user.Oid).Select(user => new { Id = user.Oid, Name = user.FirstName + " " + user.LastName }).ToArray();
                    }
                }
                return _Users;
            }
        }
    }
}