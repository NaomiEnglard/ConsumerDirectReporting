﻿Imports EasyHttp
Imports EasyHttp.Http
Imports System.Dynamic


Imports System.Security.Cryptography.X509Certificates
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Text



Public Class CdrApi
    'Protected Shared userName As String = "d.bergida"
    'Protected Shared userPassword As String = "Webs0urce17"
    Protected Shared userName As String = "j.singer"
    Protected Shared userPassword As String = "revazRC1973"
    '  Column numbers for CustomerDetailWithGrade report (132)
    Protected Shared colSignupDate As Integer = -1
    Protected Shared colMemberId As Integer = -1
    Protected Shared colFrodoId As Integer = -1
    Protected Shared colSignupMethod As Integer = -1
    Protected Shared colEmail As Integer = -1
    Protected Shared colName As Integer = -1
    Protected Shared colZip As Integer = -1

    Protected Shared conn As SqlConnection
    Protected Shared quote As String = """"
    Protected Shared quoteFieldPattern As String = ""
    Protected Shared quoteFieldRx As Regex = Nothing



    Protected Shared Function getQuoteFieldRx() As Regex
        If Not IsNothing(quoteFieldRx) Then
            Return quoteFieldRx
        End If
        Dim sb As New StringBuilder
        sb.Append("^(?<first>.+)")
        sb.Append(quote).Append("(?<f1>[^,]+),(?<f2>[^""]+)").Append(quote)
        sb.Append("(?<last>.+)$")
        quoteFieldPattern = sb.ToString()
     

        quoteFieldRx = New Regex(quoteFieldPattern)
        Return quoteFieldRx


    End Function









    Public Shared Function getReportLines(rptId As Integer, startDate As Date, endDate As Date) As String()
        Dim res As String = getReport(rptId, startDate, endDate)

        If res.Contains("error(s)") Then
            Logger.err("Unable to run report")
            Return Nothing
        End If
        Dim Lines() As String = res.Split(Environment.NewLine.ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)
        Return Lines

    End Function
    Public Shared Function getReportLines(rptId As Integer, startDate As String, endDate As String) As String()
        Dim res As String = getReport(rptId, startDate, endDate)

        If res.Contains("error(s)") Then
            Logger.err("Unable to run report " & CStr(rptId))
            Logger.err(res)

            Return Nothing
        End If
        Dim Lines() As String = res.Split(Environment.NewLine.ToCharArray, System.StringSplitOptions.RemoveEmptyEntries)
        Return Lines


    End Function
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


    Public Shared Function runCustomerDetailWithGrade() As Boolean
        'Run report 132
        'For each record, get  
        '  dateRangeFilter (SmartCredit SignupDate)
        '  MemberID  (SmartCredit member ID)
        '  ADID (FrodoId)
        '  AID - New column in clients table - SmartCreditSignupMethod
        '  email - (Used to match in case Frodo Id is missing
        '  Name -  Used for exception email in case we can't match the client
        '  Zip - Used for exception email in case we can't match the client
        '  For no-match, send:  Name, Zip, MemberId, Signupdate
        Dim rptNo As Integer
        rptNo = 132
        Dim endDate As Date = Date.Now
        Dim startDate As Date = Date.Now.AddDays(-30)
        Dim lines As String() = getReportLines(rptNo, startDate, endDate)
        If IsNothing(lines) Then
            Logger.err("Unable to create report 142")
        End If
        Dim nLines As Integer = lines.Length

        Logger.dbg("Number of report lines " & CStr(nLines))
        ' Find report columns for our data

        ' Parse top line to find the column numbers for our data
        Dim colNames As String() = lines(0).Split(New Char() {","c})
        Dim i As Integer
        Dim nCols As Integer = colNames.Length
        Logger.dbg("nCols = " & CStr(nCols))
        For i = 0 To nCols - 1
            Dim cName As String = colNames(i)
            Logger.dbg("Column number " & CStr(i) & "[" & cName & "]")
            If cName = "Member ID" Then colMemberId = i
            If cName = "dateRangeFilter" Then colSignupDate = i
            If cName = "ADID" Then colFrodoId = i
            If cName = "AID" Then colSignupMethod = i
            If cName = "email" Then colEmail = i
            If cName = "Name" Then colName = i
            If cName = "ZIP" Then colZip = i
        Next
        '  Check for column name mismatches
        If colMemberId = -1 Then
            Logger.err("Member ID column missing")
            Return False
        End If
        If colSignupDate = -1 Then
            Logger.err("dateRangeFilter column missing")
            Return False
        End If
        If colFrodoId = -1 Then
            Logger.err("ADID column missing")
            Return False
        End If
        If colSignupMethod = -1 Then
            Logger.err("AID column missing")
            Return False
        End If
        If colEmail = -1 Then
            Logger.err("email column missing")
            Return False
        End If
        If colName = -1 Then
            Logger.err("Name column missing")
            Return False
        End If
        If colZip = -1 Then
            Logger.err("ZIP column missing")
            Return False
        End If
        conn = New SqlConnection(ConfigurationManager.ConnectionStrings("frodo").ConnectionString)
        conn.Open()
        Try
            For i = 1 To nLines - 1
                Dim line = lines(i)
                Logger.dbg("Line number " & CStr(i) & " " & line)
                processCustomerDetailLine(line)
            Next

        Catch ex As Exception
            Logger.err("processCustomerDetailLine threw an exception")
            Logger.err("  Message: " & ex.Message)
            Logger.err(ex.StackTrace)
            Return False
        Finally
            conn.Close()


        End Try
        Return True

    End Function
    Public Shared Sub processCustomerDetailLine(line As String)
        Dim signupDate As Date = Nothing
        Dim memberId As String = ""
        Dim frodoId As String = ""
        Dim signupMethod As String = ""
        Dim email As String = ""
        Dim clientName As String = ""
        Dim zip As String = ""

        'Ignore the comma in quoted fields.
        Dim rx As Regex = getQuoteFieldRx()
        Dim match = rx.Match(line)
        If match.Success Then
            Dim sb As New StringBuilder
            sb.Append(match.Groups("first")).Append(match.Groups("f1")).Append("-----")
            sb.Append(match.Groups("f2")).Append(match.Groups("last"))
            Dim replacementString = sb.ToString
            line = rx.Replace(line, replacementString)
        End If



        Dim fields As String() = line.Split(New Char() {","c})
        Dim signupDateStr As String = fields(colSignupDate)
        Date.TryParse(signupDateStr, signupDate)
        memberId = fields(colMemberId)
        frodoId = fields(colFrodoId)
        If frodoId <> "" Then frodoId = frodoId.Replace("F", "")
        signupMethod = fields(colSignupMethod)
        email = fields(colEmail)
        clientName = fields(colName)
        zip = fields(colZip)
        Dim frodoIdVal As Integer
        If Not Integer.TryParse(frodoId, frodoIdVal) Then
            frodoIdVal = 0
        End If
        If Not updateClientScData(frodoIdVal, signupDate, memberId, signupMethod, email) Then
            sendExceptionEmail(frodoIdVal, email, clientName, zip)
        End If

    End Sub
    Protected Shared Function updateClientScData(frodoId As Integer, signupDate As Date, memberId As String, signupMethod As String, email As String) As Boolean

        If IsNothing(signupDate) Or memberId = "" Or signupMethod = "" Then
            Logger.err("updateClientScData missing parameter")
            Return False
        End If
        Dim cmd As SqlCommand = New SqlCommand("UpdateClientSCInfo", conn)
        cmd.CommandType = CommandType.StoredProcedure
        With cmd.Parameters
            .Add("@frodoId", SqlDbType.Int).Value = frodoId
            .Add("@memberId", SqlDbType.VarChar).Value = memberId
            .Add("@signupDate", SqlDbType.Date).Value = signupDate
            .Add("@signupMethod", SqlDbType.VarChar).Value = signupMethod
            .Add("@email", SqlDbType.VarChar).Value = email
        End With
        Dim paramReturnCode = cmd.Parameters.Add("@returnCode", SqlDbType.Int)
        paramReturnCode.Direction = ParameterDirection.Output

        cmd.ExecuteNonQuery()
        Dim returnCode = paramReturnCode.Value
        If returnCode < 0 Then
            Return False

        End If


        Return True

    End Function
    Protected Shared Sub sendExceptionEmail(frodoId As Integer, email As String, clientName As String, zip As String)
        clientName = clientName.Replace("-----", ",")
        Logger.err("Unable to match frodoId = " & CStr(frodoId) & " email = " & email & " clientName = " & clientName)

        ' TODO
    End Sub

End Class
