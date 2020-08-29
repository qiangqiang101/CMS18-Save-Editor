Imports System.ComponentModel
Imports System.IO

Public Class frmSave

    Public Profiles As New List(Of Profile)
    Public SelectedProfile As Profile = Nothing

    Private moneyMemLoc As Integer = &HD4
    Private levelMemLoc As Integer = &HD8
    Private xpMemLoc As Integer = &HDC
    Private barnMemLoc As Integer = &HE0


    Private Sub frmSave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProfiles()
        lblCopyright.Text = My.Application.Info.Copyright
    End Sub

    Private Sub LoadProfiles()
        Dim di As New DirectoryInfo(SaveGameDir)
        For Each folder In di.GetDirectories()
            Select Case folder.ToString
                Case "Unity", "Crashes"
                Case Else
                    Profiles.Add(New Profile(folder.ToString))
            End Select
        Next

        For Each pf In Profiles
            If Not cmbProfile.Items.Contains($"{pf.Name} ({pf.Folder})") Then cmbProfile.Items.Add($"{pf.Name} ({pf.Folder})")
        Next

        If Profiles.Count <> 0 Then cmbProfile.SelectedIndex = 0
    End Sub

    Private Sub cmbProfile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProfile.SelectedIndexChanged
        SelectedProfile = Profiles.Find(Function(x) cmbProfile.SelectedItem = $"{x.Name} ({x.Folder})")
        GroupBox1.Enabled = True
        lblLastSave.Text = $"Last Save {SelectedProfile.LastSave}"
        LoadGlobalSaveFile(SelectedProfile.Folder)
    End Sub

    Private Sub LoadGlobalSaveFile(folder As String)
        Dim fullPath As String = $"{SaveGameDir}\{folder}\{GlobalData}"

        If File.Exists(fullPath) Then
            Using br As New BinaryReader(File.Open(fullPath, FileMode.Open))
                br.BaseStream.Seek(moneyMemLoc, 0)
                txtMoney.Text = br.ReadInt32()

                br.BaseStream.Seek(levelMemLoc, 0)
                txtLevel.Text = br.ReadInt32()

                br.BaseStream.Seek(xpMemLoc, 0)
                txtXP.Text = br.ReadInt32()

                br.BaseStream.Seek(barnMemLoc, 0)
                txtBarn.Text = br.ReadInt32
            End Using
        End If
    End Sub

    Private Sub SaveGlobalSaveFile(folder As String)
        Dim fullPath As String = $"{SaveGameDir}\{folder}\{GlobalData}"
        If File.Exists(fullPath) Then
            Using bw As New BinaryWriter(File.Open(fullPath, FileMode.Open))
                bw.BaseStream.Seek(moneyMemLoc, 0)
                bw.Write(CInt(txtMoney.Text))

                bw.BaseStream.Seek(levelMemLoc, 0)
                bw.Write(CInt(txtLevel.Text))

                bw.BaseStream.Seek(xpMemLoc, 0)
                bw.Write(CInt(txtXP.Text))

                bw.BaseStream.Seek(barnMemLoc, 0)
                bw.Write(CInt(txtBarn.Text))
            End Using
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveGlobalSaveFile(SelectedProfile.Folder)
        MsgBox("Save file modified completed.", MsgBoxStyle.Information, "Save")
    End Sub

    Private Sub TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMoney.KeyPress, txtLevel.KeyPress, txtXP.KeyPress, txtBarn.KeyPress
        Dim allowedChars As String = $"0123456789{Chr(Keys.Back)}{Chr(Keys.Delete)}"
        If Not allowedChars.Contains(e.KeyChar) Then
            e.KeyChar = ChrW(0)
            e.Handled = True
        End If
    End Sub

    Private Sub frmSave_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        e.Cancel = True
        MsgBox($"Car Mechanic Simulator 2018 Save Editor v1.0{vbNewLine}For CMS2018 v1.6.5{vbNewLine}Created by I'm Not MentaL{vbNewLine}{vbNewLine}https://www.imnotmental.com/", MsgBoxStyle.OkOnly, "About")
    End Sub
End Class
