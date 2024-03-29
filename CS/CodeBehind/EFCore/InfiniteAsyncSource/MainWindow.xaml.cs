﻿using System.Windows;
using EFCoreIssues.Issues;
using System;
using System.Linq.Expressions;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevExpress.Xpf.Grid;

namespace EFCoreIssues {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            var source = new InfiniteAsyncSource {
                ElementType = typeof(Issue),
                KeyProperty = nameof(Issue.Id)
            };
            source.FetchRows += OnFetchRows;
            source.GetTotalSummaries += OnGetTotalSummaries;
            grid.ItemsSource = source;
            LoadLookupData();
        }

        Expression<Func<Issue, bool>> MakeFilterExpression(CriteriaOperator filter) {
            var converter = new GridFilterCriteriaToExpressionConverter<Issue>();
            return converter.Convert(filter);
        }

        void OnFetchRows(object sender, FetchRowsAsyncEventArgs e) {
            e.Result = Task.Run<FetchRowsResult>(() => {
                var context = new IssuesContext();
                var queryable = context.Issues.AsNoTracking()
                    .SortBy(e.SortOrder, defaultUniqueSortPropertyName: nameof(Issue.Id))
                    .Where(MakeFilterExpression(e.Filter));
                return queryable.Skip(e.Skip).Take(e.Take ?? 100).ToArray();
            });
        }

        void OnGetTotalSummaries(object sender, GetSummariesAsyncEventArgs e) {
            e.Result = Task.Run(() => {
                var context = new IssuesContext();
                var queryable = context.Issues.Where(MakeFilterExpression(e.Filter));
                return queryable.GetSummaries(e.Summaries);
            });
        }

        void LoadLookupData() {
            var context = new IssuesContext();
            usersLookup.ItemsSource = context.Users.Select(user => new { Id = user.Id, Name = user.FirstName + " " + user.LastName }).ToArray();
        }
    }
}
