Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Export
Imports DevExpress.XtraExport

'Detail BOM yang berwarna merah berarti sudah ditarik PO berarti meskipun di model ada perubahan dan di proses di BOM tidak akan berubah
'Update Model-BOM : Tinggal diklik Kode Modelny

Public Class FBOM
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Public Indicator As Int16
    Dim CodeID As String
    Dim Manual, MnlInsUpd As Boolean
    Dim rw As Integer = 0
    Dim Ukuran As String = ""
    Dim ArtCenter As String = ""
    Dim Gol As String = ""
    Dim Grup As String = ""
    Dim centermin, centerplus As Integer
    Dim arrPar(-1), arrPar2(-1) As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim Reader As SqlClient.SqlDataReader
        Dim command As New SqlCommand("Select Manuall,CodeID,MnlInsUpd From M_DocCode Where DocID=4", koneksi)

        With koneksi
            .Open()
            Reader = command.ExecuteReader

            If Reader.HasRows Then
                While Reader.Read
                    If IsDBNull(Reader.Item(0)) = True Then
                        Manual = False
                        CodeID = ""
                        MnlInsUpd = False
                    Else
                        Manual = Reader.Item(0)
                        CodeID = Reader.Item(1)
                        MnlInsUpd = Reader.Item(2)
                    End If
                End While
            End If
            Reader.Close()
            .Close()
        End With
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Sub CekAkses()
        Me.BVBNew.Enabled = CType(TcodeCollection.Item("BOMN"), Boolean)
        Me.BVBEdit.Enabled = CType(TcodeCollection.Item("BOMEd"), Boolean)
        Me.BVBCancelOrder.Enabled = CType(TcodeCollection.Item("BOMCO"), Boolean)
        Me.BVBApprove.Enabled = CType(TcodeCollection.Item("BOMApr"), Boolean)
        Me.BVBDelete.Enabled = CType(TcodeCollection.Item("BOMDel"), Boolean)
        Me.BVBCariAll.Enabled = CType(TcodeCollection.Item("BOMSA"), Boolean)
    End Sub

    Private Sub LockControl()
        CekAkses()
        Me.BVBExit.Enabled = True
        Me.BVTBOM_s.Enabled = True

        Me.TBKode.Properties.ReadOnly = True
        Me.DTPTanggal.Properties.ReadOnly = True
        'Me.DTPTglKirim.Properties.ReadOnly = True
        Me.TBUnit.Properties.ReadOnly = True
        Me.TBSPK.Properties.ReadOnly = True
        Me.CBOJenis.Properties.ReadOnly = True
        Me.SLUPOID.Properties.ReadOnly = True
        Me.SLUCust.Properties.ReadOnly = True
        Me.SLUMdlID.Properties.ReadOnly = True
        Me.MKet.Properties.ReadOnly = True

        'Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView3.OptionsBehavior.Editable = False

        Me.BProses.Enabled = False
        Me.BSave.Enabled = False
        Me.BSave.Enabled = False
        Me.BCancel.Enabled = False
        Me.BCancel.Enabled = False
    End Sub

    Private Sub OpenControl()
        Me.BVBNew.Enabled = False
        Me.BVBEdit.Enabled = False
        Me.BVBCancelOrder.Enabled = False
        Me.BVBApprove.Enabled = False
        Me.BVBDelete.Enabled = False
        Me.BVBPrintB.Enabled = False
        Me.BVBPrintS.Enabled = False
        Me.BVBCariAll.Enabled = False
        Me.BVBExit.Enabled = False
        Me.BVTBOM_s.Enabled = False

        Me.DTPTanggal.Properties.ReadOnly = False
        'Me.DTPTglKirim.Properties.ReadOnly = False
        Me.TBUnit.Properties.ReadOnly = False
        Me.TBSPK.Properties.ReadOnly = False
        Me.CBOJenis.Properties.ReadOnly = False
        Me.SLUPOID.Properties.ReadOnly = False
        Me.SLUCust.Properties.ReadOnly = False
        Me.SLUMdlID.Properties.ReadOnly = False
        Me.MKet.Properties.ReadOnly = False

        'Me.GridView1.OptionsBehavior.Editable = True
        Me.GridView3.OptionsBehavior.Editable = True

        Me.BProses.Enabled = True
        Me.BSave.Enabled = True
        Me.BCancel.Enabled = True

        Me.BVTBOM_e.Selected = True
    End Sub

    Public Sub LUE()
        Dim cmsl As SqlDataAdapter

        cmsl = New SqlDataAdapter("Select Distinct P.POID,POCust,P.CustID,C.Nama as Cust,B.ArtName,W.Nama as Warna,TglKirim,HCBP,B.Grup,'Job Order' As Gol From T_POBJJO P Inner Join T_POBJJODtl D On P.POID=D.POID Inner Join M_Cust C On P.CustID=C.CustID Inner Join M_Brg B On D.ArtCode=B.ArtCode Inner Join M_BrgWrn W On B.WrnID=W.WrnID Where P.stsKirim='False' and P.stsProd='False' Union All Select Distinct H.POID,'' as POCust,H.CustID,C.Nama as Cust,B.ArtName, W.Nama as Warna,TglKrmAkh As TglKirim,HCBP,H.Grup,H.Gol From T_POBJLk H Inner Join T_POBJLkDtl D On H.POID=D.POID Inner Join M_Brg B On D.ArtCode=B.ArtCode  Inner Join M_BrgWrn W On B.WrnID=W.WrnID Inner Join M_Cust C On H.CustID=C.CustID Where D.stsLunas='False'", koneksi)
        cmsl.TableMappings.Add("Table", "POBJ")
        cmsl.Fill(DsMaster, "POBJ")
        DsMaster.Tables("POBJ").Clear()
        cmsl.Fill(DsMaster, "POBJ")

        Me.SLUPOID.Properties.DataSource = DsMaster.Tables("POBJ")
        Me.SLUPOID.Properties.DisplayMember = "POID"
        Me.SLUPOID.Properties.ValueMember = "POID"

        cmsl = New SqlDataAdapter("Select MdlID,ArtName,Warna,Ket From M_Model", koneksi)
        cmsl.TableMappings.Add("Table", "M_ModelLUE")
        cmsl.Fill(DsMaster, "M_ModelLUE")
        DsMaster.Tables("M_ModelLUE").Clear()
        cmsl.Fill(DsMaster, "M_ModelLUE")

        Me.SLUMdlID.Properties.DataSource = DsMaster.Tables("M_ModelLUE")
        Me.SLUMdlID.Properties.DisplayMember = "MdlID"
        Me.SLUMdlID.Properties.ValueMember = "MdlID"


        cmsl = New SqlDataAdapter("Select CustID,C.Nama,Alamat,K.Nama As Kota From M_Cust C Inner Join M_Kota K On C.KotaID=K.KotaID Where Umum='True' and Aktif='True'", koneksi)
        cmsl.TableMappings.Add("Table", "M_CustL")
        cmsl.Fill(DsMaster, "M_CustL")
        DsMaster.Tables("M_CustL").Clear()
        cmsl.Fill(DsMaster, "M_CustL")

        Me.SLUCust.Properties.DataSource = DsMaster.Tables("M_CustL")
        Me.SLUCust.Properties.DisplayMember = "Nama"
        Me.SLUCust.Properties.ValueMember = "CustID"
    End Sub

    Public Sub FillDtl(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BOMIDD, BOMID, ArtCode, BD.Uk, BD.DivID,D.Nama as Divisi, BD.KompID, K.Nama As Komponen, BD.BBID, B.Nama As Bahan,BD.Uk,BD.UkBB,BD.Sat,Std,Qty,Keb,Pol,BD.Ket,KaliQty,SPK,stsAdd,BD.stsJasa,BD.stsMentah,BD.BBIDInd,(Select Sum (Jml) From(Select Count(*) As Jml From T_POBBDtl D1 Where D1.BOMID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_BPB H1 Inner Join T_BPBDtl D1 On H1.BPBID=D1.BPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID Union All Select Count(*) As Jml From T_RPB H1 Inner Join T_RPBDtl D1 On H1.RPBID=D1.RPBID Where H1.DocID= '" & Kode & "' and BBID=BD.BBID)As x) As PO From T_BOMDtl BD Inner Join M_Div D On BD.DivID=D.DivID Inner Join M_Komp K On BD.KompID=K.KompID Inner Join M_BB B On BD.BBID=B.BBID Where BOMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOMDtl")
        Try
            DsMaster.Tables("T_BOMDtl").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMDtl")

        DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

        Me.GridControl1.DataSource = DsMaster
        Me.GridControl1.DataMember = "T_BOMDtl"

        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Kode & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Kode & "' Order By BP.Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Kode & "' Order By BP.Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select BOMIDD,BOMID,POID,BP.ArtCodeInd,BP.ArtCode,ArtName,BP.SatID,BP.Isi,BP.IsiDlmDos,BP.Uk,Qty,QtyPol,Tot From T_BOMPO BP Inner Join M_Brg B On BP.ArtCode=B.ArtCode Where BOMID='" & Kode & "' Order By Cast(BP.Uk as Decimal(18,2))", koneksi)
            End If
        End If


        cmsl.TableMappings.Add("Table", "T_BOMPO")
        Try
            DsMaster.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOMPO")

        DsMaster.Tables("T_BOMPO").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMPO").Columns("POID"), DsMaster.Tables("T_BOMPO").Columns("ArtCode")}

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_BOMPO"

    End Sub

    Public Sub FillDt()
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select BOMID,B.PeriodID,B.CodeID,B.Tanggal,B.TglAwal,B.TglAkhir,B.TglKirim,B.Jenis,Unit,SPK,S.SpecID,B.MdlID, B.POID,S.Brand,S.StyleID,S.ShoeLast,B.ArtName,B.Warna,M.SampleSize,B.CustID,C.Nama As Cust,B.HCBP,B.Ket,B.KetProd,B.KetLain2,B.TotPsg, B.TotPsgPol,B.SisaPsg,B.stsBatal,B.stsBtlProd,B.stsLunasMan,B.stsAppMkt,B.stsLunas,stsApp,B.Grup,'Job Order' As Gol,B.InsDate,B.InsBy, B.UpdDate,B.UpdBy,B.AppDate,B.AppBy From T_BOM B Inner Join T_POBJJO PJ On B.POID=PJ.POID Left Outer Join M_Model M On B.MdlID=M.MdlID Left Outer Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID=C.CustID Where B.PeriodID=" & MainModule.periodAktif & " Union All Select Distinct BOMID,B.PeriodID,B.CodeID,B.Tanggal,B.TglAwal,B.TglAkhir,B.TglKirim,B.Jenis,Unit,SPK, S.SpecID,B.MdlID,B.POID, S.Brand,S.StyleID,S.ShoeLast, B.ArtName,B.Warna,M.SampleSize,B.CustID,C.Nama As Cust,B.HCBP,B.Ket,B.KetProd,B.KetLain2,B.TotPsg, B.TotPsgPol,B.SisaPsg,B.stsBatal, B.stsBtlProd,B.stsLunasMan,B.stsAppMkt,B.stsLunas,stsApp,B.Grup, B.Gol,B.InsDate,B.InsBy,B.UpdDate, B.UpdBy, B.AppDate,B.AppBy From T_BOM B Inner Join T_POBJLk H On B.POID=H.POID Left Outer Join T_POBJLkDtl PJ On H.POID=PJ.POID Left Outer Join M_Model M On B.MdlID=M.MdlID Left Outer Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID =C.CustID Where B.PeriodID=" & MainModule.periodAktif & " Union All Select BOMID,B.PeriodID,B.CodeID,B.Tanggal,B.TglAwal,B.TglAkhir,B.TglKirim,B.Jenis, Unit,SPK,S.SpecID,B.MdlID,B.POID,S.Brand,S.StyleID,S.ShoeLast, B.ArtName,B.Warna,M.SampleSize,B.CustID,C.Nama As Cust,B.HCBP,B.Ket, B.KetProd,B.KetLain2,B.TotPsg,B.TotPsgPol,B.SisaPsg,B.stsBatal, B.stsBtlProd,B.stsLunasMan,B.stsAppMkt,B.stsLunas,stsApp,B.Grup,B.Gol, B.InsDate,B.InsBy,B.UpdDate,B.UpdBy,B.AppDate,B.AppBy From T_BOM B Left Outer Join M_Model M On B.MdlID=M.MdlID Left Outer Join M_Spec S On S.SpecID=M.SpecID Left Outer Join M_Cust C on B.CustID=C.CustID Where B.PeriodID=" & MainModule.periodAktif & " and POID='' Order By Tanggal,BOMID Asc", koneksi)

        'cmsl = New SqlDataAdapter("Select BOMID,B.PeriodID,B.CodeID,B.Tanggal,Jenis,SPK,S.SpecID,B.MdlID,B.POID,S.Brand,S.StyleID,B.ArtName, B.Warna,C.Nama As Customer,B.HCBP, B.Ket,B.TotPsg,B.TotPsgPol,B.BtlOrder,B.SisaPsg,B.stsBatal,B.stsLunas,'Job Order' As Gol,B.InsDate, B.InsBy, B.UpdDate,B.UpdBy From T_BOM B Left Outer Join T_POBJJO PJ On B.POID=PJ.POID Inner Join M_Cust C on PJ.CustID=C.CustID Inner Join M_Model M On B.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where B.PeriodID=" & MainModule.periodAktif & " Union All Select BOMID,B.PeriodID,B.CodeID,B.Tanggal,Jenis,SPK,S.SpecID,B.MdlID,B.POID,S.Brand,S.StyleID,B.ArtName,B.Warna,PJ.Gol As Customer,B.HCBP,B.Ket,B.TotPsg,B.TotPsgPol,B.BtlOrder,B.SisaPsg,B.stsBatal,B.stsLunas,B.Gol,B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From T_BOM B Inner Join T_POBJLk PJ On B.POID=PJ.POID Inner Join M_Model M On B.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where B.PeriodID=" & MainModule.periodAktif & " Union All Select BOMID,B.PeriodID,B.CodeID,B.Tanggal,Jenis,SPK,S.SpecID,B.MdlID,B.POID,S.Brand, S.StyleID,B.ArtName,B.Warna,Cust As Customer,B.HCBP,B.Ket,B.TotPsg,B.TotPsgPol,B.BtlOrder,B.SisaPsg,B.stsBatal,B.stsLunas,B.Gol, B.InsDate,B.InsBy,B.UpdDate,B.UpdBy From T_BOM B Inner Join M_Model M On B.MdlID=M.MdlID Inner Join M_Spec S On S.SpecID=M.SpecID Where B.PeriodID=" & MainModule.periodAktif & " and POID='' Order By Tanggal,BOMID Asc", koneksi)

        cmsl.TableMappings.Add("Table", "T_BOM")
        Try
            DsMaster.Tables("T_BOM").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "T_BOM")

        Me.GridControl2.DataSource = DsMaster
        Me.GridControl2.DataMember = "T_BOM"
    End Sub

    Public Sub FillBarcode(Kode As String)
        Dim cmsl As SqlDataAdapter
        cmsl = New SqlDataAdapter("Select P.BOMID+'\'+B.ArtCode As Barcode,P.BOMID,B.ArtCode,B.ArtName,B.Ass,P.Qty+P.QtyPol As Tot From T_BOMPO P Inner Join M_Brg B On P.ArtCode=B.ArtCode Where BOMID='" & Kode & "'", koneksi)

        cmsl.TableMappings.Add("Table", "Barcode")
        Try
            DsMaster.Tables("Barcode").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsMaster, "Barcode")

        DsMaster.Tables("Barcode").PrimaryKey = New DataColumn() {DsMaster.Tables("Barcode").Columns("ArtCode")}

        Me.GridControl4.DataSource = DsMaster
        Me.GridControl4.DataMember = "Barcode"

    End Sub

    Private Sub FillQtyPO()
        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Sum(jml) From (Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' Union All Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJLkDtl2 Where POID='" & Me.SLUPOID.EditValue & "') as x", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)

            'cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)

                'cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)

                'cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)
            End If


        End If


        cmsl.TableMappings.Add("Table", "T_BOMPO")
        cmsl.SelectCommand.CommandTimeout = 9000
        cmsl.Fill(DsMaster, "T_BOMPO")

        Me.GridControl3.DataSource = DsMaster
        Me.GridControl3.DataMember = "T_BOMPO"
    End Sub

    Public Sub HitKeb()

        'Dim x : For x = Me.GridView1.RowCount - 1 To 0 Step -1
        Try
            Dim i : For i = 0 To DsMaster.Tables("T_BOMPO").Rows.Count - 1
                Dim x : For x = 0 To DsMaster.Tables("T_BOMDtl").Rows.Count - 1
                    If DsMaster.Tables("T_BOMDtl").Rows(x).RowState <> DataRowState.Deleted Then
                        If DsMaster.Tables("T_BOMDtl").Rows(x).Item("stsAdd") = False Then
                            If DsMaster.Tables("T_BOMPO").Rows(i).RowState <> DataRowState.Deleted Then
                                If DsMaster.Tables("T_BOMPO").Rows(i).Item("ArtCode") = DsMaster.Tables("T_BOMDtl").Rows(x).Item("ArtCode") Then
                                    DsMaster.Tables("T_BOMDtl").Rows(x).Item("Qty") = DsMaster.Tables("T_BOMPO").Rows(i).Item("Tot")
                                End If
                            End If

                            If DsMaster.Tables("T_BOMDtl").Rows(x).Item("KaliQty") = True Then
                                DsMaster.Tables("T_BOMDtl").Rows(x).Item("Keb") = Math.Round(DsMaster.Tables("T_BOMDtl").Rows(x).Item("Qty") * DsMaster.Tables("T_BOMDtl").Rows(x).Item("Std"), 2, MidpointRounding.AwayFromZero)
                            Else
                                If DsMaster.Tables("T_BOMDtl").Rows(x).Item("Std") <> 0 Then
                                    DsMaster.Tables("T_BOMDtl").Rows(x).Item("Keb") = Math.Round(DsMaster.Tables("T_BOMDtl").Rows(x).Item("Qty") / DsMaster.Tables("T_BOMDtl").Rows(x).Item("Std"), 2, MidpointRounding.AwayFromZero)
                                Else
                                    DsMaster.Tables("T_BOMDtl").Rows(x).Item("Keb") = 0
                                End If
                            End If

                            'DsMaster.Tables("T_BOMDtl").Rows(x).Item("Pol") = Math.Round(DsMaster.Tables("T_BOMDtl").Rows(x).Item("Keb") * (DsMaster.Tables("T_BOMDtl").Rows(x).Item("PrsPol") / 100), 2, MidpointRounding.AwayFromZero)
                        End If
                    End If
                Next
            Next

        Catch ex As Exception
            XtraMessageBox.Show("Standart Model ada yang kosong !!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
            Exit Sub
        End Try

    End Sub

    Public Sub Del()
        koneksi.Close()

        Dim cmSP As New SqlCommand("SPDelT_BOM")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
            .Parameters.Add("@By", SqlDbType.VarChar).Value = "DelEr"
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
                XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

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
            Dim link As BaseExportLink = GridView5.CreateExportLink(provider)
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

    Private Sub FBOM_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Bill Of Materials"
    End Sub

    Private Sub FBOM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CekSave = False
    End Sub

    Private Sub FBOM_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
        If BSave.Enabled = True Then
            Me.Focus()
        End If
    End Sub

    Private Sub FBOM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LockControl()
        Me.BVTBOM_e.Selected = True
    End Sub

    Private Sub BVTBOM_s_ItemPressed(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBOM_s.ItemPressed
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Bill Of Materials"
        FillDt()
        Me.BVBPrintB.Enabled = CType(TcodeCollection.Item("BOMPB"), Boolean)
        Me.BVBPrintS.Enabled = CType(TcodeCollection.Item("BOMPS"), Boolean)
        Me.BVBExBarcode.Enabled = CType(TcodeCollection.Item("BOMExEc"), Boolean)
    End Sub

    Private Sub BVBNew_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBNew.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Create Bill Of Materials"

        DsMaster.Clear()

        Me.DTPTanggal.Properties.MinValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, 1)
        Me.DTPTanggal.Properties.MaxValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))

        If System.DateTime.Now.Month <> MainModule.periodeBulan Or System.DateTime.Now.Year <> MainModule.periodeTahun Then
            If MainModule.SlstsPeriodNew() = True Then
                XtraMessageBox.Show("Ubah Periode Aktif Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Me.DTPTanggal.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
            Me.DTPTglKirim.EditValue = New Date(MainModule.periodeTahun, MainModule.periodeBulan, Date.DaysInMonth(MainModule.periodeTahun, MainModule.periodeBulan))
        Else
            Me.DTPTanggal.EditValue = Date.Now
            Me.DTPTglKirim.EditValue = Date.Now
        End If

        OpenControl()
        LUE()
        CekSave = True

        Indicator = "100"

        If Manual = True Then
            Me.TBKode.Properties.ReadOnly = False
            Me.TBKode.EditValue = ""
        Else
            Me.TBKode.Properties.ReadOnly = True
            Me.TBKode.EditValue = "--"
        End If

        Me.TBUnit.EditValue = ""
        Me.TBSPK.EditValue = ""
        Me.CBOJenis.EditValue = "Produksi"
        Me.SLUPOID.EditValue = ""
        Me.SLUCust.EditValue = ""
        Me.TBCBP.EditValue = 0
        Me.TBArtName.EditValue = ""
        Me.TBWarna.EditValue = ""
        Me.SLUMdlID.EditValue = ""
        Me.MKet.EditValue = ""
        Me.TBInfo.EditValue = ""

        FillDtl(Me.TBKode.EditValue)
        DsMaster.Tables("T_BOMDtl").Clear()
        DsMaster.Tables("T_BOMPO").Clear()
        ReDim arrPar(-1)
        ReDim arrPar2(-1)

    End Sub

    Private Sub BVBEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBEdit.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Edit Bill Of Materials"

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Periode Sudah Diclose. Silakan Hubungi Accounting Untuk Membukanya", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        'If MainModule.SlCekBOMPO(Me.GridView2.GetFocusedDataRow.Item("BOMID")) > 0 Then
        '    XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Pengiriman Atau Bahan Sudah Dikeluarkan ke Produksi Atau Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        'If MainModule.SlBOM(Me.GridView2.GetFocusedDataRow.Item("BOMID")) > 0 Then
        '    XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Ada Pengiriman Atau Bahan Sudah Dikeluarkan ke Produksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If SlCek("T_BOM", "stsApp", "BOMID", Me.GridView2.GetFocusedDataRow.Item("BOMID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Diubah Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol") = Me.GridView2.GetFocusedDataRow.Item("SisaPsg") Then
            LUE()

            Indicator = "200"
            Me.TBKode.EditValue = Me.GridView2.GetFocusedDataRow.Item("BOMID")
            Me.DTPTanggal.EditValue = Me.GridView2.GetFocusedDataRow.Item("Tanggal")
            Me.TBUnit.EditValue = Me.GridView2.GetFocusedDataRow.Item("Unit")
            Me.TBSPK.EditValue = Me.GridView2.GetFocusedDataRow.Item("SPK")
            Me.CBOJenis.EditValue = Me.GridView2.GetFocusedDataRow.Item("Jenis")
            Me.SLUPOID.EditValue = Me.GridView2.GetFocusedDataRow.Item("POID")
            Me.SLUCust.EditValue = Me.GridView2.GetFocusedDataRow.Item("CustID")
            Me.DTPTglKirim.EditValue = Me.GridView2.GetFocusedDataRow.Item("TglKirim")
            Me.TBCBP.EditValue = Me.GridView2.GetFocusedDataRow.Item("HCBP")
            Me.TBArtName.EditValue = Me.GridView2.GetFocusedDataRow.Item("ArtName")
            Me.TBWarna.EditValue = Me.GridView2.GetFocusedDataRow.Item("Warna")
            Gol = Me.GridView2.GetFocusedDataRow.Item("Gol")
            Grup = Me.GridView2.GetFocusedDataRow.Item("Grup")

            Dim cmsl As SqlDataAdapter

            cmsl = New SqlDataAdapter("Select MdlID,ArtName,Warna,Ket From M_Model Where ArtName='" & Me.TBArtName.EditValue & "'", koneksi)
            cmsl.TableMappings.Add("Table", "M_ModelLUE")
            Try
                DsMaster.Tables("M_ModelLUE").Clear()
            Catch ex As Exception

            End Try
            cmsl.Fill(DsMaster, "M_ModelLUE")

            Me.SLUMdlID.Properties.DataSource = DsMaster.Tables("M_ModelLUE")
            Me.SLUMdlID.Properties.DisplayMember = "MdlID"
            Me.SLUMdlID.Properties.ValueMember = "MdlID"

            Me.SLUMdlID.EditValue = Me.GridView2.GetFocusedDataRow.Item("MdlID")
            Me.MKet.EditValue = Me.GridView2.GetFocusedDataRow.Item("Ket")

            FillDtl(Me.TBKode.EditValue)
            ReDim arrPar(-1)
            ReDim arrPar2(-1)

            If IsDBNull(Me.GridView2.GetFocusedDataRow.Item("UpdBy")) Then
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy")
            Else
                Me.TBInfo.EditValue = "(I) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("InsDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("InsBy") & ", (U) " & Format(CDate(Me.GridView2.GetFocusedDataRow.Item("UpdDate")), "dd/MM/yy hh:mm:ss") & " " & Me.GridView2.GetFocusedDataRow.Item("UpdBy")
            End If

            OpenControl()
            CekSave = True

            If MainModule.SlCekBOMPO(Me.GridView2.GetFocusedDataRow.Item("BOMID")) > 0 Then
                Me.GridView3.OptionsBehavior.Editable = False
            End If

            Me.TBSPK.Properties.ReadOnly = True

        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diproduksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If Me.CBOJenis.EditValue = "Produksi" Then
            Me.SLUMdlID.Properties.ReadOnly = False
        Else
            Me.SLUMdlID.Properties.ReadOnly = True
        End If

    End Sub
    Private Sub BVBCancelOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCancelOrder.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Cancel Order Bill Of Materials"

        If Me.GridView2.GetFocusedDataRow.Item("stsLunas") = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dibatalkan Karena Sudah Lunas Dikirim", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlCekBOMPO(Me.GridView2.GetFocusedDataRow.Item("BOMID")) > 0 Then
            If XtraMessageBox.Show("BOM Sudah Ada Pengiriman Atau Bahan Sudah Dikeluarkan ke Produksi Atau Sudah Ditarik PO. Apakah Mau Dibatalkan ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Dim frm As New FSisaBOM(Me.GridView2.GetFocusedDataRow.Item("BOMID"), Me.GridView2.GetFocusedDataRow.Item("stsBtlProd"), Me.GridView2.GetFocusedDataRow.Item("stsBatal"), Me.GridView2.GetFocusedDataRow.Item("stsLunasMan"))

                frm = New FSisaBOM(Me.GridView2.GetFocusedDataRow.Item("BOMID"), Me.GridView2.GetFocusedDataRow.Item("stsBtlProd"), Me.GridView2.GetFocusedDataRow.Item("stsBatal"), Me.GridView2.GetFocusedDataRow.Item("stsLunasMan"))
                frm.ShowDialog()
                frm.Dispose()
                frm.Close()
            Else
                Exit Sub
            End If

        Else

            If XtraMessageBox.Show("Apakah Anda Mau Membatalkan Sisa BOM : " & Me.GridView2.GetFocusedDataRow.Item("BOMID") & " Yang Belum Diproduksi ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim frm As New FSisaBOM(Me.GridView2.GetFocusedDataRow.Item("BOMID"), Me.GridView2.GetFocusedDataRow.Item("stsBtlProd"), Me.GridView2.GetFocusedDataRow.Item("stsBatal"), Me.GridView2.GetFocusedDataRow.Item("stsLunasMan"))

                frm = New FSisaBOM(Me.GridView2.GetFocusedDataRow.Item("BOMID"), Me.GridView2.GetFocusedDataRow.Item("stsBtlProd"), Me.GridView2.GetFocusedDataRow.Item("stsBatal"), Me.GridView2.GetFocusedDataRow.Item("stsLunasMan"))
                frm.ShowDialog()
                frm.Dispose()
                frm.Close()
            End If

        End If

    End Sub
    Private Sub BVBApprove_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBApprove.ItemClick
        koneksi.Close()
        If SlCek("T_BOM", "stsApp", "BOMID", Me.GridView2.GetFocusedDataRow.Item("BOMID")) = True Then
            XtraMessageBox.Show("Data Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim cmSP As New SqlCommand("SPAppBOMReq")
        cmSP.CommandType = CommandType.StoredProcedure
        Dim x As Integer

        With cmSP
            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BOMID")
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
                    XtraMessageBox.Show("Data Berhasil Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    FillDt()
                Else
                    XtraMessageBox.Show("Data Gagal Diapprove", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

            Catch ex As Exception
                XtraMessageBox.Show("Data Gagal Diapprove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End With
    End Sub

    Private Sub BVBDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBDelete.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Delete Bill Of Materials"

        koneksi.Close()

        If MainModule.SlstsPeriodEdDel(Me.GridView2.GetFocusedDataRow.Item("PeriodID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlAdjBJ() > 0 Then
            If MainModule.BackDate = False Then
                XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Closed", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If SlCek("T_BOM", "stsApp", "BOMID", Me.GridView2.GetFocusedDataRow.Item("BOMID")) = True Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diapprove", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If MainModule.SlCekBOMPO(Me.GridView2.GetFocusedDataRow.Item("BOMID")) > 0 Then
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Ada Pengiriman Atau Bahan Sudah Dikeluarkan ke Produksi Atau Sudah Ditarik PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol") = Me.GridView2.GetFocusedDataRow.Item("SisaPsg") Then
            If XtraMessageBox.Show("Apakah Anda Mau Menghapus Data : " & Me.GridView2.GetFocusedDataRow.Item("BOMID") & " Ini ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim cmSP As New SqlCommand("SPDelT_BOM")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.GridView2.GetFocusedDataRow.Item("BOMID")
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
                            XtraMessageBox.Show("Data Berhasil Dihapus", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            FillDt()
                        Else
                            MsgBox("Masuk 1")
                            XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    Catch ex As Exception
                        MsgBox("Masuk 2")
                        XtraMessageBox.Show("Data Gagal Dihapus", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End With
            End If
        Else
            XtraMessageBox.Show("Data Tidak Bisa Dihapus Karena Sudah Diproduksi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub BVBPrintB_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintB.ItemClick
        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BOMID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Tanggal"), "Tanggal")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "Tot")

        Dim XR As New XRBOM
        XR.InitializeData(bind, "0")
    End Sub

    Private Sub BVBPrintS_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBPrintS.ItemClick
        Dim frm As New FBOM_uk(Me.GridView2.GetFocusedDataRow.Item("BOMID"))

        frm = New FBOM_uk(Me.GridView2.GetFocusedDataRow.Item("BOMID"))
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim frmDiv As New FBOM_Div(Me.GridView2.GetFocusedDataRow.Item("BOMID"))

        frmDiv = New FBOM_Div(Me.GridView2.GetFocusedDataRow.Item("BOMID"))
        frmDiv.ShowDialog()
        frmDiv.Dispose()
        frmDiv.Close()

        Dim bind As New Collection
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("BOMID"), "Kode")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SPK"), "SPK")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Cust"), "Customer")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("POID"), "POID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Brand"), "Brand")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("StyleID"), "StyleID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SpecID"), "SpecID")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Warna"), "Warna")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ArtName"), "ArtName")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("HCBP"), "HCBP")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("Ket"), "Ket")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TglKirim"), "TglKirim")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("ShoeLast"), "ShoeLast")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("SampleSize"), "SampleSize")
        bind.Add(Me.GridView2.GetFocusedDataRow.Item("TotPsg") + Me.GridView2.GetFocusedDataRow.Item("TotPsgPol"), "Tot")

        Dim cmsl As SqlDataAdapter
        Dim jml, assx As Integer

        Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "'", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_BOMPO Where BOMID ='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' and (Uk Like '%x%' Or Uk Like '%M%')", koneksi)

        With koneksi
            .Open()
            assx = command2.ExecuteScalar()
            .Close()
        End With

        If jml > 4 Then
            cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Uk", koneksi)
        Else
            If assx > 0 Then
                cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Uk", koneksi)
            Else
                cmsl = New SqlDataAdapter("Select Uk,Tot,IsiDlmDos From T_BOMPO Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "' Order By Cast(Uk as Decimal(18,2))", koneksi)
            End If
        End If

        cmsl.TableMappings.Add("Table", "T_BOMPO")
        DsLap = New System.Data.DataSet
        Try
            DsLap.Tables("T_BOMPO").Clear()
        Catch ex As Exception

        End Try
        cmsl.Fill(DsLap, "T_BOMPO")

        Dim i : For i = 1 To 8
            If i <= DsLap.Tables("T_BOMPO").Rows.Count Then
                Dim x : For x = 0 To DsLap.Tables("T_BOMPO").Rows.Count - 1
                    If i = x + 1 Then
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("Uk"), "Uk" & i)
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("Tot"), "Qty" & i)
                        bind.Add(DsLap.Tables("T_BOMPO").Rows(x).Item("IsiDlmDos"), "Isi" & i)
                    End If
                Next
            Else
                bind.Add("", "Uk" & i)
                bind.Add("", "Qty" & i)
                bind.Add("", "Isi" & i)
            End If
        Next
        bind.Add(dataTrans.Item(1).ToString, "UkStd")
        bind.Add(dataTrans.Item(2).ToString, "divisi")

        Dim XR As New XRSPK
        XR.InitializeData(bind)
    End Sub

    Private Sub BVBExBarcode_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExBarcode.ItemClick
        Dim jml As Integer
        Dim command As New SqlCommand("Select Isnull((Select Count(*) From T_BOMPO P Inner Join M_Brg B On P.ArtCode=B.ArtCode Inner Join T_ProsesKrj PK On Art=B.MerkID+B.KatID+B.JnsID+'-'+B.Urut Where BOMID='" & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "'),0)", koneksi)

        With koneksi
            .Open()
            jml = command.ExecuteScalar()
            .Close()
        End With

        If jml = 0 Then
            XtraMessageBox.Show("Proses Kerja Belum Disetting", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls", "Data Barcode " & Me.GridView2.GetFocusedDataRow.Item("BOMID") & "")

        FillBarcode(Me.GridView2.GetFocusedDataRow.Item("BOMID"))

        If fileName <> "" Then
            ExportTo(New ExportXlsProvider(fileName))

            OpenFile(fileName)
        End If
    End Sub

    Private Sub BVBCariAll_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBCariAll.ItemClick
        CType(Me.MdiParent, FUtama).BSIInfo.Caption = "Search Model"

        Dim frm As New FBOM_sa
        frm.ShowDialog()
    End Sub

    Private Sub BVBExit_ItemClick(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVBExit.ItemClick
        CekSave = False
        Me.Dispose()
    End Sub

    Private Sub BSave_Click(sender As Object, e As EventArgs) Handles BSave.Click
        If Me.CBOJenis.EditValue = "" Or IsDBNull(Me.CBOJenis.EditValue) Then
            XtraMessageBox.Show("Jenis Harus Dipilih", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBSPK.EditValue = "" Or IsDBNull(Me.TBSPK.EditValue) Then
            XtraMessageBox.Show("SPK Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Me.TBUnit.EditValue = "" Or IsDBNull(Me.TBUnit.EditValue) Then
            XtraMessageBox.Show("Unit Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        koneksi.Close()
        Me.GridView1.ActiveFilter.Clear()
        Me.GridView3.ActiveFilter.Clear()

        Select Case Indicator
            Case 100
                Dim cmSP As New SqlCommand("SPInsT_BOM")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@KdInput", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
                    .Parameters.Add("@CodeID", SqlDbType.VarChar).Value = CodeID
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.TBUnit.EditValue
                    .Parameters.Add("@SPK", SqlDbType.VarChar).Value = Me.TBSPK.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.TBArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.TBWarna.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@MdlID", SqlDbType.VarChar).Value = Me.SLUMdlID.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("Qty").SummaryText, Decimal), 1)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("QtyPol").SummaryText, Decimal), 1)
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Grup
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
                    .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
                    .Parameters.Add("@Return", SqlDbType.Int)
                    .Parameters("@Return").Direction = ParameterDirection.Output
                    .Parameters.Add("@Kode", SqlDbType.VarChar, 30)
                    .Parameters("@Kode").Direction = ParameterDirection.Output
                    .Connection = koneksi

                    Try
                        With koneksi
                            .Open()
                            cmSP.ExecuteNonQuery()
                            Me.TBKode.EditValue = cmSP.Parameters("@Kode").Value
                            x = cmSP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 1 Then
                            'Del()
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim i : For i = 0 To Me.GridView1.RowCount - 1
                            If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_BOMDtl")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                    .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                    .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                    .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                    .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                    .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                    .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                    .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                    '.Parameters.Add("@PrsPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PrsPol")
                                    .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                    .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                    .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                    .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                    .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                    .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                    .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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

                        Dim z : For z = 0 To Me.GridView3.RowCount - 1
                            If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                Dim cmSPDtl As New SqlCommand("SPInsT_BOMPO")
                                cmSPDtl.CommandType = CommandType.StoredProcedure

                                With cmSPDtl
                                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                    .Parameters.Add("@ArtCodeInd", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCodeInd")
                                    .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                    .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                    .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Isi")
                                    .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                    .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                    .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                    .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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

                        Dim cmSPBP As New SqlCommand("SPInsT_BOMstsPO")
                        cmSPBP.CommandType = CommandType.StoredProcedure

                        With cmSPBP
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        With koneksi
                            .Open()
                            cmSPBP.ExecuteNonQuery()
                            x = cmSPBP.Parameters("@Return").Value
                            .Close()
                        End With

                        If x = 0 Then
                            XtraMessageBox.Show("Data Berhasil Disimpan Dengan ID : " & Me.TBKode.EditValue & "", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ElseIf x = 1 Then
                            'Del()
                            XtraMessageBox.Show("Id Tidak Boleh Sama", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        Else
                            Del()
                            XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    Catch ex As Exception
                        Del()
                        XtraMessageBox.Show("Data Gagal Disimpan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End With

            Case 200
                Dim cmSP As New SqlCommand("SPUpT_BOM")
                cmSP.CommandType = CommandType.StoredProcedure
                Dim x As Integer

                With cmSP
                    .Parameters.Add("@Manual", SqlDbType.Bit).Value = Manual
                    .Parameters.Add("@MnlInsUpd", SqlDbType.Bit).Value = MnlInsUpd
                    .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                    .Parameters.Add("@Tgl", SqlDbType.Date).Value = Me.DTPTanggal.EditValue
                    .Parameters.Add("@Jenis", SqlDbType.VarChar).Value = Me.CBOJenis.EditValue
                    .Parameters.Add("@Unit", SqlDbType.VarChar).Value = Me.TBUnit.EditValue
                    .Parameters.Add("@SPK", SqlDbType.VarChar).Value = Me.TBSPK.EditValue
                    .Parameters.Add("@MdlID", SqlDbType.VarChar).Value = Me.SLUMdlID.EditValue
                    .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                    .Parameters.Add("@TglKirim", SqlDbType.Date).Value = Me.DTPTglKirim.EditValue
                    .Parameters.Add("@ArtName", SqlDbType.VarChar).Value = Me.TBArtName.EditValue
                    .Parameters.Add("@Warna", SqlDbType.VarChar).Value = Me.TBWarna.EditValue
                    .Parameters.Add("@CustID", SqlDbType.VarChar).Value = Me.SLUCust.EditValue
                    .Parameters.Add("@CBP", SqlDbType.Decimal).Value = Me.TBCBP.EditValue
                    .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.MKet.EditValue
                    .Parameters.Add("@TotPsg", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("Qty").SummaryText, Decimal), 1)
                    .Parameters.Add("@TotPsgPol", SqlDbType.Decimal).Value = Math.Round(CType(Me.GridView3.Columns("QtyPol").SummaryText, Decimal), 1)
                    .Parameters.Add("@Grup", SqlDbType.VarChar).Value = Grup
                    .Parameters.Add("@Gol", SqlDbType.VarChar).Value = Gol
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

                        If x = -1 Then
                            XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                        Dim y : For y = 0 To arrPar.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_BOMDtl")
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

                        Dim q : For q = 0 To arrPar2.GetUpperBound(0)
                            Dim cmSPDel As New SqlCommand("SPDelT_BOMPO")
                            cmSPDel.CommandType = CommandType.StoredProcedure

                            With cmSPDel
                                .Parameters.Add("@Id", SqlDbType.Int).Value = arrPar2(q)
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

                        Dim i : For i = 0 To GridView1.RowCount - 1
                            If Me.GridView1.GetRowCellValue(i, "BOMIDD") < 0 Then
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BOMDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                        '.Parameters.Add("@PrsPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PrsPol")
                                        .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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
                                        Me.GridView1.SetRowCellValue(i, "BOMIDD", Me.GridView1.GetRowCellValue(i, "BOMIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView1.GetRowCellValue(i, "BBID")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BOMDtl")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BOMIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "ArtCode")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Uk")
                                        .Parameters.Add("@DivID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "DivID")
                                        .Parameters.Add("@KompID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "KompID")
                                        .Parameters.Add("@BBID", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBID")
                                        .Parameters.Add("@UkBB", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "UkBB")
                                        .Parameters.Add("@Sat", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Sat")
                                        .Parameters.Add("@Std", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Std")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Qty")
                                        .Parameters.Add("@Keb", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Keb")
                                        '.Parameters.Add("@PrsPol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "PrsPol")
                                        .Parameters.Add("@Pol", SqlDbType.Decimal).Value = Me.GridView1.GetRowCellValue(i, "Pol")
                                        .Parameters.Add("@Ket", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "Ket")
                                        .Parameters.Add("@KaliQty", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "KaliQty")
                                        .Parameters.Add("@SPK", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "SPK")
                                        .Parameters.Add("@Add", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsAdd")
                                        .Parameters.Add("@stsJasa", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsJasa")
                                        .Parameters.Add("@stsMentah", SqlDbType.Bit).Value = Me.GridView1.GetRowCellValue(i, "stsMentah")
                                        .Parameters.Add("@BBIDInd", SqlDbType.VarChar).Value = Me.GridView1.GetRowCellValue(i, "BBIDInd")
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

                                    If x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        Dim z : For z = 0 To GridView3.RowCount - 1
                            If Me.GridView3.GetRowCellValue(z, "BOMIDD") < 0 Then
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(z, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPInsT_BOMPO")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                        .Parameters.Add("@ArtCodeInd", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCodeInd")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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
                                        Me.GridView3.SetRowCellValue(i, "BOMIDD", Me.GridView3.GetRowCellValue(z, "BOMIDD") * -1)
                                    ElseIf x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                If Not IsDBNull(Me.GridView3.GetRowCellValue(i, "ArtCode")) Then
                                    Dim cmSPDtl As New SqlCommand("SPUpT_BOMPO")
                                    cmSPDtl.CommandType = CommandType.StoredProcedure

                                    With cmSPDtl
                                        .Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "BOMIDD")
                                        .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                                        .Parameters.Add("@POID", SqlDbType.VarChar).Value = Me.SLUPOID.EditValue
                                        .Parameters.Add("@ArtCodeInd", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCodeInd")
                                        .Parameters.Add("@ArtCode", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "ArtCode")
                                        .Parameters.Add("@SatID", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "SatID")
                                        .Parameters.Add("@Isi", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "Isi")
                                        .Parameters.Add("@IsiDlmDos", SqlDbType.Int).Value = Me.GridView3.GetRowCellValue(z, "IsiDlmDos")
                                        .Parameters.Add("@Uk", SqlDbType.VarChar).Value = Me.GridView3.GetRowCellValue(z, "Uk")
                                        .Parameters.Add("@Qty", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "Qty")
                                        .Parameters.Add("@QtyPol", SqlDbType.Decimal).Value = Me.GridView3.GetRowCellValue(z, "QtyPol")
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

                                    If x = -1 Then
                                        XtraMessageBox.Show("Data Gagal Diubah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next

                        Dim cmSPBP As New SqlCommand("SPInsT_BOMstsPO")
                        cmSPBP.CommandType = CommandType.StoredProcedure

                        With cmSPBP
                            .Parameters.Add("@Kode", SqlDbType.VarChar).Value = Me.TBKode.EditValue
                            .Parameters.Add("@Return", SqlDbType.Int)
                            .Parameters("@Return").Direction = ParameterDirection.Output
                            .Connection = koneksi
                        End With

                        With koneksi
                            .Open()
                            cmSPBP.ExecuteNonQuery()
                            x = cmSPBP.Parameters("@Return").Value
                            .Close()
                        End With

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
        CekSave = False
    End Sub

    Private Sub BCancel_Click(sender As Object, e As EventArgs) Handles BCancel.Click
        LockControl()
        CekSave = False
    End Sub

    Private Sub SLUPOID_Leave(sender As Object, e As EventArgs) Handles SLUPOID.Leave
        If Me.SLUPOID.Properties.ReadOnly = False Then
            If Me.SLUPOID.EditValue <> "" Then
                Me.SLUCust.Properties.ReadOnly = True
                Me.SLUCust.EditValue = DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("CustID")
                Me.TBCBP.EditValue = DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("HCBP")
                Me.DTPTglKirim.EditValue = DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("TglKirim")

                Gol = DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("Gol")
                Grup = DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("Grup")

                If Me.CBOJenis.EditValue = "Produksi" Then
                    Me.SLUMdlID.Properties.ReadOnly = False

                    Dim cmsl As SqlDataAdapter
                    MsgBox(DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("ArtName"))
                    MsgBox(DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("Warna"))

                    cmsl = New SqlDataAdapter("Select MdlID,ArtName,Warna,Ket From M_Model Where ArtName='" & DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("ArtName") & "' and Warna='" & DsMaster.Tables("POBJ").Rows(SearchLookUpEdit1View.FocusedRowHandle).Item("Warna") & "'", koneksi)
                    cmsl.TableMappings.Add("Table", "M_ModelLUE")
                    cmsl.Fill(DsMaster, "M_ModelLUE")
                    DsMaster.Tables("M_ModelLUE").Clear()
                    cmsl.Fill(DsMaster, "M_ModelLUE")

                    Me.SLUMdlID.Properties.DataSource = DsMaster.Tables("M_ModelLUE")
                    Me.SLUMdlID.Properties.DisplayMember = "MdlID"
                    Me.SLUMdlID.Properties.ValueMember = "MdlID"

                    Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                        arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "BOMIDD")

                        Me.GridView3.DeleteRow(x)
                    Next

                Else
                    Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                        ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                        arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "BOMIDD")

                        Me.GridView3.DeleteRow(x)
                    Next

                    Me.SLUMdlID.Properties.ReadOnly = True

                    Dim cmsl As SqlDataAdapter
                    cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select PJ.ArtCode AS ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,ArtName,'P' as SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Where POID='" & Me.SLUPOID.EditValue & "' Union All Select Distinct PLH.ArtCode As ArtCodeInd,PL.ArtCode,ArtName,'P' As SatID,1 As Isi,IsiDlmDos,PL.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=PL.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=PL.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJLkDtl2 PL Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Inner Join M_Brg B On PL.ArtCode=B.ArtCode Where PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)

                    cmsl.TableMappings.Add("Table", "T_BOMPO")
                    cmsl.SelectCommand.CommandTimeout = 9000
                    cmsl.Fill(DsMaster, "T_BOMPO")

                    Me.GridControl3.DataSource = DsMaster
                    Me.GridControl3.DataMember = "T_BOMPO"
                End If


                Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "BOMIDD")

                    Me.GridView1.DeleteRow(y)
                Next

            Else
                Me.SLUCust.Properties.ReadOnly = False

            End If
        End If
    End Sub

    Private Sub SLUMdlID_Leave(sender As Object, e As EventArgs) Handles SLUMdlID.Leave
        If Me.SLUMdlID.Properties.ReadOnly = False Then
            Try
                'RemoveHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

                Dim x : For x = Me.GridView3.RowCount - 1 To 0 Step -1
                    ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                    arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(x, "BOMIDD")

                    Me.GridView3.DeleteRow(x)
                Next

                Me.TBArtName.EditValue = DsMaster.Tables("M_ModelLUE").Rows(SearchLookUpEdit2View.FocusedRowHandle).Item("ArtName")
                Me.TBWarna.EditValue = DsMaster.Tables("M_ModelLUE").Rows(SearchLookUpEdit2View.FocusedRowHandle).Item("Warna")
                Me.MKet.EditValue = DsMaster.Tables("M_ModelLUE").Rows(SearchLookUpEdit2View.FocusedRowHandle).Item("Ket")

                Select Case Indicator
                    Case 100

                        Try
                            DsMaster.Tables("T_BOMDtl").Clear()
                            DsMaster.Tables("T_BOMPO").Clear()
                        Catch ex As Exception

                        End Try

                        Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "BOMIDD")

                            Me.GridView1.DeleteRow(y)
                        Next


                        Dim cmsl As SqlDataAdapter
                        Dim jml, assx As Integer

                        If Me.CBOJenis.EditValue = "Produksi" Then
                            FillQtyPO()

                            '    Dim command As New SqlCommand("Select Sum(jml) From (Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' Union All Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJLkDtl2 Where POID='" & Me.SLUPOID.EditValue & "') as x", koneksi)

                            '    With koneksi
                            '        .Open()
                            '        jml = command.ExecuteScalar()
                            '        .Close()
                            '    End With

                            '    Dim command2 As New SqlCommand("Select Isnull(Max(Len(Ass)),0) From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' and Ass Like '%x%'", koneksi)

                            '    With koneksi
                            '        .Open()
                            '        assx = command.ExecuteScalar()
                            '        .Close()
                            '    End With

                            '    If jml > 4 Then
                            '        cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
                            '    Else
                            '        If assx > 0 Then
                            '            cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
                            '        Else
                            '            cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False')/PJ.Isi) As Qty, (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO P Inner Join T_BOM B On P.BOMID=B.BOMID Where P.POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and P.BOMID <> '" & Me.TBKode.EditValue & "' and B.stsBatal='False') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)
                            '        End If


                            '    End If


                            '    cmsl.TableMappings.Add("Table", "T_BOMPO")
                            '    cmsl.SelectCommand.CommandTimeout = 9000
                            '    cmsl.Fill(DsMaster, "T_BOMPO")

                            '    Me.GridControl3.DataSource = DsMaster
                            '    Me.GridControl3.DataMember = "T_BOMPO"

                            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,M.UkBB,B.Nama As Bahan,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where Exists ((Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' Union All Select ArtCode From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and M.ArtCode=PL.ArtCode) as x)) and MdlID='" & Me.SLUMdlID.EditValue & "' Order By DivID,K.Urut,KompID,BBID,M.Uk,M.BBIDInd", koneksi)

                            'Memakai Polasi di Divisi
                            'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,D.PrsPol,M.KompID,K.Nama As Komponen,M.BBID,M.UkBB,B.Nama As Bahan,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where Exists ((Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' Union All Select ArtCode From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and M.ArtCode=PL.ArtCode) as x)) and MdlID='" & Me.SLUMdlID.EditValue & "' Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)

                            cmsl.TableMappings.Add("Table", "T_BOMDtl")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMDtl")

                            DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

                            Me.GridControl1.DataSource = DsMaster
                            Me.GridControl1.DataMember = "T_BOMDtl"

                        Else

                            Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From M_ModelDtl Where MdlID='" & Me.SLUPOID.EditValue & "'", koneksi)

                            With koneksi
                                .Open()
                                jml = command.ExecuteScalar()
                                .Close()
                            End With


                            Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

                            With koneksi
                                .Open()
                                assx = command2.ExecuteScalar()
                                .Close()
                            End With

                            If jml > 4 Then
                                cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Uk", koneksi)
                            Else
                                If assx > 0 Then
                                    cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Uk", koneksi)
                                Else
                                    cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)
                                End If
                            End If

                            cmsl.TableMappings.Add("Table", "T_BOMPO")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMPO")

                            Me.GridControl3.DataSource = DsMaster
                            Me.GridControl3.DataMember = "T_BOMPO"

                            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' Order By DivID,K.Urut,KompID,BBID,M.Uk,M.BBIDInd", koneksi)

                            'Memakai Polasi di Divisi
                            'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,D.PrsPol,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)
                            cmsl.TableMappings.Add("Table", "T_BOMDtl")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMDtl")

                            DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

                            Me.GridControl1.DataSource = DsMaster
                            Me.GridControl1.DataMember = "T_BOMDtl"
                        End If

                    Case 200

                        ''Dim Hapus As Boolean

                        ''Dim a : For a = Me.GridView1.RowCount - 1 To 0 Step -1
                        ''    If DsMaster.Tables("POBB").Rows.Count - 1 > 0 Then
                        ''        Dim z : For z = 0 To DsMaster.Tables("POBB").Rows.Count - 1
                        ''            If Me.GridView1.GetRowCellValue(a, "BBID") = DsMaster.Tables("POBB").Rows(z).Item("BBID") Then
                        ''                Hapus = False
                        ''                Exit For
                        ''            Else
                        ''                Hapus = True
                        ''            End If
                        ''        Next
                        ''    Else
                        ''        Hapus = True
                        ''    End If

                        ''    If Hapus = True Then
                        ''        ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                        ''        arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(a, "BOMIDD")

                        ''        Me.GridView1.DeleteRow(a)
                        ''    End If
                        ''Next

                        Dim a : For a = Me.GridView3.RowCount - 1 To 0 Step -1
                            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
                            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetRowCellValue(a, "BOMIDD")

                            Me.GridView3.DeleteRow(a)
                        Next

                        Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
                            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "BOMIDD")

                            Me.GridView1.DeleteRow(y)
                        Next

                        Dim jml, assx As Integer
                        Dim cmsl As SqlDataAdapter

                        If Me.CBOJenis.EditValue = "Produksi" Then
                            FillQtyPO()

                            'Dim command As New SqlCommand("Select Sum(jml) From (Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' Union All Select Isnull(Max(Len(Uk)),0) As Jml From T_POBJLkDtl2 Where POID='" & Me.SLUPOID.EditValue & "') as x", koneksi)

                            'With koneksi
                            '    .Open()
                            '    jml = command.ExecuteScalar()
                            '    .Close()
                            'End With


                            'Dim command2 As New SqlCommand("Select Isnull(Max(Len(Ass)),0) From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' and Ass Like '%x%'", koneksi)

                            'With koneksi
                            '    .Open()
                            '    assx = command.ExecuteScalar()
                            '    .Close()
                            'End With

                            'If jml > 4 Then
                            '    cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty,(PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
                            'Else
                            '    If assx > 0 Then
                            '        cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty,(PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Uk", koneksi)
                            '    Else
                            '        cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "')/PJ.Isi) As Qty,(PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCodeInd=PJ.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl2 PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLkDtl PLH On PL.POID=PLH.POID and PLH.POIDD=PL.POIDDtl Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)
                            '    End If


                            'End If

                            ''cmsl = New SqlDataAdapter("Select '" & Me.SLUPOID.EditValue & "' As POID,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD,ArtCodeInd,ArtCode,'" & Me.TBArtName.EditValue & "' As ArtName,SatID,Isi,IsiDlmDos,Uk,Qty,QtyPol,Qty+QtyPol As Tot From (Select Distinct PJ.ArtCode As ArtCodeInd,B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode,'P' As SatID,1 As Isi,AD.Qty AS IsiDlmDos,AD.Uk,(PJ.Qty*AD.Qty)-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As Qty,(PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From T_POBJJODtl PJ  Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where MdlID='" & Me.SLUMdlID.EditValue & "' and POID='" & Me.SLUPOID.EditValue & "' UNION ALL Select Distinct PLH.ArtCode As ArtCodeInd,MD.ArtCode,'P' As SatID,1 As Isi,IsiDlmDos,MD.Uk,PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As Qty,PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode=MD.ArtCode and BOMID <> '" & Me.TBKode.EditValue & "') As QtyPol From M_ModelDtl MD Inner Join T_POBJLkDtl PL On MD.ArtCode=PL.ArtCode Inner Join T_POBJLk PLH On PL.POID=PLH.POID Where MdlID='" & Me.SLUMdlID.EditValue & "' and PLH.POID='" & Me.SLUPOID.EditValue & "') As x", koneksi)
                            'cmsl.TableMappings.Add("Table", "T_BOMPO")
                            'cmsl.SelectCommand.CommandTimeout = 9000
                            'cmsl.Fill(DsMaster, "T_BOMPO")

                            'Me.GridControl3.DataSource = DsMaster
                            'Me.GridControl3.DataMember = "T_BOMPO"

                            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,K.Urut,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where Exists ((Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' Union All Select ArtCode From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and M.ArtCode=PL.ArtCode) as x)) and MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,Urut,KompID,BBID,Uk,M.BBIDInd", koneksi)

                            'Memakai Polasi di Divisi
                            'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,D.PrsPol,K.Urut,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where Exists ((Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' Union All Select ArtCode From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and M.ArtCode=PL.ArtCode) as x)) and MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,Urut,KompID,BBID,Uk", koneksi)
                            cmsl.TableMappings.Add("Table", "T_BOMDtl")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMDtl")

                            DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

                            Me.GridControl1.DataSource = DsMaster
                            Me.GridControl1.DataMember = "T_BOMDtl"

                        Else
                            Dim command As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From M_ModelDtl Where MdlID='" & Me.SLUPOID.EditValue & "'", koneksi)

                            With koneksi
                                .Open()
                                jml = command.ExecuteScalar()
                                .Close()
                            End With

                            Dim command2 As New SqlCommand("Select Isnull(Max(Len(Uk)),0) From T_POBJJODtl Where POID='" & Me.SLUPOID.EditValue & "' and (Uk Like '%x%' or Uk Like '%M%')", koneksi)

                            With koneksi
                                .Open()
                                assx = command2.ExecuteScalar()
                                .Close()
                            End With

                            If jml > 4 Then
                                cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Uk", koneksi)
                            Else
                                If assx > 0 Then
                                    cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Uk", koneksi)
                                Else
                                    cmsl = New SqlDataAdapter("Select '' As POID,*,ROW_NUMBER() over (ORDER BY ArtCode)*-1 as BOMIDD From (Select Distinct M.ArtCode As ArtCodeInd, M.ArtCode, B.ArtName,SatId,Isi,1 As IsiDlmDos,Uk,0 As Qty,0 As QtyPol,0 As Tot From M_ModelDtl M Inner Join M_Brg B On M.ArtCode=B.ArtCode  Where MdlID='" & Me.SLUMdlID.EditValue & "') As x Order By Cast(Uk as Decimal(18,2))", koneksi)
                                End If
                            End If

                            cmsl.TableMappings.Add("Table", "T_BOMPO")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMPO")
                            Me.GridControl3.DataSource = DsMaster
                            Me.GridControl3.DataMember = "T_BOMPO"

                            cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,K.Urut,KompID,BBID,M.Uk,M.BBIDInd", koneksi)

                            'Memakai Polasi di Divisi
                            'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi,D.PrsPol,M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.UkBB,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0.0 as Pol,KaliQty,SPK,'False' as stsAdd,M.stsJasa,,M.stsMentah,M.BBIDInd, 0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)
                            cmsl.TableMappings.Add("Table", "T_BOMDtl")
                            cmsl.SelectCommand.CommandTimeout = 9000
                            cmsl.Fill(DsMaster, "T_BOMDtl")

                            DsMaster.Tables("T_BOMDtl").PrimaryKey = New DataColumn() {DsMaster.Tables("T_BOMDtl").Columns("ArtCode"), DsMaster.Tables("T_BOMDtl").Columns("DivID"), DsMaster.Tables("T_BOMDtl").Columns("KompID"), DsMaster.Tables("T_BOMDtl").Columns("BBID"), DsMaster.Tables("T_BOMDtl").Columns("BBIDInd")}

                            Me.GridControl1.DataSource = DsMaster
                            Me.GridControl1.DataMember = "T_BOMDtl"
                        End If

                End Select

                HitKeb()


                If DsMaster.Tables("T_BOMDtl").Rows.Count - 1 <= 0 Then
                    XtraMessageBox.Show("Data Model Dan PO Tidak Cocok Silakan Cek Kembali Data Anda", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                'AddHandler GridView1.FocusedRowChanged, AddressOf GridView1_FocusedRowChanged

            Catch ex As Exception
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub BEdDivID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdDivID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdKompID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdKompID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdBBID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BEdBBID.KeyPress
        e.Handled = True
    End Sub

    Private Sub BEdDivID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdDivID.ButtonClick
        Dim frm As New FSearch("Divisi", 1, "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Divisi", dataTrans.Item("Nama").ToString)
                'Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "PrsPol", CDec(dataTrans.Item("PrsPol").ToString))

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdKompID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdKompID.ButtonClick
        Dim frm As New FSearch("Komponen", "", "", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Komponen", dataTrans.Item("Nama").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BEdBBID_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BEdBBID.ButtonClick
        Dim frm As New FSearch("M_BB", "", "Bahan", "", Date.Now, "")
        frm.ShowDialog()

        Try
            If Not IsDBNull(dataTrans.Item("Kode").ToString) Then
                GridView1.ActiveEditor.EditValue = dataTrans.Item("Kode").ToString
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Bahan", dataTrans.Item("Nama").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "UkBB", dataTrans.Item("Uk").ToString)
                Me.GridView1.SetRowCellValue(Me.GridView1.FocusedRowHandle, "Sat", dataTrans.Item("Sat").ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles GridView1.InitNewRow
        Me.GridView1.SetRowCellValue(e.RowHandle, "BOMIDD", Me.GridView1.RowCount * -1)
        Me.GridView1.SetRowCellValue(e.RowHandle, "stsAdd", True)
        Me.GridView1.SetRowCellValue(e.RowHandle, "BOMID", Me.TBKode.EditValue)
        Me.GridView1.SetRowCellValue(e.RowHandle, "ArtCode", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Uk", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "DivID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "KompID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "BBID", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "UkBB", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Sat", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Ket", "")
        Me.GridView1.SetRowCellValue(e.RowHandle, "Std", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Qty", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "Pol", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "PO", 0)
        Me.GridView1.SetRowCellValue(e.RowHandle, "KaliQty", True)
        Me.GridView1.SetRowCellValue(e.RowHandle, "SPK", True)

        Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
        Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
    End Sub

    'Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
    '    If DsMaster.Tables("T_BOMDtl").Rows.Count - 1 >= 0 Then
    '        If Not IsDBNull(GridView1.GetRowCellValue(e.FocusedRowHandle, "BOMIDD")) Then
    '            If GridView1.GetRowCellValue(e.FocusedRowHandle, "stsAdd") = False Then
    '                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("Keb").OptionsColumn.AllowEdit = False
    '                Me.GridView1.Columns("Pol").OptionsColumn.AllowEdit = False

    '            Else
    '                Me.GridView1.Columns("DivID").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("KompID").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("BBID").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("Keb").OptionsColumn.AllowEdit = True
    '                Me.GridView1.Columns("Pol").OptionsColumn.AllowEdit = True

    '            End If
    '        End If

    '        Try
    '            If GridView1.GetRowCellValue(e.FocusedRowHandle, "PO") > 0 Then
    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '            Else
    '                Me.GridControl1.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    Private Sub GridControl1_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl1.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetFocusedDataRow.Item("BOMIDD")
        End If
    End Sub

    Private Sub GridControl3_EmbeddedNavigator_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles GridControl3.EmbeddedNavigator.ButtonClick
        If (e.Button.ButtonType = NavigatorButtonType.Remove) Then

            ReDim Preserve arrPar2(arrPar2.GetUpperBound(0) + 1)
            arrPar2(arrPar2.GetUpperBound(0)) = Me.GridView3.GetFocusedDataRow.Item("BOMIDD")

            Dim x : For x = Me.GridView1.RowCount - 1 To 0 Step -1
                If Me.GridView1.GetRowCellValue(x, "ArtCode") = Me.GridView3.GetFocusedDataRow.Item("ArtCode") Then
                    ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
                    arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(x, "BOMIDD")

                    Me.GridView1.DeleteRow(x)
                End If
            Next
        End If
    End Sub
    Private Sub GridView3_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged
        If e.Column Is GridView3.Columns("Qty") Then
            If Me.CBOJenis.EditValue = "Produksi" Or Me.CBOJenis.EditValue = "Jasa Produksi" Then
                Dim Stok As Integer

                Dim command As New SqlCommand("Select (PJ.Qty*AD.Qty)-((Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "'  and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCodeInd='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "')/PJ.Isi) As Qty From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Where POID='" & Me.SLUPOID.EditValue & "' and B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "' Union All Select PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "'  and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "'", koneksi)

                'Dim command As New SqlCommand("Select (PJ.Qty*AD.Qty)-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "'  and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Where POID='" & Me.SLUPOID.EditValue & "' and B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "' Union All Select PL.Qty-(Select Isnull(Sum(Qty),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "'  and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJLkDtl PL Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "'", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") > Stok Then
                    XtraMessageBox.Show("Qty Tidak Boleh Melebihi Sisa PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView3.SetRowCellValue(e.RowHandle, "Qty", Stok)
                End If
            End If

            Me.GridView3.SetRowCellValue(e.RowHandle, "Tot", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView3.GetRowCellValue(e.RowHandle, "QtyPol"))

            'HitKeb()

        ElseIf e.Column Is GridView3.Columns("QtyPol") Then
            If Me.CBOJenis.EditValue = "Produksi" Or Me.CBOJenis.EditValue = "Jasa Produksi" Then
                Dim Stok As Integer
                Dim command As New SqlCommand("Select (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCodeInd='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Where POID='" & Me.SLUPOID.EditValue & "' and B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "' Union All Select PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "'", koneksi)

                'Dim command As New SqlCommand("Select (PJ.QtyPol*AD.Qty)-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Where POID='" & Me.SLUPOID.EditValue & "' and B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "' Union All Select PL.QtyPol-(Select Isnull(Sum(QtyPol),0) From T_BOMPO Where POID='" & Me.SLUPOID.EditValue & "' and BOMID <> '" & Me.TBKode.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "') As Qty From T_POBJLkDtl PL Where POID='" & Me.SLUPOID.EditValue & "' and ArtCode='" & Me.GridView3.GetRowCellValue(e.RowHandle, "ArtCode") & "'", koneksi)

                With koneksi
                    .Open()
                    Stok = command.ExecuteScalar()
                    .Close()
                End With

                If Me.GridView3.GetRowCellValue(e.RowHandle, "QtyPol") > Stok Then
                    XtraMessageBox.Show("Qty Polasi Tidak Boleh Melebihi Sisa Polasi PO", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Me.GridView3.SetRowCellValue(e.RowHandle, "QtyPol", Stok)
                End If
            End If

            Me.GridView3.SetRowCellValue(e.RowHandle, "Tot", Me.GridView3.GetRowCellValue(e.RowHandle, "Qty") + Me.GridView3.GetRowCellValue(e.RowHandle, "QtyPol"))

            'HitKeb()
        End If
    End Sub

    Private Sub GridView2_DoubleClick(sender As Object, e As EventArgs) Handles GridView2.DoubleClick
        Try
            Dim frm As New FBOM_d(Me.GridView2.GetFocusedDataRow.Item("BOMID"))
            frm.ShowDialog()
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
    '    Dim Hapus As Boolean

    '    Dim y : For y = Me.GridView1.RowCount - 1 To 0 Step -1
    '        If DsMaster.Tables("POBB").Rows.Count - 1 > 0 Then
    '            Dim z : For z = 0 To DsMaster.Tables("POBB").Rows.Count - 1
    '                If Me.GridView1.GetRowCellValue(y, "BBID") = DsMaster.Tables("POBB").Rows(z).Item("BBID") Then
    '                    Hapus = False
    '                    Exit For
    '                Else
    '                    Hapus = True
    '                End If
    '            Next
    '        Else
    '            Hapus = True
    '        End If


    '        If Hapus = True Then
    '            ReDim Preserve arrPar(arrPar.GetUpperBound(0) + 1)
    '            arrPar(arrPar.GetUpperBound(0)) = Me.GridView1.GetRowCellValue(y, "BOMIDD")

    '            Me.GridView1.DeleteRow(y)
    '        End If
    '    Next

    '    If Me.CBOJenis.EditValue = "Produksi" Then
    '        Dim cmsl As SqlDataAdapter

    '        cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi, M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0 as Pol,KaliQty,SPK,'False' as stsAdd,0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where Exists ((Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' and M.ArtCode=PJ.ArtCode Union All Select ArtCode From T_POBJLkDtl2 PL Where POID='" & Me.SLUPOID.EditValue & "' and M.ArtCode=PL.ArtCode) as x)) and MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)

    '        'cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi, M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,B.Uk,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0 as Pol,KaliQty,SPK,'False' as stsAdd,0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' and ArtCode In (Select ArtCode From (Select Distinct B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk As ArtCode From T_POBJJODtl PJ Inner Join M_Brg B On PJ.ArtCode=B.ArtCode Inner Join M_BrgAssDtl AD On B.AssID=AD.AssID Inner Join M_ModelDtl MD On MD.ArtCode=B.MerkID+KatID+JnsID+'-'+Urut+'-'+WrnID+'-'+AD.Uk Where POID='" & Me.SLUPOID.EditValue & "' and MdlID='" & Me.SLUMdlID.EditValue & "' Union All Select PL.ArtCode From T_POBJLkDtl2 PL Inner Join M_ModelDtl MD On MD.ArtCode=PL.ArtCode Where POID='" & Me.SLUPOID.EditValue & "') as x) and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)

    '        cmsl.TableMappings.Add("Table", "T_BOMDtl")
    '        cmsl.SelectCommand.CommandTimeout = 9000
    '        cmsl.Fill(DsMaster, "T_BOMDtl")

    '        Me.GridControl1.DataSource = DsMaster
    '        Me.GridControl1.DataMember = "T_BOMDtl"

    '        HitKeb()

    '    Else

    '        Dim cmsl As SqlDataAdapter
    '        cmsl = New SqlDataAdapter("Select ROW_NUMBER() over (ORDER BY D.Urut,K.Urut,M.BBID)*-1 as BOMIDD,ArtCode,M.Uk,M.DivID,D.Nama as Divisi, M.KompID,K.Nama As Komponen,M.BBID,B.Nama As Bahan,B.Uk,M.Sat,M.Std,M.Ket,0.0 As Qty,0.0 As Keb,0 as Pol,KaliQty,SPK,'False' as stsAdd,0 As PO From M_ModelDtl M Inner Join M_Div D On M.DivID=D.DivID Inner Join M_Komp K On M.KompID=K.KompID Inner Join M_BB B On B.BBID=M.BBID Where MdlID='" & Me.SLUMdlID.EditValue & "' and M.BBID Not In (Select BBID From T_POBBDtl Where BOMID='" & Me.TBKode.EditValue & "') Order By DivID,K.Urut,KompID,BBID,M.Uk", koneksi)
    '        cmsl.TableMappings.Add("Table", "T_BOMDtl")
    '        cmsl.SelectCommand.CommandTimeout = 9000
    '        cmsl.Fill(DsMaster, "T_BOMDtl")

    '        Me.GridControl1.DataSource = DsMaster
    '        Me.GridControl1.DataMember = "T_BOMDtl"
    '    End If

    'End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        Try
            If (e.RowHandle >= 0) Then
                If Me.GridView1.GetRowCellValue(e.RowHandle, "PO") > 0 Then
                    e.Appearance.ForeColor = Color.White
                    e.Appearance.BackColor = Color.Red
                ElseIf Me.GridView1.GetRowCellValue(e.RowHandle, "PO") <= 0 Then
                    e.Appearance.ForeColor = Nothing
                    e.Appearance.BackColor = Nothing
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BProses_Click(sender As Object, e As EventArgs) Handles BProses.Click
        HitKeb()
    End Sub

    Private Sub TBKode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBKode.KeyPress, TBUnit.KeyPress, TBSPK.KeyPress, MKet.KeyPress
        If e.KeyChar = "'" Or e.KeyChar = "\" Then
            e.Handled = True
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim view As GridView = CType(sender, GridView)

        If view.FocusedColumn.FieldName = "UkBB" Or view.FocusedColumn.FieldName = "Ket" Then
            If e.KeyChar = "'" Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        Dim grid As GridControl = CType(sender, GridControl)
        GridView1_KeyPress(grid.FocusedView, e)
    End Sub

    Private Sub BVTBOM_s_SelectedChanged(sender As Object, e As DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs) Handles BVTBOM_s.SelectedChanged

    End Sub
End Class