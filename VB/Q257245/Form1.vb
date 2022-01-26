Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Q257245

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim list = New BindingList(Of Item)()
            For i As Integer = 0 To 10 - 1
                list.Add(New Item() With {.ID = i, .Name = "Name" & i})
            Next

            myGridControl1.DataSource = list
        End Sub
    End Class

    Public Class Item

        Public Property ID As Integer

        Public Property Name As String
    End Class
End Namespace
