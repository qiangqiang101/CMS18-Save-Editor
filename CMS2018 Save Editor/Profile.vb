Imports System.IO

Public Class Profile

    Public Name As String
    Public Folder As String
    Public LastSave As String

    Public Sub New(fldr As String)
        If IO.Directory.Exists($"{SaveGameDir}\{fldr}") Then
            Folder = fldr
            Name = File.ReadAllText($"{SaveGameDir}\{fldr}\name.txt")
            LastSave = File.ReadAllText($"{SaveGameDir}\{fldr}\lastSave.txt")
        Else
            Folder = fldr
            Name = Nothing
            LastSave = Nothing
        End If
    End Sub

End Class
