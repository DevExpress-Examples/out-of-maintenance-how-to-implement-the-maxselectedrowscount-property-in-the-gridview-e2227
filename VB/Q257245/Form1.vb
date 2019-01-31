Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Q257245
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim list = New BindingList(Of Item)()
            For i As Integer = 0 To 9
                list.Add(New Item() With { _
                    .ID = i, _
                    .Name = "Name" & i _
                })
            Next i
            myGridControl1.DataSource = list
        End Sub
    End Class
    Public Class Item
        Public Property ID() As Integer
        Public Property Name() As String
    End Class
End Namespace