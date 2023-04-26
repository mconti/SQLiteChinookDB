﻿using SQLite;
using System.Diagnostics;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
	int count = 0;
    string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "chinook.db");
    SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.SharedCache;

    public MainPage()
	{
		InitializeComponent();
	}

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        // i file allegati all'App si possono solo leggere come stream.
        // La connection di SQLite vuole invece lavorare su un file vero.
        // E' quindi necessario copiare il file dall'interno della App nella cartella AppData

        // Il file nella cartella AddData

        if (!File.Exists(targetFile))
        {
            using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("chinook.db"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    File.WriteAllBytes(targetFile, memoryStream.ToArray());
                }
            }
        }

        SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.SharedCache;
        SQLiteAsyncConnection cn1 = new SQLiteAsyncConnection(targetFile, Flags);

        List<Artist> tblArtists;

        // Prende tutti gli artisti
        tblArtists = await cn1.QueryAsync<Artist>("select * from artists where name like 'a%'");

        CounterBtn.Text = $"In questo db ci sono {tblArtists.Count()} artisti.";
        dgDati.ItemsSource = tblArtists;
    }

    private async void dgDati_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Artist a = e.CurrentSelection[0] as Artist;
        if (a != null)
        {
            SQLiteAsyncConnection cn1 = new SQLiteAsyncConnection(targetFile, Flags);
            List<Album> tblAlbums;

            // Prende tutti gli album
            tblAlbums = await cn1.QueryAsync<Album>( $"select * from albums where artistid={a.ArtistId}");
            CounterBtn.Text = $"Dell'artista {a.Name} ci sono {tblAlbums.Count()} album.";
            dgDati.ItemsSource = tblAlbums;
        }
    }
}

