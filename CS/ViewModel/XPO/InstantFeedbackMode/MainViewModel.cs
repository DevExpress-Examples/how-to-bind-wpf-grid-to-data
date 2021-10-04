using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Xpo.XPInstantFeedbackView _InstantFeedbackSource;

        public DevExpress.Xpo.XPInstantFeedbackView InstantFeedbackSource
        {
            get
            {
                if(_InstantFeedbackSource == null) {
                    var properties = new DevExpress.Xpo.ServerViewProperty[] {
new DevExpress.Xpo.ServerViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, new DevExpress.Data.Filtering.OperandProperty("Oid")),
new DevExpress.Xpo.ServerViewProperty("Subject", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Subject")),
new DevExpress.Xpo.ServerViewProperty("UserId", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("UserId")),
new DevExpress.Xpo.ServerViewProperty("Created", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Created")),
new DevExpress.Xpo.ServerViewProperty("Votes", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Votes")),
new DevExpress.Xpo.ServerViewProperty("Priority", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Priority"))
    };
                    _InstantFeedbackSource = new DevExpress.Xpo.XPInstantFeedbackView(typeof(XPOIssues.Issues.Issue), properties, null);
                    _InstantFeedbackSource.ResolveSession += (o, e) =>
                    {
                        e.Session = new DevExpress.Xpo.Session();
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