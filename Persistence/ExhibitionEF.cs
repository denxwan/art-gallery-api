using Npgsql;
using art_gallery_api;
using art_gallery_api.Persistence;
using art_gallery_api.Models;
using Microsoft.EntityFrameworkCore;

public class ExhibitionEF : IRepository, IExhibitionDataAccess
{
    private IRepository _repo => this;

    private GalleryContext context;

    public ExhibitionEF(GalleryContext context)
    {
        this.context = context;
    }

    public List<Exhibition> GetExhibitions()
    {
        return context.Exhibitions.ToList();
    }

    // public List<Artifact> GetSquareMapsOnly()
    // {
    //     var listOfMaps = context.Maps.Where(x => x.IsSquare==true).Select(x => x.Id);
    //     return context.Maps.Where(x => listOfMaps.Contains(x.Id)).ToList();
    // }

    public Exhibition InsertExhibition(Exhibition newExhibition)
    {
        // newArtifact.CreatedDate = DateTime.Now;
        // newArtifact.AcquiredDate = DateTime.Now;

        context.Exhibitions.Add(newExhibition);
        context.SaveChanges();
        var exhibitions = GetExhibitions();
        var result = exhibitions.Find(x => x.ExhibitionId==newExhibition.ExhibitionId);

        return result;
    }

    public Exhibition RemoveExhibition(int id)
    {
        var exhibitions = context.Exhibitions.ToList();
        Exhibition removingExhibition = exhibitions.Find(x => x.ExhibitionId==id);
        context.Exhibitions.Remove(removingExhibition);
        context.SaveChanges();
        return removingExhibition;
    }

    public Exhibition UpdateExhibition(Exhibition exhibition, Exhibition updatedExhibition)
    {
        // temp variables
        string temp_title;
        string? temp_descr = null;
        string temp_festuring_art_style;
        int? temp_expected_crowd = null;
        DateTime temp_exhibition_date;

        if(!updatedExhibition.ExhibitionTitle.Equals(exhibition.ExhibitionTitle))
        {
            temp_title = updatedExhibition.ExhibitionTitle;
        }
        else
        {
            temp_title = exhibition.ExhibitionTitle;
        }

        if(updatedExhibition.Description!=null)
        {
            if(!updatedExhibition.Description.Equals(exhibition.Description))
            {
                temp_descr = updatedExhibition.Description;
            }
            else
            {
                temp_descr = exhibition.Description;
            }
        }

        if(!updatedExhibition.FeaturingArtStyle.Equals(exhibition.FeaturingArtStyle))
        {
            temp_festuring_art_style = updatedExhibition.FeaturingArtStyle;
        }
        else
        {
            temp_festuring_art_style = exhibition.FeaturingArtStyle;
        }

        if(updatedExhibition.ExpectedCrowd!=exhibition.ExpectedCrowd)
        {
            temp_expected_crowd = updatedExhibition.ExpectedCrowd;
        }
        else
        {
            temp_expected_crowd = exhibition.ExpectedCrowd;
        }

        if(updatedExhibition.ExhibitionDate!=exhibition.ExhibitionDate)
        {
            temp_exhibition_date = updatedExhibition.ExhibitionDate;
        }
        else
        {
            temp_exhibition_date = exhibition.ExhibitionDate;
        }

        var searchExhibition = context.Exhibitions.FirstOrDefault(x => x.ExhibitionId==exhibition.ExhibitionId);

        if(searchExhibition!=null)
        {
            searchExhibition.ExhibitionTitle = temp_title;
            searchExhibition.Description = temp_descr;
            searchExhibition.FeaturingArtStyle = temp_festuring_art_style;
            searchExhibition.ExpectedCrowd = temp_expected_crowd;
            searchExhibition.ExhibitionDate = temp_exhibition_date;
        }
        
        context.SaveChanges();

        return searchExhibition;
    }
}