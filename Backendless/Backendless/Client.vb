Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports Newtonsoft.Json.Linq
Public Class Client
    Public Const Server As String = "http://api.backendless.com/"
    Public ReadOnly Property ApplicationId As String
    Public ReadOnly Property SecretKey As String
    Public ReadOnly Property Version As String
    Public Function CreateRequest(Url As String, Method As String, ContentType As String, Input As String) As JObject
        Dim Request As HttpWebRequest = HttpWebRequest.Create(Server + Version + "/" + Url)
        Request.Headers.Add("application-id", ApplicationId)
        Request.Headers.Add("secret-key", SecretKey)
        Request.Method = Method
        Request.ContentType = ContentType
        Request.Headers.Add("application-type", "REST")
        Dim RequestWriter As New StreamWriter(Request.GetRequestStream)
        RequestWriter.Write(Input)
        RequestWriter.Close()
        Dim Response As HttpWebResponse = Nothing
        Dim IsError As Boolean = False
        Try
            Response = Request.GetResponse
        Catch ex As WebException
            Response = ex.Response
            IsError = True
        End Try
        Dim ResponseReader As New StreamReader(Response.GetResponseStream)
        Dim Output As String = ResponseReader.ReadToEnd
        ResponseReader.Close()
        If Not IsError Then
            Return JObject.Parse(Output)
        Else
            Dim Exception = JObject.Parse(Output)
            Throw New Exception(Exception("code"), Exception("message"))
        End If
    End Function
    Public Function CreateRequest(Url As String, Method As String, ContentType As String, Input As JObject) As JObject
        Return CreateRequest(Url, Method, ContentType, Input.ToString(Newtonsoft.Json.Formatting.None))
    End Function
    Sub New(ApplicationId As String, SecretKey As String, Optional Version As String = "v1")
        Me.ApplicationId = ApplicationId
        Me.SecretKey = SecretKey
        Me.Version = Version
    End Sub
End Class
