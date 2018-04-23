Imports Microsoft.VisualBasic
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System

Namespace DXSample
	Public Class MyGridControl
		Inherits GridControl
		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
			MyBase.RegisterAvailableViewsCore(collection)
			collection.Add(New MyGridViewInfoRegistrator())
		End Sub
	End Class

	Public Class MyGridView
		Inherits GridView
		Public Sub New()
			MyBase.New()
		End Sub
		Public Sub New(ByVal grid As GridControl)
			MyBase.New(grid)
		End Sub

		Friend Const MyGridViewName As String = "MyGridView"
		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return MyGridViewName
			End Get
		End Property

		Private fMaxSelectedRowsCount As Integer = -1
		Public Property MaxSelectedRowsCount() As Integer
			Get
				Return fMaxSelectedRowsCount
			End Get
			Set(ByVal value As Integer)
				If fMaxSelectedRowsCount = value Then
					Return
				End If
				fMaxSelectedRowsCount = value
				ClearSelection()
			End Set
		End Property

		Protected Overrides Sub InvertFocusedRowSelectionCore(ByVal hitInfo As BaseHitInfo)
			If OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect AndAlso MaxSelectedRowsCount > 0 AndAlso MaxSelectedRowsCount <= SelectedRowsCount AndAlso (Not IsRowSelected((CType(hitInfo, GridHitInfo)).RowHandle)) Then
				Return
			End If
			MyBase.InvertFocusedRowSelectionCore(hitInfo)
		End Sub

		Protected Overrides Sub SelectAnchorRangeCore(ByVal controlPressed As Boolean, ByVal allowCells As Boolean)
			If MaxSelectedRowsCount > 0 AndAlso OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect Then
				Dim anchor As Integer = GetVisibleIndex(SelectionAnchorRowHandle)
				Dim focused As Integer = GetVisibleIndex(FocusedRowHandle)
				If controlPressed Then
					Dim selected As Integer = SelectedRowsCount
					Dim start As Integer = Math.Min(anchor, focused)
					Dim [end] As Integer = Math.Max(anchor, focused)
					For i As Integer = start To [end] - 1
						If (Not IsRowSelected(i)) Then
							selected += 1
						End If
					Next i
					If selected >= MaxSelectedRowsCount Then
						Return
					End If
				ElseIf Math.Abs(anchor - focused) >= MaxSelectedRowsCount Then
					Return
				End If
			End If
			MyBase.SelectAnchorRangeCore(controlPressed, allowCells)
		End Sub
	End Class

	Public Class MyGridViewInfoRegistrator
		Inherits GridInfoRegistrator
		Public Sub New()
			MyBase.New()
		End Sub

		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return MyGridView.MyGridViewName
			End Get
		End Property

		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Return New MyGridView(grid)
		End Function
	End Class
End Namespace