using DjiskstraKelurahan.Web.Models;
using Kruskal.Core;
using System.Drawing;

namespace DjiskstraKelurahan.Web.Services;

public class KelurahanService : IKelurahanService
{
    private readonly List<Kelurahan> _daftarKelurahan =
    [
        new Kelurahan { Nama = "Alak", PlaceId = "ChIJ6TBceuSbViwR_aQKEo_gGPk", Koordinat = new PointF(-10.197320007596058f, 123.53452923974321f)},
        new Kelurahan { Nama = "Namosain", PlaceId = "ChIJScJX0HGcViwRTLrJO6fFyEM", Koordinat = new PointF(-10.173405186457442f, 123.55845696004484f)},
        new Kelurahan { Nama = "Manulai 2", PlaceId = "ChIJw-f39lubViwRVuk37OLtIi0", Koordinat = new PointF(-10.217059685431211f, 123.57438838152984f)},
        new Kelurahan { Nama = "Penkase Oeleta", PlaceId = "ChIJWwrZWYmbViwRf8OjN89zb7Q", Koordinat = new PointF(-10.191157881753961f, 123.55869985269388f)},
        new Kelurahan { Nama = "Nunbaun Sabu", PlaceId = "ChIJ3ZZ5j3udViwR83LQn87c7eU", Koordinat = new PointF(-10.17752880958522f, 123.56614662440955f) },
        new Kelurahan { Nama = "Nunbaun Delha", PlaceId = "ChIJNZv0Q5adViwRsPJ8t-g56Vw", Koordinat = new PointF(-10.170672006797764f, 123.57155159557314f) },
        new Kelurahan { Nama = "Manutapen", PlaceId = "ChIJ03S29YOcViwRwUR5ld8qfNg", Koordinat = new PointF(-10.17497690768855f, 123.57932003419143f)},
        new Kelurahan { Nama = "Bakunase", PlaceId = "ChIJdeNE_2SbViwR_2cANxifdxQ", Koordinat = new PointF(-10.18580109644756f, 123.58683078393683f)},
        new Kelurahan { Nama = "Batu Plat", PlaceId = "ChIJNdc5ywybViwRVPW5uPCqri4", Koordinat = new PointF(-10.19901610738226f, 123.58015648393707f)},
        new Kelurahan { Nama = "Bakunase 2", PlaceId = "ChIJrQz0dCibViwRlifsH9j_Tso", Koordinat = new PointF(-10.187057416873795f, 123.59602969557328f)},
        new Kelurahan { Nama = "Airnona", PlaceId = "ChIJ1zluvnubViwRKRR3UmHA078", Koordinat = new PointF(-10.180744672463423f, 123.58542735324592f)},
        new Kelurahan { Nama = "Naikolan", PlaceId = "ChIJf5Gu4UWbViwRrPdoJjX9h7A", Koordinat = new PointF(-10.191071162568024f, 123.60534812811864f)},
        new Kelurahan { Nama = "Naikoten 1", PlaceId = "ChIJBxPGtuGbViwR1BmCnDnme4s", Koordinat = new PointF(-10.17991549089696f, 123.59715253604607f)},
        new Kelurahan { Nama = "Naikoten 2", PlaceId = "ChIJbxmuoSKdViwRrXRIwnDHJGk", Koordinat = new PointF(-10.172961967934091f, 123.5965393815291f)},
        new Kelurahan { Nama = "Nunleu", PlaceId = "ChIJbROj_JucViwRPlmM1dtXSio", Koordinat = new PointF(-10.173225886804897f, 123.58921683735147f)},
        new Kelurahan { Nama = "Oetete", PlaceId = "ChIJZdy1f5GcViwRUZgdPZCD5vw", Koordinat = new PointF(-10.164669639415601f, 123.58709638152904f)},
        new Kelurahan { Nama = "Kuanino", PlaceId = "ChIJSzxzD0GdViwRfhvG1kUHKHo", Koordinat = new PointF(-10.168432567716097f, 123.5934768526934f)},
        new Kelurahan { Nama = "Oebobo", PlaceId = "ChIJWXGpf72cViwR3xxaYc-V-VI", Koordinat = new PointF(-10.16614290484552f, 123.59548336803554f)},
        new Kelurahan { Nama = "Oebufu", PlaceId = "ChIJ3dGVzKuEViwRqt-bHPO3jAo", Koordinat = new PointF(-10.177104109295094f, 123.62204856433813f)},
        new Kelurahan { Nama = "Kayu Putih", PlaceId = "ChIJu1ugIuj0aS4R7f9LGwNHrsQ", Koordinat = new PointF(-10.167910522966427f, 123.61663079502227f)},
        new Kelurahan { Nama = "Fatululi", PlaceId = "ChIJjzjCE7ecViwR7bwPfSphcM4", Koordinat = new PointF(-10.160437920668093f, 123.6079643355023f)},
        new Kelurahan { Nama = "Pasir Panjang", PlaceId = "ChIJnUpdrdqdViwRJI9Nop3r4AE", Koordinat = new PointF(-10.151787194381708f, 123.60226801036424f)},
        new Kelurahan { Nama = "Kelapa Lima", PlaceId = "ChIJdeTcEzODViwREr6ppM8ODKc", Koordinat = new PointF(-10.14956473212861f, 123.6194897680353f)},
        new Kelurahan { Nama = "Oesapa Barat", PlaceId = "ChIJVZqmM_-DViwRapWnGVAMHj0", Koordinat = new PointF(-10.149923766821702f, 123.62974247783089f)},
        new Kelurahan { Nama = "TDM", PlaceId = "ChIJVVVVVUSDViwRHPL4WnFJ0p0", Koordinat = new PointF(-10.161275323860458f, 123.63474710851571f)},
        new Kelurahan { Nama = "Liliba", PlaceId = "ChIJkxmlFKCEViwR-L7ygOmiJjM", Koordinat = new PointF(-10.173798708128931f, 123.63858217783142f)},
        new Kelurahan { Nama = "Oesapa", PlaceId = "ChIJU4GuxXeDViwRQ5w05ciUA-I", Koordinat = new PointF(-10.148955448883928f, 123.65109889317314f)},
        new Kelurahan { Nama = "Oesapa Selatan", PlaceId = "ChIJNaARiwuDViwRRSVeRgLYjYE", Koordinat = new PointF(-10.161006742085174f, 123.64350987968011f)},
        new Kelurahan { Nama = "Lasiana", PlaceId = "ChIJ9WXtyq6DViwRHAvR3M8_ZeU", Koordinat = new PointF(-10.137199847078653f, 123.66804309317301f)},
        new Kelurahan { Nama = "Penfui", PlaceId = "ChIJ77Po3X2DViwRi7BkiEQFiQ4", Koordinat = new PointF(-10.169633104976105f, 123.65474673920009f)},
        new Kelurahan { Nama = "Naioni", Koordinat = new PointF(-10.255570041424118f, 123.6037314109198f) },
        new Kelurahan { Nama = "Fatukoa", Koordinat = new PointF(-10.232068892992618f, 123.60181548022861f) },
        new Kelurahan { Nama = "Sikumana", Koordinat = new PointF(-10.201638783245562f, 123.6059506569552f) },
        new Kelurahan { Nama = "Bello", Koordinat = new PointF(-10.217077974542077f, 123.62609706859182f) },
        new Kelurahan { Nama = "Oepura", Koordinat = new PointF(-10.188129079057887f, 123.60739886859155f) },
        new Kelurahan { Nama = "Mantasi", Koordinat = new PointF(-10.170727864695039f, 123.58042620906402f) },
        new Kelurahan { Nama = "Fatufeto", Koordinat = new PointF(-10.163158403576332f, 123.57477475324576f) },
        new Kelurahan { Nama = "Nunhila", Koordinat = new PointF(-10.167346465721453f, 123.57160630906411f) },
        new Kelurahan { Nama = "Airmata", Koordinat = new PointF(-10.165186904445761f, 123.57791211091835f) },
        new Kelurahan { Nama = "Lai Lai Bisi Kopan", Koordinat = new PointF(-10.161956966234323f, 123.57564201091839f) },
        new Kelurahan { Nama = "Fontein", Koordinat = new PointF(-10.165900104741416f, 123.58240018152885f)},
        new Kelurahan { Nama = "Kolhua", Koordinat = new PointF(-10.19998404157199f, 123.62882132570748f)},
        new Kelurahan { Nama = "Maulafa", Koordinat = new PointF(-10.185591633954925f, 123.62273826433831f)},
        new Kelurahan { Nama = "Naimata", Koordinat = new PointF(-10.176417088913126f, 123.64948655269356f)},
        new Kelurahan { Nama = "Solor", Koordinat = new PointF(-10.157854540630524f, 123.58202355269329f)},
        new Kelurahan { Nama = "Tode Kisar", Koordinat = new PointF(-10.15756907982664f, 123.58509607967991f)},
        new Kelurahan { Nama = "Bonipoi", Koordinat = new PointF(-10.160364536005936f, 123.58074076803553f)},
        new Kelurahan { Nama = "Merdeka", Koordinat = new PointF(-10.161477200132007f, 123.5880773661866f)},
        new Kelurahan { Nama = "Oeba", Koordinat = new PointF(-10.159322977627811f, 123.59162763735107f)},
        new Kelurahan { Nama = "Fatubesi", Koordinat = new PointF(-10.153906938809406f, 123.59393442385758f)},
        new Kelurahan { Nama = "Nefonaek", Koordinat = new PointF(-10.153569273833503f, 123.6045646257064f)},
    ];

