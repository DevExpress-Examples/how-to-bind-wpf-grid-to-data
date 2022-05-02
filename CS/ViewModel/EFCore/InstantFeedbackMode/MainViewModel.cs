using DevExpress.Mvvm;
using EFCoreIssues.Issues;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Data.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections;

namespace EFCoreIssues {
    public class MainViewModel : ViewModelBase {
        EntityInstantFeedbackSource _ItemsSource;
        public EntityInstantFeedbackSource ItemsSource {
            get
            {
                if(_ItemsSource == null) {
                    _ItemsSource = new EntityInstantFeedbackSource {
                        KeyExpression = nameof(Issue.Id)
                    };
                    _ItemsSource.GetQueryable += (sender, e) => {
                        var context = new IssuesContext();
                        e.QueryableSource = context.Issues.AsNoTracking();
                    };
                }
                return _ItemsSource;
            }
        }
        IList _Users;
        public IList Users {
            get
            {
                if(_Users == null && !DevExpress.Mvvm.ViewModelBase.IsInDesignMode) {
                    var context = new IssuesContext();
                    _Users = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
                }
                return _Users;
            }
        }
    }
}