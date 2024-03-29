﻿using DevExpress.Mvvm;
using EntityFrameworkIssues.Issues;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Linq.Expressions;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using DevExpress.Mvvm.Xpf;
using System.Collections;

namespace EntityFrameworkIssues {
    public class MainViewModel : ViewModelBase {

        Expression<Func<Issue, bool>> MakeFilterExpression(CriteriaOperator filter) {
            var converter = new GridFilterCriteriaToExpressionConverter<Issue>();
            return converter.Convert(filter);
        }
        [Command]
        public void FetchRows(FetchRowsAsyncArgs args) {
            args.Result = Task.Run<FetchRowsResult>(() => {
                var context = new IssuesContext();
                var queryable = context.Issues.AsNoTracking()
                    .SortBy(args.SortOrder, defaultUniqueSortPropertyName: nameof(Issue.Id))
                    .Where(MakeFilterExpression((CriteriaOperator)args.Filter));
                return queryable.Skip(args.Skip).Take(args.Take ?? 100).ToArray();
            });
        }
        [Command]
        public void GetTotalSummaries(GetSummariesAsyncArgs args) {
            args.Result = Task.Run(() => {
                var context = new IssuesContext();
                var queryable = context.Issues.Where(MakeFilterExpression((CriteriaOperator)args.Filter));
                return queryable.GetSummaries(args.Summaries);
            });
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