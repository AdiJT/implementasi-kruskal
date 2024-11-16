using DjiskstraKelurahan.Web.Models;
using Kruskal.Core;

namespace DjiskstraKelurahan.Web.Services;

public class KelurahanService : IKelurahanService
{
    private readonly List<Kelurahan> _daftarKelurahan =
    [
        new Kelurahan { Nama = "Alak", PlaceId = "ChIJ6TBceuSbViwR_aQKEo_gGPk"},
        new Kelurahan { Nama = "Namosain", PlaceId = "ChIJScJX0HGcViwRTLrJO6fFyEM"},
        new Kelurahan { Nama = "Manulai 2", PlaceId = "ChIJw-f39lubViwRVuk37OLtIi0"},
        new Kelurahan { Nama = "Penkase Oeleta", PlaceId = "ChIJWwrZWYmbViwRf8OjN89zb7Q"},
        new Kelurahan { Nama = "Nunbaun Sabu", PlaceId = "ChIJ3ZZ5j3udViwR83LQn87c7eU"},
        new Kelurahan { Nama = "Nunbaun Delha", PlaceId = "ChIJNZv0Q5adViwRsPJ8t-g56Vw"},
        new Kelurahan { Nama = "Manutapen", PlaceId = "ChIJ03S29YOcViwRwUR5ld8qfNg"},
        new Kelurahan { Nama = "Bakunase", PlaceId = "ChIJdeNE_2SbViwR_2cANxifdxQ"},
        new Kelurahan { Nama = "Batu Plat", PlaceId = "ChIJNdc5ywybViwRVPW5uPCqri4"},
        new Kelurahan { Nama = "Bakunase 2", PlaceId = "ChIJrQz0dCibViwRlifsH9j_Tso"},
        new Kelurahan { Nama = "Airnona", PlaceId = "ChIJ1zluvnubViwRKRR3UmHA078"},
        new Kelurahan { Nama = "Naikolan", PlaceId = "ChIJf5Gu4UWbViwRrPdoJjX9h7A"},
        new Kelurahan { Nama = "Naikoten 1", PlaceId = "ChIJBxPGtuGbViwR1BmCnDnme4s"},
        new Kelurahan { Nama = "Naikoten 2", PlaceId = "ChIJbxmuoSKdViwRrXRIwnDHJGk"},
        new Kelurahan { Nama = "Nunleu", PlaceId = "ChIJbROj_JucViwRPlmM1dtXSio"},
        new Kelurahan { Nama = "Oetete", PlaceId = "ChIJZdy1f5GcViwRUZgdPZCD5vw"},
        new Kelurahan { Nama = "Kuanino", PlaceId = "ChIJSzxzD0GdViwRfhvG1kUHKHo"},
        new Kelurahan { Nama = "Oebobo", PlaceId = "ChIJWXGpf72cViwR3xxaYc-V-VI"},
        new Kelurahan { Nama = "Oebufu", PlaceId = "ChIJ3dGVzKuEViwRqt-bHPO3jAo"},
        new Kelurahan { Nama = "Kayu Putih", PlaceId = "ChIJu1ugIuj0aS4R7f9LGwNHrsQ"},
        new Kelurahan { Nama = "Fatululi", PlaceId = "ChIJjzjCE7ecViwR7bwPfSphcM4"},
        new Kelurahan { Nama = "Pasir Panjang", PlaceId = "ChIJnUpdrdqdViwRJI9Nop3r4AE"},
        new Kelurahan { Nama = "Kelapa Lima", PlaceId = "ChIJdeTcEzODViwREr6ppM8ODKc"},
        new Kelurahan { Nama = "Oesapa Barat", PlaceId = "ChIJVZqmM_-DViwRapWnGVAMHj0"},
        new Kelurahan { Nama = "TDM", PlaceId = "ChIJVVVVVUSDViwRHPL4WnFJ0p0"},
        new Kelurahan { Nama = "Liliba", PlaceId = "ChIJkxmlFKCEViwR-L7ygOmiJjM"},
        new Kelurahan { Nama = "Oesapa", PlaceId = "ChIJU4GuxXeDViwRQ5w05ciUA-I"},
        new Kelurahan { Nama = "Oesapa Selatan", PlaceId = "ChIJNaARiwuDViwRRSVeRgLYjYE"},
        new Kelurahan { Nama = "Lasiana", PlaceId = "ChIJ9WXtyq6DViwRHAvR3M8_ZeU"},
        new Kelurahan { Nama = "Penfui", PlaceId = "ChIJ77Po3X2DViwRi7BkiEQFiQ4"},
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
