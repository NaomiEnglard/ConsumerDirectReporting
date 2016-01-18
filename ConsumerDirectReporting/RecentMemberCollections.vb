Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data.SqlClient
Imports System.Configuration

Public Class RecentMemberCollections
    Public dateRangeFilter As Date
    Public publisherId As String
    Public Customer_Id As String
    Public Credit_Event_Date As Date
    Public Member_Date As Date
    Public Member_City As String
    Public Member_State As String
    Public Member_Phone As String
    Public Member_Code_Account_Id As String
    Public Industry_Code As String
    Public Member_Code As String
    Public Collection_Agency As String
    Public Portfolio_Type As String
    Public Account_Number As String
    Public Account_Type As String
    Public ECOA_Designator As String
    Public Current_Account_Rating As String
    Public Affiliate_Remark_Code As String
    Public Generic_Remark_Code As String
    Public Rating_History_Remark_Code As String
    Public Compliance_Remark_Code As String
    Public Update_Method_Code As String
    Public Original_Creditor_Name As String
    Public Creditor_Classification As Int32
    Public Current_Balance_Amount As Int32
    Public Original_Balance_Amount As Int32
    Public Past_Due_Amount As Int32
    Public Actual_Payment_Amount As Int32
    Public Opened_Date As Date
    Public Closed_Date_Indicator As String
    Public Effective_Date As Date
    Public Paid_Out_Date As Date
    Public Effective As String
    Public First_Delinquency_Date As Date
    Public Last_Payment_Date As Date
    Public Closed_Date As Date

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
"Member_Code_Account_Id", _
"Industry_Code", _
"Member_Code", _
"Collection_Agency", _
"Portfolio_Type", _
"Account_Number", _
"Account_Type", _
"ECOA_Designator", _
"Current_Account_Rating", _
"Affiliate_Remark_Code", _
"Generic_Remark_Code", _
"Rating_History_Remark_Code", _
"Compliance_Remark_Code", _
"Update_Method_Code", _
"Original_Creditor_Name", _
"Creditor_Classification", _
"Current_Balance_Amount", _
"Original_Balance_Amount", _
"Past_Due_Amount", _
"Actual_Payment_Amount", _
"Opened_Date", _
"Closed_Date_Indicator", _
"Effective_Date", _
"Paid_Out_Date", _
"Effective", _
"First_Delinquency_Date", _
"Last_Payment_Date", _
"Closed_Date" _
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
        Member_Code_Account_Id = getString(fields(i))
        i = i + 1
        Industry_Code = getString(fields(i))
        i = i + 1
        Member_Code = getString(fields(i))
        i = i + 1
        Collection_Agency = getString(fields(i))
        i = i + 1
        Portfolio_Type = getString(fields(i))
        i = i + 1
        Account_Number = getString(fields(i))
        i = i + 1
        Account_Type = getString(fields(i))
        i = i + 1
        ECOA_Designator = getString(fields(i))
        i = i + 1
        Current_Account_Rating = getString(fields(i))
        i = i + 1
        Affiliate_Remark_Code = getString(fields(i))
        i = i + 1
        Generic_Remark_Code = getString(fields(i))
        i = i + 1
        Rating_History_Remark_Code = getString(fields(i))
        i = i + 1
        Compliance_Remark_Code = getString(fields(i))
        i = i + 1
        Update_Method_Code = getString(fields(i))
        i = i + 1
        Original_Creditor_Name = getString(fields(i))
        i = i + 1
        Creditor_Classification = getInt(fields(i))
        i = i + 1
        Current_Balance_Amount = getInt(fields(i))
        i = i + 1
        Original_Balance_Amount = getInt(fields(i))
        i = i + 1
        Past_Due_Amount = getInt(fields(i))
        i = i + 1
        Actual_Payment_Amount = getInt(fields(i))
        i = i + 1
        Opened_Date = getDate(fields(i))
        i = i + 1
        Closed_Date_Indicator = getString(fields(i))
        i = i + 1
        Effective_Date = getDate(fields(i))
        i = i + 1
        Paid_Out_Date = getDate(fields(i))
        i = i + 1
        Effective = getString(fields(i))
        i = i + 1
        First_Delinquency_Date = getDate(fields(i))
        i = i + 1
        Last_Payment_Date = getDate(fields(i))
        i = i + 1
        Closed_Date = getDate(fields(i))

    End Sub

    Private Shared Function getSqlString() As String

        If sqlQuery <> "" Then
            Return sqlQuery
        End If
        Dim sb As New StringBuilder

        sb.AppendLine("BEGIN")
        sb.AppendLine("INSERT INTO RecentMemberCollections VALUES (")
        '
        sb.AppendLine("@dateRangeFilter,")
        sb.AppendLine("@publisherId,")
        sb.AppendLine("@Customer_Id,")
        sb.AppendLine("@Credit_Event_Date,")
        sb.AppendLine("@Member_Date,")
        sb.AppendLine("@Member_City,")
        sb.AppendLine("@Member_State,")
        sb.AppendLine("@Member_Phone,")
        sb.AppendLine("@Member_Code_Account_Id,")
        sb.AppendLine("@Industry_Code,")
        sb.AppendLine("@Member_Code,")
        sb.AppendLine("@Collection_Agency,")
        sb.AppendLine("@Portfolio_Type,")
        sb.AppendLine("@Account_Number,")
        sb.AppendLine("@Account_Type,")
        sb.AppendLine("@ECOA_Designator,")
        sb.AppendLine("@Current_Account_Rating,")
        sb.AppendLine("@Affiliate_Remark_Code,")
        sb.AppendLine("@Generic_Remark_Code,")
        sb.AppendLine("@Rating_History_Remark_Code,")
        sb.AppendLine("@Compliance_Remark_Code,")
        sb.AppendLine("@Update_Method_Code,")
        sb.AppendLine("@Original_Creditor_Name,")
        sb.AppendLine("@Creditor_Classification,")
        sb.AppendLine("@Current_Balance_Amount,")
        sb.AppendLine("@Original_Balance_Amount,")
        sb.AppendLine("@Past_Due_Amount,")
        sb.AppendLine("@Actual_Payment_Amount,")
        sb.AppendLine("@Opened_Date,")
        sb.AppendLine("@Closed_Date_Indicator,")
        sb.AppendLine("@Effective_Date,")
        sb.AppendLine("@Paid_Out_Date,")
        sb.AppendLine("@Effective,")
        sb.AppendLine("@First_Delinquency_Date,")
        sb.AppendLine("@Last_Payment_Date,")
        sb.AppendLine("@Closed_Date")
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

        sb.Append("IF NOT EXISTS (SELECT Customer_id from RecentMemberCollections WHERE Customer_id = '").Append(Customer_Id).AppendLine("'")
        sb.Append("AND Account_Number = '").Append(Account_Number).AppendLine("')")
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
            .Add("@Member_Code_Account_Id", SqlDbType.VarChar).Value = getStringValue(Member_Code_Account_Id)
            .Add("@Industry_Code", SqlDbType.VarChar).Value = getStringValue(Industry_Code)
            .Add("@Member_Code", SqlDbType.VarChar).Value = getStringValue(Member_Code)
            .Add("@Collection_Agency", SqlDbType.VarChar).Value = getStringValue(Collection_Agency)
            .Add("@Portfolio_Type", SqlDbType.VarChar).Value = getStringValue(Portfolio_Type)
            .Add("@Account_Number", SqlDbType.VarChar).Value = getStringValue(Account_Number)
            .Add("@Account_Type", SqlDbType.VarChar).Value = getStringValue(Account_Type)
            .Add("@ECOA_Designator", SqlDbType.VarChar).Value = getStringValue(ECOA_Designator)
            .Add("@Current_Account_Rating", SqlDbType.VarChar).Value = getStringValue(Current_Account_Rating)
            .Add("@Affiliate_Remark_Code", SqlDbType.VarChar).Value = getStringValue(Affiliate_Remark_Code)
            .Add("@Generic_Remark_Code", SqlDbType.VarChar).Value = getStringValue(Generic_Remark_Code)
            .Add("@Rating_History_Remark_Code", SqlDbType.VarChar).Value = getStringValue(Rating_History_Remark_Code)
            .Add("@Compliance_Remark_Code", SqlDbType.VarChar).Value = getStringValue(Compliance_Remark_Code)
            .Add("@Update_Method_Code", SqlDbType.VarChar).Value = getStringValue(Update_Method_Code)
            .Add("@Original_Creditor_Name", SqlDbType.VarChar).Value = getStringValue(Original_Creditor_Name)
            .Add("@Creditor_Classification", SqlDbType.Int).Value = getIntValue(Creditor_Classification)
            .Add("@Current_Balance_Amount", SqlDbType.Int).Value = getIntValue(Current_Balance_Amount)
            .Add("@Original_Balance_Amount", SqlDbType.Int).Value = getIntValue(Original_Balance_Amount)
            .Add("@Past_Due_Amount", SqlDbType.Int).Value = getIntValue(Past_Due_Amount)
            .Add("@Actual_Payment_Amount", SqlDbType.Int).Value = getIntValue(Actual_Payment_Amount)
            .Add("@Opened_Date", SqlDbType.Date).Value = getDateValue(Opened_Date)
            .Add("@Closed_Date_Indicator", SqlDbType.VarChar).Value = getStringValue(Closed_Date_Indicator)
            .Add("@Effective_Date", SqlDbType.Date).Value = getDateValue(Effective_Date)
            .Add("@Paid_Out_Date", SqlDbType.Date).Value = getDateValue(Paid_Out_Date)
            .Add("@Effective", SqlDbType.VarChar).Value = getStringValue(Effective)
            .Add("@First_Delinquency_Date", SqlDbType.Date).Value = getDateValue(First_Delinquency_Date)
            .Add("@Last_Payment_Date", SqlDbType.Date).Value = getDateValue(Last_Payment_Date)
            .Add("@Closed_Date", SqlDbType.Date).Value = getDateValue(Closed_Date)

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
