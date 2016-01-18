Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Configuration

Public Class RecentMemberPublicRecords
 Public dateRangeFilter as date
    Public publisherId As String
    Public Customer_Id As String
    Public Credit_Event_Date As Date
    Public Member_Date As Date
    Public Member_City As String
    Public Member_State As String
    Public Member_Phone As String
    Public Member_Code As String
    Public Industry_Code As String
    Public Public_Record_Type As String
    Public Docket_Number As String
    Public Attorney As String
    Public Plaintif As String
    Public Filed_Date As Date
    Public Paid_Date As Date
    Public Effective_Date As Date
    Public Asset As Int32
    Public Liabilities_Amount As Int32
    Public Original_Balance As Int32
    Public Current_Balance As Int32
    Public ECOA_Designator As String
    Public Public_Record_Source_Code As String
    Public Court_Location_City As String
    Public Court_Location_State As String


    Private fields() As String
    Private Shared regTime As New Regex(" \d\d:\d\d:\d\d\.\d")

    Protected Shared sqlQuery As String = ""
    Public Shared fieldNames() As String = _
{ _
"dateRangeFilter", _
"publisherId", _
"Customer_Id", _
"Credit_Event_Date", _
"Member_Date", _
"Member_City", _
"Member_State", _
"Member_Phone", _
"Member_Code", _
"Industry_Code", _
"Public_Record_Type", _
"Docket_Number", _
"Attorney", _
"Plaintif", _
"Filed_Date", _
"Paid_Date", _
"Effective_Date", _
"Asset", _
"Liabilities_Amount", _
"Original_Balance", _
"Current_Balance", _
"ECOA_Designator", _
"Public_Record_Source_Code", _
"Court_Location_City", _
"Court_Location_State" _
}



    Public Sub New()

    End Sub
    Public Sub New(dLine As String)
        populate(dLine)
    End Sub
    Public Sub populate(dLine As String)
        fields = dLine.Split(New Char() {","c})
        Dim i As Integer = 0
    dateRangeFilter = getDate(fields(i))
        i = i + 1
        publisherId = getString(fields(i))
        i = i + 1
        Customer_Id = getString(fields(i))
        i = i + 1
        Credit_Event_Date = getDate(fields(i))
        i = i + 1
        Member_Date = getDate(fields(i))
        i = i + 1
        Member_City = getString(fields(i))
        i = i + 1
        Member_State = getString(fields(i))
        i = i + 1
        Member_Phone = getString(fields(i))
        i = i + 1
        Member_Code = getString(fields(i))
        i = i + 1
        Industry_Code = getString(fields(i))
        i = i + 1
        Public_Record_Type = getString(fields(i))
        i = i + 1
        Docket_Number = getString(fields(i))
        i = i + 1
        Attorney = getString(fields(i))
        i = i + 1
        Plaintif = getString(fields(i))
        i = i + 1
        Filed_Date = getDate(fields(i))
        i = i + 1
        Paid_Date = getDate(fields(i))
        i = i + 1
        Effective_Date = getDate(fields(i))
        i = i + 1
        Asset = getInt(fields(i))
        i = i + 1
        Liabilities_Amount = getInt(fields(i))
        i = i + 1
        Original_Balance = getInt(fields(i))
        i = i + 1
        Current_Balance = getInt(fields(i))
        i = i + 1
        ECOA_Designator = getString(fields(i))
        i = i + 1
        Public_Record_Source_Code = getString(fields(i))
        i = i + 1
        Court_Location_City = getString(fields(i))
        i = i + 1
        Court_Location_State = getString(fields(i))




    End Sub

    Private Shared Function getSqlString() As String

        If sqlQuery <> "" Then
            Return sqlQuery
        End If
        Dim sb As New StringBuilder

        sb.AppendLine("BEGIN")
        sb.AppendLine("INSERT INTO RecentMemberPublicRecords VALUES (")


        sb.AppendLine("@dateRangeFilter,")
        sb.AppendLine("@publisherId,")
        sb.AppendLine("@Customer_Id,")
        sb.AppendLine("@Credit_Event_Date,")
        sb.AppendLine("@Member_Date,")
        sb.AppendLine("@Member_City,")
        sb.AppendLine("@Member_State,")
        sb.AppendLine("@Member_Phone,")
        sb.AppendLine("@Member_Code,")
        sb.AppendLine("@Industry_Code,")
        sb.AppendLine("@Public_Record_Type,")
        sb.AppendLine("@Docket_Number,")
        sb.AppendLine("@Attorney,")
        sb.AppendLine("@Plaintif,")
        sb.AppendLine("@Filed_Date,")
        sb.AppendLine("@Paid_Date,")
        sb.AppendLine("@Effective_Date,")
        sb.AppendLine("@Asset,")
        sb.AppendLine("@Liabilities_Amount,")
        sb.AppendLine("@Original_Balance,")
        sb.AppendLine("@Current_Balance,")
        sb.AppendLine("@ECOA_Designator,")
        sb.AppendLine("@Public_Record_Source_Code,")
        sb.AppendLine("@Court_Location_City,")
        sb.AppendLine("@Court_Location_State")

        sb.AppendLine(")").AppendLine("END")


        sqlQuery = sb.ToString
        Logger.dbg("sqlQuery = " & sqlQuery)
        Return sqlQuery
    End Function
    Public Function getDate(dateSt As String) As Date
        If IsNothing(dateSt) Or dateSt = "" Then
            Return Nothing
        End If
        regTime.Replace(dateSt, "")  '  Get rid of trailing time string
        Dim ret As Date
        If Not Date.TryParse(dateSt, ret) Then
            Return Nothing
        End If
        Return ret
    End Function

    Public Function getDateValue(d As Date) As Object
        If IsNothing(d) Then
            Return DBNull.Value
        End If
        Return d
    End Function

    Public Function getString(s As String) As String
        If IsNothing(s) Or s = "" Then
            Return Nothing
        End If
        Return s
    End Function
    Public Function getStringValue(s As String) As Object
        If IsNothing(s) Then
            Return DBNull.Value
        End If
        Return s
    End Function

    Public Function getInt(s As String) As Integer
        If IsNothing(s) Or s = "" Then
            Return Nothing
        End If
        Dim ret As Integer
        If Not Integer.TryParse(s, ret) Then
            Return Nothing
        End If
        Return ret
    End Function
    Public Function getIntValue(i As Integer) As Object
        If IsNothing(i) Then
            Return DBNull.Value
        End If
        Return i
    End Function

    Public Sub addToTable(conn As SqlConnection)
        Dim sb = New StringBuilder

        sb.Append("IF NOT EXISTS (SELECT * from RecentMemberPublicRecords WHERE Customer_id = '").Append(Customer_Id).AppendLine("'")
        sb.Append(" AND Member_Code = '").Append(Member_Code).AppendLine("'")
        sb.Append(" AND Docket_Number = '").Append(Docket_Number).AppendLine("')")
        Dim firstLine As String = sb.ToString


        Dim sqlRest As String = getSqlString()
        Dim sql = firstLine & sqlRest
        '   Logger.dbg("sql = " & sql)
        '
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)
        cmd.CommandType = CommandType.Text
        With cmd.Parameters
  
            .Add("@dateRangeFilter", SqlDbType.Date).Value = getDateValue(dateRangeFilter)
            .Add("@publisherId", SqlDbType.VarChar).Value = getStringValue(publisherId)
            .Add("@Customer_Id", SqlDbType.VarChar).Value = getStringValue(Customer_Id)
            .Add("@Credit_Event_Date", SqlDbType.Date).Value = getDateValue(Credit_Event_Date)
            .Add("@Member_Date", SqlDbType.Date).Value = getDateValue(Member_Date)
            .Add("@Member_City", SqlDbType.VarChar).Value = getStringValue(Member_City)
            .Add("@Member_State", SqlDbType.VarChar).Value = getStringValue(Member_State)
            .Add("@Member_Phone", SqlDbType.VarChar).Value = getStringValue(Member_Phone)
            .Add("@Member_Code", SqlDbType.VarChar).Value = getStringValue(Member_Code)
            .Add("@Industry_Code", SqlDbType.VarChar).Value = getStringValue(Industry_Code)
            .Add("@Public_Record_Type", SqlDbType.VarChar).Value = getStringValue(Public_Record_Type)
            .Add("@Docket_Number", SqlDbType.VarChar).Value = getStringValue(Docket_Number)
            .Add("@Attorney", SqlDbType.VarChar).Value = getStringValue(Attorney)
            .Add("@Plaintif", SqlDbType.VarChar).Value = getStringValue(Plaintif)
            .Add("@Filed_Date", SqlDbType.Date).Value = getDateValue(Filed_Date)
            .Add("@Paid_Date", SqlDbType.Date).Value = getDateValue(Paid_Date)
            .Add("@Effective_Date", SqlDbType.Date).Value = getDateValue(Effective_Date)
            .Add("@Asset", SqlDbType.Int).Value = getIntValue(Asset)
            .Add("@Liabilities_Amount", SqlDbType.Int).Value = getIntValue(Liabilities_Amount)
            .Add("@Original_Balance", SqlDbType.Int).Value = getIntValue(Original_Balance)
            .Add("@Current_Balance", SqlDbType.Int).Value = getIntValue(Current_Balance)
            .Add("@ECOA_Designator", SqlDbType.VarChar).Value = getStringValue(ECOA_Designator)
            .Add("@Public_Record_Source_Code", SqlDbType.VarChar).Value = getStringValue(Public_Record_Source_Code)
            .Add("@Court_Location_City", SqlDbType.VarChar).Value = getStringValue(Court_Location_City)
            .Add("@Court_Location_State", SqlDbType.VarChar).Value = getStringValue(Court_Location_State)

        End With
        cmd.ExecuteNonQuery()



    End Sub
    Public Function dbg()
        Dim i As Integer
        Dim b As New StringBuilder

        For i = 0 To fields.Length - 1
            b.Append(fieldNames(i)).Append(" = ").AppendLine(fields(i))
        Next

        Return b.ToString()

    End Function

End Class
