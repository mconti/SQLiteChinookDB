using System;
using System.Collections.Generic;
using SQLite;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

try {
    
    // Connessione al db
    SQLiteConnection cn1 = new SQLiteConnection("chinook.db");
    
    List<Album> tblAlbums;
    List<Artist> tblArtists;
    List<Track> tblTracks; 

    // Prende tutti gli artisti
    tblArtists = cn1.Query<Artist>( "select * from artists" );  
    Console.WriteLine( $"In questo db ci sono {tblArtists.Count()} artisti." );

    // Versione non ottimizzata
    foreach( var artista in tblArtists )
    {
        Console.WriteLine( $"{artista.ArtistId}-{artista.Name}" );

        tblAlbums = cn1.Query<Album>( "select * from Albums" );  
        foreach( var album in tblAlbums )
        {
            if( artista.ArtistId == album.ArtistId)
                Console.WriteLine( $"\t-{album.AlbumId}-{album.Title}" );
        }
    }

    // Versione corretta ma sfrutta SQL
    foreach( var artista in tblArtists )
    {
        Console.WriteLine( $"{artista.ArtistId}-{artista.Name}" );

        tblAlbums = cn1.Query<Album>( "select * from Albums where ArtistId==" + artista.ArtistId );  
        foreach( var album in tblAlbums )
        {
            Console.WriteLine( $"\t{album.AlbumId}-{album.Title}" );
        }
    }

    // Elenco delle tracce
    int AlbumId = 232;  // Achtung baby
    
    tblAlbums = cn1.Query<Album>( "select * from Albums where AlbumId==" + AlbumId );  
    Console.WriteLine( $"{tblAlbums[0].Title}" );

    int ArtistId = tblAlbums[0].ArtistId;  // U2
    tblArtists = cn1.Query<Artist>( "select * from Artists where ArtistId==" + ArtistId );  
    Console.WriteLine( $"{tblArtists[0].Name}" );

    tblTracks = cn1.Query<Track>( "select * from Tracks where AlbumId==" + AlbumId );  
    foreach( var traccia in tblTracks )
    {
        Console.WriteLine( $"{traccia.UnitPrice}\t{traccia.Name}" );
    }


}
catch(Exception Err)
{
    Console.WriteLine( Err.Message );
}
