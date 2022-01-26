Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports System
Imports System.ComponentModel
Imports System.Drawing

Namespace DXSample

    <ToolboxItem(True)>
    Public Class MyGridControl
        Inherits GridControl

        Protected Overrides Function CreateDefaultView() As BaseView
            Return CreateView("MyGridView")
        End Function

        Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
            MyBase.RegisterAvailableViewsCore(collection)
            collection.Add(New MyGridViewInfoRegistrator())
        End Sub
    End Class

    Public Class MyGridViewInfoRegistrator
        Inherits GridInfoRegistrator

        Public Overrides ReadOnly Property ViewName As String
            Get
                Return "MyGridView"
            End Get
        End Property

        Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
            Return New MyGridView(grid)
        End Function
    End Class

    Public Class MyGridView
        Inherits Views.Grid.GridView

        Public Sub New()
        End Sub

        Public Sub New(ByVal grid As GridControl)
            MyBase.New(grid)
        End Sub

        Protected Overrides ReadOnly Property ViewName As String
            Get
                Return "MyGridView"
            End Get
        End Property

        <DefaultValue(-1)>
        Public Property MaxSelectedRowsCount As Integer = -1

        Public Overrides Sub SelectRow(ByVal rowHandle As Integer)
            If SelectedRowsCount = MaxSelectedRowsCount Then Return
            MyBase.SelectRow(rowHandle)
        End Sub

        Public Overrides Sub SelectRange(ByVal startRowHandle As Integer, ByVal endRowHandle As Integer)
            If startRowHandle = SelectionAnchorRowHandle AndAlso MaxSelectedRowsCount > -1 Then
                If endRowHandle >= startRowHandle Then
                    endRowHandle = Math.Min(endRowHandle, startRowHandle + MaxSelectedRowsCount - 1)
                Else
                    endRowHandle = Math.Max(endRowHandle, startRowHandle - MaxSelectedRowsCount + 1)
                End If
            End If

            MyBase.SelectRange(startRowHandle, endRowHandle)
        End Sub

        Public Overrides Sub SelectAll()
            If MaxSelectedRowsCount = -1 Then MyBase.SelectAll()
        End Sub
    End Class
End Namespace
