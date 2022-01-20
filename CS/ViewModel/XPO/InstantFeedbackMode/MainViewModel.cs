using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        XPInstantFeedbackView _InstantFeedbackSource;

        public XPInstantFeedbackView InstantFeedbackSource
        {
            get
            {
                if(_InstantFeedbackSource == null) {
                    var properties = new ServerViewProperty[] {
new ServerViewProperty("Oid", SortDirection.Ascending, new OperandProperty("Oid")),
new ServerViewProperty("Subject", SortDirection.None, new OperandProperty("Subject")),
new ServerViewProperty("UserId", SortDirection.None, new OperandProperty("UserId")),
new ServerViewProperty("Created", SortDirection.None, new OperandProperty("Created")),
new ServerViewProperty("Votes", SortDirection.None, new OperandProperty("Votes")),
new ServerViewProperty("Priority", SortDirection.None, new OperandProperty("Priority"))
    };
                    _InstantFeedbackSource = new XPInstantFeedbackView(typeof(XPOIssues.Issues.Issue), properties, null);
                    _InstantFeedbackSource.ResolveSession += (o, e) =>
                    {
                        e.Session = new Session();
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