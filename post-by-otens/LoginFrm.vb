﻿Public Class LoginFrm





    Private Sub SignIn_bttm_Click(sender As Object, e As EventArgs) Handles SignIn_bttm.Click


        Dim businessName As String
            businessName = bName.Text
        '   If logging in as manager
        If asManager.Checked = False Then
            Dim managerUsername = Username_txt.Text
            Dim managerPword = Password_txt.Text
            Dim adapter As New POSDataSetTableAdapters.businessTableAdapter
            If adapter.validateManager(businessName, managerPword, managerUsername) <> 0 Then
                RegisterTab.Show()
                Me.Close()
            End If
        Else
            Dim registerU = Username_txt.Text
            Dim registerP = Password_txt.Text
            Dim adapter As New POSDataSetTableAdapters.businessTableAdapter
            If adapter.validateRegister(businessName, registerP, registerU) <> 0 Then
                RegisterTab.Show()
                Me.Close()
            End If
        End If

        'If Not Username_txt.Text.Equals(u) Or Not Password_txt.Text.Equals(p) Then
        '    ErrorProvider1.SetError(SignIn_bttm, "Wrong Username or Password")
        'Else
        '    RegisterTab.Show()
        '    Me.Close()
        'End If



    End Sub
End Class