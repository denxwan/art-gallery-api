using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using art_gallery_api.Persistence;
using art_gallery_api.Models;

namespace art_gallery_api.Controllers;

[ApiController]
[Route("api/artifacts")]
public class ArtifactsController : ControllerBase
{
    // Dependency Injection

    private readonly IArtifactDataAccess _artifactsRepo;
    public ArtifactsController(IArtifactDataAccess artifactRepo)
    {
        _artifactsRepo = artifactRepo;
    }

    // Endpoints here

    // -- SELECT COMMANDS --

    [HttpGet()]
    public IEnumerable<Artifact> GetArtifacts()
    {
        return _artifactsRepo.GetArtifacts();
    }

    [HttpGet("in-gallery")]
    public IEnumerable<Artifact> GetArtifactsInGallery()
    {
        return _artifactsRepo.GetArtifactsInGallery();
    }

    [HttpGet("{id}", Name = "GetArtifact")]
    public IActionResult GetArtifactById(int id)
    {
        foreach (var artifact in _artifactsRepo.GetArtifacts())
        {
            if (artifact.ArtifactId == id)
            {
                return Ok(artifact);
            }
        }
        return NotFound();
    }

    // -- INSERT COMMANDS --

    [HttpPost()]
    public IActionResult AddArtifact(Artifact newArtifact)
    {
        if(newArtifact == null)
        {
            return BadRequest();
        }

        foreach (var artifact in _artifactsRepo.GetArtifacts())
        {
            if(newArtifact.ArtifactTitle.Equals(artifact.ArtifactTitle))
            {
                return Conflict();
            }
        }

        Artifact result = _artifactsRepo.InsertArtifact(newArtifact);

        return Created("GetArtifact", result);
    }

    // -- UPDATE COMMANDS --

    [HttpPut("{id}")]
    public IActionResult UpdateArtifact(int id, Artifact updatedArtifact)
    {
        foreach (var artifact in _artifactsRepo.GetArtifacts())
        {
            if (artifact.ArtifactId == id)
            {
                Artifact result;

                try
                {
                    result = _artifactsRepo.UpdateArtifact(artifact, updatedArtifact);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    return BadRequest();
                }

                return Ok(result);
            }
        }
        return NotFound();
    }

    // -- DELETE COMMANDS --

    [HttpDelete("{id}")]
    public IActionResult DeleteArtifact(int id)
    {
        foreach (var artifact in _artifactsRepo.GetArtifacts())
        {
            if (artifact.ArtifactId == id)
            {
                _artifactsRepo.RemoveArtifact(id);
                return Ok();
            }
        }
        return NotFound();
    }
}
