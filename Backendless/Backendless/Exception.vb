Public Class Exception
    Inherits System.Exception
    Sub New(Code As Integer, Message As String)
        MyBase.New("Code: " + Code.ToString + vbNewLine + Message)
    End Sub
End Class
