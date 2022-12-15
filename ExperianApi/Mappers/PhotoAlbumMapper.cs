using ExperianApi.Interfaces;
using ExperianApi.Models.jsonplaceholder;
using ExperianApi.Models.Photos;
using ExperianApi.Models.Response.PhotoAlbum;
using System.Collections.Generic;
using System.Linq;

namespace ExperianApi.Mappers
{
    public class PhotoAlbumMapper: IPhotoAlbumMapper
    {
        public List<Album> MapPhotosToAlbums(AlbumResponse albumResponse,PhotoResponse photoResponse) 
        {
            List<Album> albums = new List<Album>();

            albumResponse.Albums = albumResponse.Albums.OrderBy(x => x.Id).ToList();
            photoResponse.Photos = photoResponse.Photos.OrderBy(x => x.AlbumId).ToList();

            for(var i=0; i < albumResponse.Albums.Count; i++)
            {
                albums.Add(this.MapAlbumFromApiResponse(albumResponse.Albums[i]));

                foreach (JP_Photo photo in photoResponse.Photos.ToList())
                {
                    if(photo.AlbumId == albums[i].AlbumId)
                    {
                        albums[i].AlbumPhotos.Add(this.MapPhotoFromApiResponse(photo));
                        photoResponse.Photos.Remove(photo);
                    }
                }
            }

            return albums;
        }

        public Album MapAlbumFromApiResponse(JP_Album album)
        {
            Album mappedAlbum = new Album
            {
                AlbumId = album.Id,
                UserId = album.UserId,
                AlbumTitle = album.Title,
            };

            return mappedAlbum;
        }

        public Photo MapPhotoFromApiResponse(JP_Photo photo)
        {
            Photo mappedPhoto = new Photo
            {
                PhotoId = photo.Id,
                AlbumId = photo.AlbumId,
                PhotoTitle = photo.Title,
                PhotoThumbnailUrl = photo.ThumbnailUrl,
                PhotoUrl = photo.Url
            };

            return mappedPhoto;
        }
    }
}
