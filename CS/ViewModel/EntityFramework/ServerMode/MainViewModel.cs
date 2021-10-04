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
    }
}