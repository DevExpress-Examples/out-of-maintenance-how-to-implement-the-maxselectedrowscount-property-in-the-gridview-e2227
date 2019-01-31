Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Q257245
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim list = New BindingList(Of Item)()
			For i As Integer = 0 To 9
				list.Add(New Item() With {.ID = i, .Name = "Name" & i})
			Next i
			myGridControl1.DataSource = list
		End Sub
	End Class
	Public Class Item
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
	End Class
End Namespace