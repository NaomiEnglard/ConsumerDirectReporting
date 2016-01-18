Imports EasyHttp
Imports EasyHttp.Http
Imports System.Dynamic


Imports System.Security.Cryptography.X509Certificates
Imports System.Data.SqlClient
Imports System.Configuration



Public Class CdrApi
    Protected Shared userName As String = "d.bergida"
    Protected Shared userPassword As String = "Webs0urce17"



    Public Shared Function getReport(reportId As Integer, startDate As Date, enddate As Date) As String

        Dim o As Object = New ExpandoObject()

        o.userName = userName
        o.userPassword = userPassword
        Dim client As New HttpClient()
        '    

        Dim cert As X509Certificate = New X509Certificate("c:\dev\certs\cert3.cer", "l2R019a1C5w")
        'Dim cert As X509Certificate = X509Certificate.CreateFromCertFile("c:\dev\certs\certs.cer")


        Dim cc As X509CertificateCollection = New X509CertificateCollection()
        cc.Add(cert)
        client.AddClientCertificates(cc)


        Dim url = getUrl(reportId, startDate, enddate)
        '  Console.WriteLine(url)



        Dim response As HttpResponse = client.Post(url, o, HttpContentTypes.ApplicationXWwwFormUrlEncoded)
        Dim res As String = response.RawText


        Return res

    End Function

    Public Shared Function getReport(reportId As Integer, startDateSt As String, endDateSt As String) As String
        Dim res As String = ""
        Dim startDate As Date = Now()
        Dim endDate As Date = Now()
        If Not Date.TryParse(startDateSt, startDate) Then
            Return "Unable to parse start date " & startDateSt

        End If
        If Not Date.TryParse(endDateSt, endDate) Then
            Return "Unable to parse end date " & endDateSt

        End If
        res = getReport(reportId, startDate, endDate)
        Return res

    End Function
    Protected Shared Function getUrl(reportId As Integer, startDate As Date, endDate As Date) As String
        Dim url As String = "https://supportlink.consumerdirect.com/direct/report/process.htm?reportId=" & CStr(reportId)
        url = url & "&startDate=" & startDate.ToString("yyyy-MM-dd") & "&endDate=" & endDate.ToString("yyy-MM-dd")


        Return url

    End Function

    Public Shared Function getRecentMemberCollections(res As String) As RecentMemberCollections()
        If res.Contains("error(s)") Then
            Logger.err("Unable to run report")
            Return Nothing
        End If
        Dim Lines() As String = res.Split(Environment.NewLine.ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)

        Dim rmcList As RecentMemberCollections()
        ReDim rmcList(Lines.Length)



        Dim i
        For i = 1 To Lines.Length - 1

            Dim rmc As New RecentMemberCollections(Lines(i))
            Dim s As String = rmc.dbg()
            Logger.dbg("rmc ( " & i & ") = " & s)
            rmcList(i - 1) = rmc

        Next
        Return rmcList

    End Function


    Public Shared Function getRecentMemberPublicRecords(res As String) As RecentMemberPublicRecords()
        If res.Contains("error(s)") Then
            Logger.err("Unable to run report")
            Return Nothing
        End If
        Dim Lines() As String = res.Split(Environment.NewLine.ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)

        Dim rprList As RecentMemberPublicRecords()
        ReDim rprList(Lines.Length)



        Dim i
        For i = 1 To Lines.Length - 1

            Dim rpr As New RecentMemberPublicRecords(Lines(i))
            Dim s As String = rpr.dbg()
            Logger.dbg("rpr ( " & i & ") = " & s)
            rprList(i - 1) = rpr

        Next
        Return rprList

        End
    End Function



    Public Shared Sub saveRmcListToTable(rmcList As RecentMemberCollections())
        If IsNothing(rmcList) Then
            Logger.err("Unable to get rmc list")
            Return
        End If
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("frodo").ConnectionString)
        conn.Open()
        Dim i = 0
        For i = 0 To rmcList.Length - 1
            Dim rmc As RecentMemberCollections = rmcList(i)
            If Not IsNothing(rmc) Then

                rmc.addToTable(conn)
            End If


        Next
        conn.Close()

    End Sub

    Public Shared Sub saveRmpListToTable(rprList As RecentMemberPublicRecords())
        If IsNothing(rprList) Then
            Logger.err("Unable to get rpr list")
            Return
        End If
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("frodo").ConnectionString)
        conn.Open()
        Dim i = 0
        For i = 0 To rprList.Length - 1
            Dim rpr As RecentMemberPublicRecords = rprList(i)
            If Not IsNothing(rpr) Then

                rpr.addToTable(conn)
            End If


        Next
        conn.Close()

    End Sub
    Public Shared Sub runCurrentMemberCollections()
        Dim rptNo As Integer
        rptNo = 122
        Dim endDate As Date = Date.Now
        Dim startDate As Date = Date.Now.AddDays(-30)


        Dim res As String = CdrApi.getReport(rptNo, startDate, endDate)
        Logger.dbg("rptNo = " & rptNo)
        Logger.dbg("res = " & res)

        Dim rmcList As RecentMemberCollections() = CdrApi.getRecentMemberCollections(res)

        ' Console.WriteLine("Saving rmcList")
        CdrApi.saveRmcListToTable(rmcList)
    End Sub
    Public Shared Sub runCurrentMemberPublicRecords()
        Dim rptNo As Integer
        rptNo = 124
        Dim endDate As Date = Date.Now
        Dim startDate As Date = Date.Now.AddDays(-30)


        Dim res As String = CdrApi.getReport(rptNo, startDate, endDate)
        Logger.dbg("rptNo = " & rptNo)
        Logger.dbg("res = " & res)

        Dim rmpList As RecentMemberPublicRecords() = CdrApi.getRecentMemberPublicRecords(res)

        ' Console.WriteLine("Saving rmpList")
        CdrApi.saveRmpListToTable(rmpList)
    End Sub
End Class