    public List<Kelurahan> GetAll() => _daftarKelurahan;

    public Kelurahan? GetByName(string name) => _daftarKelurahan.FirstOrDefault(k => k.Nama == name);

    public Graph<Kelurahan> GetGraph() =>
        new(
            _daftarKelurahan,
            [
                (GetByName("Alak")!, GetByName("Namosain")!, 4.9),
                (GetByName("Alak")!, GetByName("Manulai 2")!, 8.4),
                (GetByName("Alak")!, GetByName("Penkase Oeleta")!, 3.9),

                (GetByName("Manulai 2")!, GetByName("Penkase Oeleta")!, 3.9),
                (GetByName("Manulai 2")!, GetByName("Batu Plat")!, 11.5),

                (GetByName("Penkase Oeleta")!, GetByName("Namosain")!, 2.8),
                (GetByName("Penkase Oeleta")!, GetByName("Nunbaun Sabu")!, 2.5),
                (GetByName("Penkase Oeleta")!, GetByName("Manutapen")!, 5.3),
                (GetByName("Penkase Oeleta")!, GetByName("Batu Plat")!, 9.3),

                (GetByName("Namosain")!, GetByName("Nunbaun Sabu")!, 1.4),

                (GetByName("Nunbaun Sabu")!, GetByName("Nunbaun Delha")!, 1.7),

                (GetByName("Nunbaun Delha")!, GetByName("Manutapen")!, 1.2),

                (GetByName("Manutapen")!, GetByName("Airnona")!, 2.7),
                (GetByName("Manutapen")!, GetByName("Bakunase")!, 3),

                (GetByName("Batu Plat")!, GetByName("Bakunase")!, 1.7),
                (GetByName("Batu Plat")!, GetByName("Bakunase 2")!, 3.7),

                (GetByName("Bakunase")!, GetByName("Airnona")!, 0.6),
                (GetByName("Bakunase")!, GetByName("Bakunase 2")!, 2.1),

                (GetByName("Airnona")!, GetByName("Nunleu")!, 2.1),
                (GetByName("Airnona")!, GetByName("Naikoten 1")!, 1.8),
                (GetByName("Airnona")!, GetByName("Bakunase 2")!, 2),

                (GetByName("Bakunase 2")!, GetByName("Naikoten 1")!, 1),
                (GetByName("Bakunase 2")!, GetByName("Naikolan")!, 2.3),

                (GetByName("Naikolan")!, GetByName("Naikoten 1")!, 1.9),
                (GetByName("Naikolan")!, GetByName("Oebufu")!, 2.8),

                (GetByName("Naikoten 1")!, GetByName("Naikoten 2")!, 1.5),
                (GetByName("Naikoten 1")!, GetByName("Nunleu")!, 1.6),
                (GetByName("Naikoten 1")!, GetByName("Oebobo")!, 2),

                (GetByName("Naikoten 2")!, GetByName("Nunleu")!, 1.2),
                (GetByName("Naikoten 2")!, GetByName("Kuanino")!, 1.4),
                (GetByName("Naikoten 2")!, GetByName("Oebobo")!, 1.4),

                (GetByName("Nunleu")!, GetByName("Oetete")!, 1.3),
                (GetByName("Nunleu")!, GetByName("Kuanino")!, 1.9),

                (GetByName("Oetete")!, GetByName("Pasir Panjang")!, 2.5),
                (GetByName("Oetete")!, GetByName("Kuanino")!, 1.2),
                (GetByName("Oetete")!, GetByName("Oebobo")!, 1),

                (GetByName("Kuanino")!, GetByName("Oebobo")!, 1.1),

                (GetByName("Oebobo")!, GetByName("Fatululi")!, 2),
                (GetByName("Oebobo")!, GetByName("Oebufu")!, 3.7),

                (GetByName("Oebufu")!, GetByName("Kayu Putih")!, 1.8),
                (GetByName("Oebufu")!, GetByName("Liliba")!, 2.3),

                (GetByName("Kayu Putih")!, GetByName("Fatululi")!, 1.7),
                (GetByName("Kayu Putih")!, GetByName("Kelapa Lima")!, 3),
                (GetByName("Kayu Putih")!, GetByName("Oesapa Barat")!, 3.7),
                (GetByName("Kayu Putih")!, GetByName("TDM")!, 3.2),

                (GetByName("Fatululi")!, GetByName("Pasir Panjang")!, 1.4),
                (GetByName("Fatululi")!, GetByName("Kelapa Lima")!, 2.6),

                (GetByName("Pasir Panjang")!, GetByName("Kelapa Lima")!, 2.1),

                (GetByName("Kelapa Lima")!, GetByName("Oesapa Barat")!, 1.3),

                (GetByName("Oesapa Barat")!, GetByName("Oesapa")!, 2.6),
                (GetByName("Oesapa Barat")!, GetByName("TDM")!, 2.2),

                (GetByName("TDM")!, GetByName("Liliba")!, 3.3),

                (GetByName("Liliba")!, GetByName("Oesapa Selatan")!, 2.2),
                (GetByName("Liliba")!, GetByName("Penfui")!, 2.3),

                (GetByName("Oesapa Selatan")!, GetByName("Oesapa")!, 3.7),
                (GetByName("Oesapa Selatan")!, GetByName("Lasiana")!, 5.3),

                (GetByName("Oesapa")!, GetByName("Lasiana")!, 2.6),

                (GetByName("Lasiana")!, GetByName("Penfui")!, 5.2),

                (GetByName("Naioni")!, GetByName("Manulai 2")!, 7.5),
                (GetByName("Naioni")!, GetByName("Fatukoa")!, 4.2),

                (GetByName("Fatukoa")!, GetByName("Sikumana")!, 4.1),

                (GetByName("Sikumana")!, GetByName("Oepura")!, 1.6),
                (GetByName("Sikumana")!, GetByName("Bello")!, 2.9),

                (GetByName("Bello")!, GetByName("Kolhua")!, 3.3),

                (GetByName("Oepura")!, GetByName("Maulafa")!, 2.2),
                (GetByName("Oepura")!, GetByName("Naikolan")!, 2.7),
                (GetByName("Oepura")!, GetByName("Oebufu")!, 2.2),

                (GetByName("Mantasi")!, GetByName("Airmata")!, 0.75),
                (GetByName("Mantasi")!, GetByName("Manutapen")!, 0.7),

                (GetByName("Fatufeto")!, GetByName("Nunhila")!, 0.75),
                (GetByName("Fatufeto")!, GetByName("Lai Lai Bisi Kopan")!, 0.24),
                (GetByName("Fatufeto")!, GetByName("Airmata")!, 0.45),
                (GetByName("Fatufeto")!, GetByName("Manutapen")!, 2.0),

                (GetByName("Nunhila")!, GetByName("Nunbaun Delha")!, 0.9),

                (GetByName("Airmata")!, GetByName("Lai Lai Bisi Kopan")!, 0.5),

                (GetByName("Lai Lai Bisi Kopan")!, GetByName("Solor")!, 0.8),
                (GetByName("Lai Lai Bisi Kopan")!, GetByName("Bonipoi")!, 0.9),

                (GetByName("Fontein")!, GetByName("Oetete")!, 0.7),
                (GetByName("Fontein")!, GetByName("Merdeka")!, 1.2),
                (GetByName("Fontein")!, GetByName("Bonipoi")!, 0.8),
                (GetByName("Fontein")!, GetByName("Nunleu")!, 1.3),

                (GetByName("Kolhua")!, GetByName("Maulafa")!, 2.2),
                (GetByName("Kolhua")!, GetByName("Naimata")!, 4.7),
                (GetByName("Kolhua")!, GetByName("Oepura")!, 3.2),

                (GetByName("Maulafa")!, GetByName("Oebufu")!, 1.2),
                (GetByName("Maulafa")!, GetByName("Naimata")!, 6.8),
                (GetByName("Maulafa")!, GetByName("Oepura")!, 2.2),

                (GetByName("Naimata")!, GetByName("Liliba")!, 1.5),
                (GetByName("Naimata")!, GetByName("Penfui")!, 2.6),

                (GetByName("Solor")!, GetByName("Tode Kisar")!, 0.5),
                (GetByName("Solor")!, GetByName("Bonipoi")!, 1.0),
                (GetByName("Solor")!, GetByName("Merdeka")!, 1.1),

                (GetByName("Tode Kisar")!, GetByName("Fatubesi")!, 1.4),
                (GetByName("Tode Kisar")!, GetByName("Merdeka")!, 1.0),

                (GetByName("Bonipoi")!, GetByName("Merdeka")!, 1.1),

                (GetByName("Merdeka")!, GetByName("Oeba")!, 0.8),
                (GetByName("Merdeka")!, GetByName("Oetete")!, 0.4),
                (GetByName("Merdeka")!, GetByName("Fatubesi")!, 1.5),

                (GetByName("Oeba")!, GetByName("Fatubesi")!, 0.85),
                (GetByName("Oeba")!, GetByName("Oetete")!, 1.2),
                (GetByName("Oeba")!, GetByName("Pasir Panjang")!, 1.6),

                (GetByName("Fatubesi")!, GetByName("Pasir Panjang")!, 1.2),

                (GetByName("Nefonaek")!, GetByName("Pasir Panjang")!, 0.4),
                (GetByName("Nefonaek")!, GetByName("Fatululi")!, 1.2),
                (GetByName("Nefonaek")!, GetByName("Oebobo")!, 2.4),
            ]
        );
}
