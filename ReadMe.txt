https://adminlte.io/themes/v3/pages/UI/buttons.html

https://medium.com/



















































/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
@*path2*@
                             @{
                                bool followq = false;
                            }
                            @foreach (var item2 in ViewBag.myBag)
                            {
                                followq = false;
                                if (item.ID == item2.CategoryID)
                                {

                                    followq = true;

                                }
                            }
                            @if (followq)
                            {
                                <a asp-action="Follow" asp-route-id="@item.ID" class="btn btn-succsess">Takip Et</a>

                            }
                            else
                            {
                                <a asp-action="Follow" asp-route-id="@item.ID" class="btn btn-primary">Takibi bırak</a>

                            }



qwegfvhqwjkelqwnebqwnlekşqwnm eş
BLOG PROJESİ -

Blank solutionla projemzi açtık.

* Solution altına ilk etapta sınıfları oluşturacağımız MODELS katmanı için CLass Library projesini ekledik.

* MODELS => Entities => Abstract ve Concrete klasörleri açılır.Uygulamada kullanılacak temel/ concrete sınıflar oluşur.

* MODELS => Enums klasörü açılır. Gerekli enumla burada utulur.

* MODELS => EntityTypeConfiguration (veritabanı ilişki/tabloları için ) klasörü açılır =>Abstract ve Concrete klasörü altında konfigurasyonlar yapılır.

not => IFormFile propertys için efcore.http.features paketini versiyonlara dikkat ederek indirdik.
       Mapleme işlemlerinde IdentityRole datalarını ekleyebilmek için IDENTİTYROLE sınıfına ihtiyacımız vardı ve bu yüzden mic.extension.ıdentity.strores nugetını versiyonuna DİKKAT ederek indirdik.
       Konfigurasyonlar içinde mic.efcore.sqlServer kullandık.



*   *   *   *   *   *   *   *   *   *   *   *


2. DataAccessLayer  olarak adlandıarabileceğimiz Repolarımıız ve Context sınıfımızı içeren katmanı oluşturacağız.

Solution altına Class Library pojesi ekleyelim.
DAL => Repositories => Abstract - Cncrete - Interfaces klasörü açılır.
DAL => Context => Context sınıfları oluşturulur.

Gerekiyorsa ki gerekecektir MODELS katmanı referans olarak verilir.

Gerekli olan  mic.aspnetcore.identity.frameworkcore / efcore.tools/ efcore.sqlServer nugetları ihtiyac anında inidirilecektir.


*   *   *   *   *   *   *

3.Kullanıcı ile iletşime geçeceğimiz web katmanı/ (ui katmanı da denebilir ) aspnet core web app (mvc) projemizi açtık.

Göç başlatmamız gerektiği için sppsettingjsona connectionstrgimiiz yazıp,StartUpda söylemeliyiz.
Göç aşamasında package manager consoldaki default pro : contextin olduğu proje + dizinde web katmanı olmalı.
Göç için efcore.design paketi kullanıyoruz.

add-migration isim ve devamında update-database kullanılmalı.

* Areas klsörüü açalım. => add - area - mvc area - isim verilir ve aalan açılmış olur.
StartUp için arealı route da verilebilmesi için endPOint eklenmeli.
NOT => area içinde açılan controllerların üstüne [Area("isim")] attribute eklemeyi unutmayalım.

* Projede identity kütüphanesini kullannacağımız için startUpda söyleyelim.

İlk etapta kullanıcı  create etmek için area dışındaki controllerda USERCONTROLLER açıp başlayalım.
UserControllerın ctorumda IAppUserRepository enhjekte ettik ve startUp içinde addscoped metotu yardımıyla soyut hallerden - concrete halini teslim almış olduk.

mapper kütüphanesi için =>AutoMapper + AutoMapper.Extensions.Mic.DI +  models => mappers aç ve içine sınıf oluştur VE STATRUPDA SÖYLE.
 
kullanıcıyı oluşturrken foto için kütüphane gerek  SixLabors.ImageSharp + SixLabors.ImageSharp.Web indir.

 

 ----------------------------------------------------------------------
 view'da file gibi bişeyler alcaksak kayıtta 
     <form  asp-action="Create" method="post" enctype="multipart/form-data">
     'daki  method="post" enctype="multipart/form-data"> kısmı yazılması gerek.
