Imports Newtonsoft.Json.Linq
Imports Zds.Database.Backendless
Module Main
    Sub Main()
        Dim Client As New Client("BC609108-C4F6-AB39-FF5F-AF67EEF12300", "7B7ACFB9-7425-2CD6-FF84-9061CC74FF00")
        Dim Input As New JObject
        Input("name") = "my name"
        Input("email") = "myemail@gmail.com"
        Input("password") = "my password"
        Dim Response = Client.CreateRequest("users/register", "POST", "application/json", Input)
        Console.Write(Response.ToString(Newtonsoft.Json.Formatting.None))
        Console.ReadKey()
    End Sub
End Module
