Imports System.IO

Public Class Profile

    Public Name As String
    Public Folder As String
    Public LastSave As String

    Public Sub New(fldr As String)
        If IO.Directory.Exists($"{SaveGameDir}\{fldr}") Then
            Folder = fldr
            Dim nametxt As String = $"{SaveGameDir}\{fldr}\name.txt"
            Name = If(File.Exists(nametxt), File.ReadAllText(nametxt), Nothing)
            Dim lastSavetxt As String = $"{SaveGameDir}\{fldr}\lastSave.txt"
            LastSave = If(File.Exists(lastSavetxt), File.ReadAllText(lastSavetxt), Nothing)
        Else
            Folder = fldr
            Name = Nothing
            LastSave = Nothing
        End If
    End Sub

End Class
