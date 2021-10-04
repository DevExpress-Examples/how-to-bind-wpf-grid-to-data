using DevExpress.Mvvm;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace XPOIssues {
    public class MainViewModel : ViewModelBase {
        DetachedObjectsHelper<XPOIssues.Issues.Issue> _DetachedObjectsHelper;

        public DetachedObjectsHelper<XPOIssues.Issues.Issue> DetachedObjectsHelper
        {
            get
            {
                if(_DetachedObjectsHelper == null) {
                    using(var session = new Session()) {
                        var classInfo = session.GetClassInfo<XPOIssues.Issues.Issue>();
                        var properties = classInfo.Members
                            .Where(member => member.IsPublic && member.IsPersistent)
                            .Select(member => member.Name)
                            .ToArray();
                        _DetachedObjectsHelper = DetachedObjectsHelper<XPOIssues.Issues.Issue>.Create(classInfo.KeyProperty.Name, properties);
                    }
                }
                return _DetachedObjectsHelper;
            }
        }

        public System.ComponentModel.PropertyDescriptorCollection Properties
        {
            get
            {
                return DetachedObjectsHelper.Properties;
            }
        }
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void FetchPage(DevExpress.Mvvm.Xpf.FetchPageAsyncArgs args) {
            args.Result = Task.Run<DevExpress.Xpf.Data.FetchRowsResult>(() =>
            {
                const int pageTakeCount = 5;
                using(var session = new DevExpress.Xpo.Session()) {
                    var queryable = session.Query<XPOIssues.Issues.Issue>().SortBy(args.SortOrder, defaultUniqueSortPropertyName: nameof(XPOIssues.Issues.Issue.Oid)).Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter));
                    var items = queryable.Skip(args.Skip).Take(args.Take * pageTakeCount).ToArray();
                    return DetachedObjectsHelper.ConvertToDetachedObjects(items);
                }
            });
        }
        [DevExpress.Mvvm.DataAnnotations.Command]
        public void GetTotalSummaries(DevExpress.Mvvm.Xpf.GetSummariesAsyncArgs args) {
            args.Result = Task.Run(() =>
            {
                using(var session = new DevExpress.Xpo.Session()) {
                    return session.Query<XPOIssues.Issues.Issue>().Where(MakeFilterExpression((DevExpress.Data.Filtering.CriteriaOperator)args.Filter)).GetSummaries(args.Summaries);
                }
            });
        }

        System.Linq.Expressions.Expression<System.Func<XPOIssues.Issues.Issue, bool>> MakeFilterExpression(DevExpress.Data.Filtering.CriteriaOperator filter) {
            var converter = new DevExpress.Xpf.Data.GridFilterCriteriaToExpressionConverter<XPOIssues.Issues.Issue>();
            return converter.Convert(filter);
        }
    }
}