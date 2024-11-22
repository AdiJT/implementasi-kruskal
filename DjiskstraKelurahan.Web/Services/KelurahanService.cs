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
        new Kelurahan { Nama = "Nunbaun Sabu", PlaceId = "ChIJ3ZZ5j3udViwR83LQn87c7eU", Koordinat = new PointF(-10.177644969745893f, 123.56617881036473f)},
        new Kelurahan { Nama = "Nunbaun Delha", PlaceId = "ChIJNZv0Q5adViwRsPJ8t-g56Vw", Koordinat = new PointF(-10.177676649795272f, 123.56610370906414f) },
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
        new Kelurahan { Nama = "Naioni", PlaceId = },
        new Kelurahan { Nama = "Fatukoa", PlaceId = },
        new Kelurahan { Nama = "Sikumana", PlaceId = },
        new Kelurahan { Nama = "Bello", PlaceId = },
        new Kelurahan { Nama = "Oepura", PlaceId = },
        new Kelurahan { Nama = "Mantasi", PlaceId = },
        new Kelurahan { Nama = "Fatufeto", PlaceId = },
        new Kelurahan { Nama = "Nunhila", PlaceId = },
        new Kelurahan { Nama = "Airmata", PlaceId = },
        new Kelurahan { Nama = "Lai Lai Bisi Kopan", PlaceId = },
        new Kelurahan { Nama = "Fontein", PlaceId = },
        new Kelurahan { Nama = "Kolhua", PlaceId = },
        new Kelurahan { Nama = "Maulafa", PlaceId = },
        new Kelurahan { Nama = "Naimata", PlaceId = },
        new Kelurahan { Nama = "Solor", PlaceId = },
        new Kelurahan { Nama = "Tode Kisar", PlaceId = },
        new Kelurahan { Nama = "Bonipoi", PlaceId = },
        new Kelurahan { Nama = "Merdeka", PlaceId = },
        new Kelurahan { Nama = "Oeba", PlaceId = },
        new Kelurahan { Nama = "Fatubesi", PlaceId = },
        new Kelurahan { Nama = "Nefonaek", PlaceId = },
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
            ]
        );
}
