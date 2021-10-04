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
    }
}