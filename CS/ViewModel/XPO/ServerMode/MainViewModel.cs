using DevExpress.Mvvm;
using XPOIssues.Issues;
using DevExpress.Xpo;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        XPServerModeView _ItemsSource;
        public XPServerModeView ItemsSource
        {
            get
            {
                if(_ItemsSource == null) {
                    var properties = new ServerViewProperty[] {
new ServerViewProperty("Subject", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Priority")),
new ServerViewProperty("Oid", SortDirection.Ascending, new DevExpress.Data.Filtering.OperandProperty("Oid"))
    };
                    var session = new Session();
                    _ItemsSource = new XPServerModeView(session, typeof(Issue), null);
                    _ItemsSource.Properties.AddRange(properties);
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
                    {
                        var session = new DevExpress.Xpo.Session();
                        _Users = session.Query<XPOIssues.Issues.User>().OrderBy(user => user.Oid).Select(user => new { Id = user.Oid, Name = user.FirstName + " " + user.LastName }).ToArray();
                    }
                }
                return _Users;
            }
        }
    }
}