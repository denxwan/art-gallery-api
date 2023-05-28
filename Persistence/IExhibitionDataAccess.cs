namespace art_gallery_api.Persistence;
using art_gallery_api.Models;
public interface IExhibitionDataAccess
{
    List<Exhibition> GetExhibitions();
    // List<Artifact> GetSquareMapsOnly();
    Exhibition InsertExhibition(Exhibition newExhibition);
    Exhibition RemoveExhibition(int id);
    Exhibition UpdateExhibition(Exhibition exhibition, Exhibition updatedExhibition);
}