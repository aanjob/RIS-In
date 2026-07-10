Imports System.Data.SqlClient
Imports DevExpress.XtraEditors
Imports System.IO

Module MainModule
    Public GlobalKoneksi As String '= "Data Source=(local);Initial Catalog=RMS;Persist Security Info=True;User ID=sa;Password=velanda;Connect Timeout=30"
    Public namaServer, port, namaDB, namaUser, namaPass, NmApp As String
    Public namaServerSink, portSink, namaDBSink, namaUserSink, namaPassSink As String
    Public B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11, B12 As Integer
    Public NmB1, NmB2, NmB3, NmB4, PilihPPn As String
    Public BatasBB, PjBBID, UserAktif, PilihJamKeAw, PilihJamKeAkh As Integer
    Public InisialAktif As String '= "SPV"
    Public PilihTgl, PilihAwal, PilihAkhir As Date
    'Public periodAktif As Integer = 12
    'Public periodeBulan As Integer = 12
    'Public periodeTahun As Integer = 2015
    Public periodAktif, periodeBulan, periodeTahun As Integer
    Public PersenLbh As Decimal
    Public LoginAktif, PassAktif, Posisi, ProsesAktif, GdAktif, LineAktif, FormDef, PilihJamAw, PilihJamAkh As String
    Public stsAlert, stsAlertJual, stsAlertApp, stsAlertReqC, stsAlertSpec, stsAlertBOM, stsAlertPO, stsAlertSampR, stsAlertPass, stsAlertBSTB, stsAlertBOMstsPO, stsAlertTrmPO, stsAlertSJKBB, stsAlertBayar, stsAlertBlmLns, SU As Boolean
    Public NmPerusahaan As String = "PT. Rajapaksi Adyaperkasa"
    'Public NmPemilik As String '= "NmPemilik Testing"
    'Public Alamat As String '= "Alamat Testing"
    Public Kota As String '= "Kota Testing"
    'Public Fax As String ' = "Fax Testing"
    'Public Telp As String ' = "Fax Testing"
    'Public NPWP As String ' = "Fax Testing"
    Public Version As String = "1606.21.4"
    Public MM1 As String '= "Jeffry Sukmadie"
    Public MM2 As String '= "Anthony"
    Public TTPOLk As String '= "Bambang Haryanto"
    Public PrintDt, AutoBM As Boolean
    Public PilihUkuran, PilihPDok, PilihGudang, PilihGudangID, PilihKat, PilihCab, PilihPrint, PilihUnit, PilihShift As String
    Public BackDate As Boolean = False
    Public NoHarga As Boolean = False
    Public CAkses As String
    Public TcodeCollection As New Collection
    Public DsMaster, DsAddDt, DsLap, DsBrcd As System.Data.DataSet
    Public DtTrans As System.Data.DataTable
    Public dataTrans, dataTrans2 As Collection
    Public OngkosKirim, PersenPPn As Decimal '= 2000
    Public CekSave As Boolean
    Public BOMTam As String
    Public MdlCp As String
    Public UserAktifBtl, LoginAktifBtl, InisialAktifBtl, PassAktifBtl As String
    Public CekAlert As Boolean = False

    Public Sub Main()
        DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Application.Run(New FUtama())
    End Sub

    Public Function FcMsgBox(ByVal Msg As String, Header As String, Icon As MessageBoxIcon) As String

        DevExpress.XtraEditors.XtraMessageBox.AllowHtmlText = True

        DevExpress.XtraEditors.XtraMessageBox.Show(String.Format("<size=15><b>" & Msg & "</b></size>"), Header, MessageBoxButtons.OK, Icon)

        DevExpress.XtraEditors.XtraMessageBox.AllowHtmlText = False
    End Function

    Public Function Cipher(ByVal text As String) As String
        Dim i As Byte
        Dim temp As String
        temp = Nothing
        If Len(text) > 0 Then
            For i = 1 To Len(text)
                temp = temp & Chr((Asc(Mid(text, i, 1)) Xor 150))
            Next
        End If
        Cipher = temp
    End Function

    'Public Function SlReminder(Alert As Boolean)

    '    Dim koneksi As New SqlConnection(GlobalKoneksi)

    '    If Alert = True Then
    '        Dim command As New SqlCommand("Select Count(*) From T_POBBDtl where stsKirim='False' and Ready is not null and DATEDIFF(day,GETDATE(),Ready)<=7", koneksi)

    '        With koneksi
    '            .Open()
    '            Return command.ExecuteScalar()
    '            .Close()
    '        End With
    '    End If
    'End Function

    Public Sub InsImage(Id As String, Jns As String, Img As Byte())
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim cmSP As New SqlCommand("SPInsM_Image")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer


        With cmSP
            .Parameters.Add("@Kode", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid()
            .Parameters.Add("@ID", SqlDbType.VarChar).Value = Id
            .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jns
            .Parameters.Add("@Picture", SqlDbType.VarBinary).Value = Img
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

            Catch ex As Exception
                XtraMessageBox.Show("Gambar Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub

    Public Sub UpImage(Kode As Guid, Id As String, Jns As String, Img As Byte())
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim cmSP As New SqlCommand("SPUpM_Image")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.UniqueIdentifier).Value = Kode
            .Parameters.Add("@ID", SqlDbType.VarChar).Value = Id
            .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Jns
            .Parameters.Add("@Picture", SqlDbType.VarBinary).Value = Img
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

            Catch ex As Exception
                XtraMessageBox.Show("Gambar Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub

    Public Sub DelImage(Id As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim cmSP As New SqlCommand("SPDelM_Image")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@ID", SqlDbType.VarChar).Value = Id
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

            Catch ex As Exception
                XtraMessageBox.Show("Gambar Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End With
    End Sub

    Public Function SlSampReq(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_TrmSRDtl Where ReqID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlLOHBOM(Tgl As Date, MerkID As String, JnsID As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Top 1 Isnull(Sum( LOHCutt+LOHSeri+LOHJht+LOHPack+LOHFinishUpp+LOHFinish),0) From M_LOH Where Awal <= '" & Tgl & "' and Akhir >= '" & Tgl & "' and MerkID='" & MerkID & "' and JnsID='" & JnsID & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

     Public Function SlCekHBhnLPB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From (Select H.Tanggal,D.*, (Select Round(Sum(HarBhn),2) From(Select TrmIDD,Sum(HarBhn) as HarBhn From (Select TrmIDD,J.POIDD,(Select Isnull((Select Top 1 HarSat From T_StokBB Where BBID=J.BBID and Tanggal<=H.Tanggal and DocID<>H.TrmID Order By Tanggal desc,StokID desc),0))/(PD.Qty/J.Qty)as HarBhn From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Left Outer Join T_POBBDtl PD On H.POID=PD.POID and PD.POIDD=D.POIDD Left Outer Join T_POBBJs J On PD.POID=J.POID and D.POIDD=J.POIDDtl) as x Group By TrmIDD,POIDD)as x where TrmIDD=D.TrmIDD) as HarBhn From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Left Outer Join T_POBBJs J On D.POIDD=J.POIDDtl where PeriodID= " & MainModule.periodAktif & " and D.HarBahan<> (Select Round(Sum(HarBhn),2) From(Select TrmIDD,Sum(HarBhn) as HarBhn From (Select TrmIDD,J.POIDD, (Select Isnull((Select Top 1 HarSat From T_StokBB Where BBID=J.BBID and Tanggal<=H.Tanggal and DocID<>H.TrmID Order By Tanggal desc,StokID desc),0))/(PD.Qty/J.Qty)as HarBhn From T_TrmBB H Inner Join T_TrmBBDtl D On H.TrmID=D.TrmID Left Outer Join T_POBBDtl PD On H.POID=PD.POID and PD.POIDD=D.POIDD Left Outer Join T_POBBJs J On PD.POID=J.POID and D.POIDD=J.POIDDtl) as x Group By TrmIDD,POIDD)as x where TrmIDD=D.TrmIDD))as w", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekNilPersBJ()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Sum(Jml) From (Select Count(*) As Jml From T_StokBJ Where HarSat<> (Select Top 1 HarSat From T_StokBJ as x Where x.ArtCode=T_StokBJ.ArtCode and x.GdID=T_StokBJ.GdID and x.Tanggal<=T_StokBJ.Tanggal and x.DocID<>T_StokBJ.DocID  Order By Tanggal desc,JnsDoc Asc,TipeDocID desc,DocID desc,StokID desc) and JnsDoc='Keluar' and PeriodID=" & MainModule.periodAktif & " Union All Select Count(*) As Jml From T_StokBJ Where (Select dbo.fcStokBJTrm (T_StokBJ.ArtCode,T_StokBJ.GdID,T_StokBJ.Tanggal,T_StokBJ.StokID))>0 and HarSat<>round((Select dbo.fcSaldoBJTrm (T_StokBJ.ArtCode,T_StokBJ.GdID,T_StokBJ.Tanggal,T_StokBJ.StokID))/(Select dbo.fcStokBJTrm (T_StokBJ.ArtCode,T_StokBJ.GdID,T_StokBJ.Tanggal,T_StokBJ.StokID)),2) and PeriodID=" & MainModule.periodAktif & " and JnsDoc='Masuk' Union All Select Count(*) As Jml From T_StokBJ Where (Select dbo.fcStokBJTrm (T_StokBJ.ArtCode,T_StokBJ.GdID,T_StokBJ.Tanggal,T_StokBJ.StokID))<=0  and HarSat<>Round(NilMasuk/Masuk,2) and PeriodID=" & MainModule.periodAktif & " and JnsDoc='Masuk')As x", koneksi)

        With koneksi
            .Open()
            Command.CommandTimeout = 9000
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function


    Public Function SlTarget(Tahun As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_Komisi where Tahun='" & Tahun & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlAllCab(UserID As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From M_UsCab where UserID='" & UserID & "' and CabID='%'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCek(Tabel As String, KolomCek As String, Kolom As String, DocID As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select " & KolomCek & " From " & Tabel & " Where " & Kolom & " = '" & DocID & "'", koneksi)

        koneksi.Close()

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With

    End Function

    Public Function SlCekSisaNP()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        If Posisi Like "*Manager*" Then

        Else
            Dim command As New SqlCommand("Select Count(*) From T_NotPes where stsKirim='False' and stsBatal='False' and stsBatalsys='False' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True')", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With
        End If

    End Function

    Public Function SlCekScmKomisi(Tahun As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_Komisi Where Tahun=" & Tahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekPencapaian(Tahun As Integer, Bulan As Integer, SubJns As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_Komisi Where Tahun=" & Tahun & " and Bulan=" & Bulan & " and SubJns='" & SubJns & "'", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekHitKms(Tahun As Integer, Bulan As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HitKomisi Where Tahun=" & Tahun & " and Bulan=" & Bulan & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCairKms(SubJns As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HitKomisi Where stsCair='False' and SubJns='" & SubJns & "'", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCairTrPromo()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HitTrPromo Where stsCair='False'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPrintFt()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_JualBJ Where stsPrint='False' and Gol='Own'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPrintDO()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_DO Where stsPrint='False' and Gol='Own'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPrintRtr()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_RtrBJ Where stsPrint='False' and Gol='Own'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekPOCust(Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_SOBB Where POCust='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlLPBLate()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_TrmBB Where Tanggal>TglScK and KetLate=''", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekSchProd()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Sum(Jml) From(Select Count(*) As Jml From T_BOM Where stsApp='True' and stsLunas='False' and DATEDIFF(day,AppDate,GETDATE())>=7 and BOMID Not In (Select BOMID From T_SchPrPPIC) Union All Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrLeat) and DATEDIFF(day,InsDate,GETDATE())>=7 Union All Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrSint) and DATEDIFF(day,InsDate,GETDATE())>=7 Union All Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrAcc) and DATEDIFF(day,InsDate,GETDATE())>=7 Union All Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrBott) and DATEDIFF(day,InsDate,GETDATE())>=7 Union All  Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrFin) and DATEDIFF(day,InsDate,GETDATE())>=7 Union All Select Count(*) As Jml From T_SchPrPPIC Where BOMID Not In (Select BOMID From T_SchPrTool) and DATEDIFF(day,InsDate,GETDATE())>=7) as x", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekAppLPR()
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        If Posisi Like "*Manager*" Then
            Dim command As New SqlCommand("Select Count(*) From T_LPR Where TotAkhir>1000000 and JnsRtr='Penjualan' and stsApp='False' and Gol='Own'", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With
        ElseIf Posisi Like "*Kepala Cabang*" Then
            Dim command As New SqlCommand("Select Count(*) From T_LPR Where TotAkhir<=1000000 and JnsRtr='Penjualan' and stsApp='False' and Gol='Own'", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With

        Else
            Dim command As New SqlCommand("Select Count(*) From T_LPR Where TotAkhir<=1000000 and (JnsRtr='Produksi' or JnsRtr='Penjualan') and stsApp='False' and Gol='Own'", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With
        End If
    End Function

    Public Function SlCekAppNP()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_NotPes Where stsApp='False' and stsBatal='False' and stsBatalsys='False' and Gol='Own' and TotAkhir+TotAkhirPst>(Select Isnull((Select MaxCL From M_JnsCust Where JnsCustID=T_NotPes.JnsCustID),0))+(Select Isnull((Select [dbo].[fcSisaCL](T_NotPes.CustID,T_NotPes.Gol)),0))", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekAppSJ()
        Dim koneksi As New SqlConnection(GlobalKoneksi)
        If Posisi Like "*Manager*" Then
            'Do Nothing
        Else
            Dim command As New SqlCommand("Select Count(*) From T_SJ Where stsApp='False' and Gol='Own' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True')", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With
        End If
    End Function

    Public Function SlCekAppReqC()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_ReqC Where stsApp='False' and stsBatal='False' and stsBatalsys='False' and SisaKirim<>0 and Gol='Own'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekSpecMdl()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(SpecIDD) From M_SpecDtl D Inner Join M_Spec H On D.SpecID=H.SpecID Where Year(Tanggal)>=2019 and SpecIDD Not In (Select SpecIDD From M_Model H2 inner Join M_ModelDtl D2 On H2.MdlID=D2.MdlID where H.SpecID=D.SpecID) and H.SpecID in (Select SpecID From M_Model)", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekAppBOM()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where (stsBtlProd='True' or stsBatal='True' or stsLunasMan='True') and stsAppMkt='False' and B.Grup In (Select Grup From M_UsGrup Where UserID=" & UserAktif & ")", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekPOBlmBOM()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count (*) From(Select Distinct H.POID From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Where Psg-BtlOrder>(Select Isnull((Select Sum(Tot) From T_BOMPO where (ArtCode=D.ArtCode Or ArtCode In (Select replace(ArtCode,Ass,Ad.Uk) From M_Brg B Inner Join M_BrgAssDtl AD On B.AssID=Ad.AssID where ArtCode=D.ArtCode)) and POID=D.POID),0))and D.stsProd='False' and H.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")) as x", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    'Public Function SlCekSampR()
    '    Dim koneksi As New SqlConnection(GlobalKoneksi)

    '    Dim command As New SqlCommand("Select Count(*) From T_SampReq where stsApp='False' and stsBatal='False' and (ChaserID=" & MainModule.UserAktif & " or MktID=" & MainModule.UserAktif & ")", koneksi)

    '    With koneksi
    '        .Open()
    '        Return command.ExecuteScalar()
    '        .Close()
    '    End With
    'End Function

    Public Function SlCekBBBlmTahu()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From M_Spec H Inner Join M_SpecDtl D On H.SpecID=D.SpecID Where Year(Tanggal)>=2020 and BBID='00000001'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekBSTB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_BSTB where stsApp='False' and Grup In (Select Grup From M_UsGrup Where UserID=" & UserAktif & ")", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekRPB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_RPB where stsApp='False'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekReqP()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_ReqP where stsApp='False'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekSJKBB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_SJKBB where stsApp='False' and JenisSJ<>'Penjasaan Bahan'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekTrmPO()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_POBB where stsTrm='False' and GetDate()>=Dateadd(dd,7,Tanggal)", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlstsPeriod()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select stsClose From M_Period Where PeriodID =" & MainModule.periodAktif & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlstsPeriodNew()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select stsClose From M_Period Where PeriodID =" & MainModule.periodAktif & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlstsPeriodEdDel(Period As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select stsClose From M_Period Where PeriodID =" & Period & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlstsCairTrPromo(Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select stsCair From T_HitTrPromo Where HitTrPromoID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPromo(Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_JualBJ Where PromoID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlTrans(CabID As String, Bulan As Integer, Tahun As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_JualBJ where CabID='" & CabID & "' and Month(Tanggal)=" & Bulan & " and Year(Tanggal)=" & Tahun & "", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCollPO(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_POBJCollPO Where CollPOID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlAppNP(Posisi As String, ByVal Tgl As DateTime) 'SlApprove(ByVal Kode As String)

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        If Posisi Like "*Manager*" Then
            Dim command As New SqlCommand("Select Count(*) From T_NotPes Where stsApp='False' and stsBatal='False' and stsBatalsys='False' and InsDate <'" & Tgl & "' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True') and TotAkhirPst+TotAkhir>(Select Isnull((Select [dbo].[fcSisaCL](T_NotPes.CustID,T_NotPes.Gol)),0))", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With
        Else
            Dim command As New SqlCommand("Select Count(*) From T_NotPes Where stsApp='False' and stsBatal='False' and stsBatalsys='False' and InsDate <'" & Tgl & "' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True')", koneksi)

            With koneksi
                .Open()
                Return command.ExecuteScalar()
                .Close()
            End With

        End If


    End Function

    Public Function SlAppSJ(ByVal Tgl As DateTime) 'SlApprove(ByVal Kode As String)

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("Select Count(*) From T_SJ Where InsDate <'" & Tgl & "' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True') and stsApp='False'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With

    End Function

    Public Function SlAppReqC(ByVal Tgl As DateTime)

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("Select Count(*) From T_ReqC Where stsApp='False' and stsBatal='False' and stsBatalsys='False' and SisaKirim>0 and InsDate <'" & Tgl & "' and CabIDAs In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & " and Def='True')", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With

    End Function

    Public Function SlCekstsRtr()

        Dim koneksi As New SqlConnection(GlobalKoneksi)
        Dim command As New SqlCommand("Select Count(*) From T_RtrBJ Where stsPakai='False' and CabID In (Select CabID From M_UsCab Where UserID=" & MainModule.UserAktif & ")", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With

    End Function

    Public Function SlKirimNP(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_SJ Where NPID='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlHProd(Tanggal As Date, Unit As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HProd Where Tanggal='" & Tanggal & "' and Unit='" & Unit & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlHProdTarget(Tanggal As Date, Unit As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HProdTarget Where Tanggal='" & Tanggal & "' and Unit='" & Unit & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlHProdJam(Tanggal As Date, Jam As Integer, Unit As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_HProdJam Where Tanggal='" & Tanggal & "' and Jam =" & Jam & " and Unit='" & Unit & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function


    Public Function SlRekalBB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_RekalBB Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function


    Public Function SlRekalBJ()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_RekalBJ Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlRekalBB2(Period As Integer)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_RekalBB Where PeriodID= " & Period & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function


    Public Function SlOpBB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_OpBB Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlOpBBGd(Gudang As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_OpBB Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & " and GdID='" & Gudang & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlAdjBB()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_AdjBB Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlOpBJ(Gol As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_OpBJ Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & " and Gol='" & Gol & "'", koneksi)

        With koneksi
            .Open()
            Return Command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlOpBJGd(Gudang As String, Tgl As Date)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_OpBJ Where '" & Tgl & "'<=Tanggal And Year(Tanggal)= " & MainModule.periodeTahun & " and GdID='" & Gudang & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlAdjBJ()
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_AdjBJ Where Month(Tanggal) =" & MainModule.periodeBulan & " And Year(Tanggal)= " & MainModule.periodeTahun & "", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlSpec(Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From M_Model Where SpecID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlMdl(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_BOM Where MdlID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlBOM(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0)+(Select Isnull(Count(*),0) From T_BPB Where DocID ='" & Kode & "') From T_BSTBDtl Where BOMID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlCekBOMPO(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0)+(Select Isnull(Count(*),0) From T_BPB Where DocID ='" & Kode & "')+(Select Isnull(Count(*),0) From T_POBBDtl Where BOMID ='" & Kode & "')+(Select Isnull(Count(*),0) From T_POBBJs Where BOMID ='" & Kode & "') From T_BSTBDtl Where BOMID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPR(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_POBBDtl Where BOMID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlPOMkt(Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From T_BOM Where POID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlKirim(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_TrmBB Where POID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlMskGd(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_TrmBJDtl Where BSTBID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlSJK(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_SJKBB Where DocID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlFtBB(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_JualBB Where SOID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlDPPBB(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_DPPBBDtl Where JualID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlTagih(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_TagihanDtl Where TrmID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlRtrTagih(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_RtrTagihDtl Where RtrID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlByrHut(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_ByrHutDtl Where TagihID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlDoubleSpec(ByVal ArtName As String, ByVal Warna As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From M_Spec Where ArtName ='" & ArtName & "' And Warna='" & Warna & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlDoubleMdl(ByVal SpecID As String, ByVal MdlID As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Isnull(Count(*),0) From M_Model Where SpecID ='" & SpecID & "' And MdlID<>'" & MdlID & "'", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    Public Function SlBOMCancel(ByVal Kode As String)
        Dim koneksi As New SqlConnection(GlobalKoneksi)

        Dim command As New SqlCommand("Select Count(*) From T_BOM where POID='" & Kode & "' and stsBatal=0 and stsLunas=0", koneksi)

        With koneksi
            .Open()
            Return command.ExecuteScalar()
            .Close()
        End With
    End Function

    'Public Function SlQCBB(ByVal TrmID As String)
    '    Dim koneksi As New SqlConnection(GlobalKoneksi)

    '    Dim command As New SqlCommand("Select Sum(x) From(Select Distinct Count (BBID) As x From T_QCBahan H Inner Join T_QCBahanDtl D On H.QCID=D.QCID Where TrmID='" & TrmID & "' Union All Select Distinct Count (BBID)*-1 As x From T_TrmBBDtl Where TrmID='" & TrmID & "')As y", koneksi)

    '    With koneksi
    '        .Open()
    '        Return command.ExecuteScalar()
    '        .Close()
    '    End With
    'End Function

#Region "Terbilang Ind"
    Function EjaAngka(ByVal val As Double) As String
        Dim say As String = ""
        Dim nilai As Decimal = val - (val - Math.Truncate(val)) 'Math.Round(val, 0, MidpointRounding.ToEven)
        Dim koma As Integer = (val - nilai) * 100 'Math.Round(val, 0, MidpointRounding.ToEven)) * 100
        Dim satuan As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim puluhan As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim ratusan As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim ribuan As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim puluhanRibu As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim ratusanribu As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim juta As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim puluhanJuta As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim ratusanJuta As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim milyar As Byte = nilai Mod 10
        nilai = nilai \ 10
        Dim puluhMilyar As Byte = nilai Mod 10
        'baca Milyar
        If puluhMilyar <> 0 Then
            If puluhMilyar = 1 Then
                If milyar = 0 Then
                    say = say & baca2(puluhMilyar, " puluh ")
                Else
                    say = say & baca2(milyar, "belas ")
                End If
            Else
                If milyar = 0 Then
                    say = say & baca2(puluhMilyar, " puluh ")
                Else
                    say = say & baca2(puluhMilyar, " puluh ") & baca1(milyar) & " "
                End If
            End If
            say = say & " milyar "
        Else
            If milyar <> 0 Then
                say = say & baca1(milyar) & " milyar "
            End If
        End If
        'baca Juta
        If ratusanJuta <> 0 Then
            If puluhanJuta = 1 Then
                If juta = 0 Then
                    say = say & baca2(ratusanJuta, " ratus ") & baca2(puluhanJuta, " puluh ")
                Else
                    say = say & baca2(ratusanJuta, " ratus ") & baca2(juta, "belas ")
                End If
            Else
                If juta = 0 Then
                    say = say & baca2(ratusanJuta, " ratus ") & baca2(puluhanJuta, " puluh")
                Else
                    say = say & baca2(ratusanJuta, " ratus ") & baca2(puluhanJuta, " puluh ") & baca1(juta) & " "
                End If
            End If
            say = say & " juta "
        Else
            If puluhanJuta <> 0 Then
                If puluhanJuta = 1 Then
                    If juta = 0 Then
                        say = say & baca2(puluhanJuta, " puluh juta ")
                    Else
                        say = say & baca2(juta, " belas juta ")
                    End If
                Else
                    If juta = 0 Then
                        say = say & baca2(puluhanJuta, " puluh juta ")
                    Else
                        say = say & baca2(puluhanJuta, " puluh ") & baca1(juta) & " juta "
                    End If
                End If
            Else
                If juta <> 0 Then
                    say = say & baca1(juta) & " juta "
                End If
            End If
        End If
        'baca Ribuan
        If ratusanribu <> 0 Then
            If puluhanRibu = 1 Then
                If ribuan = 0 Then
                    say = say & baca2(ratusanribu, " ratus ") & baca2(puluhanRibu, " puluh ")
                Else
                    say = say & baca2(ratusanribu, " ratus ") & baca2(ribuan, "belas ")
                End If
            Else
                If ribuan = 0 Then
                    say = say & baca2(ratusanribu, " ratus ") & baca2(puluhanRibu, " puluh ")
                Else
                    say = say & baca2(ratusanribu, " ratus ") & baca2(puluhanRibu, " puluh ") & baca1(ribuan) & " "
                End If
            End If
            say = say & " ribu "
        Else
            If puluhanRibu <> 0 Then
                If puluhanRibu = 1 Then
                    If ribuan = 0 Then
                        say = say & baca2(puluhanRibu, " puluh ribu ")
                    Else
                        say = say & baca2(ribuan, "belas ribu ")
                    End If
                Else
                    If ribuan = 0 Then
                        say = say & baca2(puluhanRibu, " puluh ribu ")
                    Else
                        say = say & baca2(puluhanRibu, " puluh ") & baca1(ribuan) & " ribu "
                    End If
                End If
            Else
                If ribuan <> 0 Then
                    say = say & baca2(ribuan, " ribu ")
                End If
            End If
        End If
        'baca satuan
        If ratusan <> 0 Then
            If puluhan = 1 Then
                If satuan = 0 Then
                    say = say & baca2(ratusan, " ratus ") & baca2(puluhan, " puluh ")
                Else
                    say = say & baca2(ratusan, " ratus ") & baca2(satuan, "belas ")
                End If
            Else
                If satuan = 0 Then
                    say = say & baca2(ratusan, " ratus ") & baca2(puluhan, " puluh ")
                Else
                    say = say & baca2(ratusan, " ratus ") & baca2(puluhan, " puluh ") & baca1(satuan) & " "
                End If
            End If
        Else
            If puluhan <> 0 Then
                If puluhan = 1 Then
                    If satuan = 0 Then
                        say = say & baca2(puluhan, " puluh ")
                    Else
                        say = say & baca2(satuan, "belas ")
                    End If
                Else
                    If satuan = 0 Then
                        say = say & baca2(puluhan, " puluh ")
                    Else
                        say = say & baca2(puluhan, " puluh ") & baca1(satuan) & " "
                    End If
                End If
            Else
                If satuan <> 0 Then
                    say = say & baca1(satuan)
                End If
            End If
        End If
        Return say & bacaKoma(koma)
    End Function
    Function baca1(ByVal val As Integer) As String
        Select Case val
            Case 1
                Return "satu"
            Case 2
                Return "dua"
            Case 3
                Return "tiga"
            Case 4
                Return "empat"
            Case 5
                Return "lima"
            Case 6
                Return "enam"
            Case 7
                Return "tujuh"
            Case 8
                Return "delapan"
            Case 9
                Return "sembilan"
            Case Else
                Return "nol"
        End Select
    End Function
    Function baca2(ByVal val As Integer, ByVal imbuhan As String) As String
        Select Case val
            Case 1
                imbuhan = imbuhan.Trim
                Return "se" & imbuhan & " "
            Case 2
                Return "dua" & imbuhan
            Case 3
                Return "tiga" & imbuhan
            Case 4
                Return "empat" & imbuhan
            Case 5
                Return "lima" & imbuhan
            Case 6
                Return "enam" & imbuhan
            Case 7
                Return "tujuh" & imbuhan
            Case 8
                Return "delapan" & imbuhan
            Case 9
                Return "sembilan" & imbuhan
            Case Else
                Return " "
        End Select
    End Function
    Function bacaKoma(ByVal val As Integer) As String
        If val = 0 Then
            Return ""
        Else
            Dim nilai As Integer = val
            Dim satuan As Integer = nilai Mod 10
            nilai \= 10
            Dim puluhan As Integer = nilai Mod 10
            Return " koma " & baca1(puluhan) & " " & baca1(satuan)
        End If
    End Function
#End Region

#Region "Terbilang Ing"
    Public Function ConvertCurrencyToEnglish(ByVal MyNumber As Double) As String
        Dim Temp As String
        Dim Dollars, Cents As String
        Dim DecimalPlace, Count As Integer
        Dim Place(9) As String
        Dim Numb As String
        Place(2) = " Thousand " : Place(3) = " Million " : Place(4) = " Billion " : Place(5) = " Trillion "
        ' Convert Numb to a string, trimming extra spaces.
        Numb = Trim(Str(MyNumber))
        ' Find decimal place.
        DecimalPlace = InStr(Numb, ".")
        ' If we find decimal place...
        If DecimalPlace > 0 Then
            ' Convert cents
            Temp = Left(Mid(Numb, DecimalPlace + 1) & "00", 2)
            Cents = ConvertTens(Temp)
            ' Strip off cents from remainder to convert.
            Numb = Trim(Left(Numb, DecimalPlace - 1))
        End If
        Count = 1
        Do While Numb <> ""
            ' Convert last 3 digits of Numb to English dollars.
            Temp = ConvertHundreds(Right(Numb, 3))
            If Temp <> "" Then Dollars = Temp & Place(Count) & Dollars
            If Len(Numb) > 3 Then
                ' Remove last 3 converted digits from Numb.
                Numb = Left(Numb, Len(Numb) - 3)
            Else
                Numb = ""
            End If
            Count = Count + 1
        Loop

        ' Clean up dollars.
        Select Case Dollars
            Case "" : Dollars = "No "
            Case "One" : Dollars = "One"
            Case Else : Dollars = Dollars & ""
        End Select

        ' Clean up cents.
        Select Case Cents
            Case "" : Cents = ""
            Case "One" : Cents = " And One Cent"
            Case Else : Cents = " And Cents " & Cents & ""
        End Select
        ConvertCurrencyToEnglish = Dollars & Cents
    End Function

    Private Function ConvertHundreds(ByVal MyNumber As String) As String
        Dim Result As String
        ' Exit if there is nothing to convert.
        If Val(MyNumber) = 0 Then Exit Function
        ' Append leading zeros to number.
        MyNumber = Right("000" & MyNumber, 3)
        ' Do we have a hundreds place digit to convert?
        If Left(MyNumber, 1) <> "0" Then
            Result = ConvertDigit(Left(MyNumber, 1)) & " Hundred "
        End If
        ' Do we have a tens place digit to convert?
        If Mid(MyNumber, 2, 1) <> "0" Then
            Result = Result & ConvertTens(Mid(MyNumber, 2))
        Else
            ' If not, then convert the ones place digit.
            Result = Result & ConvertDigit(Mid(MyNumber, 3))
        End If
        ConvertHundreds = Trim(Result)
    End Function

    Private Function ConvertTens(ByVal MyTens As String) As String
        Dim Result As String
        ' Is value between 10 and 19?
        If Val(Left(MyTens, 1)) = 1 Then
            Select Case Val(MyTens)
                Case 10 : Result = "Ten"
                Case 11 : Result = "Eleven"
                Case 12 : Result = "Twelve"
                Case 13 : Result = "Thirteen"
                Case 14 : Result = "Fourteen"
                Case 15 : Result = "Fifteen"
                Case 16 : Result = "Sixteen"
                Case 17 : Result = "Seventeen"
                Case 18 : Result = "Eighteen"
                Case 19 : Result = "Nineteen"
                Case Else
            End Select
        Else
            ' .. otherwise it's between 20 and 99.
            Select Case Val(Left(MyTens, 1))
                Case 2 : Result = "Twenty "
                Case 3 : Result = "Thirty "
                Case 4 : Result = "Forty "
                Case 5 : Result = "Fifty "
                Case 6 : Result = "Sixty "
                Case 7 : Result = "Seventy "
                Case 8 : Result = "Eighty "
                Case 9 : Result = "Ninety "
                Case Else
            End Select
            ' Convert ones place digit.
            Result = Result & ConvertDigit(Right(MyTens, 1))
        End If
        ConvertTens = Result
    End Function

    Private Function ConvertDigit(ByVal MyDigit As String) As String
        Select Case Val(MyDigit)
            Case 1 : ConvertDigit = "One"
            Case 2 : ConvertDigit = "Two"
            Case 3 : ConvertDigit = "Three"
            Case 4 : ConvertDigit = "Four"
            Case 5 : ConvertDigit = "Five"
            Case 6 : ConvertDigit = "Six"
            Case 7 : ConvertDigit = "Seven"
            Case 8 : ConvertDigit = "Eight"
            Case 9 : ConvertDigit = "Nine"
            Case Else : ConvertDigit = ""
        End Select
    End Function
#End Region

#Region "Tulis+Baca"

    Sub bacaSettingPeriode()
        Dim IOReader As New StreamReader(Application.StartupPath & " /periode.ini")
        periodAktif = IOReader.ReadLine
        periodeBulan = IOReader.ReadLine
        periodeTahun = IOReader.ReadLine
        IOReader.Close()
    End Sub

    Sub TulisSettingPeriode()
        Dim IOWriter As New StreamWriter(Application.StartupPath & " /periode.ini", False)
        IOWriter.WriteLine(periodAktif)
        IOWriter.WriteLine(periodeBulan)
        IOWriter.WriteLine(periodeTahun)
        IOWriter.Close()
    End Sub

    Sub bacaSettingKoneksi()
        Dim IOReader As New StreamReader(Application.StartupPath & " /setting.ini")
        'Dim IOReader As New StreamReader("\\192.168.0.99\RMS\Koneksi" & " \setting.ini")

        namaServer = Cipher(IOReader.ReadLine)
        port = Cipher(IOReader.ReadLine)
        namaDB = Cipher(IOReader.ReadLine)
        namaUser = Cipher(IOReader.ReadLine)
        namaPass = Cipher(IOReader.ReadLine)
        IOReader.Close()

        GlobalKoneksi = "Data Source=" & namaServer & "," & port & ";Initial Catalog=" & namaDB & ";Persist Security Info=True;User ID=" & namaUser & ";Password=" & namaPass & ";Connect Timeout=10" '999999990"
    End Sub

    Sub TulisSettingKoneksi()
        Dim IOWriter As New StreamWriter(Application.StartupPath & " /setting.ini", False)
        'Dim IOWriter As New StreamWriter("\\192.168.0.99\RMS\Koneksi" & " \setting.ini", False)

        IOWriter.WriteLine(Cipher(namaServer))
        IOWriter.WriteLine(Cipher(port))
        IOWriter.WriteLine(Cipher(namaDB))
        IOWriter.WriteLine(Cipher(namaUser))
        IOWriter.WriteLine(Cipher(namaPass))
        IOWriter.Close()
    End Sub

    Sub bacaSettingKoneksiDdns()
        Dim IOReader As New StreamReader(Application.StartupPath & " /setting ddns.ini")

        namaServer = Cipher(IOReader.ReadLine)
        port = Cipher(IOReader.ReadLine)
        namaDB = Cipher(IOReader.ReadLine)
        namaUser = Cipher(IOReader.ReadLine)
        namaPass = Cipher(IOReader.ReadLine)
        IOReader.Close()

        GlobalKoneksi = "Data Source=" & namaServer & "," & port & ";Initial Catalog=" & namaDB & ";Persist Security Info=True;User ID=" & namaUser & ";Password=" & namaPass & ";Connect Timeout=10" '70000"
    End Sub

    Sub TulisSettingKoneksiDdns()
        Dim IOWriter As New StreamWriter(Application.StartupPath & " /setting ddns.ini", False)

        IOWriter.WriteLine(Cipher(namaServer))
        IOWriter.WriteLine(Cipher(port))
        IOWriter.WriteLine(Cipher(namaDB))
        IOWriter.WriteLine(Cipher(namaUser))
        IOWriter.WriteLine(Cipher(namaPass))
        IOWriter.Close()
    End Sub

    Sub bacaSettingKoneksiP()
        Dim IOReader As New StreamReader(Application.StartupPath & " /settingP.ini")

        namaServer = Cipher(IOReader.ReadLine)
        port = Cipher(IOReader.ReadLine)
        namaDB = Cipher(IOReader.ReadLine)
        namaUser = Cipher(IOReader.ReadLine)
        namaPass = Cipher(IOReader.ReadLine)
        IOReader.Close()

        GlobalKoneksi = "Data Source=" & namaServer & "," & port & ";Initial Catalog=" & namaDB & ";Persist Security Info=True;User ID=" & namaUser & ";Password=" & namaPass & ";Connect Timeout=10" '999999990"
    End Sub

    Sub bacaSettingSinkron()
        Dim IOReader As New StreamReader(Application.StartupPath & " /sinkron.ini")
        'Dim IOReader As New StreamReader("\\192.168.0.99\RMS\Koneksi" & " \setting.ini")

        namaServerSink = Cipher(IOReader.ReadLine)
        portSink = Cipher(IOReader.ReadLine)
        namaDBSink = Cipher(IOReader.ReadLine)
        namaUserSink = Cipher(IOReader.ReadLine)
        namaPassSink = Cipher(IOReader.ReadLine)
        IOReader.Close()

    End Sub

    Sub TulisSettingSinkron()
        Dim IOWriter As New StreamWriter(Application.StartupPath & " /sinkron.ini", False)
        'Dim IOWriter As New StreamWriter("\\192.168.0.99\RMS\Koneksi" & " \setting.ini", False)

        IOWriter.WriteLine(Cipher(namaServerSink))
        IOWriter.WriteLine(Cipher(portSink))
        IOWriter.WriteLine(Cipher(namaDBSink))
        IOWriter.WriteLine(Cipher(namaUserSink))
        IOWriter.WriteLine(Cipher(namaPassSink))
        IOWriter.Close()
    End Sub

#End Region

End Module
