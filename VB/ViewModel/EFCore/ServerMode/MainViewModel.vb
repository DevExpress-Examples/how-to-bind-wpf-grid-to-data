Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Data
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore

Public Class MainViewModel
    Inherits ViewModelBase
    Private _ServerModeSource As DevExpress.Data.Linq.EntityServerModeSource

    Public ReadOnly Property ServerModeSource As DevExpress.Data.Linq.EntityServerModeSource
        Get
            If _ServerModeSource Is Nothing Then
                Dim context = New Issues.IssuesContext()
                _ServerModeSource = New DevExpress.Data.Linq.EntityServerModeSource With {
                    .KeyExpression = NameOf(Issues.Issue.Id),
                    .QueryableSource = context.Issues.AsNoTracking()
                }
            End If
            Return _ServerModeSource
        End Get
    End Property

End Class