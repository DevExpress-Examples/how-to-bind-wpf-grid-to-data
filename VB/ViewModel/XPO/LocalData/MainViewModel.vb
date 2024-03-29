﻿Imports DevExpress.Mvvm
Imports XPOIssues.Issues
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpo
Imports System.Linq
Imports System.Collections.Generic

Public Class MainViewModel
    Inherits ViewModelBase
    Private _UnitOfWork As UnitOfWork
    Private _ItemsSource As IList(Of User)
    Public ReadOnly Property ItemsSource As IList(Of User)
        Get
            If _ItemsSource Is Nothing AndAlso Not DevExpress.Mvvm.ViewModelBase.IsInDesignMode Then
                _UnitOfWork = New UnitOfWork()
                Dim xpCollection = New XPCollection(Of User)(_UnitOfWork)
                xpCollection.Sorting.Add(New SortProperty(NameOf(User.Oid), DevExpress.Xpo.DB.SortingDirection.Ascending))
                _ItemsSource = xpCollection
            End If
            Return _ItemsSource
        End Get
    End Property

End Class