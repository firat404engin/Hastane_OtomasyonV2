Tabii, işte emojilerle süslenmiş ve GitHub için uygun başlık yapısıyla düzenlenmiş proje açıklamanız:

---

# 🏥 Hastane Otomasyonu

## 🚪 Giriş Ekranı

**Özellikler:**
- **Hasta Girişi Butonu:** Hasta giriş formunu ("FrmHastaGiris") açar. 🧑‍⚕️
- **Doktor Girişi Butonu:** Doktor giriş formunu ("FrmDoktorGiris") açar. 👨‍⚕️
- **Sekreter Girişi Butonu:** Sekreter giriş formunu ("FrmSekreterGiris") açar. 🧑‍💼

**Kullanıcı Tipleri:**
- Hasta 🏥
- Doktor 👨‍⚕️
- Sekreter 🧑‍💼

## 👩‍⚕️ Hasta Girişi Ekranı

**Bileşenler:**
- **TC Kimlik Numarası Alanı:** Hastanın TC kimlik numarasını girmesi için alan. 🆔
- **Şifre Alanı:** Hastanın hesabına erişim sağlamak için şifre girmesi gereken alan. 🔑
- **Doğrulama Kodu Alanı:** Hastanın insan olduğunu doğrulamak için girilmesi gereken kod alanı. 🔢
- **Giriş Butonu:** Hastanın giriş bilgilerini doğruladıktan sonra sisteme giriş yapmasını sağlar. 🚪
- **Kayıt Ol Butonu:** Sistemde kayıtlı olmayan hastalar için kayıt formunu açar. ✍️

**Kayıt Olma İşlemi:**
- Hasta, kişisel bilgilerini girerek sisteme kayıt olur. 📝
- Veritabanına yeni hasta bilgileri kaydedilir. 💾

## 📅 Hasta Randevu Alma Ekranı

**FrmHastaDetay Formu:**
- **Hasta Bilgileri:** Ad, soyad ve TC kimlik numarası. 🧑‍⚕️
- **Randevu Alma Paneli:** Branş, doktor, randevu tarihi ve saati seçimi yapılabilir. 📅
- **Şikayet Belirtme Bölümü:** Hastanın şikayetlerini yazabileceği bölüm. 🗣️

**Randevu Yönetimi:**
- Hasta, uygun tarih ve saatte randevu alabilir. 📆
- Randevu kontenjanı doluysa, ek kontenjan açılabilir. 📈

**Randevu Listesi:**
- Hastanın aldığı ve katıldığı randevular listelenir. 📋
- Randevuya gidildi olarak işaretleme ve değerlendirme yapılabilir. ✅

## 🔄 Kişisel Bilgiler Güncelleme

**Bileşenler:**
- Ad, soyad, telefon numarası, cinsiyet ve şifre güncellenebilir. 🛠️

**Güncelleme İşlemi:**
- Hasta, kişisel bilgilerini güncelleyip kaydedebilir. 💼
- Veritabanındaki hasta bilgileri güncellenir. 🔄

## ⭐ Hasta Hizmet Kalitesi

- Hastanın aldığı ve daha önce katıldığı randevular, formun alt kısmındaki panellerde listelenir. 📊
- Hasta, randevuyu "gidildi" olarak işaretlediğinde, "Randevuya gittiğinizi onaylıyor musunuz?" mesajı gelir. Onaylandığında, "Hizmet Kalitesini Değerlendirme Ekranı" açılır. 📝
- Bu ekranda, 1'den 3'e kadar puanlama yapılabilir ve bu puanlar veritabanına kaydedilir. ⭐⭐⭐

## 👨‍⚕️ Doktor Detay Ekranı

**Doktor Bilgi Güncelleme:**
- Doktor, kişisel bilgilerini güncelleyebilir. 🔧
- Yeni duyurular ekleyebilir ve mevcut duyuruları yönetebilir. 📣

**Özellikler:**
- TC numarası, isim ve hastaların randevu bilgileri. 🆔
- Bilgi güncelleme için ayarlar simgesine tıklanabilir. ⚙️
- Duyuruları görüntülemek için duyuru simgesine tıklanabilir. 📰

## 🧑‍💼 Sekreter Detay Ekranı

**Özellikler:**
- Sekreterin ad, soyad ve TC kimlik numarası bilgileri görüntülenir. 🧾
- Randevu paneli ile yeni randevular oluşturulabilir. 📅
- Duyuru paneli ile duyurular yapılabilir. 📢
- Hızlı erişim paneli ile doktorlar listelenebilir ve yeni doktor kayıtları oluşturulabilir. 📋

**Yeni Branşlar ve Randevular:**
- Yeni branşlar ekleyebilir ve mevcutları güncelleyebilir. 🏥
- Aktif ve geçmiş randevulara erişim sağlayabilir. 📅

## 🛠️ Sekreter İşlemleri

- Doktor ve branş bilgilerini yönetebilir. 🧑‍⚕️
- Randevuların takibini yapabilir. 📈

