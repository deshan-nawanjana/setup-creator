Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If fbb.ShowDialog = Windows.Forms.DialogResult.OK Then
            If fbb.SelectedPath.Length = 3 Then
                MsgBox("Cannot select a partition.", MsgBoxStyle.Exclamation)
            Else
                t5.Text = fbb.SelectedPath
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        op1.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        op2.ShowDialog()
    End Sub

    Private Sub op1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles op1.FileOk
        t6.Text = op1.FileName
    End Sub

    Private Sub op2_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles op2.FileOk
        t7.Text = op2.FileName
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t1.TextChanged
        If t1.Text = "" Then
            t2.Text = ""
            t3.Text = ""
            t4.Text = ""

        Else
            t2.Text = t1.Text & " Setup Wizard"
            t3.Text = "Welcome to the " & t1.Text & " Setup Wizard"
            t4.Text = "The Setup Wizard will install " & t1.Text & " on your computer. Click 'Next' to continue or 'Cancel' to exit the Setup Wizard."

        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If fbb2.ShowDialog = Windows.Forms.DialogResult.OK Then

            t8.Text = fbb2.SelectedPath

        End If
    End Sub


    Private Sub t5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t5.Click

    End Sub

    Private Sub t5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles t5.TextChanged

        If t5.Text = "" Then
        Else
            lb.Items.Clear()
            For Each ff As String In My.Computer.FileSystem.GetFiles( _
               t5.Text, _
                FileIO.SearchOption.SearchAllSubDirectories, "*.*")
                Dim part As String = ff
                Dim wp As String = Replace(part, t5.Text & "\", "")
                lb.Items.Add(wp)
            Next
        End If


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If t1.Text = "" Or t2.Text = "" Or t3.Text = "" Or t4.Text = "" Or la.Text = "" Or t8.Text = "" Or lb.SelectedItem = "" Or t6.Text = "" Or t8.Text = "" Then
            MsgBox("OOOps ! you missed something !", MsgBoxStyle.Exclamation)
        Else
            Dim lx As String = "y"
            If CheckBox1.Checked = True Then
                lx = "y"
            Else
                lx = "n"
            End If


            Button4.Enabled = False
            My.Computer.FileSystem.CreateDirectory(t5.Text & "\" & t1.Text & "\dat\")
            My.Computer.FileSystem.WriteAllText(t5.Text & "\" & t1.Text & "\dat\80000030.dll", _
            t1.Text & "‼" & t2.Text & "‼" & t3.Text & "‼" & t4.Text & "‼" & la.Text & "‼" & t8.Text & "‼" & lb.SelectedItem & "‼" & lx & "‼", False)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\lang.inf", _
                     t5.Text, False)
            System.Diagnostics.Process.Start(Application.StartupPath & "\root.exe")
            tim.Start()

        End If
    End Sub









    Private Sub tim_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tim.Tick
        tim.Stop()
        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\lang.inf") Then
            tim.Start()
        Else
            My.Computer.FileSystem.CreateDirectory(t5.Text & "\" & t1.Text & "\dat")


            My.Computer.FileSystem.CopyFile(t6.Text, Application.StartupPath & "\dat\10000020.dll")
            My.Computer.FileSystem.CopyFile(t7.Text, Application.StartupPath & "\dat\10000030.dll")
            For Each ff As String In My.Computer.FileSystem.GetFiles( _
                         Application.StartupPath & "\dat", _
                          FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                Dim fff As New IO.FileInfo(ff)
                If My.Computer.FileSystem.FileExists(t1.Text & "\dat\" & fff.Name) Then
                    My.Computer.FileSystem.DeleteFile(t1.Text & "\dat\" & fff.Name)
                    My.Computer.FileSystem.MoveFile(ff, t5.Text & "\" & t1.Text & "\dat\" & fff.Name)
                Else
                    My.Computer.FileSystem.MoveFile(ff, t5.Text & "\" & t1.Text & "\dat\" & fff.Name)
                End If
            Next

            My.Computer.FileSystem.CopyFile(Application.StartupPath & "\prcx.dll", t5.Text & "\" & t1.Text & "\prcx.dll")
            My.Computer.FileSystem.CopyFile(Application.StartupPath & "\Setup.dll", t5.Text & "\" & t1.Text & "\Setup.exe")
            MsgBox("Complete buddy !")
            System.Diagnostics.Process.Start(t5.Text & "\" & t1.Text)
            Button4.Enabled = True
        End If
    End Sub

    Private Sub Label13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.Click
        System.Diagnostics.Process.Start("http://www.deshanblogs.blogspot.com/")
    End Sub
End Class
