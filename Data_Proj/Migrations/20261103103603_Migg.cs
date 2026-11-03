using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Proj.Migrations
{
    /// <inheritdoc />
    public partial class Migg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoundedYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongPlaylists",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false),
                    SongId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongPlaylists", x => new { x.SongId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_SongPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongPlaylists_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongPlaylists_Songs_SongId1",
                        column: x => x.SongId1,
                        principalTable: "Songs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "FoundedYear", "Genre", "Name" },
                values: new object[,]
                {
                    { 1, 1960, "Rock", "The Beatles" },
                    { 2, 1994, "Metal", "Rammstein" },
                    { 3, 1981, "Metal", "Metallica" }
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Metal music playlist", "METAAAAAAL" },
                    { 2, "Rock music hits playlist", "Rocky" },
                    { 3, "Bulgarska muzika", "BG kupon" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "ArtistId", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, 3, 1984, "Fade To Black" },
                    { 2, 2, 2004, "Stein um Stein" },
                    { 3, 1, 1968, "Hey,Jude" }
                });

            migrationBuilder.InsertData(
                table: "SongPlaylists",
                columns: new[] { "PlaylistId", "SongId", "SongId1" },
                values: new object[,]
                {
                    { 3, 1, null },
                    { 2, 2, null },
                    { 1, 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongPlaylists_PlaylistId",
                table: "SongPlaylists",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPlaylists_SongId1",
                table: "SongPlaylists",
                column: "SongId1");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongPlaylists");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
