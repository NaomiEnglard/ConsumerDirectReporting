Module Module1

    Sub Main()
        '      testReport(132)

        If CdrApi.runCustomerDetailWithGrade() Then
            Console.WriteLine("Customer update successfull")
        Else
            Console.WriteLine("Customer update failed.  See log for details")
        End If
        'Dim clArgs As String() = Environment.GetCommandLineArgs()
        'Dim rptNo As Integere
        'Integer.TryParse(clArgs(1), rptNo)

        '  These methods run the report and save the results to the DB
        '   CdrApi.runCurrentMemberPublicRecords()
        '  CdrApi.runCurrentMemberCollections()

        'This is more for testing the output of spceific reports.
        ' The csv data will appear in the debug log
        'Dim rptNo As Integer = 5
        'Dim s As String = CdrApi.getReport(rptNo, "11-20-15", "12-01-18")
        'Console.WriteLine(s)

        'Logger.dbg("rptNo = " & CStr(rptNo))

        'Logger.dbg(vbCrLf & s)


    End Sub

    Sub testReport(rptNo As Integer)
        Dim s As String = CdrApi.getReport(rptNo, "11-29-15", "12-01-22")
        Dim fName As String = "c:\dev\logs\Rpt" & CStr(rptNo) & "xx.csv"

        Dim sw As System.IO.StreamWriter
        sw = My.Computer.FileSystem.OpenTextFileWriter(fName, False)
        sw.WriteLine(s)
        sw.Close()
        Console.WriteLine(fName)
        Console.ReadKey()

    End Sub
End Module
