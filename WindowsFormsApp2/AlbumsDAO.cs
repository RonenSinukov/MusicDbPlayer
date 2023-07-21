using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class AlbumsDAO
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=music"; // connection to the MySql server. 
        /**
         * method to retrive all albums. 
         */
        public List<Album> getAllAlbums() // initilize the list and the method to return the list of albums.
        {
            List<Album> returnthese = new List<Album>();
            // connection syntax to connect to the server. 
            MySqlConnection connection = new MySqlConnection(connectionString); // connction string user name. passowrd, port, database, datasource.
            connection.Open();
            // define the sql statemnt to fetch() the data. 
            MySqlCommand command = new MySqlCommand("SELECT * FROM albums", connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) // loop to read so while read.Read();
                {
                    Album a = new Album // new list 
                    {
                        // list should be resebled by data in server.
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        description = reader.GetString(5),
                    };
                    returnthese.Add(a);
                }
            }
            connection.Close();

            return returnthese;
        }
        /*
         method to retrive albums names with the (searchTerm) in the name; 
         */
        public List<Album> searchTitles(String seatchTerm) // initilize the list and the method to return the list of albums.
        {
            List<Album> returnthese = new List<Album>();
            // connection syntax to connect to the server. 
            MySqlConnection connection = new MySqlConnection(connectionString); // connction string user name. passowrd, port, database, datasource. connect to dataCenter. 
            connection.Open();
            // define the sql statemnt to fetch() the data. 
            string searchWildPharse = "%" + seatchTerm + "%";
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT ID, ALBUM_TITLE, ARTIST, YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUMS WHERE ALBUM_TITLE LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPharse);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) // loop to read so while read.Read();
                {
                    Album a = new Album // new node 
                    {
                        // list should be resebled by data in server.
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        description = reader.GetString(5),
                    };
                    returnthese.Add(a);
                }
            }
            connection.Close(); // close connection. 

            return returnthese;
        }

        internal int addOneAlbum(Album album)
        {
           
            // connection syntax to connect to the server. 
            MySqlConnection connection = new MySqlConnection(connectionString); // connction string user name. passowrd, port, database, datasource.
            connection.Open();
            // define the sql statemnt to fetch() the data. 
            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`( `ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`," +
                " `DESCRIPTION`) VALUES (@albumtitle,@artist,@year,@imgurl,@desecription)", connection);
            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);
            command.Parameters.AddWithValue("@artist", album.ArtistName);
            command.Parameters.AddWithValue("@year", album.Year);
            command.Parameters.AddWithValue("@imgurl", album.ImageURL);
            command.Parameters.AddWithValue("@desecription", album.description);
            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;
        }

        /*
         * fetch all data from the album(tracks); 
         */
        public List<Track> getTracksForAlbum(int albumID) // initilize the list and the method to return the list of albums.
        {
            List<Track> returnthese = new List<Track>();
            // connection syntax to connect to the server. 
            MySqlConnection connection = new MySqlConnection(connectionString); // connction string user name. passowrd, port, database, datasource. connect to dataCenter. 
            connection.Open();
       
           
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM TRACKS WHERE albums_ID =@albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) // loop to read so while read.Read();
                {
                    Track t = new Track // new node 
                    {
                        // list should be resebled by data in server.
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4)
                    };
                    returnthese.Add(t);
                }
            }
            connection.Close(); // close connection. 

            return returnthese;
        }

    }
    }

