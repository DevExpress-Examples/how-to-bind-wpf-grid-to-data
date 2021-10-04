using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        DevExpress.Xpo.XPServerModeView _ServerModeSource;

        public DevExpress.Xpo.XPServerModeView ServerModeSource
        {
            get
            {
                if(_ServerModeSource == null) {
                    var properties = new DevExpress.Xpo.ServerViewProperty[] {
new DevExpress.Xpo.ServerViewProperty("Oid", DevExpress.Xpo.SortDirection.Ascending, new DevExpress.Data.Filtering.OperandProperty("Oid")),
new DevExpress.Xpo.ServerViewProperty("Subject", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Subject")),
new DevExpress.Xpo.ServerViewProperty("UserId", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("UserId")),
new DevExpress.Xpo.ServerViewProperty("Created", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Created")),
new DevExpress.Xpo.ServerViewProperty("Votes", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Votes")),
new DevExpress.Xpo.ServerViewProperty("Priority", DevExpress.Xpo.SortDirection.None, new DevExpress.Data.Filtering.OperandProperty("Priority"))
    };
                    var session = new DevExpress.Xpo.Session();
                    _ServerModeSource = new DevExpress.Xpo.XPServerModeView(session, typeof(XPOIssues.Issues.Issue), null);
                    _ServerModeSource.Properties.AddRange(properties);
                }
                return _ServerModeSource;
            }
        }
    }
}