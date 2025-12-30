using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms; // Standart ComboBox için
using bursoto1.Helpers; // MessageHelper ve ImageHelper için

namespace bursoto1
{
    public partial class FrmOgrenciler : Form
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public FrmOgrenciler()
        {
            InitializeComponent();
        }

        // 1. LİSTELEME (Filtreleme desteği ile)
        public void Listele(string filtreTipi = "Tüm Öğrenciler")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    string sorgu = "SELECT * FROM Ogrenciler";
                    
                    // Filtreleme mantığı (OgrenciBurslari tablosuna göre)
                    switch (filtreTipi)
                    {
                        case "Burs Alanlar":
                            // OgrenciBurslari tablosunda Durum = 1 (Aktif) olan öğrenciler
                            sorgu = @"SELECT DISTINCT o.* 
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 1";
                            break;
                        case "Beklemedeki Öğrenciler":
                            // OgrenciBurslari tablosunda Durum = 0 (Beklemede) olan öğrenciler
                            sorgu = @"SELECT DISTINCT o.* 
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 0";
                            break;
                        default: // "Tüm Öğrenciler"
                            sorgu = "SELECT * FROM Ogrenciler";
                            break;
                    }
                    
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    da.Fill(dt);
                }
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }


        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            // Filtreleme combobox'ını başlat
            cmbFiltre.SelectedIndex = 0; // "Tüm Öğrenciler" seçili olsun
            
            Listele();

            // Grid Çoklu Seçim
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        // Filtreleme değiştiğinde listeyi yenile
        private void cmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltre.SelectedIndex >= 0)
            {
                string secilenFiltre = cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString();
                Listele(secilenFiltre);
            }
        }


        // 4. SİL
        public void btnSil_Click(object sender, EventArgs e)
        {
            int[] seciliSatirlar = gridView1.GetSelectedRows();

            if (seciliSatirlar.Length == 0)
            {
                MessageHelper.ShowWarning("Lütfen silinecek kayıtları seçiniz.", "Seçim Yapılmadı");
                return;
            }

            if (MessageHelper.ShowConfirm($"{seciliSatirlar.Length} adet kaydı silmek istediğinize emin misiniz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        foreach (int rowHandle in seciliSatirlar)
                        {
                            var id = gridView1.GetRowCellValue(rowHandle, "ID");
                            if (id != null)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                                cmd.Parameters.AddWithValue("@p1", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageHelper.ShowSuccess("Seçilen kayıtlar başarıyla silindi.", "Silme Başarılı");
                    Listele();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }


        // Profil Göster (Çift Tıklama veya Buton)
        private void btnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                OgrenciProfili frm = new OgrenciProfili();
                frm.MdiParent = this.MdiParent;
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);

                frm.ad = dr["AD"]?.ToString();
                frm.soyad = dr["SOYAD"]?.ToString();
                frm.haneGeliri = dr["TOPLAM HANE GELİRİ"]?.ToString();
                frm.fotoYolu = dr["FOTO"]?.ToString();
                frm.telNo = dr["TELEFON"]?.ToString();
                frm.bolum = dr["BÖLÜMÜ"]?.ToString();
                frm.sinif = dr["SINIF"]?.ToString();
                frm.kardesSayisi = dr["KARDEŞ SAYISI"]?.ToString();
                frm.agno = dr["AGNO"]?.ToString();

                frm.Show();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnGoster_Click(sender, e);
        }
    }
}