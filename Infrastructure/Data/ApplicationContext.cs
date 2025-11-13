using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
 new Author
 {
     Id = 1,
     Name = "J. R. R. Tolkien",
     Description = "John Ronald Reuel Tolkien (1892-1973) fue un filólogo y escritor británico, autor de obras como El hobbit y El Señor de los Anillos. Nacido en Bloemfontein (Sudáfrica) y criado en Inglaterra, se casó con Edith Bratt en 1916, con quien tuvo cuatro hijos. Vivió con ella hasta quedar viudo en 1971."
 },
        new Author
        {
            Id = 2,
            Name = "Agatha Christie",
            Description = "Agatha Christie (1890-1976) fue una escritora británica de novelas policiales, considerada la reina del misterio. Nacida en Torquay, Inglaterra, se casó primero con Archibald Christie, con quien tuvo una hija, Rosalind. Tras divorciarse, se casó con el arqueólogo Max Mallowan, con quien permaneció casada hasta su muerte."
        },
        new Author
        {
            Id = 3,
            Name = "George Orwell",
            Description = "George Orwell (1903-1950), seudónimo de Eric Arthur Blair, fue un escritor y periodista británico conocido por Rebelión en la granja y 1984. Nació en la India británica y creció en Inglaterra. Se casó con Eileen O’Shaughnessy, quien falleció en 1945, quedando viudo. Más tarde se casó con Sonia Brownell y adoptó un hijo, Richard."
        },
        new Author
        {
            Id = 4,
            Name = "Rick Riordan",
            Description = "Rick Riordan (1964) es un escritor estadounidense de fantasía juvenil, creador de la saga Percy Jackson. Nació en San Antonio, Texas. Está casado con Becky (Rebecca) Klahn desde 1985 y tienen dos hijos, Haley y Patrick. Ha trabajado como profesor antes de dedicarse por completo a la escritura."
        },
        new Author
        {
            Id = 5,
            Name = "Cassandra Clare",
            Description = "Cassandra Clare (1973), cuyo nombre real es Judith Rumelt, es una escritora estadounidense de fantasía urbana, conocida por la saga Cazadores de Sombras. Nació de padres estadounidenses en Teherán y creció en distintos países. Está casada con Joshua Lewis; no tienen hijos y viven en Massachusetts con varios gatos."
        },
        new Author
        {
            Id = 6,
            Name = "Gaston Leroux",
            Description = "Gaston Leroux (1868-1927) fue un novelista y periodista francés, célebre por El Fantasma de la Ópera. Nació en París y trabajó como reportero antes de dedicarse a la ficción. Se casó con Marie Lefranc y, tras enviudar, con Jeanne Cayatte; tuvo dos hijos y pasó sus últimos años en Niza."
        },
        new Author
        {
            Id = 7,
            Name = "Antoine de Saint-Exupéry",
            Description = "Antoine de Saint-Exupéry (1900-1944) fue un aviador y escritor francés, autor de El Principito. Nació en Lyon en una familia de pequeña nobleza. Se casó con la artista salvadoreña Consuelo Suncín; la pareja no tuvo hijos. Desapareció en una misión aérea durante la Segunda Guerra Mundial."
        },
        new Author
        {
            Id = 8,
            Name = "Gabriel García Márquez",
            Description = "Gabriel García Márquez (1927-2014) fue un escritor y periodista colombiano, figura central del realismo mágico y autor de Cien años de soledad. Nació en Aracataca, Colombia. Se casó con Mercedes Barcha en 1958 y tuvieron dos hijos, Rodrigo y Gonzalo. Vivió entre América Latina, México y Europa."
        },
        new Author
        {
            Id = 9,
            Name = "Jules Verne",
            Description = "Jules Verne (1828-1905) fue un novelista francés pionero de la ciencia ficción, conocido por obras como La vuelta al mundo en 80 días y Veinte mil leguas de viaje submarino. Nacido en Nantes, se casó con Honorine Morel en 1857, con quien tuvo un hijo, Michel, y fue padrastro de sus dos hijas."
        },
        new Author
        {
            Id = 10,
            Name = "Suzanne Collins",
            Description = "Suzanne Collins (1962) es una escritora estadounidense, creadora de la trilogía Los Juegos del Hambre. Nació en Hartford, Connecticut. Estuvo casada muchos años con el actor Charles “Cap” Pryor y es madre de dos hijos, Charlie e Isabel. Comenzó su carrera escribiendo para televisión infantil."
        },
        new Author
        {
            Id = 11,
            Name = "James Dashner",
            Description = "James Dashner (1972) es un escritor estadounidense de literatura juvenil y ciencia ficción, conocido por la saga The Maze Runner. Nació en Georgia y se crió en una familia numerosa. Está casado con Lynette Anderson y tienen cuatro hijos; viven en Utah."
        },
        new Author
        {
            Id = 12,
            Name = "Stephen King",
            Description = "Stephen King (1947) es un escritor estadounidense, maestro del terror y el suspense, autor de It, El resplandor y muchas otras novelas. Nació en Portland, Maine. Está casado con la escritora Tabitha Spruce desde 1971 y tienen tres hijos, Naomi, Joe y Owen."
        }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, GenreName = "Fantasía" },
                new Genre { Id = 2, GenreName = "Ciencia Ficción" },
                new Genre { Id = 3, GenreName = "Misterio" },
                new Genre { Id = 4, GenreName = "No Ficción" },
                new Genre { Id = 5, GenreName = "Distopía" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Manu", Email = "manu@gmail.com", Role = UserRole.SuperAdmin, Password = "123456" },
                new User { Id = 2, Username = "Mati", Email = "mati@gmail.com", Role = UserRole.SuperAdmin, Password = "123456" }
            );

            modelBuilder.Entity<Book>().HasData(
        // RICK RIORDAN (AuthorId = 4)
        new Book
        {
            Id = 1,
            Title = "El ladrón del rayo",
            Pages = 416,
            Summary = "Percy Jackson descubre que es hijo de un dios griego y debe emprender una misión para recuperar el rayo maestro de Zeus y evitar una guerra entre los dioses del Olimpo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1481567822i/7816926.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 2,
            Title = "El mar de los monstruos",
            Pages = 279,
            Summary = "Percy y sus amigos viajan al peligroso Mar de los Monstruos para encontrar el Vellocino de Oro y salvar el Campamento Mestizo de la destrucción.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1451102362i/8620262.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 3,
            Title = "La maldición del titán",
            Pages = 352,
            Summary = "Percy se une a una nueva misión para rescatar a un semidiós desaparecido y a la diosa Artemisa, enfrentándose a criaturas cada vez más peligrosas.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1297123292i/8267049.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 4,
            Title = "La batalla del laberinto",
            Pages = 361,
            Summary = "Percy y sus aliados se adentran en el Laberinto de Dédalo para impedir que el ejército de Cronos encuentre una entrada secreta al Campamento Mestizo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1511187879i/8267056.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 5,
            Title = "El último héroe del Olimpo",
            Pages = 381,
            Summary = "Percy lidera a los semidioses en la batalla final contra Cronos en las calles de Nueva York, donde se decide el destino del Olimpo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1317254851i/8267073.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 6,
            Title = "El héroe perdido",
            Pages = 576,
            Summary = "Jason despierta sin memoria en un autobús escolar y, junto a Piper y Leo, descubre que es un semidiós y que debe rescatar a Hera para evitar una nueva amenaza.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1359821070i/17188896.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 7,
            Title = "El hijo de Neptuno",
            Pages = 521,
            Summary = "Percy, sin recuerdos de su pasado, llega a un campamento romano y se une a Hazel y Frank en una peligrosa búsqueda para liberar a la Muerte.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1369923685i/17999618.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 8,
            Title = "La marca de Atenea",
            Pages = 586,
            Summary = "Siete semidioses, griegos y romanos, deben unirse para viajar a Roma, seguir la pista de la marca de Atenea y enfrentar a los gigantes de Gea.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1397161148i/18209368.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 9,
            Title = "La casa de Hades",
            Pages = 597,
            Summary = "Mientras Percy y Annabeth luchan por sobrevivir en el Tártaro, el resto de los semidioses intenta llegar a la Casa de Hades para cerrar las Puertas de la Muerte.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1388174908i/19452719.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 10,
            Title = "La pirámide roja",
            Pages = 528,
            Summary = "Los hermanos Carter y Sadie Kane descubren que son herederos de la magia de los faraones y deben detener a un dios egipcio que amenaza con destruir el mundo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1319186494i/12929218.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 11,
            Title = "El trono de fuego",
            Pages = 452,
            Summary = "Carter y Sadie continúan su entrenamiento mágico mientras buscan despertar al dios Ra para enfrentar a la serpiente del caos, Apofis.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1332092735i/13152321.jpg",
            AuthorId = 4
        },
        new Book
        {
            Id = 12,
            Title = "La sombra de la serpiente",
            Pages = 406,
            Summary = "Los Kane deben reunir aliados y lanzar un arriesgado plan para derrotar definitivamente a Apofis antes de que sumerja al mundo en el caos.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1348735646i/16049484.jpg",
            AuthorId = 4
        },

        // CASSANDRA CLARE (AuthorId = 5)
        new Book
        {
            Id = 13,
            Title = "Ciudad de huesos",
            Pages = 485,
            Summary = "Clary Fray descubre el mundo oculto de los Cazadores de Sombras cuando presencia un asesinato que solo ella parece ver y comienza a desentrañar secretos sobre su familia.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1376162468i/9025186.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 14,
            Title = "Ciudad de cenizas",
            Pages = 453,
            Summary = "Mientras el mundo subterráneo está en peligro por los planes de Valentine, Clary intenta proteger a su familia y sus amigos mientras lidia con nuevos poderes y sentimientos.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1290632930i/8398780.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 15,
            Title = "Ciudad de cristal",
            Pages = 541,
            Summary = "Clary viaja a la ciudad de Idris en busca de una cura para su madre y se ve envuelta en una guerra que definirá el futuro de los Cazadores de Sombras y los subterráneos.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1293514063i/8510934.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 16,
            Title = "Ciudad de los ángeles caídos",
            Pages = 425,
            Summary = "Tras la aparente paz, empiezan a aparecer cuerpos de Cazadores de Sombras asesinados y una nueva amenaza se cierne sobre Nueva York, poniendo a prueba las lealtades de todos.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1296855516i/10388376.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 17,
            Title = "Ciudad de las almas perdidas",
            Pages = 535,
            Summary = "Jace ha desaparecido y parece estar ligado a Sebastian; Clary se infiltra en sus planes para salvarlo, mientras el resto del grupo busca una forma de romper ese vínculo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1343424226i/15722810.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 18,
            Title = "Ciudad del fuego celestial",
            Pages = 725,
            Summary = "La batalla final contra Sebastian se desata y los Cazadores de Sombras deben arriesgarlo todo para evitar la destrucción de su mundo.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1402362254i/22450140.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 19,
            Title = "Ángel mecánico",
            Pages = 479,
            Summary = "En la Londres victoriana, Tessa Gray descubre que puede cambiar de forma y se ve envuelta en una conspiración mágica junto a los Cazadores de Sombras del Instituto de Londres.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1324687382i/9606604.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 20,
            Title = "Príncipe mecánico",
            Pages = 498,
            Summary = "Mientras el enemigo Mortmain se acerca a ejecutar su plan, Tessa debe enfrentarse a su pasado y a sus sentimientos por Will y Jem.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1329482768i/13482202.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 21,
            Title = "Princesa mecánica",
            Pages = 567,
            Summary = "La trilogía de los Orígenes culmina con un enfrentamiento decisivo contra Mortmain y decisiones que marcarán a varias generaciones de Cazadores de Sombras.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1364075211i/17670710.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 22,
            Title = "Lady Midnight",
            Pages = 688,
            Summary = "En Los Ángeles, Emma Carstairs investiga la muerte de sus padres mientras se ve envuelta en un caso de asesinatos conectados con magia oscura y secretos del pasado.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1459625664i/29762865.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 23,
            Title = "El señor de las sombras",
            Pages = 699,
            Summary = "Emma y los Blackthorn deben enfrentar las consecuencias de sus decisiones, mientras nuevas fuerzas oscuras amenazan a los Cazadores de Sombras.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1498754479i/35544729.jpg",
            AuthorId = 5
        },
        new Book
        {
            Id = 24,
            Title = "La reina del aire y la oscuridad",
            Pages = 893,
            Summary = "Tras una tragedia devastadora, el mundo de los Cazadores de Sombras se fragmenta y la familia Blackthorn debe evitar una guerra civil en el más alto nivel.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1709644904i/43382020.jpg",
            AuthorId = 5
        },

        // VARIOS AUTORES
        new Book
        {
            Id = 25,
            Title = "El Fantasma de la Ópera",
            Pages = 360,
            Summary = "En la Ópera de París, una misteriosa figura que habita en los pasadizos subterráneos se obsesiona con la joven soprano Christine Daaé, desencadenando una historia de amor, miedo y tragedia.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1560671713l/50833391.jpg",
            AuthorId = 6
        },
        new Book
        {
            Id = 26,
            Title = "El Principito",
            Pages = 360,
            Summary = "Un aviador perdido en el desierto se encuentra con un pequeño príncipe venido de otro planeta, que le cuenta sus viajes por distintos mundos y le recuerda la importancia de la amistad y lo esencial.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1328876389i/866618.jpg",
            AuthorId = 7
        },
        new Book
        {
            Id = 27,
            Title = "100 años de soledad",
            Pages = 132,
            Summary = "La novela recorre la historia de la familia Buendía en el pueblo ficticio de Macondo, mezclando realidad y fantasía para mostrar el peso del tiempo, la memoria y la soledad.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1347626503i/370523.jpg",
            AuthorId = 8
        },
        new Book
        {
            Id = 28,
            Title = "La vuelta al mundo en 80 dias",
            Pages = 187,
            Summary = "El metódico caballero inglés Phileas Fogg apuesta que puede dar la vuelta al mundo en 80 días, emprende un viaje contrarreloj y enfrenta todo tipo de imprevistos.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1519022835i/38617139.jpg",
            AuthorId = 9
        },

        // SUZANNE COLLINS (AuthorId = 10)
        new Book
        {
            Id = 29,
            Title = "Los Juegos del Hambre",
            Pages = 374,
            Summary = "En un futuro distópico, Katniss Everdeen se ofrece voluntaria para reemplazar a su hermana en unos juegos televisados a muerte organizados por el Capitolio.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1335891621i/6596839.jpg",
            AuthorId = 10
        },
        new Book
        {
            Id = 30,
            Title = "En llamas",
            Pages = 391,
            Summary = "Tras ganar los Juegos, Katniss se convierte en símbolo de rebeldía y es obligada a volver a la arena en una edición especial que enciende aún más la revolución.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1268836422i/7776692.jpg",
            AuthorId = 10
        },
        new Book
        {
            Id = 31,
            Title = "Sinsajo",
            Pages = 390,
            Summary = "Katniss se une abiertamente a la rebelión contra el Capitolio y debe decidir hasta dónde está dispuesta a llegar para cambiar el sistema.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1347835499i/8322565.jpg",
            AuthorId = 10
        },

        // JAMES DASHNER (AuthorId = 11)
        new Book
        {
            Id = 32,
            Title = "Correr o morir",
            Pages = 384,
            Summary = "Thomas despierta en un ascensor sin recuerdos y llega al Claro, rodeado por un gigantesco laberinto cambiante que él y otros chicos deben resolver para sobrevivir.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1354770985i/9167899.jpg",
            AuthorId = 11
        },
        new Book
        {
            Id = 33,
            Title = "Prueba de fuego",
            Pages = 362,
            Summary = "Tras escapar del Laberinto, Thomas y los demás son lanzados a un desierto ardiente lleno de peligros, donde deben superar una nueva prueba mortal.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1354771494i/12043291.jpg",
            AuthorId = 11
        },
        new Book
        {
            Id = 34,
            Title = "La Cura Mortal",
            Pages = 325,
            Summary = "Thomas se enfrenta a la verdad sobre CRUEL, las pruebas y la enfermedad llamada El Destello, en una última misión para decidir el destino de la humanidad.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1339216613i/15677818.jpg",
            AuthorId = 11
        },

        // STEPHEN KING (AuthorId = 12)
        new Book
        {
            Id = 35,
            Title = "It",
            Pages = 1184,
            Summary = "Un grupo de amigos se enfrenta de niños y luego de adultos a una entidad maligna que adopta la forma de un payaso llamado Pennywise en el pueblo de Derry.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1334416842i/830502.jpg",
            AuthorId = 12
        },
        new Book
        {
            Id = 36,
            Title = "Carrie",
            Pages = 272,
            Summary = "Carrie White, una adolescente marginada con poderes telequinéticos, es llevada al límite por el acoso de sus compañeros y el fanatismo de su madre.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1166254258i/10592.jpg",
            AuthorId = 12
        },
        new Book
        {
            Id = 37,
            Title = "La Milla Verde",
            Pages = 592,
            Summary = "En el corredor de la muerte de una prisión sureña, un guardia conoce a un condenado con habilidades sobrenaturales que cambian su visión sobre la justicia.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1339551789i/15703424.jpg",
            AuthorId = 12
        },
        new Book
        {
            Id = 38,
            Title = "El Resplandor",
            Pages = 497,
            Summary = "Jack Torrance acepta cuidar un hotel aislado durante el invierno junto a su familia, pero fuerzas oscuras comienzan a afectar su mente.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1281058680i/3389955.jpg",
            AuthorId = 12
        },
        new Book
        {
            Id = 39,
            Title = "Doctor Sueño",
            Pages = 608,
            Summary = "Danny Torrance, ya adulto, lucha con sus traumas y su alcoholismo mientras protege a una niña con poderes psíquicos de un grupo que se alimenta de niños como ella.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1376955133i/18359794.jpg",
            AuthorId = 12
        },

        // AGATHA CHRISTIE (AuthorId = 2)
        new Book
        {
            Id = 40,
            Title = "Asesinato en el Orient Express",
            Pages = 196,
            Summary = "Durante un lujoso viaje en tren, el detective Hércules Poirot debe resolver el asesinato de un pasajero mientras el Orient Express está detenido por la nieve.",
            CoverUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1507482254i/36375809.jpg",
            AuthorId = 2
        }
    );

            modelBuilder.Entity<User>()
                .HasMany(u => u.Lectures)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lecture>()
                .HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tabla intermedia Book-Genre
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    j => j.HasOne<Genre>()
                          .WithMany()
                          .HasForeignKey("GenreId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Book>()
                          .WithMany()
                          .HasForeignKey("BookId")
                          .OnDelete(DeleteBehavior.Cascade)
                )
                .HasData(
                    // Fantasía
                    new { BookId = 1, GenreId = 1 },
                    new { BookId = 2, GenreId = 1 },
                    new { BookId = 3, GenreId = 1 },
                    new { BookId = 4, GenreId = 1 },
                    new { BookId = 5, GenreId = 1 },
                    new { BookId = 6, GenreId = 1 },
                    new { BookId = 7, GenreId = 1 },
                    new { BookId = 8, GenreId = 1 },
                    new { BookId = 9, GenreId = 1 },
                    new { BookId = 10, GenreId = 1 },
                    new { BookId = 11, GenreId = 1 },
                    new { BookId = 12, GenreId = 1 },
                    new { BookId = 13, GenreId = 1 },
                    new { BookId = 14, GenreId = 1 },
                    new { BookId = 15, GenreId = 1 },
                    new { BookId = 16, GenreId = 1 },
                    new { BookId = 17, GenreId = 1 },
                    new { BookId = 18, GenreId = 1 },
                    new { BookId = 19, GenreId = 1 },
                    new { BookId = 20, GenreId = 1 },
                    new { BookId = 21, GenreId = 1 },
                    new { BookId = 22, GenreId = 1 },
                    new { BookId = 23, GenreId = 1 },
                    new { BookId = 24, GenreId = 1 },
                    new { BookId = 26, GenreId = 1 },
                    new { BookId = 27, GenreId = 1 },
                    new { BookId = 28, GenreId = 1 },

                    // Misterio
                    new { BookId = 25, GenreId = 3 },
                    new { BookId = 35, GenreId = 3 },
                    new { BookId = 36, GenreId = 3 },
                    new { BookId = 37, GenreId = 3 },
                    new { BookId = 38, GenreId = 3 },
                    new { BookId = 39, GenreId = 3 },
                    new { BookId = 40, GenreId = 3 },

                    // Distopía + Ciencia Ficción (Hunger Games + Maze Runner)
                    new { BookId = 29, GenreId = 5 },
                    new { BookId = 29, GenreId = 2 },
                    new { BookId = 30, GenreId = 5 },
                    new { BookId = 30, GenreId = 2 },
                    new { BookId = 31, GenreId = 5 },
                    new { BookId = 31, GenreId = 2 },
                    new { BookId = 32, GenreId = 5 },
                    new { BookId = 32, GenreId = 2 },
                    new { BookId = 33, GenreId = 5 },
                    new { BookId = 33, GenreId = 2 },
                    new { BookId = 34, GenreId = 5 },
                    new { BookId = 34, GenreId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
