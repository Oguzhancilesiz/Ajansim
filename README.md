# Ajansim

Dijital ajans için kurumsal web sitesi ve içerik yönetim paneli. Sitedeki sayfalar, hizmetler, portföy ve blog admin panelinden yönetilir.

## Özellikler

- **İçerik yönetimi:** sayfalar, hizmetler, portföy çalışmaları, ekip, SSS
- **Blog:** kategori desteği
- **Medya kütüphanesi** ve layout (header/footer) ayarları
- **İletişim formu** ve gelen mesaj yönetimi
- **Kullanıcı yönetimi** ve dashboard

## Teknolojiler

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core + SQL Server

## Mimari

```
Ajansim.Entities → Ajansim.Mapping → Ajansim.Context → Ajansim.Services → Ajansim.WebUI
               Ajansim.Contracts, Ajansim.DTO, Ajansim.Core (ortak)
```

## Çalıştırma

```bash
git clone https://github.com/Oguzhancilesiz/Ajansim.git
cd Ajansim
dotnet ef database update --project Ajansim.Context --startup-project Ajansim.WebUI
dotnet run --project Ajansim.WebUI
```
