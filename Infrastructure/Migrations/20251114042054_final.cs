using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.bosshunting.com.au/wp-content/uploads/2020/05/t1.jpg");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://tse3.mm.bing.net/th/id/OIP.V15M1N_UMTof6UzO94jlgQHaJj?cb=ucfimg2ucfimg=1&rs=1&pid=ImgDetMain&o=7&rm=3");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://bibliotecasma.org/wp-content/uploads/2022/11/George-Orwell-1-1.png");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://i.pinimg.com/originals/73/3b/86/733b8638810a7dfcd4160db157128875.gif");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://tse1.mm.bing.net/th/id/OIP.sP-1mw0awgaUEQ5ahDUpFAHaLG?cb=ucfimg2ucfimg=1&rs=1&pid=ImgDetMain&o=7&rm=3");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://tse4.mm.bing.net/th/id/OIP.wzijbixIFiITPjzvgI4BJAAAAA?cb=ucfimg2ucfimg=1&rs=1&pid=ImgDetMain&o=7&rm=3");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.fe8ff145ddb31643dadfeac3f4ee2107?rik=fnhs31gxkzhg3A&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.364745447bbd86b72f843d5b9b26538a?rik=FwNy7zKQi9Co2w&riu=http%3a%2f%2f2.bp.blogspot.com%2f-JKg3gXA6QzM%2fU_0_S622fJI%2fAAAAAAAAABc%2ffwJBk8OvqGY%2fs1600%2fmarquez-gabriel-adv-obit-slide-lp84-superjumbo.jpg&ehk=5UOdn%2bobrrpQBErBABnnsD0TO1WefGcBUjERzkwwxzw%3d&risl=&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.de731caf0809d43f69366c90de317c08?rik=V%2fBPn4FKPKT2Sw&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.01cc2a03a16a4ca2716b27ce98df419f?rik=%2bj0SHM%2fqlk2f6w&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "https://m.media-amazon.com/images/S/amzn-author-media-prod/c0iiih99mtos73tgn0ahtjlrjk._SY600_.jpg");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "https://tse2.mm.bing.net/th/id/OIP.ZPALPbO3ohjPHAId4pkK3wHaEK?cb=ucfimg2ucfimg=1&rs=1&pid=ImgDetMain&o=7&rm=3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "Manu123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Mati123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "123456");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "123456");
        }
    }
}
