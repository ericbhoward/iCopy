Module Utilities
    Function MsgBoxWrap(Message As String, Optional Style As MsgBoxStyle = MsgBoxStyle.DefaultButton1, Optional Title As String = "iCopy") As MsgBoxResult
        If My.Settings.Silent = "False" Then
            Return MsgBox(Message, Style, Title)
        Else
            Console.WriteLine(Message)
            Return MsgBoxResult.Cancel
        End If
    End Function
End Module
