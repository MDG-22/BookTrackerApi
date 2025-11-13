using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    PageCount = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "John Ronald Reuel Tolkien (1892-1973) fue un filólogo y escritor británico, autor de obras como El hobbit y El Señor de los Anillos. Nacido en Bloemfontein (Sudáfrica) y criado en Inglaterra, se casó con Edith Bratt en 1916, con quien tuvo cuatro hijos. Vivió con ella hasta quedar viudo en 1971.", "J. R. R. Tolkien" },
                    { 2, "Agatha Christie (1890-1976) fue una escritora británica de novelas policiales, considerada la reina del misterio. Nacida en Torquay, Inglaterra, se casó primero con Archibald Christie, con quien tuvo una hija, Rosalind. Tras divorciarse, se casó con el arqueólogo Max Mallowan, con quien permaneció casada hasta su muerte.", "Agatha Christie" },
                    { 3, "George Orwell (1903-1950), seudónimo de Eric Arthur Blair, fue un escritor y periodista británico conocido por Rebelión en la granja y 1984. Nació en la India británica y creció en Inglaterra. Se casó con Eileen O’Shaughnessy, quien falleció en 1945, quedando viudo. Más tarde se casó con Sonia Brownell y adoptó un hijo, Richard.", "George Orwell" },
                    { 4, "Rick Riordan (1964) es un escritor estadounidense de fantasía juvenil, creador de la saga Percy Jackson. Nació en San Antonio, Texas. Está casado con Becky (Rebecca) Klahn desde 1985 y tienen dos hijos, Haley y Patrick. Ha trabajado como profesor antes de dedicarse por completo a la escritura.", "Rick Riordan" },
                    { 5, "Cassandra Clare (1973), cuyo nombre real es Judith Rumelt, es una escritora estadounidense de fantasía urbana, conocida por la saga Cazadores de Sombras. Nació de padres estadounidenses en Teherán y creció en distintos países. Está casada con Joshua Lewis; no tienen hijos y viven en Massachusetts con varios gatos.", "Cassandra Clare" },
                    { 6, "Gaston Leroux (1868-1927) fue un novelista y periodista francés, célebre por El Fantasma de la Ópera. Nació en París y trabajó como reportero antes de dedicarse a la ficción. Se casó con Marie Lefranc y, tras enviudar, con Jeanne Cayatte; tuvo dos hijos y pasó sus últimos años en Niza.", "Gaston Leroux" },
                    { 7, "Antoine de Saint-Exupéry (1900-1944) fue un aviador y escritor francés, autor de El Principito. Nació en Lyon en una familia de pequeña nobleza. Se casó con la artista salvadoreña Consuelo Suncín; la pareja no tuvo hijos. Desapareció en una misión aérea durante la Segunda Guerra Mundial.", "Antoine de Saint-Exupéry" },
                    { 8, "Gabriel García Márquez (1927-2014) fue un escritor y periodista colombiano, figura central del realismo mágico y autor de Cien años de soledad. Nació en Aracataca, Colombia. Se casó con Mercedes Barcha en 1958 y tuvieron dos hijos, Rodrigo y Gonzalo. Vivió entre América Latina, México y Europa.", "Gabriel García Márquez" },
                    { 9, "Jules Verne (1828-1905) fue un novelista francés pionero de la ciencia ficción, conocido por obras como La vuelta al mundo en 80 días y Veinte mil leguas de viaje submarino. Nacido en Nantes, se casó con Honorine Morel en 1857, con quien tuvo un hijo, Michel, y fue padrastro de sus dos hijas.", "Jules Verne" },
                    { 10, "Suzanne Collins (1962) es una escritora estadounidense, creadora de la trilogía Los Juegos del Hambre. Nació en Hartford, Connecticut. Estuvo casada muchos años con el actor Charles “Cap” Pryor y es madre de dos hijos, Charlie e Isabel. Comenzó su carrera escribiendo para televisión infantil.", "Suzanne Collins" },
                    { 11, "James Dashner (1972) es un escritor estadounidense de literatura juvenil y ciencia ficción, conocido por la saga The Maze Runner. Nació en Georgia y se crió en una familia numerosa. Está casado con Lynette Anderson y tienen cuatro hijos; viven en Utah.", "James Dashner" },
                    { 12, "Stephen King (1947) es un escritor estadounidense, maestro del terror y el suspense, autor de It, El resplandor y muchas otras novelas. Nació en Portland, Maine. Está casado con la escritora Tabitha Spruce desde 1971 y tienen tres hijos, Naomi, Joe y Owen.", "Stephen King" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { 1, "Fantasía" },
                    { 2, "Ciencia Ficción" },
                    { 3, "Misterio" },
                    { 4, "No Ficción" },
                    { 5, "Distopía" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "Description", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "manu@gmail.com", "123456", 2, "Manu" },
                    { 2, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "mati@gmail.com", "123456", 2, "Mati" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CoverUrl", "Pages", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1481567822i/7816926.jpg", 416, "Percy Jackson descubre que es hijo de un dios griego y debe emprender una misión para recuperar el rayo maestro de Zeus y evitar una guerra entre los dioses del Olimpo.", "El ladrón del rayo" },
                    { 2, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1451102362i/8620262.jpg", 279, "Percy y sus amigos viajan al peligroso Mar de los Monstruos para encontrar el Vellocino de Oro y salvar el Campamento Mestizo de la destrucción.", "El mar de los monstruos" },
                    { 3, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1297123292i/8267049.jpg", 352, "Percy se une a una nueva misión para rescatar a un semidiós desaparecido y a la diosa Artemisa, enfrentándose a criaturas cada vez más peligrosas.", "La maldición del titán" },
                    { 4, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1511187879i/8267056.jpg", 361, "Percy y sus aliados se adentran en el Laberinto de Dédalo para impedir que el ejército de Cronos encuentre una entrada secreta al Campamento Mestizo.", "La batalla del laberinto" },
                    { 5, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1317254851i/8267073.jpg", 381, "Percy lidera a los semidioses en la batalla final contra Cronos en las calles de Nueva York, donde se decide el destino del Olimpo.", "El último héroe del Olimpo" },
                    { 6, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1359821070i/17188896.jpg", 576, "Jason despierta sin memoria en un autobús escolar y, junto a Piper y Leo, descubre que es un semidiós y que debe rescatar a Hera para evitar una nueva amenaza.", "El héroe perdido" },
                    { 7, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1369923685i/17999618.jpg", 521, "Percy, sin recuerdos de su pasado, llega a un campamento romano y se une a Hazel y Frank en una peligrosa búsqueda para liberar a la Muerte.", "El hijo de Neptuno" },
                    { 8, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1397161148i/18209368.jpg", 586, "Siete semidioses, griegos y romanos, deben unirse para viajar a Roma, seguir la pista de la marca de Atenea y enfrentar a los gigantes de Gea.", "La marca de Atenea" },
                    { 9, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1388174908i/19452719.jpg", 597, "Mientras Percy y Annabeth luchan por sobrevivir en el Tártaro, el resto de los semidioses intenta llegar a la Casa de Hades para cerrar las Puertas de la Muerte.", "La casa de Hades" },
                    { 10, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1319186494i/12929218.jpg", 528, "Los hermanos Carter y Sadie Kane descubren que son herederos de la magia de los faraones y deben detener a un dios egipcio que amenaza con destruir el mundo.", "La pirámide roja" },
                    { 11, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1332092735i/13152321.jpg", 452, "Carter y Sadie continúan su entrenamiento mágico mientras buscan despertar al dios Ra para enfrentar a la serpiente del caos, Apofis.", "El trono de fuego" },
                    { 12, 4, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1348735646i/16049484.jpg", 406, "Los Kane deben reunir aliados y lanzar un arriesgado plan para derrotar definitivamente a Apofis antes de que sumerja al mundo en el caos.", "La sombra de la serpiente" },
                    { 13, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1376162468i/9025186.jpg", 485, "Clary Fray descubre el mundo oculto de los Cazadores de Sombras cuando presencia un asesinato que solo ella parece ver y comienza a desentrañar secretos sobre su familia.", "Ciudad de huesos" },
                    { 14, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1290632930i/8398780.jpg", 453, "Mientras el mundo subterráneo está en peligro por los planes de Valentine, Clary intenta proteger a su familia y sus amigos mientras lidia con nuevos poderes y sentimientos.", "Ciudad de cenizas" },
                    { 15, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1293514063i/8510934.jpg", 541, "Clary viaja a la ciudad de Idris en busca de una cura para su madre y se ve envuelta en una guerra que definirá el futuro de los Cazadores de Sombras y los subterráneos.", "Ciudad de cristal" },
                    { 16, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1296855516i/10388376.jpg", 425, "Tras la aparente paz, empiezan a aparecer cuerpos de Cazadores de Sombras asesinados y una nueva amenaza se cierne sobre Nueva York, poniendo a prueba las lealtades de todos.", "Ciudad de los ángeles caídos" },
                    { 17, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1343424226i/15722810.jpg", 535, "Jace ha desaparecido y parece estar ligado a Sebastian; Clary se infiltra en sus planes para salvarlo, mientras el resto del grupo busca una forma de romper ese vínculo.", "Ciudad de las almas perdidas" },
                    { 18, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1402362254i/22450140.jpg", 725, "La batalla final contra Sebastian se desata y los Cazadores de Sombras deben arriesgarlo todo para evitar la destrucción de su mundo.", "Ciudad del fuego celestial" },
                    { 19, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1324687382i/9606604.jpg", 479, "En la Londres victoriana, Tessa Gray descubre que puede cambiar de forma y se ve envuelta en una conspiración mágica junto a los Cazadores de Sombras del Instituto de Londres.", "Ángel mecánico" },
                    { 20, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1329482768i/13482202.jpg", 498, "Mientras el enemigo Mortmain se acerca a ejecutar su plan, Tessa debe enfrentarse a su pasado y a sus sentimientos por Will y Jem.", "Príncipe mecánico" },
                    { 21, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1364075211i/17670710.jpg", 567, "La trilogía de los Orígenes culmina con un enfrentamiento decisivo contra Mortmain y decisiones que marcarán a varias generaciones de Cazadores de Sombras.", "Princesa mecánica" },
                    { 22, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1459625664i/29762865.jpg", 688, "En Los Ángeles, Emma Carstairs investiga la muerte de sus padres mientras se ve envuelta en un caso de asesinatos conectados con magia oscura y secretos del pasado.", "Lady Midnight" },
                    { 23, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1498754479i/35544729.jpg", 699, "Emma y los Blackthorn deben enfrentar las consecuencias de sus decisiones, mientras nuevas fuerzas oscuras amenazan a los Cazadores de Sombras.", "El señor de las sombras" },
                    { 24, 5, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1709644904i/43382020.jpg", 893, "Tras una tragedia devastadora, el mundo de los Cazadores de Sombras se fragmenta y la familia Blackthorn debe evitar una guerra civil en el más alto nivel.", "La reina del aire y la oscuridad" },
                    { 25, 6, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1560671713l/50833391.jpg", 360, "En la Ópera de París, una misteriosa figura que habita en los pasadizos subterráneos se obsesiona con la joven soprano Christine Daaé, desencadenando una historia de amor, miedo y tragedia.", "El Fantasma de la Ópera" },
                    { 26, 7, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1328876389i/866618.jpg", 360, "Un aviador perdido en el desierto se encuentra con un pequeño príncipe venido de otro planeta, que le cuenta sus viajes por distintos mundos y le recuerda la importancia de la amistad y lo esencial.", "El Principito" },
                    { 27, 8, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1347626503i/370523.jpg", 132, "La novela recorre la historia de la familia Buendía en el pueblo ficticio de Macondo, mezclando realidad y fantasía para mostrar el peso del tiempo, la memoria y la soledad.", "100 años de soledad" },
                    { 28, 9, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1519022835i/38617139.jpg", 187, "El metódico caballero inglés Phileas Fogg apuesta que puede dar la vuelta al mundo en 80 días, emprende un viaje contrarreloj y enfrenta todo tipo de imprevistos.", "La vuelta al mundo en 80 dias" },
                    { 29, 10, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1335891621i/6596839.jpg", 374, "En un futuro distópico, Katniss Everdeen se ofrece voluntaria para reemplazar a su hermana en unos juegos televisados a muerte organizados por el Capitolio.", "Los Juegos del Hambre" },
                    { 30, 10, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1268836422i/7776692.jpg", 391, "Tras ganar los Juegos, Katniss se convierte en símbolo de rebeldía y es obligada a volver a la arena en una edición especial que enciende aún más la revolución.", "En llamas" },
                    { 31, 10, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1347835499i/8322565.jpg", 390, "Katniss se une abiertamente a la rebelión contra el Capitolio y debe decidir hasta dónde está dispuesta a llegar para cambiar el sistema.", "Sinsajo" },
                    { 32, 11, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1354770985i/9167899.jpg", 384, "Thomas despierta en un ascensor sin recuerdos y llega al Claro, rodeado por un gigantesco laberinto cambiante que él y otros chicos deben resolver para sobrevivir.", "Correr o morir" },
                    { 33, 11, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1354771494i/12043291.jpg", 362, "Tras escapar del Laberinto, Thomas y los demás son lanzados a un desierto ardiente lleno de peligros, donde deben superar una nueva prueba mortal.", "Prueba de fuego" },
                    { 34, 11, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1339216613i/15677818.jpg", 325, "Thomas se enfrenta a la verdad sobre CRUEL, las pruebas y la enfermedad llamada El Destello, en una última misión para decidir el destino de la humanidad.", "La Cura Mortal" },
                    { 35, 12, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1334416842i/830502.jpg", 1184, "Un grupo de amigos se enfrenta de niños y luego de adultos a una entidad maligna que adopta la forma de un payaso llamado Pennywise en el pueblo de Derry.", "It" },
                    { 36, 12, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1166254258i/10592.jpg", 272, "Carrie White, una adolescente marginada con poderes telequinéticos, es llevada al límite por el acoso de sus compañeros y el fanatismo de su madre.", "Carrie" },
                    { 37, 12, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1339551789i/15703424.jpg", 592, "En el corredor de la muerte de una prisión sureña, un guardia conoce a un condenado con habilidades sobrenaturales que cambian su visión sobre la justicia.", "La Milla Verde" },
                    { 38, 12, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1281058680i/3389955.jpg", 497, "Jack Torrance acepta cuidar un hotel aislado durante el invierno junto a su familia, pero fuerzas oscuras comienzan a afectar su mente.", "El Resplandor" },
                    { 39, 12, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1376955133i/18359794.jpg", 608, "Danny Torrance, ya adulto, lucha con sus traumas y su alcoholismo mientras protege a una niña con poderes psíquicos de un grupo que se alimenta de niños como ella.", "Doctor Sueño" },
                    { 40, 2, "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1507482254i/36375809.jpg", 196, "Durante un lujoso viaje en tren, el detective Hércules Poirot debe resolver el asesinato de un pasajero mientras el Orient Express está detenido por la nieve.", "Asesinato en el Orient Express" }
                });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 3 },
                    { 26, 1 },
                    { 27, 1 },
                    { 28, 1 },
                    { 29, 2 },
                    { 29, 5 },
                    { 30, 2 },
                    { 30, 5 },
                    { 31, 2 },
                    { 31, 5 },
                    { 32, 2 },
                    { 32, 5 },
                    { 33, 2 },
                    { 33, 5 },
                    { 34, 2 },
                    { 34, 5 },
                    { 35, 3 },
                    { 36, 3 },
                    { 37, 3 },
                    { 38, 3 },
                    { 39, 3 },
                    { 40, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenreId",
                table: "BookGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_BookId",
                table: "Lectures",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_UserId",
                table: "Lectures",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
