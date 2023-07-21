using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        BindingSource albumsBindingSource = new BindingSource();
        BindingSource tracksBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent(); 
        }

        public void button1_Click(object sender, EventArgs e)
        {

            AlbumsDAO albumsDAO = new AlbumsDAO();

            albumsBindingSource.DataSource = albumsDAO.getAllAlbums();
            // connect list to grid view.
            dataGridView1.DataSource = albumsBindingSource;
        }

        public void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            
            albumsBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text); // textBox1 var name, attached to button, can be attached to differnet buttuns as well.
            // connect list to grid view.
            dataGridView1.DataSource = albumsBindingSource;
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            // get row number clicked. 

            int rowClicked = dataGridView.CurrentRow.Index;
           
            String ImageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            
            pictureBox1.Load(ImageURL);

            AlbumsDAO albumsDAO = new AlbumsDAO();
            tracksBindingSource.DataSource = albumsDAO.getTracksForAlbum((int)dataGridView.Rows[rowClicked].Cells[0].Value);
            // connect list to grid view.
            dataGridView2.DataSource = tracksBindingSource;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // add all new items. 
            Album album = new Album
            {
                AlbumName = txt_album.Text,
                ArtistName = txt_artist.Text,
                Year = Int32.Parse(txt_year.Text), // will convert the string into an int since the data base expect an int 
                ImageURL = txt_imgURL.Text,
                description = txt_dececription.Text
            };
            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
             MessageBox.Show("sucess adding to database.");
            
        }
    }
}
    

