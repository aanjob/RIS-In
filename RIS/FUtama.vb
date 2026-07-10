Imports DevExpress.XtraEditors
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports DevExpress.XtraReports.UI

Public Class FUtama
    Dim koneksi As New SqlConnection(GlobalKoneksi)
    Dim Path As String
    Dim sts As String

    Public Sub CekHakAkses()
        Try

            Me.RPMaster.Visible = CType(TcodeCollection.Item("Master"), Boolean)
            Me.RPPembPenj.Visible = CType(TcodeCollection.Item("Pembelian Penjualan"), Boolean)
            Me.RPProdTrm.Visible = CType(TcodeCollection.Item("Produksi Penerimaan"), Boolean)
            Me.RPFinance.Visible = CType(TcodeCollection.Item("Finance"), Boolean)
            Me.RPTargetKomisi.Visible = CType(TcodeCollection.Item("Target, Komisi & Promo Tahunan"), Boolean)
            Me.RPBeaCukai.Visible = CType(TcodeCollection.Item("Bea Cukai"), Boolean)
            Me.RPLMaster.Visible = CType(TcodeCollection.Item("Laporan Master, Stock"), Boolean)
            Me.RPLTrans.Visible = CType(TcodeCollection.Item("Lap Transaksi"), Boolean)
            Me.RPLRekap.Visible = CType(TcodeCollection.Item("Lap Rekap"), Boolean)
            Me.RPLHutPiut.Visible = CType(TcodeCollection.Item("Lap Finance"), Boolean)
            Me.RPSetting.Visible = CType(TcodeCollection.Item("Setting"), Boolean)

            Me.RPGUmum.Visible = CType(TcodeCollection.Item("Master Umum"), Boolean)
            Me.RPGMBayar.Visible = CType(TcodeCollection.Item("Pembayaran"), Boolean)
            Me.RPGLain2.Visible = CType(TcodeCollection.Item("Master Lain-Lain"), Boolean)
            Me.RPGPembelian.Visible = CType(TcodeCollection.Item("Pembelian"), Boolean)
            Me.RPGPenj.Visible = CType(TcodeCollection.Item("Penjualan"), Boolean)
            Me.RPGRetur.Visible = CType(TcodeCollection.Item("Retur"), Boolean)
            Me.RPGSJ.Visible = CType(TcodeCollection.Item("Surat Jalan"), Boolean)
            Me.RPGMatProd.Visible = CType(TcodeCollection.Item("Material Produksi"), Boolean)
            Me.RPGProd.Visible = CType(TcodeCollection.Item("Produksi"), Boolean)
            Me.RPGTrm.Visible = CType(TcodeCollection.Item("Penerimaan"), Boolean)
            Me.RPGTrans.Visible = CType(TcodeCollection.Item("Transfer Stock"), Boolean)
            Me.RPGTagih.Visible = CType(TcodeCollection.Item("Penagihan"), Boolean)
            Me.RPGTBayar.Visible = CType(TcodeCollection.Item("Pembayaran Hutang Piutang"), Boolean)
            Me.RPGStock.Visible = CType(TcodeCollection.Item("Stock"), Boolean)
            Me.RPGTargetKomisi.Visible = CType(TcodeCollection.Item("Target & Komisi"), Boolean)
            Me.RPGPrmTahunan.Visible = CType(TcodeCollection.Item("Promo Tahunan"), Boolean)
            Me.RPGBCTrans.Visible = CType(TcodeCollection.Item("Trans BC"), Boolean)
            Me.RPGBCLap.Visible = CType(TcodeCollection.Item("Lap BC"), Boolean)
            Me.RPGLMaster.Visible = CType(TcodeCollection.Item("Lap Master"), Boolean)
            Me.RPGLStock.Visible = CType(TcodeCollection.Item("Lap Stock"), Boolean)
            Me.RPGLRekap.Visible = CType(TcodeCollection.Item("Lap Rekap"), Boolean)
            Me.RPGLOmset.Visible = CType(TcodeCollection.Item("Lap Omset"), Boolean)
            'Me.RPGLOutsPO.Visible = CType(TcodeCollection.Item("Lap Outstanding PO"), Boolean)
            Me.RPGLNilPers.Visible = CType(TcodeCollection.Item("Lap Nilai Persediaan"), Boolean)
            Me.RPGLPermintaanBB.Visible = CType(TcodeCollection.Item("Lap Permintaan Bahan"), Boolean)
            Me.RPGLHutPiut.Visible = CType(TcodeCollection.Item("Lap Hutang Piutang"), Boolean)
            Me.RPGHakAkses.Visible = CType(TcodeCollection.Item("Hak Akses"), Boolean)
            Me.RPGSalAw.Visible = CType(TcodeCollection.Item("Saldo Awal"), Boolean)
            Me.RPGSetLain2.Visible = CType(TcodeCollection.Item("Lain-Lain"), Boolean)

            Dim BI As DevExpress.XtraBars.BarItem
            Try
                For Each BI In Me.RibbonControl1.Items
                    Select Case Replace(BI.Hint, "&", "")
                        Case "Barang Jadi"
                            If CType(TcodeCollection.Item("Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Barang Jadi Character"
                            If CType(TcodeCollection.Item("Barang Jadi Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Barang Jadi Job Order"
                            If CType(TcodeCollection.Item("Barang Jadi Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Barang Jadi Own"
                            If CType(TcodeCollection.Item("Barang Jadi Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Barang Jadi Promosi"
                            If CType(TcodeCollection.Item("Barang Jadi Promosi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Divisi"
                            If CType(TcodeCollection.Item("Divisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Master Item"
                            If CType(TcodeCollection.Item("Master Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Item"
                            If CType(TcodeCollection.Item("Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bahan Baku"
                            If CType(TcodeCollection.Item("Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sparepart"
                            If CType(TcodeCollection.Item("Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jenis Bahan"
                            If CType(TcodeCollection.Item("Jenis Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Harga"
                            If CType(TcodeCollection.Item("Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Tabel Harga"
                            If CType(TcodeCollection.Item("Tabel Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Harga"
                            If CType(TcodeCollection.Item("Ubah Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Harga Character"
                            If CType(TcodeCollection.Item("Ubah Harga Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Harga Job Order"
                            If CType(TcodeCollection.Item("Ubah Harga Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Harga Own"
                            If CType(TcodeCollection.Item("Ubah Harga Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Harga Promosi"
                            If CType(TcodeCollection.Item("Ubah Harga Promosi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Variabel Harga"
                            If CType(TcodeCollection.Item("Variabel Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jenis Customer"
                            If CType(TcodeCollection.Item("Jenis Customer"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Komponen"
                            If CType(TcodeCollection.Item("Komponen"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lokasi"
                            If CType(TcodeCollection.Item("Lokasi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cabang"
                            If CType(TcodeCollection.Item("Cabang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Gudang"
                            If CType(TcodeCollection.Item("Gudang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Kota"
                            If CType(TcodeCollection.Item("Kota"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Propinsi"
                            If CType(TcodeCollection.Item("Propinsi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Master Person"
                            If CType(TcodeCollection.Item("Master Person"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Customer"
                            If CType(TcodeCollection.Item("Customer"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sales"
                            If CType(TcodeCollection.Item("Sales"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Supplier"
                            If CType(TcodeCollection.Item("Supplier"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Spesifikasi Barang Jadi"
                            If CType(TcodeCollection.Item("Spesifikasi Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Assortment"
                            If CType(TcodeCollection.Item("Assortment"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jenis Barang Jadi"
                            If CType(TcodeCollection.Item("Jenis Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Kategori"
                            If CType(TcodeCollection.Item("Kategori"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Merk"
                            If CType(TcodeCollection.Item("Merk"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Satuan"
                            If CType(TcodeCollection.Item("Satuan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Style"
                            If CType(TcodeCollection.Item("Style"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sub Grup"
                            If CType(TcodeCollection.Item("Sub Grup"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Warna"
                            If CType(TcodeCollection.Item("Warna"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bank"
                            If CType(TcodeCollection.Item("Bank"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bank Account"
                            If CType(TcodeCollection.Item("Bank Account"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Currency"
                            If CType(TcodeCollection.Item("Currency"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jenis Potongan"
                            If CType(TcodeCollection.Item("Jenis Potongan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jam"
                            If CType(TcodeCollection.Item("Jam"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Kategori TKL"
                            If CType(TcodeCollection.Item("Kategori TKL"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Kode Dokumen"
                            If CType(TcodeCollection.Item("Kode Dokumen"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "LOH"
                            If CType(TcodeCollection.Item("LOH"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Periode"
                            If CType(TcodeCollection.Item("Periode"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Proses Produksi"
                            If CType(TcodeCollection.Item("Proses Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Tipe Dokumen"
                            If CType(TcodeCollection.Item("Tipe Dokumen"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Collect PO New Article"
                            If CType(TcodeCollection.Item("Collect PO New Article"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Kedatangan"
                            If CType(TcodeCollection.Item("Jadwal Kedatangan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Barang Jadi"
                            If CType(TcodeCollection.Item("PO Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Character"
                            If CType(TcodeCollection.Item("PO Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Job Order"
                            If CType(TcodeCollection.Item("PO Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Own"
                            If CType(TcodeCollection.Item("PO Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Purchase Order"
                            If CType(TcodeCollection.Item("Purchase Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Bahan Baku"
                            If CType(TcodeCollection.Item("PO Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Bahan Baku Non Stock"
                            If CType(TcodeCollection.Item("PO Bahan Baku Non Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Bahan Baku Stock"
                            If CType(TcodeCollection.Item("PO Bahan Baku Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Sparepart"
                            If CType(TcodeCollection.Item("PO Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                            'Case "PO IT"
                            '    If CType(TcodeCollection.Item("PO IT"), Boolean) = True Then
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            '    Else
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            '    End If
                            'Case "PO IT Input"
                            '    If CType(TcodeCollection.Item("PO IT Input"), Boolean) = True Then
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            '    Else
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            '    End If

                        Case "PO Sparepart Non Stock"
                            If CType(TcodeCollection.Item("PO Sparepart Non Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PO Sparepart Stock"
                            If CType(TcodeCollection.Item("PO Sparepart Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Purchase Request"
                            If CType(TcodeCollection.Item("Purchase Request"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Purchase Request Tooling"
                            If CType(TcodeCollection.Item("Purchase Request Tooling"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Purchase Request Sparepart"
                            If CType(TcodeCollection.Item("Purchase Request Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PR Sparepart Non Stock"
                            If CType(TcodeCollection.Item("PR Sparepart Non Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "PR Sparepart Stock"
                            If CType(TcodeCollection.Item("PR Sparepart Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Nota Pesanan"
                            If CType(TcodeCollection.Item("Nota Pesanan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Nota Pesanan Job Order"
                            If CType(TcodeCollection.Item("Nota Pesanan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Nota Pesanan Own"
                            If CType(TcodeCollection.Item("Nota Pesanan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Barang Jadi"
                            If CType(TcodeCollection.Item("Penjualan Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Character"
                            If CType(TcodeCollection.Item("Penjualan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Job Order"
                            If CType(TcodeCollection.Item("Penjualan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Own"
                            If CType(TcodeCollection.Item("Penjualan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Item"
                            If CType(TcodeCollection.Item("Penjualan Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Bahan Baku"
                            If CType(TcodeCollection.Item("Penjualan Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Sparepart"
                            If CType(TcodeCollection.Item("Penjualan Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penjualan Lain-Lain"
                            If CType(TcodeCollection.Item("Penjualan Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sales Order"
                            If CType(TcodeCollection.Item("Sales Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sales Order Bahan Baku"
                            If CType(TcodeCollection.Item("Sales Order Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sales Order Sparepart"
                            If CType(TcodeCollection.Item("Sales Order Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Laporan Penerimaan Retur"
                            If CType(TcodeCollection.Item("Laporan Penerimaan Retur"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Laporan Penerimaan Retur Job Order"
                            If CType(TcodeCollection.Item("Laporan Penerimaan Retur Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Laporan Penerimaan Retur Own"
                            If CType(TcodeCollection.Item("Laporan Penerimaan Retur Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Item"
                            If CType(TcodeCollection.Item("Retur Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pembelian"
                            If CType(TcodeCollection.Item("Retur Pembelian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pembelian Bahan Baku"
                            If CType(TcodeCollection.Item("Retur Pembelian Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pembelian Sparepart"
                            If CType(TcodeCollection.Item("Retur Pembelian Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan"
                            If CType(TcodeCollection.Item("Retur Penjualan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Bahan Baku"
                            If CType(TcodeCollection.Item("Retur Penjualan Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Sparepart"
                            If CType(TcodeCollection.Item("Retur Penjualan Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Barang Jadi"
                            If CType(TcodeCollection.Item("Retur Penjualan Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Character"
                            If CType(TcodeCollection.Item("Retur Penjualan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Job Order"
                            If CType(TcodeCollection.Item("Retur Penjualan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Own"
                            If CType(TcodeCollection.Item("Retur Penjualan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Penjualan Lain-Lain"
                            If CType(TcodeCollection.Item("Retur Penjualan Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Item"
                            If CType(TcodeCollection.Item("Surat Jalan Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Keluar"
                            If CType(TcodeCollection.Item("Surat Jalan Keluar"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Keluar Bahan Baku"
                            If CType(TcodeCollection.Item("Surat Jalan Keluar Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Keluar Sparepart"
                            If CType(TcodeCollection.Item("Surat Jalan Keluar Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Masuk"
                            If CType(TcodeCollection.Item("Surat Jalan Masuk"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Masuk Bahan Baku"
                            If CType(TcodeCollection.Item("Surat Jalan Masuk Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Masuk Sparepart"
                            If CType(TcodeCollection.Item("Surat Jalan Masuk Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Barang Jadi"
                            If CType(TcodeCollection.Item("Surat Jalan Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Barang Jadi Job Order"
                            If CType(TcodeCollection.Item("Surat Jalan Barang Jadi Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Surat Jalan Barang Jadi Own"
                            If CType(TcodeCollection.Item("Surat Jalan Barang Jadi Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If
                        Case "Surat Jalan Barang Jadi Lain-lain"
                            If CType(TcodeCollection.Item("Surat Jalan Barang Jadi Lain-lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Materials Specification"
                            If CType(TcodeCollection.Item("Materials Specification"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Model"
                            If CType(TcodeCollection.Item("Model"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sample Request"
                            If CType(TcodeCollection.Item("Sample Request"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bill Of Materials"
                            If CType(TcodeCollection.Item("Bill Of Materials"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BOM Asli"
                            If CType(TcodeCollection.Item("BOM Asli"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BOM Tambahan"
                            If CType(TcodeCollection.Item("BOM Tambahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Memo"
                            If CType(TcodeCollection.Item("Memo"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BSTB Produksi"
                            If CType(TcodeCollection.Item("BSTB Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BSTB Character"
                            If CType(TcodeCollection.Item("BSTB Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BSTB Job Order"
                            If CType(TcodeCollection.Item("BSTB Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "BSTB Own"
                            If CType(TcodeCollection.Item("BSTB Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bukti Pemakaian"
                            If CType(TcodeCollection.Item("Bukti Pemakaian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bukti Pemakaian Bahan Baku"
                            If CType(TcodeCollection.Item("Bukti Pemakaian Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Bukti Pemakaian Sparepart"
                            If CType(TcodeCollection.Item("Bukti Pemakaian Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hasil Produksi"
                            If CType(TcodeCollection.Item("Hasil Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Display Hasil Produksi"
                            If CType(TcodeCollection.Item("Display Hasil Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Input Hasil Produksi"
                            If CType(TcodeCollection.Item("Display Hasil Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hasil Produksi Per Jam"
                            If CType(TcodeCollection.Item("Hasil Produksi Per Jam"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Proses Kerja"
                            If CType(TcodeCollection.Item("Proses Kerja"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hasil Produksi Target"
                            If CType(TcodeCollection.Item("Hasil Produksi Target"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hasil Produksi TKL"
                            If CType(TcodeCollection.Item("Hasil Produksi TKL"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Rencana Produksi"
                            If CType(TcodeCollection.Item("Rencana Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Kedatangan Bahan"
                            If CType(TcodeCollection.Item("Jadwal Kedatangan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian"
                            If CType(TcodeCollection.Item("Jadwal Pembelian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian Accessories"
                            If CType(TcodeCollection.Item("Jadwal Pembelian Accessories"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian Bottom"
                            If CType(TcodeCollection.Item("Jadwal Pembelian Bottom"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian Finishing"
                            If CType(TcodeCollection.Item("Jadwal Pembelian Finishing"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian Leather"
                            If CType(TcodeCollection.Item("Jadwal Pembelian Leather"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Pembelian Synthetic"
                            If CType(TcodeCollection.Item("Jadwal Pembelian Synthetic"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal PPIC"
                            If CType(TcodeCollection.Item("Jadwal PPIC"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Produksi"
                            If CType(TcodeCollection.Item("Jadwal Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Sample"
                            If CType(TcodeCollection.Item("Jadwal Sample"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Jadwal Tooling"
                            If CType(TcodeCollection.Item("Jadwal Tooling"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request"
                            If CType(TcodeCollection.Item("Request"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request Produksi"
                            If CType(TcodeCollection.Item("Request Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request Teknisi"
                            If CType(TcodeCollection.Item("Request Teknisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pemakaian"
                            If CType(TcodeCollection.Item("Retur Pemakaian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pemakaian Bahan Baku"
                            If CType(TcodeCollection.Item("Retur Pemakaian Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Pemakaian Sparepart"
                            If CType(TcodeCollection.Item("Retur Pemakaian Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                            'Case "Trial-Memo"
                            '    If CType(TcodeCollection.Item("Trial-Memo"), Boolean) = True Then
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            '    Else
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            '    End If
                            'Case "Trial Grading"
                            '    If CType(TcodeCollection.Item("Trial Grading"), Boolean) = True Then
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            '    Else
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            '    End If

                            'Case "Trial Pisau"
                            '    If CType(TcodeCollection.Item("Trial Pisau"), Boolean) = True Then
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            '    Else
                            '        BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            '    End If

                        Case "Penerimaan Item"
                            If CType(TcodeCollection.Item("Penerimaan Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Bahan Baku"
                            If CType(TcodeCollection.Item("Penerimaan Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Bahan Baku Non Stock"
                            If CType(TcodeCollection.Item("Penerimaan Bahan Baku Non Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Bahan Baku Stock"
                            If CType(TcodeCollection.Item("Penerimaan Bahan Baku Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Sparepart"
                            If CType(TcodeCollection.Item("Penerimaan Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Sparepart Stock"
                            If CType(TcodeCollection.Item("Penerimaan Sparepart Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penerimaan Sparepart Non Stock"
                            If CType(TcodeCollection.Item("Penerimaan Sparepart Non Stock"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Terima Barang Jadi"
                            If CType(TcodeCollection.Item("Terima Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Terima Barang Character"
                            If CType(TcodeCollection.Item("Terima Barang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Terima Barang Job Order"
                            If CType(TcodeCollection.Item("Terima Barang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Terima Barang Own"
                            If CType(TcodeCollection.Item("Terima Barang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Terima Sample"
                            If CType(TcodeCollection.Item("Terima Sample"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Convert"
                            If CType(TcodeCollection.Item("Convert"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Convert Character"
                            If CType(TcodeCollection.Item("Convert Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Convert Job Order"
                            If CType(TcodeCollection.Item("Convert Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Convert Own"
                            If CType(TcodeCollection.Item("Convert Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DO (TAG) Barang Jadi"
                            If CType(TcodeCollection.Item("DO (TAG) Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DO (TAG) Character"
                            If CType(TcodeCollection.Item("DO (TAG) Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DO (TAG) Job Order"
                            If CType(TcodeCollection.Item("DO (TAG) Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DO (TAG) Own"
                            If CType(TcodeCollection.Item("DO (TAG) Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request Cabang"
                            If CType(TcodeCollection.Item("Request Cabang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request Cabang Job Order"
                            If CType(TcodeCollection.Item("Request Cabang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Request Cabang Own"
                            If CType(TcodeCollection.Item("Request Cabang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Article"
                            If CType(TcodeCollection.Item("Transfer Article"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Article Character"
                            If CType(TcodeCollection.Item("Transfer Article Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Article Job Order"
                            If CType(TcodeCollection.Item("Transfer Article Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Article Own"
                            If CType(TcodeCollection.Item("Transfer Article Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Antar Gudang"
                            If CType(TcodeCollection.Item("Transfer Antar Gudang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Antar Gudang Bahan"
                            If CType(TcodeCollection.Item("Transfer Antar Gudang Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Antar Gudang Sparepart"
                            If CType(TcodeCollection.Item("Transfer Antar Gudang Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Item"
                            If CType(TcodeCollection.Item("Transfer Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Antar Bahan"
                            If CType(TcodeCollection.Item("Transfer Antar Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Antar Sparepart"
                            If CType(TcodeCollection.Item("Transfer Antar Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If
                        Case "DPP Item"
                            If CType(TcodeCollection.Item("DPP Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Bahan Baku"
                            If CType(TcodeCollection.Item("DPP Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Sparepart"
                            If CType(TcodeCollection.Item("DPP Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Barang Jadi"
                            If CType(TcodeCollection.Item("DPP Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Character"
                            If CType(TcodeCollection.Item("DPP Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Job Order"
                            If CType(TcodeCollection.Item("DPP Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "DPP Own"
                            If CType(TcodeCollection.Item("DPP Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Retur Tagihan Bahan Baku"
                            If CType(TcodeCollection.Item("Retur Tagihan Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Tagihan Bahan Baku"
                            If CType(TcodeCollection.Item("Tagihan Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro"
                            If CType(TcodeCollection.Item("Billyet Giro"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro Character"
                            If CType(TcodeCollection.Item("Billyet Giro Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro Job Order"
                            If CType(TcodeCollection.Item("Billyet Giro Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro Lain-Lain"
                            If CType(TcodeCollection.Item("Billyet Giro Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro Own"
                            If CType(TcodeCollection.Item("Billyet Giro Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Billyet Giro Penjualan Bahan"
                            If CType(TcodeCollection.Item("Billyet Giro Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash"
                            If CType(TcodeCollection.Item("Cash"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash Character"
                            If CType(TcodeCollection.Item("Cash Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash Job Order"
                            If CType(TcodeCollection.Item("Cash Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash Lain-Lain"
                            If CType(TcodeCollection.Item("Cash Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash Own"
                            If CType(TcodeCollection.Item("Cash Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Cash Penjualan Bahan"
                            If CType(TcodeCollection.Item("Cash Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Hutang"
                            If CType(TcodeCollection.Item("JM Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang"
                            If CType(TcodeCollection.Item("JM Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang Character"
                            If CType(TcodeCollection.Item("JM Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang Job Order"
                            If CType(TcodeCollection.Item("JM Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang Lain-Lain"
                            If CType(TcodeCollection.Item("JM Piutang Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang Own"
                            If CType(TcodeCollection.Item("JM Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "JM Piutang Penjualan Bahan"
                            If CType(TcodeCollection.Item("JM Piutang Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Metode Pembayaran"
                            If CType(TcodeCollection.Item("Metode Pembayaran"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Hutang"
                            If CType(TcodeCollection.Item("Pembayaran Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang"
                            If CType(TcodeCollection.Item("Pembayaran Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Character"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Job Order"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Lain-Lain"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Own"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Penjualan Bahan"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pembayaran Piutang Silang"
                            If CType(TcodeCollection.Item("Pembayaran Piutang Silang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Voucher"
                            If CType(TcodeCollection.Item("Voucher"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Voucher Character"
                            If CType(TcodeCollection.Item("Voucher Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Voucher Job Order"
                            If CType(TcodeCollection.Item("Voucher Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Voucher Own"
                            If CType(TcodeCollection.Item("Voucher Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Bahan Baku"
                            If CType(TcodeCollection.Item("Adjustment Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Barang Jadi"
                            If CType(TcodeCollection.Item("Adjustment Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Character"
                            If CType(TcodeCollection.Item("Adjustment Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Job Order"
                            If CType(TcodeCollection.Item("Adjustment Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Own"
                            If CType(TcodeCollection.Item("Adjustment Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Adjustment Promosi"
                            If CType(TcodeCollection.Item("Adjustment Promosi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Bahan Baku"
                            If CType(TcodeCollection.Item("Opname Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Barang Jadi"
                            If CType(TcodeCollection.Item("Opname Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Character"
                            If CType(TcodeCollection.Item("Opname Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Job Order"
                            If CType(TcodeCollection.Item("Opname Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Own"
                            If CType(TcodeCollection.Item("Opname Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Opname Promosi"
                            If CType(TcodeCollection.Item("Opname Promosi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Rekalkulasi"
                            If CType(TcodeCollection.Item("Rekalkulasi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Rekalkulasi Bahan Baku"
                            If CType(TcodeCollection.Item("Rekalkulasi Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Rekalkulasi Barang Jadi"
                            If CType(TcodeCollection.Item("Rekalkulasi Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Detail Komisi"
                            If CType(TcodeCollection.Item("Detail Komisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hitung Komisi"
                            If CType(TcodeCollection.Item("Hitung Komisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Pencapaian"
                            If CType(TcodeCollection.Item("Pencapaian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Scheme Komisi"
                            If CType(TcodeCollection.Item("Scheme Komisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Target"
                            If CType(TcodeCollection.Item("Target"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Target Cabang"
                            If CType(TcodeCollection.Item("Target Cabang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Variabel Target"
                            If CType(TcodeCollection.Item("Variabel Target"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hitung Trade Promo"
                            If CType(TcodeCollection.Item("Hitung Trade Promo"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Promo"
                            If CType(TcodeCollection.Item("Promo"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Promo Character"
                            If CType(TcodeCollection.Item("Promo Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Promo Job Order"
                            If CType(TcodeCollection.Item("Promo Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Promo Own"
                            If CType(TcodeCollection.Item("Promo Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Scheme Trade Promo"
                            If CType(TcodeCollection.Item("Scheme Trade Promo"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Penyelesaian Waste"
                            If CType(TcodeCollection.Item("Penyelesaian Waste"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Pemasukan Bahan"
                            If CType(TcodeCollection.Item("Lap BC Terima BB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Pemakaian Bahan"
                            If CType(TcodeCollection.Item("Lap BC Pakai BB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Pemasukan Hasil Produksi"
                            If CType(TcodeCollection.Item("Lap BC Terima BJ"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Pengeluaran Hasil Produksi"
                            If CType(TcodeCollection.Item("Lap BC Jual BJ"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Mutasi Bahan Baku"
                            If CType(TcodeCollection.Item("Lap BC Mutasi BB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Mutasi Hasil Produksi"
                            If CType(TcodeCollection.Item("Lap BC Mutasi BJ"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Waste"
                            If CType(TcodeCollection.Item("Lap BC Waste"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Subkontrak"
                            If CType(TcodeCollection.Item("Lap BC Subkontrak"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Barang Jadi Character"
                            If CType(TcodeCollection.Item("Lap Barang Jadi Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Barang Jadi Job Order"
                            If CType(TcodeCollection.Item("Lap Barang Jadi Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Barang Jadi Own"
                            If CType(TcodeCollection.Item("Lap Barang Jadi Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Customer"
                            If CType(TcodeCollection.Item("Lap Customer"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Customer Character"
                            If CType(TcodeCollection.Item("Lap Customer Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Customer Job Order"
                            If CType(TcodeCollection.Item("Lap Customer Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Customer Own"
                            If CType(TcodeCollection.Item("Lap Customer Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap New Customer"
                            If CType(TcodeCollection.Item("Lap New Customer"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap New Customer Character"
                            If CType(TcodeCollection.Item("Lap New Customer Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap New Customer Job Order"
                            If CType(TcodeCollection.Item("Lap New Customer Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap New Customer Own"
                            If CType(TcodeCollection.Item("Lap New Customer Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap QR Code Item"
                            If CType(TcodeCollection.Item("Lap QR Code Item"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap QR Code Bahan Baku"
                            If CType(TcodeCollection.Item("Lap QR Code Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap QR Code Sparepart"
                            If CType(TcodeCollection.Item("Lap QR Code Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Supplier"
                            If CType(TcodeCollection.Item("Lap Supplier"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Bahan"
                            If CType(TcodeCollection.Item("Lap Arus Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Arus Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Barang Character"
                            If CType(TcodeCollection.Item("Lap Arus Barang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Barang Job Order"
                            If CType(TcodeCollection.Item("Lap Arus Barang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Barang Own"
                            If CType(TcodeCollection.Item("Lap Arus Barang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Hasil Produksi"
                            If CType(TcodeCollection.Item("Lap Hasil Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Hasil Produksi Per Proses"
                            If CType(TcodeCollection.Item("Lap Hasil Produksi Per Proses"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Hasil Produksi Per BOM"
                            If CType(TcodeCollection.Item("Lap Hasil Produksi Per BOM"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Bahan"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Bahan Nominal"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Bahan Nominal"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Bahan Quantity"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Bahan Quantity"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Character"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Job Order"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Stock Own"
                            If CType(TcodeCollection.Item("Lap Kartu Stock Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Katalog Bahan"
                            If CType(TcodeCollection.Item("Lap Katalog Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Stock Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock Character"
                            If CType(TcodeCollection.Item("Lap Stock Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock Job Order"
                            If CType(TcodeCollection.Item("Lap Stock Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock Own"
                            If CType(TcodeCollection.Item("Lap Stock Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock+Harga Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Stock+Harga Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock+Harga All"
                            If CType(TcodeCollection.Item("Lap Stock+Harga All"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock+Harga Character"
                            If CType(TcodeCollection.Item("Lap Stock+Harga Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock+Harga Job Order"
                            If CType(TcodeCollection.Item("Lap Stock+Harga Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Stock+Harga Own"
                            If CType(TcodeCollection.Item("Lap Stock+Harga Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Check Clock Teknisi"
                            If CType(TcodeCollection.Item("Lap Check Clock Teknisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Check Clock Teknisi"
                            If CType(TcodeCollection.Item("Lap Detail Check Clock Teknisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Check Clock Teknisi"
                            If CType(TcodeCollection.Item("Lap Rekap Check Clock Teknisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding"
                            If CType(TcodeCollection.Item("Lap Outstanding"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Bahan"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Bahan Qty"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Bahan Qty"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Bahan Qty+Harga"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Bahan Qty+Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Character"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Job Order"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding PO Own"
                            If CType(TcodeCollection.Item("Lap Outstanding PO Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding Nota Pesanan"
                            If CType(TcodeCollection.Item("Lap Outstanding Nota Pesanan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding Sales Order"
                            If CType(TcodeCollection.Item("Lap Outstanding Sales Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Outstanding Surat Perintah Kerja"
                            If CType(TcodeCollection.Item("Lap Outstanding Surat Perintah Kerja"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Retur"
                            If CType(TcodeCollection.Item("Lap Qty Retur"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Retur Character"
                            If CType(TcodeCollection.Item("Lap Qty Retur Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Retur Job Order"
                            If CType(TcodeCollection.Item("Lap Qty Retur Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Retur Own"
                            If CType(TcodeCollection.Item("Lap Qty Retur Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap BOM"
                            If CType(TcodeCollection.Item("Lap Rekap BOM"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Customer-Mesin"
                            If CType(TcodeCollection.Item("Lap Rekap Customer-Mesin"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Customer-Mesin Bahan Baku"
                            If CType(TcodeCollection.Item("Lap Rekap Customer-Mesin Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Customer-Mesin Sparepart"
                            If CType(TcodeCollection.Item("Lap Rekap Customer-Mesin Sparepart"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Detail Pemakaian Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Detail Pemakaian Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Detail Penerimaan Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Detail Penerimaan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Import"
                            If CType(TcodeCollection.Item("Lap Rekap Import"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Keterlambatan Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Keterlambatan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Detail Penerimaan Bahan Qty"
                            If CType(TcodeCollection.Item("Lap Rekap Detail Penerimaan Bahan Qty"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Detail Penerimaan Bahan Qty+Harga"
                            If CType(TcodeCollection.Item("Lap Rekap Detail Penerimaan Bahan Qty+Harga"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap PPn PO"
                            If CType(TcodeCollection.Item("Lap Rekap PPn PO"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Pemakaian Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Pemakaian Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Retur Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Retur Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Penerimaan Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Penerimaan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Penerimaan Barang Jadi"
                            If CType(TcodeCollection.Item("Lap Rekap Penerimaan Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Penerimaan Character"
                            If CType(TcodeCollection.Item("Lap Rekap Penerimaan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Penerimaan Job Order"
                            If CType(TcodeCollection.Item("Lap Rekap Penerimaan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Penerimaan Own"
                            If CType(TcodeCollection.Item("Lap Rekap Penerimaan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Sample Request"
                            If CType(TcodeCollection.Item("Lap Rekap Sample Request"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Transaksi"
                            If CType(TcodeCollection.Item("Lap Rekap Transaksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Transaksi Bahan"
                            If CType(TcodeCollection.Item("Lap Rekap Transaksi Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Transaksi Character"
                            If CType(TcodeCollection.Item("Lap Rekap Transaksi Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Transaksi Job Order"
                            If CType(TcodeCollection.Item("Lap Rekap Transaksi Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Transaksi Own"
                            If CType(TcodeCollection.Item("Lap Rekap Transaksi Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rencana Produksi"
                            If CType(TcodeCollection.Item("Lap Rencana Produksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Trace Faktur"
                            If CType(TcodeCollection.Item("Lap Trace Faktur"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Trace Faktur Character"
                            If CType(TcodeCollection.Item("Lap Trace Faktur Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Trace Faktur Job Order"
                            If CType(TcodeCollection.Item("Lap Trace Faktur Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Trace Faktur Own"
                            If CType(TcodeCollection.Item("Lap Trace Faktur Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Pembelian"
                            If CType(TcodeCollection.Item("Lap Buku Pembelian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Penjualan"
                            If CType(TcodeCollection.Item("Lap Buku Penjualan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Penjualan Character"
                            If CType(TcodeCollection.Item("Lap Buku Penjualan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Penjualan Job Order"
                            If CType(TcodeCollection.Item("Lap Buku Penjualan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Penjualan Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Buku Penjualan Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Buku Penjualan Own"
                            If CType(TcodeCollection.Item("Lap Buku Penjualan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Bulanan"
                            If CType(TcodeCollection.Item("Lap Omset Bulanan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Bulanan Character"
                            If CType(TcodeCollection.Item("Lap Omset Bulanan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Bulanan Job Order"
                            If CType(TcodeCollection.Item("Lap Omset Bulanan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Bulanan Own"
                            If CType(TcodeCollection.Item("Lap Omset Bulanan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Bulanan Gabungan"
                            If CType(TcodeCollection.Item("Lap Omset Bulanan Gabungan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset HPP"
                            If CType(TcodeCollection.Item("Lap Omset HPP"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset HPP Job Order"
                            If CType(TcodeCollection.Item("Lap Omset HPP Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset HPP Own"
                            If CType(TcodeCollection.Item("Lap Omset HPP Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Lengkap"
                            If CType(TcodeCollection.Item("Lap Omset Lengkap"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Lengkap Character"
                            If CType(TcodeCollection.Item("Lap Omset Lengkap Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Lengkap Job Order"
                            If CType(TcodeCollection.Item("Lap Omset Lengkap Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Lengkap Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Omset Lengkap Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Lengkap Own"
                            If CType(TcodeCollection.Item("Lap Omset Lengkap Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Pundi"
                            If CType(TcodeCollection.Item("Lap Omset Pundi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Sales"
                            If CType(TcodeCollection.Item("Lap Omset Sales"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Sales Character"
                            If CType(TcodeCollection.Item("Lap Omset Sales Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Sales Job Order"
                            If CType(TcodeCollection.Item("Lap Omset Sales Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Sales Own"
                            If CType(TcodeCollection.Item("Lap Omset Sales Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Tahunan"
                            If CType(TcodeCollection.Item("Lap Omset Tahunan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Tahunan Character"
                            If CType(TcodeCollection.Item("Lap Omset Tahunan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Tahunan Job Order"
                            If CType(TcodeCollection.Item("Lap Omset Tahunan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Tahunan Own"
                            If CType(TcodeCollection.Item("Lap Omset Tahunan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Omset Tahunan Gabungan"
                            If CType(TcodeCollection.Item("Lap Omset Tahunan Gabungan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Penjualan Retur"
                            If CType(TcodeCollection.Item("Lap Penjualan Retur"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Penjualan Retur Character"
                            If CType(TcodeCollection.Item("Lap Penjualan Retur Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Penjualan Retur Job Order"
                            If CType(TcodeCollection.Item("Lap Penjualan Retur Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Penjualan Retur Own"
                            If CType(TcodeCollection.Item("Lap Penjualan Retur Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Penjualan Harian"
                            If CType(TcodeCollection.Item("Lap Penjualan Harian"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Omset"
                            If CType(TcodeCollection.Item("Lap Qty Omset"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Omset Character"
                            If CType(TcodeCollection.Item("Lap Qty Omset Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Omset Job Order"
                            If CType(TcodeCollection.Item("Lap Qty Omset Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Qty Omset Own"
                            If CType(TcodeCollection.Item("Lap Qty Omset Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rugi Laba Cabang"
                            If CType(TcodeCollection.Item("Lap Rugi Laba Cabang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Top Penjualan"
                            If CType(TcodeCollection.Item("Lap Top Penjualan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Top Penjualan Character"
                            If CType(TcodeCollection.Item("Lap Top Penjualan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Top Penjualan Job Order"
                            If CType(TcodeCollection.Item("Lap Top Penjualan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Top Penjualan Own"
                            If CType(TcodeCollection.Item("Lap Top Penjualan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Nilai Persediaan"
                            If CType(TcodeCollection.Item("Lap Nilai Persediaan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Nominal Persediaan Bahan"
                            If CType(TcodeCollection.Item("Lap Nominal Persediaan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Persediaan Bahan"
                            If CType(TcodeCollection.Item("Lap Persediaan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Prosentase Saldo Bahan"
                            If CType(TcodeCollection.Item("Lap Prosentase Saldo Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Saldo Bahan Per Bulan"
                            If CType(TcodeCollection.Item("Lap Saldo Bahan Per Bulan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap WIP"
                            If CType(TcodeCollection.Item("Lap WIP"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap BOM Terhadap BPB"
                            If CType(TcodeCollection.Item("Lap BOM Terhadap BPB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap BOM Terhadap LPB"
                            If CType(TcodeCollection.Item("Lap BOM Terhadap LPB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap BOM vs LPB vs BPB"
                            If CType(TcodeCollection.Item("Lap BOM vs LPB vs BPB"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Hutang"
                            If CType(TcodeCollection.Item("Lap Arus Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang"
                            If CType(TcodeCollection.Item("Lap Arus Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If


                        Case "Lap Arus Piutang All"
                            If CType(TcodeCollection.Item("Lap Arus Piutang All"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang Character"
                            If CType(TcodeCollection.Item("Lap Arus Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang Job Order"
                            If CType(TcodeCollection.Item("Lap Arus Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Arus Piutang Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang Own"
                            If CType(TcodeCollection.Item("Lap Arus Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Arus Piutang Penjualan Bahan"
                            If CType(TcodeCollection.Item("Lap Arus Piutang Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Character"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Hutang"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Job Order"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Own"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Detail Pembayaran Penjualan Bahan"
                            If CType(TcodeCollection.Item("Lap Detail Pembayaran Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Hutang"
                            If CType(TcodeCollection.Item("Lap Kartu Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang Character"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang Job Order"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang Own"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Kartu Piutang Penjualan Bahan"
                            If CType(TcodeCollection.Item("Lap Kartu Piutang Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap DPP Penjualan"
                            If CType(TcodeCollection.Item("Lap Rekap DPP Penjualan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Character"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Job Order"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Own"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Acc"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Acc"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Acc Character"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Acc Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Acc Job Order"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Acc Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap Piutang Acc Own"
                            If CType(TcodeCollection.Item("Lap Rekap Piutang Acc Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap PPn Penjualan"
                            If CType(TcodeCollection.Item("Lap Rekap PPn Penjualan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap PPn Penjualan Character"
                            If CType(TcodeCollection.Item("Lap Rekap PPn Penjualan Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap PPn Penjualan Job Order"
                            If CType(TcodeCollection.Item("Lap Rekap PPn Penjualan Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Rekap PPn Penjualan Own"
                            If CType(TcodeCollection.Item("Lap Rekap PPn Penjualan Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Serah Terima DPP"
                            If CType(TcodeCollection.Item("Lap Serah Terima DPP"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Serah Terima DPP Character"
                            If CType(TcodeCollection.Item("Lap Serah Terima DPP Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Serah Terima DPP Job Order"
                            If CType(TcodeCollection.Item("Lap Serah Terima DPP Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Serah Terima DPP Own"
                            If CType(TcodeCollection.Item("Lap Serah Terima DPP Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Sisa Piutang"
                            If CType(TcodeCollection.Item("Lap Sisa Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Sisa Piutang Character"
                            If CType(TcodeCollection.Item("Lap Sisa Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Sisa Piutang Job Order"
                            If CType(TcodeCollection.Item("Lap Sisa Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Sisa Piutang Own"
                            If CType(TcodeCollection.Item("Lap Sisa Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Hutang"
                            If CType(TcodeCollection.Item("Lap Umur Hutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang"
                            If CType(TcodeCollection.Item("Lap Umur Piutang"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang All"
                            If CType(TcodeCollection.Item("Lap Umur Piutang All"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang Character"
                            If CType(TcodeCollection.Item("Lap Umur Piutang Character"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang Job Order"
                            If CType(TcodeCollection.Item("Lap Umur Piutang Job Order"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang Lain-Lain"
                            If CType(TcodeCollection.Item("Lap Umur Piutang Lain-Lain"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang Own"
                            If CType(TcodeCollection.Item("Lap Umur Piutang Own"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Lap Umur Piutang Penjualan Bahan"
                            If CType(TcodeCollection.Item("Lap Umur Piutang Penjualan Bahan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Hak Akses"
                            If CType(TcodeCollection.Item("Hak Akses"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Akses"
                            If CType(TcodeCollection.Item("Akses"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Menu"
                            If CType(TcodeCollection.Item("Menu"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Posisi"
                            If CType(TcodeCollection.Item("Posisi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Users"
                            If CType(TcodeCollection.Item("Users"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Saldo Bahan Baku"
                            If CType(TcodeCollection.Item("Saldo Bahan Baku"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Saldo Barang Jadi"
                            If CType(TcodeCollection.Item("Saldo Barang Jadi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Saldo Hutang Supplier"
                            If CType(TcodeCollection.Item("Saldo Hutang Supplier"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Saldo Piutang Customer"
                            If CType(TcodeCollection.Item("Saldo Piutang Customer"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Backup"
                            If CType(TcodeCollection.Item("Backup"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Koneksi"
                            If CType(TcodeCollection.Item("Koneksi"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "List Alert"
                            If CType(TcodeCollection.Item("List Alert"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Alert BOM status PO"
                            If CType(TcodeCollection.Item("Alert BOM status PO"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Alert PO Belum Selesai Ditarik BOM"
                            If CType(TcodeCollection.Item("Alert PO Belum Selesai Ditarik BOM"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Alert SPK Dibatalkan Belum Approve Marketing"
                            If CType(TcodeCollection.Item("Alert SPK Dibatalkan Belum Approve Marketing"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Alert PO-SPK Belum Lunas"
                            If CType(TcodeCollection.Item("Alert PO-SPK Belum Lunas"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Perusahaan"
                            If CType(TcodeCollection.Item("Perusahaan"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Restore"
                            If CType(TcodeCollection.Item("Restore"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Set Periode"
                            If CType(TcodeCollection.Item("Set Periode"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Sinkronisasi Data"
                            If CType(TcodeCollection.Item("Sinkronisasi Data"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Transfer Balance"
                            If CType(TcodeCollection.Item("Transfer Balance"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If

                        Case "Ubah Password"
                            If CType(TcodeCollection.Item("Ubah Password"), Boolean) = True Then
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                            Else
                                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                            End If
                    End Select
                Next


            Catch ex As Exception
                XtraMessageBox.Show("Settingan Hak Akses Bermasalah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraMessageBox.Show(BI.Hint, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            XtraMessageBox.Show("Settingan Hak Akses Bermasalah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Tabbed Image"

    Private rnd As Random = New Random()
    Private Sub XtraTabbedMdiManager1_PageAdded(ByVal sender As Object, ByVal e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles XtraTabbedMdiManager1.PageAdded
        Me.PictureEdit1.Visible = False
    End Sub

    Private Sub XtraTabbedMdiManager1_PageRemoved(ByVal sender As Object, ByVal e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles XtraTabbedMdiManager1.PageRemoved
        If Me.MdiChildren().Length < 1 Then
            Me.PictureEdit1.Visible = True
            BSIInfo.Caption = NmApp
        End If
    End Sub

    Private Function CekMdiForm(ByVal HeaderText As String) As Boolean
        Dim ReturnValue As Boolean
        Dim x As Integer

        For x = 0 To XtraTabbedMdiManager1.Pages.Count - 1
            If String.Compare(CType(XtraTabbedMdiManager1.Pages(x), DevExpress.XtraTabbedMdi.XtraMdiTabPage).Text, HeaderText, False) = 0 Then
                XtraTabbedMdiManager1.SelectedPage = XtraTabbedMdiManager1.Pages(x)
                ReturnValue = True
                Exit For
            End If
            ReturnValue = False
        Next
        Return ReturnValue
    End Function

#End Region

#Region "Volume"
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319

    <DllImport("user32.dll")> _
    Public Shared Function SendMessageW(ByVal hWnd As IntPtr, _
                ByVal Msg As Integer, ByVal wParam As IntPtr, _
                ByVal lParam As IntPtr) As IntPtr
    End Function
#End Region

    Private Sub AlertBlmLns()
        If stsAlertBlmLns = True Then
            CekAlert = True
            DsLap = New System.Data.DataSet

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select * From (Select 'SPK' As Doc,* From(Select Cust,BOMID,POID,Unit,Tanggal,ArtName,Sum(Qty) As Qty,TglTrm, DATEDIFF(dd,TglTrm,GetDate()) As Hari,Sum(Sisa) As Sisa,Grup From(Select C.Nama As Cust,P.BOMID,H.POID,Unit,Tanggal,P.ArtCodeInd, B.ArtName,Sum(Qty+QtyPol) As Qty,Sum(Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang-LunasMan)-(Select Isnull((Select Sum(Psg) From T_BSTBDtl Where BOMID=P.BOMID and ArtCode=P.ArtCodeInd),0)) As Sisa,(Select Max (Tanggal) From T_BSTB H1 Inner Join T_BSTBDtl D1 On H1.BSTBID=D1.BSTBID Where BOMID=P.BOMID) As TglTrm,H.Grup From T_BOM H Inner Join M_Cust C On H.CustID=C.CustID Inner Join T_BOMPO P On H.BOMID=P.BOMID Inner Join M_Brg B On P.ArtCodeInd=B.ArtCode Where Qty+QtyPol-P.BtlOrder-P.Upp-P.Hancur-P.Hilang > 0 Group By C.Nama, P.BOMID,H.POID,Unit,Tanggal,P.ArtCodeInd,B.ArtName,H.Grup) as x Group By Cust,BOMID, POID,Unit, Tanggal,ArtName,TglTrm,Grup Having Sum(Sisa)>0) as y Where Sisa <= ((Qty * 5) / 100) Or DateDiff(DD, Tanggal,GetDate()) > 90 Union All Select 'PO' As Doc,C.Nama As Cust,'' As BOMID,H.POID,'' As Unit,Tanggal,ArtName,Sum(Qty) As Qty,null As TglTrm,DATEDIFF(DD,Tanggal,GetDate()) As Hari,Sum(Qty) As Sisa,H.Grup From T_POBJJO H Inner Join T_POBJJODtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Brg B On D.ArtCode = B.ArtCode Where D.POID+D.ArtCode Not In (Select POID+ArtCodeInd From T_BOMPO) and D.stsProd=0 and DATEDIFF(DD,Tanggal,GetDate())>30 	Group By C.Nama,H.POID,Tanggal,ArtName,H.Grup Union All Select 'PO' As Doc,C.Nama As Cust,'' As BOMID,H.POID,'' As Unit,Tanggal,ArtName, Sum(Qty) As Qty,null As TglTrm, DATEDIFF(DD,Tanggal,GetDate()) As Hari,Sum(Qty) As Sisa,H.Grup From T_POBJLk H Inner Join T_POBJLkDtl D On H.POID=D.POID Inner Join M_Cust C On H.CustID=C.CustID Inner Join M_Brg B On D.ArtCode = B.ArtCode Where D.POID+D.ArtCode Not In (Select POID+ArtCodeInd From T_BOMPO) and D.stsLunas=0 and DATEDIFF(DD,Tanggal,GetDate())>30 Group By C.Nama,H.POID,Tanggal,ArtName, H.Grup) as z Where Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ") Order By Cust", koneksi)
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.TableMappings.Add("Table", "BOMBlmLns")

            Try
                DsLap.Tables("BOMBlmLns").Clear()
            Catch ex As Exception

            End Try

            cmsl.Fill(DsLap, "BOMBlmLns")

            If DsLap.Tables("BOMBlmLns").Rows.Count > 0 Then
                Dim frm2 As New FListPOSPKBlmLns()
                frm2.ShowDialog()
                frm2.Close()
            End If
        End If

        If stsAlertBOM = True Then
            CekAlert = True
            DsLap = New System.Data.DataSet

            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select convert(bit,'FALSE') as stsAppMkt,BOMID,POID,ArtName,Warna,C.Nama As Cust,KetLain2,stsBatal,stsBtlProd,stsLunasMan From T_BOM B Inner Join M_Cust C On B.CustID=C.CustID Where (stsbtlProd='True' or stsBatal='True' or stsLunasMan='True') and stsAppMkt='False' and B.Grup In (Select Grup From M_UsGrup Where UserID=" & MainModule.UserAktif & ")", koneksi)
            cmsl.SelectCommand.CommandTimeout = 90000
            cmsl.TableMappings.Add("Table", "BOMBlmApp")

            Try
                DsLap.Tables("BOMBlmApp").Clear()
            Catch ex As Exception

            End Try

            cmsl.Fill(DsLap, "BOMBlmApp")

            If DsLap.Tables("BOMBlmApp").Rows.Count > 0 Then
                Dim frm2 As New FBOM_app()
                frm2.ShowDialog()
                frm2.Close()
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.BSITanggal.Caption = Format(System.DateTime.Now, "dd MMMM yyyy hh:mm:ss")
    End Sub

    Dim SW As Stopwatch
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim Text As String = ""
        Dim Tick As TimeSpan = SW.Elapsed

        If Tick.Minutes Mod 5 = 0 Then
            If stsAlertJual = True Then
                Dim stsBunyi As Boolean = False

                If SlPrintFt() > 0 Then
                    Text &= "Ada " & SlPrintFt() & " Data Penjualan yang Belum Diprint" & vbCrLf
                    stsBunyi = True
                End If

                If SlPrintDO() > 0 Then
                    Text &= "Ada " & SlPrintDO() & " Data DO yang Belum Diprint" & vbCrLf
                    stsBunyi = True
                End If

                If SlPrintRtr() > 0 Then
                    Text &= "Ada " & SlPrintRtr() & " Data Retur yang Belum Diprint"
                    stsBunyi = True
                End If

                If stsBunyi = True Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("" & Format(System.DateTime.Now, "dd MMMM yyyy hh:mm:ss") & "" & vbCrLf & "" & Text & "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

            If stsAlertPass = True Then
                If PassAktif = LoginAktif Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("Password WAJIB Diganti !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If
        End If

        Dim Text2 As String = ""
        Dim Text3 As String = ""

        If Tick.Minutes Mod 15 = 0 Then
            If stsAlertApp = True Then

                Dim stsBunyi As Boolean = False

                If SlCekSisaNP() > 0 Then
                    Text2 &= "Ada " & SlCekSisaNP() & " Data Nota Pesanan yang Belum Selesai Dikirim. Cek Outstanding Nota Pesanan" & vbCrLf
                    stsBunyi = True
                End If

                If SlCekAppNP() > 0 Then
                    Text2 &= "Ada " & SlCekAppNP() & " Data Nota Pesanan yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If

                If SlCekAppLPR() > 0 Then
                    Text2 &= "Ada " & SlCekAppLPR() & " Data LPR yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If

                If SlCekAppSJ() > 0 Then
                    Text2 &= "Ada " & SlCekAppSJ() & " Data Surat Jalan yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If

                If stsBunyi = True Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("" & Format(System.DateTime.Now, "dd MMMM yyyy hh:mm:ss") & "" & vbCrLf & "" & Text2 & "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

            If stsAlertReqC = True Then
                Dim stsBunyi As Boolean = False

                If SlCekAppReqC() > 0 Then
                    Text3 &= "Ada " & SlCekAppReqC() & " Data Request Cabang yang Belum Diapprove"
                    stsBunyi = True
                End If

                If stsBunyi = True Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("" & Format(System.DateTime.Now, "dd MMMM yyyy hh:mm:ss") & "" & vbCrLf & "" & Text3 & "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

            If stsAlertBSTB = True Then
                Dim stsBunyi As Boolean = False

                If SlCekBSTB() > 0 Then
                    Text &= "Ada " & SlCekBSTB() & " Data BSTB Barang Jadi yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If

                If SlCekRPB() > 0 Then
                    Text &= "Ada " & SlCekRPB() & " Data Retur Dari Produksi (RPB) yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If


                If SlCekReqP() > 0 Then
                    Text &= "Ada " & SlCekReqP() & " Data Request Produksi yang Belum Diapprove" & vbCrLf
                    stsBunyi = True
                End If

                If stsBunyi = True Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("" & Format(System.DateTime.Now, "dd MMMM yyyy hh:mm:ss") & "" & vbCrLf & "" & Text & "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If

            If stsAlertSampR = True Then
                Dim stsBunyi As Boolean = False

                'If SlCekSampR() > 0 Then
                '    Dim x : For x = 0 To 25
                '        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                '    Next

                '    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                '    Dim frm As New FListSampR()
                '    frm.ShowDialog()
                '    frm.Close()
                'End If

                If SlCekBBBlmTahu() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    Dim frm As New FListBBBlmTahu()
                    frm.ShowDialog()
                    frm.Close()
                End If

            End If

        End If

        If Tick.Minutes Mod 30 = 0 Then
            If stsAlertBOM = True Then
                Dim stsBunyi As Boolean = False

                If SlCekAppBOM() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    Dim frm As New FBOM_app()
                    frm.ShowDialog()
                    frm.Close()
                End If

            End If

            If stsAlertSpec = True Then
                Dim stsBunyi As Boolean = False

                If SlCekSpecMdl() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    Dim frm As New FListSpec()
                    frm.ShowDialog()
                    frm.Close()
                End If
            End If

            If stsAlertSJKBB = True Then
                Dim stsBunyi As Boolean = False

                If SlCekSJKBB() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)


                    XtraMessageBox.Show("Ada " & SlCekSJKBB() & " Data SJK Bahan yang Belum DiApprove", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If

        If Tick.Minutes Mod 60 = 0 Then
            If stsAlert = True Then
                Dim stsBunyi As Boolean = False

                'If SlCekSchProd() > 0 Then
                '    Dim x : For x = 0 To 25
                '        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                '    Next

                '    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                '    XtraMessageBox.Show("Ada Data Rencana Produksi/Pembelian yang Belum Diisi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If

                If SlLPBLate() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    Dim frm As New FKetLate()
                    frm.ShowDialog()
                    frm.Close()
                End If

            End If

            If stsAlertPO = True Then
                Dim stsBunyi As Boolean = False

                If SlCekPOBlmBOM() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    Dim frm As New FListPO()
                    frm.ShowDialog()
                    frm.Close()

                    'XtraMessageBox.Show("Ada " & SlCekPOBlmBOM() & " Data PO yang Baru Turun Dan Belum Selesai Ditarik BOM", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

            If stsAlertTrmPO = True Then

                If SlCekTrmPO() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("Ada " & SlCekTrmPO() & " Data PO yang Belum Diterima Accounting", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            End If

            If stsAlertBayar = True Then
                If SlCekstsRtr() > 0 Then
                    Dim x : For x = 0 To 25
                        SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                    Next

                    My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                    XtraMessageBox.Show("Ada " & SlCekstsRtr() & " Data Retur Penjualan yang Belum Dipotongkan Di Pembayaran", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If

        If Tick.Minutes Mod 200 = 0 Then
            If stsAlertBOMstsPO = True Then
                Dim x : For x = 0 To 25
                    SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
                Next

                My.Computer.Audio.Play(My.Resources.Alarm, AudioPlayMode.Background)

                Dim frm As New FBOMstsPO()
                frm.ShowDialog()
                frm.Close()
            End If
        End If

    End Sub

    Private Sub FUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Enabled = True
        Timer2.Start()

        SW = Stopwatch.StartNew
        SW.Start()

        DevExpress.UserSkins.OfficeSkins.Register()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()

        Application.EnableVisualStyles()

       Try
            bacaSettingKoneksi()
            koneksi.ConnectionString = GlobalKoneksi

            koneksi.Open()
            koneksi.Close()

            'koneksi.ConnectionString = GlobalKoneksi.Replace("Connect Timeout=10", "Connect Timeout=10")

        Catch ex As Exception
            'If ex.Message.Contains("A network-related or instance-specific error occurred while establishing a connection to SQL Server.") Then

            Try
                bacaSettingKoneksiDdns()

                koneksi.ConnectionString = GlobalKoneksi

                koneksi.Open()
                koneksi.Close()

                koneksi.ConnectionString = GlobalKoneksi.Replace("Connect Timeout=10", "Connect Timeout=70000")
            Catch exx As Exception

                Try
                    bacaSettingKoneksiP()

                    koneksi.ConnectionString = GlobalKoneksi

                    koneksi.Open()
                    koneksi.Close()

                    koneksi.ConnectionString = GlobalKoneksi.Replace("Connect Timeout=10", "Connect Timeout=70000")

                Catch eex As Exception
                    XtraMessageBox.Show("Gagal Terhubung ke Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraMessageBox.Show(eex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                    Me.Dispose()
                End Try

            End Try

            'End If
        End Try

        Try
            bacaSettingSinkron()

            Me.BSIVersi.Caption = MainModule.Version
            Me.BSIServer.Caption = MainModule.namaServer.Substring(MainModule.namaServer.Length - 4, 4)
            Me.BSIPort.Caption = MainModule.port
            Me.BSIDB.Caption = MainModule.namaDB

            Dim BI As DevExpress.XtraBars.BarItem
            For Each BI In Me.RibbonControl1.Items
                BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            Next

            Me.RPMaster.Visible = False
            Me.RPPembPenj.Visible = False
            Me.RPProdTrm.Visible = False
            Me.RPFinance.Visible = False
            Me.RPTargetKomisi.Visible = False
            Me.RPBeaCukai.Visible = False
            Me.RPLMaster.Visible = False
            Me.RPLRekap.Visible = False
            Me.RPLTrans.Visible = False
            Me.RPLHutPiut.Visible = False

            Me.RPGUmum.Visible = False
            Me.RPGMBayar.Visible = False
            Me.RPGLain2.Visible = False
            Me.RPGPembelian.Visible = False
            Me.RPGPenj.Visible = False
            Me.RPGRetur.Visible = False
            Me.RPGSJ.Visible = False
            Me.RPGMatProd.Visible = False
            Me.RPGProd.Visible = False
            Me.RPGTrm.Visible = False
            Me.RPGTrans.Visible = False
            Me.RPGTagih.Visible = False
            Me.RPGTBayar.Visible = False
            Me.RPGStock.Visible = False
            Me.RPGTargetKomisi.Visible = False
            Me.RPGPrmTahunan.Visible = False
            Me.RPGBCTrans.Visible = False
            Me.RPGBCLap.Visible = False
            Me.RPGLMaster.Visible = False
            Me.RPGLStock.Visible = False
            Me.RPGLRekap.Visible = False
            Me.RPGLOmset.Visible = False
            'Me.RPGLOutsPO.Visible = False
            Me.RPGLNilPers.Visible = False
            Me.RPGLPermintaanBB.Visible = False
            Me.RPGLHutPiut.Visible = False
            Me.RPGHakAkses.Visible = False
            Me.RPGSalAw.Visible = False
            Me.RPGSetLain2.Visible = False

            Me.RPSetting.Visible = True
            Me.RPGAkses.Visible = True
            Me.BBILogin.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BBILogout.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSITanggal.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIServer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIPort.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIDB.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Me.BSIVersi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            Dim Reader As SqlClient.SqlDataReader
            Dim cmd As SqlCommand
            cmd = New SqlCommand("Select Kota,MM1,MM2,TTPOLk,BatasBB,App,PrintDate,AutoBMentah,Ongkir,PjBBID,PersenLebih,PersenPPn From M_Perusahaan Where ID='P01'")
            'cmd = New SqlCommand("Select Perusahaan,Alamat,Kota,Telp,Fax,Pemilik,NPWP,MMCr,MMJO,MMOwn,BatasBB,App,PrintDate,Ongkir,PjBBID From M_Perusahaan Where ID='P01'")
            cmd.Connection = koneksi

            With koneksi
                .Open()
                Reader = cmd.ExecuteReader()

                If Reader.HasRows Then
                    While Reader.Read
                        MainModule.Kota = Reader.Item(0)
                        MainModule.MM1 = Reader.Item(1)
                        MainModule.MM2 = Reader.Item(2)
                        MainModule.TTPOLk = Reader.Item(3)
                        MainModule.BatasBB = Reader.Item(4)
                        MainModule.NmApp = Reader.Item(5)
                        MainModule.PrintDt = Reader.Item(6)
                        MainModule.AutoBM = Reader.Item(7)
                        MainModule.OngkosKirim = Reader.Item(8)
                        MainModule.PjBBID = Reader.Item(9)
                        MainModule.PersenLbh = Reader.Item(10)
                        MainModule.PersenPPn = Reader.Item(11)

                        'MainModule.NmPerusahaan = Reader.Item(0)
                        'MainModule.Alamat = Reader.Item(1)
                        'MainModule.Kota = Reader.Item(2)
                        'MainModule.Telp = Reader.Item(3)
                        'MainModule.Fax = Reader.Item(4)
                        'MainModule.NmPemilik = Reader.Item(5)
                        'MainModule.NPWP = Reader.Item(6)
                        'MainModule.MMCr = Reader.Item(7)
                        'MainModule.MMJO = Reader.Item(8)
                        'MainModule.MMOwn = Reader.Item(9)
                        'MainModule.BatasBB = Reader.Item(10)
                        'MainModule.NmApp = Reader.Item(11)
                        'MainModule.PrintDt = Reader.Item(12)
                        'MainModule.OngkosKirim = Reader.Item(13)
                        'MainModule.PjBBID = Reader.Item(14)
                    End While
                End If

                .Close()
            End With

            bacaSettingPeriode()

            Me.Text = ".: " & NmApp & " [ " & MonthName(MainModule.periodeBulan) & " " & MainModule.periodeTahun & " ]" & " :."

        Catch ex As Exception
            XtraMessageBox.Show("Pembacaan Data Salah", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Dispose()
        End Try


        Me.BSIInfo.Caption = "Silakan Login Untuk Dapat Masuk Ke Aplikasi"
        Dim frm As New FLogin
        frm.ShowDialog()
        Me.BSIInfo.Caption = "Menu Utama"

        If MainModule.Posisi <> "" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select MenuId,Akses From M_Akses Where PosisiID ='" & Posisi & "'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Akses")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "M_Akses")

            Dim i : For i = 0 To DsMaster.Tables("M_Akses").Rows.Count - 1
                TcodeCollection.Add(DsMaster.Tables("M_Akses").Rows(i).Item("Akses"), DsMaster.Tables("M_Akses").Rows(i).Item("MenuId"))
            Next

            CekHakAkses()

            If MainModule.FormDef <> "" Then
                If MainModule.FormDef = "Hasil Produksi Per Jam" Then
                    If CType(TcodeCollection.Item("Hasil Produksi Per Jam"), Boolean) = True Then
                        Dim frmv As New FHslProd()
                        frmv.MdiParent = Me
                        frmv.Show()

                        Me.RibbonControl1.SelectedPage = Me.RPProdTrm
                    Else
                        XtraMessageBox.Show("Form Default Tidak Ada Akses", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    'ElseIf MainModule.FormDef = "Terima Barang" Then
                    '    If CType(TcodeCollection.Item("Terima Barang"), Boolean) = True Then
                    '        Dim frmv As New FTrmBJ()
                    '        frmv.MdiParent = Me
                    '        frmv.Show()

                    '        Me.RibbonControl1.SelectedPage = Me.RPProdTrm
                    '    Else
                    '        XtraMessageBox.Show("Form Default Tidak Ada Akses", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    End If
                End If
            Else
                Me.RibbonControl1.SelectedPage = Me.RPMaster

            End If
        Else
            TcodeCollection.Clear()
            Me.RibbonControl1.SelectedPage = Me.RPSetting
        End If

        Me.BSIUser.Caption = MainModule.InisialAktif

        AlertBlmLns()
    End Sub

#Region "Master"

    Private Sub BBIJenis_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJenis.ItemClick
        Dim frm As New FBBJns()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBahan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBahan.ItemClick
        Dim frm As New FBahan("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISparepart_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISparepart.ItemClick
        Dim frm As New FBahan("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIJnsPot_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJnsPot.ItemClick
        Dim frm As New FJnsPot
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgAss_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgAss.ItemClick
        Dim frm As New FBrgAss
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBJCr.ItemClick
        Dim frm As New FBJLk("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBJJO.ItemClick
        'Dim frm As New FBJJO
        Dim frm As New FBJLk("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBJOwn.ItemClick
        Dim frm As New FBJLk("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBJPromosi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBJPromosi.ItemClick
        Dim frm As New FBJLk("Promosi")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgJns_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgJns.ItemClick
        Dim frm As New FBrgJns
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgKat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgKat.ItemClick
        Dim frm As New FBrgKat
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgMerk_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgMerk.ItemClick
        Dim frm As New FBrgMerk
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgWrn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgWrn.ItemClick
        Dim frm As New FBrgWrn
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBrgSat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBrgSat.ItemClick
        Dim frm As New FBrgSat
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISubGrup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISubGrup.ItemClick
        Dim frm As New FBrgSubGrup
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBank_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBank.ItemClick
        Dim frm As New FBank
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBankAcc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBankAcc.ItemClick
        Dim frm As New FBankAcc
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICabang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICabang.ItemClick
        Dim frm As New FCabang
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICurrency_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICurrency.ItemClick
        Dim frm As New FCurr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICust_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICust.ItemClick
        Dim frm As New FCust
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDivisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDivisi.ItemClick
        Dim frm As New FDivisi()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJnsCust_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJnsCust.ItemClick
        Dim frm As New FJnsCust
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIGudang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIGudang.ItemClick
        Dim frm As New FGudang
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJam_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJam.ItemClick
        Dim frm As New FJam
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKatTKL_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKatTKL.ItemClick
        Dim frm As New FKatTKL
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKodeDoc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKodeDoc.ItemClick
        Dim frm As New FDocCode
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKomp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKomp.ItemClick
        Dim frm As New FKomponen()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKota_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKota.ItemClick
        Dim frm As New FKota
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILOH_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILOH.ItemClick
        Dim frm As New FLOH
        frm.MdiParent = Me
        frm.Show()
    End Sub
    Private Sub BBIProp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIProp.ItemClick
        Dim frm As New FProp
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITipeDoc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITipeDoc.ItemClick
        Dim frm As New FTipeDoc
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISal_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISal.ItemClick
        Dim frm As New FSales()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStyle_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStyle.ItemClick
        Dim frm As New FStyle
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISupp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISupp.ItemClick
        Dim frm As New FSupp()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIVarTarget_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIVarTarget.ItemClick
        Dim frm As New FVarTarget()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIMPeriod_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIMPeriod.ItemClick
        Dim frm As New FPeriode
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIProses_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIProses.ItemClick
        Dim frm As New FProsesProd
        frm.MdiParent = Me
        frm.Show()
    End Sub

#End Region

#Region "Transaksi BJ"

    Private Sub BBIAdjBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIAdjBJCr.ItemClick
        Dim frm As New FAdjBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIAdjBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIAdjBJJO.ItemClick
        Dim frm As New FAdjBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIAdjBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIAdjBJOwn.ItemClick
        Dim frm As New FAdjBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIAdjPromosi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIAdjPromosi.ItemClick
        Dim frm As New FAdjBJ("Promosi")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBGCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBGCr.ItemClick
        Dim frm As New FGiro("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBGJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBGJO.ItemClick
        Dim frm As New FGiro("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBGL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBGL2.ItemClick
        Dim frm As New FGiro("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBGOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBGOwn.ItemClick
        Dim frm As New FGiro("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBGPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBGPnjBB.ItemClick
        Dim frm As New FGiro("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutCr.ItemClick
        Dim frm As New FByrPiut("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutJO.ItemClick
        Dim frm As New FByrPiut("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutL2.ItemClick
        Dim frm As New FByrPiut("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutOwn.ItemClick
        Dim frm As New FByrPiut("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutPnjBB.ItemClick
        Dim frm As New FByrPiut("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIByrPiutSlg_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIByrPiutSlg.ItemClick
        Dim frm As New FByrPiutSlg()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICashCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICashCr.ItemClick
        Dim frm As New FCash("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICashJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICashJO.ItemClick
        Dim frm As New FCash("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICashL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICashL2.ItemClick
        Dim frm As New FCash("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICashOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICashOwn.ItemClick
        Dim frm As New FCash("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICashPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICashPnjBB.ItemClick
        Dim frm As New FCash("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBICollPO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBICollPO.ItemClick
        Dim frm As New FCollPO("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIConvCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIConvCr.ItemClick
        If CekMdiForm(FConvert.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FConvert("Character")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIConvJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIConvJO.ItemClick
        If CekMdiForm(FConvert.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FConvert("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIConvOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIConvOwn.ItemClick
        If CekMdiForm(FConvert.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else

            Dim frm As New FConvert("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIDPPCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDPPCr.ItemClick
        Dim frm As New FDPPBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDPPJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDPPJO.ItemClick
        Dim frm As New FDPPBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDPPOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDPPOwn.ItemClick
        Dim frm As New FDPPBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDOCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDOCr.ItemClick
        Dim frm As New FDOBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDOJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDOJO.ItemClick
        Dim frm As New FDOBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDOOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDOOwn.ItemClick
        Dim frm As New FDOBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDetKomisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDetKomisi.ItemClick
        Dim frm As New FDetKomisi()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIHitKomisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHitKomisi.ItemClick
        Dim frm As New FHitKomisi()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIHitPromo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHitPromo.ItemClick
        Dim frm As New FHitTrPromo()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJualL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJualL2.ItemClick
        Dim frm As New FFtBebas
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIFtCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIFtCr.ItemClick
        Dim frm As New FFtBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIFtJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIFtJO.ItemClick
        Dim frm As New FFtBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIFtOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIFtOwn.ItemClick
        Dim frm As New FFtBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIHProdPerJam_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHProdPerJam.ItemClick
        Dim frm As New FHslProd()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm As New FHslProdJam()
        'frm.MdiParent = Me
        'frm.Show()
    End Sub

    Private Sub BBIDisHslProd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDisHslProd.ItemClick
        Dim frm As New FHslProd_dis()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIHProdIn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHProdIn.ItemClick
        Dim frm As New FHslProd_i()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIHProdTarget_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHProdTarget.ItemClick
        Dim frm As New FTargetProd()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm As New FHslProdTarget()
        'frm.MdiParent = Me
        'frm.Show()
    End Sub

    Private Sub BBIHProdTKL_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHProdTKL.ItemClick
        Dim frm As New FHslProdTKL()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIProsKrj_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIProsKrj.ItemClick
        Dim frm As New FProsKrj()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIScKB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIScKB.ItemClick
        Dim frm As New FSchKB()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchAcc_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchAcc.ItemClick
        Dim frm As New FSchAcc()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchBott_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchBott.ItemClick
        Dim frm As New FSchBott()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchFin_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchFin.ItemClick
        Dim frm As New FSchFin()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchLeat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchLeat.ItemClick
        Dim frm As New FSchLeat()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchSint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchSint.ItemClick
        Dim frm As New FSchSint()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchPPIC_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchPPIC.ItemClick
        Dim frm As New FSchPPIC()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchProd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchProd.ItemClick
        Dim frm As New FSchProd()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchPrSamp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchPrSamp.ItemClick
        Dim frm As New FSchSamp()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISchTool_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISchTool.ItemClick
        Dim frm As New FSchTool()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKomisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPencapaian.ItemClick
        Dim frm As New FPencapaian()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILPRJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILPRJO.ItemClick
        If CekMdiForm(FLPR.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FLPR("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBILPROwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILPROwn.ItemClick
        If CekMdiForm(FLPR.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FLPR("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBINotPesJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBINotPesJO.ItemClick
        If CekMdiForm(FNotPes.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FNotPes("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBINotPesOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBINotPesOwn.ItemClick
        If CekMdiForm(FNotPes.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FNotPes("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIOpBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOpBJCr.ItemClick
        Dim jml As Integer

        If SlCek("T_AdjBJ", "Count(*)", "PeriodID", MainModule.periodAktif) = 0 Then
            Dim command As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Dim frm As New FOpBJ("Character")
                frm.MdiParent = Me
                frm.Show()
            End If

        Else
            Dim frm As New FOpBJ("Character")
            frm.MdiParent = Me
            frm.Show()
        End If

    End Sub

    Private Sub BBIOpBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOpBJJO.ItemClick
        Dim jml As Integer

        If SlCek("T_AdjBJ", "Count(*)", "PeriodID", MainModule.periodAktif) = 0 Then
            Dim command As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Dim frm As New FOpBJ("Job Order")
                frm.MdiParent = Me
                frm.Show()
            End If

        Else
            Dim frm As New FOpBJ("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIOpBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOpBJOwn.ItemClick
        Dim jml As Integer

        If SlCek("T_AdjBJ", "Count(*)", "PeriodID", MainModule.periodAktif) = 0 Then
            Dim command As New SqlCommand("Select dbo.fcCekDocBJ(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Dim frm As New FOpBJ("Own")
                frm.MdiParent = Me
                frm.Show()
            End If

        Else
            Dim frm As New FOpBJ("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIOpPromosi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOpPromosi.ItemClick
        Dim frm As New FOpBJ("Promosi")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPOCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOCr.ItemClick
        Dim frm As New FPOBJLk("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPOJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOJO.ItemClick
        Dim frm As New FPOBJJO()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPOOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOOwn.ItemClick
        Dim frm As New FPOBJLk("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPromoCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPromoCr.ItemClick
        Dim frm As New FPromo("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPromoJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPromoJO.ItemClick
        Dim frm As New FPromo("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPromoOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPromoOwn.ItemClick
        Dim frm As New FPromo("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekalBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekalBJ.ItemClick
        Dim frm As New FRekalBJ()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIReqCJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIReqCJO.ItemClick
        If CekMdiForm(FReqCab.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FReqCab("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIReqCOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIReqCOwn.ItemClick
        If CekMdiForm(FReqCab.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FReqCab("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIRtrBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrBJCr.ItemClick
        Dim frm As New FRtrBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrBJJO.ItemClick
        Dim frm As New FRtrBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrBJOwn.ItemClick
        Dim frm As New FRtrBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrJualL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrJualL2.ItemClick
        Dim frm As New FRtrPenjBebas()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIScmKomisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIScmKomisi.ItemClick
        Dim frm As New FScmKomisi()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIScmTrPromo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIScmTrPromo.ItemClick
        Dim frm As New FScmTrPromo()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJJO.ItemClick
        'If CekMdiForm(FSJBebas.Text) Then
        '    XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'Else
        '    Dim frm As New FSJBebas("Job Order")
        '    frm.MdiParent = Me
        '    frm.Show()
        'End If
        'Dim frm As New FSJBebas()
        'frm.MdiParent = Me
        'frm.Show()
        MsgBox("Masuk")
    End Sub

    Private Sub BBISJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJOwn.ItemClick
        If CekMdiForm(FSJ.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FSJ("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBITarget_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITarget.ItemClick
        Dim frm As New FTarget()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITargetCab_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITargetCab.ItemClick
        Dim frm As New FTargetCab()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITblHarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITblHarga.ItemClick
        Dim frm As New FTblHargaBrg()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrArtCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrArtCr.ItemClick
        If CekMdiForm(FTrArt.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FTrArt("Character")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBITrArtJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrArtJO.ItemClick
        If CekMdiForm(FTrArt.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FTrArt("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBITrArtOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrArtOwn.ItemClick
        If CekMdiForm(FTrArt.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FTrArt("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBITrmBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmBJCr.ItemClick
        Dim frm As New FTrmBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmBJJO.ItemClick
        Dim frm As New FTrmBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmBJOwn.ItemClick
        Dim frm As New FTrmBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmSR_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmSR.ItemClick
        Dim frm As New FTrmSR
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUbahHargaCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUbahHargaCr.ItemClick
        Dim frm As New FUbahHargaLk("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUbahHargaJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUbahHargaJO.ItemClick
        Dim frm As New FUbahHarga("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUbahHargaOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUbahHargaOwn.ItemClick
        Dim frm As New FUbahHargaLk("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUbahHargaPromosi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUbahHargaPromosi.ItemClick
        Dim frm As New FUbahHarga("Promosi")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIVarHarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIVarHarga.ItemClick
        Dim frm As New FSetVarHarga()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIVoucherCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIVoucherCr.ItemClick
        Dim frm As New FVoucher("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIVoucherJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIVoucherJO.ItemClick
        Dim frm As New FVoucher("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIVoucherOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIVoucherOwn.ItemClick
        Dim frm As New FVoucher("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

#End Region

#Region "Transaksi Produksi"

    Private Sub BBIBOMAsli_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMAsli.ItemClick
        Me.RibbonControl1.Minimized = True

        Dim frm As New FBOM()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm2 As New FBOMv1()
        'frm2.MdiParent = Me
        'frm2.Show()
    End Sub

    Private Sub BBIBOMTam_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMTam.ItemClick
        Me.RibbonControl1.Minimized = True

        Dim frm As New FBOMTam()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm2 As New FBOMTamv1()
        'frm2.MdiParent = Me
        'frm2.Show()
    End Sub

    Private Sub BBIBSTBCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBSTBCr.ItemClick
        If CekMdiForm(FBSTB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FBSTB("Character")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIBSTBJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBSTBJO.ItemClick
        If CekMdiForm(FBSTB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FBSTB("Job Order")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIBSTBOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBSTBOwn.ItemClick
        If CekMdiForm(FBSTB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FBSTB("Own")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBISpec_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISpec.ItemClick
        Dim frm As New FSpec()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIModel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIModel.ItemClick
        Dim frm As New FModel()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISampReq_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISampReq.ItemClick
        Dim frm As New FSampReq()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIMemo_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIMemo.ItemClick
        Dim frm As New FMemo()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm2 As New FMemov1()
        'frm2.MdiParent = Me
        'frm2.Show()
    End Sub

    Private Sub BBITrlGrd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrlGrd.ItemClick
        Dim frm As New FTrialGrd()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrlPs_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrlPs.ItemClick
        Dim frm As New FTrialPs()
        frm.MdiParent = Me
        frm.Show()
    End Sub
#End Region

#Region "Transaksi Bahan"

    Private Sub BBIAdjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIAdjBB.ItemClick
        If CekMdiForm(FAdjBB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FAdjBB
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIBPB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBPB.ItemClick
        Dim frm As New FBPB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBPSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBPSpM.ItemClick
        Dim frm As New FBPB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDPPBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDPPBB.ItemClick
        Dim frm As New FDPPBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIDPPSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIDPPSpM.ItemClick
        Dim frm As New FDPPBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIFtBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIFtBB.ItemClick
        Dim frm As New FFtBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIFtSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIFtSpM.ItemClick
        Dim frm As New FFtBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJadwalPO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJadwalPO.ItemClick
        Dim frm As New FScPO()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub POBBNS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOBBNS.ItemClick
        Dim frm As New FPOBB("Bahan", "Non Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub POBBStok_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOBBStock.ItemClick
        Dim frm As New FPOBB("Bahan", "Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPOSpMNS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOSpMNS.ItemClick
        Dim frm As New FPOBB("Sparepart-Mesin", "Non Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPOSpMStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOSpMStock.ItemClick
        Dim frm As New FPOBB("Sparepart-Mesin", "Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPRTool_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPRTool.ItemClick
        Dim frm As New FPRTool
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPRSpMNS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPRSpMNS.ItemClick
        Dim frm As New FPRSpM("Sparepart-Mesin", "Non Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPRSpMStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPRSpMStock.ItemClick
        Dim frm As New FPRSpM("Sparepart-Mesin", "Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIQCBahan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm As New FQCBahan()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekalBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekalBB.ItemClick
        Dim frm As New FRekalBB()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIReqProd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIReqProd.ItemClick
        If CekMdiForm(FReqProd.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FReqProd
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIReqT_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIReqT.ItemClick
        Dim frm As New FReqT
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRPB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRPB.ItemClick
        If CekMdiForm(FRPB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FRPB("Bahan")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIRPSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRPSpM.ItemClick
        If CekMdiForm(FRPB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FRPB("Sparepart-Mesin")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBIRtrBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrBB.ItemClick
        Dim frm As New FRtrBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrSpM.ItemClick
        Dim frm As New FRtrBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOpBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOpBB.ItemClick
        Dim jml As Integer

        If SlCek("T_AdjBB", "Count(*)", "PeriodID", MainModule.periodAktif) = 0 Then
            Dim command As New SqlCommand("Select dbo.fcCekDocBB(" & MainModule.periodAktif & ")", koneksi)

            With koneksi
                .Open()
                command.CommandTimeout = 9000
                jml = command.ExecuteScalar()
                .Close()
            End With

            If jml > 0 Then
                XtraMessageBox.Show("Ada Data yang Perlu Dicek Silakan Hubungi IT", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                Dim frm As New FOpBB
                frm.MdiParent = Me
                frm.Show()
            End If

        Else
            Dim frm As New FOpBB
            frm.MdiParent = Me
            frm.Show()
        End If

    End Sub

    Private Sub BBIRtrPenjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrPenjBB.ItemClick
        Dim frm As New FRtrPenjBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIRtrPenjSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrPenjSpM.ItemClick
        Dim frm As New FRtrPenjBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISJKBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJKBB.ItemClick
        If CekMdiForm(FSJKBB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FSJKBB("Bahan")
            frm.MdiParent = Me
            frm.Show()
        End If

    End Sub

    Private Sub BBISJKSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJKSpM.ItemClick
        If CekMdiForm(FSJKBB.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FSJKBB("Sparepart-Mesin")
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub

    Private Sub BBISOBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISOBB.ItemClick
        Dim frm As New FSOBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISOSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISOSpM.ItemClick
        Dim frm As New FSOBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISJMBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJMBB.ItemClick
        Dim frm As New FSJMBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBISJMSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISJMSpM.ItemClick
        Dim frm As New FSJMBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITAG_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITAGBB.ItemClick
        Dim frm As New FTAG("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBITAGSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITAGSpM.ItemClick
        Dim frm As New FTAG("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrBB.ItemClick
        Dim frm As New FTransBB("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrSpM.ItemClick
        Dim frm As New FTransBB("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmBBNS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmBBNS.ItemClick
        Dim frm As New FTrmBB("Bahan", "Non Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmBBStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmBBStock.ItemClick
        Dim frm As New FTrmBB("Bahan", "Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmSpMNS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmSpMNS.ItemClick
        Dim frm As New FTrmBB("Sparepart-Mesin", "Non Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrmSpMStock_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrmSpMStock.ItemClick
        Dim frm As New FTrmBB("Sparepart-Mesin", "Stock")
        frm.MdiParent = Me
        frm.Show()
    End Sub

#End Region

#Region "Bea Cukai"

    Private Sub BBIWaste_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIWaste.ItemClick
        Dim frm As New FWaste()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBCTrmBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCTrmBB.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        If MainModule.PilihKat = "" Then
            XtraMessageBox.Show("Kategori Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim XR As New XRBCTrmBB
        XR.InitializeData()
    End Sub

    Private Sub BBIBCPakaiBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCPakaiBB.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        If MainModule.PilihGudangID = "" Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim XR As New XRBCPakaiBB
        XR.InitializeData()
    End Sub

    Private Sub BBIBCTrmBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCTrmBJ.ItemClick
        Dim frm As New FPilihPeriode("Bea Cukai")
        frm = New FPilihPeriode("Bea Cukai")
        'frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBCTrmBJ
        XR.InitializeData()
    End Sub

    Private Sub BBIBCJualBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCJualBJ.ItemClick
        Dim frm As New FPilihPeriode("Bea Cukai")
        frm = New FPilihPeriode("Bea Cukai")
        'frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBCJualBJ
        XR.InitializeData()
    End Sub

    Private Sub BBIBCMutBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCMutBB.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        If MainModule.PilihGudangID = "" Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim XR As New XRBCMutBB
        XR.InitializeData()
    End Sub

    Private Sub BBIBCMutBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCMutBJ.ItemClick
        Dim frm As New FPilihPeriode("Bea Cukai")
        frm = New FPilihPeriode("Bea Cukai")
        frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        If MainModule.PilihGudangID = "" Then
            XtraMessageBox.Show("Gudang Harus Diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim XR As New XRBCMutBJ
        XR.InitializeData()
    End Sub

    Private Sub BBIBCWaste_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCWaste.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBCWaste
        XR.InitializeData()
    End Sub

    Private Sub BBIBCSubkontrak_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBCSubkontrak.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBCSubKontrak
        XR.InitializeData()
    End Sub

#End Region

#Region "Setting"

    Private Sub BBIBackup_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBackup.ItemClick
        Me.BSIInfo.Caption = "Backup Database"

        Dim dialog = New FolderBrowserDialog()
        dialog.SelectedPath = Application.StartupPath

        If DialogResult.OK = dialog.ShowDialog() Then
            Path = dialog.SelectedPath
        End If

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BBIHakAkses_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIHakAkses.ItemClick
        Dim frm As New FHakAkses
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKoneksi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKoneksi.ItemClick
        Me.BSIInfo.Caption = "Setting Koneksi"

        Dim frm As New FKoneksi
        frm.ShowDialog()

        Me.BSIServer.Caption = MainModule.namaServer.Substring(MainModule.namaServer.Length - 4, 4)
        Me.BSIPort.Caption = MainModule.port
        Me.BSIDB.Caption = MainModule.namaDB
    End Sub

    Private Sub BBIBOMstsPO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMstsPO.ItemClick
        Dim frm As New FBOMstsPO
        frm.MdiParent = Me
        frm.GridView1.OptionsView.ColumnAutoWidth = True
        frm.Show()
    End Sub

    Private Sub BBIPOBlmSelesaiBOM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPOBlmSelesaiBOM.ItemClick
        Dim frm As New FListPO
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISPKBlmLns_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISPKBlmLns.ItemClick
        CekAlert = False

        Dim frm As New FListPOSPKBlmLns
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISPKBtlBlmAppMkt_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISPKBtlBlmAppMkt.ItemClick
        Dim frm As New FBOM_app
        frm.MdiParent = Me
        frm.Show()

        'Dim frm2 As New FBOMBtlAw_app
        'frm2.MdiParent = Me
        'frm2.Show()
    End Sub

    Private Sub BBIMenu_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIMenu.ItemClick
        Dim frm As New FMenu
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPerusahaan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPerusahaan.ItemClick
        Dim frm As New FPerusahaan
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPosisi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPosisi.ItemClick
        Dim frm As New FPosisi
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRestore_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRestore.ItemClick
        Me.BSIInfo.Caption = "Restore Database"

        Dim frm As New FRestoreDB
        frm = New FRestoreDB
        frm.ShowDialog()
    End Sub

    Private Sub BBISalBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISalBB.ItemClick
        Dim frm As New FSaldoBB()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISalBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISalBJ.ItemClick
        Dim frm As New FSaldoBJ()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISalHutSupp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISalHutSupp.ItemClick
        Dim frm As New FSaldoHutSupp()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISalPiutCust_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISalPiutCust.ItemClick
        Dim frm As New FSaldoPiutCust()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISetPeriod_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISetPeriod.ItemClick
        Me.BSIInfo.Caption = "Setting Periode"

        Dim frm As New FSetPeriode
        frm.ShowDialog()

        Me.Text = ".: " & NmApp & " [ " & MonthName(MainModule.periodeBulan) & " " & MainModule.periodeTahun & " ]" & " :."

        'If Me.MdiChildren().Length > 0 Then
        '    Dim x : x = Me.MdiChildren().Length - 1
        '    While x >= 0
        '        Me.MdiChildren(x).Close()
        '        x -= 1
        '    End While
        'End If
    End Sub

    Private Sub BBISinkronData_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISinkronData.ItemClick
        Dim frm As New FSinkronisasi
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITransBal_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITransBal.ItemClick
        Me.BSIInfo.Caption = "Transfer Balance"

        If MainModule.SlstsPeriodNew() = True Then
            XtraMessageBox.Show("Periode Sudah Diclose", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If XtraMessageBox.Show("Apakah Anda Mau Melakukan Proses Transfer Balance dari Bulan " & MonthName(MainModule.periodeBulan) & " Tahun " & MainModule.periodeTahun & " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            BEIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            koneksi.Close()
            BackgroundWorker2.RunWorkerAsync()
        End If

        'Dim frm As New FTransBal
        'frm.MdiParent = Me
        'frm.Show()
    End Sub

    Private Sub BBIUbahPass_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUbahPass.ItemClick
        Me.BSIInfo.Caption = "Ubah Password"

        Dim frm As New FUbahPass
        frm.ShowDialog()
    End Sub

    Private Sub BBIUsers_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUsers.ItemClick
        Dim frm As New FUser
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILogin_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILogin.ItemClick
        MainModule.Posisi = ""
        TcodeCollection.Clear()

        If Me.MdiChildren().Length > 0 Then
            Dim x : x = Me.MdiChildren().Length - 1
            While x >= 0
                Me.MdiChildren(x).Close()
                x -= 1
            End While
        End If

        Dim BI As DevExpress.XtraBars.BarItem
        For Each BI In Me.RibbonControl1.Items
            BI.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Next

        Me.RPMaster.Visible = False
        Me.RPPembPenj.Visible = False
        Me.RPProdTrm.Visible = False
        Me.RPFinance.Visible = False
        Me.RPTargetKomisi.Visible = False
        Me.RPBeaCukai.Visible = False
        Me.RPLMaster.Visible = False
        Me.RPLRekap.Visible = False
        Me.RPLTrans.Visible = False
        Me.RPLHutPiut.Visible = False

        Me.RPGUmum.Visible = False
        Me.RPGMBayar.Visible = False
        Me.RPGLain2.Visible = False
        Me.RPGPembelian.Visible = False
        Me.RPGPenj.Visible = False
        Me.RPGRetur.Visible = False
        Me.RPGSJ.Visible = False
        Me.RPGMatProd.Visible = False
        Me.RPGProd.Visible = False
        Me.RPGTrm.Visible = False
        Me.RPGTrans.Visible = False
        Me.RPGTagih.Visible = False
        Me.RPGTBayar.Visible = False
        Me.RPGStock.Visible = False
        Me.RPGTargetKomisi.Visible = False
        Me.RPGPrmTahunan.Visible = False
        Me.RPGBCTrans.Visible = False
        Me.RPGBCLap.Visible = False
        Me.RPGLMaster.Visible = False
        Me.RPGLStock.Visible = False
        Me.RPGLRekap.Visible = False
        Me.RPGLOmset.Visible = False
        'Me.RPGLOutsPO.Visible = False
        Me.RPGLNilPers.Visible = False
        Me.RPGLPermintaanBB.Visible = False
        Me.RPGLHutPiut.Visible = False
        Me.RPGHakAkses.Visible = False
        Me.RPGSalAw.Visible = False
        Me.RPGSetLain2.Visible = False

        Me.RPSetting.Visible = True
        Me.RPGAkses.Visible = True
        Me.BBILogin.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BBILogout.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIInfo.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSITanggal.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIUser.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIServer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIPort.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIDB.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Me.BSIVersi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Me.BSIUser.Caption = "User"

        Dim frm As New FLogin
        frm.ShowDialog()
        Me.BSIInfo.Caption = "Menu Utama"

        If MainModule.Posisi <> "" Then
            Dim cmsl As SqlDataAdapter
            cmsl = New SqlDataAdapter("Select MenuId,Akses From M_Akses Where PosisiID ='" & Posisi & "'", koneksi)

            cmsl.TableMappings.Add("Table", "M_Akses")
            DsMaster = New System.Data.DataSet
            cmsl.Fill(DsMaster, "M_Akses")

            Dim i : For i = 0 To DsMaster.Tables("M_Akses").Rows.Count - 1
                TcodeCollection.Add(DsMaster.Tables("M_Akses").Rows(i).Item("Akses"), DsMaster.Tables("M_Akses").Rows(i).Item("MenuId"))
            Next

            CekHakAkses()

            If MainModule.FormDef <> "" Then
                If MainModule.FormDef = "Hasil Produksi Per Jam" Then
                    If CType(TcodeCollection.Item("Hasil Produksi Per Jam"), Boolean) = True Then
                        Dim frmv As New FHslProd()
                        frmv.MdiParent = Me
                        frmv.Show()

                        Me.RibbonControl1.SelectedPage = Me.RPProdTrm
                    Else
                        XtraMessageBox.Show("Form Default Tidak Ada Akses", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If

                    'ElseIf MainModule.FormDef = "Terima Barang" Then
                    '    If CType(TcodeCollection.Item("Terima Barang"), Boolean) = True Then
                    '        Dim frmv As New FTrmBJ()
                    '        frmv.MdiParent = Me
                    '        frmv.Show()

                    '        Me.RibbonControl1.SelectedPage = Me.RPProdTrm
                    '    Else
                    '        XtraMessageBox.Show("Form Default Tidak Ada Akses", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    End If
                End If
            Else
                Me.RibbonControl1.SelectedPage = Me.RPMaster

            End If

        Else
            TcodeCollection.Clear()
            Me.RibbonControl1.SelectedPage = Me.RPSetting
        End If

        If MainModule.stsAlert = True Then
            Dim x : For x = 0 To 100
                SendMessageW(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_UP))
            Next
        End If

        'SlReminder(MainModule.stsAlert)

        Me.BSIUser.Caption = MainModule.InisialAktif

        AlertBlmLns()
    End Sub

    Private Sub BBILogout_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILogout.ItemClick
        Me.Dispose()
    End Sub

#End Region

#Region "Laporan BJ"

    Private Sub BBIArusBJAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm As New FArusBJ("All")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusBJCr.ItemClick
        Dim frm As New FArusBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusBJJO.ItemClick
        Dim frm As New FArusBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusBJOwn.ItemClick
        Dim frm As New FArusBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutAll.ItemClick
        Dim frm As New FArusPiutCust("All")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutCr.ItemClick
        Dim frm As New FArusPiutCust("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutJO.ItemClick
        Dim frm As New FArusPiutCust("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutL2.ItemClick
        Dim frm As New FArusPiutCust("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutOwn.ItemClick
        Dim frm As New FArusPiutCust("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusPiutPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusPiutPnjBB.ItemClick
        Dim frm As New FArusPiutCust("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBkPenjCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBkPenjCr.ItemClick
        Dim frm As New FPilihPeriode("Character")
        frm = New FPilihPeriode("Character")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBukuPenj
        XR.InitializeData("Character")
    End Sub

    Private Sub BBIBkPenjJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBkPenjJO.ItemClick
        Dim frm As New FPilihPeriode("Job Order")
        frm = New FPilihPeriode("Job Order")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBukuPenj
        XR.InitializeData("Job Order")
    End Sub

    Private Sub BBIBkPenjL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBkPenjL2.ItemClick
        Dim frm As New FPilihPeriode("Lain-Lain")
        frm = New FPilihPeriode("Lain-Lain")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBukuPenj
        XR.InitializeData("Lain-Lain")
    End Sub

    Private Sub BBIBkPenjOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBkPenjOwn.ItemClick
        Dim frm As New FPilihPeriode("Own")
        frm = New FPilihPeriode("Own")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBukuPenj
        XR.InitializeData("Own")
    End Sub

    Private Sub BBILapBrgCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapBrgCr.ItemClick
        Dim bind As New Collection
        bind.Add("Character", "Gol")

        Dim XR As New XRLapBrg
        XR.InitializeData(bind)
    End Sub

    Private Sub BBILapBrgJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapBrgJO.ItemClick
        Dim bind As New Collection
        bind.Add("Job Order", "Gol")

        Dim XR As New XRLapBrg
        XR.InitializeData(bind)
    End Sub

    Private Sub BBILapBrgOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapBrgOwn.ItemClick
        Dim bind As New Collection
        bind.Add("Own", "Gol")

        Dim XR As New XRLapBrg
        XR.InitializeData(bind)
    End Sub

    Private Sub BBILapCustCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapCustCr.ItemClick
        Dim frm As New FPilihCabang("Character")
        frm = New FPilihCabang("Character")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add("Character", "Gol")
        bind.Add("Lokal", "Sal")

        Dim XR As New XRLapCust
        XR.InitializeData(bind)
    End Sub

    Private Sub BBILapCustJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapCustJO.ItemClick
        Dim frm As New FPilihCabang("Job Order")
        frm = New FPilihCabang("Job Order")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add("Job Order", "Gol")
        bind.Add("Job Order", "Sal")

        Dim XR As New XRLapCust
        XR.InitializeData(bind)
    End Sub

    Private Sub BBILapCustOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapCustOwn.ItemClick
        Dim frm As New FPilihCabang("Own")
        frm = New FPilihCabang("Own")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim bind As New Collection
        bind.Add("Own", "Gol")
        bind.Add("Lokal", "Sal")

        Dim XR As New XRLapCust
        XR.InitializeData(bind)
    End Sub

    Private Sub BBIKSBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKSBJCr.ItemClick
        Dim frm As New FKrtStokBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKSBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKSBJJO.ItemClick
        Dim frm As New FKrtStokBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKSBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKSBJOwn.ItemClick
        Dim frm As New FKrtStokBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBINewCustCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBINewCustCr.ItemClick
        Dim frm As New FNewCust("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBINewCustJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBINewCustJO.ItemClick
        Dim frm As New FNewCust("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBINewCustOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBINewCustOwn.ItemClick
        Dim frm As New FNewCust("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIStokCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokCr.ItemClick
        Dim frm As New FStokBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokJO.ItemClick
        Dim frm As New FStokBJJO()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokOwn.ItemClick
        Dim frm As New FStokBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokHargaAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokHargaAll.ItemClick
        Dim frm As New FStokHargaBJAll
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokHargaCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokHargaCr.ItemClick
        Dim frm As New FStokHargaBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokHargaJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokHargaJO.ItemClick
        Dim frm As New FStokHargaBJJO
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIStokHargaOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIStokHargaOwn.ItemClick
        Dim frm As New FStokHargaBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKrtPiutCustCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtPiutCustCr.ItemClick
        Dim frm As New FKrtPiutCust("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKrtPiutCustJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtPiutCustJO.ItemClick
        Dim frm As New FKrtPiutCust("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKrtPiutCustL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtPiutCustL2.ItemClick
        Dim frm As New FKrtPiutCust("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKrtPiutCustOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtPiutCustOwn.ItemClick
        Dim frm As New FKrtPiutCust("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKrtPiutCustPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtPiutCustPnjBB.ItemClick
        Dim frm As New FKrtPiutCust("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsBlCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsBlCr.ItemClick
        Dim frm As New FOmsetBln("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsBlJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsBlJO.ItemClick
        Dim frm As New FOmsetBln("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsBlOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsBlOwn.ItemClick
        Dim frm As New FOmsetBln("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsBlGab_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsBlGab.ItemClick
        Dim frm As New FOmsetBln("Gabungan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsHPPJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsHPPJO.ItemClick
        Dim frm As New FOmsetHPP("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsHPPOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsHPPOwn.ItemClick
        Dim frm As New FOmsetHPP("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsLkpCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsLkpCr.ItemClick
        Dim frm As New FOmsetLkp("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsLkpJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsLkpJO.ItemClick
        Dim frm As New FOmsetLkp("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsLkpL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsLkpL2.ItemClick
        Dim frm As New FOmsetLkp("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsLkpOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsLkpOwn.ItemClick
        Dim frm As New FOmsetLkp("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsPnd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsPnd.ItemClick
        Dim frm As New FOmsetPnd()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsQtyArtCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsQtyArtCr.ItemClick
        Dim frm As New FOmsetQtyArt("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsQtyArtJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsQtyArtJO.ItemClick
        Dim frm As New FOmsetQtyArt("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsQtyArtOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsQtyArtOwn.ItemClick
        Dim frm As New FOmsetQtyArt("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsSlCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsSlCr.ItemClick
        Dim frm As New FOmsetSal("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsSlJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsSlJO.ItemClick
        Dim frm As New FOmsetSal("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsSlOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsSlOwn.ItemClick
        Dim frm As New FOmsetSal("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsThCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsThCr.ItemClick
        Dim frm As New FOmsetThn("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsThJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsThJO.ItemClick
        Dim frm As New FOmsetThn("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsThOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsThOwn.ItemClick
        Dim frm As New FOmsetThn("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOmsThGab_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOmsThGab.ItemClick
        Dim frm As New FOmsetThn("Gabungan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJRCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJRCr.ItemClick
        Dim frm As New FPilihPeriode("Character")
        frm = New FPilihPeriode("Character")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRJualRetur
        XR.InitializeData("Character")
    End Sub

    Private Sub BBIJRJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJRJO.ItemClick
        Dim frm As New FPilihPeriode("Job Order")
        frm = New FPilihPeriode("Job Order")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRJualRetur
        XR.InitializeData("Job Order")
    End Sub

    Private Sub BBIJROwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJROwn.ItemClick
        Dim frm As New FPilihPeriode("Own")
        frm = New FPilihPeriode("Own")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRJualRetur
        XR.InitializeData("Own")
    End Sub

    Private Sub BBILapHarian_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapHarian.ItemClick
        Dim frm As New FLapJualHarian()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTransBJAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm As New FRekTransBJ("All")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTransBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTransBJCr.ItemClick
        Dim frm As New FRekTransBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrCr.ItemClick
        Dim frm As New FRekDetPiutCust("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrJO.ItemClick
        Dim frm As New FRekDetPiutCust("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrL2.ItemClick
        Dim frm As New FRekDetPiutCust("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrOwn.ItemClick
        Dim frm As New FRekDetPiutCust("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrPnjBB.ItemClick
        Dim frm As New FRekDetPiutCust("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPiutAccCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPiutAccCr.ItemClick
        Dim frm As New FPilihPeriode("Character")
        frm = New FPilihPeriode("Character")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRRekPiutAcc
        XR.InitializeData("Character")
    End Sub

    Private Sub BBIRekPiutAccJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPiutAccJO.ItemClick
        Dim frm As New FPilihPeriode("Job Order")
        frm = New FPilihPeriode("Job Order")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRRekPiutAcc
        XR.InitializeData("Job Order")
    End Sub

    Private Sub BBIRekPiutAccOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPiutAccOwn.ItemClick
        Dim frm As New FPilihPeriode("Own")
        frm = New FPilihPeriode("Own")
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRRekPiutAcc
        XR.InitializeData("Own")
    End Sub

    Private Sub BBIRekPPnPenjCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPPnPenjCr.ItemClick
        Dim frm As New FRekPPnPenj("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPPnPenjJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPPnPenjJO.ItemClick
        Dim frm As New FRekPPnPenj("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPPnPenjOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPPnPenjOwn.ItemClick
        Dim frm As New FRekPPnPenj("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekSampReq_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekSampReq.ItemClick
        Dim frm As New FRekSampReq()
        frm.MdiParent = Me
        frm.Show()

        'Dim frm As New FPilihPeriode("")
        'frm = New FPilihPeriode("")
        'frm.ShowDialog()
        'frm.Dispose()
        'frm.Close()

        'Dim XR As New XRRekSampReq
        'XR.InitializeData()
    End Sub

    Private Sub BBIRekTransBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTransBB.ItemClick
        Dim frm As New FStokvsDoc()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILapProd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapProd.ItemClick
        Dim frm As New FLapSchProd()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTransBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTransBJJO.ItemClick
        Dim frm As New FRekTransBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()

        Dim frm2 As New FLapPOBJJO()
        frm2.MdiParent = Me
        frm2.Show()
    End Sub

    Private Sub BBIRekTransBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTransBJOwn.ItemClick
        Dim frm As New FRekTransBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTrmBJCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTrmBJCr.ItemClick
        Dim frm As New FRekTrmBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTrmBJJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTrmBJJO.ItemClick
        Dim frm As New FRekTrmBJ("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTrmBJOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTrmBJOwn.ItemClick
        Dim frm As New FRekTrmBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrQtyArtCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrQtyArtCr.ItemClick
        Dim frm As New FRtrQtyArt("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrQtyArtJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrQtyArtJO.ItemClick
        Dim frm As New FRtrQtyArt("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrQtyArtOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrQtyArtOwn.ItemClick
        Dim frm As New FRtrQtyArt("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISisaPiutCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISisaPiutCr.ItemClick
        Dim frm As New FSisaPiut("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISisaPiutJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISisaPiutJO.ItemClick
        Dim frm As New FSisaPiut("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISisaPiutOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISisaPiutOwn.ItemClick
        Dim frm As New FSisaPiut("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub
    Private Sub BBIUmrPiutAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutAll.ItemClick
        Dim frm As New FUmurPiut("All")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmrPiutCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutCr.ItemClick
        Dim frm As New FUmurPiut("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmrPiutJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutJO.ItemClick
        Dim frm As New FUmurPiut("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmrPiutL2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutL2.ItemClick
        Dim frm As New FUmurPiut("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmrPiutOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutOwn.ItemClick
        Dim frm As New FUmurPiut("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmrPiutPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmrPiutPnjBB.ItemClick
        Dim frm As New FUmurPiut("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISTDPPCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISTDPPCr.ItemClick
        Dim frm As New FFilterRekDPPBJ("Serah Terima", "Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISTDPPJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISTDPPJO.ItemClick
        Dim frm As New FFilterRekDPPBJ("Serah Terima", "Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISTDPPOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISTDPPOwn.ItemClick
        Dim frm As New FFilterRekDPPBJ("Serah Terima", "Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRLCb_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRLCb.ItemClick
        Dim frm As New FRLCab()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITopJualCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITopJualCr.ItemClick
        Dim frm As New FTopJualArt("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITopJualJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITopJualJO.ItemClick
        Dim frm As New FTopJualArt("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITopJualOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITopJualOwn.ItemClick
        Dim frm As New FTopJualArt("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsPOCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsPOCr.ItemClick
        Dim frm As New FOutsPOBJ("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsPOJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsPOJO.ItemClick
        Dim frm As New FOutsPOBJJO
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsPOOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsPOOwn.ItemClick
        Dim frm As New FOutsPOBJ("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsNotPes_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsNotPes.ItemClick
        Dim frm As New FOutsNotPes("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsSO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsSO.ItemClick
        Dim frm As New FOutsSO()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsSPK_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsSPK.ItemClick
        Dim frm As New FOutsSPK()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrFtCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrFtCr.ItemClick
        Dim frm As New FTraceFt("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrFtJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrFtJO.ItemClick
        Dim frm As New FTraceFt("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITrFtOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITrFtOwn.ItemClick
        Dim frm As New FTraceFt("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

#End Region

#Region "Laporan BB"

    Private Sub BBILapQRBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapQRBB.ItemClick
        Dim frm As New FPilihBahan("Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILapQRSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapQRSpM.ItemClick
        Dim frm As New FPilihBahan("Sparepart-Mesin")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusBB.ItemClick
        Dim frm As New FArusBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIArusHut_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIArusHut.ItemClick
        Dim frm As New FArusHutSupp
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBayarHut_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBayarHut.ItemClick
        Dim frm As New FByrHut
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBkPemb_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBkPemb.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.LCIKat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRBukuPemb
        XR.InitializeData()
    End Sub

    Private Sub BBIKrtHutSupp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKrtHutSupp.ItemClick
        Dim frm As New FKrtHutSupp
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIUmurHut_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIUmurHut.ItemClick
        Dim frm As New FUmurHut
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKSBBNom_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKSBBNom.ItemClick
        Dim frm As New FKrtStokBB("Nominal")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKatalogBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKatalogBB.ItemClick
        Dim frm As New FKatalogBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILHslProdBOM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILHslProdBOM.ItemClick
        Dim frm As New FFilterHslProd("Per BOM")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILHslProdPros_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILHslProdPros.ItemClick
        Dim frm As New FFilterHslProd("Per Proses")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIKSBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIKSBB.ItemClick
        Dim frm As New FKrtStokBB("Quantity")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILapSupp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILapSupp.ItemClick
        Dim XR As New XRLapSupp
        XR.InitializeData()
    End Sub

    Private Sub BBIJMHutang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJMHutang.ItemClick
        Dim frm As New FJMHut
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJMCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJMCr.ItemClick
        Dim frm As New FJMPiut("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJMJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJMJO.ItemClick
        Dim frm As New FJMPiut("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJML2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJML2.ItemClick
        Dim frm As New FJMPiut("Lain-Lain")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJMOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJMOwn.ItemClick
        Dim frm As New FJMPiut("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIJMPnjBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIJMPnjBB.ItemClick
        Dim frm As New FJMPiut("Penjualan Bahan")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIMtdBayar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIMtdBayar.ItemClick
        Dim frm As New FMetodeByr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIOutsPO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsPO.ItemClick
        Dim frm As New FOutsPO("Qty")
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIOutsPOHarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIOutsPOHarga.ItemClick
        Dim frm As New FOutsPO("Harga")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPersBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPersBB.ItemClick
        Dim frm As New FFilterPersBB("Qty")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPersBBNom_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPersBBNom.ItemClick
        Dim frm As New FFilterPersBB("Nominal")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIPersenSAkhBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIPersenSAkhBB.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIGudang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR As New XRPersenBB
        XR.InitializeData()
    End Sub

    Private Sub BBIRekBOMAs_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekBOMAs.ItemClick
        Dim frm As New FFilterRekBOM()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekBrcdBOM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekBrcdBOM.ItemClick
        Dim frm As New FFilterRekBOM()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILDetCCTek_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILDetCCTek.ItemClick
        Dim frm As New FDetCCTek()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILRekCCTek_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILRekCCTek.ItemClick
        Dim frm As New FRekCCTek()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetByrHut_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetByrHut.ItemClick
        Dim frm As New FRekDetHutSupp()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetPakaiBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetPakaiBB.ItemClick
        Dim frm As New FRekDetBPB()
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDPPPenj_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDPPPenj.ItemClick
        Dim frm As New FFilterRekDPPBJ("Rekap", "Semua")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekByrCr_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekByrCr.ItemClick
        Dim frm As New FRekByrPiutCust("Character")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekByrJO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekByrJO.ItemClick
        Dim frm As New FRekByrPiutCust("Job Order")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekByrOwn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekByrOwn.ItemClick
        Dim frm As New FRekByrPiutCust("Own")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPakaiBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPakaiBB.ItemClick
        Dim frm As New FRekPakaiBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPOBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPOBB.ItemClick

    End Sub

    Private Sub BBIRekPOCust_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekLPBBB.ItemClick
        Dim frm As New FRekTrmCust
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekSpM_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekSpM.ItemClick
        Dim frm As New FRekSpM
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekPPnPO_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekPPnPO.ItemClick
        Dim frm As New FRekPPnPO
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekRtrBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekRtrBB.ItemClick
        Dim frm As New FRekReturBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekTrmBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekTrmBB.ItemClick
        Dim frm As New FRekTrmBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetTrmBBQty_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetTrmBBQty.ItemClick
        Dim frm As New FRekDetTrmBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRekDetTrmBBQtyHarga_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekDetTrmBBQtyHarga.ItemClick
        Dim frm As New FPilihPeriode("Bahan")
        frm = New FPilihPeriode("Bahan")
        frm.LCIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ESIPPn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim frm2 As New FRekDetTrmBBHarga
        frm2.MdiParent = Me
        frm2.Show()

        Dim XR As New XRRekLPBHarga
        XR.InitializeData()
    End Sub

    Private Sub BBIRekImp_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRekImp.ItemClick
        Dim frm As New FOutsPO("Import")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBILKetLate_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBILKetLate.ItemClick
        Dim frm As New FLapKetLate
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIRtrTagihBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIRtrTagihBB.ItemClick
        Dim frm As New FRtrTagihanBB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBITagihBB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBITagihBB.ItemClick
        Dim frm As New FTagihanBB
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBITagihBJ_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs)
        Dim frm As New FTagihanJsBJ("Barang Jadi")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBISAkhBulan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBISAkhBulan.ItemClick
        Dim frm As New FPilihBulan
        frm = New FPilihBulan
        frm.ShowDialog()
        frm.Dispose()
        frm.Close()

        Dim XR2 As New XRSalBBBulanDtl
        XR2.InitializeData()

        Dim XR As New XRSalBBBulan
        XR.InitializeData()
    End Sub

    Private Sub BBIBOMvsLPBvsBPB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMvsLPBvsBPB.ItemClick
        Dim frm As New FBOMvsLPBvsBPB5
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub BBIBOMVsBPB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMVsBPB.ItemClick
        Dim frm As New FBOMvsBPB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIBOMVsLPB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIBOMVsLPB.ItemClick
        Dim frm As New FBOMvsLPB
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BBIWIP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BBIWIP.ItemClick
        Dim frm As New FLapWIP
        frm.MdiParent = Me
        frm.Show()
    End Sub

#End Region

    Private Sub BackgroundWorker1_DoWork1(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim Disk As String = String.Format("{0}\DB[{1}].bak", Path, Format(System.DateTime.Now, "yyyyMMdd"))

        Try
            With New SqlCommand()
                .Connection = New SqlConnection(GlobalKoneksi)
                .CommandTimeout = 7000
                .CommandType = CommandType.Text
                .CommandText = String.Format("BACKUP DATABASE {0} ", New String() {MainModule.namaDB})
                .CommandText &= String.Format("TO DISK = N'{0}' ", New String() {Disk})
                .CommandText &= String.Format("WITH NOFORMAT, NOINIT, ", New String() {"Null"})
                .CommandText &= String.Format("NAME = N'{0}-Full Database Backup', ", New String() {MainModule.namaDB})
                .CommandText &= String.Format("SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ", New String() {"Null"})
                '.CommandText &= String.Format("GO", New String() {"Null"})
                Console.WriteLine(.CommandText)
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()

                sts = "Berhasil"
            End With
        Catch ex As Exception
            sts = "Gagal"
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.ToString)
            Me.DialogResult = Windows.Forms.DialogResult.None
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If sts = "Berhasil" Then
            XtraMessageBox.Show("Proses Backup Database Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Dim a As Integer = 1

    Private Sub BackgroundWorker2_DoWork1(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Dim cmSP As New SqlCommand("SPTransBalStokBB")
        cmSP.CommandType = CommandType.StoredProcedure

        With cmSP
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP.ExecuteNonQuery()
                a = cmSP.Parameters("@Return").Value
                .Close()
            End With
        End With

        If a <> 0 Then
            XtraMessageBox.Show("Trans Balance Stok Bahan Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP2 As New SqlCommand("SPTransBalStokBBBtNum")
        cmSP2.CommandType = CommandType.StoredProcedure

        With cmSP2
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP2.ExecuteNonQuery()
                a = cmSP2.Parameters("@Return").Value
                .Close()
            End With
        End With

        If a <> 0 Then
            XtraMessageBox.Show("Trans Balance Batch Number Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP3 As New SqlCommand("SPTransBalStokBJ")
        cmSP3.CommandType = CommandType.StoredProcedure

        With cmSP3
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP3.ExecuteNonQuery()
                a = cmSP3.Parameters("@Return").Value
                .Close()
            End With
        End With

        If a <> 0 Then
            XtraMessageBox.Show("Trans Balance Stok Barang Jadi Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP4 As New SqlCommand("SPTransBalHut")
        cmSP4.CommandType = CommandType.StoredProcedure

        With cmSP4
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP4.ExecuteNonQuery()
                a = cmSP4.Parameters("@Return").Value
                .Close()
            End With
        End With

        If a <> 0 Then
            XtraMessageBox.Show("Trans Balance Hutang Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cmSP5 As New SqlCommand("SPTransBalPiut")
        cmSP5.CommandType = CommandType.StoredProcedure

        With cmSP5
            .Parameters.Add("@PeriodID", SqlDbType.Int).Value = MainModule.periodAktif
            .Parameters.Add("@By", SqlDbType.VarChar).Value = MainModule.InisialAktif
            .Parameters.Add("@Return", SqlDbType.Int)
            .Parameters("@Return").Direction = ParameterDirection.Output
            .Connection = koneksi
            .CommandTimeout = 900000

            With koneksi
                .Open()
                cmSP5.ExecuteNonQuery()
                a = cmSP5.Parameters("@Return").Value
                .Close()
            End With
        End With

        If a <> 0 Then
            XtraMessageBox.Show("Trans Balance Piutang Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        BEIPB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        If a = 0 Then
            XtraMessageBox.Show("Trans Balance Berhasil", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            XtraMessageBox.Show("Trans Balance Gagal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

  
    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        Dim frm As New FPOBB("IT", "Input")
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub BarButtonItem11_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem11.ItemClick
        If CekMdiForm(FFtBebas_SJ.Text) Then
            XtraMessageBox.Show("Menu Sudah Dibuka", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Dim frm As New FFtBebas_SJ
            frm.MdiParent = Me
            frm.Show()
        End If
    End Sub
End Class