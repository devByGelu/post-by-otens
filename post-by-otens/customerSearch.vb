﻿Public Class customerSearch
    Public businessName As String = EditStock.getName
    Public curRowIndex As Integer
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.Show()

        loadAll()

    End Sub

    Private Sub searchBox_OnValueChanged(sender As Object, e As EventArgs) Handles searchBox.OnValueChanged
        If searchBox.Text = "" Then
            loadAll()
        Else
            Dim adapter As New POSDataSetTableAdapters.DataTable1TableAdapter
            Dim rows As Integer
            rows = adapter.getCustomersOf(businessName, searchBox.Text).Rows.Count
            'adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(3) = custId of the customer
            For i = 0 To rows - 1
                'get all invoices of the specific customer from the company
                Dim adapter2 As New POSDataSetTableAdapters.invoice1TableAdapter
                Dim cID, numberOfPurchases As Integer
                cID = adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(3)
                numberOfPurchases = adapter2.getRecentDateOfPurchase(businessName, cID).Rows.Count
                'get the last the date from the last index
                DataGridView1.Rows.Add(cID, adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(0) + " " + adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(1) + " " + adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(2), adapter2.getRecentDateOfPurchase(businessName, cID).Rows(numberOfPurchases - 1).Item(0), numberOfPurchases)
            Next
        End If
    End Sub

    Private Sub loadAll()
        DataGridView1.Rows.Clear()
        Dim adapter As New POSDataSetTableAdapters.DataTable1TableAdapter
        Dim rows As Integer
        rows = adapter.getAllCustomersOf(businessName).Rows.Count
        'adapter.getCustomersOf(businessName, searchBox.Text).Rows(i).Item(3) = custId of the customer
        For i = 0 To rows - 1
            'get all invoices of the specific customer from the company
            Dim adapter2 As New POSDataSetTableAdapters.invoice1TableAdapter
            Dim cID, numberOfPurchases As Integer
            cID = adapter.getAllCustomersOf(businessName).Rows(i).Item(3)
            numberOfPurchases = adapter2.getRecentDateOfPurchase(businessName, cID).Rows.Count
            'get the last the date from the last index
            DataGridView1.Rows.Add(cID, adapter.getAllCustomersOf(businessName).Rows(i).Item(0) + " " + adapter.getAllCustomersOf(businessName).Rows(i).Item(1) + " " + adapter.getAllCustomersOf(businessName).Rows(i).Item(2), adapter2.getRecentDateOfPurchase(businessName, cID).Rows(numberOfPurchases - 1).Item(0), numberOfPurchases)
        Next
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        curRowIndex = DataGridView1.CurrentCell.RowIndex
    End Sub

    Private Sub BunifuImageButton1_Click(sender As Object, e As EventArgs) Handles BunifuImageButton1.Click
        generateInvoiceForCustomer(DataGridView1.Rows(curRowIndex).Cells(0).Value, " ")
        Timer1.Start()
        MessageBox.Show("Transaction complete", "Generated invoice")
        updateDBStock()
        RegisterTab.resetView()
        Me.Close()
        RegisterTab.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
        RegisterTab.Show()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim seconds As Integer = 1000
        seconds = seconds - 1
        If seconds < 1 Then
            Me.Close()
        End If
    End Sub
End Class