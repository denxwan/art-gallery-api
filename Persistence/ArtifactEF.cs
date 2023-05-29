using Npgsql;
using art_gallery_api;
using art_gallery_api.Persistence;
using art_gallery_api.Models;
using Microsoft.EntityFrameworkCore;

public class ArtifactEF : IRepository, IArtifactDataAccess
{
    private IRepository _repo => this;

    private GalleryContext context;

    public ArtifactEF(GalleryContext context)
    {
        this.context = context;
    }

    public List<Artifact> GetArtifacts()
    {
        return context.Artifacts.ToList();
    }

    // public List<Artifact> GetSquareMapsOnly()
    // {
    //     var listOfMaps = context.Maps.Where(x => x.IsSquare==true).Select(x => x.Id);
    //     return context.Maps.Where(x => listOfMaps.Contains(x.Id)).ToList();
    // }

    public List<Artifact> GetArtifactsInGallery()
    {
        var listOfArtifacts = context.Artifacts.Where(x => x.IsInGallery==true).Select(x=> x.ArtifactId);
        return context.Artifacts.Where(x => listOfArtifacts.Contains(x.ArtifactId)).ToList();
    }

    public Artifact InsertArtifact(Artifact newArtifact)
    {
        // newArtifact.CreatedDate = DateTime.Now;
        // newArtifact.AcquiredDate = DateTime.Now;
        newArtifact.AddedDate = DateTime.Now;
        context.Artifacts.Add(newArtifact);
        context.SaveChanges();
        var artifacts = GetArtifacts();
        var result = artifacts.Find(x => x.ArtifactId==newArtifact.ArtifactId);

        return result;
    }

    public Artifact RemoveArtifact(int id)
    {
        var artifacts = context.Artifacts.ToList();
        Artifact removingArtifact = artifacts.Find(x => x.ArtifactId==id);
        context.Artifacts.Remove(removingArtifact);
        context.SaveChanges();
        return removingArtifact;
    }

    public Artifact UpdateArtifact(Artifact artifact, Artifact updatedArtifact)
    {
        // temp variables
        string temp_title;
        string? temp_descr = null;
        string temp_artist_id;
        string temp_style_id;
        DateOnly temp_created_year;
        DateOnly temp_acquired_year;
        bool temp_is_in_gallery;

        if(!updatedArtifact.ArtifactTitle.Equals(artifact.ArtifactTitle))
        {
            temp_title = updatedArtifact.ArtifactTitle;
        }
        else
        {
            temp_title = artifact.ArtifactTitle;
        }

        if(updatedArtifact.Description!=null)
        {
            if(!updatedArtifact.Description.Equals(artifact.Description))
            {
                temp_descr = updatedArtifact.Description;
            }
            else
            {
                temp_descr = artifact.Description;
            }
        }

        if(!updatedArtifact.StyleId.Equals(artifact.StyleId))
        {
            temp_style_id = updatedArtifact.StyleId;
        }
        else
        {
            temp_style_id = artifact.StyleId;
        }

        if(updatedArtifact.ArtistId!=artifact.ArtistId)
        {
            temp_artist_id = updatedArtifact.ArtistId;
        }
        else
        {
            temp_artist_id = artifact.ArtistId;
        }

        if(updatedArtifact.IsInGallery!=artifact.IsInGallery)
        {
            temp_is_in_gallery = updatedArtifact.IsInGallery;
        }
        else
        {
            temp_is_in_gallery = artifact.IsInGallery;
        }

        var searchArtifact = context.Artifacts.FirstOrDefault(x => x.ArtifactId==artifact.ArtifactId);

        if(searchArtifact!=null)
        {
            searchArtifact.ArtifactTitle = temp_title;
            searchArtifact.Description = temp_descr;
            searchArtifact.ArtistId = temp_artist_id;
            searchArtifact.StyleId = temp_style_id;
            searchArtifact.AddedDate = DateTime.Now;
            searchArtifact.IsInGallery = temp_is_in_gallery;
        }
        
        context.SaveChanges();

        return searchArtifact;
    }
}