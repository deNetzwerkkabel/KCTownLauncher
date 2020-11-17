Imports System.Security.Principal
Public Class GameLauncher
    Private Const UrlString As String = "C:/Program Files/GameLauncher/WEB/index.html"


    Public Function isConnectionAvailable() As Boolean
        Dim objURL As New System.Uri("https://github.com")
        Dim objWebReq As System.Net.WebRequest
        objWebReq = System.Net.WebRequest.Create(objURL)
        Dim objResp As System.Net.WebResponse
        Try
            objResp = objWebReq.GetResponse
            objResp.Close()
            objResp = Nothing
            Return True
        Catch ex As Exception
            objResp = Nothing
            objWebReq = Nothing
            Return False
        End Try
    End Function


    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim inhalt As String = My.Computer.FileSystem.ReadAllText("C:\Program Files\GameLauncher\apple.install")
        If inhalt = "Installiert" Then
            Shell("python3 test.py")
            Label1.Text = "Bloody Sucuk gestartet"
        Else
            Dim Python = MsgBox("Während des Downloads wird die Python-Installation gestartet. Bitte führe alle weiteren Installationsschritte durch", vbOKOnly + vbInformation, "Information")
            If Python = vbOK Then
                If ComboBox1.SelectedItem IsNot Nothing Then
                    Dim Spiel As String
                    Spiel = ComboBox1.SelectedItem.ToString()
                    If isConnectionAvailable() = True Then
                        Label1.Text = "Downloade spiel.py"
                        My.Computer.Network.DownloadFile("http://mortus.de/games/launcher/download/bloodysucuk/Bloody_Sucuk.py", "C:\Program Files\GameLauncher\python\bloody_sucuk\Bloody_Sucuk.py")
                    Else
                        Dim internet = MsgBox("Zum Download von Bloody Sucuk wird eine Internetverbindung benötigt.", vbOKOnly + vbCritical, "Kein Internet")
                        Label1.Text = "Vorgang abgebrochen: Kein Internet"
                    End If

                Else
                    Dim nichtsgewählt = MsgBox("Kein Spiel ausgewählt", vbOKOnly + vbCritical, "Kein Spiel")
                    Label1.Text = "Vorgang abgebrochen: Spiel"

                End If

            End If
        End If
    End Sub

    Private Sub GameLauncher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isConnectionAvailable() = True Then
            WebBrowser1.Navigate("http://mortus.de/games/launcher/version/1.0/webDATA")
        Else
            WebBrowser1.Navigate("C:\Program Files\GameLauncher\index.html")
        End If
        ComboBox1.Items.Insert(0, "Bloody Sucuk")
    End Sub

End Class
