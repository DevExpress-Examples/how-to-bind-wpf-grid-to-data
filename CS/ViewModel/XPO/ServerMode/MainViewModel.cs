using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        XPServerModeView _ServerModeSource;

        public XPServerModeView ServerModeSource
        {
            get
            {
                if(_ServerModeSource == null) {
                    var properties = new ServerViewProperty[] {
new ServerViewProperty("Oid", SortDirection.Ascending, new OperandProperty("Oid")),
new ServerViewProperty("Subject", SortDirection.None, new OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new OperandProperty("Priority"))
    };
                    var session = new DevExpress.Xpo.Session();
                    _ServerModeSource = new XPServerModeView(session, typeof(XPOIssues.Issues.Issue), null);
                    _ServerModeSource.Properties.AddRange(properties);
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