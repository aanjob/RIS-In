Imports DevExpress.XtraEditors
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class FCust
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim arrPar(-1) As String
    Dim KdLama As String
#Region "Export Excel"

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Apakah Anda Mau Membuka File Ini?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportTo(ByVal provider As IExportProvider)
        Try
            Dim currentCursor As Cursor = Windows.Forms.Cursor.Current
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Me.FindForm().Refresh()
            Dim link As BaseExportLink = GridView1.CreateExportLink(provider)
            TryCast(link, GridViewExportLink).ExpandAll = False

            link.ExportTo(True)
            provider.Dispose()

            Windows.Forms.Cursor.Current = currentCursor
        Catch ex As System.IO.IOException
            XtraMessageBox.Show("File masih digunakan oleh proses yang lain")
        End Try
    End Sub

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String, ByVal Nama As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Nama
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

#End Region

    Private Sub LockControl()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("CustN"), Boolean)
        Me.BVBEdit.Enabled = False
        Me.BVBUnblock.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = True
        Me.BVTCust_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.TBNama.Properties.ReadOnly = True
        Me.SLUJnsCust.Properties.ReadOnly = True
        Me.SPTop.Properties.ReadOnly = True
        Me.SPBG.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        Me.CBOTipeByr.Properties.ReadOnly = True
        Me.CEFullPPn.Properties.ReadOnly = True
        Me.TBCLCr.Properties.ReadOnly = True
        Me.TBCLOwn.Properties.ReadOnly = True
        Me.TBDiscCust.Properties.ReadOnly = True
        Me.SLUChaser.Properties.ReadOnly = True
        Me.SLUPM.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True
        Me.MAlamat.Properties.ReadOnly = True
        Me.SLUKota.Properties.ReadOnly = True
        Me.TBNegara.Properties.ReadOnly = True
        Me.TBTelp.Properties.ReadOnly = True
        Me.TBFax.Properties.ReadOnly = True
        Me.TBEmail.Properties.ReadOnly = True
        Me.TBNPWP.Properties.ReadOnly = True
        Me.TBNamaFP.Properties.ReadOnly = True
        Me.MAlamatFP.Properties.ReadOnly = True
        Me.TBKotaFP.Properties.ReadOnly = True
        Me.TBKdPosFP.Properties.ReadOnly = True
        Me.TBPropFP.Properties.ReadOnly = True
        Me.TBNegaraFP.Properties.ReadOnly = True
        Me.SLUBank.Properties.ReadOnly = True
        Me.TBCabang.Properties.ReadOnly = True
        Me.TBNoRek.Properties.ReadOnly = True
        Me.TBAnRek.Properties.ReadOnly = True
        Me.TBNamaCP1.Properties.ReadOnly = True
        Me.TBTelpCP1.Properties.ReadOnly = True
        Me.TBHpCP1.Properties.ReadOnly = True
        Me.TBEmailCP1.Properties.ReadOnly = True
        Me.TBNamaCP2.Properties.ReadOnly = True
        Me.TBTelpCP2.Properties.ReadOnly = True
        Me.TBHpCP2.Properties.ReadOnly = True
        Me.TBEmailCP2.Properties.ReadOnly = True
        Me.CEUmum.Properties.ReadOnly = True
        Me.CEAktif.Properties.ReadOnly = True
        Me.GridView2.OptionsBehavior.Editable = False

        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBUnblock.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTCust_s.Enabled = False

        Me.TBKode.Properties.ReadOnly = False
        Me.TBNama.Properties.ReadOnly = False
        Me.SLUJnsCust.Properties.ReadOnly = False
        Me.SPTop.Properties.ReadOnly = False
        Me.SPBG.Properties.ReadOnly = False
        Me.DTPTanggal.Properties.ReadOnly = False
        Me.CBOTipeByr.Properties.ReadOnly = False
        Me.CEFullPPn.Properties.ReadOnly = False
        Me.TBCLCr.Properties.ReadOnly = False
        Me.TBCLOwn.Properties.ReadOnly = False
        Me.TBDiscCust.Properties.ReadOnly = False
        Me.SLUChaser.Properties.ReadOnly = False
        Me.SLUPM.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False
        Me.MAlamat.Properties.ReadOnly = False
        Me.SLUKota.Properties.ReadOnly = False
        Me.TBNegara.Properties.ReadOnly = False
        Me.TBTelp.Properties.ReadOnly = False
        Me.TBFax.Properties.ReadOnly = False
        Me.TBEmail.Properties.ReadOnly = False
        Me.TBNPWP.Properties.ReadOnly = False
        Me.TBNamaFP.Properties.ReadOnly = False
        Me.MAlamatFP.Properties.ReadOnly = False
        Me.TBKotaFP.Properties.ReadOnly = False
        Me.TBKdPosFP.Properties.ReadOnly = False
        Me.TBPropFP.Properties.ReadOnly = False
        Me.TBNegaraFP.Properties.ReadOnly = False
        Me.SLUBank.Properties.ReadOnly = False
        Me.TBCabang.Properties.ReadOnly = False
        Me.TBNoRek.Properties.ReadOnly = False
        Me.TBAnRek.Properties.ReadOnly = False
        Me.TBNamaCP1.Properties.ReadOnly = False
        Me.TBTelpCP1.Properties.ReadOnly = False
        Me.TBHpCP1.Properties.ReadOnly = False
        Me.TBEmailCP1.Properties.ReadOnly = False
        Me.TBNamaCP2.Properties.ReadOnly = False
        Me.TBTelpCP2.Properties.ReadOnly = False
        Me.TBHpCP2.Properties.ReadOnly = False
        Me.TBEmailCP2.Properties.ReadOnly = False
        Me.CEUmum.Properties.ReadOnly = False
        Me.CEAktif.Properties.ReadOnly = False
        Me.GridView2.OptionsBehavior.Editable = True

        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTCust_e.Selected = True

    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select KotaID,Nama From M_Kota", koneksi)
        cmsl.TableMappings.Add("Table", "M_KotaLUE")
        'DsMaster = New System.Data.DataSet
        cmsl.Fill(DsMaster, "M_KotaLUE")
        DsMaster.Tables("M_KotaLUE").Clear()
        cmsl.Fill(DsMaster, "M_KotaLUE")

        Me.SLUKota.Properties.DataSource = DsMaster.Tables("M_KotaLUE")
        Me.SLUKota.Properties.DisplayMember = "Nama"
        Me.SLUKota.Properties.ValueMember = "KotaID"

        cmsl = New SqlDataAdapter("Select JnsCustID,Nama,Harga From M_JnsCust Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_JnsCust")
        cmsl.Fill(DsMaster, "M_JnsCust")
        DsMaster.Tables("M_JnsCust").Clear()
        cmsl.Fill(DsMaster, "M_JnsCust")

        Me.SLUJnsCust.Properties.DataSource = DsMaster.Tables("M_JnsCust")
        Me.SLUJnsCust.Properties.DisplayMember = "Nama"
        Me.SLUJnsCust.Properties.ValueMember = "JnsCustID"

        cmsl = New SqlDataAdapter("Select BankID,Nama,Aktif From M_Bank Where Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_Bank")
        cmsl.Fill(DsMaster, "M_Bank")
        DsMaster.Tables("M_Bank").Clear()
        cmsl.Fill(DsMaster, "M_Bank")

        Me.SLUBank.Properties.DataSource = DsMaster.Tables("M_Bank")
        Me.SLUBank.Properties.DisplayMember = "Nama"
        Me.SLUBank.Properties.ValueMember = "BankID"

        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID Like '%Develop%' and PosisiID<>'Developer'", koneksi)
        cmsl.TableMappings.Add("Table", "ChaserLUE")
        cmsl.Fill(DsMaster, "ChaserLUE")
        DsMaster.Tables("ChaserLUE").Clear()
        cmsl.Fill(DsMaster, "ChaserLUE")

        Me.SLUChaser.Properties.DataSource = DsMaster.Tables("ChaserLUE")
        Me.SLUChaser.Properties.DisplayMember = "Nama"
        Me.SLUChaser.Properties.ValueMember = "UserID"

        cmsl = New SqlDataAdapter("Select UserID,Nama From M_User Where Aktif='True' and PosisiID ='Pattern Maker'", koneksi)
        cmsl.TableMappings.Add("Table", "PMLUE")
        cmsl.Fill(DsMaster, "PMLUE")
        DsMaster.Tables("PMLUE").Clear()
        cmsl.Fill(DsMaster, "PMLUE")

        Me.SLUPM.Properties.DataSource = DsMaster.Tables("PMLUE")
        Me.SLUPM.Properties.DisplayMember = "Nama"
        Me.SLUPM.Properties.ValueMember = "UserID"
    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select CustID,C.Nama,C.JnsCustID,J.Nama As JnsCust,J.Harga,Alamat,C.KotaID,K.Nama As Kota,P.Nama As Prop, Negara,TglJoin,TipeByr,FullPPn,CrRating,ChaserID,PtMkID,NPWP,NamaFP,AlamatFP,KotaFP,KdPosFP,PropFP,NegaraFP,NoTelp,Fax,Email,JT, BG,CLCr,CLOwn,C.BankID,B.Nama as Bank,Cabang,NoRek,AnRek,NamaCP1,NoTelpCP1,NoHPCP1,EmailCP1,NamaCP2,NoTelpCP2,NoHPCP2,EmailCP2, DiscCust,Ket,Umum,stsBlok,stsBlokMnl,C.Aktif,C.InsDate,C.InsBy, C.UpdDate,C.UpdBy From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Inner Join M_Prop P On K.PropID=P.PropID Inner Join  M_JnsCust J On C.JnsCustId=J.JnsCustId Left Outer Join M_Bank B On C.BankID=B.BankId Where CustID In (Select CustId From M_CabCust Where CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & "))", koneksi)

        cmsl.TableMappings.Add("Table", "M_Cust")
        Try
            DsMaster.Tables("M_Cust").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_Cust")

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "M_Cust"
    End Sub

    Public Sub Del()
        Dim cmSP1 As New SqlCommand("SPDelM_Cust")
        cmSP1.CommandType = CommandType.StoredProcedure
        'Dim x As Integer

        With cmSP1
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi

            With koneksi
                .Open()
                cmSP1.ExecuteNonQuery()
                'x = cmSP.Parameters("@Return").Value
                .Close()
            End With
        End With
    End Sub

    Private Sub FCust_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Master Customer"
    End Sub

    Private Sub FCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTCust_e.Selected = True
    End Sub

    Private Sub BVTCust_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTCust_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Master Customer"

        FillDt()

        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("CustEd"), Boolean)
        Me.BVBUnblock.Enabled = CType(TcodeCollection.Item("CustUnB"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("CustDel"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Master Customer"

        DsMaster.Clear()

        OpenControl()
        LUE()

        Indicator = "100"
        Me.CEAktif.Properties.ReadOnly = True

        Me.TBKode.EditValue = ""
        Me.TBNama.EditValue = ""
        Me.SLUJnsCust.EditValue = ""
        Me.TBHarga.EditValue = ""
        Me.SPTop.EditValue = 0
        Me.SPBG.EditValue = 0
        Me.TBCLCr.EditValue = 0.0
        Me.TBCLOwn.EditValue = 0.0
        Me.TBPiutCr.EditValue = 0
        Me.TBPiutOwn.EditValue = 0
        Me.TBDiscCust.EditValue = 0
        Me.SLUChaser.EditValue = ""
        Me.SLUPM.EditValue = ""
        Me.MKet.EditValue = ""
        Me.MAlamat.EditValue = ""
        Me.SLUKota.EditValue = ""
        Me.TBNegara.EditValue = ""
        Me.DTPTanggal.EditValue = System.DateTime.Now
        Me.CBOTipeByr.EditValue = ""
        Me.TBCrRating.EditValue = ""
        Me.CEFullPPn.EditValue = False
        Me.TBTelp.EditValue = ""
        Me.TBFax.EditValue = ""
        Me.TBEmail.EditValue = ""
        Me.TBNPWP.EditValue = ""
        Me.TBNamaFP.EditValue = ""
        Me.MAlamatFP.EditValue = ""
        Me.TBKotaFP.EditValue = ""
        Me.TBKdPosFP.EditValue = ""
        Me.TBPropFP.EditValue = ""
        Me.TBNegaraFP.EditValue = ""
        Me.SLUBank.EditValue = ""
        Me.TBCabang.EditValue = ""
        Me.TBNoRek.EditValue = ""
        Me.TBAnRek.EditValue = ""
        Me.TBNamaCP1.EditValue = ""
        Me.TBTelpCP1.EditValue = ""
        Me.TBHpCP1.EditValue = ""
        Me.TBEmailCP1.EditValue = ""
        Me.TBNamaCP2.EditValue = ""
        Me.TBTelpCP2.EditValue = ""
        Me.TBHpCP2.EditValue = ""
        Me.TBEmailCP2.EditValue = ""
        Me.TBInfo.EditValue = ""
        Me.CEUmum.EditValue = False
        Me.CEAktif.EditValue = True

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID, CC.CabID, Cabang, CustID, CC.Aktif From M_CabCust CC Inner Join M_Cab Cb On CC.CabID=Cb.CabID Where CustID='--'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CabCust")
        cmsl.Fill(DsMaster, "M_CabCust")
        DsMaster.Tables("M_CabCust").Clear()
        cmsl.Fill(DsMaster, "M_CabCust")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_CabCust"

        ReDim arrPar(-1)
    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Master Customer"

        LUE()
        Indicator = "200"
        Me.TBKode.EditValue = Me.GridView1.GetFocusedDataRow.Item("CustID")
        KdLama = Me.GridView1.GetFocusedDataRow.Item("CustID")
        Me.TBNama.EditValue = Me.GridView1.GetFocusedDataRow.Item("Nama")
        Me.SLUJnsCust.EditValue = Me.GridView1.GetFocusedDataRow.Item("JnsCustID")
        Me.TBHarga.EditValue = Me.GridView1.GetFocusedDataRow.Item("Harga")
        Me.SPTop.EditValue = Me.GridView1.GetFocusedDataRow.Item("JT")
        Me.SPBG.EditValue = Me.GridView1.GetFocusedDataRow.Item("BG")
        Me.DTPTanggal.EditValue = Me.GridView1.GetFocusedDataRow.Item("TglJoin")
        Me.CBOTipeByr.EditValue = Me.GridView1.GetFocusedDataRow.Item("TipeByr")
        Me.CEFullPPn.EditValue = Me.GridView1.GetFocusedDataRow.Item("FullPPn")
        Me.TBCrRating.EditValue = Me.GridView1.GetFocusedDataRow.Item("CrRating")
        Me.TBCLCr.EditValue = Me.GridView1.GetFocusedDataRow.Item("CLCr")
        Me.TBCLOwn.EditValue = Me.GridView1.GetFocusedDataRow.Item("CLOwn")
        Me.TBDiscCust.EditValue = Me.GridView1.GetFocusedDataRow.Item("DiscCust")
        Me.SLUChaser.EditValue = Me.GridView1.GetFocusedDataRow.Item("ChaserID")
        Me.SLUPM.EditValue = Me.GridView1.GetFocusedDataRow.Item("PtMkID")
        Me.MKet.EditValue = Me.GridView1.GetFocusedDataRow.Item("Ket")
        Me.MAlamat.EditValue = Me.GridView1.GetFocusedDataRow.Item("Alamat")
        Me.SLUKota.EditValue = Me.GridView1.GetFocusedDataRow.Item("KotaID")
        Me.TBNegara.EditValue = Me.GridView1.GetFocusedDataRow.Item("Negara")
        Me.TBTelp.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelp")
        Me.TBFax.EditValue = Me.GridView1.GetFocusedDataRow.Item("Fax")
        Me.TBEmail.EditValue = Me.GridView1.GetFocusedDataRow.Item("Email")
        Me.TBNPWP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NPWP")
        Me.TBNamaFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaFP")
        Me.MAlamatFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("AlamatFP")
        Me.TBKotaFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("KotaFP")
        Me.TBKdPosFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("KdPosFP")
        Me.TBPropFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("PropFP")
        Me.TBNegaraFP.EditValue = Me.GridView1.GetFocusedDataRow.Item("NegaraFP")
        Me.SLUBank.EditValue = Me.GridView1.GetFocusedDataRow.Item("BankID")
        Me.TBCabang.EditValue = Me.GridView1.GetFocusedDataRow.Item("Cabang")
        Me.TBNoRek.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoRek")
        Me.TBAnRek.EditValue = Me.GridView1.GetFocusedDataRow.Item("AnRek")
        Me.TBNamaCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaCP1")
        Me.TBTelpCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelpCP1")
        Me.TBHpCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHpCP1")
        Me.TBEmailCP1.EditValue = Me.GridView1.GetFocusedDataRow.Item("EmailCP1")
        Me.TBNamaCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NamaCP2")
        Me.TBTelpCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoTelpCP2")
        Me.TBHpCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("NoHpCP2")
        Me.TBEmailCP2.EditValue = Me.GridView1.GetFocusedDataRow.Item("EmailCP2")
        Me.CEUmum.EditValue = Me.GridView1.GetFocusedDataRow.Item("Umum")
        Me.CEAktif.EditValue = Me.GridView1.GetFocusedDataRow.Item("Aktif")
        Me.CEBlokMnl.EditValue = Me.GridView1.GetFocusedDataRow.Item("stsBlokMnl")

        If IsDBNull(Me.GridView1.GetFocusedDataRow.Item("UpdBy")) Then
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy")
        Else
            Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView1.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView1.GetFocusedDataRow.Item("UpdBy")
        End If

        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select HubID, CC.CabID, Cabang, CustID, CC.Aktif From M_CabCust CC Inner Join M_Cab Cb On CC.CabID=Cb.CabID Where CustId = '" & Me.TBKode.EditValue & "'", koneksi)

        cmsl.TableMappings.Add("Table", "M_CabCust")
        Try
            DsMaster.Tables("M_CabCust").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "M_CabCust")

        DsMaster.Tables("M_CabCust").PrimaryKey = New DataColumn() {DsMaster.Tables("M_CabCust").Columns("CabID"), DsMaster.Tables("M_CabCust").Columns("CustID")}

        ReDim arrPar(-1)

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "M_CabCust"

        OpenControl()
    End Sub

    Private Sub BVBUnblock_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBUnblock.ItemClick
        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Membuka Block Customer : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPUnBlockM_Cust")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CustID")
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

                    If x = 0 Then
                        XtraMessageBox.Show("Customer Berhasil Dibuka", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Customer Gagal Dibuka", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Customer Gagal Dibuka", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Master Customer"

        koneksi.Close()

        If XtraMessageBox.Show("Apakah Anda Mau Menghapus Customer : " & Me.GridView1.GetFocusedDataRow.Item("Nama") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim cmSP As New SqlCommand("SPDelM_Cust")
            cmSP.CommandType = CommandType.StoredProcedure
            Dim x As Integer

            With cmSP
                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView1.GetFocusedDataRow.Item("CustID")
                .Parameters.Add("@Return", SqlDbType.Int)
                .Parameters("@Return").Direction = ParameterDirection.Output
                .Connection = koneksi

                Try
                    With koneksi
                        .Open()
                        cmSP.ExecuteNonQuery()
                        x = cmSP.Parameters("@Return").Value
                        .Close()
                    End With

                    If x = 0 Then
                        XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        FillDt()
                    Else
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                Catch ex As Exception
                    XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End With
        End If
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        koneksi.Close()

        If Me.TBKode.EditValue = "" Or IsDBNull(Me.TBKode.EditValue) Then
            XtraMessageBox.Show("Kode Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.SLUKota.EditValue = "" Or IsDBNull(Me.SLUKota.EditValue) Then
            XtraMessageBox.Show("Kota Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.RowCount - 1 < 0 Then
            XtraMessageBox.Show("Cabang Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.CEUmum.EditValue = True Then
            If CStr(Me.SLUChaser.EditValue) = "" Or IsDBNull(Me.SLUChaser.EditValue) Then
                XtraMessageBox.Show("Chaser Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If CStr(Me.SLUPM.EditValue) = "" Or IsDBNull(Me.SLUPM.EditValue) Then
                XtraMessageBox.Show("Pattern Maker Harus Diisi !", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsM_Cust")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Negara", SqlDbType.VarChar).Value = Me.TBNegara.EditValue
                    .Parameters.Add("@TglJoin", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TipeByr", SqlDbType.VarChar).Value = Me.CBOTipeByr.EditValue
                    .Parameters.Add("@FullPPn", SqlDbType.Bit).Value = Me.CEFullPPn.EditValue
                    If Me.CEUmum.EditValue = True Then
                        .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                        .Parameters.Add("@PtMkID", SqlDbType.Int).Value = Me.SLUPM.EditValue
                    Else
                        .Parameters.Add("@ChaserID", SqlDbType.Int).Value = 0
                        .Parameters.Add("@PtMkID", SqlDbType.Int).Value = 0
                    End If

                    .Parameters.Add("@NPWP", SqlDbType.VarChar).Value = Me.TBNPWP.EditValue
                    .Parameters.Add("@NamaFP", SqlDbType.VarChar).Value = Me.TBNamaFP.EditValue
                    .Parameters.Add("@AlamatFP", SqlDbType.VarChar).Value = Me.MAlamatFP.EditValue
                    .Parameters.Add("@KotaFP", SqlDbType.VarChar).Value = Me.TBKotaFP.EditValue
                    .Parameters.Add("@KdPosFP", SqlDbType.VarChar).Value = Me.TBKdPosFP.EditValue
                    .Parameters.Add("@PropFP", SqlDbType.VarChar).Value = Me.TBPropFP.EditValue
                    .Parameters.Add("@NegaraFP", SqlDbType.VarChar).Value = Me.TBNegaraFP.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@Fax", SqlDbType.VarChar).Value = Me.TBFax.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPTop.EditValue
                    .Parameters.Add("@BG", SqlDbType.Int).Value = Me.SPBG.EditValue
                    .Parameters.Add("@CLCr", SqlDbType.Decimal).Value = Me.TBCLCr.EditValue
                    .Parameters.Add("@CLOwn", SqlDbType.Decimal).Value = Me.TBCLOwn.EditValue
                    .Parameters.Add("@BankId", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@Cab", SqlDbType.VarChar).Value = Me.TBCabang.EditValue
                    .Parameters.Add("@NoRek", SqlDbType.VarChar).Value = Me.TBNoRek.EditValue
                    .Parameters.Add("@AnRek", SqlDbType.VarChar).Value = Me.TBAnRek.EditValue
                    .Parameters.Add("@NamaCP1", SqlDbType.VarChar).Value = Me.TBNamaCP1.EditValue
                    .Parameters.Add("@TelpCP1", SqlDbType.VarChar).Value = Me.TBTelpCP1.EditValue
                    .Parameters.Add("@HPCP1", SqlDbType.VarChar).Value = Me.TBHpCP1.EditValue
                    .Parameters.Add("@EmailCP1", SqlDbType.VarChar).Value = Me.TBEmailCP1.EditValue
                    .Parameters.Add("@NamaCP2", SqlDbType.VarChar).Value = Me.TBNamaCP2.EditValue
                    .Parameters.Add("@TelpCP2", SqlDbType.VarChar).Value = Me.TBTelpCP2.EditValue
                    .Parameters.Add("@HPCP2", SqlDbType.VarChar).Value = Me.TBHpCP2.EditValue
                    .Parameters.Add("@EmailCP2", SqlDbType.VarChar).Value = Me.TBEmailCP2.EditValue
                    .Parameters.Add("@DiscCust", SqlDbType.VarChar).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Umum", SqlDbType.Bit).Value = Me.CEUmum.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 0 Then

                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else

                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To Me.GridView2.RowCount - 1
                            Dim cmSPDtl As New SqlCommand("SPInsM_CabCust")
                            cmSPDtl.CommandType = CommandType.StoredProcedure

                            With cmSPDtl
                                .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "CabID")
                                .Parameters.Add("@CustId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi
                            End With

                            With koneksi
                                .Open()
                                cmSPDtl.ExecuteNonQuery()
                                x = cmSPDtl.Parameters("@Return").Value
                                .Close()
                            End With
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Del()
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Del()
                        Exit Sub
                    End Try
                End With
            Case 200
                Dim cmSP As New SqlCommand("SPUpM_Cust")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@KdLama", SqlDbType.VarChar).Value = KdLama
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Nama", SqlDbType.VarChar).Value = Me.TBNama.EditValue
                    .Parameters.Add("@JnsCustID", SqlDbType.VarChar).Value = Me.SLUJnsCust.EditValue
                    .Parameters.Add("@Harga", SqlDbType.VarChar).Value = Me.TBHarga.EditValue
                    .Parameters.Add("@Alamat", SqlDbType.VarChar).Value = Me.MAlamat.EditValue
                    .Parameters.Add("@KotaID", SqlDbType.VarChar).Value = Me.SLUKota.EditValue
                    .Parameters.Add("@Negara", SqlDbType.VarChar).Value = Me.TBNegara.EditValue
                    .Parameters.Add("@TglJoin", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@TipeByr", SqlDbType.VarChar).Value = Me.CBOTipeByr.EditValue
                    .Parameters.Add("@FullPPn", SqlDbType.Bit).Value = Me.CEFullPPn.EditValue
                    If Me.CEUmum.EditValue = True Then
                        .Parameters.Add("@ChaserID", SqlDbType.Int).Value = Me.SLUChaser.EditValue
                        .Parameters.Add("@PtMkID", SqlDbType.Int).Value = Me.SLUPM.EditValue
                    Else
                        .Parameters.Add("@ChaserID", SqlDbType.Int).Value = 0
                        .Parameters.Add("@PtMkID", SqlDbType.Int).Value = 0
                    End If
                    .Parameters.Add("@NPWP", SqlDbType.VarChar).Value = Me.TBNPWP.EditValue
                    .Parameters.Add("@NamaFP", SqlDbType.VarChar).Value = Me.TBNamaFP.EditValue
                    .Parameters.Add("@AlamatFP", SqlDbType.VarChar).Value = Me.MAlamatFP.EditValue
                    .Parameters.Add("@KotaFP", SqlDbType.VarChar).Value = Me.TBKotaFP.EditValue
                    .Parameters.Add("@KdPosFP", SqlDbType.VarChar).Value = Me.TBKdPosFP.EditValue
                    .Parameters.Add("@PropFP", SqlDbType.VarChar).Value = Me.TBPropFP.EditValue
                    .Parameters.Add("@NegaraFP", SqlDbType.VarChar).Value = Me.TBNegaraFP.EditValue
                    .Parameters.Add("@NoTelp", SqlDbType.VarChar).Value = Me.TBTelp.EditValue
                    .Parameters.Add("@Fax", SqlDbType.VarChar).Value = Me.TBFax.EditValue
                    .Parameters.Add("@Email", SqlDbType.VarChar).Value = Me.TBEmail.EditValue
                    .Parameters.Add("@JT", SqlDbType.Int).Value = Me.SPTop.EditValue
                    .Parameters.Add("@BG", SqlDbType.Int).Value = Me.SPBG.EditValue
                    .Parameters.Add("@CLCr", SqlDbType.Decimal).Value = Me.TBCLCr.EditValue
                    .Parameters.Add("@CLOwn", SqlDbType.Decimal).Value = Me.TBCLOwn.EditValue
                    .Parameters.Add("@BankId", SqlDbType.VarChar).Value = Me.SLUBank.EditValue
                    .Parameters.Add("@Cab", SqlDbType.VarChar).Value = Me.TBCabang.EditValue
                    .Parameters.Add("@NoRek", SqlDbType.VarChar).Value = Me.TBNoRek.EditValue
                    .Parameters.Add("@AnRek", SqlDbType.VarChar).Value = Me.TBAnRek.EditValue
                    .Parameters.Add("@NamaCP1", SqlDbType.VarChar).Value = Me.TBNamaCP1.EditValue
                    .Parameters.Add("@TelpCP1", SqlDbType.VarChar).Value = Me.TBTelpCP1.EditValue
                    .Parameters.Add("@HPCP1", SqlDbType.VarChar).Value = Me.TBHpCP1.EditValue
                    .Parameters.Add("@EmailCP1", SqlDbType.VarChar).Value = Me.TBEmailCP1.EditValue
                    .Parameters.Add("@NamaCP2", SqlDbType.VarChar).Value = Me.TBNamaCP2.EditValue
                    .Parameters.Add("@TelpCP2", SqlDbType.VarChar).Value = Me.TBTelpCP2.EditValue
                    .Parameters.Add("@HPCP2", SqlDbType.VarChar).Value = Me.TBHpCP2.EditValue
                    .Parameters.Add("@EmailCP2", SqlDbType.VarChar).Value = Me.TBEmailCP2.EditValue
                    .Parameters.Add("@DiscCust", SqlDbType.VarChar).Value = Me.TBDiscCust.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@Umum", SqlDbType.Bit).Value = Me.CEUmum.EditValue
                    .Parameters.Add("@Aktif", SqlDbType.Bit).Value = Me.CEAktif.EditValue
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x <> 0 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelM_CabCust")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar(y)
                                .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                .Parameters.Add("@Return", SqlDbType.Int)
                                .Parameters("@Return").Direction = ParameterDirection.Output
                                .Connection = koneksi

                                With koneksi
                                    .Open()
                                    cmSPDel.ExecuteNonQuery()
                                    .Close()
                                End With

                            End With
                        Next

                        Dim i : For i = 0 To Me.GridView2.RowCount - 1
                            If Me.GridView2.GetRowCellValue(i, "HubID") < 0 Then
                                Dim cmSPDtl As New SqlCommand("SPInsM_CabCust")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@CustId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Return", SqlDbType.Int)
                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                    .Connection = koneksi
                                End With

                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    .Close()
                                End With

                                If x = 0 Then
                                    Me.GridView2.SetRowCellValue(i, "HubID", Me.GridView2.GetRowCellValue(i, "HubID") * -1)
                                End If
                            Else
                                Dim cmSPDtl As New SqlCommand("SPUpM_CabCust")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Id", SqlDbType.Int).Value = Me.GridView2.GetRowCellValue(i, "HubID")
                                    .Parameters.Add("@CabId", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "CabID")
                                    .Parameters.Add("@CustId", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@Aktif", SqlDbType.VarChar).Value = Me.GridView2.GetRowCellValue(i, "Aktif")
                                    .Parameters.Add("@Return", SqlDbType.Int)
                                    .Parameters("@Return").Direction = ParameterDirection.Output
                                    .Connection = koneksi
                                End With

                                With koneksi
                                    .Open()
                                    cmSPDtl.ExecuteNonQuery()
                                    x = cmSPDtl.Parameters("@Return").Value
                                    .Close()
                                End With
                            End If
                        Next

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With
        End Select

        LockControl()
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
    End Sub

    Private Sub SLUJnsCust_Leave(sender As Object, e As EventArgs) Handles SLUJnsCust.Leave
        Me.TBHarga.EditValue = DsMaster.Tables("M_JnsCust").Select("JnsCustID = '" & Me.SLUJnsCust.EditValue & "'")(0).Item("Harga")
    End Sub

    Private Sub GridView2_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView2.InitNewRow
        Me.GridView2.SetRowCellValue(e.RowHandle, "HubID", Me.GridView2.RowCount * -1)
        Me.GridView2.SetRowCellValue(e.RowHandle, "CustID", Me.TBKode.EditValue)
        Me.GridView2.SetRowCellValue(e.RowHandle, "Aktif", "True")
    End Sub

    Private Sub BEdSalId_ButtonClick(sender As Object, e As Controls.ButtonPressedEventArgs) Handles BEdSalID.ButtonClick
        Dim frm As New FSearch("Cabang", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView2.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView2.SetRowCellValue(Me.GridView2.FocusedRowHandle, "Cabang", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridControl2_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl2.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then
            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView2.GetFocusedDataRow.Item("HubId")
        End If

    End Sub

    Private Sub BEdSalID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdSalID.KeyPress
        e.Handled = True
    End Sub

    Private Sub TBCLCr_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TBCLCr.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Me.LCPiutCr.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                Dim command As New SqlCommand("Select dbo.fcPiutCust('" & Me.TBKode.EditValue & "','Character','" & Date.Now & "','--')", koneksi)

                With koneksi
                    .Open()
                    Me.TBPiutCr.EditValue = command.ExecuteScalar()
                    .Close()
                End With

                Me.LCPiutCr.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LCPiutCr.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Sub TBCLOwn_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCLOwn.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Me.LCPiutOwn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                Dim command As New SqlCommand("Select dbo.fcPiutCust('" & Me.TBKode.EditValue & "','Own','" & Date.Now & "','--')", koneksi)

                With koneksi
                    .Open()
                    Me.TBPiutOwn.EditValue = command.ExecuteScalar()
                    .Close()
                End With

                Me.LCPiutOwn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.LCPiutOwn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBNama.KeyPress, MAlamat.KeyPress, TBNegara.KeyPress, TBTelp.KeyPress, TBFax.KeyPress, TBEmail.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBNPWP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNPWP.KeyPress, TBNamaFP.KeyPress, MAlamatFP.KeyPress, TBKotaFP.KeyPress, TBPropFP.KeyPress, TBNegaraFP.KeyPress, TBKdPosFP.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBCabang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBCabang.KeyPress, TBNoRek.KeyPress, TBAnRek.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TBNamaCP1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBNamaCP1.KeyPress, TBTelpCP1.KeyPress, TBHpCP1.KeyPress, TBEmailCP1.KeyPress, TBNamaCP2.KeyPress, TBTelpCP2.KeyPress, TBHpCP2.KeyPress, TBEmailCP2.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Then
            e.Handled = True
        End If
    End Sub

    Private Sub BExExcel_Click(sender As Object, e As EventArgs) Handles BExExcel.Click
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Lap Master Customer Per Tanggal " & Format(Me.DTPTanggal.EditValue, "dd-MM-yy") & "")
        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))
            OpenFile(fileName)
        End If
    End Sub
End Class