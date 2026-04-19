using Triplace.Application.Commands;
using Triplace.Application.Services;
using Triplace.Domain.Enums;
using Triplace.Domain.Ids;
using Triplace.Domain.ValueObjects;

namespace Triplace.Api.Seeding;

public static class WieliczkaSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var attractionService = services.GetRequiredService<AttractionService>();
        var catalogService = services.GetRequiredService<SeasonalCatalogService>();
        var relationService = services.GetRequiredService<RelationService>();
        var routeService = services.GetRequiredService<RouteService>();

        // ─────────────────────────────────────────────
        // 1. Kopalnia Soli "Wieliczka" — węzeł główny
        // ─────────────────────────────────────────────
        var kopalniaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Kopalnia Soli Wieliczka",
            AttractionCategory.Museum,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.AudioGuide,
                AttractionAmenity.GuideAvailable,
                AttractionAmenity.WheelchairAccess,
                AttractionAmenity.GiftShop,
                AttractionAmenity.Cafe
            },
            [
                new MetadataEntry("adres",           "ul. Daniłowicza 10, 32-020 Wieliczka"),
                new MetadataEntry("lokalizacja",     "49.9837° N, 20.0577° E"),
                new MetadataEntry("dojazd",
                    "Autobus 304 z ul. Ogrodowej (Dworzec Główny Zachód) Kraków, ~30 min; " +
                    "bilet 60-min lub wieloprzejazdowy strefa I+II+III"),
                new MetadataEntry("dni_zamkniecia",
                    "1 stycznia, Niedziela Wielkanocna, 1 listopada, 24–25 grudnia"),
                new MetadataEntry("strona_www",      "https://www.kopalnia.pl"),
                new MetadataEntry("tagi",
                    "kopalnia, sól, UNESCO, Wieliczka, Małopolska, podziemia, zabytek")
            ]));

        // ─────────────────────────────────────────────────────────────────
        // 2. Trasa Turystyczna
        // ─────────────────────────────────────────────────────────────────
        // Źródło: kopalnia.pl/turysta-indywidualny/.../trasa-turystyczna
        //
        // Parametry fizyczne:
        //   Dystans:     ok. 3,5 km
        //   Schody:      ok. 800 (w tym 380 na starcie)
        //   Głębokość:   135 m p.p.t.
        //   Temperatura: 17–18 °C
        //   Czas:        2–3 h
        //   Max. grupa:  40 osób
        //   Min. wiek:   brak (dzieci w każdym wieku; do 4 lat bezpłatnie)
        //
        // Logistyka:
        //   Zbiórka:     Szyb Daniłowicza, ul. Daniłowicza 10
        //   Zakończenie: Szyb Regis lub Szyb Daniłowicza
        //   Języki:      PL, EN, DE, FR, IT, RU, ES, UA
        //   Bagaż:       max. 20×20×35 cm; bezpłatna przechowalnia przy wejściu
        //
        // Godziny (wg kopalniawieliczka.eu):
        //   Sty–Mar, Lis–Gru:  9:00–17:00
        //   Kwi–Cze, Wrz–Paź:  8:00–18:00
        //   Lip–Sie:            8:00–20:00
        //
        // Cennik orientacyjny 2025:
        //   Normalny ~103 zł | Ulgowy ~82 zł | Do 4 lat bezpłatnie
        //   Rodzinny od ~335 zł (2 dorosłych + 2 dzieci)
        //
        // Gastronomia: Karczma Górnicza (125 m p.p.t.) + Bistro Posolone + Hotel Grand Sal****
        // Bilet obejmuje: Muzeum Żup Krakowskich (ekspozycja podziemna, poziom III)
        // ─────────────────────────────────────────────────────────────────
        var trasaTurystycznaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Kopalnia Wieliczka – Trasa Turystyczna",
            AttractionCategory.Museum,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Long,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.AudioGuide,
                AttractionAmenity.GuideAvailable,
                AttractionAmenity.WheelchairAccess,  // częściowo; rezerwacja: rezerwacja@kopalnia.pl
                AttractionAmenity.GiftShop,
                AttractionAmenity.Cafe,
                AttractionAmenity.FamilyFriendly,    // dzieci w każdym wieku; do 4 lat bezpłatnie
                AttractionAmenity.BlindFriendly       // pies asystujący / przewodnik dopuszczony
            },
            [
                new MetadataEntry("opis",
                    "Główny szlak Kopalni — ok. 3,5 km krętych korytarzy, ~800 schodów " +
                    "i zejście na 135 m pod ziemię. Solankowe jeziora, Kaplica św. Kingi, " +
                    "solne rzeźby i dawne maszyny górnicze. Zwiedzanie zawsze z przewodnikiem. " +
                    "Bilet obejmuje podziemną ekspozycję Muzeum Żup Krakowskich."),
                new MetadataEntry("tagi",
                    "trasa-turystyczna, Kaplica-Kingi, podziemia, UNESCO, solankowe-jeziora, " +
                    "schody, przewodnik, rodzinne, historia, sól, Muzeum-Żup-Krakowskich"),
                // Parametry trasy
                new MetadataEntry("dystans",           "ok. 3,5 km"),
                new MetadataEntry("schody",            "ok. 800 (w tym 380 na samym starcie)"),
                new MetadataEntry("glebokosc",         "135 m p.p.t."),
                new MetadataEntry("temperatura",       "17–18 °C"),
                new MetadataEntry("czas_zwiedzania",   "2–3 h"),
                new MetadataEntry("max_grupa",         "40 osób"),
                new MetadataEntry("min_wiek",          "brak — dzieci w każdym wieku"),
                // Logistyka
                new MetadataEntry("miejsce_zbiórki",   "Szyb Daniłowicza, ul. Daniłowicza 10"),
                new MetadataEntry("miejsce_wyjscia",   "Szyb Regis lub Szyb Daniłowicza"),
                new MetadataEntry("adres",             "ul. Daniłowicza 10, 32-020 Wieliczka"),
                new MetadataEntry("jezyki",
                    "polski, angielski, niemiecki, francuski, włoski, rosyjski, hiszpański, ukraiński"),
                // Godziny otwarcia wg sezonu
                new MetadataEntry("godziny_styczen_marzec",       "9:00–17:00"),
                new MetadataEntry("godziny_kwiecien_czerwiec",     "8:00–18:00"),
                new MetadataEntry("godziny_lipiec_sierpien",       "8:00–20:00"),
                new MetadataEntry("godziny_wrzesien_pazdziernik",  "8:00–18:00"),
                new MetadataEntry("godziny_listopad_grudzien",     "9:00–17:00"),
                // Sprzedaż biletów
                new MetadataEntry("sprzedaz_biletow",
                    "Online (kopalnia.pl) | Automat przy Szybie Daniłowicza | " +
                    "Kasy Szybu Daniłowicza (pod warunkiem dostępności miejsc)"),
                // Cennik orientacyjny 2025
                new MetadataEntry("bilet_normalny",        "ok. 103 zł"),
                new MetadataEntry("bilet_ulgowy",          "ok. 82 zł"),
                new MetadataEntry("bilet_dziecko_do4lat",  "bezpłatnie"),
                new MetadataEntry("bilet_rodzinny",        "od ok. 335 zł (2 dorosłych + 2 dzieci)"),
                // Praktyczne
                new MetadataEntry("strój",
                    "Wygodne buty (dużo schodów); kurtka lub sweter — 17–18 °C pod ziemią"),
                new MetadataEntry("bagaz",
                    "Max. 20×20×35 cm na trasie. Bezpłatna przechowalnia przy wejściu " +
                    "(ograniczona liczba skrytek). Unikaj wózków dziecięcych — brak miejsca."),
                new MetadataEntry("gastronomia",
                    "Karczma Górnicza (125 m p.p.t.) — kuchnia polska, napoje; " +
                    "Bistro Posolone (powierzchnia, obok Szybu Daniłowicza); " +
                    "restauracja Hotelu Grand Sal****"),
                new MetadataEntry("toalety",
                    "Przy Szybie Daniłowicza przed wejściem + dwukrotnie w podziemiach"),
                new MetadataEntry("bankomat",
                    "W pobliżu Szybu Daniłowicza; płatność kartą we wszystkich punktach kopalni"),
                new MetadataEntry("dostepnosc_niepelnosprawni",
                    "Część wyrobisk przystosowana. Zwiedzanie PN–ND na pierwszej i ostatniej godzinie " +
                    "recepcji (PL lub EN). Rezerwacja obowiązkowa: rezerwacja@kopalnia.pl"),
                new MetadataEntry("pies_asystujacy",
                    "Pies asystujący i przewodnik osób niewidomych/słabowidzących — dozwolony"),
                new MetadataEntry("msza_sw",
                    "Kaplica św. Kingi — niedziele 7:30 (zjazd od 7:00). " +
                    "Grupy zorg.: zgłoszenie drg@kopalnia.pl (PN–PT). Indywidualni — bez rezerwacji.")
            ]));

        // ─────────────────────────────────────────────────────────────────
        // 3. Trasa Górnicza
        // ─────────────────────────────────────────────────────────────────
        // Źródło: kopalnia.pl/turysta-indywidualny/.../trasa-gornicza
        //
        // Parametry fizyczne:
        //   Dystans:     2 km
        //   Temperatura: 14–16 °C (chłodniejsza niż Trasa Turystyczna!)
        //   Czas:        2–3 h
        //   Max. grupa:  20 osób (połowa limitu Trasy Turystycznej)
        //   Min. wiek:   ukończone 10 lat; niepełnoletni pod opieką dorosłego
        //
        // Logistyka:
        //   Zbiórka:     Szyb Regis, pl. Kościuszki 9
        //   Przybycie:   min. 15 min przed — formalności BHP; spóźnienie = brak wejścia bez zwrotu
        //   Języki:      wyłącznie polski i angielski
        //   Bagaż:       szatnia z indywidualnymi szafkami w Szybie Regis
        //
        // Wyposażenie zapewniane przez kopalnię:
        //   kombinezon ochronny, kask, lampa górnicza, pochłaniacz CO
        //
        // Formalności przed wejściem:
        //   przebranie | pobranie sprzętu | szkolenie BHP | podpisanie oświadczenia | ewidencja zjazdów
        //
        // Godziny: wyznaczone sloty; aktualny harmonogram na kopalnia.pl
        // Bilety: online (max 90 dni z góry, min 1 h przed) lub kasy Szybu Regis
        //
        // Cennik orientacyjny 2025:
        //   Normalny ~115 zł | Ulgowy ~92 zł
        //
        // WAŻNE: brak restauracji i toalet na trasie; NIE dla niepełnosprawnych
        // ─────────────────────────────────────────────────────────────────
        var trasaGorniczaId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Kopalnia Wieliczka – Trasa Górnicza",
            AttractionCategory.Entertainment,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Medium,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.GuideAvailable
                // Brak FamilyFriendly — min. 10 lat i surowe warunki
                // Brak WheelchairAccess — trasa NIE jest przystosowana dla niepełnosprawnych
            },
            [
                new MetadataEntry("opis",
                    "Aktywna przygoda w surowych wyrobiskach poza standardową strefą kopalni. " +
                    "Uczestnicy wcielają się w górników prowadzonych przez przodowego — " +
                    "zakładają kombinezon, hełm, lampę i pochłaniacz CO, przechodzą szkolenie BHP " +
                    "i podpisują ewidencję zjazdów. Najstarsze wyrobiska pamiętają średniowiecze."),
                new MetadataEntry("tagi",
                    "trasa-górnicza, przygoda, górnicy, aktywna, kombinezon, BHP, szkolenie, " +
                    "przodowy, wyrobiska, średniowiecze, kondycja-fizyczna, 10-plus"),
                // Parametry trasy
                new MetadataEntry("dystans",           "2 km"),
                new MetadataEntry("temperatura",       "14–16 °C"),
                new MetadataEntry("czas_zwiedzania",   "2–3 h"),
                new MetadataEntry("max_grupa",         "20 osób"),
                new MetadataEntry("min_wiek",
                    "ukończone 10 lat; osoby niepełnoletnie wyłącznie pod opieką dorosłego"),
                // Logistyka
                new MetadataEntry("miejsce_zbiórki",   "Szyb Regis, pl. Kościuszki 9, 32-020 Wieliczka"),
                new MetadataEntry("adres",             "pl. Kościuszki 9, 32-020 Wieliczka (Szyb Regis)"),
                new MetadataEntry("przybycie",
                    "Min. 15 min przed godziną na bilecie — formalności BHP są obowiązkowe. " +
                    "Spóźnienie = brak wejścia; bilety nie podlegają zwrotowi."),
                new MetadataEntry("jezyki",            "wyłącznie polski i angielski"),
                // Godziny — wyznaczone sloty
                new MetadataEntry("godziny_wejsc",
                    "Wyznaczone sloty godzinowe; aktualny harmonogram: kopalnia.pl/trasa-gornicza"),
                // Sprzedaż biletów
                new MetadataEntry("sprzedaz_biletow",
                    "Online (kopalnia.pl) — max. 90 dni z wyprzedzeniem, min. 1 h przed wejściem | " +
                    "Kasy Szybu Regis (pod warunkiem dostępności)"),
                // Cennik orientacyjny 2025
                new MetadataEntry("bilet_normalny",    "ok. 115 zł"),
                new MetadataEntry("bilet_ulgowy",      "ok. 92 zł"),
                // Wyposażenie
                new MetadataEntry("wyposazenie",
                    "Kombinezon ochronny, kask, lampa górnicza, pochłaniacz CO — " +
                    "zapewniane przez kopalnię na miejscu"),
                // Ubranie i bagaż
                new MetadataEntry("strój",
                    "Strój pod kombinezon odpowiedni do 14–16 °C. " +
                    "Obuwie pełne, nietekstylne (osłaniające stopę) — obowiązkowe."),
                new MetadataEntry("bagaz",
                    "Szatnia z indywidualnymi szafkami w budynku Szybu Regis. " +
                    "Nie zabieraj dużych bagaży."),
                // Formalności BHP
                new MetadataEntry("formalnosci_bhp",
                    "1. Przebranie w kombinezon ochronny | " +
                    "2. Pobranie: lampy, kasku, pochłaniacza CO | " +
                    "3. Szkolenie BHP + podpisanie oświadczenia | " +
                    "4. Wpisanie do imiennej ewidencji zjazdów"),
                // Ograniczenia
                new MetadataEntry("dostepnosc_niepelnosprawni",
                    "Trasa NIE jest przystosowana dla osób niepełnosprawnych"),
                new MetadataEntry("gastronomia",
                    "Brak punktów gastronomicznych na trasie. " +
                    "Bistro Posolone i Hotel Grand Sal**** na powierzchni " +
                    "(przy Szybie Daniłowicza — ok. 700 m od Szybu Regis)"),
                new MetadataEntry("toalety",
                    "Tylko w budynku Szybu Regis przed i po zwiedzaniu. " +
                    "Brak toalet w podziemiach na tej trasie.")
            ]));

        // ─────────────────────────────────────────────
        // 4. Tężnia Solankowa
        // ─────────────────────────────────────────────
        var tezniaSolankId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Tężnia Solankowa w Wieliczce",
            AttractionCategory.Park,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn },
            VisitDuration.Short,
            IsOutdoor: true,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.FamilyFriendly,
                AttractionAmenity.WheelchairAccess,
                AttractionAmenity.ParkingNearby
            },
            [
                new MetadataEntry("opis",
                    "Największa tężnia solankowa w południowej Polsce, w Parku św. Kingi. " +
                    "Inhalacja solankowego aerozolu działa leczniczo na górne i dolne drogi oddechowe. " +
                    "Z wieży roztacza się widok na teren kopalni i miasto. " +
                    "Zalecany pobyt: minimum 30 minut."),
                new MetadataEntry("tagi",
                    "tężnia, solanka, inhalacja, zdrowie, drogi-oddechowe, relaks, widok, " +
                    "Park-Kingi, surface, na-zewnątrz"),
                new MetadataEntry("adres",          "Park św. Kingi, 32-020 Wieliczka"),
                new MetadataEntry("lokalizacja",    "49.9845° N, 20.0545° E"),
                new MetadataEntry("czas_zwiedzania","min. 30 minut"),
                new MetadataEntry("bilet_normalny", "ok. 9 zł"),
                new MetadataEntry("bilet_ulgowy",   "ok. 7 zł"),
                new MetadataEntry("sezonowosc",     "Czynna wiosna–jesień; harmonogram na kopalnia.pl"),
                new MetadataEntry("polecana_dla",
                    "Osoby z dolegliwościami górnych i dolnych dróg oddechowych; " +
                    "rekreacja dla wszystkich")
            ]));

        // ─────────────────────────────────────────────
        // 5. Kaplica św. Kingi — highlight Trasy Turystycznej
        // ─────────────────────────────────────────────
        var kaplicaKingiId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Kaplica św. Kingi (Kopalnia Wieliczka)",
            AttractionCategory.Church,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.GuideAvailable,
                AttractionAmenity.BlindFriendly,
                AttractionAmenity.FamilyFriendly
            },
            [
                new MetadataEntry("opis",
                    "Najsłynniejsza komora Kopalni — kaplica wykuta w soli kamiennej. " +
                    "Ołtarze, rzeźby i żyrandole w całości z soli. Regularne msze niedzielne. " +
                    "Wstęp wliczony w bilet Trasy Turystycznej."),
                new MetadataEntry("tagi",       "kaplica, kościół, Kinga, solne-rzeźby, msza, UNESCO, must-see"),
                new MetadataEntry("glebokosc",  "101 m p.p.t. (poziom III)"),
                new MetadataEntry("wymiary",    "54 m dł. × 18 m szer. × 12 m wys."),
                new MetadataEntry("msza",
                    "Niedziela 7:30 (zjazd od 7:00). " +
                    "Grupy zorg.: zgłoszenie drg@kopalnia.pl (PN–PT). " +
                    "Turyści indywidualni i rodziny — bez rezerwacji."),
                new MetadataEntry("bilet",  "Wliczony w bilet Trasy Turystycznej"),
                new MetadataEntry("adres",  "ul. Daniłowicza 10, 32-020 Wieliczka")
            ]));

        // ─────────────────────────────────────────────
        // 6. Muzeum Żup Krakowskich
        // ─────────────────────────────────────────────
        var muzeuZupId = await attractionService.CreateDraftAsync(new CreateAttractionCommand(
            "Muzeum Żup Krakowskich (Wieliczka)",
            AttractionCategory.Museum,
            new HashSet<Season> { Season.Spring, Season.Summer, Season.Autumn, Season.Winter },
            VisitDuration.Short,
            IsOutdoor: false,
            IsFree: false,
            new HashSet<AttractionAmenity>
            {
                AttractionAmenity.GuideAvailable,
                AttractionAmenity.WheelchairAccess
            },
            [
                new MetadataEntry("opis",
                    "Podziemna ekspozycja historii solnictwa na poziomie III kopalni. " +
                    "Wstęp wliczony w bilet Trasy Turystycznej."),
                new MetadataEntry("tagi",       "muzeum, solnictwo, historia, Żupy-Krakowskie, poziom-III"),
                new MetadataEntry("adres",      "ul. Daniłowicza 10, 32-020 Wieliczka"),
                new MetadataEntry("bilet",      "Wliczony w bilet Trasy Turystycznej"),
                new MetadataEntry("lokalizacja","Poziom III kopalni (135 m p.p.t.)")
            ]));

        // ─────────────────────────────────────────────
        // 7. Publikacja wszystkich atrakcji
        // ─────────────────────────────────────────────
        foreach (var id in new[]
        {
            kopalniaId, trasaTurystycznaId, trasaGorniczaId,
            tezniaSolankId, kaplicaKingiId, muzeuZupId
        })
            await attractionService.PublishAsync(id);

        // ─────────────────────────────────────────────
        // 8. Hierarchia
        // ─────────────────────────────────────────────
        await attractionService.AddChildAsync(kopalniaId, trasaTurystycznaId);
        await attractionService.AddChildAsync(kopalniaId, trasaGorniczaId);
        await attractionService.AddChildAsync(kopalniaId, tezniaSolankId);
        await attractionService.AddChildAsync(trasaTurystycznaId, kaplicaKingiId);
        await attractionService.AddChildAsync(trasaTurystycznaId, muzeuZupId);

        // ─────────────────────────────────────────────
        // 9. Katalog sezonowy
        // ─────────────────────────────────────────────
        var katalogId = await catalogService.CreateAsync(new CreateSeasonalCatalogCommand(
            "Wieliczka – Lato 2025",
            Season.Summer,
            new DateOnly(2025, 4, 1),
            new DateOnly(2025, 10, 31),
            "Małopolska",
            "Sezonowy katalog Kopalni Soli Wieliczka — rozszerzony harmonogram lato/wiosna/jesień",
            maxCapacity: null));

        foreach (var id in new[] { kopalniaId, trasaTurystycznaId, trasaGorniczaId, tezniaSolankId })
            await catalogService.AddAttractionAsync(katalogId, id);

        // ─────────────────────────────────────────────
        // 10. Relacje
        // ─────────────────────────────────────────────

        // WYKLUCZENIE: Trasa Turystyczna ↔ Trasa Górnicza
        // "Nie planuj obu tras tego samego dnia" — każda 2–3 h w wymagających warunkach.
        // Ograniczenie modelu: Exclusion semantycznie = wzajemne wykluczenie,
        // a nie "możliwe, lecz wyczerpujące razem".
        await relationService.AddExclusionAsync(
            new AttractionId((await attractionService.GetByIdAsync(trasaTurystycznaId))!.Id.Value),
            new AttractionId((await attractionService.GetByIdAsync(trasaGorniczaId))!.Id.Value));

        // REKOMENDACJA: po każdej trasie podziemnej → Tężnia Solankowa
        await relationService.AddRecommendationAsync(
            new AttractionId((await attractionService.GetByIdAsync(trasaTurystycznaId))!.Id.Value),
            new AttractionId((await attractionService.GetByIdAsync(tezniaSolankId))!.Id.Value));

        await relationService.AddRecommendationAsync(
            new AttractionId((await attractionService.GetByIdAsync(trasaGorniczaId))!.Id.Value),
            new AttractionId((await attractionService.GetByIdAsync(tezniaSolankId))!.Id.Value));

        // ─────────────────────────────────────────────
        // 11. Trasy (Route)
        // ─────────────────────────────────────────────

        // Trasa A — Dzień Klasyczny
        await routeService.CreateAsync(new CreateRouteCommand(
            "Wieliczka – Dzień Klasyczny",
            "Optymalna wizyta indywidualna: Trasa Turystyczna (2–3 h, max. 40 osób, bez limitu wieku, " +
            "zbiórka Szyb Daniłowicza) oraz relaks w Tężni Solankowej (30+ min). " +
            "Ubierz wygodne buty i kurtka na 17–18 °C. Bilety zarezerwuj online.",
            Season.Summer,
            scopeAttractionId: null,
            [
                new RouteItemCommand(new AttractionId(trasaTurystycznaId.Value), Priority.Must),
                new RouteItemCommand(new AttractionId(tezniaSolankId.Value),     Priority.Optional)
            ]));

        // Trasa B — Szlak Górniczy
        await routeService.CreateAsync(new CreateRouteCommand(
            "Wieliczka – Szlak Górniczy",
            "Aktywna przygoda dla osób 10+ lat w dobrej kondycji. " +
            "Trasa Górnicza (2–3 h, max. 20 osób, 14–16 °C, zbiórka Szyb Regis). " +
            "Przybądź 15 min wcześniej na formalności BHP — spóźnienie = brak wejścia bez zwrotu. " +
            "Kombinezon, hełm i lampa zapewniane. Brak toalet i gastronomii na trasie. " +
            "Bilety online max. 90 dni i min. 1 h przed. Po trasie — polecana Tężnia.",
            Season.Summer,
            scopeAttractionId: null,
            [
                new RouteItemCommand(new AttractionId(trasaGorniczaId.Value), Priority.Must),
                new RouteItemCommand(new AttractionId(tezniaSolankId.Value),  Priority.Optional)
            ]));

        // Trasa C — Pełne Doświadczenie (2 dni)
        await routeService.CreateAsync(new CreateRouteCommand(
            "Wieliczka – Pełne Doświadczenie (2 dni)",
            "Dla entuzjastów historii solnictwa. " +
            "Dzień 1: Trasa Turystyczna (Kaplica św. Kingi, Muzeum Żup) + Tężnia Solankowa. " +
            "Dzień 2: Trasa Górnicza (min. 10 lat, dobra kondycja, Szyb Regis). " +
            "WAŻNE: nie planuj obu tras w jednym dniu — każda trwa 2–3 h w wymagających warunkach " +
            "i przy różnych temperaturach (17–18 °C vs. 14–16 °C).",
            Season.Summer,
            scopeAttractionId: null,
            [
                new RouteItemCommand(new AttractionId(trasaTurystycznaId.Value), Priority.Must),
                new RouteItemCommand(new AttractionId(trasaGorniczaId.Value),    Priority.Must),
                new RouteItemCommand(new AttractionId(tezniaSolankId.Value),     Priority.Optional),
                new RouteItemCommand(new AttractionId(kaplicaKingiId.Value),     Priority.Optional)
            ]));
    }
}