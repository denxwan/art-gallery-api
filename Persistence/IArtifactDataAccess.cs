namespace art_gallery_api.Persistence;
using art_gallery_api.Models;
public interface IArtifactDataAccess
{
    List<Artifact> GetArtifacts();
    List<Artifact> GetArtifactsInGallery();
    Artifact InsertArtifact(Artifact newArtifact);
    Artifact RemoveArtifact(int id);
    Artifact UpdateArtifact(Artifact artifact, Artifact updatedArtifact);
}